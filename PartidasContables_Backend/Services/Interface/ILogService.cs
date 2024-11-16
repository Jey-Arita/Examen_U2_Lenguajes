using PartidasContables.Dtos.Common;
using PartidasContables.Dtos.LogDatabase;

namespace PartidasContables.Services.Interface
{
    public interface ILogService
    {
        Task RegistrarLogAsync(string accion, string idPartida, string IdCuenta, string Emai);
    }
}
