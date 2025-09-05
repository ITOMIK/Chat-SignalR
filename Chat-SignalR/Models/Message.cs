namespace Chat_SignalR.Models
{
    public class Message
    {
        public int Id { get; set; }
        public Guid publicId { get; set; }
        public Breanch Breanch { get; set; }
        virtual public long BreanchId { get; set; }
        public User User { get; set; }
       virtual public long UserId { get; set; }

        public string Text { get; set; }
    }
}
