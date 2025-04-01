using AgencyPlatform.Application.DTOs.Auth;
using AgencyPlatform.Application.Interfaces.Services;
using AgencyPlatform.Infrastructure.Data.Entities;
using AgencyPlatform.Infrastructure.Repositories;
using AgencyPlatform.Shared.Helpers;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Http;
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

        public UserService(IUserRepository userRepository, IConfiguration config, IHttpContextAccessor httpContextAccessor)
        {
            _userRepository = userRepository;
            _config = config;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<AuthResponseDto> RegisterAsync(RegisterRequestDto dto)
        {
            var existingUser = await _userRepository.GetByEmailAsync(dto.Email);
            if (existingUser is not null)
                throw new ArgumentException("Este correo ya está registrado. Intenta con otro diferente.");

            var salt = PasswordHasher.GenerateSalt();
            var hashedPassword = PasswordHasher.HashPassword(dto.Password, salt);
            var ip = _httpContextAccessor.HttpContext?.Connection?.RemoteIpAddress?.ToString() ?? "127.0.0.1";
            var now = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Unspecified);

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

            await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAsync();

            return GenerateToken(user);
        }

        public async Task<AuthResponseDto> LoginAsync(LoginRequestDto dto)
        {
            var user = await _userRepository.GetByEmailAsync(dto.Email);
            if (user is null)
                throw new UnauthorizedAccessException("Usuario no encontrado. Revisa tu correo o regístrate.");

            if (!PasswordHasher.VerifyPassword(dto.Password, user.contrasena!, user.salt!))
                throw new UnauthorizedAccessException("La contraseña ingresada no es válida.");

            user.ultimo_ip = _httpContextAccessor.HttpContext?.Connection?.RemoteIpAddress?.ToString();
            user.ultimo_login = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Unspecified);
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
                new Claim(ClaimTypes.NameIdentifier, user.id_usuario.ToString()),
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
    }
}
