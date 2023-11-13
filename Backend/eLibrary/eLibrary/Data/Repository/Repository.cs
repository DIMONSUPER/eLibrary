using eLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace eLibrary.Data.Repository;

public class Repository : IRepository
{
    protected readonly ApplicationContext _applicationContext;

    public Repository(ApplicationContext context)
    {
        _applicationContext = context;
    }

    public T Create<T>(T entity) where T : class, IEntity
    {
        _applicationContext.Set<T>().Add(entity);
        _applicationContext.SaveChanges();

        return entity;
    }

    public void Delete<T>(T entity) where T : class, IEntity
    {
        _applicationContext.Set<T>().Remove(entity);

        _applicationContext.SaveChanges();
    }

    public T Find<T>(Func<T, bool> predicate) where T : class, IEntity
    {
        return _applicationContext.Set<T>().FirstOrDefault(predicate);
    }

    public IEnumerable<T> GetAll<T>(params string[] includeProperties) where T : class, IEntity
    {
        IQueryable<T> query = _applicationContext.Set<T>();

        if (includeProperties is not null)
        {
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
        }

        return query;
    }

    public T GetById<T>(int id, params string[] includeProperties) where T : class, IEntity
    {
        return GetAll<T>(includeProperties).FirstOrDefault(x => x.Id == id);
    }

    public void Update<T>(T entity) where T : class, IEntity
    {
        _applicationContext.Set<T>().Attach(entity);
        _applicationContext.Entry(entity).State = EntityState.Modified;
        _applicationContext.SaveChanges();
    }
}
