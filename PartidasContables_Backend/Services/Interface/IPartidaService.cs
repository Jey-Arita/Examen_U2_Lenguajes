using PartidasContables.DataBase.Entities;
using PartidasContables.Dtos.Common;
using PartidasContables.Dtos.Partida;

namespace PartidasContables.Services.Interface
{
    public interface IPartidaService
    {
        Task<ResponseDto<List<PartidaDto>>> ListPartidaAsync();
        Task<ResponseDto<PartidaDto>> CrearPartidaAsync(PartidaCreateDto partidaDto);
    }
}
