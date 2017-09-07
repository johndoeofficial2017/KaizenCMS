using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.Configuration.Mapping
{
    public class Kaizen00010Map : EntityTypeConfiguration<Kaizen00010>
    {
        public Kaizen00010Map()
        {
            // Primary Key
            this.HasKey(t => t.ScreenID);

            // Properties
            this.Property(t => t.ScreenID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.ScreenName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.ScreenDescription)
                .HasMaxLength(100);

            this.Property(t => t.MainTable)
                .HasMaxLength(15);

            // Table & Column Mappings
            this.ToTable("Kaizen00010");
            this.Property(t => t.ScreenID).HasColumnName("ScreenID");
            this.Property(t => t.ModuleID).HasColumnName("ModuleID");
            this.Property(t => t.ScreenName).HasColumnName("ScreenName");
            this.Property(t => t.ScreenDescription).HasColumnName("ScreenDescription");
            this.Property(t => t.HasSubScreen).HasColumnName("HasSubScreen");
            this.Property(t => t.MainTable).HasColumnName("MainTable");

            // Relationships
            this.HasOptional(t => t.Kaizen000)
                .WithMany(t => t.Kaizen00010)
                .HasForeignKey(d => d.ModuleID);

        }
    }
}
