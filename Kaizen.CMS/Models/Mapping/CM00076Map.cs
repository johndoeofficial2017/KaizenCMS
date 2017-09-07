using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.CMS.Mapping
{
    public class CM00076Map : EntityTypeConfiguration<CM00076>
    {
        public CM00076Map()
        {
            // Primary Key
            this.HasKey(t => t.GraphID);

            // Properties
            this.Property(t => t.GraphName)
                .HasMaxLength(50);

            this.Property(t => t.GraphTitle)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("CM00076");
            this.Property(t => t.GraphID).HasColumnName("GraphID");
            this.Property(t => t.ViewID).HasColumnName("ViewID");
            this.Property(t => t.GraphName).HasColumnName("GraphName");
            this.Property(t => t.GraphTypeID).HasColumnName("GraphTypeID");
            this.Property(t => t.GraphTitle).HasColumnName("GraphTitle");

            // Relationships
            this.HasRequired(t => t.CM00040)
                .WithMany(t => t.CM00076)
                .HasForeignKey(d => d.GraphTypeID);
            this.HasRequired(t => t.CM00071)
                .WithMany(t => t.CM00076)
                .HasForeignKey(d => d.ViewID);

        }
    }
}
