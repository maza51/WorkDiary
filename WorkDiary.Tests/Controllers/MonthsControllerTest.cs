using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WorkDiary.Controllers;
using WorkDiary.Entities.Diary;
using WorkDiary.Services.Diary;
using Xunit;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace WorkDiary.Tests.Controllers
{
    public class MonthsControllerTest
    {
        private MonthsController _controllerUnderTest;
        private Mock<IMonthService> _monthServiceMock;

        public MonthsControllerTest()
        {
            _monthServiceMock = new Mock<IMonthService>();
            _controllerUnderTest = new MonthsController(_monthServiceMock.Object);

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

        public class IndexTest : MonthsControllerTest
        {
            [Fact]
            public async Task Should_return_viewResult_with_all_Month()
            {
                // Arrange
                var months = new List<Month>
                {
                    new Month { YearNumber = 2022, MonthNumber = 1, UserName = "User" },
                    new Month { YearNumber = 2022, MonthNumber = 2, UserName = "User" }
                };

                _monthServiceMock
                    .Setup(x => x.GetAllAsync("User"))
                    .Returns(Task.FromResult(months));

                // Act
                var result = await _controllerUnderTest.Index();

                // Assert
                var viewResult = Assert.IsType<ViewResult>(result);
                Assert.Same(months, viewResult.Model);
            }
        }

        public class MonthTest : MonthsControllerTest
        {
            [Fact]
            public async Task Should_return_viewResult_with_one_Month()
            {
                // Arrange
                var id = 1;
                var month = new Month { Id = id, YearNumber = 2022, MonthNumber = 1, UserName = "User" };

                _monthServiceMock
                    .Setup(x => x.GetByIdAsync(id, "User"))
                    .Returns(Task.FromResult(month));

                // Act
                var result = await _controllerUnderTest.Month(id);

                // Assert
                var viewResult = Assert.IsType<ViewResult>(result);
                Assert.Same(month, viewResult.Model);
            }

            [Fact]
            public async Task Should_return_NotFound_if_there_is_no_month()
            {
                // Arrange
                var id = 1;
                Month month = null;

                _monthServiceMock
                    .Setup(x => x.GetByIdAsync(id, "User"))
                    .Returns(Task.FromResult(month));

                // Act
                var result = await _controllerUnderTest.Month(id);

                // Assert
                Assert.IsType<NotFoundObjectResult>(result);
            }
        }
    }
}
