using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.Configuration.Mapping
{
    public class Prn00001Map : EntityTypeConfiguration<Prn00001>
    {
        public Prn00001Map()
        {
            // Primary Key
            this.HasKey(t => t.PrnCatTypeID);

            // Properties
            this.Property(t => t.PrnCatTypeName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Prn00001");
            this.Property(t => t.PrnCatTypeID).HasColumnName("PrnCatTypeID");
            this.Property(t => t.PrnCatTypeName).HasColumnName("PrnCatTypeName");
        }
    }
}
