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
        private readonly ILogger<PartidaEntity> _logger;

        public PartidaService(PartidaDbContext context, IMapper mapper, ILogger<PartidaEntity> logger)
        {
            _context = context;
            _mapper = mapper;
            this._logger = logger;
        }

        public async Task<ResponseDto<PartidaDto>> CrearPartidaAsync(PartidaCreateDto partidaCreateDto)
        {
            // Validar que la partida tenga al menos un detalle
            if (partidaCreateDto.Detalles == null || !partidaCreateDto.Detalles.Any())
            {
                return new ResponseDto<PartidaDto>
                {
                    StatusCode = 400,
                    Status = false,
                    Message = "La partida debe tener al menos un detalle.",
                    Data = null,
                };
            }

            // Comenzamos la transacción
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
                        return new ResponseDto<PartidaDto>
                        {
                            StatusCode = 404,
                            Status = false,
                            Message = $"La cuenta con Id {detalle.IdCatalogoCuenta} no existe.",
                            Data = null,
                        };
                    }

                    // Actualizamos el saldo según el tipo de cuenta
                    if (catalogoCuenta.TipoCuenta == "Activo" || catalogoCuenta.TipoCuenta == "Gasto")
                        catalogoCuenta.Saldo += detalle.Monto;
                    else if (catalogoCuenta.TipoCuenta == "Pasivo" || catalogoCuenta.TipoCuenta == "Ingreso")
                        catalogoCuenta.Saldo -= detalle.Monto;

                    _context.CatalogoCuentas.Update(catalogoCuenta);
                }

                // Asignamos los detalles mapeados a partidaEntity
                partidaEntity.Detalles = _mapper.Map<List<DetallePartidaEntity>>(partidaCreateDto.Detalles);

                // Agregamos la entidad de partida al contexto
                _context.Partidas.Add(partidaEntity);

                // Guardamos los cambios en la base de datos
                await _context.SaveChangesAsync();

                // Confirmamos la transacción
                await transaction.CommitAsync();

                // Mapear `PartidaEntity` a `PartidaDto`
                var partidaDto = _mapper.Map<PartidaDto>(partidaEntity);

                // Devolvemos la respuesta exitosa con PartidaDto
                return new ResponseDto<PartidaDto>
                {
                    StatusCode = 201,
                    Status = true,
                    Message = "Partida y detalles creados correctamente.",
                    Data = partidaDto
                };
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();

                return new ResponseDto<PartidaDto>
                {
                    StatusCode = 500,
                    Status = false,
                    Message = "Error al crear la partida",
                    Data = null,
                };
            }
        }



        public async Task<ResponseDto<List<PartidaDto>>> ListPartidaAsync()
        {
            var partidasEntity = await _context.Partidas
        .Include(p => p.Detalles) // Incluye los detalles de cada partida
        .ToListAsync();

            // Verificar si no hay partidas en la base de datos
            if (partidasEntity == null || !partidasEntity.Any())
            {
                return new ResponseDto<List<PartidaDto>>
                {
                    StatusCode = 404,
                    Status = false,
                    Message = "No se encontraron partidas.",
                    Data = null,
                };
            }

            // Mapear la lista de entidades a una lista de DTOs
            var partidasDto = _mapper.Map<List<PartidaDto>>(partidasEntity);

            // Devolver la lista de partidas en el DTO de respuesta
            return new ResponseDto<List<PartidaDto>>
            {
                StatusCode = 200,
                Status = true,
                Message = "Partidas obtenidas correctamente.",
                Data = partidasDto,
            };
        }
    }

}

