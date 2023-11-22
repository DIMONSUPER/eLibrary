using BGNet.TestAssignment.BusinessLogic.Resources;
using BGNet.TestAssignment.Models.Dtos;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace BGNet.TestAssignment.BusinessLogic.Validators.Requests.Library;

public class BookCreateUpdaterRequestValidator : AbstractValidator<BookDto>
{
    public BookCreateUpdaterRequestValidator(IStringLocalizer<ValidationMessages> localizer)
    {
        RuleFor(x => x.Title).MinimumLength(1).WithMessage(x => localizer[nameof(ValidationMessages.BookTitleMinimumLength)]);
        RuleFor(x => x.Title).MaximumLength(64).WithMessage(x => localizer[nameof(ValidationMessages.BookTitleMaximumLength)]);

        RuleFor(x => x.PublicationYear).LessThanOrEqualTo(DateTime.Now.Year).WithMessage(x => localizer[nameof(ValidationMessages.BookPublicationYearLessThanOrEqualTo)]);

        RuleFor(x => x.Genre).MinimumLength(3).WithMessage(x => localizer[nameof(ValidationMessages.BookGenreMinimumLength)]);
        RuleFor(x => x.Genre).MaximumLength(64).WithMessage(x => localizer[nameof(ValidationMessages.BookGenreMaximumLength)]);

        RuleFor(x => x.AuthorId).NotEmpty().WithMessage(x => localizer[nameof(ValidationMessages.BookAuthorIdNotEmpty)]);
    }
}