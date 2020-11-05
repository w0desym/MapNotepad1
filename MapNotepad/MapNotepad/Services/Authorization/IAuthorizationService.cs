using MapNotepad.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MapNotepad
{
    public interface IAuthorizationService
    {
        int Authorize(int id);
        Task<int> RegisterAsync(User item);
    }
}
