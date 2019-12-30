--ALTER TABLE [dbo].[Product] DROP CONSTRAINT [FK_Product_Product_TrajectID];
--ALTER TABLE [dbo].[WinkelwagenItem] DROP CONSTRAINT [FK_WinkelwagenItem_Product_ProductID];
ALTER TABLE [dbo].[BestellingItem] DROP CONSTRAINT [FK_BestellingItem_Product_ProductID];
DROP TABLE [dbo].[Product];