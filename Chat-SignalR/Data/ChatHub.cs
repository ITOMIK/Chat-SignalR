using Chat_SignalR.Repositories;
using Microsoft.AspNetCore.SignalR;

namespace Chat_SignalR.Data
{
    public class ChatHub : Hub
    {
        private readonly MessageRepository messageRepository;
        private readonly UserReposytory userReposytory;
        public ChatHub(MessageRepository _messageRepository, UserReposytory _userRep)
        {
            messageRepository = _messageRepository; 
            userReposytory = _userRep;
        }
        public async Task SendMessageToBreanch(int breanchId, string message)
        {
            var userClaim = Context.User?.FindFirst("userId");
            var userName = Context.User?.Identity?.Name ?? "Guest";
            var userpubId = userClaim?.Value ?? "Guest";
            var user =await userReposytory.GetByPublicId(userpubId);
            if (user==null)
            {
                throw new Exception("No User");
            }
            await messageRepository.Add(new Models.Message() { BreanchId=breanchId, UserId= user.id, Text= message,publicId = Guid.NewGuid() });
            await Clients.Group(breanchId.ToString()).SendAsync("ReceiveMessage", user.name, message);
        }

        public override async Task OnConnectedAsync()
        {
            var httpContext = Context.GetHttpContext();
            var breanchId = httpContext.Request.Query["breanchId"];

            if (!string.IsNullOrEmpty(breanchId))
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, breanchId);
            }

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var httpContext = Context.GetHttpContext();
            var breanchId = httpContext.Request.Query["breanchId"];

            if (!string.IsNullOrEmpty(breanchId))
            {
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, breanchId);
            }

            await base.OnDisconnectedAsync(exception);
        }
    }
}
