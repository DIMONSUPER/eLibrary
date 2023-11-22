using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Results;
using BGNet.TestAssignment.Models.Responses;
using System.Net;
using System.Text;

namespace BGNet.TestAssignment.BusinessLogic.Validators;

public class ValidationResultFactory : IFluentValidationAutoValidationResultFactory
{
    #region -- IFluentValidationAutoValidationResultFactory implementation --

    public IActionResult CreateActionResult(ActionExecutingContext context, ValidationProblemDetails? validationProblemDetails)
    {
        return new BadRequestObjectResult(new ApiResponse
        {
            StatusCode = (int)HttpStatusCode.BadRequest,
            Message = "Validation failed",
            Errors = ConvertDictionaryToList(validationProblemDetails?.Errors),
        });
    }

    #endregion

    #region -- Private helpers --

    private IEnumerable<string> ConvertDictionaryToList(IDictionary<string, string[]>? dictionary)
    {
        IEnumerable<string> result;

        if (dictionary is not null && dictionary.Count > 0) 
        {
            result = dictionary.Values.SelectMany(x => x);
        }
        else
        {
            result = Enumerable.Empty<string>();
        }

        return result;
    }

    #endregion
}
