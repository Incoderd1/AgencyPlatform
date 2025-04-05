using AgencyPlatform.Application.DTOs;
using AgencyPlatform.Application.DTOs.Auth;
using AgencyPlatform.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgencyPlatform.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto dto)
        {
            var response = await _userService.RegisterAsync(dto);
            // Enviar correo de verificación automáticamente después del registro
            await _userService.ResendVerificationEmailAsync(dto.Email);
            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto dto)
        {
            var response = await _userService.LoginAsync(dto);
            return Ok(response);
        }

        [Authorize]
        [HttpGet("me")]
        public IActionResult Me()
        {
            var userId = User.FindFirst("id")?.Value;
            var email = User.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value;
            var role = User.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value;

            return Ok(new
            {
                Id = userId,
                Email = email,
                TipoUsuario = role
            });
        }

        [HttpGet("users")]
        public async Task<IActionResult> GetUsers([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _userService.GetUsersAsync(page, pageSize);
            return Ok(result);
        }

        [HttpPost("confirm-email")]
        public async Task<IActionResult> ConfirmEmail([FromBody] ConfirmEmailRequestDto dto)
        {
            try
            {
                string result;

                // Si se proporciona un token, usar ese token
                if (!string.IsNullOrEmpty(dto.Token))
                {
                    result = await _userService.ConfirmEmailAsync(dto.Token);
                }
                // Si no hay token pero el usuario está autenticado, usar su ID
                else if (User.Identity.IsAuthenticated)
                {
                    var userId = User.FindFirst("id")?.Value;
                    if (string.IsNullOrEmpty(userId) || !int.TryParse(userId, out int id))
                    {
                        return BadRequest(new { error = true, message = "No se pudo identificar al usuario." });
                    }

                    result = await _userService.ConfirmEmailAsync(null, id);
                }
                else
                {
                    return BadRequest(new { error = true, message = "Se requiere un token de verificación o estar autenticado." });
                }

                return Ok(new { message = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = true, message = ex.Message });
            }
        }

        [HttpPost("resend-verification")]
        public async Task<IActionResult> ResendVerification([FromBody] ResendVerificationEmailDto dto)
        {
            await _userService.ResendVerificationEmailAsync(dto.Email);
            return Ok(new { message = "Correo de verificación reenviado correctamente." });
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequestDto dto)
        {
            await _userService.ForgotPasswordAsync(dto);
            return Ok(new { message = "Correo de recuperación enviado correctamente." });
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequestDto dto)
        {
            await _userService.ResetPasswordAsync(dto);
            return Ok(new { message = "Contraseña restablecida correctamente." });
        }
    }
}