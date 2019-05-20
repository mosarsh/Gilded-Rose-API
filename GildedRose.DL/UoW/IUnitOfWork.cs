using GildedRose.DL.Repository;
using System;
using System.Threading.Tasks;

namespace GildedRose.DL.UoW
{
    /// <summary>
    /// Unit of Work Interfaces 
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Inventory Repository Interface
        /// </summary>
        IInventoryRepository InventoryRepository { get; }

        /// <summary>
        /// Purchase Repository Interface
        /// </summary>
        IPurchaseRepository PurchaseRepository { get; }

        /// <summary>
        /// Purchase Inventory Repository Interface
        /// </summary>
        IPurchaseInventoryRepository PurchaseInventoryRepository { get; }

        /// <summary>
        /// User Repository Interface
        /// </summary>
        IUserRepository UserRepository { get; }

        /// <summary>
        /// Commit(SaveChanges) Interface
        /// </summary>
        /// <returns></returns>
        int Commit();

        /// <summary>
        /// Async Commit(SaveChanges) Interface
        /// </summary>
        /// <returns></returns>
        Task<int> CommitAsync();
    }
}
