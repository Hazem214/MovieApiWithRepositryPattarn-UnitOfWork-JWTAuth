using AutoMapper;
using Movie.EF;
using Movie.Models;
using Mvoie.RepositryWithUOW.DTO;
using Mvoie.RepositryWithUOW.IRopositry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Mvoie.RepositryWithUOW.Repositry
{
    public class MovieRepositry:BaseRepositry<MovieDetail>,IMovieRepositry
    {
        public List<string> _validExtentionIMage = new List<string> { ".jpg", ".png" };
        public long _maxsizeAlowed = 1048576;
        private readonly IMapper _mapper;
        public MovieRepositry(MovieContext context,IMapper mapper):base(context)
        {
          _mapper = mapper;
          
           
        }

        IEnumerable<MovieWithGenreDTO> IMovieRepositry.GetAll(string Include)
        {
            var Movies = base.GetAll(Include);
            List<MovieWithGenreDTO> MovieWithGenreList = new List<MovieWithGenreDTO>();
            foreach (var movie in Movies)
            {
                MovieWithGenreDTO movieWithGenreDTO = new MovieWithGenreDTO()
                {
                    Id = movie.Id,
                    Title = movie.Title,
                    Rate = movie.Rate,
                    StoreLine = movie.StoreLine,
                    Year = movie.Year,
                    ImageName = movie.ImageName,
                    GenreId = movie.GenreId,
                    Image = movie.Image,
                    Genre = movie.Gernre.Name
                };
                MovieWithGenreList.Add(movieWithGenreDTO);


            }
            return MovieWithGenreList;


        }
        public MovieWithGenreDTO GetById(Expression<Func<MovieDetail, bool>> criteria, string Includes = null)
        {
            var movie = base.GetById(criteria, Includes);
            if (movie == null) return null;

            //MovieWithGenreDTO movieWithGenreDTO = new MovieWithGenreDTO()
            //{
            //    Id = movie.Id,
            //    Title = movie.Title,
            //    Rate = movie.Rate,
            //    StoreLine = movie.StoreLine,
            //    Year = movie.Year,
            //    ImageName = movie.ImageName,
            //    GenreId = movie.GenreId,
            //    Image = movie.Image,
            //    Genre = movie.Gernre.Name
            //};

            // var  movieWithGenreDTO=_mapper.Map<MovieWithGenreDTO>(movie);
            var movieWithGenreDTO = _mapper.Map<MovieWithGenreDTO>(movie);
            movieWithGenreDTO.Genre = movie.Gernre.Name;
            

            return movieWithGenreDTO;
        }

        public MovieWithGenreDTO GetByName(Expression<Func<MovieDetail, bool>> criteria, string[] Includes = null)
        {
            var movie = base.GetByName(criteria, Includes);

            if (movie != null)
            {
                MovieWithGenreDTO movieDTO = new MovieWithGenreDTO()
                {
                    Id = movie.Id,
                    Title = movie.Title,
                    Rate = movie.Rate,
                    StoreLine = movie.StoreLine,
                    Year = movie.Year,
                    ImageName = movie.ImageName,
                    GenreId = movie.GenreId,
                    Image = movie.Image,
                    Genre = movie.Gernre.Name

                };
                return movieDTO;
            }
            return null;

        }

        public ResponseViewModel<MovieDetail> Add(MovieDTO movieDTO)
        {
            if (!_validExtentionIMage.Contains(Path.GetExtension(movieDTO.Image.FileName).ToLower())) return new ResponseViewModel<MovieDetail> { Data = null, Message = "This Extension Not Allow  ", Issuccess = false };

            if (movieDTO.Image.Length>_maxsizeAlowed) return new ResponseViewModel<MovieDetail> { Data = null, Message = "this File is Big ", Issuccess = false };

            //To Convert image into array of byte 
            using var Datastream = new MemoryStream();

                movieDTO.Image.CopyTo(Datastream);

            string NewNameOfImage = Guid.NewGuid().ToString() + movieDTO.Image.FileName;



            MovieDetail movieDetails = new MovieDetail {
                Title = movieDTO.Title,
                Year = movieDTO.Year,
                Rate = movieDTO.Rate,
                StoreLine = movieDTO.StoreLine,
                GenreId = movieDTO.GenreId,
                Image = Datastream.ToArray(),
                ImageName= NewNameOfImage
               

            };

            var movie= base.Add(movieDetails);
            return new ResponseViewModel<MovieDetail> { Data = movie, Message = "Added Successfuly", Issuccess = true };

        }

       

        public ResponseViewModel<MovieDTO> Update(int id, MovieDTO movieDTO)
        {
            MovieDetail movie = base.Find(id);
            
            if (movie == null) return new ResponseViewModel<MovieDTO>() { Data = null, Message = "This Id Not Exist", Issuccess = false };

            using var Datastream = new MemoryStream();
            movieDTO.Image.CopyTo(Datastream);
            var NewNameOfImage = Guid.NewGuid().ToString() + movieDTO.Image.FileName;

            movie.Title = movieDTO.Title;
            movie.Rate = movieDTO.Rate;
            movie.StoreLine = movieDTO.StoreLine;
            movie.Year = movieDTO.Year;
            movie.GenreId = movieDTO.GenreId;
            movie.Image = Datastream.ToArray();
            movie.ImageName = NewNameOfImage;


            base.Update(movie);

            return new ResponseViewModel<MovieDTO>() { Data = movieDTO, Message = "Updated Successfuly", Issuccess = true };
        }

        public ResponseViewModel<MovieDTO> Delete(int id)
        {
            var movie=base.Find(id);
            if(movie==null)
            return new ResponseViewModel<MovieDTO>() { Data = null, Message = "This Id Not Exist", Issuccess = false };

            base.Delete(movie);
            return new ResponseViewModel<MovieDTO>() { Data = null, Message = "Deleted Successfuly ", Issuccess = true };
        }
    }
}
