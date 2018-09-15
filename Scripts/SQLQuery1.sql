select * from Transactions

SELECT [dbo].[Transactions].[Date] AS [Date], 
CONCAT([dbo].[Stores].[StoreName], ', ', [Stores].[StoreLocation]) AS [Store Name],
[dbo].[Transactions].[InvoiceNumber] AS [Invoice Number],
CONCAT([dbo].[Variants].[BrandName], ' ', 
[dbo].[Products].[Description], ' ', 
[dbo].[Variants].[Description], ' ',
[dbo].[Variants].[PackSize], ' ',
[dbo].[Products].[UoM]) AS [Product Description],
[dbo].[Transactions].[Quantity] AS [Quantity],
[dbo].[Transactions].[Weight] AS [Weight],
[dbo].[Transactions].[TotalPrice] AS [Total Product Price],
[dbo].[Transactions].[Sale] AS [Sale],
[dbo].[Transactions].[InvoiceTotalAmount] AS [Total Invoice Amount],
[dbo].[Transactions].[Saved] AS [Saved]
FROM [dbo].[Transactions]
INNER JOIN [dbo].[Stores] ON [dbo].[Transactions].[StoreID] = [dbo].[Stores].[StoreID]
INNER JOIN [dbo].[Variants] ON [dbo].[Transactions].[VariantID] = [dbo].[Variants].[VariantID]
INNER JOIN [dbo].[Products] ON [dbo].[Variants].[ProductCode] = [dbo].[Products].[ProductCode]
ORDER BY [Date] ASC

SELECT [dbo].[Stores].[StoreID] AS [Store ID], 
CONCAT([dbo].[Stores].[StoreName], ', ', [dbo].[Stores].[StoreLocation]) AS [Store Name] 
FROM [dbo].[Stores]

