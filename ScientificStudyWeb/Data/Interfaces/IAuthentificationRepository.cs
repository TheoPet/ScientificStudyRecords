using System.Threading.Tasks;
using ScientificStudyWeb.Models;

namespace ScientificStudyWeb.Data.Interfaces
{
    public interface IAuthentificationRepository
    {
         Task<User> Register(User user, string password);
         Task<User> Login (string username, string password);
         Task<bool> UserExists(string username);
    }
}