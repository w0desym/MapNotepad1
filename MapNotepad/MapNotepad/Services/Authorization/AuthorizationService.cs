using MapNotepad.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MapNotepad
{
    class AuthorizationService : IAuthorizationService
    {
        private readonly IUserService _userService;
        private readonly IRepositoryService _repositoryService;
        public AuthorizationService(IUserService userService,
            IRepositoryService repositoryService)
        {
            _userService = userService;
            _repositoryService = repositoryService;
        }
        public void Authorize(int id)
        {
            _userService.SetCurrentUser(id);
        }
        public async Task<int> RegisterAsync(User user)
        {
            if (user.Id != 0)
            {
                await _repositoryService.UpdateItemAsync(user);
                return user.Id;
            }
            else
            {
                return await _repositoryService.InsertItemAsync(user);
            }
        }
    }
}
