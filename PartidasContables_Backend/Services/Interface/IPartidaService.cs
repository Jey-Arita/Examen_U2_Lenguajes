using PartidasContables.DataBase.Entities;
using PartidasContables.Dtos.Common;
using PartidasContables.Dtos.Partida;

namespace PartidasContables.Services.Interface
{
    public interface IPartidaService
    {
        Task<ResponseDto<PartidaEntity>> CrearPartidaAsync(PartidaDto partidaDto);
    }
}
