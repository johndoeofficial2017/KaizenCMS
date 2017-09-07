﻿using Kaizen.BusinessLogic;
using Kaizen.BusinessLogic.Admin;
using Kaizen.BusinessLogic.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UIServer.Areas.CRM.Controllers
{
    public class leadsController : Controller
    {
        // GET: CRM/leads
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Create(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null)
                return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }
        public ActionResult GetCaseFields(string KaizenPublicKey, int TRXTypeID)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            CRM00200Services service = new CRM00200Services(KaizenUser);
            return Json(service.GetAllFieldsByTRXTypeID(TRXTypeID), JsonRequestBehavior.AllowGet);
        }
        public ActionResult SaveLeadsData(int TRXTypeID,IList<KaizenColumn> KaizenColumn, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CRM00200Services service = new CRM00200Services(KaizenUser);
            return Json(service.SaveLeadsData(TRXTypeID,KaizenColumn), JsonRequestBehavior.AllowGet);
        }
    }
}