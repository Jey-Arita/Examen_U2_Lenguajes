﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PartidasContables.Dtos.Auth;
using PartidasContables.Dtos.Common;
using PartidasContables.Services;
using PartidasContables.Services.Interface;

namespace PartidasContables.Controllers.AuthController
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ILogService _logService;

        public AuthController(
            IAuthService authService, ILogService logService
            )
        {
            this._authService = authService;
            this._logService = logService;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult<ResponseDto<LoginResponseDto>>> Login(LoginDto dto)
        {
            var response = await _authService.LoginAsync(dto);
            //await _logService.RegistrarLogAsync("Inicio de Sesion", null, "N/A", response.Data.Email);
            return StatusCode(response.StatusCode, response);
        }

    }
}
