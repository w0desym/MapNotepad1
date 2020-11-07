using MapNotepad.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapNotepad
{
    class AuthenticationService : IAuthenticationService
    {
        private readonly IRepositoryService _repositoryService;
        public AuthenticationService(IRepositoryService repositoryService)
        {
            _repositoryService = repositoryService;
        }
        public async Task<int> AuthenticateAsync(string email, string password)
        {
            var users = await _repositoryService.GetItemsAsync<User>();
            var matchingUser = users.FirstOrDefault(x => x.Email == email && x.Password == password);
            int id;

            id = matchingUser != null ? matchingUser.Id : 0;

            return id;
        }
        public async Task<int> AuthenticateAsync(string email)
        {
            var users = await _repositoryService.GetItemsAsync<User>();
            var matchingUser = users.FirstOrDefault(x => x.Email == email);
            int id;

            id = matchingUser != null ? matchingUser.Id : 0;

            return id;
        }
    }
}
