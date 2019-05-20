using GildedRose.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GildedRose.BL.Logics
{
    /// <summary>
    /// Inventory Logic Interface
    /// </summary>
    public interface IInventoryLogic
    {
        /// <summary>
        /// Get All Inventory Items Asynchronous
        /// </summary>
        /// <returns>IEnumerable Items of Inventory</returns>
        Task<IEnumerable<InventoryModel>> GetAllAsync();
    }
}
