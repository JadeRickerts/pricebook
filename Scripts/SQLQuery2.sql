SELECT [dbo].[Stores].[StoreID] AS [Store ID], 
CONCAT([dbo].[Stores].[StoreName], ', ', [dbo].[Stores].[StoreLocation]) AS [Store Name] 
FROM [dbo].[Stores]