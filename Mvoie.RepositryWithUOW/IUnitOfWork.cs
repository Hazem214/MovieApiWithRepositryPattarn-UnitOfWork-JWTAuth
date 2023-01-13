using Movie.Models;
using Mvoie.RepositryWithUOW.IRopositry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mvoie.RepositryWithUOW
{
    public interface IUnitOfWork:IDisposable
    {
        public IGenreRepositry Genres { get; }
        public IMovieRepositry Movies { get; }
        public IUserRepositry Users { get; }
       
        int Complete();
        
    }
}
