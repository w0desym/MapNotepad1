using MapNotepad.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MapNotepad.Services
{
    public interface IGoogleService
    {
        Task<User> TryLoginAsync();
        void Logout();
    }
}
