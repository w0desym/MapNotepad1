using MapNotepad.Models;
using Plugin.GoogleClient;
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
        private readonly IGoogleClientManager _googleClientManager;
        public AuthorizationService(IUserService userService,
            IRepositoryService repositoryService,
            IGoogleClientManager googleClientManager)
        {
            _userService = userService;
            _repositoryService = repositoryService;
            _googleClientManager = googleClientManager;
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
        public async Task<User> LoginGoogleAsync()
        {
            User user = new User();

            try
            {
                var googleUser = await _googleClientManager.LoginAsync();

                if (googleUser != null)
                {
                    user.Email = googleUser.Data.Email;
                    user.Name = googleUser.Data.Name;
                }
            }
            catch
            {

            }

            return user;
        }

        public void LogoutGoogle()
        {
            _googleClientManager.Logout();
        }
    }
}
