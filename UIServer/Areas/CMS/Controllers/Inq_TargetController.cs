using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kaizen.BusinessLogic.CMS;
using Kaizen.BusinessLogic.Configuration;
using Kaizen.CMS;

namespace UIServer.Areas.CMS.Controllers
{
    public class Inq_TargetController : Controller
    {
        // GET: CMS/Inq_Target
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult PeriodTarget()
        {
            return View();
        }
        public ActionResult TargetCompare()
        {
            return View();
        }

        public ActionResult GetAgentTargetClaimById(string KaizenPublicKey, int AgentID)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");

            var response = new List<MyResponse>
            {
                new MyResponse()
                {
                    ReYear = "2010 1",
                    Period = "Period 1",
                    Target = 2000,
                    Claim = 100
                },
                new MyResponse()
                {
                     ReYear = "2010 1",
                    Period = "Period 2",
                    Target = 2000,
                    Claim = 500
                },
                new MyResponse()
                {
                     ReYear = "2010 1",
                    Period = "Period 3",
                    Target = 2000,
                    Claim = 1000
                },
                new MyResponse()
                {
                     ReYear = "2010 1",
                    Period = "Period 7",
                    Target = 2000,
                    Claim = 2000
                },
 new MyResponse()
                {
                    ReYear = "2010 2",
                    Period = "Period 1",
                    Target = 2000,
                    Claim = 100
                },
                new MyResponse()
                {
                     ReYear = "2010 2",
                    Period = "Period 2",
                    Target = 2000,
                    Claim = 500
                },
                new MyResponse()
                {
                     ReYear = "2010 2",
                    Period = "Period 3",
                    Target = 2000,
                    Claim = 1000
                },
                new MyResponse()
                {
                     ReYear = "2010 2",
                    Period = "Period 7",
                    Target = 2000,
                    Claim = 2000
                },

            };
            return Json(response, JsonRequestBehavior.AllowGet);
        }
    }

    public class MyResponse
    {
        public string ReYear { get; set; }
        public string Period { get; set; }
        public int Target { get; set; }
        public int Claim { get; set; }
    }
}