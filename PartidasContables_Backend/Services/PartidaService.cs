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

        public async Task<ResponseDto<PartidaEntity>> CrearPartidaAsync(PartidaDto partidaDto)
        {
            // Validación: La partida debe tener al menos un detalle
            if (partidaDto.Detalles == null || !partidaDto.Detalles.Any())
            {
                return new ResponseDto<PartidaEntity>
                {
                    StatusCode = 400,
                    Status = false,
                    Message = "La partida debe tener al menos un detalle.",
                    Data = null,
                };
            }

            try
            {
                // Mapeo del DTO a la entidad PartidaEntity
                var partidaEntity = _mapper.Map<PartidaEntity>(partidaDto);

                // Mapeamos cada DetallePartidaDto a DetallePartidaEntity
                foreach (var detalleDto in partidaDto.Detalles)
                {
                    // Buscamos la cuenta en el catálogo por su ID
                    var cuenta = await _context.CatalogoCuentas.FindAsync(detalleDto.IdCatalogoCuenta);
                    if (cuenta == null)
                    {
                        throw new Exception($"Cuenta con ID {detalleDto.IdCatalogoCuenta} no encontrada.");
                    }

                    // Mapeamos DetallePartidaDto a DetallePartidaEntity y le asignamos la cuenta relacionada
                    var detalleEntity = _mapper.Map<DetallePartidaEntity>(detalleDto);
                    detalleEntity.CatalogoCuenta = cuenta;  // Asignamos la cuenta encontrada

                    // Asociamos el detalle a la partida
                    partidaEntity.Detalles.Add(detalleEntity);
                }

                // Aquí puedes agregar la lógica para modificar el saldo de las cuentas asociadas si es necesario

                // Agregamos la partida a la base de datos
                await _context.Partidas.AddAsync(partidaEntity);
                await _context.SaveChangesAsync();

                return new ResponseDto<PartidaEntity>
                {
                    StatusCode = 201,
                    Status = true,
                    Message = "Registro creado correctamente.",
                    Data = partidaEntity,
                };
            }
            catch (Exception ex)
            {
                return new ResponseDto<PartidaEntity>
                {
                    StatusCode = 500,
                    Status = false,
                    Message = $"Error al crear la partida: {ex.Message}",
                    Data = null,
                };
            }
        }
    }
}

