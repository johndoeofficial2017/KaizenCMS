using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.CMS.Mapping
{
    public class CM00037Map : EntityTypeConfiguration<CM00037>
    {
        public CM00037Map()
        {
            // Primary Key
            this.HasKey(t => t.TableSource);

            // Properties
            this.Property(t => t.TableSource)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.SourceTypeName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("CM00037");
            this.Property(t => t.TableSource).HasColumnName("TableSource");
            this.Property(t => t.SourceTypeName).HasColumnName("SourceTypeName");
            this.Property(t => t.IsCustomTable).HasColumnName("IsCustomTable");
        }
    }
}
