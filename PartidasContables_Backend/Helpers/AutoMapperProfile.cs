using AutoMapper;
using PartidasContables.DataBase.Entities;
using PartidasContables.Dtos.CatalogoCuenta;
using PartidasContables.Dtos.DetallePartida;
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
            // Mapeo de PartidaCreateDto a PartidaEntity
            CreateMap<PartidaCreateDto, PartidaEntity>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid())) // Generar Id automático para la Partida
                .ForMember(dest => dest.Fecha, opt => opt.MapFrom(src => DateTime.UtcNow)); // Asignar la fecha actual

            // Mapeo de DetallePartidaDto a DetallePartidaEntity
            CreateMap<DetallePartidaCreateDto, DetallePartidaEntity>()
                .ForMember(dest => dest.Monto, opt => opt.MapFrom(src => src.Monto)) // Monto del detalle
                .ForMember(dest => dest.TipoMovimiento, opt => opt.MapFrom(src => src.TipoMovimiento)) // Tipo de movimiento (Debe/Haber)
                .ForMember(dest => dest.Descripcion, opt => opt.MapFrom(src => src.Descripcion)); // Descripción del detalle

            // Mapeo de CatalogoCuentaDto a CatalogoCuentaEntity
            CreateMap<CatalogoCuentaDto, CatalogoCuentaEntity>()
                .ForMember(dest => dest.NumeroCuenta, opt => opt.MapFrom(src => src.NumeroCuenta)) // Número de cuenta
                .ForMember(dest => dest.Descripcion, opt => opt.MapFrom(src => src.Descripcion)) // Descripción
                .ForMember(dest => dest.TipoCuenta, opt => opt.MapFrom(src => src.TipoCuenta)) // Tipo de cuenta (Activo, Pasivo, etc.)
                .ForMember(dest => dest.Saldo, opt => opt.MapFrom(src => src.Saldo)); // Saldo de la cuenta
        }
        private void MapForCatalogoCuenta()
        {
            CreateMap<CatalogoCuentaEntity, CatalogoCuentaDto>().ForMember(dest => dest.Saldo, opt => opt.MapFrom(src => src.Saldo));
            CreateMap<CatalogoCuentaCreateDto, CatalogoCuentaEntity>();
            CreateMap<CatalogoCuentaEditDto, CatalogoCuentaEntity>();
        }
    }
}
