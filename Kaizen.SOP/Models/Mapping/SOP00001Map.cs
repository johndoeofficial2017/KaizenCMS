using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.SOP.Mapping
{
    public class SOP00001Map : EntityTypeConfiguration<SOP00001>
    {
        public SOP00001Map()
        {
            // Primary Key
            this.HasKey(t => new { t.SOPTypeSetupID, t.SOPTYPE });

            // Properties
            this.Property(t => t.SOPTypeSetupID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.SOPTypeSetupName)
                .HasMaxLength(50);

            this.Property(t => t.NextNumberPrefix)
                .IsRequired()
                .HasMaxLength(5);

            this.Property(t => t.SOPTypeSetupBackOrder)
                .IsFixedLength()
                .HasMaxLength(21);

            this.Property(t => t.SOPTypeSetupFulfillment)
                .IsFixedLength()
                .HasMaxLength(21);

            this.Property(t => t.SOPTypeSetupInvoice)
                .IsFixedLength()
                .HasMaxLength(21);

            // Table & Column Mappings
            this.ToTable("SOP00001");
            this.Property(t => t.SOPTypeSetupID).HasColumnName("SOPTypeSetupID");
            this.Property(t => t.SOPTypeSetupName).HasColumnName("SOPTypeSetupName");
            this.Property(t => t.SOPTYPE).HasColumnName("SOPTYPE");
            this.Property(t => t.NextDocumentNumber).HasColumnName("NextDocumentNumber");
            this.Property(t => t.NextNumberPrefix).HasColumnName("NextNumberPrefix");
            this.Property(t => t.NextNumberlenth).HasColumnName("NextNumberlenth");
            this.Property(t => t.UseProspect).HasColumnName("UseProspect");
            this.Property(t => t.IsRepeatable).HasColumnName("IsRepeatable");
            this.Property(t => t.IsOverride).HasColumnName("IsOverride");
            this.Property(t => t.SOPTypeSetupBackOrder).HasColumnName("SOPTypeSetupBackOrder");
            this.Property(t => t.SOPTypeSetupFulfillment).HasColumnName("SOPTypeSetupFulfillment");
            this.Property(t => t.SOPTypeSetupInvoice).HasColumnName("SOPTypeSetupInvoice");

            // Relationships
            this.HasRequired(t => t.SOP00000)
                .WithMany(t => t.SOP00001)
                .HasForeignKey(d => d.SOPTYPE);

        }
    }
}
