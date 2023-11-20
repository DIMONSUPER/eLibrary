using BGNet.TestAssignment.DataAccess.Entities;
using BGNet.TestAssignment.DataAccess.Repository;
using BGNet.TestAssignment.Models.Dtos;
using Mapster;

namespace BGNet.TestAssignment.BusinessLogic.Services;

public class AuthorService : IAuthorService
{
    private readonly IRepository _repository;

    public AuthorService(IRepository repository)
    {
        _repository = repository;
    }

    #region -- IAuthorRepository implementation --

    public AuthorDto Create(AuthorDto authorDto)
    {
        var author = authorDto.Adapt<Author>();

        return _repository.Create(author).Adapt<AuthorDto>();
    }

    public void Delete(int id)
    {
        var author = _repository.GetById<Author>(id);

        if (author is not null)
        {
            _repository.Delete(author);
        }
    }

    public IEnumerable<AuthorDto> GetAll()
    {
        return _repository.GetAll<Author>().Adapt<IEnumerable<AuthorDto>>();
    }

    public AuthorDto? GetById(int id)
    {
        return _repository.GetById<Author>(id).Adapt<AuthorDto>();
    }

    public void Update(AuthorDto authorDto)
    {
        var author = authorDto.Adapt<Author>();

        _repository.Update(author);
    }

    #endregion
}

public interface IAuthorService
{
    AuthorDto Create(AuthorDto authorDto);
    IEnumerable<AuthorDto> GetAll();
    void Update(AuthorDto authorDto);
    AuthorDto? GetById(int id);
    void Delete(int id);
}
