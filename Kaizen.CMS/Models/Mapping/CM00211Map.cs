using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.CMS.Mapping
{
    public class CM00211Map : EntityTypeConfiguration<CM00211>
    {
        public CM00211Map()
        {
            // Primary Key
            this.HasKey(t => t.ActionID);

            // Properties
            this.Property(t => t.CaseRef)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.AgentID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.NextActionTypeID)
                .IsFixedLength()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("CM00211");
            this.Property(t => t.ActionID).HasColumnName("ActionID");
            this.Property(t => t.ActionDate).HasColumnName("ActionDate");
            this.Property(t => t.CaseRef).HasColumnName("CaseRef");
            this.Property(t => t.ActionTypeID).HasColumnName("ActionTypeID");
            this.Property(t => t.Remarks).HasColumnName("Remarks");
            this.Property(t => t.AgentID).HasColumnName("AgentID");
            this.Property(t => t.NextActionTypeID).HasColumnName("NextActionTypeID");
            this.Property(t => t.NextActionDate).HasColumnName("NextActionDate");
            this.Property(t => t.NextActionAMT).HasColumnName("NextActionAMT");

            // Relationships
        }
    }
}
