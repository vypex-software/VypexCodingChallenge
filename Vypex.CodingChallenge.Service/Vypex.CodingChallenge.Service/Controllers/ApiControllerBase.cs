using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using Vypex.CodingChallenge.Application.Results;
using IResult = Vypex.CodingChallenge.Application.Results.IResult;
using NotFoundResult = Vypex.CodingChallenge.Application.Results.NotFoundResult;

namespace Vypex.CodingChallenge.Service.Controllers;

[ApiController]
[Produces(MediaTypeNames.Application.Json)]
[ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
[ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
[ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
[ProducesResponseType(typeof(string), StatusCodes.Status422UnprocessableEntity)]
[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
public abstract class ApiControllerBase : ControllerBase
{
    protected IActionResult MapResultToHttpResponse<T>(IResult<T> result) =>
        result switch
        {
            SuccessfulResult<T> successfulResult => MapSuccessfulResult<T, object>(successfulResult),
            _ => MapUnsuccessfulResult(result)
        };

    private IActionResult MapSuccessfulResult<T, TResponse>(SuccessfulResult<T> successfulResult, Func<T, TResponse>? map = null) =>
        successfulResult.Type switch
        {
            SuccessfulResultType.Ok
                => map is null ? Ok(successfulResult.Content) : Ok(map(successfulResult.Content)),

            SuccessfulResultType.Created
                => map is null ? Created(string.Empty, successfulResult.Content) : Created(string.Empty, map(successfulResult.Content)),

            _ => throw new NotImplementedException($"{successfulResult.GetType()} mapping is not implemented.")
        };

    private IActionResult MapUnsuccessfulResult(IResult result) =>
        result switch
        {
            ValidationFailedResult validationFailedResult
                => ValidationProblem(new ValidationProblemDetails(validationFailedResult.ValidationErrors)),

            NotFoundResult notFoundResult => NotFound(notFoundResult.Error),

            UnexpectedErrorResult unexpectedErrorResult
                => Problem(unexpectedErrorResult.Error),

            UnprocessableResult unprocessableResult
                => UnprocessableEntity(unprocessableResult.Error),

            _ => throw new NotImplementedException($"{result.GetType()} mapping is not implemented.")
        };
}
