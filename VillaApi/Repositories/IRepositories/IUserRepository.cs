using VillaApi.Model.Dtos.LoginDto;
using VillaApi.Utility;

namespace VillaApi.Repositories.IRepositories
{
    public interface IUserRepository
    {
        Task <ApiResponse> Login(LoginRequestDto loginRequestDto);
    }
}
