using GildedRose.Api.Auth;
using GildedRose.Api.Extensions;
using GildedRose.Api.Filters;
using GildedRose.BL.Logics;
using GildedRose.DTO;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GildedRose.Api.Controllers
{
    public class PurchaseController : ApiController
    {
        public IPurchaseLogic _purchaseLogic { get; private set; }
        public PurchaseController(IPurchaseLogic purchaseLogic)
        {
            _purchaseLogic = purchaseLogic;
        }

        [HttpPost]
        [JwtAuthentication]
        [ValidateModel]
        public HttpResponseMessage Post([FromBody] List<PurchaseModel> purchaseModel)
        {
            try
            {
                var userId = User.Identity.GetUserId();

                if (string.IsNullOrEmpty(userId))
                {
                    throw new ArgumentNullException("Please, try to generate access token again.");
                }

                _purchaseLogic.Purchase(purchaseModel, Convert.ToInt16(userId));

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
    }
}
