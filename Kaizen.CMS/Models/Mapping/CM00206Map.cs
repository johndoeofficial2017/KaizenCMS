using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.CMS.Mapping
{
    public class CM00206Map : EntityTypeConfiguration<CM00206>
    {
        public CM00206Map()
        {
            // Primary Key
            this.HasKey(t => t.AssignLineID);

            // Properties
            this.Property(t => t.AssigmentID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.CaseRef)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.AgentID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("CM00206");
            this.Property(t => t.AssignLineID).HasColumnName("AssignLineID");
            this.Property(t => t.AssigmentID).HasColumnName("AssigmentID");
            this.Property(t => t.CaseRef).HasColumnName("CaseRef");
            this.Property(t => t.AgentID).HasColumnName("AgentID");
            this.Property(t => t.AssignHistoryDate).HasColumnName("AssignHistoryDate");

            // Relationships
            this.HasRequired(t => t.CM00202)
                .WithMany(t => t.CM00206)
                .HasForeignKey(d => d.AssigmentID);

        }
    }
}
