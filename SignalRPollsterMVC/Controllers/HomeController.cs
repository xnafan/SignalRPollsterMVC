using Microsoft.AspNetCore.Mvc;
using SignalRPollsterMVC.Models;
using System.Diagnostics;

namespace SignalRPollsterMVC.Controllers;

public class HomeController : Controller
{
    IPollProvider _pollProvider;
    public HomeController(IPollProvider pollProvider)
    {
        _pollProvider = pollProvider;
    }

    public IActionResult Index(string pollId)
    {
        if (!string.IsNullOrEmpty(pollId))
        {
            ViewData["@pollId"] = pollId;
            return View(_pollProvider.GetPollInfo(pollId));
        }
        else
        {
            return View();
        }
    }

    public IActionResult Create()
    {
        var poll = new Poll("1234", "Title", "description", new List<string>() {"option 1", "option 2" });
            return View(poll);
    }
    
    [HttpPost]
    public IActionResult Create(Poll newPoll)
    {
        var id = _pollProvider.AddPoll(newPoll);
        return Redirect("/poll/" + id);
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}