using Cinema.Domain.Identity;
using Cinema.Persistence.Contextos;
using Cinema.Persistence.Contratos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Persistence
{
    public class UserPersist : GeralPersist, IUserPersist
    {
        private readonly CinemaContext _context;

        public UserPersist(CinemaContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetUserByUserNameAsync(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(user => user.UserName == username.ToLower());
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _context.Users.FirstOrDefaultAsync(user => user.Id == id);
        }
    }
}
