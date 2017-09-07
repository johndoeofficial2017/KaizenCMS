using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.Configuration.Mapping
{
    public class KaizenSessionMap : EntityTypeConfiguration<KaizenSession>
    {
        public KaizenSessionMap()
        {
            // Primary Key
            this.HasKey(t => new { t.CompanyID, t.UserName, t.KaizenPublicKey });

            // Properties
            this.Property(t => t.CompanyID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.UserName)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("KaizenSession");
            this.Property(t => t.CompanyID).HasColumnName("CompanyID");
            this.Property(t => t.UserName).HasColumnName("UserName");
            this.Property(t => t.KaizenPublicKey).HasColumnName("KaizenPublicKey");
            this.Property(t => t.LoginDate).HasColumnName("LoginDate");
            this.Property(t => t.LoginDateOut).HasColumnName("LoginDateOut");

            // Relationships
            this.HasRequired(t => t.User)
                .WithMany(t => t.KaizenSessions)
                .HasForeignKey(d => d.UserName);

        }
    }
}
