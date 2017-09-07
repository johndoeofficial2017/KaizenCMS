using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.CMS.Mapping
{
    public class CM00077Map : EntityTypeConfiguration<CM00077>
    {
        public CM00077Map()
        {
            // Primary Key
            this.HasKey(t => new { t.FieldName, t.GraphID });

            // Properties
            this.Property(t => t.FieldName)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(20);

            this.Property(t => t.GraphID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.FieldDisplay)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("CM00077");
            this.Property(t => t.FieldName).HasColumnName("FieldName");
            this.Property(t => t.GraphID).HasColumnName("GraphID");
            this.Property(t => t.FieldDisplay).HasColumnName("FieldDisplay");

            // Relationships
            this.HasRequired(t => t.CM00076)
                .WithMany(t => t.CM00077)
                .HasForeignKey(d => d.GraphID);

        }
    }
}
