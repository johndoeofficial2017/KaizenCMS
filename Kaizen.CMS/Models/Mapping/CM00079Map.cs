using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.CMS.Mapping
{
    public class CM00079Map : EntityTypeConfiguration<CM00079>
    {
        public CM00079Map()
        {
            // Primary Key
            this.HasKey(t => new { t.GraphID, t.FieldName });

            // Properties
            this.Property(t => t.GraphID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.FieldName)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(20);

            this.Property(t => t.FieldDisplay)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.FieldValue)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(20);

            this.Property(t => t.FieldColor)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("CM00079");
            this.Property(t => t.GraphID).HasColumnName("GraphID");
            this.Property(t => t.FieldName).HasColumnName("FieldName");
            this.Property(t => t.FieldDisplay).HasColumnName("FieldDisplay");
            this.Property(t => t.FieldValue).HasColumnName("FieldValue");
            this.Property(t => t.SummeryTypeID).HasColumnName("SummeryTypeID");
            this.Property(t => t.FieldColor).HasColumnName("FieldColor");

            // Relationships
            this.HasRequired(t => t.CM00076)
                .WithMany(t => t.CM00079)
                .HasForeignKey(d => d.GraphID);

        }
    }
}
