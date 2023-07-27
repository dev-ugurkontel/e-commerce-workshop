using Core.DataAccess.Abstract;
using Core.Entity.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataAccess.Concrete.EF
{
    public class EfEntityRepositoryBase<TEntity> : IEntityRepository<TEntity> where TEntity : class, IEntity, new() 
    {
        private readonly DbContext _db;

        public EfEntityRepositoryBase(DbContext db)
        {
                _db = db;
        }
        
        public List<TEntity> GetAll(Expression<Func<TEntity, bool>>? filter = null)
        {
            return filter == null ? _db.Set<TEntity>().ToList() : _db.Set<TEntity>().Where(filter).ToList();
        }
        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            return _db.Set<TEntity>().SingleOrDefault(filter);
        }

        public TEntity Add(TEntity entity)
        {
            var addEntity = _db.Entry(entity);
            addEntity.State = EntityState.Added;
            _db.SaveChanges();
            return entity;
        }

        public void AddRange(List<TEntity> entities)
        {
            _db.Set<TEntity>().AddRange(entities);
            _db.SaveChanges();
        }

        public void Update(TEntity entity)
        {
            var updateEntity = _db.Entry(entity);
            updateEntity.State = EntityState.Modified;
            _db.SaveChanges();
        }

        public void UpdateRange(List<TEntity> entities)
        {
            _db.Set<TEntity>().UpdateRange(entities);
            _db.SaveChanges();
        }

        public void Delete(TEntity entity)
        {
            var deleteEntity = _db.Entry(entity);
            deleteEntity.State = EntityState.Deleted;
            _db.SaveChanges();

            //ALTERNATIF
            //_db.Set<TEntity>().Remove(entity);
            //_db.SaveChanges();
        }    
     
    }
}
