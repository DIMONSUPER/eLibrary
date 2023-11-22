using BGNet.TestAssignment.DataAccess.Entities;
using BGNet.TestAssignment.DataAccess.Repository;
using BGNet.TestAssignment.Models.Dtos;
using BGNet.TestAssignment.Models.Requests;
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

    public bool VerifyLoginRequest(LoginRequest loginRequest)
    {
        var user = GetFullUserByUsername(loginRequest.Username);

        return user is not null && BCrypt.Net.BCrypt.Verify(loginRequest.Password, user.Password);
    }

    public bool VerifyUsernameIsNotBusy(string username)
    {
        return !_repository.GetNoTrackingQueryable<User>().Any(x => x.Username == username);
    }

    #endregion
}

public interface IUserService
{
    ShortUserDto Create(FullUserDto fullUserDto);
    FullUserDto? GetFullUserByUsername(string username);
    ShortUserDto? GetShortUserByUsername(string username);
    bool VerifyLoginRequest(LoginRequest loginRequest);
    bool VerifyUsernameIsNotBusy(string username);
}
