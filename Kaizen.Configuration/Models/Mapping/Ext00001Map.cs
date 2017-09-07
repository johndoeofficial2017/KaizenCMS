using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.Configuration.Mapping
{
    public class Ext00001Map : EntityTypeConfiguration<Ext00001>
    {
        public Ext00001Map()
        {
            // Primary Key
            this.HasKey(t => t.DataBaseSourceID);

            // Properties
            this.Property(t => t.DataBaseSourceID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.CompanyUserName)
                .IsFixedLength()
                .HasMaxLength(20);

            this.Property(t => t.CompanyName)
                .HasMaxLength(20);

            this.Property(t => t.CompanyPassword)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Ext00001");
            this.Property(t => t.DataBaseSourceID).HasColumnName("DataBaseSourceID");
            this.Property(t => t.CompanyUserName).HasColumnName("CompanyUserName");
            this.Property(t => t.CompanyName).HasColumnName("CompanyName");
            this.Property(t => t.CompanyPassword).HasColumnName("CompanyPassword");
        }
    }
}
