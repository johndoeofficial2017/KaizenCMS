using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.SOP.Mapping
{
    public class SOP00013Map : EntityTypeConfiguration<SOP00013>
    {
        public SOP00013Map()
        {
            // Primary Key
            this.HasKey(t => t.StatementCycleID);

            // Properties
            this.Property(t => t.StatementCycleID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.StatementCycleName)
                .HasMaxLength(12);

            // Table & Column Mappings
            this.ToTable("SOP00013");
            this.Property(t => t.StatementCycleID).HasColumnName("StatementCycleID");
            this.Property(t => t.StatementCycleName).HasColumnName("StatementCycleName");
        }
    }
}
