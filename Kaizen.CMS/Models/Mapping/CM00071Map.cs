using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.CMS.Mapping
{
    public class CM00071Map : EntityTypeConfiguration<CM00071>
    {
        public CM00071Map()
        {
            // Primary Key
            this.HasKey(t => t.ViewID);

            // Properties
            this.Property(t => t.ViewName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.ViewDescription)
                .HasMaxLength(100);

            this.Property(t => t.WhereCondition)
                .HasMaxLength(1000);

            this.Property(t => t.WhereCustomCondition)
                .HasMaxLength(1000);

            this.Property(t => t.TableSource)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("CM00071");
            this.Property(t => t.ViewID).HasColumnName("ViewID");
            this.Property(t => t.TRXTypeID).HasColumnName("TRXTypeID");
            this.Property(t => t.ViewName).HasColumnName("ViewName");
            this.Property(t => t.ViewDescription).HasColumnName("ViewDescription");
            this.Property(t => t.IsEmailable).HasColumnName("IsEmailable");
            this.Property(t => t.IsPrintable).HasColumnName("IsPrintable");
            this.Property(t => t.IsPivotTable).HasColumnName("IsPivotTable");
            this.Property(t => t.IsGraph).HasColumnName("IsGraph");
            this.Property(t => t.IsSummery).HasColumnName("IsSummery");
            this.Property(t => t.IsDefault).HasColumnName("IsDefault");
            this.Property(t => t.IsReminder).HasColumnName("IsReminder");
            this.Property(t => t.WhereCondition).HasColumnName("WhereCondition");
            this.Property(t => t.WhereCustomCondition).HasColumnName("WhereCustomCondition");
            this.Property(t => t.ViewSortCondition).HasColumnName("ViewSortCondition");
            this.Property(t => t.TableSource).HasColumnName("TableSource");

            // Relationships
            this.HasRequired(t => t.CM00029)
                .WithMany(t => t.CM00071)
                .HasForeignKey(d => d.TRXTypeID);

        }
    }
}
