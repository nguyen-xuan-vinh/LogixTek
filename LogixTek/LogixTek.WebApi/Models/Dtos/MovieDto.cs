using AutoMapper;
using LogixTek.WebApi.Entities;

namespace LogixTek.WebApi.Models.Dtos
{
    public class MovieDto : Profile
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int NumberOfLike { get; set; }

        public string ImageUrl { get; set; }

        public bool? Liked { get; set; }

        public bool? Disliked { get; set; }

        public MovieDto()
        {
            CreateMap<Movie, MovieDto>();
        }
    }
}
