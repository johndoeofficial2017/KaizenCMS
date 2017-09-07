using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.CMS.Mapping
{
    public class CM00019Map : EntityTypeConfiguration<CM00019>
    {
        public CM00019Map()
        {
            // Primary Key
            this.HasKey(t => t.ContractStatusID);

            // Properties
            this.Property(t => t.ContractStatusName)
                .HasMaxLength(40);

            // Table & Column Mappings
            this.ToTable("CM00019");
            this.Property(t => t.ContractStatusID).HasColumnName("ContractStatusID");
            this.Property(t => t.ContractStatusName).HasColumnName("ContractStatusName");
        }
    }
}
