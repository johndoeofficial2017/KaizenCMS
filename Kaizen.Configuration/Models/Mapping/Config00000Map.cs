using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.Configuration.Mapping
{
    public class Config00000Map : EntityTypeConfiguration<Config00000>
    {
        public Config00000Map()
        {
            // Primary Key
            this.HasKey(t => t.TableID);

            // Properties
            this.Property(t => t.TableID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("Config00000");
            this.Property(t => t.TableID).HasColumnName("TableID");
            this.Property(t => t.IsExpiryDate).HasColumnName("IsExpiryDate");
            this.Property(t => t.ExpiryMonthDate).HasColumnName("ExpiryMonthDate");
            this.Property(t => t.ExpirtyPeriodDays).HasColumnName("ExpirtyPeriodDays");
        }
    }
}
