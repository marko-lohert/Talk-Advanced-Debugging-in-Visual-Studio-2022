using Microsoft.Data.SqlClient;
using QuickPoll.Shared;

namespace QuickPoll.Server.DataAccess;

public class VoteDao
{
    public void SaveVote(Vote vote)
    {
        if (vote is null)
            return;

        string cmdText = $@"
                INSERT INTO Vote(PollId, AnswerId)
                VALUES({vote.Poll.PollId}, {vote.SelectedAnswer.AnswerId});
                ";

        using SqlConnection connection = new(ConnectionString);
        connection.Open();

        SqlCommand sqlCommand = new(cmdText.ToString(), connection);

        int rowsInserted = sqlCommand.ExecuteNonQuery();

        connection.Close();
    }

    // For Microsoft.Data.SqlClient version >= 4.0 please read
    // https://weblog.west-wind.com/posts/2021/Dec/07/Connection-Failures-with-MicrosoftDataSqlClient-4-and-later
    private string ConnectionString = @"Data Source = .\SQLEXPRESS;Initial Catalog = QuickPoll;Integrated Security=true;Encrypt=False";

}
