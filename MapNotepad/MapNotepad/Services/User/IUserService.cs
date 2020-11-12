using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MapNotepad.Services
{
    public interface IUserService
    {
        int CurrentUserId { get; set; }
        Task<int> RegisterAsync(string email, string name, string password);
        Task<bool> LoginAsync(string email, [Optional] string password, bool isSocialMediaAuthorizing = false);
        void Logout();
    }
}
