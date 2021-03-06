﻿using Kaizen.BusinessLogic.Configuration;
using Kaizen.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UIServer.Areas.Tools.Controllers
{
    public class TeamUpRoomsController : Controller
    {
        // GET: TeamUp/TeamUpRooms
        public ActionResult Index(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null)
                return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }
    }
}