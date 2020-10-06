using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using UL.Calculator.Entities;

namespace UL.Calculator.Data.Tests
{
    [TestFixture]
    public class BaseRepositoryTest
    {
        private Repository<User> _sut;

        [SetUp]
        public void Setup()
        {
            _sut = new Repository<User>(GetContext());
        }

        [Test]
        public void AuthorRepository_Searches_By_Author()
        {
            var result = _sut.FindBy(x => x.Email == "s.Thompson@ul.com").Single();

            Assert.IsNotNull(result);
            Assert.AreEqual(result.Id, 1);
        }

        private CalculatorDBContext GetContext()
        {
            var options = new DbContextOptionsBuilder<CalculatorDBContext>()
                             .UseInMemoryDatabase(Guid.NewGuid().ToString())
                             .Options;

            var context = new CalculatorDBContext(options);
            context.User.Add(new User()
            {
                Id = 1,
                FirstName = "Simon ",
                LastName = "Thompson ",
                Email = "s.Thompson@ul.com"
            });

            context.User.Add(new User()
            {
                Id = 2,
                FirstName = "Stephanie",
                LastName = "Gibbens",
                Email = "s.Gibbens@ul.com"
            });

            context.SaveChanges();
            return context;
        }
    }
}