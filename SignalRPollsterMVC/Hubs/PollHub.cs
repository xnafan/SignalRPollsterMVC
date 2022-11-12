using Microsoft.AspNetCore.SignalR;
namespace SignalRPollsterSiteMVC.Hubs;
public class PollHub : Hub
{
    private IPollProvider _pollProvider;
    public PollHub(IPollProvider pollProvider) => _pollProvider = pollProvider;
    public async Task GetPollInfo(string pollId) => await Clients.Caller.SendAsync("receivePollInfo", _pollProvider.GetPollInfo(pollId));
    public async Task AddPoll(Poll poll) => await Clients.Caller.SendAsync("pollCreated", _pollProvider.AddPoll(poll));
    public async Task Vote(string pollId, int vote)
    {
        await Clients.OthersInGroup("1").SendAsync("goToPoll", "2");
        await Clients.Group(pollId).SendAsync("voteUpdated", _pollProvider.Vote(pollId, vote));
    }
    public async Task JoinPoll(string pollId) => await Groups.AddToGroupAsync(Context.ConnectionId, pollId);

    public async Task GoToPoll(string currentPollId, string nextPollId) => await Clients.OthersInGroup(currentPollId).SendAsync("goToPoll", nextPollId);
}