using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.CMS.Mapping
{
    public class CM00041Map : EntityTypeConfiguration<CM00041>
    {
        public CM00041Map()
        {
            // Primary Key
            this.HasKey(t => t.TableRuleID);

            // Properties
            this.Property(t => t.TableRuleID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.TableRuleName)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("CM00041");
            this.Property(t => t.TableRuleID).HasColumnName("TableRuleID");
            this.Property(t => t.TableRuleName).HasColumnName("TableRuleName");
        }
    }
}
