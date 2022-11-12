using System.Linq;
namespace SignalRPollsterSiteMVC.DataAccessLayer;

public class InMemoryPollProvider : IPollProvider
{
    private Dictionary<string, Poll> _polls = new()
    {
        { "1234", new Poll(id : "1234",
            title : "What's your favorite fruit?",
            description : "Fruit poll",
            choices : new List<string> { "Apples", "Bananas", "Cherries","Water melons"})},
        {
            "4321",
            new Poll(id: "4321",
            title: "What is an algorithm?",
            description: "Algorithms",
            choices: new List<string> { "A greek alcoholic beverage", "A physical component in a computer", "A process or set of rules to be followed in calculations or other problem-solving ", "To evade the legal process of a court by hiding within or secretly leaving its jurisdiction" })
        },
        {
            "1",
            new Poll(id: "1",
            title: "What is high cohesion?",
            description: "Software Architecture",
            choices: new List<string> { "When strings are combined", "When a class only has properties of the same type", "Getters and Setters beginning with the same letter", "When functionality and data that are used together are in the same class and/or namespace" })
        },
        {
            "2",
            new Poll(id: "2",
            title: "What is a Web API controller in ASP.NET?",
            description: ".NET",
            choices: new List<string> { "Using a silicone based lubricant", "By inheriting the Object class", "By defining all dependencies using interfaces", "By moving instantiation into factory classes" })
        },
        {
            "3",
            new Poll(id: "3",
            title: "How do you achieve low coupling?",
            description: "Software Architecture",
            choices: new List<string> { "Using a silicone based lubricant", "By inheriting the Object class", "By defining all dependencies using interfaces", "By moving instantiation into factory classes" })
        }
    };

    public string AddPoll(Poll poll)
    {
        string pollId = ShortUIDTool.CreateShortId();
        _polls[pollId] = poll;
        return pollId;
    }

    public Poll GetPollInfo(string pollId) => _polls[pollId];
    public IEnumerable<int> Vote(string pollId, int choiceNumber) => GetPollInfo(pollId).Results;

    int[] IPollProvider.Vote(string pollId, int vote)
    {
        _polls[pollId].Vote(vote);
        return _polls[pollId].Results.ToArray();
    }
}