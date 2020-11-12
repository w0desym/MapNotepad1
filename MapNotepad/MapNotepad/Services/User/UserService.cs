using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MapNotepad.Services
{
    class UserService : IUserService
    {
        private readonly ISettingsManager _settingsManager;
        private readonly IAuthenticationService _authenticationService;
        private readonly IRegistrationService _registrationService;

        public UserService(
            ISettingsManager settingsManager,
            IAuthenticationService authenticationService,
            IRegistrationService registrationService)
        {
            _settingsManager = settingsManager;
            _authenticationService = authenticationService;
            _registrationService = registrationService;
        }

        public int CurrentUserId
        {
            get => _settingsManager.CurrentUser;
            set => _settingsManager.CurrentUser = value;
        }

        public async Task<int> RegisterAsync(string email, string name, string password)
        {
            return await _registrationService.RegisterAsync(email, name, password);
        }

        public async Task<bool> LoginAsync(string email, [Optional] string password, bool isSocialMediaAuthorizing = false)
        {
            bool result;
            int id = await _authenticationService.AuthenticateAsync(email, password, isSocialMediaAuthorizing);
            
            if (id != -1)
            {
                CurrentUserId = id;
                result = true;
            }
            else
            {
                result = false;
            }

            return result;
        }

        public void Logout()
        {
            CurrentUserId = -1;
        }
    }
}
