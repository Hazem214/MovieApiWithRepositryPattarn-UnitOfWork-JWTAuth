using Movie.Models;
using Mvoie.RepositryWithUOW.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Mvoie.RepositryWithUOW.IRopositry
{
    public interface IMovieRepositry:IBaseRepositry<MovieDetail>
    {
        IEnumerable<MovieWithGenreDTO> GetAll(string Include);
        MovieWithGenreDTO GetById( Expression<Func<MovieDetail, bool>> criteria, string Includes = null);
        MovieWithGenreDTO GetByName(Expression<Func<MovieDetail, bool>> criteria, string [] Includes = null);
        ResponseViewModel<MovieDetail> Add(MovieDTO movieDTO);
        ResponseViewModel<MovieDTO> Update(int id, MovieDTO genre);

        ResponseViewModel<MovieDTO> Delete(int id);

    }
}
