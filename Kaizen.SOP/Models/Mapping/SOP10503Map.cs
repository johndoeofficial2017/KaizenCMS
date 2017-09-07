using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.SOP.Mapping
{
    public class SOP10503Map : EntityTypeConfiguration<SOP10503>
    {
        public SOP10503Map()
        {
            // Primary Key
            this.HasKey(t => new { t.CollCode, t.LotRowID });

            // Properties
            this.Property(t => t.CollCode)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(15);

            this.Property(t => t.LotRowID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.CollName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.CollValue)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("SOP10503");
            this.Property(t => t.CollCode).HasColumnName("CollCode");
            this.Property(t => t.LotRowID).HasColumnName("LotRowID");
            this.Property(t => t.CollName).HasColumnName("CollName");
            this.Property(t => t.CollValue).HasColumnName("CollValue");
            this.Property(t => t.CollType).HasColumnName("CollType");

            // Relationships
            this.HasRequired(t => t.SOP10502)
                .WithMany(t => t.SOP10503)
                .HasForeignKey(d => d.LotRowID);

        }
    }
}
