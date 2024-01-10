using System.Net.Http.Json;

namespace AzureFunctionsMlbCSharp
{
    public class MlbService : IMlbService
    {
        private readonly IHttpClientFactory _clientFactory;

        public MlbService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        async Task<TeamSchedule?> IMlbService.GetTeamSchedule(string team, string? season)
        {
            var client = _clientFactory.CreateClient("mlb");

            HttpResponseMessage response = await client.GetAsync($"/getMLBTeamSchedule?teamAbv={team.ToUpper()}&season={season ?? DateTime.Now.Year.ToString()}");

            return response.IsSuccessStatusCode ? (await response.Content.ReadFromJsonAsync<TeamScheduleResponse>())?.Schedule : null;
        }
    }
}