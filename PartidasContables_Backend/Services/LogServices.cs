using PartidasContables.DataBase.Entities;
using PartidasContables.DataBase;
using PartidasContables.Services.Interface;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace PartidasContables.Services
{
    public class LogService : ILogService
    {
        private readonly LogDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LogService(LogDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
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
