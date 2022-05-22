using QuickPoll.Client.Analyzer;
using QuickPoll.Shared;
using System.Net.Http.Json;

namespace QuickPoll.Client.Pages;

public partial class AddPoll
{
    public Poll NewPoll { get; set; } = new();

    protected override void OnInitialized()
    {
        base.OnInitialized();

        NewPoll = new()
        {
            PossibleAnswers = new()
        };

        // There should be at least two answers -> there should be at least two text boxes for answers.
        NewPoll.PossibleAnswers.Add(new Answer());
        NewPoll.PossibleAnswers.Add(new Answer());
    }

    public void BtnAddAnswer()
    {
        if (NewPoll is null)
            NewPoll = new();

        if (NewPoll.PossibleAnswers is null)
            NewPoll.PossibleAnswers = new();

        NewPoll.PossibleAnswers.Add(new Answer());
    }

    public async Task HandleValidSubmit()
    {
        await Http.PostAsJsonAsync<Poll>("Poll", NewPoll);
    }

    public void AnalyzeNewPoll()
    {
        NewPollAnalyzer analyzer = new();
        NewPollAnalyzeResult = analyzer.Analyze(NewPoll);
    }

    public string NewPollAnalyzeResult { get; set; } = string.Empty;
}
