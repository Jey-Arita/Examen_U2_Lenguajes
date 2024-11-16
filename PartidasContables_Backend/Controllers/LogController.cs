using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PartidasContables.Constants;
using PartidasContables.DataBase.Entities;
using PartidasContables.Dtos.Common;
using PartidasContables.Dtos.Partida;
using PartidasContables.Services.Interface;
using PartidasContables.DataBase;
using PartidasContables.Dtos.CatalogoCuenta;
using PartidasContables.Dtos.LogDatabase;
using System;

namespace PartidasContables.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ApiController]
    [Route("api/log")]
    public class LogController : ControllerBase
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogService _logService;

        // Constructor para LogController
        public LogController(IHttpContextAccessor httpContextAccessor, ILogService logService)
        {
            _httpContextAccessor = httpContextAccessor;
            _logService = logService;
        }

        // Método GET para obtener los logs
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<ResponseDto<List<LogDatabaseDto>>>> GetResultAsync()
        {
            var response = await _logService.ObtenerLogsAsync();
            return StatusCode(response.StatusCode, response);
        }
    }
}
