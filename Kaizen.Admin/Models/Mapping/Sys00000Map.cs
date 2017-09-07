using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.Admin.Mapping
{
    public class Sys00000Map : EntityTypeConfiguration<Sys00000>
    {
        public Sys00000Map()
        {
            // Primary Key
            this.HasKey(t => new { t.UserName, t.SequenceName });

            // Properties
            this.Property(t => t.UserName)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.SequenceName)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Sys00000");
            this.Property(t => t.UserName).HasColumnName("UserName");
            this.Property(t => t.SequenceName).HasColumnName("SequenceName");
            this.Property(t => t.KaizenID).HasColumnName("KaizenID");
        }
    }
}
