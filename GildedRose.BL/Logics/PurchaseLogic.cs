using AutoMapper;
using GildedRose.DL;
using GildedRose.DL.UoW;
using GildedRose.DTO;
using System;
using System.Collections.Generic;

namespace GildedRose.BL.Logics
{
    /// <summary>
    /// Purchase Logic Class
    /// </summary>
    public class PurchaseLogic : IPurchaseLogic
    {
        /// <summary>
        /// local property to lock thread
        /// </summary>
        private static readonly object purchaseLock = new object();

        /// <summary>
        /// Local Property for mapper
        /// </summary>
        public IMapper Mapper { get; private set; }

        /// <summary>
        /// Local property for unit of work
        /// </summary>
        public IUnitOfWork UnitOfWork { get; private set; }

        /// <summary>
        /// Constructor for DI
        /// </summary>
        /// <param name="mapper">Mapper instance</param>
        /// <param name="unitOfWork">UnitofWork instance</param>
        public PurchaseLogic(IMapper mapper, IUnitOfWork unitOfWork)
        {
            Mapper = mapper;
            UnitOfWork = unitOfWork;
        }

        /// <summary>
        /// Purchase method to buy a Inventory Items
        /// </summary>
        /// <param name="model"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public int Purchase(List<PurchaseModel> model, int userId)
        {
            // Lock thread to finish and then to proceed with another call.
            lock (purchaseLock)
            {
                foreach (var purchaseModel in model)
                {
                    var availableInvetory = UnitOfWork.InventoryRepository.AvailableInventory(purchaseModel.Id, purchaseModel.Quantity);

                    // Check if item is available to proceed
                    if (availableInvetory != null)
                    {
                        availableInvetory.Stock = availableInvetory.Stock - purchaseModel.Quantity;
                        availableInvetory.ModifiedAt = DateTime.UtcNow;
                        UnitOfWork.InventoryRepository.Update(availableInvetory);

                        var purchase_inventory = new Purchase_Inventory
                        {
                            Purchase = new Purchase
                            {
                                UserId = userId,
                                PurchasedAt = DateTime.UtcNow
                            },
                            Inventory = availableInvetory,
                            NumberOfItems = purchaseModel.Quantity,
                            TotalPrice = availableInvetory.Price * purchaseModel.Quantity
                        };

                        // This will do cascade saving in entity framework.
                        UnitOfWork.PurchaseInventoryRepository.Add(purchase_inventory);
                    }
                    else
                    {
                        // TODO: there is a better way to do this. Needs to be change and have classes for exceptions.
                        throw new ArgumentException($"Inventory Id:{purchaseModel.Id} with the amount of {purchaseModel.Quantity} is not available in the stock.");
                    }
                }
                return UnitOfWork.Commit();
            }
        }
    }
}
