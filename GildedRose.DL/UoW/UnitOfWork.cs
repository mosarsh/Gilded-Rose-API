using GildedRose.DL.Repository;
using System.Threading.Tasks;

namespace GildedRose.DL.UoW
{
    /// <summary>
    /// Unit of Work Class
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        /// <summary>
        /// Db context
        /// </summary>
        private readonly Entities _context;

        /// <summary>
        /// Constructor to initialize DB context
        /// </summary>
        public UnitOfWork()
        {
            _context = new Entities();
        }

        private IInventoryRepository _inventoryRepository;
        /// <summary>
        /// Inventory Repository Property to set DB context and return instance
        /// </summary>
        public IInventoryRepository InventoryRepository
        {
            get
            {
                if (_inventoryRepository == null)
                {
                    _inventoryRepository = new InventoryRepository(_context);
                }
                return _inventoryRepository;
            }
        }

        private IPurchaseRepository _purchaseRepository;
        /// <summary>
        /// Purchase Repository Property to set DB context and return instance
        /// </summary>
        public IPurchaseRepository PurchaseRepository
        {
            get
            {
                if (_purchaseRepository == null)
                {
                    _purchaseRepository = new PurchaseRepository(_context);
                }
                return _purchaseRepository;
            }
        }

        private IPurchaseInventoryRepository _purchaseInventoryRepository;
        /// <summary>
        /// Purchase Repository Property to set DB context and return instance
        /// </summary>
        public IPurchaseInventoryRepository PurchaseInventoryRepository
        {
            get
            {
                if (_purchaseInventoryRepository == null)
                {
                    _purchaseInventoryRepository = new PurchaseInventoryRepository(_context);
                }
                return _purchaseInventoryRepository;
            }
        }

        private IUserRepository _userRepository;
        /// <summary>
        /// User Repository Property to set DB context and return instance
        /// </summary>
        public IUserRepository UserRepository
        {
            get
            {
                if (_userRepository == null)
                {
                    _userRepository = new UserRepository(_context);
                }
                return _userRepository;
            }
        }

        /// <summary>
        /// Commit Method to save changes
        /// </summary>
        /// <returns></returns>
        public int Commit()
        {
            return _context.SaveChanges();
        }

        /// <summary>
        /// Aync Commit methos to save changes asynchronous
        /// </summary>
        /// <returns></returns>
        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Dispose method to release unmanaged resources
        /// </summary>
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
