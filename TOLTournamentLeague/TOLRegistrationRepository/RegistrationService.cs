using TOLTournamentLeague.DOM;

namespace TOLTournamentLeague.TOLRegistrationRepository
{
    public class RegistrationService:IRegistrationService
    {
        private readonly IRegistrationRepository _registrationRepository;

        public RegistrationService(IRegistrationRepository registrationRepository)
        {
            _registrationRepository = registrationRepository;
        }

        public Task<IEnumerable<TOLRegistration>> GetRegistrationsAsync()
        {
            return _registrationRepository.GetRegistrationsAsync();
        }

        public Task<TOLRegistration> GetRegistrationByIdAsync(int id)
        {
            return _registrationRepository.GetRegistrationByIdAsync(id);
        }

        public Task AddRegistrationAsync(TOLRegistration registration)
        {
            return _registrationRepository.AddRegistrationAsync(registration);
        }

        public Task UpdateRegistrationAsync(TOLRegistration registration)
        {
            return _registrationRepository.UpdateRegistrationAsync(registration);
        }

        public Task DeleteRegistrationAsync(int id)
        {
            return _registrationRepository.DeleteRegistrationAsync(id);
        }

        public Task GetRegistrationByLinkedInUrlAsync(string linkedInUrl)
        {
            throw new NotImplementedException();
        }
    }
}
