USE AutomotiveRepairSystem;
GO

CREATE TRIGGER trg_BeforeServiceBooking
ON ScheduledService
INSTEAD OF INSERT
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @VehicleId UNIQUEIDENTIFIER, 
			@ServiceId UNIQUEIDENTIFIER, 
			@AppointmentDate DATETIME, 
			@BatchId UNIQUEIDENTIFIER

	SELECT @VehicleId = VehicleId, 
			@ServiceId = ServiceId, 
			@AppointmentDate = AppointmentDate
	From inserted;

	-- Check for an open batch for the given vehicle --
	SELECT TOP 1 @BatchId = ss.BatchId
	FROM ScheduledService ss
	WHERE ss.VehicleId = @VehicleId AND ss.Status = 'Scheduled';

	IF @BatchId IS NOT NULL
	BEGIN
		-- Found scheduled service, update existing batch --
		UPDATE ServiceBatch
		SET To_be_completed = To_be_completed + 1
		WHERE BatchId = @BatchId;
	END
	ELSE
	BEGIN
		-- No scheduled services found, create new batch --
		SET @BatchId = NEWID();

		INSERT INTO ServiceBatch (BatchId, Complete, To_be_completed, Cancelled, Status)
		VALUES (@BatchId, 0, 1, 0, 'Scheduled');
	END

	-- Insert the new Scheduled Service --
	INSERT INTO ScheduledService(ScheduledServiceId, ServiceId, VehicleId, BatchId, Status, AppointmentDate, ScheduledDate)
	VALUES (NEWID(), @ServiceId, @VehicleId, @BatchId, 'Scheduled', @AppointmentDate, GETDATE());
END
GO

GO
CREATE TRIGGER trg_ServiceTracking
ON ScheduledService
AFTER UPDATE
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @Status VARCHAR(25), @BatchId UNIQUEIDENTIFIER;

	SELECT @STATUS = Status, @BatchId = BatchId
	From inserted;

	DECLARE @Inserted TABLE(
		BatchId UNIQUEIDENTIFIER,
		Status VARCHAR(25),
	);

	-- If the Status == Complete, increment the value of complete in the ServiceBatch --
	IF @Status = 'Complete'
	BEGIN
		UPDATE ScheduledService
		SET Complete = 1
		WHERE BatchId = @BatchId AND Status = 'Complete'

		UPDATE ServiceBatch
		SET Complete = Complete + 1
		WHERE BatchId = @BatchId
	END
	
	-- If the Status == Cancelled increment the value of cancelled and decrement the value of to_be_completed in the ServiceBatch
	IF @Status = 'Cancelled'
	BEGIN
		Update sb
		SET sb.To_be_completed = sb.To_be_completed - 1, sb.Cancelled = sb.Cancelled + 1
		FROM ServiceBatch sb
		WHERE sb.BatchId = @BatchId
	END
END
GO

CREATE TRIGGER trg_InvoiceGenerator
ON ServiceBatch
AFTER UPDATE
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @Complete INT, @To_be_completed INT, @BatchId UNIQUEIDENTIFIER, @TotalServicePrice DECIMAL(15,2);

	-- Get the new values from the inserted pseudo-table (updated rows)
	SELECT @Complete = Complete, @To_be_completed = To_be_completed, @BatchId = BatchId
	FROM inserted;

	-- Check if the Complete and To_be_completed are equal
	IF @Complete = @To_be_completed
	BEGIN

		UPDATE sb
		SET Status = 'Complete'
		FROM ServiceBatch sb
		WHERE sb.BatchId = @BatchId

		-- Retrieve and sum the prices from the Service table for the matching ServiceId's --
		SELECT @TotalServicePrice = SUM(s.[Price (excl. VAT)])
		FROM ScheduledService ss
		JOIN Service s ON ss.ServiceId = s.ServiceId
		WHERE ss.BatchId = @BatchId AND ss.Status = 'Complete'

		-- Insert into Invoice if there was a valid TotalServicePrice calculated
		IF @TotalServicePrice IS NOT NULL
		BEGIN
			-- Insert into Invoice Table --
			INSERT INTO Invoice(InvoiceId, CreationDate, TotalAmount, AmountDue, Status, BatchId)
			VALUES (NEWID(), GETDATE(), @TotalServicePrice, @TotalServicePrice, 'Not Sent', @BatchId)

			-- Update the related Scheduled Services records to 'Invoiced'
			Update ss
			SET ss.Status = 'Invoiced'
			FROM ScheduledService ss
			WHERE ss.BatchId = @BatchId AND ss.Status = 'Complete'
		END
	END
