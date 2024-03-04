using LogixTek.WebApi.Models.Dtos;
using LogixTek.WebApi.Models;
using LogixTek.WebApi.Entities;

namespace LogixTek.WebApi.Services.Interface
{
    public interface IUserService
    {
        Task<UserLoginDto> LoginAsync(UserLoginRequest request);

        Task<UserRegisterDto> RegisterAsync(UserRegisterRequest request);

        Task<User> GetUserByIdAsync(int userId);
    }
}
