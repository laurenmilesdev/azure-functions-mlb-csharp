namespace AzureFunctionsMlbCSharp
{
    public class Team
    {
        public string Name { get; }
        public string Abbreviation { get; }

        public Team(string name, string abbreviation)
        {
            Name = name;
            Abbreviation = abbreviation;
        }
    }
}