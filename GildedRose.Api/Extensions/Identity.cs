using System.Security.Claims;
using System.Security.Principal;

namespace GildedRose.Api.Extensions
{
    public static class Identity
    {
        public static string GetUserId(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("UserId");
            return (claim != null) ? claim.Value : string.Empty;
        }

        public static string GetEmail(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst(ClaimTypes.Email);
            return (claim != null) ? claim.Value : string.Empty;
        }

    }
}