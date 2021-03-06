﻿using Kaizen.BusinessLogic.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UIServer.Areas.SOP.Controllers
{
    public class SOP_Inq_PointOfSaleController : Controller
    {
        // GET: SOP_Inq_PointOfSale
        public ActionResult Index(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }
    }
}