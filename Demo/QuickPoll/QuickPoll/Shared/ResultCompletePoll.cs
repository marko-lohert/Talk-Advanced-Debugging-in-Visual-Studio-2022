namespace QuickPoll.Shared;

public class ResultCompletePoll
{
    public string PollName { get; set; }
    public List<ResultSingleAnswer> AllResults { get; set; }

    public void CalculatePercentage()
    {
        int totalVotes = AllResults.Sum(x => x.VotesCount); 

        foreach (ResultSingleAnswer resultSingleAnswer in AllResults)
        {
            resultSingleAnswer.VotesPercentage = (decimal)resultSingleAnswer.VotesCount / totalVotes;
        }
    }
}
