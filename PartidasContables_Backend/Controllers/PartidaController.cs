using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PartidasContables.Constants;
using PartidasContables.DataBase.Entities;
using PartidasContables.Dtos.Common;
using PartidasContables.Dtos.Partida;
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

        public PartidaController(IPartidaService partidaService, IHttpContextAccessor httpContextAccessor)
        {
            _partidaService = partidaService;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<ResponseDto<PartidaDto>>> PartidaList()
        {
            var response = await _partidaService.ListPartidaAsync();
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        [Authorize(Roles = $"{RolesConstant.ADMIN}")]
        public async Task<ActionResult<ResponseDto<PartidaEntity>>> Create(PartidaCreateDto partidaCreateDto)
        {
            // Llamamos al servicio para crear la partida
            var response = await _partidaService.CrearPartidaAsync(partidaCreateDto);

            // Devolvemos la respuesta con el código de estado correspondiente
            return StatusCode(response.StatusCode, response);
        }
    }
}
