using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.Admin.Mapping
{
    public class Sys00007Map : EntityTypeConfiguration<Sys00007>
    {
        public Sys00007Map()
        {
            // Primary Key
            this.HasKey(t => t.DocumentTypeID);

            // Properties
            this.Property(t => t.DocumentTypeName)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Sys00007");
            this.Property(t => t.DocumentTypeID).HasColumnName("DocumentTypeID");
            this.Property(t => t.ContactSourceID).HasColumnName("ContactSourceID");
            this.Property(t => t.DocumentTypeName).HasColumnName("DocumentTypeName");

            // Relationships
            this.HasRequired(t => t.Sys00005)
                .WithMany(t => t.Sys00007)
                .HasForeignKey(d => d.ContactSourceID);

        }
    }
}
