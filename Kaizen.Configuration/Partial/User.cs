using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kaizen.Configuration
{
    public partial class User
    {
        [NotMapped]
        public string ConnectionId { get; set; }

        [NotMapped]
        public string NewPassword { get; set; }
        [NotMapped]
        public string FullName
        {
            get
            {
                if (string.IsNullOrEmpty(this.FirstName) & string.IsNullOrEmpty(this.LastName))
                    return this.UserName;
                return (string.IsNullOrEmpty(this.FirstName) ? "" : this.FirstName) + " " + 
                    (string.IsNullOrEmpty(this.LastName) ? "" : this.LastName);
            }
        }
        [NotMapped]
        public string ConnectionString;
        [NotMapped]
        public Guid PublicKey { get; set; }
        [NotMapped]
        public string CompanyID { get; set; }
        [NotMapped]
        public Screen CurrentScreen { get; set; }
        [NotMapped]
        public int KaizenID { get; set; }
        [NotMapped]
        public string TransactionID { get; set; }
        [NotMapped]
        public List<Screen> Screens { get; set; }


        [NotMapped]
        public string CompanyName { get; set; }
        [NotMapped]
        public string SegmentMark { get; set; }
    }
    public enum Roles : int
    {
        Member = 1,
        Administrator = 2
    }
}
