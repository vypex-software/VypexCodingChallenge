using Microsoft.AspNetCore.Http;
using Vypex.CodingChallenge.Domain.Interfaces;

namespace Vypex.CodingChallenge.Infrastructure;

public class HttpUserContext : IUserContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public HttpUserContext(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string? UserName =>
        _httpContextAccessor.HttpContext?.User?.Identity?.Name;
}
