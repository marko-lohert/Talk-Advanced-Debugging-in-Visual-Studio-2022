using Microsoft.AspNetCore.Mvc;
using QuickPoll.Server.DataAccess;
using QuickPoll.Server.Utils;
using QuickPoll.Shared;

namespace QuickPoll.Server.Controllers;

[Route("[controller]")]
[ApiController]
public class PollController : ControllerBase
{
    [HttpGet]
    public Poll GetPoll(string pollName)
    {
        PollDao pollDao = new();
        return pollDao.GetPoll(pollName);
    }

    [HttpGet("GetPollResult")]
    public ResultCompletePoll GetPollResult(string pollName)
    {
        try
        {
            if (!SQLInjectionUtility.IsSaveFromSQLInjection(pollName))
                return null;

            PollDao pollDao = new();
            return pollDao.GetPollResult(pollName);
        }
        catch
        {
            return null;
        }
    }

    [HttpPost]
    public void SavePoll(Poll newPoll)
    {
        PollDao pollDao = new();
        pollDao.SavePoll(newPoll);
    }
}
