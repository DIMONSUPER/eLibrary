using BGNet.TestAssignment.DataAccess.Entities;

namespace BGNet.TestAssignment.DataAccess.Repository;

public interface IRepository
{
    T Create<T>(T entity) where T : class, IEntity;

    void Delete<T>(T entity) where T : class, IEntity;

    void Update<T>(T entity) where T : class, IEntity;

    IQueryable<T> GetQueryable<T>() where T : class, IEntity;

    IQueryable<T> GetNoTrackingQueryable<T>() where T : class, IEntity;
}
