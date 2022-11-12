namespace SignalRPollsterSiteMVC.DataAccessLayer
{
    public interface IPollProvider
    {
        Poll GetPollInfo(string pollId);
        int[] Vote(string pollId, int vote);
        string AddPoll(Poll poll);
    }
}
