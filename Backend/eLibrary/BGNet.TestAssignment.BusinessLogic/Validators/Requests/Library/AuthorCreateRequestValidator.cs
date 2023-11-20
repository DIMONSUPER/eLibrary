using BGNet.TestAssignment.Models.Dtos;
using FluentValidation;

namespace BGNet.TestAssignment.BusinessLogic.Validators.Requests.Library;

public class AuthorCreateRequestValidator : AbstractValidator<AuthorDto>
{
    public AuthorCreateRequestValidator()
    {
        RuleFor(x => x.FirstName).Length(1, 16);
        RuleFor(x => x.LastName).Length(1, 16);
        RuleFor(x => x.DateOfBirth).InclusiveBetween(DateTime.Now.AddYears(-150).ToUniversalTime(), DateTime.Now.ToUniversalTime());
    }
}
