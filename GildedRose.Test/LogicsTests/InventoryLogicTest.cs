using AutoMapper;
using GildedRose.BL.Logics;
using GildedRose.DL;
using GildedRose.DL.UoW;
using GildedRose.DTO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GildedRose.Test.LogicsTests
{
    [TestClass]
    public class InventoryLogicTest
    {
        private Mock<IMapper> _mapper;
        private Mock<IUnitOfWork> _unitOfWork;
        private InventoryLogic _inventoryLogic;
        private List<Inventory> _lstInventory;

        [TestInitialize]
        public void Initialize()
        {
            _mapper = new Mock<IMapper>();
            _unitOfWork = new Mock<IUnitOfWork>();
            _inventoryLogic = new InventoryLogic(_mapper.Object, _unitOfWork.Object);

            _lstInventory = new List<Inventory>()
            {
                new Inventory() { Id = 1, Name = "Hersey Shakes", Description ="Toxic effect of 2-Propanol, accidental (unintentional), subs", Price = 55.02m },
                new Inventory() { Id = 2, Name = "Oil - Margarine", Description ="Asphyxiation due to smothering in furniture, accidental", Price = 74.77m },
                new Inventory() { Id = 3, Name = "Vermacelli - Sprinkles, Assorted", Description ="Oth complication of other internal prosth dev/grft, init", Price = 15.02m }
            };
        }

        [TestMethod]
        public async Task Get_All_Async_Success()
        {
            //Arrange
            _mapper.Setup(m => m.Map<IEnumerable<Inventory>, IEnumerable<InventoryModel>>(It.IsAny<IEnumerable<Inventory>>())).Returns(new List<InventoryModel>().AsEnumerable());
            _unitOfWork.Setup(u => u.InventoryRepository.GetAllAsync()).ReturnsAsync(_lstInventory.AsEnumerable());

            //Act
            var results = await _inventoryLogic.GetAllAsync();

            //Assert
            Assert.IsNotNull(results);
        }
    }
}
