using System.Threading.Tasks;
using ScientificStudiesRecord.Models;

namespace ScientificStudiesRecord.Data
{
    public interface IAuthentificationRepository
    {
         Task<User> Register(User user, string password);
         Task<User> Login (string username, string password);
         Task<bool> UserExists(string username);
    }
}