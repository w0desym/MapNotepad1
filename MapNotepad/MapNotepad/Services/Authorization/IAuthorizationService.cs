using MapNotepad.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MapNotepad
{
    public interface IAuthorizationService
    {
        int Authorize(int id);
        int Register(User item);
    }
}
