using PartidasContables.Dtos.Auth;
using PartidasContables.Dtos.Common;
using System.Security.Claims;

namespace PartidasContables.Services.Interface
{
    public interface IAuthService
    {
        Task<ResponseDto<LoginResponseDto>> LoginAsync(LoginDto dto);
        ClaimsPrincipal GetTokenPrincipal(string token);
    }
}
