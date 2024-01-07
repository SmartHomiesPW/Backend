using Microsoft.EntityFrameworkCore;
using Moq;
using SmartHomeBackend.Database;
using SmartHomeBackend.Models;
using SmartHomeBackend.Services;
using Xunit;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace SmartHomeBackendUnitTests.Services
{

    public class UserServiceTests
    {

        [Fact]
        public void UserShouldBeFoundInDatabase()
        {
            var testUser = new User() { User_Id = Guid.NewGuid(), Email = "w@pl.pl", Password = "www" };

            var itemsData = new List<User>
            {
                testUser
            }.AsQueryable();

            var mockSet = new Mock<DbSet<User>>();
            mockSet.As<IQueryable<User>>().Setup(m => m.Provider).Returns(itemsData.Provider);
            mockSet.As<IQueryable<User>>().Setup(m => m.Expression).Returns(itemsData.Expression);
            mockSet.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(itemsData.ElementType);
            mockSet.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(itemsData.GetEnumerator());

            var mockContext = new Mock<SmartHomeDbContext>();
            mockContext.Setup(c => c.Users).Returns(mockSet.Object);

            var myService = new UserService(mockContext.Object);

            var result = myService.UserExists(testUser.User_Id.ToString());

            Assert.AreEqual(true, result);
        }

        [Fact]
        public void UserShouldNotBeFoundInDatabase()
        {
            var testUser = new User() { User_Id = Guid.NewGuid(), Email = "w@test.pl", Password = "www" };

            var itemsData = new List<User>
            {
                testUser
            }.AsQueryable();

            var mockSet = new Mock<DbSet<User>>();
            mockSet.As<IQueryable<User>>().Setup(m => m.Provider).Returns(itemsData.Provider);
            mockSet.As<IQueryable<User>>().Setup(m => m.Expression).Returns(itemsData.Expression);
            mockSet.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(itemsData.ElementType);
            mockSet.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(itemsData.GetEnumerator());

            var mockContext = new Mock<SmartHomeDbContext>();
            mockContext.Setup(c => c.Users).Returns(mockSet.Object);

            var myService = new UserService(mockContext.Object);

            var result = myService.UserExists(Guid.NewGuid().ToString());

            Assert.AreEqual(false, result);
        }

        [Fact]
        public void TheFoundUserShouldHaveCorrectCredentials()
        {
            // Arrange
            var testUser = new User() { User_Id = Guid.NewGuid(), Email = "w@test.pl", Password = "www" };

            var itemsData = new List<User>
            {
                testUser
            }.AsQueryable();

            var mockSet = new Mock<DbSet<User>>();
            mockSet.As<IQueryable<User>>().Setup(m => m.Provider).Returns(itemsData.Provider);
            mockSet.As<IQueryable<User>>().Setup(m => m.Expression).Returns(itemsData.Expression);
            mockSet.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(itemsData.ElementType);
            mockSet.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(itemsData.GetEnumerator());

            var mockContext = new Mock<SmartHomeDbContext>();
            mockContext.Setup(c => c.Users).Returns(mockSet.Object);

            var myService = new UserService(mockContext.Object);

            var user = myService.GetUser(testUser.User_Id.ToString());
            var result = user.User_Id == testUser.User_Id
                    && user.Email == testUser.Email
                    && user.Password == testUser.Password;

            Assert.AreEqual(true, result);
        }

        [Fact]
        public void TheNumberOfUsersShouldBeEqualToFour()
        {
            var itemsData = new List<User>
            {
                new User() {User_Id = Guid.NewGuid(), Email = "1@test.pl", Password = "1"},
                new User() {User_Id = Guid.NewGuid(), Email = "2@test.pl", Password = "2"},
                new User() {User_Id = Guid.NewGuid(), Email = "3@test.pl", Password = "3"},
                new User() {User_Id = Guid.NewGuid(), Email = "4@test.pl", Password = "4"}
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
