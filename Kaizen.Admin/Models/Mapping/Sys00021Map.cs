using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.Admin.Mapping
{
    public class Sys00021Map : EntityTypeConfiguration<Sys00021>
    {
        public Sys00021Map()
        {
            // Primary Key
            this.HasKey(t => t.TransactionType);

            // Properties
            this.Property(t => t.TransactionType)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.TransactionTypeName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Sys00021");
            this.Property(t => t.TransactionType).HasColumnName("TransactionType");
            this.Property(t => t.TransactionTypeName).HasColumnName("TransactionTypeName");
        }
    }
}
