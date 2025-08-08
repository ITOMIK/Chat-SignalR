namespace Chat_SignalR.Models
{
    public class Breanch
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid PublicId { get; set; }
        public List<Message> messages { get; set; }
    }
}
