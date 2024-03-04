using LogixTek.WebApi.Authorization;
using LogixTek.WebApi.Models;
using LogixTek.WebApi.Models.Dtos;
using LogixTek.WebApi.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace LogixTek.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("Register")]
        public async Task<UserRegisterDto> Register([FromBody] UserRegisterRequest userRegisterRequest)
        {
            return await _userService.RegisterAsync(userRegisterRequest);
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<UserLoginDto> Login([FromBody] UserLoginRequest userLoginRequest)
        {
            return await _userService.LoginAsync(userLoginRequest);
        }
    }
}
