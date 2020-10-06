using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using UL.Calculator.Core.Model;
using UL.Calculator.Services;
using UL.Calculator.WebAPI.Controllers;

namespace UL.Calculator.WebAPI.Tests.Controllers
{
    [TestFixture]
    public class AccountControllerTest
    {
        private AccountController _sut;
        private Mock<IUserService> _userService;

        [SetUp]
        public void Setup()
        {
            _userService = new Mock<IUserService>();
            _sut = new AccountController(_userService.Object);
        }

        [Test]
        public async Task Should_Return_Success_Response_For_Right_Credentials()
        {
            // Arrange

            _userService.Setup(x => x.Authenticate(It.IsAny<Credentials>()))
                           .ReturnsAsync(MockUserInfoResults());

            //Act
            var result = await _sut.Authenticate(new Credentials()) as OkObjectResult;

            //Assert
            _userService.Verify(x => x.Authenticate(It.IsAny<Credentials>()), Times.Once);
            Assert.IsNotNull(result);
            result.Value.Should().BeOfType<UserInfo>().Which.Name.Should().Be("Simon Thompson");
            result.Should().BeOfType<OkObjectResult>().Which.StatusCode.Should().Be(200);
        }

        [Test]
        public async Task Should_Return_Unauthorized_Response_For_Wrong_Credentials()
        {
            // Arrange

            _userService.Setup(x => x.Authenticate(It.IsAny<Credentials>()))
                           .ReturnsAsync((UserInfo)null);

            //Act
            var result = await _sut.Authenticate(new Credentials()) as UnauthorizedObjectResult;

            //Assert
            _userService.Verify(x => x.Authenticate(It.IsAny<Credentials>()), Times.Once);

            Assert.IsNotNull(result);
            result.Should().BeOfType<UnauthorizedObjectResult>().Which.StatusCode.Should().Be(401);
        }

        private static UserInfo MockUserInfoResults()
        {
            return new UserInfo
            {
                Name = "Simon Thompson",
                SubscriptionType = "Premium"
            };
        }
    }
}