using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.CMS.Mapping
{
    public class CM00057Map : EntityTypeConfiguration<CM00057>
    {
        public CM00057Map()
        {
            // Primary Key
            this.HasKey(t => new { t.TRXTypeID, t.UserName });

            // Properties
            this.Property(t => t.TRXTypeID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.UserName)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("CM00057");
            this.Property(t => t.TRXTypeID).HasColumnName("TRXTypeID");
            this.Property(t => t.UserName).HasColumnName("UserName");

            // Relationships
            this.HasRequired(t => t.CM00029)
                .WithMany(t => t.CM00057)
                .HasForeignKey(d => d.TRXTypeID);

        }
    }
}
