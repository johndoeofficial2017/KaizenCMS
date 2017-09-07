using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.CMS.Mapping
{
    public class CM00085Map : EntityTypeConfiguration<CM00085>
    {
        public CM00085Map()
        {
            // Primary Key
            this.HasKey(t => new { t.FieldName, t.ViewID });

            // Properties
            this.Property(t => t.FieldName)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(20);

            this.Property(t => t.ViewID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.FieldDisplay)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("CM00085");
            this.Property(t => t.FieldName).HasColumnName("FieldName");
            this.Property(t => t.ViewID).HasColumnName("ViewID");
            this.Property(t => t.FieldDisplay).HasColumnName("FieldDisplay");

            // Relationships
            this.HasRequired(t => t.CM00071)
                .WithMany(t => t.CM00085)
                .HasForeignKey(d => d.ViewID);

        }
    }
}
