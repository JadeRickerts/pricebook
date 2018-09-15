UPDATE [dbo].[Transactions]
SET [Date] = '2018-09-15', [StoreID] = 101, [InvoiceTotalAmount] = 6.99
WHERE [InvoiceNumber] = 201809151002270

SELECT * FROM Transactions