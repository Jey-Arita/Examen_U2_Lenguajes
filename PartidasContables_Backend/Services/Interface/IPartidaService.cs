using PartidasContables.DataBase.Entities;
using PartidasContables.Dtos.Common;
using PartidasContables.Dtos.Partida;

namespace PartidasContables.Services.Interface
{
    public interface IPartidaService
    {
        Task<ResponseDto<List<PartidaDto>>> ListPartidaAsync(PartidaDto partidaDto);
        Task<ResponseDto<PartidaEntity>> CrearPartidaAsync(PartidaDto partidaDto);
    }
}
