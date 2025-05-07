CREATE TRIGGER trg_InvoiceGenerator
ON ServiceBatch
AFTER UPDATE
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @TotalServicePrice DECIMAL(15,2);

	DECLARE @Inserted TABLE(
		BatchId UNIQUEIDENTIFIER,
		Complete INT,
		To_be_completed INT,
		TotalServicePrice DECIMAL(15, 2)
	);

	INSERT INTO @Inserted(BatchId, Complete, To_be_completed)
	SELECT BatchId, Complete, To_be_completed FROM inserted;

	-- Update Batch Status to Complete
	UPDATE sb
	SET Status = 'Complete'
	FROM ServiceBatch sb
	JOIN @Inserted i ON sb.BatchId = i.BatchId
	WHERE i.Complete = i.To_be_completed

	-- Retrieve and sum the prices from the Service table for the matching ServiceId's
	UPDATE i
	SET i.TotalServicePrice = total.TotalServicePrice
	FROM @Inserted i
	JOIN (
		SELECT ss.BatchId, SUM(s.[Price (excl. VAT)]) AS TotalServicePrice
		FROM ScheduledService ss
		JOIN Service s ON ss. ServiceId = s.ServiceId
		JOIN @inserted i ON ss.BatchId = i.BatchId
		WHERE ss.Status = 'Complete'
		GROUP BY ss.BatchId
	) total ON i.BatchId = total.BatchId

	-- Insert into Invoice if there was a valid TotalServicePrice calculated
	INSERT INTO Invoice(InvoiceId, CreationDate, TotalAmount, AmountDue, Status, BatchId)
	SELECT
		NEWID(), 
		GETDATE(), 
		i.TotalServicePrice, 
		i.TotalServicePrice, 
		'Not Sent',
		i.BatchId
	FROM @Inserted i
	WHERE 
		i.Complete = i.To_be_completed
		AND i.TotalServicePrice IS NOT NULL
		AND NOT EXISTS (
			SELECT 1
			FROM Invoice inv
			WHERE inv.BatchId = i.BatchId
		);

	-- Update the related Scheduled Services records to 'Invoiced'
	UPDATE ss
	SET ss.Status = 'Invoiced'
	FROM ScheduledService ss
	JOIN @Inserted i ON i.BatchId = ss.BatchId
	WHERE i.Complete = i.To_be_completed
END;
GO