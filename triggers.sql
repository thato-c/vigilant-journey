USE AutomotiveRepairSystem
GO
-- CREATE SOFT DELETE Procedure - Update isDeleted and DeletionTimeStamp --
CREATE PROCEDURE GetOrCreateServiceBatches
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @TempInserted TABLE(
		VehicleId UNIQUEIDENTIFIER,
		ServiceId UNIQUEIDENTIFIER,
		AppointmentDate DATETIME
	);

	-- Temporary table to return VehicleId + BatchId
	CREATE TABLE #VehicleBatchMap(
		VehicleId UNIQUEIDENTIFIER PRIMARY KEY,
		BatchId UNIQUEIDENTIFIER
	);

	-- Step 1: Insert existing batches
	INSERT INTO #VehicleBatchMap(VehicleId, BatchId)
	SELECT DISTINCT i.VehicleId, ss.BatchId
	FROM @TempInserted i
	JOIN ScheduledService ss ON i.VehicleId = ss.VehicleId
	WHERE ss.Status = 'Scheduled';

	-- Step 2: Create batches for missing ones
	INSERT INTO ServiceBatch (BatchId, Complete, To_be_completed, Cancelled, Status)
	OUTPUT i.VehicleId, inserted.BatchId 
	INTO #VehicleBatchMap(VehicleId, BatchId)
	SELECT NEWID(), 0, 0, 0, 'Scheduled'
	FROM @TempInserted i
	WHERE NOT EXISTS (
		SELECT 1 FROM #VehicleBatchMap vb WHERE vb.VehicleId = i.VehicleId
	);

	-- Step 3: Update To_be_completed count
	UPDATE sb
	SET To_be_completed = To_be_completed + v.CountPerVehicle
	FROM ServiceBatch sb
	JOIN(
		SELECT vb.BatchId, COUNT(*) AS CountPerVehicle
		FROM #VehicleBatchMap vb
		JOIN @TempInserted i ON i.VehicleId = vb.VehicleId
		GROUP BY vb.BatchId
	) v ON sb.BatchId = v.BatchId

	SELECT * FROM #VehicleBatchMap;
END
GO

CREATE TRIGGER trg_BeforeServiceBooking
ON ScheduledService
INSTEAD OF INSERT
AS
BEGIN
	SET NOCOUNT ON;

	-- Step 1: Store inserted data into a temp table
	DECLARE @Inserted TABLE(
		ServiceId UNIQUEIDENTIFIER,
		VehicleId UNIQUEIDENTIFIER,
		AppointmentDate DATETIME
	);

	INSERT INTO @Inserted (ServiceId, VehicleId, AppointmentDate)
	SELECT ServiceId, VehicleId, AppointmentDate FROM inserted;

	-- Step 2: Get or create BatchIds per VehicleId
	DECLARE @VehicleBatch TABLE(
		VehicleId UNIQUEIDENTIFIER,
		BatchId UNIQUEIDENTIFIER
	);

	INSERT INTO @VehicleBatch (VehicleId, BatchId)
	EXEC GetOrCreateServiceBatches @Inserted;

	-- Step 3: Insert into ScheduledService with correct BatchIds
	INSERT INTO ScheduledService(
		ScheduledServiceId, ServiceId, VehicleId, BatchId, Status, 
		AppointmentDate, ScheduledDate
	)
	SELECT 
		NEWID(), i.ServiceId, i.VehicleId, vb.BatchId, 'Scheduled', i.AppointmentDate, GETDATE()
	FROM @Inserted i
	JOIN @VehicleBatch vb ON i.VehicleId = vb.VehicleId;
END;
GO

