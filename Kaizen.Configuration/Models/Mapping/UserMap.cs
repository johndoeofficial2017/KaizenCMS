using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.Configuration.Mapping
{
    public class UserMap : EntityTypeConfiguration<User>
    {
        public UserMap()
        {
            // Primary Key
            this.HasKey(t => t.UserName);

            // Properties
            this.Property(t => t.UserName)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.UserPassword)
                .IsRequired();

            this.Property(t => t.FirstName)
                .HasMaxLength(50);

            this.Property(t => t.LastName)
                .HasMaxLength(50);

            this.Property(t => t.OsUserName)
                .IsFixedLength()
                .HasMaxLength(20);

            this.Property(t => t.PhotoExtension)
                .HasMaxLength(5);

            // Table & Column Mappings
            this.ToTable("Users");
            this.Property(t => t.UserName).HasColumnName("UserName");
            this.Property(t => t.UserPassword).HasColumnName("UserPassword");
            this.Property(t => t.FirstName).HasColumnName("FirstName");
            this.Property(t => t.LastName).HasColumnName("LastName");
            this.Property(t => t.OsUserName).HasColumnName("OsUserName");
            this.Property(t => t.IsDisabled).HasColumnName("IsDisabled");
            this.Property(t => t.PhotoExtension).HasColumnName("PhotoExtension");
            this.Property(t => t.IsCustomer).HasColumnName("IsCustomer");
            this.Property(t => t.IsVendor).HasColumnName("IsVendor");
            this.Property(t => t.IsDebtor).HasColumnName("IsDebtor");
            this.Property(t => t.IsPartner).HasColumnName("IsPartner");
            this.Property(t => t.IsAgent).HasColumnName("IsAgent");
            this.Property(t => t.IsClient).HasColumnName("IsClient");
            this.Property(t => t.IsEmployee).HasColumnName("IsEmployee");
            this.Property(t => t.LastLogin).HasColumnName("LastLogin");
            this.Property(t => t.IsPasswordchange).HasColumnName("IsPasswordchange");
            this.Property(t => t.ChangePassDate).HasColumnName("ChangePassDate");
            this.Property(t => t.IsChangePassRequired).HasColumnName("IsChangePassRequired");
            this.Property(t => t.LastTryLoginCount).HasColumnName("LastTryLoginCount");
        }
    }
}
