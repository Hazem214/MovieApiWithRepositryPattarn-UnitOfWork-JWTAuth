using AutoMapper;
using Movie.EF;
using Movie.Models;
using Mvoie.RepositryWithUOW.DTO;
using Mvoie.RepositryWithUOW.IRopositry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Mvoie.RepositryWithUOW.Repositry
{
    public class GenreRepositry : BaseRepositry<Genre>, IGenreRepositry
    {
        private readonly IMapper _mapper;
        public GenreRepositry(MovieContext Cotnext,IMapper mapper) : base(Cotnext)
        {
            _mapper = mapper;   
        }

        public IEnumerable<GenreWithMovie> GetAll(string include)
        {
            var Genres = base.GetAll(include);
            List<GenreWithMovie> genreWithMovieList = new List<GenreWithMovie>();


            foreach (var genre in Genres)
            {
                GenreWithMovie genreWithMovie = new GenreWithMovie();
                genreWithMovie.Id = genre.Id;
                genreWithMovie.Name = genre.Name;

                foreach (var movie in genre.Movies)
                {
                    genreWithMovie.Movies.Add(movie.Title);
                }
                genreWithMovieList.Add(genreWithMovie);

            }

            return genreWithMovieList;
        }

        public GenreWithMovie GetById( Expression<Func<Genre, bool>> criteria, string Includes)
        {
            var genre = base.GetById( criteria, Includes);
            if (genre != null)
            {
                GenreWithMovie genreWithMovie = new GenreWithMovie
                {
                    Name = genre.Name,
                    Id = genre.Id
                };
                if (genre.Movies != null)
                {
                    foreach (var movie in genre.Movies)
                    {
                        genreWithMovie.Movies.Add(movie.Title);
                    }
                }

                return genreWithMovie;
            }

            return null;
       
        }

  

        public GenreWithMovie GetByName(Expression<Func<Genre, bool>> criteria, string[] Includes = null)
        {
            var Genre = base.GetByName(criteria, Includes);
            if (Genre != null)
            {
                var genreWithMovie = new GenreWithMovie()
                {
                    Id = Genre.Id,
                    Name = Genre.Name
                };
                if (Genre.Movies != null)
                {
                    foreach (var movie in Genre.Movies)
                    {
                        genreWithMovie.Movies.Add(movie.Title);
                    }
                }

                return genreWithMovie;
            }
            return null;

        }

        public ResponseViewModel<Genre> Add(GenreDTO genreDto)
        {
           Genre genre= new Genre { Name = genreDto.Name };

           base.Add(genre);

            return new ResponseViewModel<Genre> { Data = genre, Message = "Added Successfuly", Issuccess = true };

        }



        public ResponseViewModel<GenreDTO> Update(int id,GenreDTO genre)
        {
           var Genre= base.Find(id);
            if(Genre==null) return new ResponseViewModel<GenreDTO>() { Data = null, Message = "This Id Not Exist", Issuccess = false };

            Genre.Name = genre.Name;

            var newGenre = base.Update(Genre);
            var NewGenreDTO = new GenreDTO() { Name = newGenre.Name };

            return new ResponseViewModel<GenreDTO>() { Data = NewGenreDTO, Message = "Updaed SuccessFuly", Issuccess = true };
        }

        public ResponseViewModel<GenreDTO> Delete(int id)
        {
            var genre = base.Find(id);
            if (genre != null)
            {
                base.Delete(genre);
              

                return new ResponseViewModel<GenreDTO>() { Data = null, Message = "Deleted Successfuly", Issuccess = true };
            }
            return new ResponseViewModel<GenreDTO>() { Data = null, Message = "this Id Not Exist", Issuccess = false };
         
           
        }

  

    }
}
