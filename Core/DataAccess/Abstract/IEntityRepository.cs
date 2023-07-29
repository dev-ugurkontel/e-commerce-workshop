using Core.Entity.Abstract;
using System.Linq.Expressions;

namespace Core.DataAccess.Abstract
{
    public interface IEntityRepository<T> where T : class,IEntity,new()
    {
        List<T> GetAll(Expression<Func<T,bool>>? filter = null);
        T Get(Expression<Func<T, bool>> filter);
        T Add(T entity);
        void AddRange(List<T> entities);
        void UpdateRange(List<T> entities);
        void Update(T entity);
        void Delete(T entity);        
    }
}
