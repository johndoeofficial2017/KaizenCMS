using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.Configuration.Mapping
{
    public class WorkstationMap : EntityTypeConfiguration<Workstation>
    {
        public WorkstationMap()
        {
            // Primary Key
            this.HasKey(t => t.WorkstationID);

            // Properties
            this.Property(t => t.WorkstationName)
                .HasMaxLength(200);

            this.Property(t => t.WorkstationAddress)
                .HasMaxLength(200);

            this.Property(t => t.WorkstationCpuID)
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("Workstation");
            this.Property(t => t.WorkstationID).HasColumnName("WorkstationID");
            this.Property(t => t.WorkstationName).HasColumnName("WorkstationName");
            this.Property(t => t.WorkstationAddress).HasColumnName("WorkstationAddress");
            this.Property(t => t.WorkstationCpuID).HasColumnName("WorkstationCpuID");
            this.Property(t => t.WorkstationIsActive).HasColumnName("WorkstationIsActive");
            this.Property(t => t.RegisterDate).HasColumnName("RegisterDate");
        }
    }
}
