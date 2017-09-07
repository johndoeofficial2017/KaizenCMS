using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.SOP.Mapping
{
    public class SOP10303Map : EntityTypeConfiguration<SOP10303>
    {
        public SOP10303Map()
        {
            // Primary Key
            this.HasKey(t => t.LineID);

            // Properties
            this.Property(t => t.SOPNUMBE)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.CheckNumber)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.CheckDate)
                .HasMaxLength(50);

            this.Property(t => t.CheckBankName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("SOP10303");
            this.Property(t => t.LineID).HasColumnName("LineID");
            this.Property(t => t.SOPNUMBE).HasColumnName("SOPNUMBE");
            this.Property(t => t.DOCAMNT).HasColumnName("DOCAMNT");
            this.Property(t => t.CheckNumber).HasColumnName("CheckNumber");
            this.Property(t => t.CheckDate).HasColumnName("CheckDate");
            this.Property(t => t.CheckBankName).HasColumnName("CheckBankName");

            // Relationships
            this.HasRequired(t => t.SOP10300)
                .WithMany(t => t.SOP10303)
                .HasForeignKey(d => d.SOPNUMBE);

        }
    }
}
