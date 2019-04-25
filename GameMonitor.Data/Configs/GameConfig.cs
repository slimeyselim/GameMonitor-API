using GameMonitor.Data.Entities;
using System.Data.Entity.ModelConfiguration;

namespace GameMonitor.Data.Configs
{
    public class GameConfig : EntityTypeConfiguration<Game>
    {
        public GameConfig()
        {
            ToTable("games");
            HasKey(g => g.Id);
            Property(g => g.Name).HasColumnName("Name");
            Property(g => g.DesiredPrice).HasColumnName("DesiredPrice");

            HasMany<Item>(i => i.Items).WithRequired(g => g.Game).HasForeignKey(g => g.GameId);
        }
    }
}