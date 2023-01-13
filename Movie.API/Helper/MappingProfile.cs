using AutoMapper;
using Movie.Models;
using Mvoie.RepositryWithUOW.DTO;

namespace Movie.API.Helper
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Genre, GenreWithMovie>();
            CreateMap<MovieDetail, MovieWithGenreDTO>();
            CreateMap<RegisterDTO, User>();
        }
    }
}
