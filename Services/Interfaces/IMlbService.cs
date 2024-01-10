namespace AzureFunctionsMlbCSharp
{
    public interface IMlbService
    {
        Task<TeamSchedule?> GetTeamSchedule(string team, string? season = null);
    }
}