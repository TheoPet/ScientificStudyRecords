using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using ScientificStudyWeb.Models;
using ScientificStudyWeb.Data.Interfaces;


namespace ScientificStudyWeb.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly ScientificStudiesRecordDbContext _context;
        internal DbSet<User> dbSet;


        public UserRepository(ScientificStudiesRecordDbContext context)
        {
            _context = context;
            dbSet = context.Set<User>();
        }

        public async Task<User> Login(string username, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username.Equals(username.ToLower()));

            if (user == null)
                return null;

            if (!VerifyPasswordHash(password, user.PasswordHash, user.PaswordSalt))
                return null;

            return user;
        }

        public void Add(User user)
        {
            dbSet.Add(user);
        }

        public async Task<bool> UserExists(string username)
        {
            return await _context.Users.AnyAsync(u => u.Username.ToLower().Equals(username));
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

                for (var i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i])
                        return false;
                }
                return true;
            }
        }

     

    }
}