using BGNet.TestAssignment.Api.Models;

namespace BGNet.TestAssignment.Api.Data.Repository;

public interface IUserRepository
{
    User Create(User user);
    User? GetByUsername(string username);
    User? GetById(int id);
}
