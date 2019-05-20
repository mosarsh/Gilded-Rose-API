namespace GildedRose.DTO
{
    /// <summary>
    /// Inventory Model
    /// </summary>
    public class InventoryModel
    {
        /// <summary>
        /// Inventory Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Inventory Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Inventory Description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Inventory Price
        /// </summary>
        public decimal Price { get; set; }
    }
}
