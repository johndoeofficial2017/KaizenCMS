using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.CMS.Mapping
{
    public class CM00059Map : EntityTypeConfiguration<CM00059>
    {
        public CM00059Map()
        {
            // Primary Key
            this.HasKey(t => t.CaseStatusTypeID);

            // Properties
            this.Property(t => t.CaseStatusTypeID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.CaseStatusTypeName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("CM00059");
            this.Property(t => t.CaseStatusTypeID).HasColumnName("CaseStatusTypeID");
            this.Property(t => t.CaseStatusTypeName).HasColumnName("CaseStatusTypeName");
        }
    }
}
