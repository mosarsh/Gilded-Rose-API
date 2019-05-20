using GildedRose.BL.Auth;
using GildedRose.BL.Logics;
using GildedRose.DTO;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace GildedRose.Api.Controllers
{
    /// <summary>
    /// Token Controller Class
    /// </summary>
    public class TokenController : ApiController
    {
        /// <summary>
        /// Local property for user logic
        /// </summary>
        public IUserLogic _userLogic { get; private set; }

        /// <summary>
        /// Local property for JWT Auth Manager
        /// </summary>
        public IJwtAuthManager _jwtAuthManager { get; private set; }
        public TokenController(IUserLogic userLogic, IJwtAuthManager jwtAuthManager)
        {
            _userLogic = userLogic;
            _jwtAuthManager = jwtAuthManager;
        }

        /// <summary>
        /// Method to Authenticate user and get to back
        /// </summary>
        /// <param name="model">User Login model</param>
        /// <returns>HttpResponseMessage</returns>
        [HttpPost]
        public async Task<HttpResponseMessage> Post(UserLoginModel model)
        {
            try
            {
                await _userLogic.CheckUser(model);
                var token = _jwtAuthManager.GenerateJWTToken(model.Email);
                return Request.CreateResponse(HttpStatusCode.OK, token);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized, ex.Message);
            }
        }
    }
}
