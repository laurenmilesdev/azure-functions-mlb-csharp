using System.Text.Json.Serialization;

namespace AzureFunctionsMlbCSharp
{
    public class Game
    {
        [JsonPropertyName("gameID")]
        public string? Id { get; set; }

        [JsonPropertyName("away")]
        public string? AwayTeam { get; set; }

        [JsonPropertyName("home")]
        public string? HomeTeam { get; set; }

        public string? GameType { get; set; }
        public string? GameTime { get; set; }
        public string? GameDate { get; set; }
        public string? GameStatus { get; set; }
        public ProbableStartingPitchers? ProbableStartingPitchers { get; set; }

    }
}