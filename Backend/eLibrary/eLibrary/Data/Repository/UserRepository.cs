using eLibrary.Models;

namespace eLibrary.Data.Repository;

public class UserRepository : IUserRepository
{
    private readonly IRepository _repository;

    public UserRepository(IRepository repository)
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
