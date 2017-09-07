using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.CMS.Mapping
{
    public class CM00001Map : EntityTypeConfiguration<CM00001>
    {
        public CM00001Map()
        {
            // Primary Key
            this.HasKey(t => t.OperatorID);

            // Properties
            this.Property(t => t.OperatorID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.OperatorNme)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("CM00001");
            this.Property(t => t.OperatorID).HasColumnName("OperatorID");
            this.Property(t => t.OperatorNme).HasColumnName("OperatorNme");
        }
    }
}
