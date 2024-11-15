using PartidasContables.Dtos.Common;
using PartidasContables.Dtos.LogDatabase;

namespace PartidasContables.Services.Interface
{
    public interface ILogService
    {
        Task<ResponseDto<Guid>> CreateLog(LogDatabaseCreateDto logDto);
        Task<ResponseDto<List<LogDatabaseDto>>> GetLogs();
    }
}
