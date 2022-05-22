namespace QuickPoll.Client.Pages;

public partial class Index
{
    VoteInPoll VoteInPollComponent;
    PollResult PollResultComponent;

    public async Task PollSelectedHandler(string pollName)
    {
        await VoteInPollComponent!.LoadPoll(pollName);
        VoteInPollComponent?.Show();
    }

    public async Task VotedHandler(string pollName)
    {
        await PollResultComponent!.LoadPollResult(pollName);
        PollResultComponent?.Show();
    }
}
