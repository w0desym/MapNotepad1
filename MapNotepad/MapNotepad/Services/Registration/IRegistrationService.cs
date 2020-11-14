using System.Threading.Tasks;

namespace MapNotepad.Services
{
    public interface IRegistrationService
    {
        Task<int> RegisterAsync(string email, string name, string password);
    }
}
