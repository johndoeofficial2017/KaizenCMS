using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.SOP.Mapping
{
    public class Sys00005Map : EntityTypeConfiguration<Sys00005>
    {
        public Sys00005Map()
        {
            // Primary Key
            this.HasKey(t => t.ContactSourceID);

            // Properties
            this.Property(t => t.ContactSourceID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.ContactSourceName)
                .IsRequired()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("Sys00005");
            this.Property(t => t.ContactSourceID).HasColumnName("ContactSourceID");
            this.Property(t => t.ContactSourceName).HasColumnName("ContactSourceName");
        }
    }
}
