using AutoMapper;
using PartidasContables.DataBase.Entities;
using PartidasContables.Dtos.CatalogoCuenta;
using PartidasContables.Dtos.DetallePartidaDto;
using PartidasContables.Dtos.Partida;

namespace PartidasContables.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() 
        {
            MapForPartidas();
            MapForCatalogoCuenta();
        }

        private void MapForPartidas()
        {
            // Mapeo de PartidaContableEntity a PartidaDto
            CreateMap<PartidaEntity, PartidaDto>()
                .ForMember(dest => dest.Detalles, opt => opt.MapFrom(src => src.Detalles));

            // Mapeo de DetallePartidaEntity a DetallePartidaDto
            CreateMap<DetallePartidaEntity, DetallePartidaDto>()
                .ForMember(dest => dest.IdCatalogoCuenta, opt => opt.MapFrom(src => src.CatalogoCuenta.Id)); // Aquí asumes que el detalle tiene un 'CatalogoCuenta'

            // Mapeo de PartidaDto a PartidaContableEntity
            CreateMap<PartidaDto, PartidaEntity>()
                .ForMember(dest => dest.Detalles, opt => opt.MapFrom(src => src.Detalles));

            // Mapeo de DetallePartidaDto a DetallePartidaEntity
            CreateMap<DetallePartidaDto, DetallePartidaEntity>()
                .ForMember(dest => dest.CatalogoCuenta, opt => opt.MapFrom(src => new CatalogoCuentaEntity { Id = src.IdCatalogoCuenta }));
        }
        private void MapForCatalogoCuenta()
        {
            CreateMap<CatalogoCuentaEntity, CatalogoCuentaDto>().ForMember(dest => dest.Saldo, opt => opt.MapFrom(src => src.Saldo));
        }
    }
}
