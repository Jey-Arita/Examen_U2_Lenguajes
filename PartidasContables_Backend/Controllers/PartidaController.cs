using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PartidasContables.Constants;
using PartidasContables.DataBase.Entities;
using PartidasContables.Dtos.Common;
using PartidasContables.Dtos.Partida;
using PartidasContables.Services;
using PartidasContables.Services.Interface;

namespace PartidasContables.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ApiController]
    [Route("api/partida")]
    public class PartidaController : ControllerBase
    {
        private readonly IPartidaService _partidaService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogService _logService;

        public PartidaController(IPartidaService partidaService, IHttpContextAccessor httpContextAccessor, ILogService logService)
        {
            _partidaService = partidaService;
            _httpContextAccessor = httpContextAccessor;
            _logService = logService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<ResponseDto<PartidaDto>>> PartidaList()
        {
            var response = await _partidaService.ListPartidaAsync();
            await _logService.RegistrarLogAsync("Ver Partidas", null);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        [Authorize(Roles = $"{RolesConstant.ADMIN}")]
        public async Task<ActionResult<ResponseDto<PartidaEntity>>> Create(PartidaCreateDto partidaCreateDto)
        {
            // Llamamos al servicio para crear la partida
            var response = await _partidaService.CrearPartidaAsync(partidaCreateDto);

            // Registrar el log de la acción de creación
           await _logService.RegistrarLogAsync("Crear Partida", response.Data.Id);

            // Devolvemos la respuesta con el código de estado correspondiente
            return StatusCode(response.StatusCode, response);
        }
    }
}
