using Microsoft.EntityFrameworkCore;
using SmartHomeBackend.Models;
using System.Collections.Generic;

namespace SmartHomeBackend.Database
{
    public class SmartHomeDbContext : DbContext
    {
        public SmartHomeDbContext(DbContextOptions<SmartHomeDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Models.System> Systems { get; set; }
        public DbSet<SwitchableLight> SwitchableLights { get; set; }
    }
}
