using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.CMS.Mapping
{
    public class CM00051Map : EntityTypeConfiguration<CM00051>
    {
        public CM00051Map()
        {
            // Primary Key
            this.HasKey(t => new { t.UserName, t.CaseStatusID });

            // Properties
            this.Property(t => t.UserName)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.CaseStatusID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("CM00051");
            this.Property(t => t.UserName).HasColumnName("UserName");
            this.Property(t => t.CaseStatusID).HasColumnName("CaseStatusID");

            // Relationships
            this.HasRequired(t => t.CM00700)
                .WithMany(t => t.CM00051)
                .HasForeignKey(d => d.CaseStatusID);

        }
    }
}
