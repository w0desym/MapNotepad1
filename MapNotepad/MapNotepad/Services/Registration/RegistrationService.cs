using MapNotepad.Models;
using System.Threading.Tasks;

namespace MapNotepad.Services
{
    class RegistrationService : IRegistrationService
    {
        private readonly IRepositoryService _repositoryService;

        public RegistrationService(IRepositoryService repositoryService)
        {
            _repositoryService = repositoryService;
        }

        public async Task<int> RegisterAsync(string email, string name, string password)
        {
            User newUser = null;

            if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(password))
            {
                newUser = new User { Email = email.ToUpper(), Name = name, Password = password };
            }

            return await _repositoryService.TryInsertItemAsync(newUser);
        }
    }
}
