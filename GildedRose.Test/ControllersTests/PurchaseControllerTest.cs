using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Principal;
using System.Web.Http;
using GildedRose.Api.Controllers;
using GildedRose.BL.Logics;
using GildedRose.DTO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace GildedRose.Test.ControllersTests
{
    [TestClass]
    public class PurchaseControllerTest
    {
        private Mock<IPurchaseLogic> _purchaseLogic;
        private PurchaseController _purchaseController;
        private List<PurchaseModel> lstPurchaseModel;
        private ClaimsIdentity _identity;
        private string _correctUserId = "1";
        private string _wrongUserId = "";
        private Claim _claim;

        [TestInitialize]
        public void Initialize()
        {

            _purchaseLogic = new Mock<IPurchaseLogic>();
            _purchaseController = new PurchaseController(_purchaseLogic.Object);
            _purchaseController.Request = new HttpRequestMessage();
            _purchaseController.Configuration = new HttpConfiguration();

            lstPurchaseModel = new List<PurchaseModel>()
            {
                new PurchaseModel() { Id = 1, Quantity = 2 },
                new PurchaseModel() { Id = 2, Quantity = 3 },
                new PurchaseModel() { Id = 4, Quantity = 5 }
            };
        }

        [TestMethod]
        public void Purchase_Post_Success()
        {
            //Arrange
            _claim = new Claim("UserId", _correctUserId);
            _identity = Mock.Of<ClaimsIdentity>(ci => ci.FindFirst(It.IsAny<string>()) == _claim);
            _purchaseController.User = Mock.Of<IPrincipal>(ip => ip.Identity == _identity);
            _purchaseLogic.Setup(x => x.Purchase(lstPurchaseModel, Convert.ToInt32(_correctUserId)));

            //Acts
            var response = _purchaseController.Post(lstPurchaseModel);

            //Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public void Purchase_Post_OnArgumenNullException()
        {
            //Arrange
            _claim = new Claim("UserId", _wrongUserId);
            _identity = Mock.Of<ClaimsIdentity>(ci => ci.FindFirst(It.IsAny<string>()) == _claim);
            _purchaseController.User = Mock.Of<IPrincipal>(ip => ip.Identity == _identity);

            //Acts
            var response = _purchaseController.Post(lstPurchaseModel);

            //Assert
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}
