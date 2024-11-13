using AutoMapper;
using PartidasContables.DataBase.Entities;
using PartidasContables.Dtos.DetallePartidaDto;
using PartidasContables.Dtos.Partida;

namespace PartidasContables.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() 
        {
            MapForPartidas();
        }

        private void MapForPartidas()
        {
            CreateMap<PartidaDto, PartidaEntity>()
                .ForMember(dest => dest.Detalles, opt => opt.Ignore()); // Ignoramos Detalles por ahora

            CreateMap<DetallePartidaDto, DetallePartidaEntity>();
        }
    }
}
