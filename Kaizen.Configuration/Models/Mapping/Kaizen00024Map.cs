using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.Configuration.Mapping
{
    public class Kaizen00024Map : EntityTypeConfiguration<Kaizen00024>
    {
        public Kaizen00024Map()
        {
            // Primary Key
            this.HasKey(t => t.FieldTypeID);

            // Properties
            this.Property(t => t.FieldTypeID)
                .IsRequired()
                .HasMaxLength(15);

            this.Property(t => t.FieldTypeName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Kaizen00024");
            this.Property(t => t.FieldTypeID).HasColumnName("FieldTypeID");
            this.Property(t => t.FieldTypeName).HasColumnName("FieldTypeName");
        }
    }
}
