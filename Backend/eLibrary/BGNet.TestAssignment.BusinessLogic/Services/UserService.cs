using BGNet.TestAssignment.DataAccess.Entities;
using BGNet.TestAssignment.DataAccess.Repository;

namespace BGNet.TestAssignment.BusinessLogic.Services;

public class UserService : IUserService
{
    private readonly IRepository _repository;

    public UserService(IRepository repository)
    {
        _repository = repository;
    }

    #region -- IUserRepository implementation --

    public User Create(User user)
    {
        return _repository.Create(user);
    }

    public User? GetByUsername(string username)
    {
        return _repository.Find<User>(x => x.Username == username);
    }

    public User? GetById(int id)
    {
        return _repository.Find<User>(x => x.Id == id);
    }

    #endregion
}

public interface IUserService
{
    User Create(User user);
    User? GetByUsername(string username);
    User? GetById(int id);
}
