using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Kaizen.BusinessLogic.Purchase;
using Kaizen.Purchase;
using Kaizen.BusinessLogic.Configuration;
using Kaizen.Configuration;

namespace UIServer.Areas.SOP.Controllers
{
    public class SOP_AddressTypeCodeController : Controller
    {
        // GET: SOP_AddressTypeCode
        public ActionResult Index(string KaizenPublicKey)
        {
            KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null)
                return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }

        public ActionResult Create(string KaizenPublicKey)
        {
            KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null)
                return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }
    }
}