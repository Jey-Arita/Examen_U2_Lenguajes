using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        public PartidaController(IPartidaService partidaService)
        {
            _partidaService = partidaService;
        }

        [HttpPost]
        [AllowAnonymous]
        //[Authorize(Roles = $"{RolesConstant.ADMIN}")]
        public async Task<ActionResult<ResponseDto<PartidaEntity>>> Create(PartidaDto partidaDto)
        {
            // Llamamos al servicio para crear la partida
            var response = await _partidaService.CrearPartidaAsync(partidaDto);

            // Devolvemos la respuesta con el código de estado correspondiente
            return StatusCode(response.StatusCode, response);
        }
    }
}
