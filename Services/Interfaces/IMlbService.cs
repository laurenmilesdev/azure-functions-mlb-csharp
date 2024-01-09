namespace AzureFunctionsMlbCSharp
{
    public interface IMlbService
    {
        Task<TeamSchedule?> GetTeamSchedule(string teamAbv, string? season = null);
    }
}