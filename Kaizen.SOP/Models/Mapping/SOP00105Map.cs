using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.SOP.Mapping
{
    public class SOP00105Map : EntityTypeConfiguration<SOP00105>
    {
        public SOP00105Map()
        {
            // Primary Key
            this.HasKey(t => new { t.ContactTypeID, t.CUSTNMBR });

            // Properties
            this.Property(t => t.ContactTypeID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.CUSTNMBR)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.ContactPerson)
                .HasMaxLength(50);

            this.Property(t => t.PersonPosition)
                .HasMaxLength(50);

            this.Property(t => t.PersonEmailAdd)
                .HasMaxLength(50);

            this.Property(t => t.OtherInfo)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("SOP00105");
            this.Property(t => t.ContactTypeID).HasColumnName("ContactTypeID");
            this.Property(t => t.CUSTNMBR).HasColumnName("CUSTNMBR");
            this.Property(t => t.ContactPerson).HasColumnName("ContactPerson");
            this.Property(t => t.PersonPosition).HasColumnName("PersonPosition");
            this.Property(t => t.PersonEmailAdd).HasColumnName("PersonEmailAdd");
            this.Property(t => t.OtherInfo).HasColumnName("OtherInfo");

            // Relationships
            this.HasRequired(t => t.SOP00100)
                .WithMany(t => t.SOP00105)
                .HasForeignKey(d => d.CUSTNMBR);
            this.HasRequired(t => t.Sys00006)
                .WithMany(t => t.SOP00105)
                .HasForeignKey(d => d.ContactTypeID);

        }
    }
}
