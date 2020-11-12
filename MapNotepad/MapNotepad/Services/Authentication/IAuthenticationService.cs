using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MapNotepad.Services
{
    public interface IAuthenticationService
    {
        Task<int> AuthenticateAsync(string email, [Optional] string password, bool isSocialMediaAuthorizing = false);
    }
}
