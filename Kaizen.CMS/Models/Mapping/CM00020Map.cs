using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.CMS.Mapping
{
    public class CM00020Map : EntityTypeConfiguration<CM00020>
    {
        public CM00020Map()
        {
            // Primary Key
            this.HasKey(t => new { t.PaymentLayerID, t.ContractID });

            // Properties
            this.Property(t => t.PaymentLayerID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.ContractID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("CM00020");
            this.Property(t => t.PaymentLayerID).HasColumnName("PaymentLayerID");
            this.Property(t => t.ContractID).HasColumnName("ContractID");
            this.Property(t => t.LayerFrom).HasColumnName("LayerFrom");
            this.Property(t => t.LayerTo).HasColumnName("LayerTo");
            this.Property(t => t.LayerRate).HasColumnName("LayerRate");
        }
    }
}
