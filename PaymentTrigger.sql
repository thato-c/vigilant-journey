GO
CREATE TRIGGER trg_Payment
ON PAYMENT
AFTER INSERT
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @Inserted TABLE(
		InvoiceId UNIQUEIDENTIFIER,
		AmountPaid DECIMAL(15, 2),
		AmountDue DECIMAL(15, 2)
	)

	INSERT INTO @Inserted(InvoiceId, AmountPaid)
	SELECT InvoiceId, AmountPaid FROM inserted;

	UPDATE i
	SET i.AmountDue = inv.AmountDue - i.AmountPaid
	FROM @Inserted i
	JOIN Invoice inv ON inv.InvoiceId = i.InvoiceId

	UPDATE inv
	SET inv.AmountDue = i.AmountDue, 
		inv.Status = CASE WHEN i.AmountDue <= 0 THEN 'Paid' ELSE 'Partially Paid' END
	FROM Invoice inv
	JOIN @Inserted i ON i.InvoiceId = inv.InvoiceId
	WHERE i.AmountPaid > 0 AND (inv.AmountDue <> i.AmountDue OR inv.Status <> 'Paid');
END
GO