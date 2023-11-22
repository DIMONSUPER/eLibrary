using BGNet.TestAssignment.BusinessLogic.Resources;
using BGNet.TestAssignment.Models.Dtos;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace BGNet.TestAssignment.BusinessLogic.Validators.Requests.Library;

public class AuthorCreateUpdaterRequestValidator : AbstractValidator<AuthorDto>
{
    public AuthorCreateUpdaterRequestValidator(IStringLocalizer<ValidationMessages> localizer)
    {
        RuleFor(x => x.FirstName).MinimumLength(1).WithMessage(x => localizer[nameof(ValidationMessages.FirstNameMinimumLength)]);
        RuleFor(x => x.FirstName).MaximumLength(16).WithMessage(x => localizer[nameof(ValidationMessages.FirstNameMaximumLength)]);

        RuleFor(x => x.LastName).MinimumLength(1).WithMessage(x => localizer[nameof(ValidationMessages.LastNameMinimumLength)]);
        RuleFor(x => x.LastName).MaximumLength(16).WithMessage(x => localizer[nameof(ValidationMessages.LastNameMaximumLength)]);

        RuleFor(x => x.DateOfBirth).LessThanOrEqualTo(DateTime.UtcNow).WithMessage(x => localizer[nameof(ValidationMessages.DateOfBirthLessThanOrEqualTo)]);
    }
}
