using QuickPoll.Shared;
using System.Net.Http.Json;

namespace QuickPoll.Client.Pages;

public partial class PollResult
{
    public ResultCompletePoll Result { get; set; }
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

    public async Task LoadPollResult(string pollName)
    {
        Console.WriteLine("Loading poll results...");

        Result = await Http.GetFromJsonAsync<ResultCompletePoll>($"Poll/GetPollResult?pollName={pollName}");
        if (Result is not null)
        {
            Result.CalculatePercentage();
            StateHasChanged();
        }
    }
}
