using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MapNotepad
{
    class UserService : IUserService
    {
        private readonly ISettingsManager _settingsManager;
        public UserService(ISettingsManager settingsManager)
        {
            _settingsManager = settingsManager;
        }

        public int GetCurrentUser()
        {
            return _settingsManager.CurrentUser;
        }
        public void SetCurrentUser(int id)
        {
            _settingsManager.CurrentUser = id;
        }
    }
}
