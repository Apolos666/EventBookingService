using System.Security.Principal;

namespace BuildingBlocks.Services.HttpAccessor;

public interface IUserIdentityAccessor
{
    IIdentity UserIdentity { get; }
    string UserId { get; }
    string UserName { get; }
    string Email { get; }
    string GetClaimValue(string claimType);
}