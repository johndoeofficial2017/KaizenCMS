using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.Configuration.Mapping
{
    public class Kaizen00006Map : EntityTypeConfiguration<Kaizen00006>
    {
        public Kaizen00006Map()
        {
            // Primary Key
            this.HasKey(t => t.FieldTypeID);

            // Properties
            this.Property(t => t.FieldTypeID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.FieldTypeName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Kaizen00006");
            this.Property(t => t.FieldTypeID).HasColumnName("FieldTypeID");
            this.Property(t => t.FieldTypeName).HasColumnName("FieldTypeName");
        }
    }
}
