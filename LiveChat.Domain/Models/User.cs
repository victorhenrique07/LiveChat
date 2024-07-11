namespace LiveChat.Domain.Models
{
    public class User
    {
        public int Id { get; private set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public IReadOnlyCollection<Guild>? Guilds { get; set; }

        public string Role { get; private set; }

        public User(string name, string email, string password)
        {
            Name = name;
            Email = email;
            Password = password;
        }
    }
}
