using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.CMS.Mapping
{
    public class CM00053Map : EntityTypeConfiguration<CM00053>
    {
        public CM00053Map()
        {
            // Primary Key
            this.HasKey(t => new { t.StatusActionTypeID, t.UserName });

            // Properties
            this.Property(t => t.StatusActionTypeID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.UserName)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("CM00053");
            this.Property(t => t.StatusActionTypeID).HasColumnName("StatusActionTypeID");
            this.Property(t => t.UserName).HasColumnName("UserName");

            // Relationships
            this.HasRequired(t => t.CM00003)
                .WithMany(t => t.CM00053)
                .HasForeignKey(d => d.StatusActionTypeID);

        }
    }
}
