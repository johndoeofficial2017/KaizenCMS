using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UIServer.Controllers
{
    [AllowAnonymous]
    public class PhoneController : Controller
    {
        // GET: Phone
        [AllowAnonymous]
        public ActionResult Index(string PhoneFrom,string PhoneTo,bool IsPhoneIN)
        {
            Kaizen.BusinessLogic.Configuration.PhoneServices srv = new Kaizen.BusinessLogic.Configuration.PhoneServices(null);
            Kaizen.Configuration.Kaizen00100 o = new Kaizen.Configuration.Kaizen00100();
            o.PhoneFrom = PhoneFrom;
            o.PhoneTo = PhoneTo;
            o.IsPhoneIN = IsPhoneIN;
            o.TransactionDate = DateTime.Now;
            //string sql = "insert into Kaizen00100(TransactionDate,PhoneFrom,PhoneTo,IsPhoneIN)";
            //sql += "values(getdate(),'"+ PhoneFrom + "','"+ PhoneTo + "')";
            //srv.ExecuteSqlCommand(sql);
            srv.AddKaizen00100(o);
            return View();
        }
    }
}