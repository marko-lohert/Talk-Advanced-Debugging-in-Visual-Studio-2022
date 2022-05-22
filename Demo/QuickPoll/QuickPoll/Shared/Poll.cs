using System.ComponentModel.DataAnnotations;

namespace QuickPoll.Shared;

public class Poll
{
    public long PollId { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter the name of the poll")]
    public string PollName { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter the question for this poll")]
    public string Question { get; set; }

    public List<Answer> PossibleAnswers { get; set; }
}
