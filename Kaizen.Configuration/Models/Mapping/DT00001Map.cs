using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.Configuration.Mapping
{
    public class DT00001Map : EntityTypeConfiguration<DT00001>
    {
        public DT00001Map()
        {
            // Primary Key
            this.HasKey(t => t.ViewType);

            // Properties
            this.Property(t => t.ViewType)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.ViewName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("DT00001");
            this.Property(t => t.ViewType).HasColumnName("ViewType");
            this.Property(t => t.ViewName).HasColumnName("ViewName");
        }
    }
}
