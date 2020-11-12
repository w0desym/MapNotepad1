using MapNotepad.Models;
using Plugin.GoogleClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace MapNotepad.Services
{
    class GoogleService : IGoogleService
    {
        private readonly IGoogleClientManager _googleClientManager;
        public GoogleService(IGoogleClientManager googleClientManager)
        {
            _googleClientManager = googleClientManager;
        }

        public async Task<User> TryLoginAsync()
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
                user = null;
            }

            return user;
        }
        public void Logout()
        {
            _googleClientManager.Logout();
        }
    }
}
