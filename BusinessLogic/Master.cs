using Kaizen.Configuration;
using Kaizen.Configuration.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Kaizen.BusinessLogic
{
    public static class Master
    {
        private static List<Kaizen00001> CMS_Case = new List<Kaizen00001>();
        private static List<Kaizen00001> CMS_CaseAssign = new List<Kaizen00001>();
        public static List<CM00000> _cMSConfiguration = null;
        public static CM00000 GetCMSConfiguration(string CompanyID)
        {
            if (_cMSConfiguration == null)
            {
                Configuration.CM00000Services service = new Configuration.CM00000Services();
                _cMSConfiguration = service.GetAll().ToList();
            }
            return _cMSConfiguration.Find(ss => ss.CompanyID == CompanyID);
        }
        public static string ReportConnectionString(string CompanyID)
        {
            string ConnectionString = "Password=XXXX;Persist Security Info=True;User ID=wael;initial catalog=TWO;Data Source=SSSS";
            CryptoGrapher cr = new CryptoGrapher(CryptoGrapher.CryptoEnum.DES);
            string UserPassword = cr.Decrypting(System.Configuration.ConfigurationManager.AppSettings["XXXX"]);
            ConnectionString = ConnectionString.Replace("XXXX", UserPassword);
            ConnectionString = ConnectionString.Replace("TWO", CompanyID.Trim());
            string SSSS = System.Configuration.ConfigurationManager.AppSettings["SSSS"].ToString();
            ConnectionString = ConnectionString.Replace("SSSS", SSSS.Trim());
            return ConnectionString;
        }
        public static List<Kaizen00001> GetScreenFields(string KaizenPublicKey, int ScreenID)
        {
            KaizenSession User = Kaizen.BusinessLogic.Configuration.UserServices.GetKaizenSession(KaizenPublicKey);
            Configuration.Kaizen00001Services srv = new Configuration.Kaizen00001Services(User);
            switch (ScreenID)
            {
                case (int)Screen.CMS_Case:
                    CMS_Case = srv.GetAllByScreen(ScreenID);
                    return CMS_Case;
                case (int)Screen.CMS_CaseAssign:
                    CMS_CaseAssign = srv.GetAllByScreen(ScreenID);
                    return CMS_CaseAssign;
            }
            return null;
        }
        public static string GetField(List<Kaizen00001> Kaizen00001, int FieldID)
        {
            return Kaizen00001.Find(ss => ss.FieldID == FieldID).FieldName;
        }

        public static string GetFieldText(List<Kaizen00001> Kaizen00001, int FieldID)
        {
            return Kaizen00001.Find(ss => ss.FieldID == FieldID).FieldName;
        }
        public static int GetFieldIsDynamicTable(List<Kaizen00001> Kaizen00001, int FieldID)
        {
            bool temp = Kaizen00001.Find(ss => ss.FieldID == FieldID).IsDynamicTable;
            if (temp)
                return FieldID;
            else
                return 0;
        }
        #region  Login 
        //private static List<KaizenSession> LogedUsers;
        public static List<KaizenSession> OnlineKaizenUser
        {
            get
            {
                //if (LogedUsers == null)
                //    LogedUsers = new List<KaizenSession>();
                //return LogedUsers;
                if (System.Web.HttpContext.Current.Application["LogedUsers"] == null)
                    System.Web.HttpContext.Current.Application["LogedUsers"] = new List<User>();
                return (List<KaizenSession>)System.Web.HttpContext.Current.Application["LogedUsers"];
            }
            set
            {
                //LogedUsers = value;
                System.Web.HttpContext.Current.Application["LogedUsers"] = value;
            }
        }
        public static int GetLogedUsersCount()
        {
            return OnlineKaizenUser.Count;
        }
        public static bool RemoveSessionByKaizenID(string Kaizen)
        {
            if (string.IsNullOrEmpty(Kaizen)) return false;
            bool temp = OnlineKaizenUser.Remove(OnlineKaizenUser.Find(ee => ee.KaizenPublicKey == Guid.Parse(Kaizen)));
            return temp;
        }
        public static bool RemoveSessionByUserName(string UserName)
        {
            if (string.IsNullOrEmpty(UserName)) return false;
            bool temp = OnlineKaizenUser.Remove(OnlineKaizenUser.Find(ee => ee.UserName == UserName));
            return temp;
        }

        public static KaizenSession GetKaizenSessionByConnectionId(string ConnectionId)
        {
            if (string.IsNullOrEmpty(ConnectionId)) return null;
            KaizenSession temp = OnlineKaizenUser.Find(ee => ee.ConnectionIds.Contains(ConnectionId));
            return temp;
        }
        public static bool RemoveSessionByConnectionId(string ConnectionId)
        {
            if (string.IsNullOrEmpty(ConnectionId)) return false;
            KaizenSession tempUser = GetKaizenSessionByConnectionId(ConnectionId);
            if (tempUser == null) return false;
            return OnlineKaizenUser.Remove(tempUser);
        }
        // LoginToCompany -- AuthenticationController
        public static string GetMyPhoto(User U)
        {
            if (U == null)
                return "EmpCard.jpg";
            else if (string.IsNullOrEmpty(U.PhotoExtension))
                return "EmpCard.jpg";
            else
                return U.UserName.Trim() + "." + U.PhotoExtension.Trim();
        }
        public static string GetMyPhoto(string UserName, string PhotoExtension)
        {
            if (string.IsNullOrEmpty(PhotoExtension))
                return "EmpCard.jpg";
            else if (string.IsNullOrEmpty(UserName))
                return "EmpCard.jpg";
            else
                return UserName.Trim() + "." + PhotoExtension.Trim();
        }



        #endregion
        public static string AccountFormat(string CompanyID, string Account)
        {
            string result = string.Empty;
            Company comp = InstalledCompany.Find(xx => xx.CompanyID.Trim() == CompanyID.Trim());
            foreach (CompanySegment seg in comp.CompanySegments)
            {
                string temp = Account.Substring(0, seg.SegmentLength);
                Account = Account.Substring(temp.Length);
                result += temp;
                if (seg.SegmentID != comp.CompanySegments.Last().SegmentID)
                {
                    result += comp.SegmentMark.Trim();
                }
            }
            //result = result.Substring(0, result.Length - 1);
            return result;
        }

        public static List<Company> InstalledCompany = new List<Company>();
        internal static List<Kaizen.GL.GL00102> InstalledCurrencyCode = new List<Kaizen.GL.GL00102>();


        private static List<Config00100> InventoryConfig = null;
        private static List<Config00001> ConfigHR = null;
        public static List<Kaizen000> InsalledModules;
        internal static List<Kaizen000> StandardModule;
        internal static List<KaizenTask> InsalledTask;
        internal static List<KaizenMenu> MenueTask;
        static Master()
        {
            OnlineKaizenUser = new List<KaizenSession>();
            Configuration.CompanyacessServices com = new Configuration.CompanyacessServices();
            InstalledCompany = com.GetAllCompanies();
            InsalledModules = new List<Kaizen000>();

            StandardModule = new List<Kaizen000>();
            InsalledTask = new List<KaizenTask>();
            MenueTask = new List<KaizenMenu>();
            //UtilitiesTask = new List<KaizenTask>();
            //ConfigurationTask = new List<KaizenTask>();

            StandardModule.Add(new Kaizen000() { ModuleID = (int)Module.Financial, ModuleName = "Financial" });
            StandardModule.Add(new Kaizen000() { ModuleID = (int)Module.FixedAssets, ModuleName = "Fixed Assets" });
            StandardModule.Add(new Kaizen000() { ModuleID = (int)Module.Purchase, ModuleName = "Purchase" });
            StandardModule.Add(new Kaizen000() { ModuleID = (int)Module.Sales, ModuleName = "Sales" });
            StandardModule.Add(new Kaizen000() { ModuleID = (int)Module.Inventory, ModuleName = "Inventory" });
            StandardModule.Add(new Kaizen000() { ModuleID = (int)Module.Project, ModuleName = "Project Accounting" });
            StandardModule.Add(new Kaizen000() { ModuleID = (int)Module.HumanResources, ModuleName = "Human Resources" });
            StandardModule.Add(new Kaizen000() { ModuleID = (int)Module.CRM, ModuleName = "Service Call Management" });
            // StandardModule.Add(new Kaizen000() { ModuleID = (int)Module.RelationshipManagement, ModuleName = "Customer Relationship Management" });
            StandardModule.Add(new Kaizen000() { ModuleID = (int)Module.CollectionManagementSystem, ModuleName = "Collection Management" });

            StandardModule.Add(new Kaizen000() { ModuleID = (int)Module.Admin, ModuleName = "Administrator" });
            StandardModule.Add(new Kaizen000() { ModuleID = (int)Module.Medical, ModuleName = "Medical" });
            StandardModule.Add(new Kaizen000() { ModuleID = (int)Module.PBX, ModuleName = "PBX" });
            StandardModule.Add(new Kaizen000() { ModuleID = (int)Module.Tools, ModuleName = "Tools" });
            StandardModule.Add(new Kaizen000() { ModuleID = (int)Module.Extender, ModuleName = "Extender" });
            Configuration.Kaizen000Services srv = new Configuration.Kaizen000Services();
            IList<Kaizen000> KaizenCompanyModuleList = srv.GetListFromKaizen();

            foreach (var o in KaizenCompanyModuleList.ToList())
            {
                int ModuleID = o.ModuleID;
                InsalledModules.Add(StandardModule.Find(ss => ss.ModuleID == o.ModuleID));
                continue;
                switch (ModuleID)
                {
                    case 6:
                        #region Payroll
                        MenueTask.Add(new KaizenMenu() { MenuID = 600, MenuName = "Project", ScreenPath = "Project_Project", ModuleID = 6, MenueType = MenueType.Common });
                        #endregion
                        break;
                    case 8:
                        break;
                    case 9:

                        MenueTask.Add(new KaizenMenu() { MenuID = 90, MenuName = "Bulk Email", ScreenPath = "Admin_MassageTemplate", ModuleID = 9, MenueType = MenueType.Utility });

                        break;
                    case 10:
                        #region CMS
                        MenueTask.Add(new KaizenMenu() { MenuID = 2, MenuName = "Case Item Transaction", ScreenPath = "CMS_CaseItem", ModuleID = 10, MenueType = MenueType.Transaction });
                        MenueTask.Add(new KaizenMenu() { MenuID = 5, MenuName = "Case Assignment", ScreenPath = "CMS_CaseAsgin", ModuleID = 10, MenueType = MenueType.Transaction });


                        //MenueTask.Add(new KaizenMenu() { MenuID = 16, MenuName = "Jeckets", ScreenPath = "CMS_Jeckets", ModuleID = 10, MenueType = MenueType.Common });

                        MenueTask.Add(new KaizenMenu() { MenuID = 10, MenuName = "Debtor Class", ScreenPath = "CMS_DebtorClass", ModuleID = 10, MenueType = MenueType.Configuration });
                        MenueTask.Add(new KaizenMenu() { MenuID = 11, MenuName = "Debtor Group", ScreenPath = "CMS_Group", ModuleID = 10, MenueType = MenueType.Configuration });
                        MenueTask.Add(new KaizenMenu() { MenuID = 12, MenuName = "Debtor Priority", ScreenPath = "CMS_Priority", ModuleID = 10, MenueType = MenueType.Configuration });
                        MenueTask.Add(new KaizenMenu() { MenuID = 14, MenuName = "Debtor Status", ScreenPath = "CMS_DebtorStatus", ModuleID = 10, MenueType = MenueType.Configuration });
                        MenueTask.Add(new KaizenMenu() { MenuID = 15, MenuName = "Debtor Address Type", ScreenPath = "CMS_DebtorAddressType", ModuleID = 10, MenueType = MenueType.Configuration });

                        MenueTask.Add(new KaizenMenu() { MenuID = 91, MenuName = "Contract Status", ScreenPath = "CMS_ContractStatus", ModuleID = 10, MenueType = MenueType.Configuration });
                        //MenueTask.Add(new KaizenMenu() { MenuID = 29, MenuName = "Case Type", ScreenPath = "CMS_CaseType", ModuleID = 10, MenueType = MenueType.Configuration });
                        //MenueTask.Add(new KaizenMenu() { MenuID = 24, MenuName = "Case Status", ScreenPath = "CMS_CaseStatus", ModuleID = 10, MenueType = MenueType.Configuration });
                        MenueTask.Add(new KaizenMenu() { MenuID = 25, MenuName = "Client Class", ScreenPath = "CMS_ClientClass", ModuleID = 10, MenueType = MenueType.Configuration });
                        MenueTask.Add(new KaizenMenu() { MenuID = 26, MenuName = "Client Status", ScreenPath = "CMS_ClientStatus", ModuleID = 10, MenueType = MenueType.Configuration });
                        MenueTask.Add(new KaizenMenu() { MenuID = 27, MenuName = "Client Address Type", ScreenPath = "CMS_ClientAddressType", ModuleID = 10, MenueType = MenueType.Configuration });
                        MenueTask.Add(new KaizenMenu() { MenuID = 28, MenuName = "Client Contact Type", ScreenPath = "CMS_ClientContactType", ModuleID = 10, MenueType = MenueType.Configuration });

                        #endregion 
                        break;
                }
            }
        }
        #region Menue
        public static List<Kaizen004View> GetMyKaizenMenu(List<Kaizen004View> Arr, int ModuleID, int MenueTypeID)
        {
            List<Kaizen004View> result = Arr.FindAll(ss => ss.ModuleID == ModuleID &&
            ss.MenueTypeID == MenueTypeID);
            return result;
        }

        //public static string GetPagePath(string ScreenPath)
        //{
        //    User U = GetKaizenSession(System.Web.HttpContext.Current.Request["KaizenPublicKey"]);
        //    if (U == null) return "";
        //    return "/" + ScreenPath.Trim() + "/Index?KaizenPublicKey=" + U.PublicKey.ToString();
        //}
        #endregion
        internal static bool IsHrInsalled()
        {
            Kaizen000 HR = InsalledModules.Find(ee => ee.ModuleID == 7);
            if (HR != null)
            {
                return true;
            }
            return false;
        }
        internal static Config00001 GetConfigHR(string CompanyID)
        {
            Config00001 _ConfigHR = Master.ConfigHR.Find(ss => ss.CompanyID.Trim() == CompanyID.Trim());
            if (_ConfigHR == null)
            {
                Kaizen000 HR = InsalledModules.Find(ee => ee.ModuleID == 7);
                if (HR != null)
                {
                    //Config00001Repository rep = new Config00001Repository("Kaizen");
                    //ConfigHR = rep.GetAll().ToList();
                    //_ConfigHR = ConfigHR.Find(ss => ss.CompanyID.Trim() == CompanyID.Trim());
                    //if (_ConfigHR == null)
                    //{
                    //    throw new Exception("HR Files are Missing");
                    //}
                }
                else
                {
                    throw new Exception("HR is not installed");
                }
            }
            return _ConfigHR;
        }
        internal static Config00100 GetInventoryConfig(string CompanyID)
        {
            Config00100 iInventoryConfig = Master.InventoryConfig.Find(ss => ss.CompanyID.Trim() == CompanyID.Trim());
            if (iInventoryConfig == null)
            {
                Kaizen000 Inventory = InsalledModules.Find(ee => ee.ModuleID == 5);
                if (Inventory != null)
                {
                    //Kaizen.Configuration.Repository.Config00100Repository rep = new Kaizen.Configuration.Repository.Config00100Repository("Kaizen");
                    //InventoryConfig = rep.GetAll().ToList();
                    //iInventoryConfig = Master.InventoryConfig.Find(ss => ss.CompanyID.Trim() == CompanyID.Trim());
                    //if (iInventoryConfig == null)
                    //{
                    //    throw new Exception("Inventory Files are Missing");
                    //}
                }
                else
                {
                    throw new Exception("Inventory is not installed");
                }
            }
            return iInventoryConfig;
        }
        public static string Specifier(int decimalDigit)
        {
            string Str = string.Empty;
            for (byte i = 1; i <= decimalDigit; i++)
            {
                Str = Str + "0";
            }
            string specifier = "#,###." + Str + ";(#,###." + Str + ")";
            return specifier;
        }

        //public static string DefualSpecifier()
        //{
        //    string Str = string.Empty;
        //    for (byte i = 1; i <= DecimalDigit; i++)
        //    {
        //        Str = Str + "0";
        //    }
        //    string specifier = "#,###." + Str + ";(#,###." + Str + ")";
        //    return specifier;
        //}
        public static List<Kaizen000> MyModule(List<Kaizen004View> MenueALL)
        {
            List<Kaizen000> result = new List<Kaizen000>();
            foreach (Kaizen004View o in MenueALL)
            {
                Kaizen000 temp = result.Find(ss => ss.ModuleID == o.ModuleID);
                if (temp == null)
                {
                    temp = InsalledModules.Find(ss => ss.ModuleID == o.ModuleID);
                    result.Add(temp);
                }

            }
            return result;
        }



        #region HR

        #endregion


    }

}
