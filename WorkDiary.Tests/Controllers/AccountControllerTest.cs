using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WorkDiary.Controllers;
using WorkDiary.Controllers.Api;
using WorkDiary.Entities.Diary;
using WorkDiary.Models;
using WorkDiary.Services;
using WorkDiary.Services.Diary;
using Xunit;

namespace WorkDiary.Tests.Controllers
{
    public class AccountControllerTest
    {
        private AccountController _controllerUnderTest;
        private Mock<IAccountService> _accountServiceMock;

        public AccountControllerTest()
        {
            _accountServiceMock = new Mock<IAccountService>();
            _controllerUnderTest = new AccountController(_accountServiceMock.Object);
        }

        public class LiginTest : AccountControllerTest
        {
            [Fact]
            public async Task Should_use_SignInAsync()
            {
                // Arrange
                var loginModel = new LoginModel { UserName = "User", Password = "Password" };

                // Act
                await _controllerUnderTest.Login(loginModel);

                // Assert
                _accountServiceMock.Verify(x => x.SignInAsync(loginModel, It.IsAny<HttpContext>()), Times.Once);
            }
        }

        public class RegisterTest : AccountControllerTest
        {
            [Fact]
            public async Task Should_use_RegisterAsync()
            {
                // Arrange
                var registerModel = new RegisterModel { UserName = "User", Password = "Password" };

                // Act
                await _controllerUnderTest.Register(registerModel);

                // Assert
                _accountServiceMock.Verify(x => x.RegisterAsync(registerModel, It.IsAny<HttpContext>()), Times.Once);
            }
        }
    }
}

