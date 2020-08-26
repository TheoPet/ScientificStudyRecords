using System.Threading.Tasks;
using ScientificStudyWeb.Models;

namespace ScientificStudyWeb.Data.Interfaces
{
    public interface IUserRepository
    {
        void Add(User user);
        Task<User> Login(string username, string password);
        Task<bool> UserExists(string username);
    }
}