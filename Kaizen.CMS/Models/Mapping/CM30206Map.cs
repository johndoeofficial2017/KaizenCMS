using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.CMS.Mapping
{
    public class CM30206Map : EntityTypeConfiguration<CM30206>
    {
        public CM30206Map()
        {
            // Primary Key
            this.HasKey(t => t.AssignHistoryID);

            // Properties
            this.Property(t => t.AssignHistoryID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.AgentID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("CM30206");
            this.Property(t => t.AssignHistoryID).HasColumnName("AssignHistoryID");
            this.Property(t => t.TrxID).HasColumnName("TrxID");
            this.Property(t => t.AgentID).HasColumnName("AgentID");
            this.Property(t => t.AssignHistoryDate).HasColumnName("AssignHistoryDate");
        }
    }
}
