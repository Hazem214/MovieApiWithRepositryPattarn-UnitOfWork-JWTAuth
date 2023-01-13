using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Mvoie.RepositryWithUOW.IRopositry
{
    public interface IBaseRepositry<T> where T : class
    {
        IEnumerable<T> GetAll(string include);
        T Find(int id);
        T GetById( Expression<Func<T, bool>> criteria, string  Includes = null);
        T GetByName(Expression<Func<T,bool>> criteria,string[] Includes =null);
        T Add(T Entity);
        T Update(T Entity);
        void Delete(T Entity);
    }
}
