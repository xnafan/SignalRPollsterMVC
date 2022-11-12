namespace SignalRPollsterSiteMVC.DataAccessLayer.Model
{
    public class Poll
    {
        public string? Id { get; set; }
        public string Title { get; set; } = "no title";
        public string Description { get; set; } = "no description";
        public List<string> Choices { get; set; } = new ();
        public List<int> Results { get; set; }

        public Poll(string? id, string title, string description, List<string> choices)
        {
            Id = id;
            Title = title;
            Description = description;
            Choices = choices;
            Results = new List<int>(new int[Choices.Count]);
        }
        public Poll()
        {

        }

        public int Vote(int choiceNumber)
        {
            if (choiceNumber >= Choices.Count) { throw new ArgumentOutOfRangeException($"No choice number {choiceNumber}! Only {Choices.Count} choices in Poll and list is zero-indexed."); }
            Results[choiceNumber]++;
            return Results[choiceNumber];
        }
    }
}