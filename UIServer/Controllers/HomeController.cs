using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kaizen.Configuration;
using Kaizen.BusinessLogic.Configuration;

namespace UIServer.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(string KaizenPublicKey)
        {
            KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("LoginToCompany", "SysUser");

            UserServices Oservice = new UserServices(KaizenUser);
            List<Kaizen004View> result = Oservice.GetMyMenue().OrderBy(x => x.ModuleID).ToList();
            // 1 =  Configuration
            #region Configuration
            List<Kaizen000> ModuleConfiguration = new List<Kaizen000>();
            List<Kaizen004View> Configuration = result.FindAll(ss => ss.MenueTypeID == 1);
            foreach (Kaizen004View myMenues in Configuration)
            {
                Kaizen000 Menue = ModuleConfiguration.Find(ss => ss.ModuleID == myMenues.ModuleID);
                if (Menue == null)
                {
                    Menue = Kaizen.BusinessLogic.Master.InsalledModules.Find(ss => ss.ModuleID == myMenues.ModuleID);
                    ModuleConfiguration.Add(Menue);
                }
            }
            #endregion
            // 2 =  Utility  <li><a href="" ng-click="AddNewMainTab('Admin_Massage','Message')" id="Admin_Massage">Massages</a></li>
            List<Kaizen000> ModuleUtility = new List<Kaizen000>();
            List<Kaizen004View> Utility = result.FindAll(ss => ss.MenueTypeID == 2);
            foreach (Kaizen004View myMenues in Utility)
            {
                Kaizen000 Menue = ModuleUtility.Find(ss => ss.ModuleID == myMenues.ModuleID);
                if (Menue == null)
                {
                    Menue = Kaizen.BusinessLogic.Master.InsalledModules.Find(ss => ss.ModuleID == myMenues.ModuleID);
                    ModuleUtility.Add(Menue);
                }
            } 
            // 3 Transaction
            List<Kaizen000> ModuleTransaction = new List<Kaizen000>();
            List<Kaizen004View> Transactions = result.FindAll(ss => ss.MenueTypeID == 3);
          
            foreach (Kaizen004View myMenues in Transactions)
            {
                Kaizen000 Menue = ModuleTransaction.Find(ss => ss.ModuleID == myMenues.ModuleID);
                if (Menue == null)
                {
                    Menue = Kaizen.BusinessLogic.Master.InsalledModules.Find(ss => ss.ModuleID == myMenues.ModuleID);
                    ModuleTransaction.Add(Menue);
                }
            }
            // 4 ModuleCommon
            List<Kaizen000> ModuleCommon = new List<Kaizen000>();
            List<Kaizen004View> Common = result.FindAll(ss => ss.MenueTypeID == 4);
            foreach (Kaizen004View myMenues in Common)
            {
                Kaizen000 Menue = ModuleCommon.Find(ss => ss.ModuleID == myMenues.ModuleID);
                if (Menue == null)
                {
                    Menue = Kaizen.BusinessLogic.Master.InsalledModules.Find(ss => ss.ModuleID == myMenues.ModuleID);
                    ModuleCommon.Add(Menue);
                }
            }
            // 6 Inquiry 
            List<Kaizen000> ModuleInquiry = new List<Kaizen000>();
            List<Kaizen004View> Inquiry = result.FindAll(ss => ss.MenueTypeID == 6);
            foreach (Kaizen004View myMenues in Inquiry)
            {
                Kaizen000 Menue = ModuleInquiry.Find(ss => ss.ModuleID == myMenues.ModuleID);
                if (Menue == null)
                {
                    Menue = Kaizen.BusinessLogic.Master.InsalledModules.Find(ss => ss.ModuleID == myMenues.ModuleID);
                    ModuleInquiry.Add(Menue);
                }
            }
            ViewBag.ModuleConfiguration = ModuleConfiguration;
            ViewBag.ModuleUtility = ModuleUtility;
            ViewBag.ModuleCommon = ModuleCommon;
            ViewBag.ModuleTransaction = ModuleTransaction;
            ViewBag.ModuleInquiry = ModuleInquiry;
            ViewBag.MenueALL = result;

            ViewBag.Title = Kaizen.BusinessLogic.MessageBox.ERPSystemName;
            //ViewBag.IsAdministrator = true;
            
            return View();
        }
        public ActionResult Lock()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
    }
}
