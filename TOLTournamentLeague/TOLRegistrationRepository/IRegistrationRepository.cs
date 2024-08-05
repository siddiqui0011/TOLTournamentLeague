using TOLTournamentLeague.DOM;

namespace TOLTournamentLeague.TOLRegistrationRepository
{
    public interface IRegistrationRepository
    {
        Task<IEnumerable<TOLRegistration>> GetRegistrationsAsync();
        Task<TOLRegistration> GetRegistrationByIdAsync(int id);
        Task AddRegistrationAsync(TOLRegistration registration);
        Task UpdateRegistrationAsync(TOLRegistration registration);
        Task DeleteRegistrationAsync(int id);
    }
}
