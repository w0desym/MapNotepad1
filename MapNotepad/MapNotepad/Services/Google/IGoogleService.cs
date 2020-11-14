using MapNotepad.Models;
using System.Threading.Tasks;

namespace MapNotepad.Services
{
    public interface IGoogleService
    {
        Task<User> TryLoginAsync();
        void Logout();
    }
}
