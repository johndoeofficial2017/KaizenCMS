using System.Data.Entity.ModelConfiguration;

namespace Kaizen.SOP.Mapping
{
    public class IV00014Map : EntityTypeConfiguration<IV00014>
    {
        public IV00014Map()
        {
            // Primary Key
            this.HasKey(t => t.ListID);

            // Properties
            this.Property(t => t.ListName)
                .HasMaxLength(50);

            this.Property(t => t.CollCode)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(15);

            // Table & Column Mappings
            this.ToTable("IV00014");
            this.Property(t => t.ListID).HasColumnName("ListID");
            this.Property(t => t.ListName).HasColumnName("ListName");
            this.Property(t => t.CollCode).HasColumnName("CollCode");

            // Relationships
            this.HasRequired(t => t.IV00013)
                .WithMany(t => t.IV00014)
                .HasForeignKey(d => d.CollCode);

        }
    }
}
