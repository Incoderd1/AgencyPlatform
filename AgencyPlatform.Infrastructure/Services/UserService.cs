using AgencyPlatform.Application.DTOs.Auth;
using AgencyPlatform.Application.DTOs;
using AgencyPlatform.Application.Interfaces.Services;
using AgencyPlatform.Infrastructure.Data.Entities;
using AgencyPlatform.Infrastructure.Repositories;
using AgencyPlatform.Shared.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AgencyPlatform.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _config;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IEmailSender _emailSender;

        public UserService(IUserRepository userRepository, IConfiguration config, IHttpContextAccessor httpContextAccessor, IEmailSender emailSender)
        {
            _userRepository = userRepository;
            _config = config;
            _httpContextAccessor = httpContextAccessor;
            _emailSender = emailSender;
        }

        public async Task<AuthResponseDto> RegisterAsync(RegisterRequestDto dto)
        {
            var existingUser = await _userRepository.GetByEmailAsync(dto.Email);
            if (existingUser != null)
                throw new ArgumentException("Este correo ya está registrado. Intenta con otro diferente.");

            var salt = PasswordHasher.GenerateSalt();
            var hashedPassword = PasswordHasher.HashPassword(dto.Password, salt);
            var ip = _httpContextAccessor.HttpContext?.Connection?.RemoteIpAddress?.ToString() ?? "127.0.0.1";
            var now = DateTime.UtcNow;

            var user = new usuario
            {
                uuid = Guid.NewGuid(),
                email = dto.Email,
                contrasena = hashedPassword,
                salt = salt,
                tipo_usuario = dto.TipoUsuario ?? "cliente",
                metodo_auth = "password",
                factor_2fa = false,
                estado = "pendiente",
                verificado_email = false,
                fecha_registro = now,
                fecha_actualizacion = now,
                ip_registro = ip,
                ultimo_ip = ip
            };

            var token = Guid.NewGuid().ToString("N");
            user.token_verificacion = token;
            user.fecha_expiracion_token = now.AddHours(24);

            await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAsync();

            var verificationLink = $"{_config["Frontend:BaseUrl"]}/verificar?token={token}";
            await _emailSender.SendEmailAsync(user.email!, "Verifica tu cuenta",
                $"Hola! Gracias por registrarte. Verifica tu correo haciendo clic aquí: <a href='{verificationLink}'>Verificar cuenta</a>");

            return GenerateToken(user);
        }

        public async Task<AuthResponseDto> LoginAsync(LoginRequestDto dto)
        {
            var user = await _userRepository.GetByEmailAsync(dto.Email);
            if (user == null)
                throw new UnauthorizedAccessException("Usuario no encontrado. Revisa tu correo o regístrate.");

            if (!PasswordHasher.VerifyPassword(dto.Password, user.contrasena!, user.salt!))
                throw new UnauthorizedAccessException("La contraseña ingresada no es válida.");

            user.ultimo_ip = _httpContextAccessor.HttpContext?.Connection?.RemoteIpAddress?.ToString();
            user.ultimo_login = DateTime.UtcNow;
            await _userRepository.SaveChangesAsync();

            return GenerateToken(user);
        }

        private AuthResponseDto GenerateToken(usuario user)
        {
            var jwtKey = _config["Jwt:Key"]!;
            var jwtIssuer = _config["Jwt:Issuer"]!;
            var jwtExpireMinutes = int.Parse(_config["Jwt:ExpireMinutes"]!);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim("id", user.id_usuario.ToString()),
                new Claim(ClaimTypes.Email, user.email!),
                new Claim(ClaimTypes.Role, user.tipo_usuario!)
            };

            var token = new JwtSecurityToken(
                issuer: jwtIssuer,
                audience: jwtIssuer,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(jwtExpireMinutes),
                signingCredentials: creds
            );

            return new AuthResponseDto
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = token.ValidTo,
                Email = user.email!,
                TipoUsuario = user.tipo_usuario!
            };
        }

        public async Task<string> ConfirmEmailAsync(string token, int? userId = null)
        {
            // Si se proporciona un userId, verifica directamente por ID
            if (userId.HasValue)
            {
                var userById = await _userRepository.GetByIdAsync(userId.Value);
                if (userById == null)
                    throw new ApplicationException("Usuario no encontrado.");

                if (userById.verificado_email)
                    return "El correo ya está verificado.";

                userById.verificado_email = true;
                userById.estado = "activo";
                userById.fecha_actualizacion = DateTime.UtcNow;

                await _userRepository.SaveChangesAsync();

                return "Correo verificado correctamente.";
            }

            // Si no se proporciona un userId, verifica por token como antes
            var user = await _userRepository.Query()
                .FirstOrDefaultAsync(u => u.token_verificacion == token && !u.verificado_email);

            if (user == null)
                throw new ApplicationException("Token inválido o expirado.");

            if (user.fecha_expiracion_token < DateTime.UtcNow)
                throw new ApplicationException("El token ha expirado.");

            user.verificado_email = true;
            user.token_verificacion = null;
            user.fecha_expiracion_token = null;
            user.estado = "activo";
            user.fecha_actualizacion = DateTime.UtcNow;

            await _userRepository.SaveChangesAsync();

            return "Correo verificado correctamente.";
        }

        public async Task ResendVerificationEmailAsync(string email)
        {
            var user = await _userRepository.GetByEmailAsync(email);

            if (user == null)
                throw new ApplicationException("No se encontró ningún usuario con ese correo.");

            if (user.verificado_email)
                throw new ApplicationException("Este correo ya ha sido verificado.");

            user.token_verificacion = TokenGenerator.GenerateToken();
            user.fecha_expiracion_token = DateTime.UtcNow.AddHours(2);
            user.fecha_actualizacion = DateTime.UtcNow;

            await _userRepository.SaveChangesAsync();

            var verificationLink = $"{_config["Frontend:BaseUrl"]}/verificar?token={user.token_verificacion}";

            await _emailSender.SendEmailAsync(user.email!, "Reenvío de verificación",
                $"Hola! Reenvío del enlace para verificar tu correo: <a href='{verificationLink}'>Verificar ahora</a>");
        }

        public async Task ForgotPasswordAsync(ForgotPasswordRequestDto dto)
        {
            var user = await _userRepository.GetByEmailAsync(dto.Email);
            if (user == null)
                throw new ApplicationException("El correo no está registrado.");

            user.token_verificacion = TokenGenerator.GenerateToken();
            user.fecha_expiracion_token = DateTime.UtcNow.AddHours(2);
            user.fecha_actualizacion = DateTime.UtcNow;

            await _userRepository.SaveChangesAsync();

            var resetLink = $"{_config["Frontend:BaseUrl"]}/reset-password?token={user.token_verificacion}";

            await _emailSender.SendEmailAsync(user.email!, "Recuperación de contraseña",
                $"<p>Haz clic en el siguiente enlace para restablecer tu contraseña:</p><a href='{resetLink}'>Restablecer ahora</a>");
        }

        public async Task ResetPasswordAsync(ResetPasswordRequestDto dto)
        {
            var user = await _userRepository.Query()
                .FirstOrDefaultAsync(u => u.token_verificacion == dto.Token);

            if (user == null)
                throw new ApplicationException("Token inválido o expirado.");

            if (user.fecha_expiracion_token < DateTime.UtcNow)
                throw new ApplicationException("El token ha expirado.");

            var salt = PasswordHasher.GenerateSalt();
            user.contrasena = PasswordHasher.HashPassword(dto.NewPassword, salt);
            user.salt = salt;

            user.token_verificacion = null;
            user.fecha_expiracion_token = null;
            user.fecha_actualizacion = DateTime.UtcNow;

            await _userRepository.SaveChangesAsync();
        }

        public async Task<PaginatedResultDto<UserDto>> GetUsersAsync(int page, int pageSize)
        {
            // Validar los parámetros
            if (page < 1)
                page = 1;
            if (pageSize < 1 || pageSize > 100)
                pageSize = 10;

            // Calcular el número de registros a omitir
            var skip = (page - 1) * pageSize;

            // Obtener el total de registros
            var totalItems = await _userRepository.Query().CountAsync();

            // Obtener los usuarios paginados
            var users = await _userRepository.Query()
                .OrderByDescending(u => u.fecha_registro)
                .Skip(skip)
                .Take(pageSize)
                .Select(u => new UserDto
                {
                    Id = u.id_usuario,
                    Email = u.email,
                    TipoUsuario = u.tipo_usuario,
                    Estado = u.estado,
                    VerificadoEmail = u.verificado_email,
                    Factor2FA = u.factor_2fa,
                    FechaRegistro = u.fecha_registro,
                    UltimoLogin = u.ultimo_login
                })
                .ToListAsync();

            // Calcular el número total de páginas
            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            // Crear y devolver el resultado paginado
            return new PaginatedResultDto<UserDto>
            {
                Items = users,
                Page = page,
                PageSize = pageSize,
                TotalItems = totalItems,
                TotalPages = totalPages,
                HasPreviousPage = page > 1,
                HasNextPage = page < totalPages
            };
        }
    }
}