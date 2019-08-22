using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace MGG.Bookloan.WebAPI.Authorize
{
    public class ClaimsAuthorizeAttribute : TypeFilterAttribute
    {
        public ClaimsAuthorizeAttribute(string claimName, string claimValue) : base(typeof(ClaimFilter))
        {
            Arguments = new object[] {new Claim(claimName, claimValue)};
        }
    }
}