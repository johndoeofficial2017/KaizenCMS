using System.Data.Entity.ModelConfiguration;

namespace Kaizen.SOP.Mapping
{
    public class Sys00003Map : EntityTypeConfiguration<Sys00003>
    {
        public Sys00003Map()
        {
            // Primary Key
            this.HasKey(t => t.ModuleID);

            // Properties
            this.Property(t => t.ModuleName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Sys00003");
            this.Property(t => t.ModuleID).HasColumnName("ModuleID");
            this.Property(t => t.ModuleName).HasColumnName("ModuleName");
        }
    }
}
