using System.ComponentModel.DataAnnotations.Schema;
namespace Kaizen.SOP
{
    public partial class SOP00110
    {
        [NotMapped]
        public string FullName
        {
            get
            {
                string fullname = this.FirstName + " " + this.MidName + " " + this.LastName;
                return fullname;
            }
        }
    }
}
