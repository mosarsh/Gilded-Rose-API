using GildedRose.BL.Logics;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace GildedRose.Api.Controllers
{
    /// <summary>
    /// Inventory Controller class
    /// </summary>
    public class InventoryController : ApiController
    {
        /// <summary>
        /// local property inventory logic
        /// </summary>
        public IInventoryLogic _inventoryLogic { get; private set; }

        /// <summary>
        /// Constructor for DI
        /// </summary>
        /// <param name="inventoryLogic"></param>
        public InventoryController(IInventoryLogic inventoryLogic)
        {
            _inventoryLogic = inventoryLogic;
        }

        /// <summary>
        /// Get all inventories.
        /// TODO: There is a need to have pagination.
        /// </summary>
        /// <returns>HttpResponseMessage object</returns>
        [HttpGet]
        public async Task<HttpResponseMessage> Get()
        {
            try
            {
                var inventories = await _inventoryLogic.GetAllAsync();
                return Request.CreateResponse(HttpStatusCode.OK, inventories);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
