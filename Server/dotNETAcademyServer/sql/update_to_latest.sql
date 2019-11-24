IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191124175654_InitialCreate')
BEGIN
    CREATE TABLE [Klanten] (
        [Id] int NOT NULL IDENTITY,
        [AzureId] nvarchar(max) NULL,
        CONSTRAINT [PK_Klanten] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191124175654_InitialCreate')
BEGIN
    CREATE TABLE [Product] (
        [ID] int NOT NULL IDENTITY,
        [Prijs] float NOT NULL,
        [Categorie] nvarchar(max) NULL,
        [FotoURLCard] nvarchar(max) NULL,
        [Type] nvarchar(max) NULL,
        [Beschrijving] nvarchar(120) NULL,
        [LangeBeschrijving] nvarchar(max) NULL,
        [Titel] nvarchar(450) NOT NULL,
        [Discriminator] nvarchar(max) NOT NULL,
        [TrajectID] int NULL,
        CONSTRAINT [PK_Product] PRIMARY KEY ([ID]),
        CONSTRAINT [FK_Product_Product_TrajectID] FOREIGN KEY ([TrajectID]) REFERENCES [Product] ([ID]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191124175654_InitialCreate')
BEGIN
    CREATE TABLE [Bestellingen] (
        [Id] int NOT NULL IDENTITY,
        [Datum] datetime2 NOT NULL,
        [TotaalPrijs] float NOT NULL,
        [KlantId] int NULL,
        CONSTRAINT [PK_Bestellingen] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Bestellingen_Klanten_KlantId] FOREIGN KEY ([KlantId]) REFERENCES [Klanten] ([Id]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191124175654_InitialCreate')
BEGIN
    CREATE TABLE [Winkelwagens] (
        [Id] int NOT NULL IDENTITY,
        [Datum] datetime2 NOT NULL,
        [KlantId] int NULL,
        [TotaalPrijs] float NOT NULL,
        CONSTRAINT [PK_Winkelwagens] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Winkelwagens_Klanten_KlantId] FOREIGN KEY ([KlantId]) REFERENCES [Klanten] ([Id]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191124175654_InitialCreate')
BEGIN
    CREATE TABLE [BestellingItem] (
        [Id] int NOT NULL IDENTITY,
        [ProductID] int NULL,
        [Aantal] int NOT NULL,
        [BestellingId] int NULL,
        CONSTRAINT [PK_BestellingItem] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_BestellingItem_Bestellingen_BestellingId] FOREIGN KEY ([BestellingId]) REFERENCES [Bestellingen] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_BestellingItem_Product_ProductID] FOREIGN KEY ([ProductID]) REFERENCES [Product] ([ID]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191124175654_InitialCreate')
BEGIN
    CREATE TABLE [WinkelwagenItem] (
        [Id] int NOT NULL IDENTITY,
        [ProductID] int NULL,
        [Aantal] int NOT NULL,
        [WinkelwagenId] int NULL,
        CONSTRAINT [PK_WinkelwagenItem] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_WinkelwagenItem_Product_ProductID] FOREIGN KEY ([ProductID]) REFERENCES [Product] ([ID]) ON DELETE NO ACTION,
        CONSTRAINT [FK_WinkelwagenItem_Winkelwagens_WinkelwagenId] FOREIGN KEY ([WinkelwagenId]) REFERENCES [Winkelwagens] ([Id]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191124175654_InitialCreate')
BEGIN
    CREATE INDEX [IX_Bestellingen_KlantId] ON [Bestellingen] ([KlantId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191124175654_InitialCreate')
BEGIN
    CREATE INDEX [IX_BestellingItem_BestellingId] ON [BestellingItem] ([BestellingId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191124175654_InitialCreate')
BEGIN
    CREATE INDEX [IX_BestellingItem_ProductID] ON [BestellingItem] ([ProductID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191124175654_InitialCreate')
BEGIN
    CREATE UNIQUE INDEX [IX_Product_Titel] ON [Product] ([Titel]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191124175654_InitialCreate')
BEGIN
    CREATE INDEX [IX_Product_TrajectID] ON [Product] ([TrajectID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191124175654_InitialCreate')
BEGIN
    CREATE UNIQUE INDEX [IX_Product_Titel1] ON [Product] ([Titel]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191124175654_InitialCreate')
BEGIN
    CREATE INDEX [IX_WinkelwagenItem_ProductID] ON [WinkelwagenItem] ([ProductID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191124175654_InitialCreate')
BEGIN
    CREATE INDEX [IX_WinkelwagenItem_WinkelwagenId] ON [WinkelwagenItem] ([WinkelwagenId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191124175654_InitialCreate')
BEGIN
    CREATE INDEX [IX_Winkelwagens_KlantId] ON [Winkelwagens] ([KlantId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191124175654_InitialCreate')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20191124175654_InitialCreate', N'2.2.6-servicing-10079');
END;

GO

