using MapNotepad.Models;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace MapNotepad.Services
{
    class AuthenticationService : IAuthenticationService
    {
        private readonly IRepositoryService _repositoryService;

        public AuthenticationService(IRepositoryService repositoryService)
        {
            _repositoryService = repositoryService;
        }

        public async Task<int> AuthenticateAsync(string email, [Optional] string password, bool isSocialMediaAuthorizing = false)
        {
            int id;
            var users = await _repositoryService.GetItemsAsync<User>();
            User matchingUser = null;

            if (isSocialMediaAuthorizing)
            {
                if (users != null && !string.IsNullOrEmpty(email))
                {
                    matchingUser = users.FirstOrDefault(x => x.Email == email.ToUpper());
                }
            }
            else
            {
                if (users != null && !string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password))
                {
                    matchingUser = users.FirstOrDefault(x => x.Email == email.ToUpper() && x.Password == password);
                }
            }

            id = matchingUser != null ? matchingUser.Id : -1;

            return id;
        }
    }
}
