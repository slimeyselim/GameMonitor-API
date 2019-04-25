using GameMonitor.Data.Entities;
using System.Data.Entity.ModelConfiguration;


namespace GameMonitor.Data.Configs
{
    public class ItemConfig: EntityTypeConfiguration<Item>
    {
        public ItemConfig()
        {
            ToTable("items");
            HasKey(g => g.Id);
            Property(g => g.GameId).HasColumnName("GameId");
            Property(g => g.Source).HasColumnName("Source");
            Property(g => g.Link).HasColumnName("Link");
            Property(g => g.CurrentPrice).HasColumnName("CurrentPrice");
            Property(g => g.DateUpdated).HasColumnName("DateUpdated");
            Property(g => g.DateEnd).HasColumnName("DateEnd");
            Property(g => g.Comments).HasColumnName("Comments");

            HasRequired<Game>(g => g.Game);
        }
    }
}
