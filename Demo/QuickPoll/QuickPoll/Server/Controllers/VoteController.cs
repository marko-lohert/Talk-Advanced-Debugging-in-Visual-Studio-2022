using Microsoft.AspNetCore.Mvc;
using QuickPoll.Server.DataAccess;
using QuickPoll.Shared;

namespace QuickPoll.Server.Controllers;

[Route("[controller]")]
[ApiController]
public class VoteController : ControllerBase
{
    [HttpPost]
    public void SaveVote(Vote vote)
    {
        VoteDao voteDao = new();
        voteDao.SaveVote(vote);
    }
}
