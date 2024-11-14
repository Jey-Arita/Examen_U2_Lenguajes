using PartidasContables.DataBase.Entities;
using PartidasContables.Dtos.CatalogoCuenta;
using PartidasContables.Dtos.Common;
using PartidasContables.Dtos.Partida;

namespace PartidasContables.Services.Interface
{
    public interface ICatalogoCuentaService
    {
        Task<ResponseDto<List<CatalogoCuentaDto>>> ListCatalogoCuentaAsync();
        Task<ResponseDto<CatalogoCuentaDto>> CrearCatalogoCuentaAsync(CatalogoCuentaCreateDto catalogoCuentaDto);
        Task<ResponseDto<CatalogoCuentaDto>> EditAsync(CatalogoCuentaEditDto catalogoCuentaEditDto, Guid id);
        Task<ResponseDto<CatalogoCuentaDto>> DeleteAsync(Guid id);
    }
}
