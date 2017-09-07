using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.Configuration.Mapping
{
    public class DT00103Map : EntityTypeConfiguration<DT00103>
    {
        public DT00103Map()
        {
            // Primary Key
            this.HasKey(t => new { t.SourceID, t.UserName });

            // Properties
            this.Property(t => t.SourceID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.UserName)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("DT00103");
            this.Property(t => t.SourceID).HasColumnName("SourceID");
            this.Property(t => t.UserName).HasColumnName("UserName");
            this.Property(t => t.IsDefault).HasColumnName("IsDefault");

            // Relationships
            this.HasRequired(t => t.DT00100)
                .WithMany(t => t.DT00103)
                .HasForeignKey(d => d.SourceID);

        }
    }
}
