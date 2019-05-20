using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using GildedRose.Api.Controllers;
using GildedRose.BL.Auth;
using GildedRose.BL.Logics;
using GildedRose.DTO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace GildedRose.Test.ControllersTests
{
    [TestClass]
    public class TokenControllerTest
    {
        private Mock<IUserLogic> _userLogic;
        private Mock<IJwtAuthManager> _jwtAuthManager;
        private TokenController _tokenController;
        private UserLoginModel _userLoginModel;
        private string _validToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6ImFyc2hhay5tb3ZzaXNzaWFuQGdtYWlsLmNvbSIsIm5iZiI6MTU1ODEyNTA4MywiZXhwIjoxNTU4MTI1NjgzLCJpYXQiOjE1NTgxMjUwODN9.0PrWmXrVYgEeEZJTDJF2AoeRanqVyTlpn34zCwOzEjw";
        private int _expires_in_minute = 10;

        [TestInitialize]
        public void Initialize()
        {

            _userLogic = new Mock<IUserLogic>();
            _jwtAuthManager = new Mock<IJwtAuthManager>();
            _tokenController = new TokenController(_userLogic.Object, _jwtAuthManager.Object);
            _tokenController.Request = new HttpRequestMessage();
            _tokenController.Configuration = new HttpConfiguration();

            _userLoginModel = new UserLoginModel
            {
                Email = "name.surname@gmail.com",
                Password = "password"
            };
        }

        [TestMethod]
        public async Task Inventory_Login_Success()
        {
            //Arrange
            _userLogic.Setup(x => x.CheckUser(_userLoginModel)).ReturnsAsync(true);
            _jwtAuthManager.Setup(x => x.GenerateJWTToken(_userLoginModel.Email, _expires_in_minute)).Returns(_validToken);

            //Act
            var response = await _tokenController.Post(_userLoginModel);

            //Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public async Task Inventory_Login_Failed()
        {
            //Arrange
            _userLogic.Setup(x => x.CheckUser(_userLoginModel)).Throws(new Exception());

            //Act
            var response = await _tokenController.Post(_userLoginModel);

            //Assert
            Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
        }
    }
}
