using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.CMS.Mapping
{
    public class CM00205Map : EntityTypeConfiguration<CM00205>
    {
        public CM00205Map()
        {
            // Primary Key
            this.HasKey(t => t.LineID);

            // Properties
            this.Property(t => t.CaseRef)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(20);

            this.Property(t => t.AgentID)
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.PhotoExtension)
                .HasMaxLength(5);

            // Table & Column Mappings
            this.ToTable("CM00205");
            this.Property(t => t.LineID).HasColumnName("LineID");
            this.Property(t => t.CaseRef).HasColumnName("CaseRef");
            this.Property(t => t.TRXTypeID).HasColumnName("TRXTypeID");
            this.Property(t => t.AgentID).HasColumnName("AgentID");
            this.Property(t => t.PhotoExtension).HasColumnName("PhotoExtension");

            // Relationships
            this.HasRequired(t => t.CM00203)
                .WithMany(t => t.CM00205)
                .HasForeignKey(d => new { d.CaseRef, d.TRXTypeID });

        }
    }
}
