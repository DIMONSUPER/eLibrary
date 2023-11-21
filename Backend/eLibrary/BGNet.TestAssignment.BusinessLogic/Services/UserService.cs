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

    public ShortUserDto Create(FullUserDto createUserDto)
    {
        var user = createUserDto.Adapt<User>();

        return _repository.Create(user).Adapt<ShortUserDto>();
    }

    public FullUserDto? GetFullUserByUsername(string username)
    {
        return _repository.GetNoTrackingQueryable<User>().SingleOrDefault(x => x.Username == username).Adapt<FullUserDto>();
    }

    public ShortUserDto? GetShortUserByUsername(string username)
    {
        return _repository.GetNoTrackingQueryable<User>().SingleOrDefault(x => x.Username == username).Adapt<ShortUserDto>();
    }

    public ShortUserDto? GetById(int id)
    {
        return _repository.GetNoTrackingQueryable<User>().SingleOrDefault(x => x.Id == id).Adapt<ShortUserDto>();
    }

    #endregion
}

public interface IUserService
{
    ShortUserDto Create(FullUserDto fullUserDto);
    FullUserDto? GetFullUserByUsername(string username);
    ShortUserDto? GetShortUserByUsername(string username);
    ShortUserDto? GetById(int id);
}
