using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.Admin.Mapping
{
    public class Sys00104Map : EntityTypeConfiguration<Sys00104>
    {
        public Sys00104Map()
        {
            // Primary Key
            this.HasKey(t => t.DocTypeID);

            // Properties
            this.Property(t => t.DocTypeName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Sys00104");
            this.Property(t => t.DocTypeID).HasColumnName("DocTypeID");
            this.Property(t => t.DocTypeName).HasColumnName("DocTypeName");
        }
    }
}
