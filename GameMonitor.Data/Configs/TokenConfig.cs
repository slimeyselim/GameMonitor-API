using GameMonitor.Data.Entities;
using System.Data.Entity.ModelConfiguration;

namespace GameMonitor.Data.Configs
{
    public class TokenConfig : EntityTypeConfiguration<Token>
    {
        public TokenConfig()
        {
            ToTable("tokens");
            HasKey(t => t.TokenId);
            Property(t => t.UserId).HasColumnName("UserId");
            Property(t => t.AuthToken).HasColumnName("AuthToken");
            Property(t => t.IssuedOn).HasColumnName("IssuedOn");
            Property(t => t.ExpiresOn).HasColumnName("ExpiresOn");
        }
    }
}
