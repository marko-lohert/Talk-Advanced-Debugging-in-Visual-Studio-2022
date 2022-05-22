using Microsoft.AspNetCore.Components;
using System.ComponentModel.DataAnnotations;

namespace QuickPoll.Client.Pages;

public partial class PollSelection
{
    [Parameter] public EventCallback<string> OnPollSelected { get; set; }

    public SelectPollModel SelectedPollModel { get; set; } = new();

    public async Task HandleValidSubmit()
    {
        if (OnPollSelected.HasDelegate)
            await OnPollSelected.InvokeAsync(SelectedPollModel.PollName);
    }

    public class SelectPollModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter the name of the poll")]
        public string PollName { get; set; }
    }
}
