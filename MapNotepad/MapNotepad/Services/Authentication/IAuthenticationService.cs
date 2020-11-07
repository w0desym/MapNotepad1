using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MapNotepad
{
    public interface IAuthenticationService
    {
        Task<int> AuthenticateAsync(string login, string password);
        Task<int> AuthenticateAsync(string email);
    }
}
