using Kaizen.BusinessLogic.Configuration;
using Kaizen.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UIServer.Controllers
{
    public class Sys_EncryptionController : Controller
    {
        // GET: Sys_Encryption
        public ActionResult Index(string KaizenPublicKey)
        {
            KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("Login", "SysUser");
            return PartialView();
        }
    }
}