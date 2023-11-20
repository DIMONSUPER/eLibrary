using BGNet.TestAssignment.DataAccess.Contexts;
using BGNet.TestAssignment.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace BGNet.TestAssignment.DataAccess.Repository;

public class Repository : IRepository
{
    protected readonly LibraryDbContext _applicationContext;

    public Repository(LibraryDbContext context)
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

    public void Update<T>(T entity) where T : class, IEntity
    {
        _applicationContext.Set<T>().Attach(entity);
        _applicationContext.Entry(entity).State = EntityState.Modified;
        _applicationContext.SaveChanges();
    }

    public IQueryable<T> GetQueryable<T>() where T : class, IEntity => _applicationContext.Set<T>().AsQueryable();

    public IQueryable<T> GetNoTrackingQueryable<T>() where T : class, IEntity => _applicationContext.Set<T>().AsNoTracking();
}
