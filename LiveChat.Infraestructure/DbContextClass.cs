using LiveChat.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace LiveChat.Infraestructure
{
    public class DbContextClass : DbContext
    {
        protected readonly IConfiguration Configuration;

        public DbContextClass(IConfiguration configuration)
        {
            Configuration = configuration;
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"));
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Guild> Guilds { get; set; }
        public DbSet<Channel> Channels { get; set; }
        public DbSet<GuildMember> GuildMembers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GuildMember>()
                .HasKey(gm => new { gm.UserId, gm.GuildId });

            modelBuilder.Entity<GuildMember>()
                .HasOne(gm => gm.User)
                .WithMany(u => u.Guilds)
                .HasForeignKey(gm => gm.UserId);

            modelBuilder.Entity<GuildMember>()
                .HasOne(gm => gm.Guild)
                .WithMany(g => g.Members)
                .HasForeignKey(gm => gm.GuildId);

            modelBuilder.Entity<Guild>()
                .HasOne(e => e.Owner)
                .WithMany()
                .HasForeignKey(g => g.OwnerId)
                .IsRequired();

            modelBuilder.Entity<Guild>()
                .HasMany(e => e.Channels)
                .WithOne(e => e.Guild)
                .HasForeignKey(g => g.GuildId)
                .IsRequired(false);

            modelBuilder.Entity<Channel>()
                .HasMany(e => e.Messages)
                .WithOne(e => e.Channel)
                .HasForeignKey(g => g.ChannelId)
                .IsRequired(true);
        }
    }
}
