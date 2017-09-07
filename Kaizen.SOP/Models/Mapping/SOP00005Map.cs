using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.SOP.Mapping
{
    public class SOP00005Map : EntityTypeConfiguration<SOP00005>
    {
        public SOP00005Map()
        {
            // Primary Key
            this.HasKey(t => t.SalesPersonID);

            // Properties
            this.Property(t => t.SalesPersonID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.UserCode)
                .HasMaxLength(5);

            this.Property(t => t.SiteID)
                .IsFixedLength()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("SOP00005");
            this.Property(t => t.SalesPersonID).HasColumnName("SalesPersonID");
            this.Property(t => t.UserCode).HasColumnName("UserCode");
            this.Property(t => t.SOPTYPE).HasColumnName("SOPTYPE");
            this.Property(t => t.SiteID).HasColumnName("SiteID");

            // Relationships
            this.HasRequired(t => t.SOP00002)
                .WithOptional(t => t.SOP00005);

        }
    }
}
