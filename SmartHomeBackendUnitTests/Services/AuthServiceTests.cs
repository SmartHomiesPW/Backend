﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SmartHomeBackend.Database;
using SmartHomeBackend.Models.Dto;
using SmartHomeBackend.Services;
using Xunit;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace SmartHomeBackendUnitTests.Services
{
    public class AuthServiceTests
    {
        // Be mindful that these tests use the cloud database, although they are working on rollback transactions

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

                var testUserRegistration = new UserRegistrationDto() { Email = "w@pl.pl", Password = "www" };

                var transaction = context.Database.BeginTransaction();
                var result = await myService.CreateNewUser(testUserRegistration);
                Assert.IsNotNull(result);
                Assert.IsTrue(
                    await myService.VerifyUser(
                        new UserLoginDto
                        {
                            Email = testUserRegistration.Email,
                            Password = testUserRegistration.Password
                        })
                    );
                transaction.Rollback();
                context.ChangeTracker.Clear();
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
                var transaction = context.Database.BeginTransaction();
                context.Users.RemoveRange(context.Users.ToList());

                var myService = new AuthService(context);

                var testUser = new UserRegistrationDto() { Email = "w@pl.pl", Password = "www" };

                var result = await myService.CreateNewUser(testUser);
                var deletedUser = await myService.RemoveUser(result?.User_Id ?? Guid.Empty);
                Assert.AreEqual(testUser.Email, deletedUser?.Email);
                transaction.Rollback();
                context.ChangeTracker.Clear();
            }
        }
        [Fact]
        public async void UserShouldBeAbleToLogIn()
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

                var testUser = new UserRegistrationDto() { Email = "w@pl.pl", FirstName = "Peter", Password = "www" };

                var transaction = context.Database.BeginTransaction();
                var result = await myService.CreateNewUser(testUser);
                Assert.IsNotNull(result);

                var foundUser = await myService.FindUserFromLogin(
                    new UserLoginDto() { Email = testUser.Email, Password = testUser.Password }
                    );
                Assert.AreEqual(testUser.Email, foundUser?.Email);
                Assert.AreEqual(testUser.FirstName, foundUser?.FirstName);

                transaction.Rollback();
                context.ChangeTracker.Clear();
            }
        }

        [Fact]
        public async void ShouldNotAllowRegistrationOfUserWithAnEmailAlreadyExistingInDB()
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

                var testUser1 = new UserRegistrationDto() { Email = "w@pl.pl", FirstName = "Peter", Password = "www" };
                var testUser2 = new UserRegistrationDto() { Email = "w@pl.pl", FirstName = "John", Password = "nnn" };

                var transaction = context.Database.BeginTransaction();

                var result1 = await myService.CreateNewUser(testUser1);
                Assert.IsNotNull(result1);

                var result2 = await myService.CreateNewUser(testUser2);
                Assert.IsNull(result2);

                transaction.Rollback();
                context.ChangeTracker.Clear();
            }
        }
    }
}
