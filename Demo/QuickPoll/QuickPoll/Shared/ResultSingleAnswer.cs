namespace QuickPoll.Shared;

public class ResultSingleAnswer
{
    public Answer Answer { get; set; } 
    public int VotesCount { get; set; }
    public decimal VotesPercentage { get; set; }
}