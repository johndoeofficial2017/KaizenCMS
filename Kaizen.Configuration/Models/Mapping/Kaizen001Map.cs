using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.Configuration.Mapping
{
    public class Kaizen001Map : EntityTypeConfiguration<Kaizen001>
    {
        public Kaizen001Map()
        {
            // Primary Key
            this.HasKey(t => t.MenueTypeID);

            // Properties
            this.Property(t => t.MenueTypeID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.MenueTypeName)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Kaizen001");
            this.Property(t => t.MenueTypeID).HasColumnName("MenueTypeID");
            this.Property(t => t.MenueTypeName).HasColumnName("MenueTypeName");
        }
    }
}
