using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.CMS.Mapping
{
    public class CM10107Map : EntityTypeConfiguration<CM10107>
    {
        public CM10107Map()
        {
            // Primary Key
            this.HasKey(t => t.LineID);

            // Properties
            this.Property(t => t.CaseRef)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(20);

            this.Property(t => t.AgentID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.CIFNumber)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(20);

            this.Property(t => t.TargetID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("CM10107");
            this.Property(t => t.LineID).HasColumnName("LineID");
            this.Property(t => t.CaseRef).HasColumnName("CaseRef");
            this.Property(t => t.TRXTypeID).HasColumnName("TRXTypeID");
            this.Property(t => t.AgentID).HasColumnName("AgentID");
            this.Property(t => t.CIFNumber).HasColumnName("CIFNumber");
            this.Property(t => t.TargetID).HasColumnName("TargetID");
            this.Property(t => t.ClaimAmount).HasColumnName("ClaimAmount");
            this.Property(t => t.OutstandingAMT).HasColumnName("OutstandingAMT");
        }
    }
}
