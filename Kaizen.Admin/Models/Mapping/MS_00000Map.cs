using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.Admin.Mapping
{
    public class MS_00000Map : EntityTypeConfiguration<MS_00000>
    {
        public MS_00000Map()
        {
            // Primary Key
            this.HasKey(t => t.MsgTypeID);

            // Properties
            this.Property(t => t.MsgTypeID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.MsgTypeName)
                .HasMaxLength(20);

            // Table & Column Mappings
            this.ToTable("MS_00000");
            this.Property(t => t.MsgTypeID).HasColumnName("MsgTypeID");
            this.Property(t => t.MsgTypeName).HasColumnName("MsgTypeName");
        }
    }
}
