using LogixTek.WebApi.Models.Dtos;

namespace LogixTek.WebApi.Services.Interface
{
    public interface IMovieService
    {
        Task<List<MovieDto>> GetAllMovieAsync(int userId);
        Task<bool> LikeMovieAsync(int movieId, int userId, bool like);
    }
}
