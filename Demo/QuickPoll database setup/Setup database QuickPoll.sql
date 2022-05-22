------------------------------
-- Setup Database QuickPoll --
--                          --
-- Run on (tested on):      -- 
-- Microsoft SQL Server     --
------------------------------

CREATE DATABASE QuickPoll;
GO

USE QuickPoll;
GO

CREATE TABLE Poll
(
    Id BIGINT NOT NULL IDENTITY PRIMARY KEY,
    PollName NVARCHAR(1024) NOT NULL,
    Question NVARCHAR(2048) NOT NULL
);

CREATE TABLE Answer
(
    Id BIGINT NOT NULL IDENTITY PRIMARY KEY,
    PollId BIGINT NOT NULL,
    AnswerText NVARCHAR(2048) NOT NULL
);

CREATE TABLE Vote
(
    Id BIGINT NOT NULL IDENTITY PRIMARY KEY,
    PollId BIGINT NOT NULL,
    AnswerId BIGINT NOT NULL
);

CREATE UNIQUE INDEX Poll_PollName
ON Poll (PollName);

CREATE INDEX Answer_PollId
ON Answer (PollId);

CREATE INDEX Vote_PollId
ON Vote (PollId);

CREATE INDEX Vote_AnswerId
ON Vote (AnswerId);