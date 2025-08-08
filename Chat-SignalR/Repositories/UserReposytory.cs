using Chat_SignalR.Data;
using Chat_SignalR.Models;
using Microsoft.EntityFrameworkCore;

namespace Chat_SignalR.Repositories
{
    public class UserReposytory
    {
        private readonly DataBaseContext _context;
        public UserReposytory(DataBaseContext context)
        {
            _context = context;
        }

        public async Task Add(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task<User?> GetByName(string name)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.name == name);
        }
    }
}
