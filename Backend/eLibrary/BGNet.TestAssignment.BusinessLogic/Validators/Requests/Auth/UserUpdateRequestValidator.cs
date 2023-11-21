using BGNet.TestAssignment.BusinessLogic.Resources;
using BGNet.TestAssignment.Models.Dtos;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace BGNet.TestAssignment.BusinessLogic.Validators.Requests.Auth;

public class UserUpdateRequestValidator : AbstractValidator<ShortUserDto>
{
    public UserUpdateRequestValidator(IStringLocalizer<ValidationMessages> localizer)
    {
        RuleFor(x => x.Username).MinimumLength(4).WithMessage(x => localizer[nameof(ValidationMessages.UserUsernameMinimumLength)]);
        RuleFor(x => x.Username).MaximumLength(16).WithMessage(x => localizer[nameof(ValidationMessages.UserUsernameMaximumLength)]);
        RuleFor(x => x.Username).Matches(@"^[a-zA-Z0-9]*$").WithMessage(x => localizer[nameof(ValidationMessages.UserUsernameMatchesAlphabet)]);

        RuleFor(x => x.FirstName).MinimumLength(1).WithMessage(x => localizer[nameof(ValidationMessages.FirstNameMinimumLength)]);
        RuleFor(x => x.FirstName).MaximumLength(16).WithMessage(x => localizer[nameof(ValidationMessages.FirstNameMaximumLength)]);

        RuleFor(x => x.LastName).MinimumLength(1).WithMessage(x => localizer[nameof(ValidationMessages.LastNameMinimumLength)]);
        RuleFor(x => x.LastName).MaximumLength(16).WithMessage(x => localizer[nameof(ValidationMessages.LastNameMaximumLength)]);

        RuleFor(x => x.Address).MinimumLength(1).WithMessage(x => localizer[nameof(ValidationMessages.UserAddressMinimumLength)]);

        RuleFor(x => x.DateOfBirth).LessThanOrEqualTo(DateTime.Now.ToUniversalTime()).WithMessage(x => localizer[nameof(ValidationMessages.DateOfBirthLessThanOrEqualTo)]);
        RuleFor(x => x.DateOfBirth).GreaterThan(DateTime.Now.AddYears(-150).ToUniversalTime()).WithMessage(x => localizer[nameof(ValidationMessages.UserDateOfBirthGreaterThan)]);
    }
}
