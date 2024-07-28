using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LiveChat.Domain.Models
{
    public class Guild
    {
        public int Id { get; private set; }

        [Required]
        public string Name { get; set; }

        public DateTime CreatedAt { get; private set; }

        [JsonIgnore]
        public ICollection<GuildMember>? Members { get; set; } = new List<GuildMember>();

        public ICollection<Channel>? Channels { get; set; } = new List<Channel>();

        [Required]
        public int OwnerId { get; set; }
        public User Owner { get; set; }

        public Guild() { }

        public Guild(string name, ICollection<GuildMember>? members, User owner)
        {
            Name = name;
            CreatedAt = DateTime.Today;
            Members = members;
            OwnerId = owner.Id;
            Owner = owner;
        }
    }
}
