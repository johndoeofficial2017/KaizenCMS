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
    public class CM00130Services
    {
        #region Infrastructure

        private Kaizen.CMS.Repository.CM00130Repository _CM00130Repository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public CM00130Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _CM00130Repository = new CM00130Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion
        public DataCollection<CM00130> GetAllViewBYSQLQuery(string Filters, string Searchcritaria,
int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }

            DataCollection<CM00130> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _CM00130Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _CM00130Repository.GetWhereListWithPaging("CM00130", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }


        public DataCollection<CM00130> GetAllViewBYSQLQuery(string Filters,
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
            DataCollection<CM00130> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _CM00130Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _CM00130Repository.GetWhereListWithPaging("CM00130", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<CM00130> GetAllViewBYSQLQuery(string Filters,
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

            DataCollection<CM00130> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _CM00130Repository.GetListWithPaging(ss => ss.PartnerClassID == CUSTCLAS, PageSize, Page, ss => Member);
            else
                result = _CM00130Repository.GetWhereListWithPaging("CM00130", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }


        public DataCollection<CM00130> GetAllViewBYSQLQuery(string WhereClause, int PageSize, int Page, string Member, ListSortDirection SortDirection)
        {
            CM00130Repository rep = new CM00130Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            DataCollection<CM00130> result = null;
            if (SortDirection == ListSortDirection.Ascending)
                result = rep.GetWhereListWithPaging(WhereClause, Page, PageSize, s => Member, true);
            else
                result = rep.GetWhereListWithPaging(WhereClause, Page, PageSize, s => Member, false);
            return result;
        }

        public DataCollection<CM00130> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, int SortDirection)
        {
            IList<CM00130> L = null;
            var tasks = _CM00130Repository.GetWhereListWithPaging(sqlenquiry, PageSize, Page, ss => Member, bool.Parse(SortDirection.ToString()));
            DataCollection<CM00130> result = tasks;
            return result;
        }
        public DataCollection<CM00130> GetListWithPaging(int PageSize, int Page, string Member, int SortDirection)
        {
            DataCollection<CM00130> result = null;
            var tasks = _CM00130Repository.GetListWithPaging(PageSize, Page, ss => Member, null);
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
        public DataCollection<CM00130> GetListWithPaging(int PageSize, int Page, string Member, string SortDirection)
        {
            DataCollection<CM00130> result = null;
            var tasks = _CM00130Repository.GetListWithPaging(PageSize, Page, ss => new { ss.PartnerID });
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
        public IList<CM00130> GetAll()
        {
            var tasks = _CM00130Repository.GetAll();
            IList<CM00130> result = tasks;
            return result;
        }
        public IList<CM00022> GetAllClientStatus()
        {
            Kaizen.CMS.Repository.CM00022Repository _CM00022Repository = new CM00022Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            var tasks = _CM00022Repository.GetAll();
            IList<CM00022> result = tasks;
            return result;
        }

        public CM00130 GetSingle(string PartnerID)
        {
            var tasks = _CM00130Repository.GetSingle(x => x.PartnerID == PartnerID);
           
            return tasks;
        }
        public KaizenResult AddCM00130(CM00130 newTask)
        {
            
            var result = _CM00130Repository.AddKaizenObject(newTask);
            //Kaizen.BusinessLogic.Configuration.CompanyacessServices srv = new Configuration.CompanyacessServices(UserContext);
            //Kaizen.BusinessLogic.CMS.CM00021Services srvclientClass = new CM00021Services(UserContext);
            //srvclientClass.ExecuteSqlCommand("update CM00021 set LastClientID = " + srv.GetNextClientKaizenID(newTask.PartnerClassID.Trim())
            //    + " where CUSTCLAS = '" + newTask.PartnerClassID.Trim() + "'");
            //srv.DeleteNextClientID(newTask.PartnerClassID.Trim());
            return result;
        }
        public KaizenResult Update(CM00130 UpdateCM00130)
        {
            var result = _CM00130Repository.UpdateKaizenObject(UpdateCM00130);
            return result;
        }
        public KaizenResult Delete(string PartnerID)
        {
            var result = _CM00130Repository.RemoveKaizenObject(xx => xx.PartnerID == PartnerID);
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
                        _CM00130Repository.ExecuteSqlCommand(SQL);
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

        public IList<CM00002> GetParentClassList()
        {
            Kaizen.CMS.Repository.CM00002Repository _CM00002Repository = new CM00002Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            var tasks = _CM00002Repository.GetAll();
            IList<CM00002> result = tasks;
            return result;
        }

        public IList<CM00033> GetParentAddressTypes()
        {
            Kaizen.CMS.Repository.CM00033Repository _CM00033Repository = new CM00033Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            var tasks = _CM00033Repository.GetAll();
            IList<CM00033> result = tasks;
            return result;
        }

        #region Partner Address 
        public KaizenResult AddCM00131(CM00131 CM00131)
        {
            Kaizen.CMS.Repository.CM00131Repository _CM00131Repository = new CM00131Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            var result = _CM00131Repository.AddKaizenObject(CM00131);
            return result;
        }
        public KaizenResult UpdateCM00131(CM00131 CM00131)
        {
            Kaizen.CMS.Repository.CM00131Repository _CM00131Repository = new CM00131Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            var result = _CM00131Repository.UpdateKaizenObject(CM00131);
            return result;
        }
        public IList<CM00131> GetPartnerAddressData(int addressCodeType,string PartnerID)
        {
            Kaizen.CMS.Repository.CM00131Repository _CM00131Repository = new CM00131Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            var tasks = _CM00131Repository.GetAll().Where(x => x.AddressCodeType == addressCodeType && x.PartnerID.Trim() == PartnerID.Trim()).ToList();
            IList<CM00131> result = tasks;
            return result;
        }
        public IList<CM00131> GetPartnerAddressData(int[] addressCodeTypeList, string PartnerID)
        {
            Kaizen.CMS.Repository.CM00131Repository _CM00131Repository = new CM00131Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            var tasks = _CM00131Repository.GetAll().Where(x => addressCodeTypeList.Contains(x.AddressCodeType) && x.PartnerID.Trim() == PartnerID.Trim()).ToList();
            IList<CM00131> result = tasks;
            return result;
        }
        public IList<CM00131> GetPartnerAddressDataByPartnerID(string PartnerID)
        {
            Kaizen.CMS.Repository.CM00131Repository _CM00131Repository = new CM00131Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            var tasks = _CM00131Repository.GetAll().Where(x => x.PartnerID.Trim() == PartnerID.Trim()).ToList();
            IList<CM00131> result = tasks;
            return result;
        }
        
        public KaizenResult DeletePartnerAddress(CM00131 newTask)
        {
            Kaizen.CMS.Repository.CM00131Repository _CM00131Repository = new CM00131Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            var result = _CM00131Repository.DeleteKaizenObject(newTask);
            return result;
        }
        #endregion
    }
}
