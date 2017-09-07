using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.CMS.Mapping
{
    public class CM00106Map : EntityTypeConfiguration<CM00106>
    {
        public CM00106Map()
        {
            // Primary Key
            this.HasKey(t => new { t.ContactTypeID, t.DebtorID });

            // Properties
            this.Property(t => t.ContactTypeID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.DebtorID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(20);

            this.Property(t => t.ContactPerson)
                .HasMaxLength(50);

            this.Property(t => t.PersonPosition)
                .HasMaxLength(50);

            this.Property(t => t.PersonEmailAdd)
                .HasMaxLength(50);

            this.Property(t => t.Pnone01)
                .HasMaxLength(300);

            this.Property(t => t.Pnone02)
                .HasMaxLength(300);

            this.Property(t => t.Ext1)
                .HasMaxLength(5);

            this.Property(t => t.Ext2)
                .HasMaxLength(5);

            this.Property(t => t.MobileNo1)
                .HasMaxLength(100);

            this.Property(t => t.MobileNo2)
                .HasMaxLength(100);

            this.Property(t => t.OtherInfo)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("CM00106");
            this.Property(t => t.ContactTypeID).HasColumnName("ContactTypeID");
            this.Property(t => t.DebtorID).HasColumnName("DebtorID");
            this.Property(t => t.ContactPerson).HasColumnName("ContactPerson");
            this.Property(t => t.PersonPosition).HasColumnName("PersonPosition");
            this.Property(t => t.PersonEmailAdd).HasColumnName("PersonEmailAdd");
            this.Property(t => t.Pnone01).HasColumnName("Pnone01");
            this.Property(t => t.Pnone02).HasColumnName("Pnone02");
            this.Property(t => t.Ext1).HasColumnName("Ext1");
            this.Property(t => t.Ext2).HasColumnName("Ext2");
            this.Property(t => t.MobileNo1).HasColumnName("MobileNo1");
            this.Property(t => t.MobileNo2).HasColumnName("MobileNo2");
            this.Property(t => t.OtherInfo).HasColumnName("OtherInfo");

            // Relationships
            this.HasRequired(t => t.CM00100)
                .WithMany(t => t.CM00106)
                .HasForeignKey(d => d.DebtorID);
        }
    }
}
