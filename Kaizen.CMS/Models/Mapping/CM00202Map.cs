using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.CMS.Mapping
{
    public class CM00202Map : EntityTypeConfiguration<CM00202>
    {
        public CM00202Map()
        {
            // Primary Key
            this.HasKey(t => t.AssigmentID);

            // Properties
            this.Property(t => t.AssigmentID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.UserName)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.AgentID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.AssignDescription)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("CM00202");
            this.Property(t => t.AssigmentID).HasColumnName("AssigmentID");
            this.Property(t => t.AssignDate).HasColumnName("AssignDate");
            this.Property(t => t.UserName).HasColumnName("UserName");
            this.Property(t => t.AgentID).HasColumnName("AgentID");
            this.Property(t => t.AssignDescription).HasColumnName("AssignDescription");
        }
    }
}
