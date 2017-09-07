using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.SOP.Mapping
{
    public class SOP00020Map : EntityTypeConfiguration<SOP00020>
    {
        public SOP00020Map()
        {
            // Primary Key
            this.HasKey(t => t.TRXTypeID);

            // Properties
            this.Property(t => t.TrxTypeName)
                .HasMaxLength(15);

            this.Property(t => t.NextDocumentNumber)
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.NextNumberPrefix)
                .HasMaxLength(5);

            // Table & Column Mappings
            this.ToTable("SOP00020");
            this.Property(t => t.TRXTypeID).HasColumnName("TRXTypeID");
            this.Property(t => t.TrxTypeName).HasColumnName("TrxTypeName");
            this.Property(t => t.NextDocumentNumber).HasColumnName("NextDocumentNumber");
            this.Property(t => t.NextNumberPrefix).HasColumnName("NextNumberPrefix");
            this.Property(t => t.NextNumberlenth).HasColumnName("NextNumberlenth");
        }
    }
}
