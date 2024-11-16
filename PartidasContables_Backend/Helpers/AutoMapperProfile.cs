using AutoMapper;
using PartidasContables.DataBase.Entities;
using PartidasContables.Dtos.CatalogoCuenta;
using PartidasContables.Dtos.DetallePartida;
using PartidasContables.Dtos.DetallePartidaDto;
using PartidasContables.Dtos.LogDatabase;
using PartidasContables.Dtos.Partida;
using System.Security.AccessControl;

namespace PartidasContables.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() 
        {
            MapForPartidas();
            MapForCatalogoCuenta();
            MapForLogs();
        }

        private void MapForPartidas()
        {
            // Mapeo de PartidaCreateDto a PartidaEntity
            CreateMap<PartidaCreateDto, PartidaEntity>()
            .ForMember(dest => dest.IdUsuario, opt => opt.MapFrom(src => src.IdUsuario))
            .ForMember(dest => dest.Detalles, opt => opt.MapFrom(src => src.Detalles));


            // Mapeo de DetallePartidaCreateDto a DetallePartidaEntity
            CreateMap<DetallePartidaCreateDto, DetallePartidaEntity>();

            CreateMap<PartidaEntity, PartidaDto>()
            .ForMember(dest => dest.Detalles, opt => opt.MapFrom(src => src.Detalles));

            CreateMap<DetallePartidaEntity, DetallesPartidaDto>();
        }
        private void MapForCatalogoCuenta()
        {
            CreateMap<CatalogoCuentaEntity, CatalogoCuentaDto>().ForMember(dest => dest.Saldo, opt => opt.MapFrom(src => src.Saldo));
            CreateMap<CatalogoCuentaCreateDto, CatalogoCuentaEntity>();
            CreateMap<CatalogoCuentaEditDto, CatalogoCuentaEntity>();
        }

        private void MapForLogs()
        {
            CreateMap<LogEntity, LogDatabaseDto>().ForMember(dest => dest.IdPartida, opt => opt.MapFrom(src => src.IdPartida));
        } 
    }
}
