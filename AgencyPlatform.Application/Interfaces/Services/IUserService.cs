﻿using AgencyPlatform.Application.DTOs;
using AgencyPlatform.Application.DTOs.Auth;
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
        Task<PaginatedResultDto<UserDto>> GetUsersAsync(int page, int pageSize);
        Task<string> ConfirmEmailAsync(string token, int? userId = null);
        Task ResendVerificationEmailAsync(string email);
        Task ForgotPasswordAsync(ForgotPasswordRequestDto dto);
        Task ResetPasswordAsync(ResetPasswordRequestDto dto);

    }
}
