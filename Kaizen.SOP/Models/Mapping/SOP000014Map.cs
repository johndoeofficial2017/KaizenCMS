using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.SOP.Mapping
{
    public class SOP000014Map : EntityTypeConfiguration<SOP000014>
    {
        public SOP000014Map()
        {
            // Primary Key
            this.HasKey(t => t.SetupID);

            // Properties
            this.Property(t => t.SetupID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.POSPrefix)
                .IsRequired()
                .HasMaxLength(5);

            // Table & Column Mappings
            this.ToTable("SOP000014");
            this.Property(t => t.SetupID).HasColumnName("SetupID");
            this.Property(t => t.POSPrefix).HasColumnName("POSPrefix");
            this.Property(t => t.POSLenth).HasColumnName("POSLenth");
            this.Property(t => t.POSLastNumber).HasColumnName("POSLastNumber");
        }
    }
}
