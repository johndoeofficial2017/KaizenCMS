using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.Configuration.Mapping
{
    public class Kaizen00012Map : EntityTypeConfiguration<Kaizen00012>
    {
        public Kaizen00012Map()
        {
            // Primary Key
            this.HasKey(t => t.LookUpTableNameID);

            // Properties
            this.Property(t => t.LookUpTableNameID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.LookUpTableName)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Kaizen00012");
            this.Property(t => t.LookUpTableNameID).HasColumnName("LookUpTableNameID");
            this.Property(t => t.LookUpTableName).HasColumnName("LookUpTableName");
        }
    }
}
