using AgencyPlatform.Application.DTOs;
using AgencyPlatform.Application.DTOs.Auth;
using AgencyPlatform.Shared.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgencyPlatform.Application.Interfaces.Services
{
    public interface IUserService
    {
        Task<AuthResponseDto> RegisterAsync(RegisterRequestDto dto);
        Task<AuthResponseDto> LoginAsync(LoginRequestDto dto);
        Task<PaginatedResult<UserDto>> GetUsersAsync(int page, int pageSize);
        Task ResendVerificationEmailAsync(string email);
        Task ForgotPasswordAsync(ForgotPasswordRequestDto dto);
        Task ResetPasswordAsync(ResetPasswordRequestDto dto);
        Task<string> ConfirmEmailAsync(string token);



    }
}
