using BGNet.TestAssignment.BusinessLogic.Services;
using BGNet.TestAssignment.BusinessLogic.Validators.Requests.Library;
using FluentValidation;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Results;
using BGNet.TestAssignment.BusinessLogic.Validators;

namespace BGNet.TestAssignment.BusinessLogic;

public static class DependencyInjection
{
    public static IServiceCollection RegisterServicesAndValidators(this IServiceCollection services)
    {
        services.AddLocalization();
        services.AddValidatorsFromAssemblyContaining<AuthorCreateUpdaterRequestValidator>();
        services.AddFluentValidationAutoValidation(configuartion => configuartion.OverrideDefaultResultFactoryWith<ValidationResultFactory>());

        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IAuthorService, AuthorService>();
        services.AddScoped<IBookService, BookService>();
        services.AddScoped<IAuthService, AuthService>();

        return services;
    }
}
