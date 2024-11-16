using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PartidasContables.Constants;
using PartidasContables.DataBase.Entities;
using PartidasContables.Dtos.Common;
using PartidasContables.Dtos.Partida;
using PartidasContables.Services.Interface;

namespace PartidasContables.Controllers
{
    public class LogController : Controller
    {
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

        }
    }
}
