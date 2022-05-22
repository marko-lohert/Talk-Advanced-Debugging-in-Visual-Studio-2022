using System.ComponentModel.DataAnnotations;

namespace QuickPoll.Shared;

public class Answer
{
    public long AnswerId { get; set; }

    public long PollId { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter the name of the poll")]
    public string AnswerText { get; set; }
}