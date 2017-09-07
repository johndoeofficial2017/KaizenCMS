using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.Admin.Mapping
{
    public class MS_00201Map : EntityTypeConfiguration<MS_00201>
    {
        public MS_00201Map()
        {
            // Primary Key
            this.HasKey(t => t.LineID);

            // Properties
            this.Property(t => t.TrxID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.ReceiverEmail)
                .HasMaxLength(75);

            // Table & Column Mappings
            this.ToTable("MS_00201");
            this.Property(t => t.LineID).HasColumnName("LineID");
            this.Property(t => t.TrxID).HasColumnName("TrxID");
            this.Property(t => t.ReceiverEmail).HasColumnName("ReceiverEmail");
            this.Property(t => t.IsSent).HasColumnName("IsSent");

            // Relationships
            this.HasRequired(t => t.MS_00200)
                .WithMany(t => t.MS_00201)
                .HasForeignKey(d => d.TrxID);

        }
    }
}
