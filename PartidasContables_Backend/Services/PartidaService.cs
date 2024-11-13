using AutoMapper;
using PartidasContables.DataBase.Entities;
using PartidasContables.DataBase;
using PartidasContables.Dtos.Partida;
using Microsoft.EntityFrameworkCore;

namespace PartidasContables.Services
{
    public class PartidaService
    {
        private readonly PartidaDbContext _context;
        private readonly IMapper _mapper;

        public PartidaService(PartidaDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task CrearPartidaAsync(PartidaDto partidaDto)
        {
            // Mapeo de PartidaDto a PartidaEntity
            var partidaEntity = _mapper.Map<PartidaEntity>(partidaDto);

            // Guardar la partida para generar el Id
            _context.Partidas.Add(partidaEntity);
            await _context.SaveChangesAsync();

            // Asignación de IdPartida en cada detalle y ajuste de cuentas
            foreach (var detalleDto in partidaDto.Detalles)
            {
                detalleDto.IdPartida = partidaEntity.Id; // Asigna el IdPartida generado

                // Obtener la entidad de cuenta del catálogo relacionada
                var cuentaCatalogo = await _context.CatalogoCuentas
                    .FirstOrDefaultAsync(c => c.Id == detalleDto.IdCatalogoCuenta);

                if (cuentaCatalogo == null)
                {
                    throw new Exception($"La cuenta con Id {detalleDto.IdCatalogoCuenta} no existe en el catálogo.");
                }

                // Ajustar el saldo en función de si la cuenta permite movimiento y su tipo de operación
                if (cuentaCatalogo.PermiteMovimiento)
                {
                    // Suponiendo que "TipoCuenta" indica "Debe" o "Haber" para el ajuste
                    if (cuentaCatalogo.TipoCuenta == "Debe")
                    {
                        cuentaCatalogo.Saldo += detalleDto.Monto; // Aumentar en cuentas de Debe
                    }
                    else if (cuentaCatalogo.TipoCuenta == "Haber")
                    {
                        cuentaCatalogo.Saldo -= detalleDto.Monto; // Disminuir en cuentas de Haber
                    }
                }
            }

            // Mapeo de DetallePartidaDto a DetallePartidaEntity y guardado en base de datos
            var detalles = _mapper.Map<List<DetallePartidaEntity>>(partidaDto.Detalles);
            _context.DetallesPartida.AddRange(detalles);

            // Guardar los cambios en la base de datos, incluyendo el ajuste en las cuentas del catálogo
            await _context.SaveChangesAsync();
        }
    }

}
