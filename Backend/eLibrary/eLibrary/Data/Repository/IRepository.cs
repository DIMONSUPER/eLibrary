using BGNet.TestAssignment.Api.Models;

namespace BGNet.TestAssignment.Api.Data.Repository;

public interface IRepository
{
    IEnumerable<T> GetAll<T>(params string[] includeProperties) where T : class, IEntity;

    T Find<T>(Func<T, bool> predicate) where T : class, IEntity;

    T GetById<T>(int id, params string[] includeProperties) where T : class, IEntity;

    T Create<T>(T entity) where T : class, IEntity;

    void Update<T>(T entity) where T : class, IEntity;

    void Delete<T>(T entity) where T : class, IEntity;
}
