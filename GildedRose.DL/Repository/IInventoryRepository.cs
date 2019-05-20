namespace GildedRose.DL.Repository
{
    /// <summary>
    /// Inteface for Inventory Repository class
    /// </summary>
    public interface IInventoryRepository : IRepository<Inventory>
    {
        /// <summary>
        /// Available Inventory method interface
        /// </summary>
        /// <param name="id">Inevntory Id</param>
        /// <param name="quantity"> Inventory quantity</param>
        /// <returns>Inventory that is available</returns>
        Inventory AvailableInventory(int id, int quantity);
    }
}