END;
GO

GO
CREATE TRIGGER trg_Payment
ON PAYMENT
AFTER INSERT
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @InvoiceId UNIQUEIDENTIFIER, @AmountPaid DECIMAL(15, 2), @AmountDue DECIMAL(15, 2);

	SELECT @InvoiceId = InvoiceId, @AmountPaid = AmountPaid
	FROM inserted

	Update Invoice
	SET AmountDue = AmountDue - @AmountPaid
	WHERE InvoiceId = @InvoiceId

	SELECT @AmountDue = AmountDue
	FROM Invoice
	WHERE InvoiceId = @InvoiceId

	IF @AmountDue = 0
	BEGIN
		Update Invoice
		SET Status = 'Paid'
		WHERE InvoiceId = @InvoiceId
	END

	ELSE IF @AmountDue > 0
	BEGIN
		Update Invoice
		SET Status = 'Partially Paid'
		WHERE InvoiceId = @InvoiceId
	END
END
GO


-- Case: Scheduled Service Exists (if ScheduledService == Scheduled WHERE VehicleId == inserted.VehicleId)--
-- Result: The inserted scheduled service should have the BatchId from the Service and to_be_completed in the Batch incremented
INSERT INTO ScheduledService(AppointmentDate, VehicleId, ServiceId) VALUES
('2025-04-22', '065DCCF4-6D1D-4575-85D5-0EBAD5C26628', '608F4305-CE0A-4A41-A077-42F313E924B8');

-- Case: Scheduled Service does not Exist (if !ScheduledService == Scheduled WHERE VehicleId == inserted.VehicleId)--
-- Result: A new batch should be created and the scheduled service should have that for a BatchId
INSERT INTO ScheduledService(AppointmentDate, VehicleId, ServiceId) VALUES
('2025-04-20', 'EE776D16-7D92-46E9-BC16-33C868CC8CB7', '4C8EF1AC-9D8D-4C84-BAC6-651C2A4C627B');

-- Case: Scheduled Service does not Exist (if !ScheduledService == Scheduled WHERE VehicleId == inserted.VehicleId)--
-- Result: A new batch should be created and the scheduled service should have that for a BatchId
INSERT INTO ScheduledService(AppointmentDate, VehicleId, ServiceId) VALUES
('2025-04-25', 'EE776D16-7D92-46E9-BC16-33C868CC8CB7', '4C8EF1AC-9D8D-4C84-BAC6-651C2A4C627B');

-- Case: Scheduled Service Complete --
-- Result: Increment Complete column in related Batch entry --
UPDATE ScheduledService
SET Status = 'Complete'
WHERE ScheduledServiceId = 'B6533C5A-2B8F-46F9-BC9E-AA7BA0700E6E'

-- Case: Scheduled Service Cancelled --
-- Result: Decrement To_be_completed column and Increment Cancelled column --
UPDATE ScheduledService
SET STATUS = 'Cancelled'
WHERE ScheduledServiceId = '68E4E2B0-5695-4665-B33D-E27629486DCE'

-- Case: Scheduled Service Complete --
-- Result: Increment Complete column in related Batch entry --
UPDATE ScheduledService
SET Status = 'Complete'
WHERE ScheduledServiceId = 'EC5A4711-70DD-4817-913E-B0AD059AC30B'

-- Case: Payment Partially Paid --
-- Result: Invoice AmountDue = old AmountDue - AmountPaid and Status = "Partially Paid" --
INSERT INTO Payment(AmountPaid, PaymentMethod, InvoiceId) VALUES
(6.00, 'Cash', '0273865C-0438-4C1E-B199-E3FD705FAC10')

-- Case: Payment Paid --
-- Result: Invoice AmountDue = 0 and Status = "Paid" --
INSERT INTO Payment(AmountPaid, PaymentMethod, InvoiceId) VALUES
(6.50, 'Cash', '0273865C-0438-4C1E-B199-E3FD705FAC10')

SELECT * FROM Service
SELECT * FROM Make
SELECT * FROM Model
SELECT * FROM Fuel
SELECT * FROM Customer
SELECT * FROM Vehicle
SELECT * FROM ServiceBatch
SELECT * FROM ScheduledService
SELECT * FROM Invoice
SELECT * FROM Payment