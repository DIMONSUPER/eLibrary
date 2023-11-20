using BGNet.TestAssignment.DataAccess.Entities;
using BGNet.TestAssignment.DataAccess.Repository;
using BGNet.TestAssignment.Models.Dtos;
using Mapster;

namespace BGNet.TestAssignment.BusinessLogic.Services;

public class UserService : IUserService
{
    private readonly IRepository _repository;

    public UserService(IRepository repository)
    {
        _repository = repository;
    }

    #region -- IUserRepository implementation --

    public UserDto Create(CreateUserDto createUserDto)
    {
        var user = createUserDto.Adapt<User>();

        return _repository.Create(user).Adapt<UserDto>();
    }

    public UserDto? GetByUsername(string username)
    {
        return _repository.Find<User>(x => x.Username == username).Adapt<UserDto>();
    }

    public UserDto? GetById(int id)
    {
        return _repository.Find<User>(x => x.Id == id).Adapt<UserDto>();
    }

    #endregion
}

public interface IUserService
{
    UserDto Create(CreateUserDto createUserDto);
    UserDto? GetByUsername(string username);
    UserDto? GetById(int id);
}
