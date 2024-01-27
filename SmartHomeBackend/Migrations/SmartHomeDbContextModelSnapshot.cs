﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using SmartHomeBackend.Database;

#nullable disable

namespace SmartHomeBackend.Migrations
{
    [DbContext(typeof(SmartHomeDbContext))]
    partial class SmartHomeDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("SmartHomeBackend.Models.Alarm", b =>
                {
                    b.Property<string>("Alarm_Id")
                        .HasColumnType("text");

                    b.Property<string>("AccessCode")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Details")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("IsActive")
                        .HasColumnType("integer");

                    b.Property<int>("IsTriggered")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("System_Id")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Alarm_Id");

                    b.ToTable("Alarms");
                });

            modelBuilder.Entity("SmartHomeBackend.Models.AlarmSensor", b =>
                {
                    b.Property<string>("Alarm_Sensor_Id")
                        .HasColumnType("text");

                    b.Property<string>("Alarm_Id")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Details")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Is_On")
                        .HasColumnType("integer");

                    b.Property<int>("Movement_Detected")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Alarm_Sensor_Id");

                    b.ToTable("AlarmSensors");
                });

            modelBuilder.Entity("SmartHomeBackend.Models.AlarmTrigger", b =>
                {
                    b.Property<string>("AlarmTrigger_Id")
                        .HasColumnType("text");

                    b.Property<string>("Alarm_Id")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("AlarmTrigger_Id");

                    b.ToTable("AlarmTriggers");
                });

            modelBuilder.Entity("SmartHomeBackend.Models.DoorLock", b =>
                {
                    b.Property<string>("DoorLock_Id")
                        .HasColumnType("text");

                    b.Property<string>("Details")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("IsOn")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("System_Id")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("DoorLock_Id");

                    b.ToTable("DoorLocks");
                });

            modelBuilder.Entity("SmartHomeBackend.Models.HumiditySensor", b =>
                {
                    b.Property<string>("Sensor_Id")
                        .HasColumnType("text");

                    b.Property<string>("Details")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("System_Id")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("Value")
                        .HasColumnType("numeric");

                    b.HasKey("Sensor_Id");

                    b.ToTable("Humidity_Sensors");
                });

            modelBuilder.Entity("SmartHomeBackend.Models.HumiditySensorLog", b =>
                {
                    b.Property<int>("Log_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Log_Id"));

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<double>("Humidity")
                        .HasColumnType("double precision");

                    b.Property<int>("SensorId")
                        .HasColumnType("integer");

                    b.HasKey("Log_Id");

                    b.ToTable("Humidity_Sensors_Logs");
                });

            modelBuilder.Entity("SmartHomeBackend.Models.SunlightSensor", b =>
                {
                    b.Property<string>("Sensor_Id")
                        .HasColumnType("text");

                    b.Property<string>("Details")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("System_Id")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("Value")
                        .HasColumnType("numeric");

                    b.HasKey("Sensor_Id");

                    b.ToTable("Light_Sensors");
                });

            modelBuilder.Entity("SmartHomeBackend.Models.SunlightSensorLog", b =>
                {
                    b.Property<int>("Log_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Log_Id"));

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<double>("LightValue")
                        .HasColumnType("double precision");

                    b.Property<int>("SensorId")
                        .HasColumnType("integer");

                    b.HasKey("Log_Id");

                    b.ToTable("SunlightSensorsLogs");
                });

            modelBuilder.Entity("SmartHomeBackend.Models.SwitchableLight", b =>
                {
                    b.Property<string>("Switchable_Light_Id")
                        .HasColumnType("text");

                    b.Property<string>("Details")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("System_Id")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Value")
                        .HasColumnType("integer");

                    b.HasKey("Switchable_Light_Id");

                    b.ToTable("Switchable_Lights");
                });

            modelBuilder.Entity("SmartHomeBackend.Models.System", b =>
                {
                    b.Property<string>("System_Id")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("System_Id");

                    b.ToTable("Systems");
                });

            modelBuilder.Entity("SmartHomeBackend.Models.TemperatureSensor", b =>
                {
                    b.Property<string>("Sensor_Id")
                        .HasColumnType("text");

                    b.Property<string>("Details")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("System_Id")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("Value")
                        .HasColumnType("numeric");

                    b.HasKey("Sensor_Id");

                    b.ToTable("Temperature_Sensors");
                });

            modelBuilder.Entity("SmartHomeBackend.Models.TemperatureSensorLog", b =>
                {
                    b.Property<int>("Log_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Log_Id"));

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("SensorId")
                        .HasColumnType("integer");

                    b.Property<double>("Temperature")
                        .HasColumnType("double precision");

                    b.HasKey("Log_Id");

                    b.ToTable("TemperatureSensorsLogs");
                });

            modelBuilder.Entity("SmartHomeBackend.Models.User", b =>
                {
                    b.Property<Guid>("User_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("User_Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("SmartHomeBackend.Models.UserSystem", b =>
                {
                    b.Property<Guid>("User_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("System_Id")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("User_Id");

                    b.ToTable("UsersSystems");
                });
#pragma warning restore 612, 618
        }
    }
}
