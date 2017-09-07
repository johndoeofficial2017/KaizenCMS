using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.Configuration.Mapping
{
    public class Kaizen00000Map : EntityTypeConfiguration<Kaizen00000>
    {
        public Kaizen00000Map()
        {
            // Primary Key
            this.HasKey(t => t.languageID);

            // Properties
            this.Property(t => t.languageName)
                .IsRequired()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("Kaizen00000");
            this.Property(t => t.languageID).HasColumnName("languageID");
            this.Property(t => t.languageName).HasColumnName("languageName");
            this.Property(t => t.IsLeft).HasColumnName("IsLeft");
        }
    }
}
