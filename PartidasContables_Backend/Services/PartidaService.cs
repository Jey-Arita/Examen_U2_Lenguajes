using AutoMapper;
using PartidasContables.DataBase.Entities;
using PartidasContables.DataBase;
using PartidasContables.Dtos.Partida;
using PartidasContables.Dtos.Common;
using Microsoft.EntityFrameworkCore;
using PartidasContables.Services.Interface;
using PartidasContables.Dtos.DetallePartidaDto;

namespace PartidasContables.Services
{
    public class PartidaService : IPartidaService
    {
        private readonly PartidaDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<PartidaEntity> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PartidaService(PartidaDbContext context, IMapper mapper, ILogger<PartidaEntity> logger, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
            this._httpContextAccessor = httpContextAccessor;
        }

        public async Task<ResponseDto<PartidaDto>> CrearPartidaAsync(PartidaCreateDto partidaCreateDto)
        {
            //Extraemos el Id del usuario para guardarlo en la base de datos
            var userIdClaim = _httpContextAccessor.HttpContext.User.Claims
        .FirstOrDefault(c => c.Type == "UserId");

            // Verificaramos si traemos la clain
            if (userIdClaim == null)
            {
                return new ResponseDto<PartidaDto>
                {
                    StatusCode = 401,
                    Status = false,
                    Message = "No se encontró el UserId en el token.",
                    Data = null,
                };
            }

            var userId = userIdClaim.Value;

            // Asignamos al DtoPartida el Id del usuario que extragimos antes
            partidaCreateDto.IdUsuario = userId;

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

            // Comenzamos la transaccion
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // Creamos la partida y meapeamos
                var partidaEntity = _mapper.Map<PartidaEntity>(partidaCreateDto);

                // Validamos y actualizamos el saldo en el catálogo de cuentas
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
                    if (catalogoCuenta.TipoCuenta == "Activo" || detalle.TipoMovimiento == "Debe")
                        catalogoCuenta.Saldo += detalle.Monto;
                    else if (catalogoCuenta.TipoCuenta == "Pasivo" || detalle.TipoMovimiento == "Haber")
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

                // Mapeamos
                var partidaDto = _mapper.Map<PartidaDto>(partidaEntity);

                // Devolvemos correcto
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
                //Si sale error, devolvemos todo atras para evitar generar datos
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

            // Mapear la lista de entidades a una lista de dtos
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

