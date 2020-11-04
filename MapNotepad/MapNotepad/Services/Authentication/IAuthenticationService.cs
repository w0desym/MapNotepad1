using System;
using System.Collections.Generic;
using System.Text;

namespace MapNotepad
{
    public interface IAuthenticationService
    {
        int Authenticate(string login, string password);
    }
}
