using System.Data.Entity.ModelConfiguration;

namespace Kaizen.SOP.Mapping
{
    public class UPR00030Map : EntityTypeConfiguration<UPR00030>
    {
        public UPR00030Map()
        {
            // Primary Key
            this.HasKey(t => t.NationalityID);

            // Properties
            this.Property(t => t.NationalityID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.NationalityName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("UPR00030");
            this.Property(t => t.NationalityID).HasColumnName("NationalityID");
            this.Property(t => t.NationalityName).HasColumnName("NationalityName");
        }
    }
}
