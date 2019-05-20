using Microsoft.IdentityModel.Tokens;
using System;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace GildedRose.BL.Auth
{
    /// <summary>
    /// JWT Authentication Manager Class
    /// </summary>
    public class JwtAuthManager : IJwtAuthManager
    {
        /// <summary>
        /// Secrect key taken from app.config
        /// TODO: Take it into app.config
        /// </summary>
        private static string SecretKey = "JIOBLi6eVjBpvGtWBgJzjWd2QH0sOn5tI8rIFXSHKijXWEt/3J2jFYL79DQ1vKu+EtTYgYkwTluFRDdtF41yAQ==";

        /// <summary>
        /// Method to generate JWT token
        /// </summary>
        /// <param name="username">username which is email</param>
        /// <param name="expire_in_Minutes">token expiry in minutes, defalt value is set to 10 mins</param>
        /// <returns>JWT token as a string</returns>
        public string GenerateJWTToken(string username, int expire_in_Minutes = 10)
        {
            var symmetric_Key = Convert.FromBase64String(SecretKey);
            var token_Handler = new JwtSecurityTokenHandler();

            var now = DateTime.UtcNow;
            var securitytokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                        {
                            new Claim(ClaimTypes.Name, username)
                        }),

                Expires = now.AddMinutes(Convert.ToInt32(expire_in_Minutes)),

                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(symmetric_Key), SecurityAlgorithms.HmacSha256Signature)
            };

            var stoken = token_Handler.CreateToken(securitytokenDescriptor);
            var token = token_Handler.WriteToken(stoken);

            return token;
        }

        /// <summary>
        /// Mehtod to get prinicial information
        /// </summary>
        /// <param name="token">JWT token to identify user</param>
        /// <returns>Claims Prinicial to get user info and keep in application domain</returns>
        public ClaimsPrincipal GetPrincipal(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

                if (jwtToken == null)
                {
                    return null;
                }

                var symmetricKey = Convert.FromBase64String(SecretKey);

                var validationParameters = new TokenValidationParameters()
                {
                    RequireExpirationTime = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(symmetricKey)
                };

                SecurityToken securityToken;
                var principal = tokenHandler.ValidateToken(token, validationParameters, out securityToken);

                return principal;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
