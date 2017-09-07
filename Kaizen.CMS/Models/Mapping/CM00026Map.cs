using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.CMS.Mapping
{
    public class CM00026Map : EntityTypeConfiguration<CM00026>
    {
        public CM00026Map()
        {
            // Primary Key
            this.HasKey(t => t.StatusScriptID);

            // Properties
            this.Property(t => t.StatusScriptName)
                .IsRequired()
                .HasMaxLength(25);

            this.Property(t => t.StatusScriptMain)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("CM00026");
            this.Property(t => t.StatusScriptID).HasColumnName("StatusScriptID");
            this.Property(t => t.CaseStatusID).HasColumnName("CaseStatusID");
            this.Property(t => t.StatusScriptName).HasColumnName("StatusScriptName");
            this.Property(t => t.StatusScriptMain).HasColumnName("StatusScriptMain");

            // Relationships
            this.HasRequired(t => t.CM00700)
                .WithMany(t => t.CM00026)
                .HasForeignKey(d => d.CaseStatusID);

        }
    }
}
