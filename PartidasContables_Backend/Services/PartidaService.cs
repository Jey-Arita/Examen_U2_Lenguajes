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

                foreach (var detalleDto in partidaDto.Detalles)
                {
                    // Buscar la cuenta en el catálogo por su ID
                    var cuenta = await _context.CatalogoCuentas.FindAsync(detalleDto.IdCatalogoCuenta);
                    if (cuenta == null)
                    {
                        throw new Exception($"Cuenta con ID {detalleDto.IdCatalogoCuenta} no encontrada.");
                    }

                    // Mapeamos el detalle y le asignamos la cuenta relacionada
                    var detalleEntity = _mapper.Map<DetallePartidaEntity>(detalleDto);
                    detalleEntity.CatalogoCuenta = cuenta;

                    // Ajuste de saldo basado en el tipo de cuenta
                    if (cuenta.TipoCuenta == "Activo")
                    {
                        cuenta.Saldo += detalleDto.Monto;  // Las cuentas Activas incrementan el saldo
                    }
                    else if (cuenta.TipoCuenta == "Pasivo")
                    {
                        cuenta.Saldo -= detalleDto.Monto;  // Las cuentas Pasivas disminuyen el saldo
                    }
                    else
                    {
                        throw new Exception($"Tipo de cuenta no válido para la cuenta con ID {detalleDto.IdCatalogoCuenta}.");
                    }

                    // Asociar el detalle a la partida
                    partidaEntity.Detalles.Add(detalleEntity);
                }

                // Agregar la partida a la base de datos
                await _context.Partidas.AddAsync(partidaEntity);

                // Guardar los cambios, incluyendo los ajustes de saldo
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

<<<<<<< HEAD
    }
}
=======

        public async Task<ResponseDto<List<PartidaDto>>> ListPartidaAsync(PartidaDto partidaDto)
        {
            var partidaEntity = await _context.Partidas.Where(x => x.Fecha <= DateTime.Now).ToListAsync(); ;

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

>>>>>>> a66aad80391c3f10b357f7a1b477a539972e414d

