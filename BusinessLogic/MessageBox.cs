using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaizen.BusinessLogic
{

    public static class MessageBox
    {
        public static string ERPSystemName
        {
            get
            {
                return "Kaizen Enterprise Solution";
            }
        }
        public static string Successful
        {
            get
            {
                return "successful";
            }
        }
        //
        public static string Fail
        {
            get
            {
                return "Fail";
            }
        }
        public static string AlreadyLoged
        {
            get
            {
                return "AlreadyLoged";
            }
        }
        public static string LoginFail
        {
            get
            {
                return "LoginFail";
            }
        }
        public static string MissingRequiredField
        {
            get
            {
                return "missing required fields";
            }
        }
        public static string InvalidShipping
        {
            get
            {
                return "InvalidShipping";
            }
        }
        public static string InvalidZero
        {
            get
            {
                return "InvalidZero";
            }
        }

        public static string NoSufficientSiteQuantity
        {
            get
            {
                return "No Sufficient Site Quantity";
            }
        }
        public static string UserNotconfigured
        {
            get
            {
                return "User Not configured";
            }
        }
        public static string ModuleNotConfigured
        {
            get
            {
                return "Error :- Module Not Configured";
            }
        }
        public static string DateStartAfterEnd
        {
            get
            {
                return "Start date after end date";
            }
        }
        public static string HRErrorHeader
        {
            get
            {
                return "HR Missing Entery";
            }
        }
    }
}
