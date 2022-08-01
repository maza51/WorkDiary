using System;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Security.Claims;
using WorkDiary.Controllers;
using WorkDiary.Services.Diary;
using WorkDiary.Controllers.Api;
using Xunit;
using System.Threading.Tasks;
using WorkDiary.Entities.Diary;

namespace WorkDiary.Tests.Controllers
{
    public class ApiDayControllerTest
    {
        private DayController _controllerUnderTest;
        private Mock<IDayService> _dayServiceMock;

        public ApiDayControllerTest()
        {
            _dayServiceMock = new Mock<IDayService>();
            _controllerUnderTest = new DayController(_dayServiceMock.Object);

            var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, "User")
                };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var claimsPrincipal = new ClaimsPrincipal(identity);

            var context = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = claimsPrincipal
                }
            };

            _controllerUnderTest.ControllerContext = context;
        }

        public class CreateOrUpdateTest : ApiDayControllerTest
        {
            [Fact]
            public async Task Should_use_CreateOrUpdateAsync()
            {
                // Arrange
                var day = new Day { DayNumber = 1 };

                // Act
                await _controllerUnderTest.CreateOrUpdate(day);

                // Assert
                _dayServiceMock.Verify(x => x.CreateOrUpdateAsync(It.IsAny<Day>(), It.IsAny<string>()), Times.Once);
            }

            [Fact]
            public async Task Should_not_use_CreateOrUpdateAsync_if_day_null()
            {
                // Arrange
                Day day = null;

                // Act
                await _controllerUnderTest.CreateOrUpdate(day);

                // Assert
                _dayServiceMock.Verify(x => x.CreateOrUpdateAsync(It.IsAny<Day>(), It.IsAny<string>()), Times.Never);
            }
        }
    }

}

