namespace Chat_SignalR.Services
{
    public class PasswordService
    {
        public string Generate(string password)
        {
            return BCrypt.Net.BCrypt.EnhancedHashPassword(password);

        }
        public bool Verify(string password, string hashPassword)
        {
            return BCrypt.Net.BCrypt.EnhancedVerify(password, hashPassword);
        }
    }
}
