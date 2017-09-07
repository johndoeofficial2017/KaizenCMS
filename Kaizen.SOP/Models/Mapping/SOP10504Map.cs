using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.SOP.Mapping
{
    public class SOP10504Map : EntityTypeConfiguration<SOP10504>
    {
        public SOP10504Map()
        {
            // Primary Key
            this.HasKey(t => t.BinID);

            // Properties
            this.Property(t => t.BinID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.SOPNUMBE)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("SOP10504");
            this.Property(t => t.BinID).HasColumnName("BinID");
            this.Property(t => t.ItemLineIndex).HasColumnName("ItemLineIndex");
            this.Property(t => t.SOPNUMBE).HasColumnName("SOPNUMBE");
            this.Property(t => t.IsBinGroup).HasColumnName("IsBinGroup");
            this.Property(t => t.AppliedQuantity).HasColumnName("AppliedQuantity");

            // Relationships
            this.HasRequired(t => t.SOP10501)
                .WithMany(t => t.SOP10504)
                .HasForeignKey(d => new { d.ItemLineIndex, d.SOPNUMBE });

        }
    }
}
