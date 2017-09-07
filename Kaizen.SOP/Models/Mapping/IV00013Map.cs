using System.Data.Entity.ModelConfiguration;

namespace Kaizen.SOP.Mapping
{
    public class IV00013Map : EntityTypeConfiguration<IV00013>
    {
        public IV00013Map()
        {
            // Primary Key
            this.HasKey(t => t.CollCode);

            // Properties
            this.Property(t => t.CollCode)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(15);

            this.Property(t => t.CollName)
                .HasMaxLength(50);

            this.Property(t => t.LotNumber)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.LookupFrom)
                .IsFixedLength()
                .HasMaxLength(15);

            this.Property(t => t.LookupLotNumber)
                .IsFixedLength()
                .HasMaxLength(15);

            this.Property(t => t.LookupLotName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("IV00013");
            this.Property(t => t.CollCode).HasColumnName("CollCode");
            this.Property(t => t.CollName).HasColumnName("CollName");
            this.Property(t => t.LotNumber).HasColumnName("LotNumber");
            this.Property(t => t.CollType).HasColumnName("CollType");
            this.Property(t => t.LookupFrom).HasColumnName("LookupFrom");
            this.Property(t => t.LookupLotNumber).HasColumnName("LookupLotNumber");
            this.Property(t => t.LookupLotName).HasColumnName("LookupLotName");

        }
    }
}
