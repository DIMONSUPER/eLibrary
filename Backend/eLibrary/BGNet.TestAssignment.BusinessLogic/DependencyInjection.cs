using BGNet.TestAssignment.BusinessLogic.Services;
using BGNet.TestAssignment.BusinessLogic.Validators.Requests.Library;
using BGNet.TestAssignment.DataAccess.Repository;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;

namespace BGNet.TestAssignment.BusinessLogic;

public static class DependencyInjection
{
    public static IServiceCollection RegisterServicesAndValidators(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<AuthorCreateRequestValidator>();
        services.AddFluentValidationAutoValidation();

        services.AddScoped<IRepository, Repository>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IAuthorService, AuthorService>();
        services.AddScoped<IBookService, BookService>();

        return services;
    }
}
