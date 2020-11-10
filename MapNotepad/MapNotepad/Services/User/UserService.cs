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

        public int CurrentUserId
        {
            get => _settingsManager.CurrentUser;
            set => _settingsManager.CurrentUser = value;
        }
    }
}
