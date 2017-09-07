using System;
using System.Collections.Generic;
namespace Kaizen.Configuration
{
    public class KaizenUser
    {
        public string FullName { get; set; }
        public string PhotoExtension { get; set; }
        public string UserName { get; set; }
        public string ConnectionString; 
        public Guid PublicKey { get; set; }
        public Guid ConnectionId { get; set; }
        public string CompanyID { get; set; }
        public Screen CurrentScreen { get; set; }
        public int KaizenID { get; set; }
        public string TransactionID { get; set; }
        public virtual ICollection<Kaizen00400> Kaizen00400 { get; set; }
    }
}
