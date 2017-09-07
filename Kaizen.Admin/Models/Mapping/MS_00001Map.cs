using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.Admin.Mapping
{
    public class MS_00001Map : EntityTypeConfiguration<MS_00001>
    {
        public MS_00001Map()
        {
            // Primary Key
            this.HasKey(t => t.MsgSourceID);

            // Properties
            this.Property(t => t.MsgSourceID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.MsgSourceName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("MS_00001");
            this.Property(t => t.MsgSourceID).HasColumnName("MsgSourceID");
            this.Property(t => t.MsgSourceName).HasColumnName("MsgSourceName");
        }
    }
}
