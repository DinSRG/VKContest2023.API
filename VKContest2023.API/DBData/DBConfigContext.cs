using Microsoft.EntityFrameworkCore;
using VKContest2023.API.Configuration;
using VKContest2023.API.Model;

namespace VKContest2023.API.DBData
{
    public class DBConfigContext : DbContext
    {
        protected readonly IConfiguration _configuration;

        public DBConfigContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseNpgsql(_configuration.GetConnectionString("DefaultDB"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new GroupsConfiguration());
            modelBuilder.ApplyConfiguration(new StatesConfiguration());
            modelBuilder.ApplyConfiguration(new UsersConfiguration());
        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserGroup> UserGroups { get; set; }
        public DbSet<UserState> UserStates { get; set; }
    }
}
