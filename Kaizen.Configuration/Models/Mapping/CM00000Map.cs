using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Kaizen.Configuration.Mapping
{
    public class CM00000Map : EntityTypeConfiguration<CM00000>
    {
        public CM00000Map()
        {
            // Primary Key
            this.HasKey(t => t.CompanyID);

            // Properties
            this.Property(t => t.CompanyID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.AgentNationalityIdUpload)
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.AgentCUSTCLASUpload)
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.ClientPrefixValue)
                .HasMaxLength(5);

            this.Property(t => t.AgentPrefixValue)
                .HasMaxLength(5);

            this.Property(t => t.BatchPrefixValue)
                .HasMaxLength(5);

            this.Property(t => t.RCTPrefix)
                .HasMaxLength(5);

            this.Property(t => t.CaseFixedPrefix)
                .HasMaxLength(5);

            this.Property(t => t.CasesAssignmentPrefix)
                .HasMaxLength(5);

            this.Property(t => t.CasesLetterPrefix)
                .HasMaxLength(5);

            this.Property(t => t.SMSLetterPrefix)
                .HasMaxLength(5);

            this.Property(t => t.EmailLetterPrefix)
                .HasMaxLength(5);

            this.Property(t => t.AgentReplacementPrefix)
                .HasMaxLength(5);

            // Table & Column Mappings
            this.ToTable("CM00000");
            this.Property(t => t.CompanyID).HasColumnName("CompanyID");
            this.Property(t => t.AgentNationalityIdUpload).HasColumnName("AgentNationalityIdUpload");
            this.Property(t => t.AgentGenderIdUpload).HasColumnName("AgentGenderIdUpload");
            this.Property(t => t.AgentCUSTCLASUpload).HasColumnName("AgentCUSTCLASUpload");
            this.Property(t => t.AgentGroupIDUpload4).HasColumnName("AgentGroupIDUpload4");
            this.Property(t => t.IsClientSerial).HasColumnName("IsClientSerial");
            this.Property(t => t.IsCLientPublicSerial).HasColumnName("IsCLientPublicSerial");
            this.Property(t => t.ClientPrefixValue).HasColumnName("ClientPrefixValue");
            this.Property(t => t.ClientPrefixlengh).HasColumnName("ClientPrefixlengh");
            this.Property(t => t.ClientLastClientID).HasColumnName("ClientLastClientID");
            this.Property(t => t.IsAgentSerial).HasColumnName("IsAgentSerial");
            this.Property(t => t.IsAgentPublicSerial).HasColumnName("IsAgentPublicSerial");
            this.Property(t => t.AgentPrefixValue).HasColumnName("AgentPrefixValue");
            this.Property(t => t.AgentPefixlengh).HasColumnName("AgentPefixlengh");
            this.Property(t => t.AgentLastID).HasColumnName("AgentLastID");
            this.Property(t => t.BatchPrefixValue).HasColumnName("BatchPrefixValue");
            this.Property(t => t.BatchPrefixlengh).HasColumnName("BatchPrefixlengh");
            this.Property(t => t.BatchLastID).HasColumnName("BatchLastID");
            this.Property(t => t.RCTPrefix).HasColumnName("RCTPrefix");
            this.Property(t => t.RCTLenth).HasColumnName("RCTLenth");
            this.Property(t => t.RCTLastNumber).HasColumnName("RCTLastNumber");
            this.Property(t => t.IsCaseFixedSerial).HasColumnName("IsCaseFixedSerial");
            this.Property(t => t.CaseFixedPrefix).HasColumnName("CaseFixedPrefix");
            this.Property(t => t.CaseFixedLenth).HasColumnName("CaseFixedLenth");
            this.Property(t => t.CaseFixedLastNumber).HasColumnName("CaseFixedLastNumber");
            this.Property(t => t.CasesAssignmentPrefix).HasColumnName("CasesAssignmentPrefix");
            this.Property(t => t.CasesAssignmentPrefixlengh).HasColumnName("CasesAssignmentPrefixlengh");
            this.Property(t => t.CasesAssignmentLastID).HasColumnName("CasesAssignmentLastID");
            this.Property(t => t.CasesLetterPrefix).HasColumnName("CasesLetterPrefix");
            this.Property(t => t.CasesLetterlengh).HasColumnName("CasesLetterlengh");
            this.Property(t => t.CasesLetterLastID).HasColumnName("CasesLetterLastID");
            this.Property(t => t.SMSLetterPrefix).HasColumnName("SMSLetterPrefix");
            this.Property(t => t.SMSLetterlengh).HasColumnName("SMSLetterlengh");
            this.Property(t => t.SMSLetterLastID).HasColumnName("SMSLetterLastID");
            this.Property(t => t.EmailLetterPrefix).HasColumnName("EmailLetterPrefix");
            this.Property(t => t.EmailLetterlengh).HasColumnName("EmailLetterlengh");
            this.Property(t => t.EmailLetterLastID).HasColumnName("EmailLetterLastID");
            this.Property(t => t.AgentReplacementPrefix).HasColumnName("AgentReplacementPrefix");
            this.Property(t => t.AgentReplacementlengh).HasColumnName("AgentReplacementlengh");
            this.Property(t => t.AgentReplacementLastID).HasColumnName("AgentReplacementLastID");
        }
    }
}
