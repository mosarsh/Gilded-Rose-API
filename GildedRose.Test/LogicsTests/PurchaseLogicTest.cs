using System;
using System.Collections.Generic;
using System.Threading;
using AutoMapper;
using GildedRose.BL.Logics;
using GildedRose.DL;
using GildedRose.DL.UoW;
using GildedRose.DTO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace GildedRose.Test.LogicsTests
{
    [TestClass]
    public class PurchaseLogicTest
    {
        private Mock<IMapper> _mapper;
        private Mock<IUnitOfWork> _unitOfWork;
        private PurchaseLogic _purchaseLogic;
        private List<PurchaseModel> _lstPurchaseModel;
        private List<int> _purchasedAmount;
        private Inventory _inventory;

        private void PurchaseItem()
        {
            _purchasedAmount.Add(_purchaseLogic.Purchase(_lstPurchaseModel, It.IsAny<Int32>()));
        }

        [TestInitialize]
        public void Initialize()
        {
            _mapper = new Mock<IMapper>();
            _unitOfWork = new Mock<IUnitOfWork>();
            _purchaseLogic = new PurchaseLogic(_mapper.Object, _unitOfWork.Object);
            _lstPurchaseModel = new List<PurchaseModel>()
            {
                new PurchaseModel {Id = 1, Quantity = 2 },
                new PurchaseModel {Id = 1, Quantity = 2 }
            };

            _purchasedAmount = new List<int>();

            _inventory = new Inventory { Id = 1, Name = "Test Name", Description = "Test Description", Price = 55.55m };
        }

        [TestMethod]
        public void Purchase_Success()
        {
            //arrange
            _unitOfWork.Setup(a => a.InventoryRepository.AvailableInventory(It.IsAny<Int32>(), It.IsAny<Int32>())).Returns(_inventory);
            _unitOfWork.Setup(a => a.InventoryRepository.Update(It.IsAny<Inventory>()));
            _unitOfWork.Setup(a => a.PurchaseInventoryRepository.Add(It.IsAny<Purchase_Inventory>()));

            //act
            var _thread1 = new Thread(PurchaseItem);
            var _thread2 = new Thread(PurchaseItem);
            var _thread3 = new Thread(PurchaseItem);
            var _thread4 = new Thread(PurchaseItem);

            _thread1.Start();
            _thread2.Start();
            _thread3.Start();
            _thread4.Start();

            _thread1.Join();
            _thread2.Join();
            _thread3.Join();
            _thread4.Join();

            //Assert
            Assert.AreEqual(4, _purchasedAmount.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), AllowDerivedTypes = true)]
        public void Purchase_Not_Available_Inventory()
        {
            //arrange
            _unitOfWork.Setup(a => a.InventoryRepository.AvailableInventory(It.IsAny<Int32>(), It.IsAny<Int32>())).Throws(new ArgumentNullException());

            //act
            _purchaseLogic.Purchase(_lstPurchaseModel, It.IsAny<Int32>());

            //Assert
        }
    }
}
