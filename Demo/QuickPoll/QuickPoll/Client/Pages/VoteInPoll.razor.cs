using Microsoft.AspNetCore.Components;
using QuickPoll.Shared;
using System.Net.Http.Json;

namespace QuickPoll.Client.Pages;

public partial class VoteInPoll
{
    [Parameter] public EventCallback<string> OnVoted { get; set; }

    public Poll poll = new();

    public int SelectedAnswer { get; set; } = int.MinValue;

    private bool Visible { get; set; } = false;

    public void Show()
    {
        Visible = true;
        StateHasChanged();
    }

    public void Hide()
    {
        Visible = false;
        StateHasChanged();
    }

    public async Task LoadPoll(string pollName)
    {
        poll = await Http.GetFromJsonAsync<Poll>($"Poll?pollName={pollName}");
        StateHasChanged();
    }

    private async Task HandleValidSubmit()
    {
        await SaveVote();

        if (OnVoted.HasDelegate)
            await OnVoted.InvokeAsync(poll.PollName);
    }

    private async Task SaveVote()
    {
        Answer selectedAnswer = poll?.PossibleAnswers?.Find(x => x.AnswerId == SelectedAnswer);
        Vote SelectVote = new(poll, selectedAnswer);

        await Http.PostAsJsonAsync<Vote>($"Vote", SelectVote);

        Console.WriteLine("The vote was recorded");
    }
}
