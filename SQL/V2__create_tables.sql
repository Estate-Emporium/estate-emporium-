USE EstateEmporium;
GO

CREATE TABLE [PropertySales] (
  [SaleID] bigint IDENTITY(1,1) PRIMARY KEY,
  [BuyerID] bigint NOT NULL,
  [SellerID] bigint NOT NULL,
  [PropertyID] bigint NOT NULL,
  [SalePrice]  bigint NOT NULL,
  [HomeLoanID] bigint NOT NULL,
  [PurchaseDate] datetime NOT NULL,
  [StatusID] integer
)
GO

CREATE TABLE [Status] (
  [StatusID] integer PRIMARY KEY,
  [Status] nvarchar(255)
)
GO

EXEC sp_addextendedproperty
@name = N'Column_Description',
@value = 'Pending Failed Sucessful Requested',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table',  @level1name = 'Status',
@level2type = N'Column', @level2name = 'Status';
GO

ALTER TABLE [PropertySales] ADD FOREIGN KEY ([StatusID]) REFERENCES [Status] ([StatusID])
GO
