using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.CMS.Mapping
{
    public class CM00027Map : EntityTypeConfiguration<CM00027>
    {
        public CM00027Map()
        {
            // Primary Key
            this.HasKey(t => t.ChangeStatusSourceID);

            // Properties
            this.Property(t => t.ChangeStatusSourceID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.ChangeStatusSourceName)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("CM00027");
            this.Property(t => t.ChangeStatusSourceID).HasColumnName("ChangeStatusSourceID");
            this.Property(t => t.ChangeStatusSourceName).HasColumnName("ChangeStatusSourceName");
        }
    }
}
