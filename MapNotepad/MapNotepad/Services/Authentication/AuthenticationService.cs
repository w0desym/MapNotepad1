using MapNotepad.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MapNotepad
{
    class AuthenticationService : IAuthenticationService
    {
        private readonly IRepositoryService _repositoryService;
        public AuthenticationService(IRepositoryService repositoryService)
        {
            _repositoryService = repositoryService;
        }
        public int Authenticate(string email, string password)
        {
            var user = _repositoryService.GetItems<User>().FirstOrDefault(x => x.Email == email && x.Password == password);
            if (user != null)
                return user.Id;
            else
                return 0;
        }
    }
}
