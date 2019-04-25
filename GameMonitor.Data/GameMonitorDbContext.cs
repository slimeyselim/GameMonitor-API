using System.Data.Entity;
using GameMonitor.Data.Entities;
using GameMonitor.Data.Configs;

namespace GameMonitor.Data
{
    public class GameMonitorDbContext : DbContext
    {
        public GameMonitorDbContext() : base("GameMonitorDb")
        {
            this.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
        }

        public DbSet<Game> Games { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Token> Tokens { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new GameConfig());
            modelBuilder.Configurations.Add(new ItemConfig());
            modelBuilder.Configurations.Add(new UserConfig());
            modelBuilder.Configurations.Add(new TokenConfig());

            base.OnModelCreating(modelBuilder);
        }
    }
}
