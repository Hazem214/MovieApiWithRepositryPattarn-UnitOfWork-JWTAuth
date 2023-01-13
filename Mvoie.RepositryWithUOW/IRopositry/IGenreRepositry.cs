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
    public interface IGenreRepositry:IBaseRepositry<Genre>
    {
        IEnumerable< GenreWithMovie> GetAll(string include);
        GenreWithMovie GetById( Expression<Func<Genre, bool>> criteria, string  Includes = null);

        GenreWithMovie GetByName(Expression<Func<Genre, bool>> criteria, string[] Includes = null);
        ResponseViewModel<Genre> Add(GenreDTO genreDto);

        ResponseViewModel<GenreDTO> Update(int id, GenreDTO genre);
        ResponseViewModel<GenreDTO> Delete(int id);

    }
}
