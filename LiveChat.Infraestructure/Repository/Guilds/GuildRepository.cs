using LiveChat.Domain.Models;
using LiveChat.Domain.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveChat.Infraestructure.Repository.Guilds
{
    public class GuildRepository : IGuildRepository
    {
        private readonly DbContextClass dbContextClass;

        public GuildRepository(DbContextClass dbContextClass)
        {
            this.dbContextClass = dbContextClass;
        }

        public async Task<IReadOnlyCollection<GuildMember>> AddMemberAsync(User user, Guild guild)
        {
            if (guild.Members.Any(gm => gm.UserId == user.Id))
            {
                throw new Exception("User is already a member of the guild.");
            }

            guild.Members.Add(new GuildMember { UserId = user.Id, GuildId = guild.Id });

            await dbContextClass.SaveChangesAsync();

            await dbContextClass.Entry(guild).Collection(g => g.Members).LoadAsync();
            var updatedMembers = guild.Members.ToList().AsReadOnly();

            return updatedMembers;
        }

        public async Task<Guild> CreateNewGuild(Guild Guild)
        {
            var guild = dbContextClass.Guilds.Add(Guild);

            await dbContextClass.SaveChangesAsync();

            return guild.Entity;
        }

        public async Task<Guild> GetAvailableGuild(int id)
        {
            return await dbContextClass.Guilds
            .Include(g => g.Members)
            .ThenInclude(gm => gm.User)
            .FirstOrDefaultAsync(g => g.Id == id);
        }

        public async Task<IReadOnlyCollection<GuildMember>> DeleteUserAsync(User user, Guild guild)
        {
            var guildMember = guild.Members.FirstOrDefault(m => m.UserId == user.Id);

            if (guildMember == null)
            {
                throw new Exception("User is not a member of the guild.");
            }

            guild.Members.Remove(guildMember);

            await dbContextClass.SaveChangesAsync();

            return guild.Members.ToList();
        }

        public async Task<List<Guild>> GetAvailableGuilds()
        {
            return await dbContextClass.Guilds.ToListAsync();
        }

        public Task<User> UpdateMemberRoleAsync(User user)
        {
            throw new NotImplementedException();
        }

        public async Task<Channel> CreateNewChannel(Channel Channel)
        {
            Guild guild = dbContextClass.Guilds.Where(x => x.Id == Channel.GuildId).FirstOrDefault();

            guild.Channels.Add(Channel);

            await dbContextClass.SaveChangesAsync();

            return Channel;
        }
    }
}
