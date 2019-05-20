using System.Data.Entity;

namespace GildedRose.DL.Repository
{
    /// <summary>
    /// Purchase Inventory Repository Class
    /// </summary>
    public class PurchaseInventoryRepository : Repository<Purchase_Inventory>, IPurchaseInventoryRepository
    {
        public PurchaseInventoryRepository(DbContext context) : base(context) { }

    }
}
