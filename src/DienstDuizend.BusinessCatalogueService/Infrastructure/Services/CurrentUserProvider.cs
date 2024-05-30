using System.Security.Claims;
using DienstDuizend.BusinessCatalogueService.Common.Interfaces;
using DienstDuizend.BusinessCatalogueService.Infrastructure.Exceptions;
using DienstDuizend.BusinessCatalogueService.Infrastructure.Persistence;

namespace DienstDuizend.BusinessCatalogueService.Infrastructure.Services;

public class CurrentUserProvider(IHttpContextAccessor httpContextAccessor, ApplicationDbContext dbContext) : ICurrentUserProvider
{
    public Guid GetCurrentUserId()
    {
        var user = httpContextAccessor.HttpContext?.User;
        
        var sub = user?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

        if (sub is null)
            throw Error.InternalError("User.UnknownUser", "The current user is unknown, try logging in again.");
        
        return Guid.Parse(sub);
    }
    
    // public string GetCurrentUserRole()
    // {
    //     var user = httpContextAccessor.HttpContext?.User;
    //         
    //     return user?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
    // }
    
}