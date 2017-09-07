using System;
using System.Collections.Generic;
using System.Linq;


using Kaizen.Configuration;
using Kaizen.Configuration.Repository;

namespace Kaizen.BusinessLogic.Configuration
{
    public class UserServices
    {
        #region Infrastructure

        private UserRepository _UserRepository;
        private KaizenSession UserContext;
        public UserServices()
        {
            _UserRepository = new UserRepository();
        }
        public UserServices(KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _UserRepository = new UserRepository(UserContext.UserName, UserContext.Password);
        }
        #endregion
        public DataCollection<User> GetAllViewBYSQLQuery(string Filters, string Searchcritaria,
int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }
            DataCollection<User> result = null;
            result = _UserRepository.GetWhereListWithPaging("Users", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<User> GetAllViewBYSQLQuery(string Filters,
    string FieldID, int FltrOperator, string Searchcritaria,
    int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }
            else
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = " and ";
                if (FieldID == "-1")
                {
                    SeachStr += Help.GetFilter("UserName", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("FirstName", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("LastName", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                }
                else
                    SeachStr = Help.GetFilter(FieldID, (KaizenFilterOperator)FltrOperator, Searchcritaria);
            }

            DataCollection<User> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _UserRepository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _UserRepository.GetWhereListWithPaging("[Users]", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }


        public List<User> GetAllUsers()
        {
            var tasks = _UserRepository.GetAll();
            List<User> result = tasks.ToList();
            return result;
        }

        public DataCollection<User> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, int SortDirection)
        {
            IList<User> L = null;
            var tasks = _UserRepository.GetWhereListWithPaging(sqlenquiry, PageSize, Page, ss => Member, bool.Parse(SortDirection.ToString()));
            DataCollection<User> result = tasks;
            return result;
        }
        public DataCollection<User> GetListWithPaging(int PageSize, int Page, string Member, int SortDirection)
        {
            DataCollection<User> result = null;
            var tasks = _UserRepository.GetListWithPaging(PageSize, Page, ss => Member);
            result = tasks;
            return result;
        }
        public DataCollection<User> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, string SortDirection)
        {
            IList<User> L = null;
            var tasks = _UserRepository.GetListWithPaging(sqlenquiry, PageSize, Page, ss => Member, ss => SortDirection
                , xx => xx.UserName);
            DataCollection<User> result = tasks;
            return result;
        }
        public DataCollection<User> GetListWithPaging(string SearchTerm, int PageSize, int Page, string Member, string SortDirection)
        {
            DataCollection<User> result = null;
            var tasks = _UserRepository.GetListWithPaging(x => x.UserName.Contains(SearchTerm) ||
                x.FirstName.Contains(SearchTerm)
                , PageSize, Page, ss => new { ss.UserName }, null);
            result = tasks;
            return result;
        }
        public DataCollection<User> GetListWithPaging(int PageSize, int Page, string Member, string SortDirection)
        {
            DataCollection<User> result = null;
            var tasks = _UserRepository.GetListWithPaging(PageSize, Page, ss => new { ss.UserName }, null);
            result = tasks;
            return result;
        }
        public User GetSingle(string UserName)
        {
            var tasks = _UserRepository.GetSingle(x => x.UserName.Trim() == UserName.Trim());
            return tasks;
        }
        public User GetSingleFromKaizen(string UserName)
        {
            UserRepository rep = new UserRepository();
            var tasks = rep.GetSingleFromKaizen(x => x.UserName.Trim() == UserName.Trim());
            return tasks;
        }
        public User GetSingleWithInclude(string UserName)
        {
            var tasks = _UserRepository.GetSingle(x => x.UserName.Trim() == UserName.Trim(), x => x.Kaizen00020);
            return tasks;
        }
        public KaizenResult AddUser(User newTask)
        {
            string UserPassword = newTask.UserPassword;
            CryptoGrapher cr = new CryptoGrapher(CryptoGrapher.CryptoEnum.DES);
            newTask.UserPassword = cr.Encrypting(UserPassword);
            var result = _UserRepository.AddKaizenObject(newTask);
            if (result.Status)
            {
                _UserRepository.ExecuteSqlCommandFromKaizen("CREATE LOGIN [" + newTask.UserName.Trim() + "] WITH PASSWORD = N'" + UserPassword + "', DEFAULT_DATABASE = Kaizen , DEFAULT_LANGUAGE = [us_english], CHECK_EXPIRATION = OFF, CHECK_POLICY = OFF;");
                //_UserRepository.ExecuteSqlCommandFromSystem("USE master DENY VIEW ANY DATABASE TO " + newTask.UserName.Trim() + ";");
                //_UserRepository.ExecuteSqlCommandFromKaizen("ALTER AUTHORIZATION ON DATABASE::Kaizen TO " + newTask.UserName.Trim() + ";");
                _UserRepository.ExecuteSqlCommandFromKaizen("USE Kaizen CREATE USER [" + newTask.UserName.Trim() + "] FOR LOGIN [" + newTask.UserName.Trim() + "];");
                _UserRepository.ExecuteSqlCommandFromKaizen("USE Kaizen EXEC sp_addrolemember N'db_owner', N'" + newTask.UserName.Trim() + "';");

            }
            return result;
        }
        public KaizenResult CompanyChangePassword(string UserName, string UserPassword)
        {
            User temp = this.GetSingleFromKaizen(UserName);
            CryptoGrapher cr = new CryptoGrapher(CryptoGrapher.CryptoEnum.DES);
            temp.UserPassword = cr.Encrypting(UserPassword);
            var result = _UserRepository.UpdateKaizenObject(temp);
            return result;
        }
        public KaizenResult CompanyChangePassword(User UpdateUser)
        {
            CryptoGrapher cr = new CryptoGrapher(CryptoGrapher.CryptoEnum.DES);
            UpdateUser.UserPassword = cr.Encrypting(UpdateUser.UserPassword);
            //User temp = this.GetSingle(UpdateUser.UserName);
            //UpdateUser.UserPassword = temp.UserPassword;
            var result = _UserRepository.UpdateKaizenObject(UpdateUser);
            return result;
        }
        public KaizenResult Update(User UpdateUser)
        {
            //CryptoGrapher cr = new CryptoGrapher(CryptoGrapher.CryptoEnum.DES);
            //UpdateUser.UserPassword = cr.Encrypting(UpdateUser.UserPassword);
            User temp = this.GetSingle(UpdateUser.UserName);
            UpdateUser.UserPassword = temp.UserPassword;
            var result = _UserRepository.UpdateKaizenObject(UpdateUser);
            return result;
        }
        public KaizenResult UpdateNameFromKaizen(User UpdateUser)
        {
            UserRepository rep = new UserRepository();
            User temp = rep.GetSingleFromKaizen(ss => ss.UserName == UpdateUser.UserName.Trim());
            temp.FirstName = UpdateUser.FirstName;
            temp.LastName = UpdateUser.LastName;
            var result = _UserRepository.UpdateKaizenObjectFromKaizen(temp);
            return result;
        }
        public KaizenResult UpdateProfile(User UpdateUser)
        {
            var result = _UserRepository.UpdateWithPropertiesKaizenObject(UpdateUser, ss => ss.FirstName, ss => ss.LastName, ss => ss.PhotoExtension);
            return result;
        }
        public KaizenResult Delete(string UserName)
        {
            _UserRepository.ExecuteSqlCommandFromSystem("USE Kaizen DROP USER " + UserName.Trim());
            CompanyAccessRepository rr = new CompanyAccessRepository(this.UserContext.UserName, UserContext.Password);
            List<CompanyAccess> L = rr.GetAll(ss => ss.UserName == UserName.Trim()).ToList();
            foreach (CompanyAccess o in L)
            {
                _UserRepository.ExecuteSqlCommandFromSystem("USE " + o.CompanyID + " DROP USER " + UserName.Trim());
                _UserRepository.ExecuteSqlCommand("delete CompanyAccess where UserName='" + UserName.Trim() + "' and CompanyID='" + o.CompanyID + "'");
            }
            var result = _UserRepository.RemoveKaizenObject(xx => xx.UserName == UserName);
            return result;
        }

        public List<KaizenSession> GetCurrentUserLog()
        {
            KaizenSessionRepository rep = new KaizenSessionRepository(UserContext.UserName, UserContext.Password);
            List<KaizenSession> L = rep.GetAll(ss => ss.UserName == UserContext.UserName && ss.CompanyID == UserContext.CompanyID, ss => new { ss.LoginDate }).ToList();
            return L;
        }
        public List<Kaizen00400> GetChattingMessage(string UserName, string UserNameTo)
        {
            CHattingMessageRepository rep = new CHattingMessageRepository(UserContext.UserName, UserContext.Password);
            List<Kaizen00400> L = rep.GetAll(ss => ss.UserName == UserContext.UserName && ss.UserNameTo == UserNameTo
            , ss => new { ss.MessageDate }).ToList();

            return L;
        }
        public bool AddChattingMessage(string UserName, string UserNameTo, string KaizenMessageLine)
        {
            CHattingMessageRepository rep = new CHattingMessageRepository(UserContext.UserName, UserContext.Password);
            Kaizen00400 o = new Kaizen00400();
            o.UserName = UserName;
            o.UserNameTo = UserNameTo;
            o.MessageDate = DateTime.Now;
            o.KaizenMessageLine = KaizenMessageLine;
            o.IsReceived = false;
            rep.Add(o);
            o = new Kaizen00400();
            o.UserName = UserNameTo;
            o.UserNameTo = UserName;
            o.MessageDate = DateTime.Now;
            o.KaizenMessageLine = KaizenMessageLine;
            o.IsReceived = true;
            rep.Add(o);
            return true;
        }
        //public string HashPassword(string password)
        //{
        //    return BCrypt.Net.BCrypt.HashPassword(password);
        //}

        public bool Verify(string password, string CrypPassword)
        {
            CryptoGrapher cr = new CryptoGrapher(CryptoGrapher.CryptoEnum.DES);
            string UserPassword = cr.Decrypting(CrypPassword);
            if (UserPassword == password)
                return true;
            else
                return false;
        }
        public bool Authenticate(string userName, string password)
        {
            User U = _UserRepository.GetSingleFromKaizen(x => x.UserName.Trim() == userName.Trim());
            if (U == null || U.IsDisabled)
                return false;
            if (Verify(password, U.UserPassword))
                return true;
            return false;
        }
        public bool AuthenticateUser(string userName, string password)
        {
            User U = _UserRepository.GetSingleFromKaizen(x => x.UserName.Trim() == userName.Trim());
            if (U == null)
                throw new Exception("Invalid User Name");
            else if (U.IsDisabled)
                throw new Exception("User Account is not active");
            else if (Verify(password, U.UserPassword) == false)
                throw new Exception("Invalid Password");

            if (Verify(password, U.UserPassword))
                return true;

            return false;
        }
        public string PasswordEncryption(string password)
        {
            CryptoGrapher cr = new CryptoGrapher(CryptoGrapher.CryptoEnum.DES);
            return cr.Encrypting(password);
        }
        public string PasswordDecryption(string password)
        {
            CryptoGrapher cr = new CryptoGrapher(CryptoGrapher.CryptoEnum.DES);
            return cr.Decrypting(password);
        }

        public List<Kaizen004View> GetMyMenue()
        {
            Kaizen004ViewRepository Repository = new Kaizen004ViewRepository(UserContext.UserName, UserContext.Password);
            return Repository.GetList(xx => xx.UserName.Trim() == this.UserContext.UserName.Trim()
            && xx.CompanyID == "Greenology"
            ).ToList();
        }
        public static void RemoveSessionByKaizenPublicKey(string KaizenPublicKey)
        {
            //Master.RemoveSessionByKaizenID(ConnectionId);
            //Master.OnlineKaizenUser.Remove(UserKaizen);
            UserRepository logsrv = new UserRepository();
            logsrv.ExecuteSqlCommandFromKaizen("update KaizenSession set LoginDateOut = getdate() where KaizenPublicKey = '" +
               KaizenPublicKey + "'");
        }
        //public static void RemoveSessionByConnectionId(Guid KaizenPublicKey)
        //{
        //    UserRepository logsrv = new UserRepository();
        //    logsrv.ExecuteSqlCommandFromKaizen("update KaizenSession set LoginDateOut = getdate() where KaizenPublicKey='" +
        //       KaizenPublicKey.ToString() + "'");
        //}

        public void CreateSession(KaizenSession UserContext)
        {
            KaizenSessionServices logsrv = new KaizenSessionServices();
            KaizenSession UserKaizen = new KaizenSession()
            {
                CompanyID = UserContext.CompanyID,
                KaizenPublicKey = UserContext.KaizenPublicKey,
                UserName = UserContext.UserName,
                LoginDate = DateTime.Now
            };
            logsrv.AddSession(UserKaizen);
            if (Master.OnlineKaizenUser.FindAll(ss => ss.UserName == UserKaizen.UserName && ss.CompanyID == UserKaizen.CompanyID).Count == 0)
                Master.OnlineKaizenUser.Add(UserContext);
            _UserRepository.ExecuteSqlCommand("update Users set LastLogin = getdate() where UserName = '" + UserKaizen.UserName.Trim() + "'");
            User Usr = this.GetSingleFromKaizen(UserContext.UserName);
            UserContext.FirstName = Usr.FirstName;
            UserContext.LastName = Usr.LastName;
            //UserContext.UserRole = Usr.UserRole;
            UserContext.PhotoExtension = Usr.PhotoExtension;
            CompanyAccessServices serv = new CompanyAccessServices();
            CompanyAccess oCompanyAccess = serv.GetSingleFromKaizen(UserContext.CompanyID, UserContext.UserName);
            Company com = Master.InstalledCompany.Find(ss => ss.CompanyID.Trim() == UserContext.CompanyID.Trim());
            UserContext.CompanyName = com.CompanyName;
            UserContext.SegmentMark = com.SegmentMark;
            UserContext.GLFormat = com.GLFormat;
            //UserContext.AgentID = oCompanyAccess.AgentID;
            //if (string.IsNullOrEmpty(oCompanyAccess.DashboardCode))
            //    UserContext.DashboardCode = string.Empty;
            //else
            //    UserContext.DashboardCode = oCompanyAccess.DashboardCode.Trim();


            if (oCompanyAccess.CurrentDate.HasValue)
                UserContext.CurrentDate = oCompanyAccess.CurrentDate;
            else
                UserContext.CurrentDate = com.CurrentDate;

            if (oCompanyAccess.YearCode.HasValue)
                UserContext.YearCode = oCompanyAccess.YearCode;
            else
                UserContext.YearCode = com.YearCode;

            if (string.IsNullOrEmpty(oCompanyAccess.CurrencyCode))
                UserContext.CurrencyCode = com.CurrencyCode;
            else
                UserContext.CurrencyCode = oCompanyAccess.CurrencyCode;

            if (string.IsNullOrEmpty(oCompanyAccess.ExchangeTableID))
                UserContext.ExchangeTableID = com.ExchangeTableID;
            else
                UserContext.ExchangeTableID = oCompanyAccess.ExchangeTableID;

            if (oCompanyAccess.DecimalDigit.HasValue)
                UserContext.DecimalDigit = oCompanyAccess.DecimalDigit;
            else
                UserContext.DecimalDigit = com.DecimalDigit;

            if (oCompanyAccess.ExchangeRateID.HasValue)
                UserContext.ExchangeRateID = oCompanyAccess.ExchangeRateID;
            else
                UserContext.ExchangeRateID = com.ExchangeRateID;

            if (oCompanyAccess.IsMultiply.HasValue)
                UserContext.IsMultiply = oCompanyAccess.IsMultiply;
            else
                UserContext.IsMultiply = com.IsMultiply;

            if (oCompanyAccess.ExchangeRate.HasValue)
                UserContext.ExchangeRate = oCompanyAccess.ExchangeRate;
            else
                UserContext.ExchangeRate = com.ExchangeRate;

            if (oCompanyAccess.CurrentDate.HasValue)
                UserContext.CurrentDate = oCompanyAccess.CurrentDate;
            else
                UserContext.CurrentDate = com.CurrentDate;

            if (string.IsNullOrEmpty(oCompanyAccess.CheckbookCode))
                UserContext.CheckbookCode = com.CheckbookCode;
            else
                UserContext.CheckbookCode = oCompanyAccess.CheckbookCode.Trim();

            if (string.IsNullOrEmpty(oCompanyAccess.SOPTypeSetupID))
                UserContext.SOPTypeSetupID = com.SOPTypeSetupID;
            else
                UserContext.SOPTypeSetupID = oCompanyAccess.SOPTypeSetupID;
            //---
            if (oCompanyAccess.SOPTYPE.HasValue)
                UserContext.SOPTYPE = oCompanyAccess.SOPTYPE;
            else
                UserContext.SOPTYPE = com.SOPTYPE;

            if (string.IsNullOrEmpty(oCompanyAccess.SiteID))
                UserContext.SiteID = com.SiteID;
            else
                UserContext.SiteID = oCompanyAccess.SiteID;

        }
        public static KaizenSession GetMySession(string UserName)
        {
            if (string.IsNullOrEmpty(UserName)) return null;
            KaizenSession temp = Master.OnlineKaizenUser.Find(ee => ee.UserName == UserName);
            return temp;
        }
        public static KaizenSession GetKaizenSession(string KaizenPublicKey)
        {
            if (string.IsNullOrEmpty(KaizenPublicKey)) return null;
            if (Master.OnlineKaizenUser == null) return null;
            KaizenSession temp = Master.OnlineKaizenUser.Find(ee => ee.KaizenPublicKey == Guid.Parse(KaizenPublicKey));
            return temp;
        }
        public static KaizenSession UpdateKaizenSession(KaizenSession updatedObject)
        {
            if (updatedObject == null)
                return null;

            var KaizenPublicKey = updatedObject.KaizenPublicKey;
            if (Guid.Empty == KaizenPublicKey) return null;
            if (Master.OnlineKaizenUser == null) return null;

            var users = Master.OnlineKaizenUser;
            var oldUser = users.Find(ee => ee.KaizenPublicKey == KaizenPublicKey);
            oldUser = updatedObject;
            Master.OnlineKaizenUser = users;

            return GetKaizenSession(KaizenPublicKey.ToString());
        }

        public static KaizenSession GetKaizenSession(string CompanyID, string UserName)
        {
            KaizenSession temp = GetMySessions(UserName).Find(ee => ee.CompanyID == CompanyID);
            return temp;
        }
        public static KaizenSession GetFreeKaizenSession(string UserName)
        {
            KaizenSession temp = GetMySessions(UserName).Find(ee => string.IsNullOrEmpty(ee.CompanyID));
            return temp;
        }
        public static void RemoveFreeKaizenSession(string UserName)
        {
            KaizenSession UserKaizen = GetFreeKaizenSession(UserName);
            Master.OnlineKaizenUser.Remove(UserKaizen);
        }
        public static List<KaizenSession> GetUserSessions(string UserName)
        {
            List<KaizenSession> temp = Master.OnlineKaizenUser.FindAll(ee => ee.UserName.Trim() == UserName.Trim());
            return temp;
        }
        public static KaizenSession GetUserSession(string UserName, string CompanyID)
        {
            KaizenSession temp = GetUserSessions(UserName).Find(ss => ss.CompanyID.Trim() == CompanyID.Trim());
            return temp;
        }
        public static KaizenSession GetMySession(string CompanyID, string username)
        {
            KaizenSession temp = GetMySessions(username).Find(ee => ee.CompanyID == CompanyID);
            return temp;
        }
        public static List<KaizenSession> GetMySessions(string userName)
        {
            List<KaizenSession> temp = GetUserSessions(userName);
            return temp;
        }
        public static List<KaizenSession> GetSessions(string CompanyID)
        {
            List<KaizenSession> temp = Master.OnlineKaizenUser.FindAll(ee => ee.CompanyID == CompanyID);
            return temp;
        }

    }
}
