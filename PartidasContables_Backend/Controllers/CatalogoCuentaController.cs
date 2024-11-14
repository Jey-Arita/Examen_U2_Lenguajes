using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        public CatalogoCuentaController(ICatalogoCuentaService service)
        {
            this._service = service;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<ResponseDto<List<CatalogoCuentaDto>>>> CatatalogoListAsync()
        {
            var response = await _service.ListCatalogoCuentaAsync();
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        [AllowAnonymous]
        //[Authorize(Roles = $"{RolesConstant.ADMIN}")]
        public async Task<ActionResult<ResponseDto<CatalogoCuentaDto>>> Create(CatalogoCuentaCreateDto dto)
        {
            var response = await _service.CrearCatalogoCuentaAsync(dto);

            return StatusCode(response.StatusCode, response);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseDto<CatalogoCuentaDto>>> EditarCuenta(CatalogoCuentaEditDto catalogoCuentaEditDto, Guid Id)
        {
            var responde = await _service.EditAsync(catalogoCuentaEditDto, Id);
            return StatusCode(responde.StatusCode, responde);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseDto<CatalogoCuentaDto>>> EliminarCuenta(Guid id)
        {
            var response = await _service.DeleteAsync(id);
            return StatusCode(response.StatusCode, response);
        }
    }
}
