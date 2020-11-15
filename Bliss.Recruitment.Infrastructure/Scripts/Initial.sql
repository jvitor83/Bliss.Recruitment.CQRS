CREATE DATABASE Recruitment;
GO

USE Recruitment;
GO

CREATE SCHEMA app AUTHORIZATION dbo
GO

CREATE TABLE app.OutboxMessages
(
	[Id] UNIQUEIDENTIFIER NOT NULL,
	[OccurredOn] DATETIME2 NOT NULL,
	[Type] VARCHAR(255) NOT NULL,
	[Data] VARCHAR(MAX) NOT NULL,
	[ProcessedDate] DATETIME2 NULL,
	CONSTRAINT [PK_app_OutboxMessages_Id] PRIMARY KEY ([Id] ASC)
)
GO

CREATE TABLE app.InternalCommands
(
	[Id] UNIQUEIDENTIFIER NOT NULL,
	[EnqueueDate] DATETIME2 NOT NULL,
	[Type] VARCHAR(255) NOT NULL,
	[Data] VARCHAR(MAX) NOT NULL,
	[ProcessedDate] DATETIME2 NULL,
	CONSTRAINT [PK_app_InternalCommands_Id] PRIMARY KEY ([Id] ASC)
)
GO


CREATE SCHEMA questions AUTHORIZATION dbo
GO

CREATE TABLE [questions].[Questions] (
    [Id] UNIQUEIDENTIFIER NOT NULL,
    [Question] nvarchar(max) NULL,
    [ImageUrl] nvarchar(max) NULL,
    [ThumbUrl] nvarchar(max) NULL,
    [PublishedAt] datetime2 NOT NULL,
    CONSTRAINT [PK_Questions] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [questions].[Choices] (
    [Id] UNIQUEIDENTIFIER NOT NULL,
    [Choice] nvarchar(max) NULL,
    [Votes] int NOT NULL,
    [QuestionId] UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_Choices] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Choices_Questions_QuestionId] FOREIGN KEY ([QuestionId]) REFERENCES [questions].[Questions] ([Id]) ON DELETE NO ACTION
);


INSERT INTO [questions].[Questions] ([Id], [Question], [ImageUrl], [ThumbUrl], [PublishedAt]) VALUES 
    ('7bceb39b-4174-4dfa-8b72-0cd9f2943193', 'Favourite programming language?', 'https://dummyimage.com/600x400/000/fff.png&text=question+1+image+(600x400)', 'https://dummyimage.com/120x120/000/fff.png&text=question+1+image+(120x120)', '2015-08-05T08:40:51.620Z')
;
GO
INSERT INTO [questions].[Choices] ([Id], [Choice], [Votes], [QuestionId]) VALUES 
    ('126cefd9-6595-4846-9eb1-92ac47a29b8d', 'Swift', 2048, '7bceb39b-4174-4dfa-8b72-0cd9f2943193'),
    ('6e56ff8c-a199-408d-9651-8714e8cd0cd1', 'Python', 1024, '7bceb39b-4174-4dfa-8b72-0cd9f2943193'),
    ('691d10e7-c941-4269-ac57-8687567ffd9e', 'Objective-C', 512, '7bceb39b-4174-4dfa-8b72-0cd9f2943193'),
    ('b4a5995e-eec6-4ec0-93af-910191b451d2', 'Ruby', 256, '7bceb39b-4174-4dfa-8b72-0cd9f2943193')
;
GO
