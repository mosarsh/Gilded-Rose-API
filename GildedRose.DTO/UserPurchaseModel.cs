using System.Collections.Generic;

namespace GildedRose.DTO
{
    /// <summary>
    /// User Purchase Model
    /// </summary>
    public class UserPurchaseModel
    {
        /// <summary>
        /// User Id
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Purchase Items
        /// </summary>
        public List<PurchaseModel> Items { get; set; }
    }
}
