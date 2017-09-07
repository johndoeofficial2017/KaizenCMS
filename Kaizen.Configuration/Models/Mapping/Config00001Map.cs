using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.Configuration.Mapping
{
    public class Config00001Map : EntityTypeConfiguration<Config00001>
    {
        public Config00001Map()
        {
            // Primary Key
            this.HasKey(t => t.CompanyID);

            // Properties
            this.Property(t => t.CompanyID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.PrefixValue)
                .HasMaxLength(5);

            // Table & Column Mappings
            this.ToTable("Config00001");
            this.Property(t => t.CompanyID).HasColumnName("CompanyID");
            this.Property(t => t.IsAutoItemID).HasColumnName("IsAutoItemID");
            this.Property(t => t.IsAutoItemIDByCat).HasColumnName("IsAutoItemIDByCat");
            this.Property(t => t.Prefixlengh).HasColumnName("Prefixlengh");
            this.Property(t => t.PrefixValue).HasColumnName("PrefixValue");
            this.Property(t => t.LastEmployeeID).HasColumnName("LastEmployeeID");
        }
    }
}
