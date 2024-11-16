using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
<<<<<<< HEAD
using PartidasContables.Constants;
using PartidasContables.DataBase.Entities;
using PartidasContables.Dtos.Common;
using PartidasContables.Dtos.Partida;
using PartidasContables.Services.Interface;
=======
using PartidasContables.DataBase;
using PartidasContables.Dtos.CatalogoCuenta;
using PartidasContables.Dtos.Common;
using PartidasContables.Dtos.LogDatabase;
using PartidasContables.Services.Interface;
using System;
>>>>>>> 124e91a2ad6911ad062f55b096ce7185b4647a39

namespace PartidasContables.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/logs")]
    [ApiController]
    public class LogController : Controller
    {
<<<<<<< HEAD
        [Authorize(AuthenticationSchemes = "Bearer")]
        [ApiController]
        [Route("api/log")]
        public class PartidaController : ControllerBase
        {
            private readonly IHttpContextAccessor _httpContextAccessor;
            private readonly ILogService _logService;

            public PartidaController(IHttpContextAccessor httpContextAccessor, ILogService logService)
            {
                _httpContextAccessor = httpContextAccessor;
                _logService = logService;
            }

=======
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
>>>>>>> 124e91a2ad6911ad062f55b096ce7185b4647a39
        }
    }
}
