using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.CMS.Mapping
{
    public class CM00064Map : EntityTypeConfiguration<CM00064>
    {
        public CM00064Map()
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
            this.ToTable("CM00064");
            this.Property(t => t.ViewID).HasColumnName("ViewID");
            this.Property(t => t.UserName).HasColumnName("UserName");

            // Relationships
            this.HasRequired(t => t.CM00062)
                .WithMany(t => t.CM00064)
                .HasForeignKey(d => d.ViewID);

        }
    }
}
