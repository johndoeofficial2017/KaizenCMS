using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.Configuration.Mapping
{
    public class Kaizen007Map : EntityTypeConfiguration<Kaizen007>
    {
        public Kaizen007Map()
        {
            // Primary Key
            this.HasKey(t => new { t.RoleID, t.CompanyID, t.TRXTypeID });

            // Properties
            this.Property(t => t.RoleID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.CompanyID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.TRXTypeID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("Kaizen007");
            this.Property(t => t.RoleID).HasColumnName("RoleID");
            this.Property(t => t.CompanyID).HasColumnName("CompanyID");
            this.Property(t => t.TRXTypeID).HasColumnName("TRXTypeID");

            // Relationships

        }
    }
}
