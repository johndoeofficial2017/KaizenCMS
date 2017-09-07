using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.Configuration.Mapping
{
    public class Kaizen00009Map : EntityTypeConfiguration<Kaizen00009>
    {
        public Kaizen00009Map()
        {
            // Primary Key
            this.HasKey(t => t.DropValueID);

            // Properties
            this.Property(t => t.ExtraFieldID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(5);

            this.Property(t => t.FieldValue)
                .IsRequired()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("Kaizen00009");
            this.Property(t => t.DropValueID).HasColumnName("DropValueID");
            this.Property(t => t.ExtraFieldID).HasColumnName("ExtraFieldID");
            this.Property(t => t.FieldValue).HasColumnName("FieldValue");

            // Relationships
            this.HasRequired(t => t.Kaizen00003)
                .WithMany(t => t.Kaizen00009)
                .HasForeignKey(d => d.ExtraFieldID);

        }
    }
}
