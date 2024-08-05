using TOLTournamentLeague.DOM;

namespace TOLTournamentLeague.LeagueRepository
{
    public interface ILeagueRepository
    {
        Task<IEnumerable<TournamentLeague>> GetAllLeaguesAsync();
        Task AddLeagueAsync(string name);
        Task<TournamentLeague> GetLeagueByIdAsync(int id);
        Task ActivateLeagueAsync(int id);
        Task<TournamentLeague> GetActiveLeagueAsync();
    }
}
