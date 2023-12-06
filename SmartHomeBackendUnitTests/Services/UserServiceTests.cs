using Microsoft.EntityFrameworkCore;
using Moq;
using SmartHomeBackend.Database;
using SmartHomeBackend.Models;
using SmartHomeBackend.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace SmartHomeBackendUnitTests.Services
{

    public class UserServiceTests
    {

        [Fact]
        public void UserShouldBeFoundInDatabase()
        {
            var itemsData = new List<User>
            {
                new User(){User_Id="w", Email="w@wp.pl", Password="www"},
            }.AsQueryable();

            var mockSet = new Mock<DbSet<User>>();
            mockSet.As<IQueryable<User>>().Setup(m => m.Provider).Returns(itemsData.Provider);
            mockSet.As<IQueryable<User>>().Setup(m => m.Expression).Returns(itemsData.Expression);
            mockSet.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(itemsData.ElementType);
            mockSet.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(itemsData.GetEnumerator());

            var mockContext = new Mock<SmartHomeDbContext>();
            mockContext.Setup(c => c.Users).Returns(mockSet.Object);

            var myService = new UserService(mockContext.Object);

            var result = myService.UserExists("w");

            Assert.AreEqual(true, result); 
        }

        [Fact]
        public void UserShouldNotBeFoundInDatabase()
        {
            var itemsData = new List<User>
            {
                new User(){User_Id="w", Email="w@wp.pl", Password="www"},
            }.AsQueryable();

            var mockSet = new Mock<DbSet<User>>();
            mockSet.As<IQueryable<User>>().Setup(m => m.Provider).Returns(itemsData.Provider);
            mockSet.As<IQueryable<User>>().Setup(m => m.Expression).Returns(itemsData.Expression);
            mockSet.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(itemsData.ElementType);
            mockSet.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(itemsData.GetEnumerator());

            var mockContext = new Mock<SmartHomeDbContext>();
            mockContext.Setup(c => c.Users).Returns(mockSet.Object);

            var myService = new UserService(mockContext.Object);

            var result = myService.UserExists("u");

            Assert.AreEqual(false, result);
        }

        [Fact]
        public void TheFoundUserShouldHaveCorrectCredentials()
        {
            // Arrange
            var itemsData = new List<User>
            {
                new User(){User_Id="testId", Email="test@wp.pl", Password="test123"},
            }.AsQueryable();

            var mockSet = new Mock<DbSet<User>>();
            mockSet.As<IQueryable<User>>().Setup(m => m.Provider).Returns(itemsData.Provider);
            mockSet.As<IQueryable<User>>().Setup(m => m.Expression).Returns(itemsData.Expression);
            mockSet.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(itemsData.ElementType);
            mockSet.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(itemsData.GetEnumerator());

            var mockContext = new Mock<SmartHomeDbContext>();
            mockContext.Setup(c => c.Users).Returns(mockSet.Object);

            var myService = new UserService(mockContext.Object);

            var user = myService.GetUser("testId");
            var result = user.User_Id == itemsData.ElementAt(0).User_Id &&
                user.Email == itemsData.ElementAt(0).Email &&
                user.Password == itemsData.ElementAt(0).Password;

            Assert.AreEqual(true, result); 
        }

        [Fact]
        public void TheNumberOfUsersShouldBeEqualToFour()
        {
            var itemsData = new List<User>
            {
                new User(),
                new User(),
                new User(),
                new User()
            }.AsQueryable();

            var mockSet = new Mock<DbSet<User>>();
            mockSet.As<IQueryable<User>>().Setup(m => m.Provider).Returns(itemsData.Provider);
            mockSet.As<IQueryable<User>>().Setup(m => m.Expression).Returns(itemsData.Expression);
            mockSet.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(itemsData.ElementType);
            mockSet.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(itemsData.GetEnumerator());

            var mockContext = new Mock<SmartHomeDbContext>();
            mockContext.Setup(c => c.Users).Returns(mockSet.Object);

            var myService = new UserService(mockContext.Object);

            var result = myService.GetAllUsers().Count;

            Assert.AreEqual(4, result);
        }
    }
}
