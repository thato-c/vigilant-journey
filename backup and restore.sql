use Master

-- BACKUP --
BACKUP DATABASE AutomotiveRepairSystem
TO DISK = 'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\Repairsystem_FULL.bak'
WITH INIT, FORMAT

BACKUP DATABASE AutomotiveRepairSystem
TO DISK = 'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\Repairsystem_DIFF.bak'
WITH DIFFERENTIAL, INIT, FORMAT

BACKUP DATABASE AutomotiveRepairSystem
TO DISK = 'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\Repairsystem_LOG.bak'
WITH INIT, FORMAT

-- RESTORATION --
RESTORE DATABASE AutomotiveRepairSystem
FROM DISK = 'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\Repairsystem_FULL.bak'
WITH REPLACE, NORECOVERY, STATS = 10

RESTORE DATABASE AutomotiveRepairSystem
FROM DISK = 'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\Repairsystem_DIFF.bak'
WITH NORECOVERY , STATS = 10

RESTORE DATABASE AutomotiveRepairSystem
FROM DISK = 'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\Repairsystem_LOG.bak'
WITH RECOVERY, STATS = 10
