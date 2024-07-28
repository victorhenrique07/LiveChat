using LiveChat.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveChat.Domain.Repository
{
    public interface IGuildMemberRepository
    {
        public Task<List<GuildMember>> GetAllGuildMembers(Guild Guild);
        public Task<List<GuildMember>> GetAllUserGuilds(User User);
        
    }
}
