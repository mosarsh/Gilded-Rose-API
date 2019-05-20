using GildedRose.DTO;
using System.Collections.Generic;

namespace GildedRose.BL.Logics
{
    /// <summary>
    /// Purchase Logic Interface
    /// </summary>
    public interface IPurchaseLogic
    {
        /// <summary>
        /// Purchase method contract
        /// </summary>
        /// <param name="model">Purchase model</param>
        /// <param name="userId">User Id</param>
        /// <returns>List of Purchase model</returns>
        int Purchase(List<PurchaseModel> model, int userId);
    }
}
