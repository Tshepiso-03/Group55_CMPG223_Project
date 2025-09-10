CREATE TABLE [dbo].[Author] (
    [Id]          INT           IDENTITY (1, 1) NOT NULL,
    [FirstName]   VARCHAR (30)  NULL,
    [LastName]    VARCHAR (30)  NULL,
    [Biography]   VARCHAR (100) NULL,
    [Nationality] VARCHAR (50)  NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);
