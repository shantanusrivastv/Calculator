using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using UL.Calculator.Core.Model;
using UL.Calculator.Data;
using UL.Calculator.Entities;
using UL.Calculator.Services.Mapper;

using ms = Microsoft.Extensions.Configuration;

namespace UL.Calculator.Services.Tests

{
    [TestFixture]
    public class UserServiceTest
    {
        private ms.IConfiguration _config;
        private IMapper _mapper;
        private Mock<IRepository<UserLogin>> _repository;
        private UserService _sut;

        [SetUp]
        public void Setup()
        {
            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(new CalculatorMapper()));
            //Seeing Up AutoMapper Profile to easy setup and we don't have to mock all mappings
            _mapper = new AutoMapper.Mapper(mapperConfiguration);
            _config = GetConfiguration(); //Making use of appsettings.json file
            _repository = new Mock<IRepository<UserLogin>>();

            _sut = new UserService(_config, _mapper, _repository.Object);
        }

        [Test]
        public async Task Should_Generate_JWT_Token_With_UserInfo_For_Valid_Credentials()
        {
            // Arrange

            _repository.Setup(c =>
                            c.FindBy(It.IsAny<Expression<Func<UserLogin, bool>>>(),
                                      It.IsAny<Expression<Func<UserLogin, object>>[]>()))
                            .Returns(MockUserInfoResults());
            //Act

            var result = await _sut.Authenticate(new Credentials());

            //Assert
            Assert.IsNotNull(result);
            result.Should().BeOfType<UserInfo>().Which.Name.Should().Be("Simon Thompson");
            result.Should().BeOfType<UserInfo>().Which.Token.Should().NotBeNullOrEmpty();
        }

        [Test]
        public async Task Should_Return_Null_For_InValid_Credentials()
        {
            // Arrange

            _repository.Setup(c =>
                            c.FindBy(It.IsAny<Expression<Func<UserLogin, bool>>>(),
                                      It.IsAny<Expression<Func<UserLogin, object>>[]>()))
                            .Returns(MockEmptyUserInfoResults());
            //Act

            var result = await _sut.Authenticate(new Credentials());

            //Assert
            Assert.IsNull(result);
        }

        private IQueryable<UserLogin> MockUserInfoResults()
        {
            IList<UserLogin> userLogins = new List<UserLogin>()
            {
                new UserLogin()
                {
                    Username = "s.Thompson@ul.com",
                    Password = "admin",
                    SubscriptionType = SubscriptionType.Premium,
                    User = new User()
                    {
                        FirstName = "Simon",
                        LastName = "Thompson"
                    }
                }
            };
            return userLogins.AsQueryable();
        }

        private IQueryable<UserLogin> MockEmptyUserInfoResults()
        {
            IList<UserLogin> userLogins = new List<UserLogin>();
            return userLogins.AsQueryable();
        }

        private IConfiguration GetConfiguration()
        {
            return new ConfigurationBuilder()
                      .SetBasePath(Directory.GetCurrentDirectory())
                      .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                      .AddEnvironmentVariables()
                      .Build();
        }
    }
}