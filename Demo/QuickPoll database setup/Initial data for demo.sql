------------------------------------------------------------------------------
-- Initial data for QuickPoll      demo                                     --
--                                                                          --
-- Run this SQL command before the demo.                                    --
-- These SQL commands will clean up data left from the previous time a demo --
-- was run, and prepare data that a presenter expects when a demo is run.   --
--                                                                          --
-- Run on (tested on):                                                      --     
-- Microsoft SQL Server 2019                                                --
------------------------------------------------------------------------------


USE QuickPoll;
GO



---------------------------------
-- Clean up - delete old data. --
---------------------------------

DELETE FROM Vote;
DELETE FROM Answer;
DELETE FROM Poll;


-----------------------------
-- Insert data for a demo. --
-----------------------------


-- TABLE Poll

DECLARE @PollId INT;

INSERT INTO Poll(PollName, Question)
VALUES('Life', 'What is the answer to the ultimate question of life, the universe, and everything?');

SET @PollId = SCOPE_IDENTITY() 

SELECT CONCAT('Inserted a new poll. PollId = ', @PollId);


-- TABLE Answer

DECLARE @Answer1Id INT;

INSERT INTO Answer(PollId, AnswerText)
VALUES(@PollId, 'Coffee');

SET @Answer1Id = SCOPE_IDENTITY();

DECLARE @Answer2Id INT;

INSERT INTO Answer(PollId, AnswerText)
VALUES(@PollId, '42');

SET @Answer2Id = SCOPE_IDENTITY();

DECLARE @Answer3Id INT;

INSERT INTO Answer(PollId, AnswerText)
VALUES(@PollId, 'Chocolate');

SET @Answer3Id = SCOPE_IDENTITY();


-- TABLE Vote

INSERT INTO Vote(PollId, AnswerId)
VALUES
    (@PollId, @Answer1Id),
    (@PollId, @Answer1Id),
    (@PollId, @Answer1Id),
    (@PollId, @Answer1Id),
    (@PollId, @Answer1Id),
    (@PollId, @Answer2Id),
    (@PollId, @Answer2Id),
    (@PollId, @Answer2Id),
    (@PollId, @Answer2Id),
    (@PollId, @Answer2Id),
    (@PollId, @Answer2Id),
    (@PollId, @Answer2Id),
    (@PollId, @Answer2Id),
    (@PollId, @Answer2Id),
    (@PollId, @Answer2Id),
    (@PollId, @Answer3Id),
    (@PollId, @Answer3Id),
    (@PollId, @Answer3Id),
    (@PollId, @Answer3Id),
    (@PollId, @Answer3Id);