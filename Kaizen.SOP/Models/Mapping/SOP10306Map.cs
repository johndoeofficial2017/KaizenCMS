using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.SOP.Mapping
{
    public class SOP10306Map : EntityTypeConfiguration<SOP10306>
    {
        public SOP10306Map()
        {
            // Primary Key
            this.HasKey(t => t.LineID);

            // Properties
            this.Property(t => t.SOPNUMBE)
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.ItemID)
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.ItemName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("SOP10306");
            this.Property(t => t.LineID).HasColumnName("LineID");
            this.Property(t => t.SOPNUMBE).HasColumnName("SOPNUMBE");
            this.Property(t => t.DOCDATE).HasColumnName("DOCDATE");
            this.Property(t => t.ItemID).HasColumnName("ItemID");
            this.Property(t => t.ItemName).HasColumnName("ItemName");
            this.Property(t => t.QTY).HasColumnName("QTY");
            this.Property(t => t.Description).HasColumnName("Description");
        }
    }
}
