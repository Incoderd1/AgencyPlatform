﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgencyPlatform.Application.DTOs.Auth
{
    public class ConfirmEmailRequestDto
    {
        public string Token { get; set; } = string.Empty;
    }
}
