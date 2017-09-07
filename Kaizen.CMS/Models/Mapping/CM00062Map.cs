using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.CMS.Mapping
{
    public class CM00062Map : EntityTypeConfiguration<CM00062>
    {
        public CM00062Map()
        {
            // Primary Key
            this.HasKey(t => t.ViewID);

            // Properties
            this.Property(t => t.ViewName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("CM00062");
            this.Property(t => t.ViewID).HasColumnName("ViewID");
            this.Property(t => t.CaseStatusID).HasColumnName("CaseStatusID");
            this.Property(t => t.ViewName).HasColumnName("ViewName");

            // Relationships
            this.HasRequired(t => t.CM00700)
                .WithMany(t => t.CM00062)
                .HasForeignKey(d => d.CaseStatusID);

        }
    }
}
