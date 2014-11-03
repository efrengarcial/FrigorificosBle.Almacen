using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrigorificosBle.Almacen.Core.Dao
{
    public interface IRepository<TEntity>  where TEntity : class
    {
        /*IList<TEntity> GetAll();
        TEntity Get(int id);
        void Save(TEntity model);
        void Delete(int id);
        */

        TEntity GetById(object id);
        void Insert(TEntity entity);
        void Update(TEntity entity);
        void Delete(int id);
        void Delete(TEntity entity);
        IList<TEntity> GetAll();

        //IQueryable<T> All();
        //IEnumerable<T> Find(Func<T, bool> predicate);
    }
}
