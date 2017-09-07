using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.SOP.Mapping
{
    public class SOP10106Map : EntityTypeConfiguration<SOP10106>
    {
        public SOP10106Map()
        {
            // Primary Key
            this.HasKey(t => new { t.LineID, t.LotNumber });

            // Properties
            this.Property(t => t.LineID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.LotNumber)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("SOP10106");
            this.Property(t => t.LineID).HasColumnName("LineID");
            this.Property(t => t.LotNumber).HasColumnName("LotNumber");
            this.Property(t => t.ItemQuantity).HasColumnName("ItemQuantity");
        }
    }
}
