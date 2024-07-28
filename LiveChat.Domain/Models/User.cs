using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LiveChat.Domain.Models
{
    public class User
    {
        public int Id { get; private set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public string Role { get; private set; }

        public ICollection<GuildMember> Guilds { get; set; } = new List<GuildMember>();

        public User() { }

        public User(string name, string email, string password, string role = null)
        {
            Name = name;
            Email = email;
            Password = password;
            Role = role != null ? role : "user";
        }

        public User(string role) => Role = role;
    }
}
