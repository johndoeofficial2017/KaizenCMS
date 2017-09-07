using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.Admin.Mapping
{
    public class CPR00001Map : EntityTypeConfiguration<CPR00001>
    {
        public CPR00001Map()
        {
            // Primary Key
            this.HasKey(t => t.RecordTypeID);

            // Properties
            this.Property(t => t.RecordTypeID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.RecordTypeName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("CPR00001");
            this.Property(t => t.RecordTypeID).HasColumnName("RecordTypeID");
            this.Property(t => t.RecordTypeName).HasColumnName("RecordTypeName");
        }
    }
}
