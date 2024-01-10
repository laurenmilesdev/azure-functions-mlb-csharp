using System.Text.Json.Serialization;

namespace AzureFunctionsMlbCSharp
{
    public class TeamSchedule
    {
        public string? Team { get; set; }

        [JsonPropertyName("schedule")]
        public Game[]? Games { get; set; }
    }
}