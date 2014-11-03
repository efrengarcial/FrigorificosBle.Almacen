using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//http://codereview.stackexchange.com/questions/33109/repository-service-design-pattern
//http://www.codeproject.com/Articles/526874/Repositorypluspattern-cplusdoneplusright
namespace FrigorificosBle.Almacen.Core.Dao
{
    public class EfRepository<TEntity> : IRepository<TEntity>
      where TEntity :class, new()
    {
        private readonly DbContext _context;
        private IDbSet<TEntity> _entities;

        public EfRepository(DbContext context)
        {
            _context = context;
        }

        private IDbSet<TEntity> Entities
        {
            get { return _entities ?? (_entities = _context.Set<TEntity>()); }
        }

        public TEntity GetById(object id)
        {
            return Entities.Find(id);
        }

        public void Insert(TEntity entity)
        {
            Entities.Add(entity);
            _context.SaveChanges();
        }

        public void Update(TEntity entity)
        {
            Entities.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            // Create a new instance of an entity (BaseEntity) and add the id.
           /* var entity = new TEntity
            {
                ID = id
            }; */

            // Attach the entity to the context and call the delete method.
            //Entities.Attach(entity);
            //Delete(entity);
        }

        public void Delete(TEntity entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                Entities.Attach(entity);
            }
            Entities.Remove(entity);
            _context.SaveChanges();
        }

        public IList<TEntity> GetAll()
        {
            return Entities.ToList(); 
        }
    }
}
