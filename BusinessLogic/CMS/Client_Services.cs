using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.CMS.Repository;
using Kaizen.CMS;
using System.ComponentModel;

namespace Kaizen.BusinessLogic.CMS
{
    public class CM00110Services
    {
        #region Infrastructure

        private Kaizen.CMS.Repository.CM00110Repository _CM00110Repository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public CM00110Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _CM00110Repository = new CM00110Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion
        public DataCollection<CM00110> GetAllViewBYSQLQuery(string Filters, string Searchcritaria,
int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }

            DataCollection<CM00110> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _CM00110Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _CM00110Repository.GetWhereListWithPaging("CM00110", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }


        public DataCollection<CM00110> GetAllViewBYSQLQuery(string Filters,
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
                    SeachStr += Help.GetFilter("ClientID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("VendorID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("ClientName", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("ShortName", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("ClientDescription", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("ContactPerson", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("CUSTCLAS", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("StatusID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("ParentClientID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("AddressCode", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("BillAddressCode", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("RemitToAddressCode", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                }
                else
                    SeachStr = Help.GetFilter(FieldID, (KaizenFilterOperator)FltrOperator, Searchcritaria);
            }
            DataCollection<CM00110> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _CM00110Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _CM00110Repository.GetWhereListWithPaging("CM00110", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<CM00110> GetAllViewBYSQLQuery(string Filters,
string FieldID, int FltrOperator, string Searchcritaria, string CUSTCLAS,
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
                    SeachStr += Help.GetFilter("ClientID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("ClientName", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("ShortName", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("ClientDescription", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("ContactPerson", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("CUSTCLAS", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("StatusID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("ParentClientID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("AddressCode", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("BillAddressCode", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("RemitToAddressCode", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                }
                else
                    SeachStr = Help.GetFilter(FieldID, (KaizenFilterOperator)FltrOperator, Searchcritaria);
            }

            DataCollection<CM00110> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _CM00110Repository.GetListWithPaging(ss => ss.CUSTCLAS == CUSTCLAS, PageSize, Page, ss => Member);
            else
                result = _CM00110Repository.GetWhereListWithPaging("CM00110", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }


        public DataCollection<CM00110> GetAllViewBYSQLQuery(string WhereClause, int PageSize, int Page, string Member, ListSortDirection SortDirection)
        {
            CM00110Repository rep = new CM00110Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            DataCollection<CM00110> result = null;
            if (SortDirection == ListSortDirection.Ascending)
                result = rep.GetWhereListWithPaging(WhereClause, Page, PageSize, s => Member, true);
            else
                result = rep.GetWhereListWithPaging(WhereClause, Page, PageSize, s => Member, false);
            return result;
        }

        public DataCollection<CM00110> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, int SortDirection)
        {
            IList<CM00110> L = null;
            var tasks = _CM00110Repository.GetWhereListWithPaging(sqlenquiry, PageSize, Page, ss => Member, bool.Parse(SortDirection.ToString()));
            DataCollection<CM00110> result = tasks;
            return result;
        }
        public DataCollection<CM00110> GetListWithPaging(int PageSize, int Page, string Member, int SortDirection)
        {
            DataCollection<CM00110> result = null;
            var tasks = _CM00110Repository.GetListWithPaging(PageSize, Page, ss => Member, null);
            result = tasks;
            return result;
        }

        public DataCollection<CM_Client> GetAllBYSQLQuery(string Filters, string Searchcritaria, int PageSize, int Page, string Member, string SortDirection)
        {
            Kaizen.CMS.Repository.CM_ClientRepository _CM_ClientRepository = new CM_ClientRepository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            DataCollection<CM_Client> result = null;
            if (!string.IsNullOrEmpty(Filters))
            {
                Page = Page - 1;
                if (!string.IsNullOrEmpty(Searchcritaria))
                {
                    Filters += " and ClientID like '%" + Searchcritaria.Trim() + "%'";
                    Filters += " and ClientName like '%" + Searchcritaria.Trim() + "%'";
                    Filters += " and ClientDescription like '%" + Searchcritaria.Trim() + "%'";
                }
                string sql = "select  * from CM_Client";
                sql += " where " + Filters + " ORDER BY " + Member + " " + SortDirection + " OFFSET " + Page.ToString() + " ROWS FETCH NEXT " + PageSize.ToString() + " ROWS ONLY";
                var tasks = _CM_ClientRepository.GetSQLData(sql);
                if (tasks != null)
                {
                    tasks.TotalItemCount = tasks.Items.Count;
                    tasks.TotalPageCount = (int)Math.Ceiling((double)tasks.TotalItemCount / (double)PageSize);
                }
                result = tasks;
                return result;
            }
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                var tasks = _CM_ClientRepository.GetListWithPaging(PageSize, Page, ss => Member, ss => SortDirection);
                result = tasks;
            }
            else
            {
                var tasks = _CM_ClientRepository.GetListWithPaging(xx => xx.ClientName.Contains(Searchcritaria)
                    || xx.ClientDescription.Contains(Searchcritaria) || xx.ClientID.Contains(Searchcritaria)
                    , PageSize, Page, ss => Member, ss => SortDirection);
                result = tasks;
            }
            return result;
        }
        public DataCollection<CM00110> GetListWithPaging(int PageSize, int Page, string Member, string SortDirection)
        {
            DataCollection<CM00110> result = null;
            var tasks = _CM00110Repository.GetListWithPaging(PageSize, Page, ss => new { ss.ClientID });
            result = tasks;
            return result;
        }
        public DataCollection<CM_Client> GetAllBYSQLQueryView(string sqlenquiry, int PageSize, int Page, string Member, string SortDirection)
        {
            Kaizen.CMS.Repository.CM_ClientRepository _CM_ClientRepository = new CM_ClientRepository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            var tasks = _CM_ClientRepository.GetListWithPaging(sqlenquiry, PageSize, Page, ss => Member, ss => SortDirection);
            DataCollection<CM_Client> result = tasks;
            return result;
        }
        public DataCollection<CM_Client> GetSQLData(string Searchcritaria, int PageSize, int Page, string Member, string SortDirection)
        {
            Page = Page - 1;
            DataCollection<CM_Client> result = null;

            Kaizen.CMS.Repository.CM_ClientRepository _CM00100ViewRepository = new CM_ClientRepository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            string sql = "select * from CM_Client " + Searchcritaria + " ORDER BY " + Member + " " + SortDirection + " OFFSET " + Page.ToString() + " ROWS FETCH NEXT " + PageSize.ToString() + " ROWS ONLY";
            var tasks = _CM00100ViewRepository.GetSQLData(sql);
            if (tasks != null)
            {
                if (string.IsNullOrEmpty(Searchcritaria))
                    tasks.TotalItemCount = (int)_CM00100ViewRepository.Count();
                else
                    tasks.TotalItemCount = _CM00100ViewRepository.GetSQLINT("select count(ClientID) from CM_Client " + Searchcritaria);
                tasks.TotalPageCount = (int)Math.Ceiling((double)tasks.TotalItemCount / (double)PageSize);
            }
            result = tasks;
            return result;
        }
        public IList<CM00110> GetAll()
        {
            var tasks = _CM00110Repository.GetAll();
            IList<CM00110> result = tasks;
            return result;
        }
        public IList<CM00022> GetAllClientStatus()
        {
            Kaizen.CMS.Repository.CM00022Repository _CM00022Repository = new CM00022Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            var tasks = _CM00022Repository.GetAll();
            IList<CM00022> result = tasks;
            return result;
        }

        public CM00110 GetSingle(string ClientID)
        {
            var tasks = _CM00110Repository.GetSingle(x => x.ClientID == ClientID);
            if (tasks.AddressCode != null)
            {
                CM00111Repository _CM00111Repository = new CM00111Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
                tasks.MainAddress = _CM00111Repository.GetSingle(ss => ss.AddressCode == tasks.AddressCode && ss.ClientID == ClientID);
            }
            if (tasks.BillAddressCode != null)
            {
                CM00111Repository _CM00111Repository = new CM00111Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
                tasks.BillAddress = _CM00111Repository.GetSingle(ss => ss.AddressCode == tasks.BillAddressCode && ss.ClientID == ClientID);
            }
            if (tasks.RemitToAddressCode != null)
            {
                CM00111Repository _CM00111Repository = new CM00111Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
                tasks.RemitAddress = _CM00111Repository.GetSingle(ss => ss.AddressCode == tasks.RemitToAddressCode && ss.ClientID == ClientID);
            }
            return tasks;
        }
        public KaizenResult AddCM00110(CM00110 newTask)
        {
            var result = _CM00110Repository.AddKaizenObject(newTask);
            Kaizen.BusinessLogic.Configuration.CompanyacessServices srv = new Configuration.CompanyacessServices(UserContext);

            Kaizen.BusinessLogic.CMS.CM00021Services srvclientClass = new CM00021Services(UserContext);
            srvclientClass.ExecuteSqlCommand("update CM00021 set LastClientID = " + srv.GetNextClientKaizenID(newTask.CUSTCLAS.Trim())
                + " where CUSTCLAS = '" + newTask.CUSTCLAS.Trim() + "'");

            srv.DeleteNextClientID(newTask.CUSTCLAS.Trim());
            return result;
        }
        public KaizenResult Update(CM00110 UpdateCM00110)
        {
            var result = _CM00110Repository.UpdateKaizenObject(UpdateCM00110);
            return result;
        }
        public KaizenResult Delete(string ClientID)
        {
            var result = _CM00110Repository.RemoveKaizenObject(xx => xx.ClientID == ClientID);
            return result;
        }

        public string GetNextClient(string CUSTCLAS)
        {
            CM00021Services srv = new CM00021Services(this.UserContext);
            CM00021 classtemp = srv.GetSingle(CUSTCLAS);
            string SequenceName = "CMS_Client_" + classtemp.CUSTCLAS.Trim() + "_Sequence";
            int KaizenID = -1;
            Kaizen.Admin.Repository.Sys00000Repository Repository = new Kaizen.Admin.Repository.Sys00000Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            List<Kaizen.Admin.Sys00000> AllSessions = Repository.GetList(xx => xx.SequenceName.Trim() == SequenceName.Trim()).ToList();
            Kaizen.Admin.Sys00000 temp = AllSessions.Find(xx => xx.UserName.Trim() == UserContext.UserName.Trim());
            if (temp == null)
            {
                string SQL = string.Empty;
                foreach (Kaizen.Admin.Sys00000 row in AllSessions)
                {
                    Kaizen.Configuration.KaizenSession oKaizenSession = Configuration.UserServices.GetUserSession(row.UserName, UserContext.CompanyID);
                    if (oKaizenSession == null)
                    {
                        SQL = "update Sys00000 set UserName = '" + UserContext.UserName.Trim() + "' where UserName ='" +
                            row.UserName.TrimStart() + "' and SequenceName ='" + SequenceName + "'";
                        _CM00110Repository.ExecuteSqlCommand(SQL);
                        KaizenID = row.KaizenID;
                        break;
                    }
                    bool notUse = true;
                    foreach (Kaizen.Configuration.Screen scrn in oKaizenSession.Screens)
                    {
                        if (scrn == Kaizen.Configuration.Screen.GLTransaction)
                        {
                            notUse = false;
                            break;
                        }
                    }
                    if (notUse)
                    {
                        KaizenID = row.KaizenID;
                        break;
                    }
                }
            }
            else
                KaizenID = temp.KaizenID;
            ////-----------New ID-----------------------------------------------------
            if (KaizenID == -1)
            {
                string SQL = "SELECT NEXT VALUE FOR " + SequenceName;
                long? SequenceNumber = Repository.GetSQLLong(SQL);
                if (SequenceNumber.HasValue)
                {
                    KaizenID = (int)SequenceNumber.Value;
                    SQL = "insert into Sys00000(UserName,SequenceName,KaizenID) values (";
                    SQL += "'" + UserContext.UserName + "','" + SequenceName + "'," + SequenceNumber.ToString() + ");";
                    SQL += "update CM00021 set LastClientID =" + KaizenID.ToString() + " where CUSTCLAS='" + classtemp.CUSTCLAS.Trim() + "'";
                    Repository.ExecuteSqlCommand(SQL);
                }
                else
                    return "";
            }
            if (UserContext.Screens == null)
                UserContext.Screens = new List<Kaizen.Configuration.Screen>();
            UserContext.Screens.Add(Kaizen.Configuration.Screen.Client);

            int templenth = classtemp.Prefixlengh.Value - KaizenID.ToString().Length;
            string Str = string.Empty;
            for (byte i = 1; i <= templenth; i++)
            {
                Str = Str + "0";
            }
            string DOPrefix = string.Empty;
            if (!string.IsNullOrEmpty(classtemp.PrefixValue))
                DOPrefix = classtemp.PrefixValue.Trim();
            return DOPrefix + Str + KaizenID.ToString();
        }

        public KaizenResult UpdateClientData(string ParentClientID,string ClientID)
        {
            CM00110 CM00110Obj = _CM00110Repository.GetAll().Where(x => x.ClientID.Equals(ClientID)).FirstOrDefault();
            CM00110Obj.ParentClientID = ParentClientID;
            var result = _CM00110Repository.UpdateKaizenObject(CM00110Obj);
            return result;
        }

        public KaizenResult DeleteParentClientID(string ClientID)
        {
            CM00110 CM00110Obj = _CM00110Repository.GetAll().Where(x => x.ClientID.Equals(ClientID)).FirstOrDefault();
            CM00110Obj.ParentClientID = "";
            var result = _CM00110Repository.UpdateKaizenObject(CM00110Obj);
            return result;
        }

        public IList<CM00111> GetClientAddressData(int addressCode, string ClientID)
        {
            Kaizen.CMS.Repository.CM00111Repository _CM00111Repository = new CM00111Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            var tasks = _CM00111Repository.GetAll().Where(x => x.AddressCode == addressCode && x.ClientID.Trim() == ClientID.Trim()).ToList();
            IList<CM00111> result = tasks;
            return result;
        }

        public IList<CM00111> GetClientAddressData(int[] addressCodeTypeList, string ClientID)
        {
            Kaizen.CMS.Repository.CM00111Repository _CM00111Repository = new CM00111Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            var tasks = _CM00111Repository.GetAll().Where(x => addressCodeTypeList.Contains(x.AddressCode) && x.ClientID.Trim() == ClientID.Trim()).ToList();
            IList<CM00111> result = tasks;
            return result;
        }

        public IList<CM00111> GetClientAddressDataByClientID(string ClientID)
        {
            Kaizen.CMS.Repository.CM00111Repository _CM00111Repository = new CM00111Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            var tasks = _CM00111Repository.GetAll().Where(x => x.ClientID.Trim() == ClientID.Trim()).ToList();
            IList<CM00111> result = tasks;
            return result;
        }


        public IList<CM00008> GetClientAddressTypes()
        {
            Kaizen.CMS.Repository.CM00008Repository _CM00008Repository = new CM00008Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            var tasks = _CM00008Repository.GetAll();
            IList<CM00008> result = tasks;
            return result;
        }
    }
}
