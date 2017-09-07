using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.CMS.Mapping
{
    public class CM00220Map : EntityTypeConfiguration<CM00220>
    {
        public CM00220Map()
        {
            // Primary Key
            this.HasKey(t => t.TrxID);

            // Properties
            this.Property(t => t.TrxID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.AgentID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.AgentIDTO)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.UserName)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.TrxComments)
                .HasMaxLength(500);

            // Table & Column Mappings
            this.ToTable("CM00220");
            this.Property(t => t.TrxID).HasColumnName("TrxID");
            this.Property(t => t.AgentID).HasColumnName("AgentID");
            this.Property(t => t.AgentIDTO).HasColumnName("AgentIDTO");
            this.Property(t => t.StartDate).HasColumnName("StartDate");
            this.Property(t => t.EndDate).HasColumnName("EndDate");
            this.Property(t => t.UserName).HasColumnName("UserName");
            this.Property(t => t.TrxComments).HasColumnName("TrxComments");

            // Relationships
            this.HasRequired(t => t.CM00105)
                .WithMany(t => t.CM00220)
                .HasForeignKey(d => d.AgentID);

        }
    }
}
