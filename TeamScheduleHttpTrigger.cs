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
            string? season = req.Query["season"];
            var response = req.CreateResponse();

            _logger.LogInformation($"HTTP trigger function processed a request for {team} schedule.");

            if (!string.IsNullOrEmpty(team) && IsValidTeam(team))
            {
                var teamSchedule = await _mlbService.GetTeamSchedule(team, season);

                if (teamSchedule != null)
                {
                    await HandleResponse(response, HttpStatusCode.OK, "Successfully processed request.", teamSchedule);
                }
                else
                {
                    await HandleResponse(response, HttpStatusCode.BadRequest, "There was an issue processing request.");
                }
            }
            else
            {
                await HandleResponse(response, HttpStatusCode.BadRequest, "Error processing request. Missing or incorrect team abbreviation.");
            }

            return response;
        }

        private async Task HandleResponse(HttpResponseData response, HttpStatusCode status, string message, TeamSchedule? schedule = null)
        {

            _logger.LogInformation(message);
            response.StatusCode = status;

            if (schedule != null)
            {
                await response.WriteAsJsonAsync(schedule);
            }
            else
            {
                await response.WriteStringAsync(message);
            }
        }

        private static bool IsValidTeam(string team)
        {
            return Constants.MlbTeams.Select(p => p.Abbreviation).Contains(team);
        }
    }
}