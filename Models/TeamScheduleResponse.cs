using System.Text.Json.Serialization;

namespace AzureFunctionsMlbCSharp
{
    public class TeamScheduleResponse
    {
        public int StatusCode { get; set; }

        [JsonPropertyName("body")]
        public TeamSchedule? Schedule { get; set; }
    }
}