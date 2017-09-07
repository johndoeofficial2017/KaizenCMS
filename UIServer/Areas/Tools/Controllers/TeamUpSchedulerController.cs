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
    public class TeamUpSchedulerController : Controller
    {
        // GET: TeamUp/TeamUpScheduler
        public ActionResult Index(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null)
                return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }

        public ActionResult GetAllMeetings(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            Met00100Services service = new Met00100Services(KaizenUser);
            IList<Met00100> L = service.GetAll();
            List<Met00100> result = new List<Met00100>();
            foreach (Met00100 obj in L)
            {
                Met00100 tt = new Met00100()
                {
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
                    Met00102 = obj.Met00102
                };
                foreach (Met00101 oMet00001 in obj.Met00101)
                {
                    tt.Met00101.Add(new Met00101() {
                        CalendarID = oMet00001.CalendarID,
                        //CalendarName = oMet00001.CalendarName
                        //,UserName = oMet00001.UserName
                        MeetingID = obj.MeetingID,
                        TableID=oMet00001.TableID
                    });
                }
                var rptType = service.GetMeetingRepeatType(Convert.ToInt32(obj.MeetingRepeatTypeID));
                if(rptType!=null)
                    tt.Met00005 = new Met00005() { MeetingRepeatTypeID = rptType.MeetingRepeatTypeID, MeetingRepeatTypeName = rptType.MeetingRepeatTypeName };

                result.Add(tt);
                // obj. = service.GetAllCalenderNamesByMeeting(obj.MeetingID);
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetAllCalenderNames(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            Met00100Services service = new Met00100Services(KaizenUser);
            IList<Met00001> L = service.GetAllCalenderNames();
            return Json(L, JsonRequestBehavior.AllowGet);
        }

        private Met00100 CopyFrom(Met00100 obj)
        {
            Met00100 Met00100 = new Met00100()
            {
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
            };

            return Met00100;
        }

        public ActionResult SaveData(Met00100 Met00100, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            Met00100Services service = new Met00100Services(KaizenUser);

            //Met00100.RepeatEndOn = Convert.ToDateTime(Met00100.RepeatEndOn);
            Met00100.RecurrenceRule = GenerateRecurrenceRule(Met00100);
                        
            if (Met00100.MeetingID==0)
                service.AddMet00100(Met00100);
            else
            {
                Met00100 updateMet00100 = CopyFrom(Met00100);

                service.Update00100(updateMet00100);

            }

            service.Delete(Met00100.MeetingID);
            if (Met00100.Met00101.Count > 0)
            {
                service.SaveMet00101(Met00100.Met00101.ToList());
            }

            if (Met00100.Met00102.Count>0)
                service.SaveMet00102(Met00100.Met00102.ToList());
            
            return GetAllMeetings(KaizenPublicKey);
        }

        public ActionResult DeleteData(Met00100 Met00100, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            Met00100Services service = new Met00100Services(KaizenUser);
            return Json(service.Delete(Met00100), JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetAllRooms(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            Met00100Services service = new Met00100Services(KaizenUser);
            IList<Met00007> L = service.GetAllRooms();
            return Json(L, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetAllMeetingRepeatTypes(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            Met00100Services service = new Met00100Services(KaizenUser);
            IList<Met00005> L = service.GetAllMeetingRepeatTypes();
            return Json(L, JsonRequestBehavior.AllowGet);
        }

        public static string GenerateRecurrenceRule(Met00100 Met00100)
        {
            string recurrenceRule = "";
            try
            {
                if (Met00100.MeetingRepeatTypeID == 1)
                    return recurrenceRule;

                if (Met00100.MeetingRepeatTypeID == 2)
                    return GetDailyRecurrencRule(Met00100);
                if (Met00100.MeetingRepeatTypeID == 3)
                    return GetWeeklyRecurrencRule(Met00100);
                if (Met00100.MeetingRepeatTypeID == 4)
                    return GetMonthlyRecurrencRule(Met00100);
                if (Met00100.MeetingRepeatTypeID == 5)
                    return GetYearlyRecurrencRule(Met00100);
            }
            catch (Exception ex)
            {

            }
            return recurrenceRule;
        }

        public static string GetDailyRecurrencRule(Met00100 Met00100)
        {
            string recurrenceRule = "",FREQ="DAILY";

            try
            {
                if (Met00100.RepeatEvery == 1)
                    recurrenceRule = string.Format("FREQ={0}", FREQ);
                else if (Met00100.RepeatEvery > 1)
                {
                    recurrenceRule = string.Format("FREQ={0};INTERVAL={1}", FREQ,Met00100.RepeatEvery);
                }

                if(Met00100.RepeatEndType.Equals("After",StringComparison.OrdinalIgnoreCase))
                {
                    recurrenceRule += string.Format(";COUNT={0}",Met00100.RepeatEndAfter);
                }
                else if (Met00100.RepeatEndType.Equals("On", StringComparison.OrdinalIgnoreCase))
                {
                    recurrenceRule += string.Format(";UNTIL={0}", Convert.ToDateTime(Met00100.RepeatEndOn).ToString("yyyyMMdd"));
                }
            }
            catch(Exception ex) {
                recurrenceRule = "";
            }
            return recurrenceRule;
        }
                
        public static string GetWeeklyRecurrencRule(Met00100 Met00100)
        {
            string recurrenceRule = "", FREQ = "WEEKLY",BYDAY="";

            try
            {
                if (Met00100.RepeatEvery == 1)
                    recurrenceRule = string.Format("FREQ={0}", FREQ);
                else if (Met00100.RepeatEvery > 1)
                {
                    recurrenceRule = string.Format("FREQ={0};INTERVAL={1}", FREQ, Met00100.RepeatEvery);
                }

                if (Convert.ToBoolean(Met00100.RepeatSun))
                    BYDAY = "SU";
                if (Convert.ToBoolean(Met00100.RepeatMon))
                {
                    if (!string.IsNullOrEmpty(BYDAY))
                        BYDAY += ",";
                    BYDAY += "MO";
                }
                if (Convert.ToBoolean(Met00100.RepeatTus))
                {
                    if (!string.IsNullOrEmpty(BYDAY))
                        BYDAY += ",";
                    BYDAY += "TU";
                }
                if (Convert.ToBoolean(Met00100.RepeatWed))
                {
                    if (!string.IsNullOrEmpty(BYDAY))
                        BYDAY += ",";
                    BYDAY += "WE";
                }
                if (Convert.ToBoolean(Met00100.RepeatThur))
                {
                    if (!string.IsNullOrEmpty(BYDAY))
                        BYDAY += ",";
                    BYDAY += "TH";
                }
                if (Convert.ToBoolean(Met00100.RepeatFrid))
                {
                    if (!string.IsNullOrEmpty(BYDAY))
                        BYDAY += ",";
                    BYDAY += "FR";
                }
                if (Convert.ToBoolean(Met00100.RepeatSat))
                {
                    if (!string.IsNullOrEmpty(BYDAY))
                        BYDAY += ",";
                    BYDAY += "SA";
                }
                
                if (!string.IsNullOrEmpty(BYDAY))
                    recurrenceRule += string.Format(";BYDAY={0}", BYDAY);

                if (Met00100.RepeatEndType.Equals("After", StringComparison.OrdinalIgnoreCase))
                {
                    recurrenceRule += string.Format(";COUNT={0}", Met00100.RepeatEndAfter);
                }
                else if (Met00100.RepeatEndType.Equals("On", StringComparison.OrdinalIgnoreCase))
                {
                    recurrenceRule += string.Format(";UNTIL={0}", Convert.ToDateTime(Met00100.RepeatEndOn).ToString("yyyyMMdd"));
                }
            }
            catch (Exception ex)
            {
                recurrenceRule = "";
            }
            return recurrenceRule;
        }

        public static string GetMonthlyRecurrencRule(Met00100 Met00100)
        {
            string recurrenceRule = "", FREQ="MONTHLY", BYDAY="";
            int MonthDayOccurence = 0;

            try
            {
                if (Met00100.RepeatEvery == 1)
                    recurrenceRule = string.Format("FREQ={0}", FREQ);
                else if (Met00100.RepeatEvery > 1)
                {
                    recurrenceRule = string.Format("FREQ={0};INTERVAL={1}", FREQ, Met00100.RepeatEvery);
                }

                if (Met00100.RepeatTypeID == 1)
                {
                    recurrenceRule += string.Format(";BYMONTHDAY={0}", Met00100.MonthRepeatOn);
                }
                else if (Met00100.RepeatTypeID == 2)
                {
                    if (Met00100.MonthRepeatWeekDay == -2)
                    {
                        MonthDayOccurence = Convert.ToInt32(Met00100.MonthDayOccurence);
                        BYDAY = "SU,MO,TU,WE,TH,FR,SA";

                        recurrenceRule += string.Format(";BYDAY={0};BYSETPOS={1}", BYDAY, MonthDayOccurence);
                    }
                    else if (Met00100.MonthRepeatWeekDay == -1)
                    {
                        MonthDayOccurence = Convert.ToInt32(Met00100.MonthDayOccurence);
                        BYDAY = "MO,TU,WE,TH,FR";

                        recurrenceRule += string.Format(";BYDAY={0};BYSETPOS={1}", BYDAY, MonthDayOccurence);
                    }
                    else if (Met00100.MonthRepeatWeekDay == 0)
                    {
                        MonthDayOccurence = Convert.ToInt32(Met00100.MonthDayOccurence);
                        BYDAY = "SU,SA";

                        recurrenceRule += string.Format(";BYDAY={0};BYSETPOS={1}", BYDAY, MonthDayOccurence);
                    }
                    else if(Met00100.MonthRepeatWeekDay>=1&& Met00100.MonthRepeatWeekDay <= 7)
                    {
                        MonthDayOccurence = Convert.ToInt32(Met00100.MonthDayOccurence);

                        #region generate month week day occurence
                        switch (Met00100.MonthRepeatWeekDay)
                        {
                            case 1:
                                recurrenceRule += string.Format(";BYDAY={0}{1}", MonthDayOccurence,"SU");
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

                if (Met00100.RepeatEndType.Equals("After", StringComparison.OrdinalIgnoreCase))
                {
                    recurrenceRule += string.Format(";COUNT={0}", Met00100.RepeatEndAfter);
                }
                else if (Met00100.RepeatEndType.Equals("On", StringComparison.OrdinalIgnoreCase))
                {
                    recurrenceRule += string.Format(";UNTIL={0}", Convert.ToDateTime(Met00100.RepeatEndOn).ToString("yyyyMMdd"));
                }
            }
            catch (Exception ex)
            {
                recurrenceRule = "";
            }
            return recurrenceRule;
        }

        public static string GetYearlyRecurrencRule(Met00100 Met00100)
        {
            string recurrenceRule = "", FREQ="YEARLY", BYDAY="";
            int YearMonthDayOccurence = 0;

            try
            {
                if (Met00100.RepeatEvery == 1)
                    recurrenceRule = string.Format("FREQ={0}", FREQ);
                else if (Met00100.RepeatEvery > 1)
                {
                    recurrenceRule = string.Format("FREQ={0};INTERVAL={1}", FREQ, Met00100.RepeatEvery);
                }

                if (Met00100.RepeatTypeID == 3)
                {
                    recurrenceRule += string.Format(";BYMONTH={0};BYMONTHDAY={1}", Met00100.YearRepeatMonth, Met00100.YearRepeatOn);
                }
                else if (Met00100.RepeatTypeID == 4)
                {
                    recurrenceRule+= string.Format(";BYMONTH={0}", Met00100.YearRepeatMonth);

                    if (Met00100.YearRepeatMonthDay == -2)
                    {
                        YearMonthDayOccurence = Convert.ToInt32(Met00100.YearMonthDayOccurence);
                        BYDAY = "SU,MO,TU,WE,TH,FR,SA";

                        recurrenceRule += string.Format(";BYDAY={0};BYSETPOS={1}", BYDAY, YearMonthDayOccurence);
                    }
                    else if (Met00100.YearRepeatMonthDay == -1)
                    {
                        YearMonthDayOccurence = Convert.ToInt32(Met00100.YearMonthDayOccurence);
                        BYDAY = "MO,TU,WE,TH,FR";

                        recurrenceRule += string.Format(";BYDAY={0};BYSETPOS={1}", BYDAY, YearMonthDayOccurence);
                    }
                    else if (Met00100.YearRepeatMonthDay == 0)
                    {
                        YearMonthDayOccurence = Convert.ToInt32(Met00100.YearMonthDayOccurence);
                        BYDAY = "SU,SA";

                        recurrenceRule += string.Format(";BYDAY={0};BYSETPOS={1}", BYDAY, YearMonthDayOccurence);
                    }
                    else if (Met00100.YearRepeatMonthDay >= 1 && Met00100.YearRepeatMonthDay <= 7)
                    {
                        YearMonthDayOccurence = Convert.ToInt32(Met00100.YearMonthDayOccurence);

                        #region generate month week day occurence
                        switch (Met00100.YearRepeatMonthDay)
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

                if (Met00100.RepeatEndType.Equals("After", StringComparison.OrdinalIgnoreCase))
                {
                    recurrenceRule += string.Format(";COUNT={0}", Met00100.RepeatEndAfter);
                }
                else if (Met00100.RepeatEndType.Equals("On", StringComparison.OrdinalIgnoreCase))
                {
                    recurrenceRule += string.Format(";UNTIL={0}", Convert.ToDateTime(Met00100.RepeatEndOn).ToString("yyyyMMdd"));
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