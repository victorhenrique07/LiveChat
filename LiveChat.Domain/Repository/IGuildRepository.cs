using LiveChat.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveChat.Domain.Repository
{
    public interface IGuildRepository
    {
        public Task<Guild> CreateNewGuild(Guild guild);
        public Task<List<Guild>> GetAvailableGuilds();
        public Task<Guild> GetAvailableGuild(int Id);
        public Task<IReadOnlyCollection<GuildMember>> AddMemberAsync(User User, Guild Guild);
        public Task<User> UpdateMemberRoleAsync(User user);
        public Task<IReadOnlyCollection<GuildMember>> DeleteUserAsync(User User, Guild Guild);
        public Task<Channel> CreateNewChannel(Channel Channel);
    }
}