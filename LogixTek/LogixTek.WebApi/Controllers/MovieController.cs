using LogixTek.WebApi.Authorization;
using LogixTek.WebApi.Models.Dtos;
using LogixTek.WebApi.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace LogixTek.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMovieService _movieService;
        public MovieController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet("getall/{userId}")]
        public async Task<List<MovieDto>> GetAll(int userId)
        {
            return await _movieService.GetAllMovieAsync(userId);
        }

        [HttpPost("Like/{id}")]
        public async Task<bool> Like(int id, [FromBody] int userId) => await _movieService.LikeMovieAsync(id, userId, true);

        [HttpPost("Dislike/{id}")]
        public async Task<bool> Dislike(int id, [FromBody] int userId)
        {
            return await _movieService.LikeMovieAsync(id, userId, false);
        }
    }
}
