using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.SOP.Mapping
{
    public class SOP00501Map : EntityTypeConfiguration<SOP00501>
    {
        public SOP00501Map()
        {
            // Primary Key
            this.HasKey(t => t.BatchID);

            // Properties
            this.Property(t => t.BatchID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.BatchDescription)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("SOP00501");
            this.Property(t => t.BatchID).HasColumnName("BatchID");
            this.Property(t => t.BatchAmount).HasColumnName("BatchAmount");
            this.Property(t => t.BatchDescription).HasColumnName("BatchDescription");
            this.Property(t => t.BatchTRXcount).HasColumnName("BatchTRXcount");
            this.Property(t => t.IsTransactionDate).HasColumnName("IsTransactionDate");
            this.Property(t => t.PostingDate).HasColumnName("PostingDate");
        }
    }
}
