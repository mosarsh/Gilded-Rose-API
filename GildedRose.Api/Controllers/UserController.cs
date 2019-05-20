using GildedRose.BL.Logics;
using GildedRose.DTO;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GildedRose.Api.Controllers
{
    /// <summary>
    /// User Controller Class
    /// </summary>
    public class UserController : ApiController
    {
        /// <summary>
        /// Local property for User logic
        /// </summary>
        public IUserLogic _userLogic { get; private set; }

        /// <summary>
        /// UserController Constructor for DI
        /// </summary>
        /// <param name="userLogic"></param>
        public UserController(IUserLogic userLogic)
        {
            _userLogic = userLogic;
        }

        /// <summary>
        /// Add a user Method
        /// </summary>
        /// <param name="model">User Request model</param>
        /// <returns>HttpResponseMessage</returns>
        public HttpResponseMessage Post(UserRequestModel model)
        {
            try
            {
                //TODO: Validation check shall be added before passing to logic layer
                var user = _userLogic.Add(model);

                return Request.CreateResponse(HttpStatusCode.Created, user);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }

        }
    }
}
