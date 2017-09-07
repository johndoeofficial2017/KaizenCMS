using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.Configuration.Mapping
{
    public class Met00006Map : EntityTypeConfiguration<Met00006>
    {
        public Met00006Map()
        {
            // Primary Key
            this.HasKey(t => t.RepeatTypeID);

            // Properties
            this.Property(t => t.RepeatTypeID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.RepeatUnit)
                .HasMaxLength(10);

            this.Property(t => t.RepeatTypeName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Met00006");
            this.Property(t => t.RepeatTypeID).HasColumnName("RepeatTypeID");
            this.Property(t => t.RepeatUnit).HasColumnName("RepeatUnit");
            this.Property(t => t.RepeatTypeName).HasColumnName("RepeatTypeName");
        }
    }
}
