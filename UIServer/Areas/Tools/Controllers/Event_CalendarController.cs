using Kaizen.BusinessLogic.Configuration;
using Kaizen.Configuration;
using Kaizen.Configuration.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UIServer.Areas.Tools.Controllers
{
    public class Event_CalendarController : Controller
    {
        // GET: Tools/Event_Calendar
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetAllMeetings(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            Met00200Services service = new Met00200Services(KaizenUser);
            IList<Met00200> L = service.GetAll();
            List<Met00200> result = new List<Met00200>();
            foreach (Met00200 obj in L)
            {
                Met00200 tt = new Met00200()
                {
                    TRXTypeID=obj.TRXTypeID,
                    MeetingRoomID = obj.MeetingRoomID,
                    MeetingName = obj.MeetingName,
                    MeetingDescription = obj.MeetingDescription,
                    IsFullDay = obj.IsFullDay,
                    MeetingRepeatTypeID = obj.MeetingRepeatTypeID,
                    RepeatEvery = obj.RepeatEvery,
                    StartDateTime = obj.StartDateTime,
                    EndDateTime = obj.EndDateTime,
                    MeetingID = obj.MeetingID,
                    MonthRepeatOn = obj.MonthRepeatOn,
                    MonthRepeatWeekDay = obj.MonthRepeatWeekDay,
                    RepeatEndAfter = obj.RepeatEndAfter,
                    RepeatEndNever = obj.RepeatEndNever,
                    RepeatEndOn = obj.RepeatEndOn,
                    RepeatFrid = obj.RepeatFrid,
                    RepeatMon = obj.RepeatMon,
                    RepeatSat = obj.RepeatSat,
                    RepeatSun = obj.RepeatSun,
                    RepeatThur = obj.RepeatThur,
                    RepeatTus = obj.RepeatTus,
                    RepeatTypeID = obj.RepeatTypeID,
                    RepeatWed = obj.RepeatWed,
                    YearRepeatMonth = obj.YearRepeatMonth,
                    YearRepeatMonthDay = obj.YearRepeatMonthDay,
                    YearRepeatOn = obj.YearRepeatOn,
                    RepeatEndType = obj.RepeatEndType,
                    MonthDayOccurence = obj.MonthDayOccurence,
                    YearMonthDayOccurence = obj.YearMonthDayOccurence,
                    RecurrenceRule = obj.RecurrenceRule,
                    Met00202 = obj.Met00202,
                };
                foreach (Met00201 oMet00011 in obj.Met00201)
                {
                    tt.Met00201.Add(new Met00201()
                    {
                        CalendarID = oMet00011.CalendarID,
                        //CalendarName = oMet00011.CalendarName
                        //,UserName = oMet00011.UserName
                        MeetingID = obj.MeetingID,
                        TableID = oMet00011.TableID
                    });
                }

                tt.Met00203 = service.GetMet00203(obj.MeetingID);

                var rptType = service.GetMeetingRepeatType(Convert.ToInt32(obj.MeetingRepeatTypeID));
                if (rptType != null)
                    tt.Met00005 = new Met00005() { MeetingRepeatTypeID = rptType.MeetingRepeatTypeID, MeetingRepeatTypeName = rptType.MeetingRepeatTypeName };

                result.Add(tt);
                // obj. = service.GetAllCalenderNamesByMeeting(obj.MeetingID);
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetAllCalenderNames(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            Met00200Services service = new Met00200Services(KaizenUser);
            IList<Met00011> L = service.GetAllCalenderNames();
            return Json(L, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetAllCM00203(string KaizenPublicKey,int TRXTypeID)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            Kaizen.BusinessLogic.CMS.CM00203Services service = new Kaizen.BusinessLogic.CMS.CM00203Services(KaizenUser);
            IList<Kaizen.CMS.CM00203> L = service.GetAllByCaseType(TRXTypeID);
            return Json(L, JsonRequestBehavior.AllowGet);
        }
        public ActionResult SearchCaseRefCM00203(string KaizenPublicKey,int TRXTypeID, string searchText)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            Kaizen.BusinessLogic.CMS.CM00203Services service = new Kaizen.BusinessLogic.CMS.CM00203Services(KaizenUser);
            IList<Kaizen.CMS.CM00203> L = service.GetTop10BYCaseRef(TRXTypeID,searchText).Items;
            return Json(L, JsonRequestBehavior.AllowGet);
        }
        private Met00200 CopyFrom(Met00200 obj)
        {
            Met00200 Met00200 = new Met00200()
            {
                MeetingRoomID = obj.MeetingRoomID,
                TRXTypeID=obj.TRXTypeID,
                MeetingName = obj.MeetingName,
                MeetingDescription = obj.MeetingDescription,
                IsFullDay = obj.IsFullDay,
                MeetingRepeatTypeID = obj.MeetingRepeatTypeID,
                RepeatEvery = obj.RepeatEvery,
                StartDateTime = obj.StartDateTime,
                EndDateTime = obj.EndDateTime,
                MeetingID = obj.MeetingID,
                MonthRepeatOn = obj.MonthRepeatOn,
                MonthRepeatWeekDay = obj.MonthRepeatWeekDay,
                RepeatEndAfter = obj.RepeatEndAfter,
                RepeatEndNever = obj.RepeatEndNever,
                RepeatEndOn = obj.RepeatEndOn,
                RepeatFrid = obj.RepeatFrid,
                RepeatMon = obj.RepeatMon,
                RepeatSat = obj.RepeatSat,
                RepeatSun = obj.RepeatSun,
                RepeatThur = obj.RepeatThur,
                RepeatTus = obj.RepeatTus,
                RepeatTypeID = obj.RepeatTypeID,
                RepeatWed = obj.RepeatWed,
                YearRepeatMonth = obj.YearRepeatMonth,
                YearRepeatMonthDay = obj.YearRepeatMonthDay,
                YearRepeatOn = obj.YearRepeatOn,
                RepeatEndType = obj.RepeatEndType,
                MonthDayOccurence = obj.MonthDayOccurence,
                YearMonthDayOccurence = obj.YearMonthDayOccurence,
                RecurrenceRule = obj.RecurrenceRule,
            };

            return Met00200;
        }
        public ActionResult SaveData(Met00200 Met00200, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            Met00200Services service = new Met00200Services(KaizenUser);

            //Met00200.RepeatEndOn = Convert.ToDateTime(Met00200.RepeatEndOn);
            Met00200.RecurrenceRule = GenerateRecurrenceRule(Met00200);

            if (Met00200.MeetingID == 0)
                service.AddMet00200(Met00200);
            else
            {
                Met00200 updateMet00200 = CopyFrom(Met00200);
                service.Update00200(updateMet00200);
            }

            service.Delete(Met00200.MeetingID);
            if (Met00200.Met00201.Count > 0)
            {
                IList<Met00201> Met00201List = GetMet00201(Met00200.Met00201.ToList());
                service.SaveMet00201(Met00201List);
            }

            service.DeleteMet00203(Met00200.MeetingID);
            if (Met00200.Met00203.Count > 0)
            {
                IList<Met00203> Met00203List = GetMet00203(Met00200.Met00203.ToList());
                service.SaveMet00203(Met00203List);
            }

            if (Met00200.Met00202.Count > 0)
                service.SaveMet00202(Met00200.Met00202.ToList());

            return GetAllMeetings(KaizenPublicKey);
        }
        private IList<Met00201> GetMet00201(IList<Met00201> Met00201List)
        {
            foreach (Met00201 Met00201 in Met00201List)
            {
                Met00201.Met00200 = null;
            }

            return Met00201List;
        }
        private IList<Met00203> GetMet00203(IList<Met00203> Met00203List)
        {
            foreach (Met00203 Met00203 in Met00203List)
            {
                Met00203.Met00200 = null;
            }

            return Met00203List;
        }

        public ActionResult DeleteData(Met00200 Met00200, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            Met00200Services service = new Met00200Services(KaizenUser);
            return Json(service.Delete(Met00200), JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetAllRooms(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            Met00200Services service = new Met00200Services(KaizenUser);
            IList<Met00204> L = service.GetAllRooms();
            return Json(L, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetAllMeetingRepeatTypes(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            Met00200Services service = new Met00200Services(KaizenUser);
            IList<Met00005> L = service.GetAllMeetingRepeatTypes();
            return Json(L, JsonRequestBehavior.AllowGet);
        }

        public static string GenerateRecurrenceRule(Met00200 Met00200)
        {
            string recurrenceRule = "";
            try
            {
                if (Met00200.MeetingRepeatTypeID == 1)
                    return recurrenceRule;

                if (Met00200.MeetingRepeatTypeID == 2)
                    return GetDailyRecurrencRule(Met00200);
                if (Met00200.MeetingRepeatTypeID == 3)
                    return GetWeeklyRecurrencRule(Met00200);
                if (Met00200.MeetingRepeatTypeID == 4)
                    return GetMonthlyRecurrencRule(Met00200);
                if (Met00200.MeetingRepeatTypeID == 5)
                    return GetYearlyRecurrencRule(Met00200);
            }
            catch (Exception ex)
            {

            }
            return recurrenceRule;
        }

        public static string GetDailyRecurrencRule(Met00200 Met00200)
        {
            string recurrenceRule = "", FREQ = "DAILY";

            try
            {
                if (Met00200.RepeatEvery == 1)
                    recurrenceRule = string.Format("FREQ={0}", FREQ);
                else if (Met00200.RepeatEvery > 1)
                {
                    recurrenceRule = string.Format("FREQ={0};INTERVAL={1}", FREQ, Met00200.RepeatEvery);
                }

                if (Met00200.RepeatEndType.Equals("After", StringComparison.OrdinalIgnoreCase))
                {
                    recurrenceRule += string.Format(";COUNT={0}", Met00200.RepeatEndAfter);
                }
                else if (Met00200.RepeatEndType.Equals("On", StringComparison.OrdinalIgnoreCase))
                {
                    recurrenceRule += string.Format(";UNTIL={0}", Convert.ToDateTime(Met00200.RepeatEndOn).ToString("yyyyMMdd"));
                }
            }
            catch (Exception ex)
            {
                recurrenceRule = "";
            }
            return recurrenceRule;
        }

        public static string GetWeeklyRecurrencRule(Met00200 Met00200)
        {
            string recurrenceRule = "", FREQ = "WEEKLY", BYDAY = "";

            try
            {
                if (Met00200.RepeatEvery == 1)
                    recurrenceRule = string.Format("FREQ={0}", FREQ);
                else if (Met00200.RepeatEvery > 1)
                {
                    recurrenceRule = string.Format("FREQ={0};INTERVAL={1}", FREQ, Met00200.RepeatEvery);
                }

                if (Convert.ToBoolean(Met00200.RepeatSun))
                    BYDAY = "SU";
                if (Convert.ToBoolean(Met00200.RepeatMon))
                {
                    if (!string.IsNullOrEmpty(BYDAY))
                        BYDAY += ",";
                    BYDAY += "MO";
                }
                if (Convert.ToBoolean(Met00200.RepeatTus))
                {
                    if (!string.IsNullOrEmpty(BYDAY))
                        BYDAY += ",";
                    BYDAY += "TU";
                }
                if (Convert.ToBoolean(Met00200.RepeatWed))
                {
                    if (!string.IsNullOrEmpty(BYDAY))
                        BYDAY += ",";
                    BYDAY += "WE";
                }
                if (Convert.ToBoolean(Met00200.RepeatThur))
                {
                    if (!string.IsNullOrEmpty(BYDAY))
                        BYDAY += ",";
                    BYDAY += "TH";
                }
                if (Convert.ToBoolean(Met00200.RepeatFrid))
                {
                    if (!string.IsNullOrEmpty(BYDAY))
                        BYDAY += ",";
                    BYDAY += "FR";
                }
                if (Convert.ToBoolean(Met00200.RepeatSat))
                {
                    if (!string.IsNullOrEmpty(BYDAY))
                        BYDAY += ",";
                    BYDAY += "SA";
                }

                if (!string.IsNullOrEmpty(BYDAY))
                    recurrenceRule += string.Format(";BYDAY={0}", BYDAY);

                if (Met00200.RepeatEndType.Equals("After", StringComparison.OrdinalIgnoreCase))
                {
                    recurrenceRule += string.Format(";COUNT={0}", Met00200.RepeatEndAfter);
                }
                else if (Met00200.RepeatEndType.Equals("On", StringComparison.OrdinalIgnoreCase))
                {
                    recurrenceRule += string.Format(";UNTIL={0}", Convert.ToDateTime(Met00200.RepeatEndOn).ToString("yyyyMMdd"));
                }
            }
            catch (Exception ex)
            {
                recurrenceRule = "";
            }
            return recurrenceRule;
        }

        public static string GetMonthlyRecurrencRule(Met00200 Met00200)
        {
            string recurrenceRule = "", FREQ = "MONTHLY", BYDAY = "";
            int MonthDayOccurence = 0;

            try
            {
                if (Met00200.RepeatEvery == 1)
                    recurrenceRule = string.Format("FREQ={0}", FREQ);
                else if (Met00200.RepeatEvery > 1)
                {
                    recurrenceRule = string.Format("FREQ={0};INTERVAL={1}", FREQ, Met00200.RepeatEvery);
                }

                if (Met00200.RepeatTypeID == 1)
                {
                    recurrenceRule += string.Format(";BYMONTHDAY={0}", Met00200.MonthRepeatOn);
                }
                else if (Met00200.RepeatTypeID == 2)
                {
                    if (Met00200.MonthRepeatWeekDay == -2)
                    {
                        MonthDayOccurence = Convert.ToInt32(Met00200.MonthDayOccurence);
                        BYDAY = "SU,MO,TU,WE,TH,FR,SA";

                        recurrenceRule += string.Format(";BYDAY={0};BYSETPOS={1}", BYDAY, MonthDayOccurence);
                    }
                    else if (Met00200.MonthRepeatWeekDay == -1)
                    {
                        MonthDayOccurence = Convert.ToInt32(Met00200.MonthDayOccurence);
                        BYDAY = "MO,TU,WE,TH,FR";

                        recurrenceRule += string.Format(";BYDAY={0};BYSETPOS={1}", BYDAY, MonthDayOccurence);
                    }
                    else if (Met00200.MonthRepeatWeekDay == 0)
                    {
                        MonthDayOccurence = Convert.ToInt32(Met00200.MonthDayOccurence);
                        BYDAY = "SU,SA";

                        recurrenceRule += string.Format(";BYDAY={0};BYSETPOS={1}", BYDAY, MonthDayOccurence);
                    }
                    else if (Met00200.MonthRepeatWeekDay >= 1 && Met00200.MonthRepeatWeekDay <= 7)
                    {
                        MonthDayOccurence = Convert.ToInt32(Met00200.MonthDayOccurence);

                        #region generate month week day occurence
                        switch (Met00200.MonthRepeatWeekDay)
                        {
                            case 1:
                                recurrenceRule += string.Format(";BYDAY={0}{1}", MonthDayOccurence, "SU");
                                break;
                            case 2:
                                recurrenceRule += string.Format(";BYDAY={0}{1}", MonthDayOccurence, "MO");
                                break;
                            case 3:
                                recurrenceRule += string.Format(";BYDAY={0}{1}", MonthDayOccurence, "TU");
                                break;
                            case 4:
                                recurrenceRule += string.Format(";BYDAY={0}{1}", MonthDayOccurence, "WE");
                                break;
                            case 5:
                                recurrenceRule += string.Format(";BYDAY={0}{1}", MonthDayOccurence, "TH");
                                break;
                            case 6:
                                recurrenceRule += string.Format(";BYDAY={0}{1}", MonthDayOccurence, "FR");
                                break;
                            case 7:
                                recurrenceRule += string.Format(";BYDAY={0}{1}", MonthDayOccurence, "SA");
                                break;
                        }
                        #endregion
                    }
                }

                if (Met00200.RepeatEndType.Equals("After", StringComparison.OrdinalIgnoreCase))
                {
                    recurrenceRule += string.Format(";COUNT={0}", Met00200.RepeatEndAfter);
                }
                else if (Met00200.RepeatEndType.Equals("On", StringComparison.OrdinalIgnoreCase))
                {
                    recurrenceRule += string.Format(";UNTIL={0}", Convert.ToDateTime(Met00200.RepeatEndOn).ToString("yyyyMMdd"));
                }
            }
            catch (Exception ex)
            {
                recurrenceRule = "";
            }
            return recurrenceRule;
        }

        public static string GetYearlyRecurrencRule(Met00200 Met00200)
        {
            string recurrenceRule = "", FREQ = "YEARLY", BYDAY = "";
            int YearMonthDayOccurence = 0;

            try
            {
                if (Met00200.RepeatEvery == 1)
                    recurrenceRule = string.Format("FREQ={0}", FREQ);
                else if (Met00200.RepeatEvery > 1)
                {
                    recurrenceRule = string.Format("FREQ={0};INTERVAL={1}", FREQ, Met00200.RepeatEvery);
                }

                if (Met00200.RepeatTypeID == 3)
                {
                    recurrenceRule += string.Format(";BYMONTH={0};BYMONTHDAY={1}", Met00200.YearRepeatMonth, Met00200.YearRepeatOn);
                }
                else if (Met00200.RepeatTypeID == 4)
                {
                    recurrenceRule += string.Format(";BYMONTH={0}", Met00200.YearRepeatMonth);

                    if (Met00200.YearRepeatMonthDay == -2)
                    {
                        YearMonthDayOccurence = Convert.ToInt32(Met00200.YearMonthDayOccurence);
                        BYDAY = "SU,MO,TU,WE,TH,FR,SA";

                        recurrenceRule += string.Format(";BYDAY={0};BYSETPOS={1}", BYDAY, YearMonthDayOccurence);
                    }
                    else if (Met00200.YearRepeatMonthDay == -1)
                    {
                        YearMonthDayOccurence = Convert.ToInt32(Met00200.YearMonthDayOccurence);
                        BYDAY = "MO,TU,WE,TH,FR";

                        recurrenceRule += string.Format(";BYDAY={0};BYSETPOS={1}", BYDAY, YearMonthDayOccurence);
                    }
                    else if (Met00200.YearRepeatMonthDay == 0)
                    {
                        YearMonthDayOccurence = Convert.ToInt32(Met00200.YearMonthDayOccurence);
                        BYDAY = "SU,SA";

                        recurrenceRule += string.Format(";BYDAY={0};BYSETPOS={1}", BYDAY, YearMonthDayOccurence);
                    }
                    else if (Met00200.YearRepeatMonthDay >= 1 && Met00200.YearRepeatMonthDay <= 7)
                    {
                        YearMonthDayOccurence = Convert.ToInt32(Met00200.YearMonthDayOccurence);

                        #region generate month week day occurence
                        switch (Met00200.YearRepeatMonthDay)
                        {
                            case 1:
                                recurrenceRule += string.Format(";BYDAY={0}{1}", YearMonthDayOccurence, "SU");
                                break;
                            case 2:
                                recurrenceRule += string.Format(";BYDAY={0}{1}", YearMonthDayOccurence, "MO");
                                break;
                            case 3:
                                recurrenceRule += string.Format(";BYDAY={0}{1}", YearMonthDayOccurence, "TU");
                                break;
                            case 4:
                                recurrenceRule += string.Format(";BYDAY={0}{1}", YearMonthDayOccurence, "WE");
                                break;
                            case 5:
                                recurrenceRule += string.Format(";BYDAY={0}{1}", YearMonthDayOccurence, "TH");
                                break;
                            case 6:
                                recurrenceRule += string.Format(";BYDAY={0}{1}", YearMonthDayOccurence, "FR");
                                break;
                            case 7:
                                recurrenceRule += string.Format(";BYDAY={0}{1}", YearMonthDayOccurence, "SA");
                                break;
                        }
                        #endregion
                    }
                }

                if (Met00200.RepeatEndType.Equals("After", StringComparison.OrdinalIgnoreCase))
                {
                    recurrenceRule += string.Format(";COUNT={0}", Met00200.RepeatEndAfter);
                }
                else if (Met00200.RepeatEndType.Equals("On", StringComparison.OrdinalIgnoreCase))
                {
                    recurrenceRule += string.Format(";UNTIL={0}", Convert.ToDateTime(Met00200.RepeatEndOn).ToString("yyyyMMdd"));
                }
            }
            catch (Exception ex)
            {
                recurrenceRule = "";
            }
            return recurrenceRule;
        }
    }
}