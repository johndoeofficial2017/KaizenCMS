using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.Configuration.Mapping
{
    public class KaizenAuditMap : EntityTypeConfiguration<KaizenAudit>
    {
        public KaizenAuditMap()
        {
            // Primary Key
            this.HasKey(t => new { t.Kaizen_DATE, t.Kaizen_USER, t.Kaizen_HOST, t.Kaizen_DB, t.Kaizen_Table });

            // Properties
            this.Property(t => t.Kaizen_USER)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Kaizen_HOST)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Kaizen_DB)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Kaizen_Table)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("KaizenAudit");
            this.Property(t => t.Kaizen_DATE).HasColumnName("Kaizen_DATE");
            this.Property(t => t.Kaizen_USER).HasColumnName("Kaizen_USER");
            this.Property(t => t.Kaizen_HOST).HasColumnName("Kaizen_HOST");
            this.Property(t => t.Kaizen_DB).HasColumnName("Kaizen_DB");
            this.Property(t => t.Kaizen_Table).HasColumnName("Kaizen_Table");
            this.Property(t => t.Ins).HasColumnName("Ins");
            this.Property(t => t.Del).HasColumnName("Del");
        }
    }
}
