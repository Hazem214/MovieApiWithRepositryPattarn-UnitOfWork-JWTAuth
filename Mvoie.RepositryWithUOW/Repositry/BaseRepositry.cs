using Microsoft.EntityFrameworkCore;
using Movie.EF;
using Mvoie.RepositryWithUOW.IRopositry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Mvoie.RepositryWithUOW.Repositry
{
    public class BaseRepositry<T>:IBaseRepositry<T> where T : class
    {
        private readonly MovieContext _cotnext;

        public BaseRepositry( MovieContext Cotnext)
        {
            _cotnext = Cotnext;
        }

     
        public IEnumerable<T> GetAll(string include)
        {
            return _cotnext.Set<T>().Include(include).ToList();
            
        }

        public T Find(int id)
        {
            return _cotnext.Set<T>().Find(id);
        }

        public T GetById( Expression<Func<T, bool>> criteria, string  Includes = null)
        {

 
            return _cotnext.Set<T>().Include(Includes).SingleOrDefault(criteria);

        }

        public T GetByName(Expression<Func<T, bool>> criteria, string[] Includes = null)
        {
            IQueryable<T> query = _cotnext.Set<T>();

            if (Includes != null)
                foreach (var incluse in Includes)
                    query = query.Include(incluse);

            return query.SingleOrDefault(criteria);



        }
        public  T Add(T Entity)
        {
            _cotnext.Set<T>().Add(Entity);  
            return Entity;
        }

        public T Update(T Entity)
        {
          
            _cotnext.Set<T>().Update(Entity);

            return Entity;
        }

        public void Delete(T Entity)
        {
            _cotnext.Set<T>().Remove(Entity);
        }

   
    }
}
