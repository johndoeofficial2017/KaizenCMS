using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.Configuration.Mapping
{
    public class Kaizen00016Map : EntityTypeConfiguration<Kaizen00016>
    {
        public Kaizen00016Map()
        {
            // Primary Key
            this.HasKey(t => new { t.TRXTypeID, t.CompanyID });

            // Properties
            this.Property(t => t.TRXTypeID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.CompanyID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.TrxTypeName)
                .HasMaxLength(30);

            // Table & Column Mappings
            this.ToTable("Kaizen00016");
            this.Property(t => t.TRXTypeID).HasColumnName("TRXTypeID");
            this.Property(t => t.CompanyID).HasColumnName("CompanyID");
            this.Property(t => t.TrxTypeName).HasColumnName("TrxTypeName");
        }
    }
}
