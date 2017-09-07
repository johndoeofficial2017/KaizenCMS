using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.Configuration.Mapping
{
    public class Kaizen00007Map : EntityTypeConfiguration<Kaizen00007>
    {
        public Kaizen00007Map()
        {
            // Primary Key
            this.HasKey(t => t.EXT_ScreenID);

            // Properties
            this.Property(t => t.ScreenName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.ScreenDescription)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("Kaizen00007");
            this.Property(t => t.EXT_ScreenID).HasColumnName("EXT_ScreenID");
            this.Property(t => t.ScreenID).HasColumnName("ScreenID");
            this.Property(t => t.ScreenName).HasColumnName("ScreenName");
            this.Property(t => t.ScreenDescription).HasColumnName("ScreenDescription");

            // Relationships
            this.HasRequired(t => t.Kaizen00010)
                .WithMany(t => t.Kaizen00007)
                .HasForeignKey(d => d.ScreenID);

        }
    }
}
