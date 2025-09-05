using Chat_SignalR.Data;
using Chat_SignalR.Models;
using Microsoft.EntityFrameworkCore;

namespace Chat_SignalR.Repositories
{
    public class BreanchRepository
    {
        private readonly DataBaseContext _context;
        public BreanchRepository(DataBaseContext context)
        {
            _context = context;
        }
        public async Task Add(Breanch breanch)
        {
            await _context.Breanches.AddAsync(breanch);
            await _context.SaveChangesAsync();
        }
        public async Task Remove(int id)
        {
            var b = await _context.Breanches.FirstOrDefaultAsync(x => x.Id == id);
            if (b == null)
                return;
            _context.Breanches.Remove(b);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Breanch>> GetAllBreanches()
        {
            return await _context.Breanches.ToListAsync();
        }

        public async Task<Breanch> GetBreanchById(int id)
        {
            var b = await _context.Breanches
                          .Include(x => x.messages)
                          .ThenInclude(m => m.User)
                          .FirstOrDefaultAsync(x => x.Id == id);
            if (b == null) return null;
            return b;
        }
    }
}
