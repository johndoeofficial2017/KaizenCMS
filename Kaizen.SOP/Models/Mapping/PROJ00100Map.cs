using System.Data.Entity.ModelConfiguration;

namespace Kaizen.SOP.Mapping
{
    public class PROJ00100Map : EntityTypeConfiguration<PROJ00100>
    {
        public PROJ00100Map()
        {
            // Primary Key
            this.HasKey(t => t.ProjectID);

            // Properties
            this.Property(t => t.ProjectID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.ProjectName)
                .HasMaxLength(20);

            this.Property(t => t.ProjectDescription)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("PROJ00100");
            this.Property(t => t.ProjectID).HasColumnName("ProjectID");
            this.Property(t => t.ProjectName).HasColumnName("ProjectName");
            this.Property(t => t.ProjectDescription).HasColumnName("ProjectDescription");
        }
    }
}
