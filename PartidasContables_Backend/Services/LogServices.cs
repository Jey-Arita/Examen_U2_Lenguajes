//using AutoMapper;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.IdentityModel.Tokens;
//using Newtonsoft.Json;
//using PartidasContables.DataBase;
//using PartidasContables.DataBase.Entities;
//using PartidasContables.Dtos.Common;
//using PartidasContables.Dtos.LogDatabase;
//using PartidasContables.Services.Interface;

//namespace PartidasContables.Services
//{
//    public class LogServices : ILogService
//    {
//        private readonly LogDbContext _context;
//        private readonly IMapper _mapper;

//        public LogServices(LogDbContext context, IMapper mapper)
//        {
//            this._context = context;
//            this._mapper = mapper;
//        }
//        public async Task<ResponseDto<Guid>> CreateLog(LogDatabaseCreateDto logDto)
//        {
//            try
//            {
//                // Mapeamos LogDatabaseCreate a LogEntity
//                var logEntity = _mapper.Map<LogEntity>(logDto);
//                logEntity.Fecha = DateTime.UtcNow;

//                // Convertimos los detalles en JSON para almacenarlos
//                if (logDto.Detalles != null)
//                {
//                    logEntity.Detalles = Newtonsoft.Json.JsonConvert.SerializeObject(logDto.Detalles);
//                }

//                _context.Logs.Add(logEntity);
//                await _context.SaveChangesAsync();

//                return new ResponseDto<Guid>
//                {
//                    StatusCode = 201,
//                    Status = true,
//                    Data = logEntity.Id,
//                    Message = "Log creado exitosamente"
//                };
//            }
//            catch (Exception ex)
//            {
//                return new ResponseDto<Guid>
//                {
//                    Status = false,
//                    Message = $"Error al crear el log: {ex.Message}"
//                };
//            }
//        }

//        public async Task<ResponseDto<List<LogDatabaseDto>>> GetLogs()
//        {
//            try
//            {
//                // Consultamos todos los logs y los convertimos a LogDatabaseDto
//                var logs = await _context.Logs.ToListAsync();
//                var logDtos = _mapper.Map<List<LogDatabaseDto>>(logs);

//                // Deserializamos el campo Detalles de JSON a Dictionary<string, object>
//                foreach (var logDto in logDtos)
//                {
//                    if (!string.IsNullOrEmpty(logDto.Detalles))
//                    {
//                        try
//                        {
//                            logDto.Detalles = JsonConvert.DeserializeObject<Dictionary<string, object>>(logDto.Detalles);
//                        }
//                        catch (JsonException ex)
//                        {
//                            // Maneja el error de deserialización aquí, por ejemplo, logueándolo
//                            logDto.Detalles = null; // O puedes asignar un valor por defecto
//                        }
//                    }
//                }
//                return new ResponseDto<List<LogDatabaseDto>>
//                {
//                    StatusCode = 200,
//                    Status = true,
//                    Data = logDtos,
//                    Message = "Logs obtenidos exitosamente"
//                };
//            }
//            catch (Exception ex)
//            {
//                return new ResponseDto<List<LogDatabaseDto>>
//                {
//                    Status = false,
//                    Message = $"Error al obtener los logs: {ex.Message}"
//                };
//            }
//        }
//    }
//}
