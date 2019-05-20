using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using GildedRose.Api.Controllers;
using GildedRose.BL.Logics;
using GildedRose.DTO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace GildedRose.Test.ControllersTests
{
    [TestClass]
    public class InventoryControllerTest
    {
        private Mock<IInventoryLogic> _inventoryLogic;
        private InventoryController _inventoryController;
        private List<InventoryModel> listInventory;

        [TestInitialize]
        public void Initialize()
        {

            _inventoryLogic = new Mock<IInventoryLogic>();
            _inventoryController = new InventoryController(_inventoryLogic.Object);
            _inventoryController.Request = new HttpRequestMessage();
            _inventoryController.Configuration = new HttpConfiguration();

            listInventory = new List<InventoryModel>()
            {
                new InventoryModel() { Id = 1, Name = "Hersey Shakes", Description ="Toxic effect of 2-Propanol, accidental (unintentional), subs", Price = 55.02m },
                new InventoryModel() { Id = 2, Name = "Oil - Margarine", Description ="Asphyxiation due to smothering in furniture, accidental", Price = 74.77m },
                new InventoryModel() { Id = 3, Name = "Vermacelli - Sprinkles, Assorted", Description ="Oth complication of other internal prosth dev/grft, init", Price = 15.02m }
            };
        }

        [TestMethod]
        public async Task Inventory_GetAllAsync_Success()
        {
            //Arrange
            _inventoryLogic.Setup(x => x.GetAllAsync()).ReturnsAsync(listInventory.AsEnumerable());

            //Act
            var response = await _inventoryController.Get();

            //Assert
            List<InventoryModel> Inventory;
            Assert.IsTrue(response.TryGetContentValue(out Inventory));

            Assert.AreEqual(Inventory.Count, 3);
            Assert.AreEqual(1, Inventory[0].Id);
            Assert.AreEqual("Oil - Margarine", Inventory[1].Name);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), AllowDerivedTypes = true)]
        public async Task Inventory_GetAllAsync_OnException()
        {
            //Arrange
            _inventoryLogic.Setup(x => x.GetAllAsync()).Throws(new Exception());

            //Act
            var response = await _inventoryController.Get();

            //Assert
        }
    }
}
