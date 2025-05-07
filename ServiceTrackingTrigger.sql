GO
CREATE TRIGGER trg_ServiceTracking
ON ScheduledService
AFTER UPDATE
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @Inserted TABLE(
		ServiceId UNIQUEIDENTIFIER,
		BatchId UNIQUEIDENTIFIER,
		Status VARCHAR(25)
	);

	INSERT INTO @Inserted (ServiceId, BatchId, Status)
	SELECT ServiceId, BatchId, Status FROM inserted;

	-- If the Status == Complete, 
	-- Update the Complete column to 1 
	UPDATE ss
	SET ss.Complete = 1
	FROM ScheduledService ss
	JOIN @Inserted i ON ss.ServiceId = i.ServiceId
	WHERE i.Status = 'Complete'

	-- Increment the value of complete in the ServiceBatch
	UPDATE sb
	SET sb.Complete = sb.Complete + i.CompleteCount
	FROM ServiceBatch sb
	JOIN (
		SELECT BatchId, Count(*) AS CompleteCount
		FROM @Inserted
		WHERE Status = 'Complete'
		GROUP BY BatchId
	) i ON sb.BatchId = i.BatchId

	-- If the Status == Cancelled 
	-- increment the value of cancelled
	-- decrement the value of to_be_completed in the ServiceBatch
	Update sb
	SET sb.To_be_completed = sb.To_be_completed - c.CancelledCount, 
		sb.Cancelled = sb.Cancelled + c.CancelledCount
	FROM ServiceBatch sb
	JOIN (
		SELECT BatchId, COUNT(*) AS CancelledCount
		FROM @Inserted
		WHERE Status = 'Cancelled'
		GROUP BY BatchId
	) c ON sb.BatchId = c.BatchId;
END
GO