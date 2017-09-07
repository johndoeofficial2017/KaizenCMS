using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.CMS.Mapping
{
    public class CM_CaseUploadMissingAgentMap : EntityTypeConfiguration<CM_CaseUploadMissingAgent>
    {
        public CM_CaseUploadMissingAgentMap()
        {
            // Primary Key
            this.HasKey(t => t.AgentID);

            // Properties
            this.Property(t => t.AgentID)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.UserCode)
                .IsFixedLength()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("CM_CaseUploadMissingAgent");
            this.Property(t => t.AgentID).HasColumnName("AgentID");
            this.Property(t => t.UserCode).HasColumnName("UserCode");
        }
    }
}
