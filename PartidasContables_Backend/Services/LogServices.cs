using PartidasContables.DataBase.Entities;
using PartidasContables.DataBase;
using PartidasContables.Services.Interface;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;
using PartidasContables.Dtos.Common;
using PartidasContables.Dtos.LogDatabase;
using AutoMapper;
using PartidasContables.Dtos.CatalogoCuenta;
using Microsoft.EntityFrameworkCore;

namespace PartidasContables.Services
{
    public class LogService : ILogService
    {
        private readonly LogDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public LogService(LogDbContext context, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            this._mapper = mapper;
        }

        public async Task<ResponseDto<List<LogDatabaseDto>>> ObtenerLogsAsync()
        {
            var entities = await _context.Logs.ToListAsync();
            var dtos = _mapper.Map<List<LogDatabaseDto>>(entities);

            return new ResponseDto<List<LogDatabaseDto>>
            {
                StatusCode = 200,
                Status = true,
                Message = "Listado de cuentas obtenido exitosamente",
                Data = dtos
            };
        }

        public async Task RegistrarLogAsync(string accion, string idPartida, string IdCuenta,string Emai)
        {
            // Obtener el IdUsuario desde el token
            var userId = _httpContextAccessor.HttpContext?.User?.FindFirst("UserId")?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                throw new Exception("No se encontró el UserId en el token.");
            }

            // Crear un nuevo objeto de log
            var log = new LogEntity
            {
                Fecha = DateTime.Now,
                IdUsuario = userId,
                Accion = accion,
                IdPartida = idPartida.ToString(),
                IdCuenta = IdCuenta,
                Email = Emai,
            };

            // Agregar el log al DbContext
            _context.Logs.Add(log);

            // Guardar los cambios en la base de datos
            await _context.SaveChangesAsync();
        }
    }
}
