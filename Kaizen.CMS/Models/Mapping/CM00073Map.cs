using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.CMS.Mapping
{
    public class CM00073Map : EntityTypeConfiguration<CM00073>
    {
        public CM00073Map()
        {
            // Primary Key
            this.HasKey(t => new { t.ViewID, t.UserName });

            // Properties
            this.Property(t => t.ViewID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.UserName)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("CM00073");
            this.Property(t => t.ViewID).HasColumnName("ViewID");
            this.Property(t => t.UserName).HasColumnName("UserName");
            this.Property(t => t.IsDefault).HasColumnName("IsDefault");

            // Relationships
            this.HasRequired(t => t.CM00071)
                .WithMany(t => t.CM00073)
                .HasForeignKey(d => d.ViewID);

        }
    }
}
