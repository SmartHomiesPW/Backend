using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SmartHomeBackend.Database;
using SmartHomeBackend.Models;
using SmartHomeBackend.Services;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace SmartHomeBackendUnitTests.Services
{
    public class AuthServiceTests
    {
        [Fact]
        public async void UserShouldBeAddedToDatabase()
        {
            var options = new DbContextOptionsBuilder<SmartHomeDbContext>()
                .UseSqlite("Data Source=test.db").Options;

            using (var context = new SmartHomeDbContext(options))
            {
                var sqlCommand = $"DELETE FROM Users;";
                context.Database.ExecuteSqlRaw(sqlCommand);
                
                var myService = new AuthService(context);

                var result = await myService.CreateNewUser(new User { User_Id = "1", Email = "user1@wp.pl", Password="lolz" });
                Assert.IsTrue(await myService.VerifyUser(result));
            }
        }

        [Fact]
        public async void UserShouldBeRemovedFromDatabase()
        {
            var options = new DbContextOptionsBuilder<SmartHomeDbContext>()
                .UseSqlite("Data Source=test.db").Options;

            using (var context = new SmartHomeDbContext(options))
            {
                var sqlCommand = $"DELETE FROM Users;";
                context.Database.ExecuteSqlRaw(sqlCommand);

                var myService = new AuthService(context);

                var user = new User { User_Id = "1", Email = "user1@wp.pl", Password = "lolz" };
                
                await myService.CreateNewUser(user);
                await myService.RemoveUser(user);
                
                Assert.IsFalse(await myService.VerifyUser(user));
            }
        }
    }
}
