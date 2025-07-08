using Microsoft.AspNetCore.Mvc;
using Vypex.CodingChallenge.Application.Models;
using Vypex.CodingChallenge.Application.Services;

namespace Vypex.CodingChallenge.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProjectsController : ControllerBase
{
    [HttpGet]
    public Task<ICollection<ProjectSummaryDto>> GetAllAsync([FromServices] IProjectService projectService, [FromQuery] string? search) =>
        projectService.GetProjectSummariesAsync(search);

    [HttpGet("{id}")]
    public Task<ProjectDto?> GetById([FromServices] IProjectService projectService, [FromRoute] int id) =>
        projectService.GetProjectAsync(id);

    [HttpPatch("{id}")]
    public Task<ProjectDto?> UpdateProject([FromServices] IProjectService projectService, [FromRoute] int id, [FromBody] ProjectUpdateDto project) =>
        projectService.UpdateProjectAsync(id, project);
}
