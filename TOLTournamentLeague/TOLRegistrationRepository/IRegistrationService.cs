using TOLTournamentLeague.DOM;

namespace TOLTournamentLeague.TOLRegistrationRepository
{
    public interface IRegistrationService
    {
        Task<IEnumerable<TOLRegistration>> GetRegistrationsAsync();
        Task<TOLRegistration> GetRegistrationByIdAsync(int id);
        Task AddRegistrationAsync(TOLRegistration registration);
        Task UpdateRegistrationAsync(TOLRegistration registration);
        Task DeleteRegistrationAsync(int id);
        Task GetRegistrationByLinkedInUrlAsync(string linkedInUrl);
    }
}
