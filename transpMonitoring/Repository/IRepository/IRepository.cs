using System.Linq.Expressions;

namespace vehicleMonitoring.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll(string? includeProperties =null);

        T Get(Expression<Func<T,bool>> filter,string? includeProperties = null);

        void Add(T entity);
        void Delete(T entity);
        void DeleteRange (IEnumerable<T> entities);
    }
}
