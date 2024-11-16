using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PartidasContables.Constants;
using PartidasContables.DataBase.Entities;
using PartidasContables.Dtos.CatalogoCuenta;
using PartidasContables.Dtos.Common;
using PartidasContables.Dtos.Partida;
using PartidasContables.Services;
using PartidasContables.Services.Interface;

namespace PartidasContables.Controllers
{
    [Route("api/catalogo-cuentas")]
    [ApiController]
    public class CatalogoCuentaController : ControllerBase
    {
        private readonly ICatalogoCuentaService _service;
        private readonly ILogService _logService;

        public CatalogoCuentaController(ICatalogoCuentaService service, ILogService logService)
        {
            this._service = service;
            this._logService = logService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<ResponseDto<List<CatalogoCuentaDto>>>> CatatalogoListAsync()
        {
            var response = await _service.ListCatalogoCuentaAsync();
            await _logService.RegistrarLogAsync("Ver Cuentas", "N/A", "N/A", "N/A");
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        [Authorize(Roles = $"{RolesConstant.ADMIN}")]
        public async Task<ActionResult<ResponseDto<CatalogoCuentaDto>>> Create(CatalogoCuentaCreateDto dto)
        {
            var response = await _service.CrearCatalogoCuentaAsync(dto);

            return StatusCode(response.StatusCode, response);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = $"{RolesConstant.ADMIN}")]
        public async Task<ActionResult<ResponseDto<CatalogoCuentaDto>>> EditarCuenta(CatalogoCuentaEditDto catalogoCuentaEditDto, Guid Id)
        {
            var responde = await _service.EditAsync(catalogoCuentaEditDto, Id);
            return StatusCode(responde.StatusCode, responde);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = $"{RolesConstant.ADMIN}")]
        public async Task<ActionResult<ResponseDto<CatalogoCuentaDto>>> EliminarCuenta(Guid id)
        {
            var response = await _service.DeleteAsync(id);
            return StatusCode(response.StatusCode, response);
        }
    }
}
