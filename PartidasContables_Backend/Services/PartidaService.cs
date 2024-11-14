using AutoMapper;
using PartidasContables.DataBase.Entities;
using PartidasContables.DataBase;
using PartidasContables.Dtos.Partida;
using PartidasContables.Dtos.Common;
using Microsoft.EntityFrameworkCore;
using PartidasContables.Services.Interface;

namespace PartidasContables.Services
{
    public class PartidaService : IPartidaService
    {
        private readonly PartidaDbContext _context;
        private readonly IMapper _mapper;

        public PartidaService(PartidaDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ResponseDto<PartidaEntity>> CrearPartidaAsync(PartidaCreateDto partidaCreateDto)
        {
            if (partidaCreateDto.Detalles == null || !partidaCreateDto.Detalles.Any())
            {
                return new ResponseDto<PartidaEntity>
                {
                    StatusCode = 400,
                    Status = false,
                    Message = "La partida debe tener al menos un detalle.",
                    Data = null,
                };
            }

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // Crear la entidad Partida y mapear los detalles
                var partidaEntity = _mapper.Map<PartidaEntity>(partidaCreateDto);

                // Validar y actualizar el saldo en el catálogo de cuentas
                foreach (var detalle in partidaCreateDto.Detalles)
                {
                    var catalogoCuenta = await _context.CatalogoCuentas
                        .FirstOrDefaultAsync(c => c.Id == detalle.IdCatalogoCuenta);

                    if (catalogoCuenta == null)
                    {
                        await transaction.RollbackAsync();
                        return new ResponseDto<PartidaEntity>
                        {
                            StatusCode = 404,
                            Status = false,
                            Message = $"La cuenta con Id {detalle.IdCatalogoCuenta} no existe.",
                            Data = null,
                        };
                    }

                    if (catalogoCuenta.TipoCuenta == "Activo" || catalogoCuenta.TipoCuenta == "Gasto")
                        catalogoCuenta.Saldo += detalle.Monto;
                    else if (catalogoCuenta.TipoCuenta == "Pasivo" || catalogoCuenta.TipoCuenta == "Ingreso")
                        catalogoCuenta.Saldo -= detalle.Monto;

                    _context.CatalogoCuentas.Update(catalogoCuenta);
                }

                // Asigna los detalles mapeados directamente a la propiedad de detalles de partidaEntity
                partidaEntity.Detalles = _mapper.Map<List<DetallePartidaEntity>>(partidaCreateDto.Detalles);

                // Agrega la entidad de partida (incluyendo los detalles) al contexto
                _context.Partidas.Add(partidaEntity);

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return new ResponseDto<PartidaEntity>
                {
                    StatusCode = 201,
                    Status = true,
                    Message = "Partida y detalles creados correctamente.",
                    Data = partidaEntity
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                var innerException = ex.InnerException?.Message ?? "No inner exception.";
                return new ResponseDto<PartidaEntity>
                {
                    StatusCode = 500,
                    Status = false,
                    Message = $"Error: {ex.Message}. Inner Exception: {innerException}",
                    Data = null,
                };
            }
        }




        public async Task<ResponseDto<List<PartidaDto>>> ListPartidaAsync(PartidaDto partidaDto)
        {
            var partidaEntity = await _context.Partidas.ToListAsync(); ;

            var librosDtos = _mapper.Map<List<PartidaDto>>(partidaEntity);

            return new ResponseDto<List<PartidaDto>>
            {
                StatusCode = 200,
                Status = true,
                Message = "Lista de partidas obtenida correctamente.",
                Data = librosDtos
            };
        }
    }

}

