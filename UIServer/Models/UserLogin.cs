using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UIServer.Models
{
    public class UserLogin
    {
        [Required]
        public string UserName { get; set; }

        //[Required(ErrorMessage = "Current Password is required."), DataType(DataType.Password)]
        //[RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[$@$!%*?&+=#]) [A-Za-z\\d$@$!%*?&+=#]{8,40}$", ErrorMessage = "Password must contain at least one upper character,one lower character,one number and one special character")]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
        public string ReturnUrl { get; set; }
    }
}