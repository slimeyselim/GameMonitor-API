using GameMonitor.Data.Entities;
using System.Data.Entity.ModelConfiguration;

namespace GameMonitor.Data.Configs
{
    public class UserConfig : EntityTypeConfiguration<User>
    {
        public UserConfig()
        {
            ToTable("users");
            HasKey(g => g.Id);
            Property(g => g.Username).HasColumnName("Username");
            Property(g => g.Password).HasColumnName("Password");
        }
    }
}
