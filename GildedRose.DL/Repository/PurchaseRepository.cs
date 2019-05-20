using System.Data.Entity;

namespace GildedRose.DL.Repository
{
    /// <summary>
    /// Purchase Inventory Class
    /// </summary>
    public class PurchaseRepository : Repository<Purchase>, IPurchaseRepository
    {
        public PurchaseRepository(DbContext context) : base(context){}
    }
}
