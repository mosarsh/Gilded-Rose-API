using GildedRose.BL.Auth;
using GildedRose.BL.Logics;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Filters;

namespace GildedRose.Api.Auth
{
    /// <summary>
    /// This code needs to be refactored to add better error handling and there are many todo to check user info in database
    /// It is getting token, verifies if the token is valid, get the user name out of it, creates Identity.
    /// </summary>
    public class JwtAuthentication : Attribute, IAuthenticationFilter
    {
        public string Realm { get; set; }
        public bool AllowMultiple => false;

        [Inject]
        public IUserLogic _userLogic { get; set; }
        [Inject]
        public IJwtAuthManager _jwtAuthManager { get; set; }

        public async Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
        {

            var request = context.Request;
            var authorization = request.Headers.Authorization;
            // checking request header value having required scheme "Bearer" or not.
            if (authorization == null || authorization.Scheme != "Bearer" || string.IsNullOrEmpty(authorization.Parameter))
            {
                context.ErrorResult = new AuthFailureResult("JWT Token is Missing", request);
                return;
            }
            // Getting Token value from header values.
            var token = authorization.Parameter;
            var principal = await AuthJwtToken(token);

            if (principal == null)
            {
                context.ErrorResult = new AuthFailureResult("Invalid JWT Token", request);
            }
            else
            {
                context.Principal = principal;
            }
        }

        private bool ValidateToken(string token, out string username)
        {
            username = null;

            var simplePrinciple = _jwtAuthManager.GetPrincipal(token);
            if (simplePrinciple == null)
            {
                return false;
            }

            var identity = simplePrinciple.Identity as ClaimsIdentity;

            if (identity == null)
            {
                return false;
            }

            if (!identity.IsAuthenticated)
            {
                return false;
            }

            var usernameClaim = identity.FindFirst(ClaimTypes.Name);
            username = usernameClaim?.Value;

            if (string.IsNullOrEmpty(username))
            {
                return false;
            }

            // TODO: Shall be implemented more validation to check whether username exists in your DB or not or something else. 

            return true;
        }

        protected Task<IPrincipal> AuthJwtToken(string token)
        {
            string username;

            if (ValidateToken(token, out username))
            {
                var userIdentity = _userLogic.GetUserByEmail(username);

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, username),
                    new Claim("UserId", userIdentity.Id.ToString()),
                    new Claim(ClaimTypes.Name, userIdentity.FirstName)
                    // you can add more claims if needed like Roles ( Admin or normal user or something else)
                };

                var identity = new ClaimsIdentity(claims, "Jwt");
                IPrincipal user = new ClaimsPrincipal(identity);

                return Task.FromResult(user);
            }

            return Task.FromResult<IPrincipal>(null);
        }

        public Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
        {
            Challenge(context);
            return Task.FromResult(0);
        }

        private void Challenge(HttpAuthenticationChallengeContext context)
        {
            string parameter = null;

            if (!string.IsNullOrEmpty(Realm))
                parameter = "realm=\"" + Realm + "\"";

            //context.ChallengeWith("Bearer", parameter);
        }
    }
}