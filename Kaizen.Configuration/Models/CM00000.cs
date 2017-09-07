using System;
using System.Collections.Generic;

namespace Kaizen.Configuration
{
    public partial class CM00000
    {
        public string CompanyID { get; set; }
        public string AgentNationalityIdUpload { get; set; }
        public Nullable<byte> AgentGenderIdUpload { get; set; }
        public string AgentCUSTCLASUpload { get; set; }
        public Nullable<int> AgentGroupIDUpload4 { get; set; }
        public bool IsClientSerial { get; set; }
        public Nullable<bool> IsCLientPublicSerial { get; set; }
        public string ClientPrefixValue { get; set; }
        public Nullable<short> ClientPrefixlengh { get; set; }
        public Nullable<int> ClientLastClientID { get; set; }
        public bool IsAgentSerial { get; set; }
        public Nullable<bool> IsAgentPublicSerial { get; set; }
        public string AgentPrefixValue { get; set; }
        public Nullable<short> AgentPefixlengh { get; set; }
        public Nullable<int> AgentLastID { get; set; }
        public string BatchPrefixValue { get; set; }
        public short BatchPrefixlengh { get; set; }
        public int BatchLastID { get; set; }
        public string RCTPrefix { get; set; }
        public Nullable<byte> RCTLenth { get; set; }
        public Nullable<byte> RCTLastNumber { get; set; }
        public Nullable<bool> IsCaseFixedSerial { get; set; }
        public string CaseFixedPrefix { get; set; }
        public Nullable<byte> CaseFixedLenth { get; set; }
        public Nullable<int> CaseFixedLastNumber { get; set; }
        public string CasesAssignmentPrefix { get; set; }
        public Nullable<short> CasesAssignmentPrefixlengh { get; set; }
        public Nullable<int> CasesAssignmentLastID { get; set; }
        public string CasesLetterPrefix { get; set; }
        public Nullable<short> CasesLetterlengh { get; set; }
        public Nullable<int> CasesLetterLastID { get; set; }
        public string SMSLetterPrefix { get; set; }
        public Nullable<short> SMSLetterlengh { get; set; }
        public Nullable<int> SMSLetterLastID { get; set; }
        public string EmailLetterPrefix { get; set; }
        public Nullable<short> EmailLetterlengh { get; set; }
        public Nullable<int> EmailLetterLastID { get; set; }
        public string AgentReplacementPrefix { get; set; }
        public Nullable<short> AgentReplacementlengh { get; set; }
        public Nullable<int> AgentReplacementLastID { get; set; }
    }
}
