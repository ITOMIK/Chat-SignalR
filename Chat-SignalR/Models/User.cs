namespace Chat_SignalR.Models
{
        public enum Role
        {
            User=0,
            Admin=1
        } 

    public class User
    {
        public string name {  get; set; }
        public string hashPassword { get; set; }
        public long id { get; set; }

        public List<Role> roles { get; set; }

        int karma { get; set; } = 0;
        public Guid publicId { get; set; }
        public DateTime Created { get; set; } = DateTime.UtcNow;

        public User(string name, string hashPassword)
        {
            this.name = name;
            this.hashPassword = hashPassword;
            this.publicId = Guid.NewGuid();
            this.roles = new List<Role>() { Role.User};
        }
        public User(string name, string hashPassword, Role role)
        {
            if (role == Role.User)
                throw new Exception("Пользователь не может иметь повторяющиеся роли");
            this.name = name;
            this.hashPassword = hashPassword;
            this.publicId = Guid.NewGuid();
            this.roles = new List<Role>() { Role.User, role };
        }

        /// <summary>
        /// конструктор для EF Core
        /// </summary>
        public User() { }

    }
}
