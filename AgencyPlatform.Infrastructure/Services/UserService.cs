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

            var user = new Usuario
            {
                Uuid = Guid.NewGuid(),
                Email = dto.Email,
                Contrasena = hashedPassword,
                Salt = salt,
                TipoUsuario = dto.TipoUsuario ?? "cliente",
                MetodoAuth = "password",
                Factor2fa = false,
                Estado = "pendiente",
                VerificadoEmail = false,
                FechaRegistro = now,
                FechaActualizacion = now,
                IpRegistro = ip,
                UltimoIp = ip
            };

            var token = Guid.NewGuid().ToString("N");
            user.TokenVerificacion = token;
            user.FechaExpiracionToken = now.AddHours(24);

            await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAsync();

            var verificationLink = $"{_config["Frontend:BaseUrl"]}/verificar?token={token}";
            await _emailSender.SendEmailAsync(user.Email!, "Verifica tu cuenta",
                $"Hola! Gracias por registrarte. Verifica tu correo haciendo clic aquí: <a href='{verificationLink}'>Verificar cuenta</a>");

            return GenerateToken(user);
        }

        public async Task<AuthResponseDto> LoginAsync(LoginRequestDto dto)
        {
            var user = await _userRepository.GetByEmailAsync(dto.Email);
            if (user == null)
                throw new UnauthorizedAccessException("Usuario no encontrado. Revisa tu correo o regístrate.");

            if (!PasswordHasher.VerifyPassword(dto.Password, user.Contrasena!, user.Salt!))
                throw new UnauthorizedAccessException("La contraseña ingresada no es válida.");

            user.UltimoIp = _httpContextAccessor.HttpContext?.Connection?.RemoteIpAddress?.ToString();
            user.UltimoLogin = DateTime.UtcNow;
            await _userRepository.SaveChangesAsync();

            return GenerateToken(user);
        }

        private AuthResponseDto GenerateToken(Usuario user)
        {
            var jwtKey = _config["Jwt:Key"]!;
            var jwtIssuer = _config["Jwt:Issuer"]!;
            var jwtExpireMinutes = int.Parse(_config["Jwt:ExpireMinutes"]!);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim("id", user.IdUsuario.ToString()),
                new Claim(ClaimTypes.Email, user.Email!),
                new Claim(ClaimTypes.Role, user.TipoUsuario!)
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
                Email = user.Email!,
                TipoUsuario = user.TipoUsuario!
            };
        }

        public async Task<string> ConfirmEmailAsync(string token, int? userId = null)
        {
            if (userId.HasValue)
            {
                var userById = await _userRepository.GetByIdAsync(userId.Value);
                if (userById == null)
                    throw new ApplicationException("Usuario no encontrado.");

                if (userById.VerificadoEmail)
                    return "El correo ya está verificado.";

                userById.VerificadoEmail = true;
                userById.Estado = "activo";
                userById.FechaActualizacion = DateTime.UtcNow;

                await _userRepository.SaveChangesAsync();

                return "Correo verificado correctamente.";
            }

            var user = await _userRepository.Query()
                .FirstOrDefaultAsync(u => u.TokenVerificacion == token && !u.VerificadoEmail);

            if (user == null)
                throw new ApplicationException("Token inválido o expirado.");

            if (user.FechaExpiracionToken < DateTime.UtcNow)
                throw new ApplicationException("El token ha expirado.");

            user.VerificadoEmail = true;
            user.TokenVerificacion = null;
            user.FechaExpiracionToken = null;
            user.Estado = "activo";
            user.FechaActualizacion = DateTime.UtcNow;

            await _userRepository.SaveChangesAsync();

            return "Correo verificado correctamente.";
        }

        public async Task ResendVerificationEmailAsync(string email)
        {
            var user = await _userRepository.GetByEmailAsync(email);

            if (user == null)
                throw new ApplicationException("No se encontró ningún usuario con ese correo.");

            if (user.VerificadoEmail)
                throw new ApplicationException("Este correo ya ha sido verificado.");

            user.TokenVerificacion = TokenGenerator.GenerateToken();
            user.FechaExpiracionToken = DateTime.UtcNow.AddHours(2);
            user.FechaActualizacion = DateTime.UtcNow;

            await _userRepository.SaveChangesAsync();

            var verificationLink = $"{_config["Frontend:BaseUrl"]}/verificar?token={user.TokenVerificacion}";

            await _emailSender.SendEmailAsync(user.Email!, "Reenvío de verificación",
                $"Hola! Reenvío del enlace para verificar tu correo: <a href='{verificationLink}'>Verificar ahora</a>");
        }

        public async Task ForgotPasswordAsync(ForgotPasswordRequestDto dto)
        {
            var user = await _userRepository.GetByEmailAsync(dto.Email);
            if (user == null)
                throw new ApplicationException("El correo no está registrado.");

            user.TokenVerificacion = TokenGenerator.GenerateToken();
            user.FechaExpiracionToken = DateTime.UtcNow.AddHours(2);
            user.FechaActualizacion = DateTime.UtcNow;

            await _userRepository.SaveChangesAsync();

            var resetLink = $"{_config["Frontend:BaseUrl"]}/reset-password?token={user.TokenVerificacion}";

            await _emailSender.SendEmailAsync(user.Email!, "Recuperación de contraseña",
                $"<p>Haz clic en el siguiente enlace para restablecer tu contraseña:</p><a href='{resetLink}'>Restablecer ahora</a>");
        }

        public async Task ResetPasswordAsync(ResetPasswordRequestDto dto)
        {
            var user = await _userRepository.Query()
                .FirstOrDefaultAsync(u => u.TokenVerificacion == dto.Token);

            if (user == null)
                throw new ApplicationException("Token inválido o expirado.");

            if (user.FechaActualizacion < DateTime.UtcNow)
                throw new ApplicationException("El token ha expirado.");

            var salt = PasswordHasher.GenerateSalt();
            user.Contrasena = PasswordHasher.HashPassword(dto.NewPassword, salt);
            user.Salt = salt;

            user.TokenVerificacion = null;
            user.FechaExpiracionToken = null;
            user.FechaActualizacion = DateTime.UtcNow;

            await _userRepository.SaveChangesAsync();
        }

        public async Task<PaginatedResult<UserDto>> GetUsersAsync(int page, int pageSize)
        {
            if (page < 1)
                page = 1;
            if (pageSize < 1 || pageSize > 100)
                pageSize = 10;

            var query = _userRepository.Query()
                .OrderByDescending(u => u.FechaRegistro)
                .Select(u => new UserDto
                {
                    Id = u.IdUsuario,
                    Email = u.Email,
                    TipoUsuario = u.TipoUsuario,
                    Estado = u.Estado,
                    VerificadoEmail = u.VerificadoEmail,
                    Factor2FA = u.Factor2fa,
                    FechaRegistro = u.FechaRegistro,
                    UltimoLogin = u.UltimoLogin
                });

            return await PaginationHelper.CreateAsync(query, page, pageSize);
        }

    }
}
