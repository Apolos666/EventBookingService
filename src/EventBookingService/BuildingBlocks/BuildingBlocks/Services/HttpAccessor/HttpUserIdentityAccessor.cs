using System.Security.Claims;
using System.Security.Principal;
using Microsoft.AspNetCore.Http;

namespace BuildingBlocks.Services.HttpAccessor;

public class HttpUserIdentityAccessor
    (IHttpContextAccessor httpContextAccessor)
    : IUserIdentityAccessor
{
    public IIdentity UserIdentity => httpContextAccessor.HttpContext?.User.Identity;
    public string UserId => GetClaimValue(ClaimTypes.NameIdentifier);
    public string UserName => GetClaimValue("preferred_username");
    public string Email => GetClaimValue(ClaimTypes.Email);
    
    public string GetClaimValue(string claimType)
    {
        return httpContextAccessor.HttpContext?.User.FindFirstValue(claimType);
    }
}