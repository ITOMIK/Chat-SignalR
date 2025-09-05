using Chat_SignalR.Data;
using Chat_SignalR.Models;

namespace Chat_SignalR.Repositories
{
    public class MessageRepository
    {
        private readonly DataBaseContext _context;
        public MessageRepository(DataBaseContext context)
        {
            _context = context;
        }
        public async Task Add(Message mes)
        {
            await _context.Messages.AddAsync(mes);
            await _context.SaveChangesAsync();
        }
    }
}
