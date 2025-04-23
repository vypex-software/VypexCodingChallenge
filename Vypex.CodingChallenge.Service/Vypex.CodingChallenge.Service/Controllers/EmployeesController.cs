using MediatR;
using Microsoft.AspNetCore.Mvc;
using Vypex.CodingChallenge.Application.Commands.AddOrUpdateLeaveCommand;
using Vypex.CodingChallenge.Application.Queries.GetEmployeeLeaveDetails;
using Vypex.CodingChallenge.Application.Queries.GetEmployeesQuery;

namespace Vypex.CodingChallenge.Service.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeesController : ApiControllerBase
{
    private readonly ISender _mediatr;
    public EmployeesController(ISender mediator)
    {
        _mediatr = mediator;
    }

    /// <summary>
    /// Add or Update employee leave details
    /// </summary>
    /// <param name="employeeId"></param>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>Success or Failed Update</returns>

    [HttpPost("{employee_id}")]
    public async Task<IActionResult> UpsertEmployeeLeaveDetails(
   [FromRoute(Name = "employee_id")] Guid employeeId,
   [FromBody] UpsertEmployeeLeaveCommand command,
   CancellationToken cancellationToken)
    {
        var result = await _mediatr.Send(command, cancellationToken);

        return MapResultToHttpResponse(result);
    }

    /// <summary>
    /// Get employee leave details
    /// </summary>
    /// <param name="employeeId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>List of leave details</returns>

    [HttpGet("{employee_id}")]
    public async Task<IActionResult> GetEmployeeLeaveDetails(
   [FromRoute(Name = "employee_id")] Guid employeeId,
   CancellationToken cancellationToken)
    {
        var query = new GetEmployeeLeaveDetailsQuery
        {
            EmployeeId = employeeId
        };
        var result = await _mediatr.Send(query, cancellationToken);

        return MapResultToHttpResponse(result);
    }

    /// <summary>
    /// Get all employees list with total leave taken
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns>Employee list with details</returns>

    [HttpGet]
    public async Task<IActionResult> GetAllEmployeeDetails(CancellationToken cancellationToken)
    {
        var query = new GetAllEmployeeDetailsQuery();
        var result = await _mediatr.Send(query, cancellationToken);
        return MapResultToHttpResponse(result);
    }
}
