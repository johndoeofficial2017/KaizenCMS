using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.CMS.Mapping
{
    public class CM00047Map : EntityTypeConfiguration<CM00047>
    {
        public CM00047Map()
        {
            // Primary Key
            this.HasKey(t => t.SummeryTypeID);

            // Properties
            this.Property(t => t.SummeryTypeID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.SummeryTypeName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("CM00047");
            this.Property(t => t.SummeryTypeID).HasColumnName("SummeryTypeID");
            this.Property(t => t.SummeryTypeName).HasColumnName("SummeryTypeName");
        }
    }
}
