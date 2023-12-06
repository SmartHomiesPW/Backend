BEGIN TRANSACTION;
CREATE TABLE IF NOT EXISTS "System_Users" (
	"Id"	TEXT NOT NULL,
	"System_Id"	TEXT NOT NULL,
	"User_Id"	TEXT NOT NULL,
	PRIMARY KEY("Id"),
	FOREIGN KEY("User_Id") REFERENCES "Users"("User_Id"),
	FOREIGN KEY("System_Id") REFERENCES "Systems"("System_Id")
);
CREATE TABLE IF NOT EXISTS "Systems" (
	"System_Id"	TEXT NOT NULL,
	"Name"	TEXT NOT NULL,
	PRIMARY KEY("System_Id")
);
CREATE TABLE IF NOT EXISTS "Light_Sensors" (
	"Sensor_Id"	TEXT NOT NULL,
	"System_Id"	TEXT NOT NULL,
	"Name"	TEXT NOT NULL,
	PRIMARY KEY("Sensor_Id"),
	FOREIGN KEY("System_Id") REFERENCES "Systems"("System_Id")
);
CREATE TABLE IF NOT EXISTS "Humidity_Sensors" (
	"Sensor_Id"	TEXT NOT NULL,
	"System_Id"	TEXT NOT NULL,
	"Name"	TEXT NOT NULL,
	PRIMARY KEY("Sensor_Id"),
	FOREIGN KEY("System_Id") REFERENCES "Systems"("System_Id")
);
CREATE TABLE IF NOT EXISTS "Temperature_Sensors" (
	"Sensor_Id"	TEXT NOT NULL,
	"System_Id"	TEXT NOT NULL,
	"Name"	TEXT NOT NULL,
	PRIMARY KEY("Sensor_Id"),
	FOREIGN KEY("System_Id") REFERENCES "Systems"("System_Id")
);
CREATE TABLE IF NOT EXISTS "Switchable_Lights" (
	"Switchable_Light_Id"	TEXT NOT NULL,
	"System_Id"	TEXT NOT NULL,
	"Name"	TEXT NOT NULL,
	"Value"	INTEGER NOT NULL,
	PRIMARY KEY("Switchable_Light_Id"),
	FOREIGN KEY("System_Id") REFERENCES "Systems"("System_Id")
);
CREATE TABLE IF NOT EXISTS "Alarms" (
	"Alarm_Id"	TEXT NOT NULL,
	"System_Id"	TEXT NOT NULL,
	"Name"	TEXT NOT NULL,
	PRIMARY KEY("Alarm_Id"),
	FOREIGN KEY("System_Id") REFERENCES "Systems"("System_Id")
);
CREATE TABLE IF NOT EXISTS "Alarm_Triggers" (
	"Trigger_Id"	TEXT NOT NULL,
	"Alarm_Id"	TEXT NOT NULL,
	"Date"	TEXT NOT NULL,
	PRIMARY KEY("Trigger_Id"),
	FOREIGN KEY("Alarm_Id") REFERENCES "Alarms"("Alarm_Id")
);
CREATE TABLE IF NOT EXISTS "Cameras" (
	"Camera_Id"	TEXT NOT NULL,
	"System_Id"	TEXT NOT NULL,
	"Name"	TEXT NOT NULL,
	"Data"	BLOB NOT NULL,
	PRIMARY KEY("Camera_Id"),
	FOREIGN KEY("System_Id") REFERENCES "Systems"("System_Id")
);
CREATE TABLE IF NOT EXISTS "Users" (
	"User_Id"	TEXT NOT NULL,
	"Email"	TEXT NOT NULL,
	"Password"	TEXT NOT NULL,
	PRIMARY KEY("User_Id")
);
INSERT INTO "Systems" VALUES ('1','Adrian''s system');
INSERT INTO "Switchable_Lights" VALUES ('1','1','Livingroom''s light',0);
INSERT INTO "Users" VALUES ('1','user1@wp.pl','lolz');
COMMIT;
