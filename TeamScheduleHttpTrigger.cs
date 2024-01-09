using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace AzureFunctionsMlbCSharp
{
    public class TeamScheduleHttpTrigger
    {
        private readonly ILogger _logger;
        private readonly IMlbService _mlbService;

        public TeamScheduleHttpTrigger(ILoggerFactory loggerFactory, IMlbService mlbService)
        {
            _logger = loggerFactory.CreateLogger<TeamScheduleHttpTrigger>();
            _mlbService = mlbService;
        }

        [Function("TeamScheduleHttpTrigger")]
        public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestData req)
        {
            string? team = req.Query["team"];
            var response = req.CreateResponse();

            _logger.LogInformation($"HTTP trigger function processed a request for {team} schedule.");

            if (!string.IsNullOrEmpty(team) && IsValidTeam(team))
            {
                var teamSchedule = await _mlbService.GetTeamSchedule(team);

                if (teamSchedule != null)
                {
                    response.StatusCode = HttpStatusCode.OK;

                    await response.WriteAsJsonAsync(teamSchedule);

                    _logger.LogInformation($"Successfully processed request.");
                }
            }
            else
            {
                var errorMessage = "Error processing request. Missing or incorrect team abbreviation.";
                response.StatusCode = HttpStatusCode.BadRequest;

                // Add in team abbreviation list here
                await response.WriteStringAsync($"{errorMessage} Please try again.");

                _logger.LogInformation(errorMessage);
            }

            return response;
        }

        private static bool IsValidTeam(string team)
        {
            return Constants.MlbTeams.Select(p => p.Abbreviation).Contains(team);
        }
    }
}