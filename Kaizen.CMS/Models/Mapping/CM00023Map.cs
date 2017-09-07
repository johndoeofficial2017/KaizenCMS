using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.CMS.Mapping
{
    public class CM00023Map : EntityTypeConfiguration<CM00023>
    {
        public CM00023Map()
        {
            // Primary Key
            this.HasKey(t => t.AgentGroup);

            // Properties
            this.Property(t => t.AgentGroup)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.AgentGroupName)
                .HasMaxLength(30);

            this.Property(t => t.NextDocumentNumber)
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.NextNumberPrefix)
                .HasMaxLength(5);

            // Table & Column Mappings
            this.ToTable("CM00023");
            this.Property(t => t.AgentGroup).HasColumnName("AgentGroup");
            this.Property(t => t.AgentGroupName).HasColumnName("AgentGroupName");
            this.Property(t => t.NextDocumentNumber).HasColumnName("NextDocumentNumber");
            this.Property(t => t.NextNumberPrefix).HasColumnName("NextNumberPrefix");
            this.Property(t => t.NextNumberlenth).HasColumnName("NextNumberlenth");
        }
    }
}
