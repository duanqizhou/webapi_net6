using System.Security.Claims;

namespace webapi.Common
{
    public static class UserContextHelper
    {
        public static int? GetCurrentUserId(ClaimsPrincipal user)
        {
            var userIdClaim = user?.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (userIdClaim != null && int.TryParse(userIdClaim.Value, out var userId))
            {
                return userId;
            }
            return null;
        }

        public static string? GetCurrentUsername(ClaimsPrincipal user)
        {
            return user?.Identity?.Name;
        }
    }
}
