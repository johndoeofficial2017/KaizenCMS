using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.CMS.Mapping
{
    public class CM00024Map : EntityTypeConfiguration<CM00024>
    {
        public CM00024Map()
        {
            // Primary Key
            this.HasKey(t => t.FunctionCode);

            // Properties
            this.Property(t => t.FunctionCode)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.FunctionDisplay)
                .IsRequired()
                .HasMaxLength(15);

            this.Property(t => t.FunctionEntryValue)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("CM00024");
            this.Property(t => t.FunctionCode).HasColumnName("FunctionCode");
            this.Property(t => t.FieldTypeID).HasColumnName("FieldTypeID");
            this.Property(t => t.FunctionDisplay).HasColumnName("FunctionDisplay");
            this.Property(t => t.FunctionEntryValue).HasColumnName("FunctionEntryValue");

            // Relationships
            this.HasRequired(t => t.CM00028)
                .WithMany(t => t.CM00024)
                .HasForeignKey(d => d.FieldTypeID);

        }
    }
}
