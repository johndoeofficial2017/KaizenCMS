using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.CMS.Mapping
{
    public class CM20202Map : EntityTypeConfiguration<CM20202>
    {
        public CM20202Map()
        {
            // Primary Key
            this.HasKey(t => t.BatchID);

            // Properties
            this.Property(t => t.BatchID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.ClientID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(15);

            this.Property(t => t.ClientName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.CreatedBy)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.UploadedFileName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("CM20202");
            this.Property(t => t.BatchID).HasColumnName("BatchID");
            this.Property(t => t.ClientID).HasColumnName("ClientID");
            this.Property(t => t.ClientName).HasColumnName("ClientName");
            this.Property(t => t.BookingDate).HasColumnName("BookingDate");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.UploadedFileName).HasColumnName("UploadedFileName");
        }
    }
}
