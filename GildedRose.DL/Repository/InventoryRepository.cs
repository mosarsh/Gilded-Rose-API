using System.Linq;

namespace GildedRose.DL.Repository
{
    /// <summary>
    /// Inventory Repository class
    /// </summary>
    public class InventoryRepository : Repository<Inventory>, IInventoryRepository
    {
        private readonly Entities dbContext;
        public InventoryRepository(Entities context) : base(context)
        {
            dbContext = context;
        }

        /// <summary>
        /// Available Inventory method
        /// </summary>
        /// <param name="id">Inevntory Id</param>
        /// <param name="quantity"> Inventory quantity</param>
        /// <returns>Inventory that is available</returns>
        public Inventory AvailableInventory(int id, int quantity)
        {
            return dbContext.Inventories.FirstOrDefault(i => i.Id == id && i.Stock >= quantity);
        }
    }
}
