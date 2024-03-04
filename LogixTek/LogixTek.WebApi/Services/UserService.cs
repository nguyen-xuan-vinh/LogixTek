using AutoMapper;
using LogixTek.WebApi.Authorization;
using LogixTek.WebApi.Entities;
using LogixTek.WebApi.Helpers;
using LogixTek.WebApi.Models;
using LogixTek.WebApi.Models.Dtos;
using LogixTek.WebApi.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace LogixTek.WebApi.Services
{
    public class UserService : IUserService
    {
        private DataBaseContext _context;
        private IJwtUtils _jwtUtils;
        private IMapper _mapper;
        public UserService(DataBaseContext context, IJwtUtils jwtUtils, IMapper mapper)
        {
            _context = context;
            _jwtUtils = jwtUtils;
            _mapper = mapper;
        }

        public async Task<UserLoginDto> LoginAsync(UserLoginRequest request)
        {
            User user = await GetUserByUserNameAsync(request.Username);

            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
                throw new AppException("Username or password is incorrect");

            UserLoginDto response = _mapper.Map<UserLoginDto>(user);
            response.Token = _jwtUtils.GenerateToken(user);
            return response;
        }

        public async Task<UserRegisterDto> RegisterAsync(UserRegisterRequest request)
        {
            if (_context.Users.Any(x => x.Username == request.Username))
                throw new AppException("Username '" + request.Username + "' is already taken");

            User user = new()
            {
                Name = request.Name,
                Username = request.Username,
                Password = request.Password,
            };

            user.Password = BCrypt.Net.BCrypt.HashPassword(request.Password);

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return _mapper.Map<UserRegisterDto>(user);
        }

        public async Task<User> GetUserByUserNameAsync(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Username == username) ?? throw new KeyNotFoundException("User not found");
        }

        public async Task<User> GetUserByIdAsync(int userId)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Id == userId) ?? throw new KeyNotFoundException("User not found");
        }
    }
}
