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
            try
            {
                var response = await _userService.RegisterAsync(dto);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = true, message = ex.Message });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto dto)
        {
            try
            {
                var response = await _userService.LoginAsync(dto);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = true, message = ex.Message });
            }
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

        [HttpPost("resend-verification")]
        public async Task<IActionResult> ResendVerification([FromBody] ResendVerificationEmailDto dto)
        {
            try
            {
                await _userService.ResendVerificationEmailAsync(dto.Email);
                return Ok(new { message = "Correo de verificación reenviado correctamente." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = true, message = ex.Message });
            }
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequestDto dto)
        {
            try
            {
                await _userService.ForgotPasswordAsync(dto);
                return Ok(new { message = "Correo de recuperación enviado correctamente." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = true, message = ex.Message });
            }
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequestDto dto)
        {
            try
            {
                await _userService.ResetPasswordAsync(dto);
                return Ok(new { message = "Contraseña restablecida correctamente." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = true, message = ex.Message });
            }
        }

        [HttpGet("verify")]
        public async Task<IActionResult> VerifyEmail([FromQuery] string token)
        {
            try
            {
                var result = await _userService.ConfirmEmailAsync(token);
                return Ok(new { message = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = true, message = ex.Message });
            }
        }
    }
}