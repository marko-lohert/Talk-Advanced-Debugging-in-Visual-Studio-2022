using Microsoft.Data.SqlClient;
using QuickPoll.Shared;
using System.Data;
using System.Text;

namespace QuickPoll.Server.DataAccess;

public class PollDao
{
    public Poll GetPoll(string pollName)
    {
        DataSet datasSet = GetPollFromDatabase(pollName);

        Poll poll = MapPoll(datasSet, pollName);

        return poll;
    }

    private DataSet GetPollFromDatabase(string pollName)
    {
        using SqlConnection connection = new(ConnectionString);
        System.Data.DataSet dataSet = new();

        try
        {
            pollName = PrepareSingleQuotesForSQL(pollName);

            string cmdText = $@"SELECT Poll.Id AS PollId, Question
                                FROM Poll
                                WHERE LOWER(PollName) = LOWER('{pollName}');

                                SELECT Answer.Id AS AnswerId, AnswerText
                                FROM Answer
                                INNER JOIN Poll ON Poll.Id = Answer.PollId
                                WHERE LOWER(PollName) = LOWER('{pollName}');";


            connection.Open();

            using SqlDataAdapter sqlAdapter = new(cmdText, connection);
            sqlAdapter.Fill(dataSet);
        }
        finally
        {
            connection.Close();
        }

        return dataSet;
    }

    private Poll MapPoll(DataSet dataSet, string pollName)
    {
        Poll poll = new();
        poll.PollId = (long)dataSet.Tables[0].Rows[0]["PollId"];
        poll.PollName = pollName;
        poll.Question = dataSet.Tables[0].Rows[0]["Question"].ToString();

        poll.PossibleAnswers = new();
        foreach (DataRow row in dataSet.Tables[1].Rows)
        {
            long answerId = (long)row["AnswerId"];
            string answerText = row["AnswerText"].ToString();
            Answer answer = new()
            {
                AnswerId = answerId,
                PollId = poll.PollId,
                AnswerText = answerText
            };
            poll.PossibleAnswers.Add(answer);
        }

        return poll;
    }


    public ResultCompletePoll GetPollResult(string pollName)
    {
        using SqlConnection connection = new(ConnectionString);
        ResultCompletePoll resultCompletePoll = new();

        try
        {
            pollName = PrepareSingleQuotesForSQL(pollName);

            string cmdText = $@"SELECT Answer.AnswerText, COUNT(Vote.AnswerId) AS VotesCount
                                FROM Vote
                                INNER JOIN Answer ON Answer.Id = Vote.AnswerId
                                INNER JOIN Poll ON Vote.PollId = Poll.Id
                                WHERE LOWER(Poll.PollName) = LOWER('{pollName}')
                                GROUP BY Answer.AnswerText;";


            connection.Open();

            using SqlDataAdapter sqlAdapter = new(cmdText, connection);
            System.Data.DataTable dataTable = new();
            sqlAdapter.Fill(dataTable);

            resultCompletePoll.PollName = pollName;

            resultCompletePoll.AllResults = new();
            foreach (DataRow row in dataTable.Rows)
            {
                ResultSingleAnswer resultSingleAnswer = new();

                resultSingleAnswer.Answer = new();
                resultSingleAnswer.Answer.AnswerText = row["AnswerText"].ToString();
                resultSingleAnswer.VotesCount = (int)row["VotesCount"];

                resultCompletePoll.AllResults.Add(resultSingleAnswer);
            }
        }
        finally
        {
            connection.Close();
        }

        return resultCompletePoll;
    }

    public void SavePoll(Poll newPoll)
    {
        if (newPoll is null)
            return;

        PrepareSingleQuotesForSQL(newPoll);

        StringBuilder sbCmdText = new();

        sbCmdText.AppendLine($@"
                DECLARE @PollId INT;

                INSERT INTO Poll(PollName, Question)
                VALUES('{newPoll.PollName}', '{newPoll.Question}');
            
                SET @PollId = SCOPE_IDENTITY();
                ");

        foreach (Answer answer in newPoll.PossibleAnswers)
        {
            sbCmdText.AppendLine($@"
                    INSERT INTO Answer(PollId, AnswerText)
                    VALUES(@PollId, '{answer.AnswerText}');
                    ");
        }

        using SqlConnection connection = new(ConnectionString);
        connection.Open();

        SqlCommand sqlCommand = new(sbCmdText.ToString(), connection);

        int rowsInserted = sqlCommand.ExecuteNonQuery();

        connection.Close();
    }

    private string PrepareSingleQuotesForSQL(string inputStr)
    {
        if (inputStr is null or "")
            return inputStr;

        return inputStr.Replace(@"'", @"''");
    }

    private void PrepareSingleQuotesForSQL(Poll poll)
    {
        if (poll is null)
            return;

        poll.PollName = PrepareSingleQuotesForSQL(poll.PollName);
        poll.Question = PrepareSingleQuotesForSQL(poll.Question);

        if (poll.PossibleAnswers is null)
            return;

        foreach (Answer answer in poll.PossibleAnswers)
        {
            answer.AnswerText = PrepareSingleQuotesForSQL(answer.AnswerText);
        }
    }

    // For Microsoft.Data.SqlClient version >= 4.0 please read
    // https://weblog.west-wind.com/posts/2021/Dec/07/Connection-Failures-with-MicrosoftDataSqlClient-4-and-later
    private string ConnectionString = @"Data Source = .\SQLEXPRESS;Initial Catalog = QuickPoll;Integrated Security=true;Encrypt=False";
}
