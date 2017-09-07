using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.SOP.Mapping
{
    public class Sys00006Map : EntityTypeConfiguration<Sys00006>
    {
        public Sys00006Map()
        {
            // Primary Key
            this.HasKey(t => t.ContactTypeID);

            // Properties
            this.Property(t => t.ContactTypeID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.ContactTypeName)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Sys00006");
            this.Property(t => t.ContactTypeID).HasColumnName("ContactTypeID");
            this.Property(t => t.ContactTypeName).HasColumnName("ContactTypeName");
            this.Property(t => t.ContactSourceID).HasColumnName("ContactSourceID");

            // Relationships
            this.HasRequired(t => t.Sys00005)
                .WithMany(t => t.Sys00006)
                .HasForeignKey(d => d.ContactSourceID);

        }
    }
}
