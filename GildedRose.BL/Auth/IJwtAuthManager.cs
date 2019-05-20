using System.Security.Claims;

namespace GildedRose.BL.Auth
{
    /// <summary>
    /// JWT Authentication Manager Interface
    /// </summary>
    public interface IJwtAuthManager
    {
        /// <summary>
        /// Method to generate JWT token
        /// </summary>
        /// <param name="username">username which is email</param>
        /// <param name="expire_in_Minutes">token expiry in minutes, defalt value is set to 10 mins</param>
        /// <returns>JWT token as a string</returns>
        string GenerateJWTToken(string username, int expire_in_Minutes = 10);

        /// <summary>
        /// Mehtod to get prinicial information
        /// </summary>
        /// <param name="token">JWT token to identify user</param>
        /// <returns>Claims Prinicial to get user info and keep in application domain</returns>
        ClaimsPrincipal GetPrincipal(string token);
    }
}
