using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GildedRose.Api.Controllers;
using GildedRose.BL.Logics;
using GildedRose.DTO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace GildedRose.Test.ControllersTests
{
    [TestClass]
    public class UserControllerTest
    {
        private Mock<IUserLogic> _userLogic;
        private UserController _userController;
        private UserRequestModel _userRequestModel;
        private UserResponseModel _userResponseModel;

        [TestInitialize]
        public void Initialize()
        {
            _userLogic = new Mock<IUserLogic>();
            _userController = new UserController(_userLogic.Object);
            _userController.Request = new HttpRequestMessage();
            _userController.Configuration = new HttpConfiguration();

            _userRequestModel = new UserRequestModel
            {
                FirstName = "Name",
                LastName = "Surname",
                Email = "name.surname@gmail.com",
                Password = "password",
                ConfirmPassword = "password"
            };

            _userResponseModel = new UserResponseModel
            {
                FirstName = "Name",
                LastName = "Surname",
                Email = "name.surname@gmail.com"
            };
        }

        [TestMethod]
        public void Add_User_Success()
        {
            //Arrange
            _userLogic.Setup(u => u.Add(_userRequestModel)).Returns(_userResponseModel);

            //Act
            var response = _userController.Post(_userRequestModel);

            //Assert
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
        }

        [TestMethod]
        public void Add_User_Failed()
        {
            //Arrange
            _userLogic.Setup(u => u.Add(_userRequestModel)).Throws(new Exception());

            //Act
            var response = _userController.Post(_userRequestModel);

            //Assert
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}
