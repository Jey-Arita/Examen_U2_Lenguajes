using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PartidasContables.DataBase;
using PartidasContables.Dtos.CatalogoCuenta;
using PartidasContables.Dtos.Common;
using PartidasContables.Dtos.LogDatabase;
using PartidasContables.Services.Interface;
using System;

namespace PartidasContables.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/logs")]
    [ApiController]
    public class LogController : Controller
    {
        private readonly ILogService _logService;

        public LogController(ILogService logService)
        {
            this._logService = logService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<ResponseDto<List<LogDatabaseDto>>>>  GetResultAsync()
        {
            var response = await _logService.ObtenerLogsAsync();
            return StatusCode(response.StatusCode, response);
        }
    }
}
