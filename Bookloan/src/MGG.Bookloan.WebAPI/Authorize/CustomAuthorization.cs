using System.Linq;
using Microsoft.AspNetCore.Http;

namespace MGG.Bookloan.WebAPI.Authorize
{
    public class CustomAuthorization
    {
        public static bool ClientClaimVerified(HttpContext context, string claimName, string claimValue)
        {
            return context.User.Identity.IsAuthenticated &&
                   context.User.Claims.Any(x => x.Type == claimName && x.Value.Split(',').Contains(claimValue));
        }
    }
}