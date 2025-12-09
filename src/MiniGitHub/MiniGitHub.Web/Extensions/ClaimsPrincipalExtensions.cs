using System.Security.Claims;
using MiniGitHub.Domain.Entities;

namespace MiniGitHub.Web.Extensions;

public static class ClaimsPrincipalExtensions {
    public static long GetUserId(this ClaimsPrincipal self) {
        return TryGetUserId(self) ?? -1;
    }
    
    public static string GetUsername(this ClaimsPrincipal self) {
        return TryGetUsername(self) ?? string.Empty;
    }
    
    public static string GetEmail(this ClaimsPrincipal self) {
        return TryGetEmail(self) ?? string.Empty;
    }

    public static bool TryGetUserId(this ClaimsPrincipal self, out long userId) {
        var res = TryGetUserId(self);
        if (res == null) {
            userId = -1;
            return false;
        }

        userId = res.Value;
        return true;
    } 
    
    public static long? TryGetUserId(this ClaimsPrincipal self) {
        return self.FindFirst(ClaimTypes.NameIdentifier)?.Value.ToLong();
    }
    
    public static string? TryGetEmail(this ClaimsPrincipal self) {
        return self.FindFirst(ClaimTypes.Email)?.Value;
    }

    public static string? TryGetUsername(this ClaimsPrincipal self) {
        return self.FindFirst(ClaimTypes.Name)?.Value;
    }
    
    public static bool IsAuthenticated(this ClaimsPrincipal? self) {
        if (self == null) return false;
        return self.Identity?.IsAuthenticated == true;
    }
}