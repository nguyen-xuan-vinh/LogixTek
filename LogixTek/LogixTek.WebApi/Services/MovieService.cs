using AutoMapper;
using LogixTek.WebApi.Entities;
using LogixTek.WebApi.Models.Dtos;
using LogixTek.WebApi.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace LogixTek.WebApi.Services
{
    public class MovieService : IMovieService
    {
        private DataBaseContext _context;
        private IMapper _mapper;
        public MovieService(DataBaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<MovieDto>> GetAllMovieAsync(int userId)
        {
            IList<Movie> movies = await _context.Movies.ToListAsync();
            List<MovieDto> movieDtos = _mapper.Map<List<MovieDto>>(movies);

            movieDtos.ForEach(async x =>
            {
                var actioned = await GetMovieActionByMovieIdAsync(x.Id, userId);
                x.NumberOfLike = await NumberOfLikedAsync(x.Id);
                if (actioned != null && actioned.IsActive.HasValue && actioned.IsActive.Value)
                {
                    x.Liked = actioned.Status == 1 ? true : false;
                }
            });
            return movieDtos;
        }

        public async Task<bool> LikeMovieAsync(int movieId, int userId, bool like)
        {
            var actioned = await GetMovieActionByMovieIdAsync(movieId, userId);
            try
            {
                if (actioned != null)
                {
                    if (actioned.Status == 1)
                    {

                        if (like)
                        {
                            actioned.IsActive = false;
                        }
                        else
                        {
                            actioned.Status = 2;
                        }
                    }
                    _context.MovieActions.Update(actioned);
                }
                else
                {
                    var movieAction = new MovieAction();
                    movieAction.MovieId = movieId;
                    movieAction.ByUserId = userId;
                    movieAction.IsActive = true;
                    movieAction.Status = 1;
                    await _context.MovieActions.AddAsync(movieAction);
                    
                }
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        private async Task<MovieAction> GetMovieActionByMovieIdAsync(int movieId, int userId)
        {
            var result = await _context.MovieActions.SingleOrDefaultAsync(x => x.MovieId == movieId && x.ByUserId == userId);
            return result;
        }

        private async Task<int> NumberOfLikedAsync(int movieId)
        {
            return await _context.MovieActions.Where(x => x.Id == movieId && x.IsActive.HasValue && x.IsActive.Value).CountAsync();
        }
    }
}
