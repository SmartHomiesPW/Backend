using Microsoft.EntityFrameworkCore;
using SmartHomeBackend.Models;
using System.Collections.Generic;

namespace SmartHomeBackend.Database
{
    public class SmartHomeDbContext : DbContext
    {
        public SmartHomeDbContext(DbContextOptions<SmartHomeDbContext> options) : base(options) { }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<TemperatureSensor> TemperatureSensors { get; set; }
        public virtual DbSet<TemperatureSensorLog> TemperatureSensorsLogs { get; set; }
        public virtual DbSet<HumiditySensor> HumiditySensors { get; set; }
        public virtual DbSet<HumiditySensorLog> HumiditySensorsLogs { get; set; }
        public virtual DbSet<SunlightSensor> SunlightSensors { get; set; }
        public virtual DbSet<SunlightSensorLog> SunlightSensorsLogs { get; set; }
        public virtual DbSet<Models.System> Systems { get; set; }
        public virtual DbSet<SwitchableLight> SwitchableLights { get; set; }
        public virtual DbSet<Alarm> Alarms { get; set; }
        public virtual DbSet<AlarmSensor> AlarmSensors { get; set; }
        public virtual DbSet<AlarmTrigger> AlarmTriggers { get; set; }
    }
}
