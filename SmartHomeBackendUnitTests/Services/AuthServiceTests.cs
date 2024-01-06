using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
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
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            var options = new DbContextOptionsBuilder<SmartHomeDbContext>()
                .UseNpgsql(config.GetConnectionString("DefaultConnection"));

            using (var context = new SmartHomeDbContext(options.Options))
            {
                context.Users.RemoveRange(context.Users.ToList());

                var myService = new AuthService(context);

                var result = await myService.CreateNewUser(new User { User_Id = "1", Email = "adek@test.pl", Password="lolz" });
                Assert.IsTrue(await myService.VerifyUser(result));
            }
        }

        [Fact]
        public async void UserShouldBeRemovedFromDatabase()
        {
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            var options = new DbContextOptionsBuilder<SmartHomeDbContext>()
                .UseNpgsql(config.GetConnectionString("DefaultConnection"));

            using (var context = new SmartHomeDbContext(options.Options))
            {
                context.Users.RemoveRange(context.Users.ToList());

                var myService = new AuthService(context);

                var user = new User { User_Id = "1", Email = "adek@test.pl", Password = "lolz" };
                
                await myService.CreateNewUser(user);
                await myService.RemoveUser(user);
                
                Assert.IsFalse(await myService.VerifyUser(user));
            }
        }
    }
}
