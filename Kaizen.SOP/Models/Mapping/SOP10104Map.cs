using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.SOP.Mapping
{
    public class SOP10104Map : EntityTypeConfiguration<SOP10104>
    {
        public SOP10104Map()
        {
            // Primary Key
            this.HasKey(t => new { t.LineID, t.BinID });

            // Properties
            this.Property(t => t.LineID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.BinID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("SOP10104");
            this.Property(t => t.LineID).HasColumnName("LineID");
            this.Property(t => t.BinID).HasColumnName("BinID");
            this.Property(t => t.IsBinGroup).HasColumnName("IsBinGroup");
            this.Property(t => t.AppliedQuantity).HasColumnName("AppliedQuantity");

            // Relationships
            this.HasRequired(t => t.SOP10101)
                .WithMany(t => t.SOP10104)
                .HasForeignKey(d => d.BinID);

        }
    }
}
