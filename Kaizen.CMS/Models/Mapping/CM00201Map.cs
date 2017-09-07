using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.CMS.Mapping
{
    public class CM00201Map : EntityTypeConfiguration<CM00201>
    {
        public CM00201Map()
        {
            // Primary Key
            this.HasKey(t => t.DocumentID);

            // Properties
            this.Property(t => t.DocumentID)
                .IsRequired()
                .HasMaxLength(40);

            this.Property(t => t.ContractID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.DocumentName)
                .IsRequired()
                .HasMaxLength(10);

            this.Property(t => t.DocumentDescription)
                .HasMaxLength(50);

            this.Property(t => t.PhotoExtension)
                .HasMaxLength(5);

            // Table & Column Mappings
            this.ToTable("CM00201");
            this.Property(t => t.DocumentID).HasColumnName("DocumentID");
            this.Property(t => t.ContractID).HasColumnName("ContractID");
            this.Property(t => t.DocumentName).HasColumnName("DocumentName");
            this.Property(t => t.DocumentDescription).HasColumnName("DocumentDescription");
            this.Property(t => t.PhotoExtension).HasColumnName("PhotoExtension");
        }
    }
}
