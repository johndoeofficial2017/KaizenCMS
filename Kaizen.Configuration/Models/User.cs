using System;
using System.Collections.Generic;

namespace Kaizen.Configuration
{
    public partial class User
    {
        public User()
        {
            this.CompanyAccesses = new List<CompanyAccess>();
            this.Kaizen00005 = new List<Kaizen00005>();
            this.Kaizen00020 = new List<Kaizen00020>();
            this.Kaizen00400 = new List<Kaizen00400>();
            this.KaizenSessions = new List<KaizenSession>();
            this.KaizenSessionFails = new List<KaizenSessionFail>();
        }

        public string UserName { get; set; }
        public string UserPassword { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string OsUserName { get; set; }
        public bool IsDisabled { get; set; }
        public string PhotoExtension { get; set; }
        public bool IsCustomer { get; set; }
        public bool IsVendor { get; set; }
        public bool IsDebtor { get; set; }
        public bool IsPartner { get; set; }
        public bool IsAgent { get; set; }
        public bool IsClient { get; set; }
        public bool IsEmployee { get; set; }
        public Nullable<System.DateTime> LastLogin { get; set; }
        public bool IsPasswordchange { get; set; }
        public Nullable<System.DateTime> ChangePassDate { get; set; }
        public Nullable<bool> IsChangePassRequired { get; set; }
        public Nullable<int> LastTryLoginCount { get; set; }
        public virtual ICollection<CompanyAccess> CompanyAccesses { get; set; }
        public virtual ICollection<Kaizen00005> Kaizen00005 { get; set; }
        public virtual ICollection<Kaizen00020> Kaizen00020 { get; set; }
        public virtual ICollection<Kaizen00400> Kaizen00400 { get; set; }
        public virtual ICollection<KaizenSession> KaizenSessions { get; set; }
        public virtual ICollection<KaizenSessionFail> KaizenSessionFails { get; set; }
    }
}
