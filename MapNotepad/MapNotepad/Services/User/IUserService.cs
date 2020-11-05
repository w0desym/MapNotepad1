using System;
using System.Collections.Generic;
using System.Text;

namespace MapNotepad
{
    public interface IUserService
    {
        int GetCurrentUser();
        void SetCurrentUser(int id);
    }
}
