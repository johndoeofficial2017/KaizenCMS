﻿using Kaizen.BusinessLogic.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UIServer.Areas.IV.Controllers
{
    public class IV_Inq_TransactionDetailController : Controller
    {
        // GET: IV_Inq_TransactionDetail
        public ActionResult Index(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }
    }
}