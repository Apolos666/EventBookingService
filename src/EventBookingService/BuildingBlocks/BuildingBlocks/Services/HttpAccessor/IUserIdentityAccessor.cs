using System.Security.Principal;
using Microsoft.AspNetCore.Http;

namespace BuildingBlocks.Services.HttpAccessor;

public interface IUserIdentityAccessor
{
    public HttpContext HttpContext { get; }
    IIdentity UserIdentity { get; }
    string UserId { get; }
    string UserName { get; }
    string Email { get; }
    string GetClaimValue(string claimType);
}