using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.Configuration.Mapping
{
    public class Config00100Map : EntityTypeConfiguration<Config00100>
    {
        public Config00100Map()
        {
            // Primary Key
            this.HasKey(t => t.CompanyID);

            // Properties
            this.Property(t => t.CompanyID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("Config00100");
            this.Property(t => t.CompanyID).HasColumnName("CompanyID");
            this.Property(t => t.IsAutoItemID).HasColumnName("IsAutoItemID");
            this.Property(t => t.IsAutoItemIDByCat).HasColumnName("IsAutoItemIDByCat");

            // Relationships
            this.HasRequired(t => t.Company)
                .WithOptional(t => t.Config00100);

        }
    }
}
