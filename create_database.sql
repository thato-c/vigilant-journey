USE master;
GO

CREATE DATABASE AutomotiveRepairSystem
CONTAINMENT = NONE
ON
(
	NAME = 'AutomotiveRepairData',
	FILENAME = 'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\automotiveRepairData.mdf',
	SIZE = 100MB,
	MAXSIZE = UNLIMITED,
	FILEGROWTH = 10MB
)
LOG ON
(
	NAME = 'AutomotiveRepairData_log',
	FILENAME = 'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\automotiveRepairData.ldf',
	SIZE = 50MB,
	MAXSIZE = 100MB,
	FILEGROWTH = 5MB
)
COLLATE Latin1_General_CI_AS;
GO