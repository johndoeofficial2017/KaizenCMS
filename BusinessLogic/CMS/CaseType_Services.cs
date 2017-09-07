using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaizen.CMS.Repository;
using Kaizen.CMS;

namespace Kaizen.BusinessLogic.CMS
{
    public class CM00029Services
    {
        #region Infrastructure

        private Kaizen.CMS.Repository.CM00029Repository _CM00029Repository;
        private Kaizen.Configuration.KaizenSession UserContext;
        public CM00029Services(Kaizen.Configuration.KaizenSession _UserContext)
        {
            UserContext = _UserContext;
            _CM00029Repository = new CM00029Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

        }
        #endregion
        public DataCollection<CM00029> GetAllViewBYSQLQuery(string Filters, string Searchcritaria,
int PageSize, int Page, string Member, bool IsAscending)
        {
            string SeachStr = string.Empty;
            if (string.IsNullOrEmpty(Searchcritaria))
            {
                if (!string.IsNullOrEmpty(Filters))
                    SeachStr = Filters;
            }

            DataCollection<CM00029> result = null;
            if (string.IsNullOrEmpty(SeachStr))
                result = _CM00029Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            else
                result = _CM00029Repository.GetWhereListWithPaging("CM00029", PageSize, Page, SeachStr, Member, IsAscending);
            return result;
        }

        public DataCollection<CM00029> GetAllViewBYSQLQuery(string Filters,
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
                    SeachStr += Help.GetFilter("TRXTypeID", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                    SeachStr += " or ";
                    SeachStr += Help.GetFilter("TrxTypeName", (KaizenFilterOperator)FltrOperator, Searchcritaria);
                }
                else
                    SeachStr = Help.GetFilter(FieldID, (KaizenFilterOperator)FltrOperator, Searchcritaria);
            }

            DataCollection<CM00029> result = null;
            if (string.IsNullOrEmpty(SeachStr))
            {
                result = _CM00029Repository.GetWhereListWithPaging(PageSize, Page, ss => Member, IsAscending);
            }
            else
            {
                result = _CM00029Repository.GetWhereListWithPaging("CM00029", PageSize, Page, SeachStr, Member, IsAscending);
            }

            return result;
        }


        public DataCollection<CM00029> GetAllBYSQLQuery(string sqlenquiry, int PageSize, int Page, string Member, string SortDirection)
        {
            if (string.IsNullOrEmpty(sqlenquiry))
            {
                var tasks = _CM00029Repository.GetListWithPaging(PageSize, Page, ss => Member, ss => SortDirection);
                DataCollection<CM00029> result = tasks;
                return result;
            }
            else
            {
                var tasks = _CM00029Repository.GetListWithPaging(sqlenquiry, PageSize, Page, ss => Member, ss => SortDirection);
                DataCollection<CM00029> result = tasks;
                return result;
            }

        }
        public DataCollection<CM00029> GetListWithPaging(int PageSize, int Page, string Member, string SortDirection)
        {
            DataCollection<CM00029> result = null;
            var tasks = _CM00029Repository.GetListWithPaging(PageSize, Page, ss => new { ss.TRXTypeID });
            result = tasks;
            return result;
        }
        public IList<CM00029> GetAllMe()
        {
            var tasks = _CM00029Repository.GetAll(xx => xx.CM00057.Any(cc => cc.UserName ==UserContext.UserName.Trim()));
            IList<CM00029> result = tasks;
            return result;
        }

        public IList<CM00029> GetAll()
        {
            try
            {
                IList<CM00029> L = null;
                try
                {
                    var tasks = _CM00029Repository.GetAll();
                    IList<CM00029> result = tasks;
                    return result;
                }
                catch (Exception ex)
                {
                }
                return null;
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    System.Diagnostics.Debug.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:", eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        System.Diagnostics.Debug.WriteLine("- GenderID: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage);
                    }
                }
            }
            return null;
        }
        public List<CM00039> GetAllByTRXTypeID(int TRXTypeID)
        {
            CM00039Repository rep = new CM00039Repository(this.UserContext.CompanyID, this.UserContext.UserName, this.UserContext.Password);
            var tasks = rep.GetAll(ss => ss.TRXTypeID == TRXTypeID);
            IList<CM00039> result = tasks;
            foreach (CM00039 o in result)
            {
                o.FieldName = o.FieldName.Trim();
            }
            return result.OrderBy(x => x.FieldOrder).ToList(); ;
        }
        public CM00029 GetSingle(int TRXTypeID)
        {
            var tasks = _CM00029Repository.GetSingle(x => x.TRXTypeID == TRXTypeID);
            return tasks;
        }
        public System.Data.DataTable GetDynamicDataSource(int GraphID, string GraphTypeName)
        {
            string sql = "";
            CM00040Repository rep = new CM00040Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);

            var graphCategories = GetGraphCategories(GraphID);
            var graphFields = GetGraphFields(GraphID);

            if (GraphTypeName.Equals("pie", StringComparison.OrdinalIgnoreCase) ||
                    GraphTypeName.Equals("donut", StringComparison.OrdinalIgnoreCase) ||
                    GraphTypeName.Equals("funnel", StringComparison.OrdinalIgnoreCase))
                sql = GetSQLForGraphDataInclusiveCategory(graphCategories, graphFields);
            //sql = "select ClientName , sum(isnull(ClaimAmount,0))  ClaimAmount from CM00203 group by ClientName";
            else
            {
                //sql = "select ClientName , sum(isnull(ClaimAmount,0))  ClaimAmount,sum(isnull(OutstandingAMT,0)) OutstandingAMT from CM00203 group by ClientName";
                sql = GetSQLForGraphDataExclusiveCategory(graphCategories, graphFields);
            }

            var tasks = _CM00029Repository.ExecuteSqlCommandToDataTable(sql);
            return tasks;
        }
        private string GetSQLForGraphDataExclusiveCategory(IList<CM00077> graphCategories, IList<CM00078> graphFields)
        {
            string sqlQuery = "";
            List<string> sqlFields = new List<string>();
            List<string> categories = new List<string>();
            try
            {
                sqlQuery = "select ";

                int cnt = 0;
                foreach (CM00077 objCM00077 in graphCategories)
                {
                    sqlFields.Add(objCM00077.FieldName);
                    categories.Add(objCM00077.FieldName);
                }
                foreach (CM00078 objCM00078 in graphFields)
                {
                    sqlFields.Add(objCM00078.CM00047.SummeryTypeName + "(" + "isnull(" + objCM00078.FieldName + ",0)) " + objCM00078.FieldName);
                }
                sqlQuery += string.Join(",", sqlFields.ToArray());
                sqlQuery += " from CM00203 group by ";
                sqlQuery += string.Join(",", categories.ToArray());

            }
            catch (Exception ex) { }

            return sqlQuery;
        }
        private string GetSQLForGraphDataInclusiveCategory(IList<CM00077> graphCategories, IList<CM00078> graphFields)
        {
            string sqlQuery = "";
            List<string> sqlFields = new List<string>();
            List<string> categories = new List<string>();
            try
            {
                sqlQuery = "select ";

                int cnt = 0;
                if (graphCategories.Count > 0)
                {
                    sqlFields.Add(graphCategories.FirstOrDefault().FieldName);
                    categories.Add(graphCategories.FirstOrDefault().FieldName);
                }

                if (graphFields.Count > 0)
                {
                    CM00078 objCM00078 = graphFields.FirstOrDefault();
                    sqlFields.Add(objCM00078.CM00047.SummeryTypeName + "(" + "isnull(" + objCM00078.FieldName + ",0)) " + objCM00078.FieldName);
                }
                sqlQuery += string.Join(",", sqlFields.ToArray());
                sqlQuery += " from CM00203 group by ";
                sqlQuery += string.Join(",", categories.ToArray());

            }
            catch (Exception ex) { }

            return sqlQuery;
        }
        public IList<CM00077> GetGraphCategories(int GraphID)
        {
            CM00077Repository rep = new CM00077Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            var result = rep.GetAll().Where(xx => xx.GraphID == GraphID).ToList();
            return result;
        }
        public IList<CM00078> GetGraphFields(int GraphID)
        {
            CM00078Repository rep = new CM00078Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            var result = rep.GetAll(i => i.CM00047).Where(xx => xx.GraphID == GraphID).ToList();
            return result;
        }
        public List<string> GetDynamicSeriesNames(int GraphID)
        {
            CM00078Repository rep = new CM00078Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            var seriesNames = rep.GetAll(xx => xx.GraphID == GraphID);
            List<string> result = seriesNames.Select(x => x.FieldDisplay).ToList();
            return result;
        }
        public KaizenResult AddCM00029(CM00029 newTask)
        {
            var result = _CM00029Repository.AddKaizenObject(newTask);
            return result;
        }
        public KaizenResult AddCM00029(IList<CM00029> newTask)
        {
            var result = _CM00029Repository.AddKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Update(CM00029 newTask)
        {
            var result = _CM00029Repository.UpdateKaizenObject(newTask);
            return result;
        }
        public KaizenResult Update(IList<CM00029> newTask)
        {
            var result = _CM00029Repository.UpdateKaizenObject(newTask.ToArray());
            return result;
        }

        public KaizenResult Delete(CM00029 newTask)
        {
            var result = _CM00029Repository.DeleteKaizenObject(newTask);
            return result;
        }
        public KaizenResult Delete(IList<CM00029> newTask)
        {
            var result = _CM00029Repository.DeleteKaizenObject(newTask.ToArray());
            return result;
        }

        // Status Group Roles
        public IList<CM00056> GetRolesByCaseType(int TRXTypeID)
        {
            CM00056Repository rep = new CM00056Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            var actionTyperoles = rep.GetAll(xx => xx.TRXTypeID == TRXTypeID);
            IList<CM00056> result = actionTyperoles;
            return result;
        }

        public KaizenResult AddCM00056(CM00056 newTask)
        {
            CM00056Repository rep = new CM00056Repository(this.UserContext.CompanyID, this.UserContext.UserName, this.UserContext.Password);
            var result = rep.AddKaizenObject(newTask);
            rep.ExecuteSqlCommand("exec [dbo].[Sys_TempReconcile20]");
            return result;
        }
        public int ExecuteSqlCommand(string myQuery)
        {
            var result = _CM00029Repository.ExecuteSqlCommand(myQuery);
            return result;
        }
        public KaizenResult DeleteCM00056(CM00056 newTask)
        {
            CM00056Repository rep = new CM00056Repository(this.UserContext.CompanyID, this.UserContext.UserName, this.UserContext.Password);
            var result = rep.DeleteKaizenObject(newTask);
            return result;
        }

        public IList<CM00057> GetCaseTypeByUser(string userName)
        {
            CM00057Repository rep = new CM00057Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            var tasks = rep.GetAll(xx => xx.UserName == userName);
            IList<CM00057> result = tasks;
            return result;
        }

        public KaizenResult DeleteCaseTypeByUser(CM00057 caseTypeUser)
        {
            CM00057Repository rep = new CM00057Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            var result = rep.DeleteKaizenObject(caseTypeUser);
            return result;
        }

        public KaizenResult AddCM00057(CM00057 caseTypeUser)
        {
            CM00057Repository rep = new CM00057Repository(this.UserContext.CompanyID, this.UserContext.UserName, this.UserContext.Password);
            var result = rep.AddKaizenObject(caseTypeUser);
            return result;
        }

        public IList<CM00055> GetProductsByCaseType(int TRXTypeID)
        {
            CM00055Repository rep = new CM00055Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            var caseTypeProducts = rep.GetAll(xx => xx.TRXTypeID == TRXTypeID);
            IList<CM00055> result = caseTypeProducts;
            return result;
        }

        #region Case Type Fields
        public KaizenResult AddCM00070(CM00070 caseTypeField)
        {
            CM00070Repository rep = new CM00070Repository(this.UserContext.CompanyID, this.UserContext.UserName, this.UserContext.Password);
            var result = rep.AddKaizenObject(caseTypeField);
            return result;
        }
        public IList<CM00028> GetFieldTypeList()
        {
            try
            {
                try
                {
                    CM00028Repository rep = new CM00028Repository(this.UserContext.CompanyID, this.UserContext.UserName, this.UserContext.Password);
                    var fieldTypes = rep.GetAll();
                    IList<CM00028> result = fieldTypes;
                    return result;
                }
                catch (Exception ex)
                {
                }
                return null;
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    System.Diagnostics.Debug.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:", eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        System.Diagnostics.Debug.WriteLine("- GenderID: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage);
                    }
                }
            }
            return null;
        }
        public IList<CM00070> GetFieldsByCaseType(int TRXTypeID)
        {
            CM00070Repository rep = new CM00070Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            var caseTypeFields = rep.GetAll(xx => xx.TRXTypeID == TRXTypeID);
            IList<CM00070> result = caseTypeFields;
            return result;
        }

        public KaizenResult Update(CM00070 fieldType)
        {
            CM00070Repository rep = new CM00070Repository(this.UserContext.CompanyID, this.UserContext.UserName, this.UserContext.Password);
            KaizenResult result = rep.UpdateKaizenObject(fieldType);
            return result;
        }

        public KaizenResult Delete(CM00070 fieldType)
        {
            CM00070Repository rep = new CM00070Repository(this.UserContext.CompanyID, this.UserContext.UserName, this.UserContext.Password);
            var result = rep.DeleteKaizenObject(fieldType);
            return result;
        }
        #endregion

        #region Case Type Views
        public IList<CM00071> GetViews()
        {
            CM00071Repository rep = new CM00071Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            var caseTypeFields = rep.GetAll();
            IList<CM00071> result = caseTypeFields;
            return result;
        }
        public IList<CM00071> GetViewsByCaseType(int TRXTypeID)
        {
            CM00071Repository rep = new CM00071Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            var caseTypeFields = rep.GetAll(xx => xx.TRXTypeID == TRXTypeID);
            IList<CM00071> result = caseTypeFields;
            return result;
        }

        public IList<CM00071> GetViews(int TRXTypeID, string UserName)
        {
            CM00071Repository rep = new CM00071Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            var caseTypeFields = rep.GetList(xx => xx.TRXTypeID == TRXTypeID && xx.CM00073.Any(cc => cc.UserName == UserName), null, null, ww => ww.CM00073);
            List<CM00071> ViewResult = new List<CM00071>();
            foreach (CM00071 o in caseTypeFields)
            {
                CM00071 newobj = new CM00071()
                {
                    ViewID = o.ViewID,
                    ViewName = o.ViewName,
                    WhereCondition = o.WhereCondition,
                    ViewSortCondition = o.ViewSortCondition,
                    IsDefault = o.IsDefault,
                    IsPivotTable = o.IsPivotTable
                };
                ViewResult.Add(newobj);
            }
            foreach (CM00071 o in caseTypeFields)
            {
                CM00073 temp = o.CM00073.FirstOrDefault();
                if (temp.IsDefault)
                {
                    foreach (CM00071 tt in ViewResult)
                        tt.IsDefault = false;
                    o.IsDefault = true;
                    break;
                }
            }
            return ViewResult;
        }
        public KaizenResult AddCM00071(CM00071 caseTypeView)
        {
            CM00071Repository rep = new CM00071Repository(this.UserContext.CompanyID, this.UserContext.UserName, this.UserContext.Password);
            var result = rep.AddKaizenObject(caseTypeView);
            return result;
        }
        public KaizenResult Update(CM00071 viewType)
        {
            CM00071Repository rep = new CM00071Repository(this.UserContext.CompanyID, this.UserContext.UserName, this.UserContext.Password);
            KaizenResult result = rep.UpdateKaizenObject(viewType);
            return result;
        }
        public KaizenResult Update(IList<CM00071> viewList)
        {
            CM00071Repository rep = new CM00071Repository(this.UserContext.CompanyID, this.UserContext.UserName, this.UserContext.Password);
            KaizenResult result = rep.UpdateKaizenObject(viewList.ToArray());
            return result;
        }
        public KaizenResult Delete(CM00071 viewType)
        {
            CM00071Repository rep = new CM00071Repository(this.UserContext.CompanyID, this.UserContext.UserName, this.UserContext.Password);
            var result = rep.DeleteKaizenObject(viewType);
            return result;
        }
        #endregion

        #region Case Type View Roles
        public IList<CM00072> GetRolesByView(int viewID)
        {
            CM00072Repository rep = new CM00072Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            var rolesByViews = rep.GetAll(xx => xx.ViewID == viewID);
            IList<CM00072> result = rolesByViews;
            return result;
        }
        public KaizenResult AddCM00072(CM00072 viewRole)
        {
            CM00072Repository rep = new CM00072Repository(this.UserContext.CompanyID, this.UserContext.UserName, this.UserContext.Password);
            var result = rep.AddKaizenObject(viewRole);
            rep.ExecuteSqlCommand("exec [dbo].[Sys_TempReconcile20]");
            return result;
        }
        public KaizenResult DeleteCM00072(CM00072 viewRole)
        {
            CM00072Repository rep = new CM00072Repository(this.UserContext.CompanyID, this.UserContext.UserName, this.UserContext.Password);
            var result = rep.DeleteKaizenObject(viewRole);
            return result;
        }
        #endregion

        #region Case Type View User
        public KaizenResult AddCM00073(CM00073 caseTypeViewUser)
        {
            CM00073Repository rep = new CM00073Repository(this.UserContext.CompanyID, this.UserContext.UserName, this.UserContext.Password);
            var result = rep.AddKaizenObject(caseTypeViewUser);
            return result;
        }
        public KaizenResult UpdateCM00073(CM00073 caseTypeViewUser)
        {
            CM00073Repository rep = new CM00073Repository(this.UserContext.CompanyID, this.UserContext.UserName, this.UserContext.Password);
            var result = rep.UpdateKaizenObject(caseTypeViewUser);
            return result;
        }
        public KaizenResult DeleteCaseTypeViewUser(CM00073 caseTypeViewUser)
        {
            CM00073Repository rep = new CM00073Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            var result = rep.DeleteKaizenObject(caseTypeViewUser);
            return result;
        }
        public IList<CM00073> GetCaseTypeViewsByUser(int TRXTypeID, string userName)
        {
            CM00073Repository rep = new CM00073Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            var tasks = rep.GetAll(xx => xx.UserName == userName && xx.CM00071.TRXTypeID == TRXTypeID);
            IList<CM00073> result = tasks;
            return result;
        }
        #endregion

        #region Case Type Views Fields
        public IList<CM00074> GetAllFieldsByCaseType(int TRXTypeID)
        {
            CM00074Repository rep = new CM00074Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            var caseTypeViewsFields = rep.GetAll(xx => xx.TRXTypeID == TRXTypeID);
            IList<CM00074> result = caseTypeViewsFields;
            return result;
        }
        public IList<CM00075> GetAllFieldsByView(int viewID)
        {
            CM00075Repository rep = new CM00075Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            var viewsFields = rep.GetAll(xx => xx.ViewID == viewID);
            IList<CM00075> result = viewsFields;
            foreach (CM00075 row in result)
            {
                row.FieldName = row.FieldName.Trim();
            }
            return result;
        }
        public KaizenResult AddCM00075(CM00075 viewField)
        {
            CM00075Repository rep = new CM00075Repository(this.UserContext.CompanyID, this.UserContext.UserName, this.UserContext.Password);
            var result = rep.AddKaizenObject(viewField);
            return result;
        }
        public KaizenResult UpdateCM00075(IList<CM00075> viewFieldList)
        {
            CM00075Repository rep = new CM00075Repository(this.UserContext.CompanyID, this.UserContext.UserName, this.UserContext.Password);
            var result = rep.UpdateKaizenObject(viewFieldList.ToArray());
            return result;
        }
        public KaizenResult DeleteCM00075(CM00075 viewField)
        {
            CM00075Repository rep = new CM00075Repository(this.UserContext.CompanyID, this.UserContext.UserName, this.UserContext.Password);
            var result = rep.DeleteKaizenObject(viewField);
            return result;
        }
        #endregion

        #region Case Type Standard Fields
        public IList<CM00074> GetStandardFieldsByCaseType(int TRXTypeID)
        {
            CM00074Repository rep = new CM00074Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            var caseTypeViewsFields = rep.GetAll(xx => xx.TRXTypeID == TRXTypeID);
            IList<CM00074> result = caseTypeViewsFields;
            return result;
        }
        public KaizenResult UpdateCM00074(IList<CM00074> standardFieldList, int callNumber)
        {
            CM00074 objCM00074 = standardFieldList.FirstOrDefault();
            int TRXTypeID = 0;
            if (objCM00074 != null)
                TRXTypeID = objCM00074.TRXTypeID;

            CM00074Repository rep = new CM00074Repository(this.UserContext.CompanyID, this.UserContext.UserName, this.UserContext.Password);
            IList<CM00074> standardFields = rep.GetAll(xx => xx.TRXTypeID == TRXTypeID);

            if (callNumber == 1)
            {
                foreach (CM00074 field in standardFields)
                {
                    foreach (CM00074 updateField in standardFieldList)
                    {
                        if (field.FieldName.Equals(updateField.FieldName, StringComparison.OrdinalIgnoreCase))
                        {
                            updateField.IsEmailable = field.IsEmailable;
                            updateField.IsFilterable = field.IsFilterable;
                            updateField.Islockable = field.Islockable;
                            updateField.Islocked = field.Islocked;
                            updateField.IsPrintable = field.IsPrintable;
                            updateField.IsRequired = field.IsRequired;
                            updateField.IsSortable = field.IsSortable;
                        }
                    }
                }
            }
            else if (callNumber == 2)
            {
                foreach (CM00074 field in standardFields)
                {
                    foreach (CM00074 updateField in standardFieldList)
                    {
                        if (field.FieldName.Equals(updateField.FieldName, StringComparison.OrdinalIgnoreCase))
                        {
                            updateField.FieldDisplay = field.FieldDisplay;
                            updateField.FieldOrder = field.FieldOrder;
                            updateField.IsActive = field.IsActive;
                            updateField.FieldTypeID = field.FieldTypeID;
                            updateField.FieldWidth = field.FieldWidth;
                        }
                    }
                }
            }

            var result = rep.UpdateKaizenObject(standardFieldList.ToArray());
            return result;
        }
        public IList<CM00038> GetStandardFields()
        {
            CM00038Repository rep = new CM00038Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            var standardFields = rep.GetAll();
            IList<CM00038> result = standardFields;
            return result;
        }
        public KaizenResult DeleteCM00074(CM00074 graphViewField)
        { 
            CM00074Repository rep = new CM00074Repository(this.UserContext.CompanyID, this.UserContext.UserName, this.UserContext.Password);
            var result = rep.DeleteKaizenObject(graphViewField);
            return result;
        }
        #endregion


        public IList<CM00038> GetSetUpStandardFields()
        {
            CM00038Repository rep = new CM00038Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            var standardFields = rep.GetAll();
            IList<CM00038> result = standardFields;
            return result;
        }
        public KaizenResult UpdateCM00038(IList<CM00038> standardFieldList)
        {
            CM00038Repository rep = new CM00038Repository(this.UserContext.CompanyID, this.UserContext.UserName, this.UserContext.Password);
            var result = rep.UpdateKaizenObject(standardFieldList.ToArray());
            return result;
        }

        #region Case Type Graph Setup
        public IList<CM00040> GetGraphTypes()
        {
            CM00040Repository rep = new CM00040Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            var graphTypes = rep.GetAll();
            IList<CM00040> result = graphTypes;
            return result;
        }
        public IList<CM00076> GetGraphData(int ViewID)
        {
            CM00076Repository rep = new CM00076Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            var graphData = rep.GetAll(x => x.ViewID == ViewID);
            IList<CM00076> result = graphData;
            return result;
        }
        public IList<CM00076> GetGraphs(int ViewID)
        {
            CM00076Repository rep = new CM00076Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            var graphData = rep.GetAll(x => x.CM00040).Where(x => x.ViewID == ViewID).ToList();
            IList<CM00076> result = graphData;
            return result;
        }
        public IList<CM00076> GetGraphDataById(int GraphID)
        {
            CM00076Repository rep = new CM00076Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            var graphData = rep.GetAll().Where(x => x.GraphID == GraphID).ToList();
            IList<CM00076> result = graphData;
            return result;
        }
        public KaizenResult AddCM00076(CM00076 graph)
        {
            CM00076Repository rep = new CM00076Repository(this.UserContext.CompanyID, this.UserContext.UserName, this.UserContext.Password);
            var result = rep.AddKaizenObject(graph);
            return result;
        }
        public KaizenResult UpdateCM00076(CM00076 graph)
        {
            CM00076Repository rep = new CM00076Repository(this.UserContext.CompanyID, this.UserContext.UserName, this.UserContext.Password);
            var result = rep.UpdateKaizenObject(graph);
            return result;
        }
        public KaizenResult DeleteCM00076(CM00076 graph)
        {
            CM00076Repository rep = new CM00076Repository(this.UserContext.CompanyID, this.UserContext.UserName, this.UserContext.Password);
            var result = rep.DeleteKaizenObject(graph);
            return result;
        }
        #endregion

        #region Case Type Graph View
        public KaizenResult AddCM00077(CM00077 graphViewField)
        {
            CM00077Repository rep = new CM00077Repository(this.UserContext.CompanyID, this.UserContext.UserName, this.UserContext.Password);
            var result = rep.AddKaizenObject(graphViewField);
            return result;
        }
        public KaizenResult UpdateCM00077(IList<CM00077> graphViewFieldList)
        {
            CM00077Repository rep = new CM00077Repository(this.UserContext.CompanyID, this.UserContext.UserName, this.UserContext.Password);
            var result = rep.UpdateKaizenObject(graphViewFieldList.ToArray());
            return result;
        }
        public KaizenResult DeleteCM00077(CM00077 graphViewField)
        {
            CM00077Repository rep = new CM00077Repository(this.UserContext.CompanyID, this.UserContext.UserName, this.UserContext.Password);
            var result = rep.DeleteKaizenObject(graphViewField);
            return result;
        }
        #endregion

        #region For CM00078
        public IList<CM00047> GetSummaryTypes()
        {
            CM00047Repository rep = new CM00047Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            var summaryTypes = rep.GetAll();
            IList<CM00047> result = summaryTypes;
            return result;
        }

        public KaizenResult AddCM00078(CM00078 graphViewField)
        {
            CM00078Repository rep = new CM00078Repository(this.UserContext.CompanyID, this.UserContext.UserName, this.UserContext.Password);
            var result = rep.AddKaizenObject(graphViewField);
            return result;
        }
        public KaizenResult UpdateCM00078(IList<CM00078> graphViewFieldList)
        {
            CM00078Repository rep = new CM00078Repository(this.UserContext.CompanyID, this.UserContext.UserName, this.UserContext.Password);
            var result = rep.UpdateKaizenObject(graphViewFieldList.ToArray());
            return result;
        }
        public KaizenResult DeleteCM00078(CM00078 graphViewField)
        {
            CM00078Repository rep = new CM00078Repository(this.UserContext.CompanyID, this.UserContext.UserName, this.UserContext.Password);
            var result = rep.DeleteKaizenObject(graphViewField);
            return result;
        }
        public IList<CM00077> GetGraphViewsFields(int GraphID)
        {
            CM00077Repository rep = new CM00077Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            var graphData = rep.GetAll(x => x.GraphID == GraphID);
            IList<CM00077> result = graphData;
            return result;
        }
        public IList<CM00078> GetGraphViewsFields_Summary(int GraphID)
        {
            CM00078Repository rep = new CM00078Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            var graphData = rep.GetAll(x => x.GraphID == GraphID);
            IList<CM00078> result = graphData;
            return result;
        }
        #endregion

        #region Case Type Equation Field
        public IList<CM00024> FillFieldCodeList(int FieldTypeID)
        {
            CM00024Repository rep = new CM00024Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            var caseTypeFields = rep.GetAll(xx => xx.FieldTypeID == FieldTypeID);
            IList<CM00024> result = caseTypeFields;
            return result;
        }
        public IList<CM00080> GetEquationFieldsByCaseType(int TRXTypeID)
        {
            CM00080Repository rep = new CM00080Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            var caseTypeEqFields = rep.GetAll(xx => xx.TRXTypeID == TRXTypeID);
            IList<CM00080> result = caseTypeEqFields;
            return result;
        }
        public KaizenResult AddCM00080(CM00080 caseTypeField)
        {
            CM00080Repository rep = new CM00080Repository(this.UserContext.CompanyID, this.UserContext.UserName, this.UserContext.Password);
            var result = rep.AddKaizenObject(caseTypeField);
            return result;
        }

        public KaizenResult Update(CM00080 fieldType)
        {
            CM00080Repository rep = new CM00080Repository(this.UserContext.CompanyID, this.UserContext.UserName, this.UserContext.Password);
            KaizenResult result = rep.UpdateKaizenObject(fieldType);
            return result;
        }

        public KaizenResult Delete(CM00080 fieldType)
        {
            CM00080Repository rep = new CM00080Repository(this.UserContext.CompanyID, this.UserContext.UserName, this.UserContext.Password);
            var result = rep.DeleteKaizenObject(fieldType);
            return result;
        }
        #endregion

        #region Case Type Equation Field By View
        public IList<CM00071> GetViews_EquationFieldsByCaseType(int TRXTypeID)
        {
            CM00071Repository rep = new CM00071Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            var viewData = rep.GetAll(x => x.TRXTypeID == TRXTypeID);
            IList<CM00071> result = viewData;
            return result;
        }
        public IList<CM00075> GetStandardFieldsByCaseTypeAndView(int TRXTypeID, int ViewID)
        {
            CM00075Repository rep = new CM00075Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            var caseTypeViewsFields = rep.GetAll(xx => xx.TRXTypeID == TRXTypeID && xx.ViewID == ViewID);
            IList<CM00075> result = caseTypeViewsFields;
            return result;
        }
        public IList<CM00081> GetEquationFieldsByCaseTypeAndView(int TRXTypeID, int ViewID)
        {
            CM00081Repository rep = new CM00081Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            var caseTypeEqFields = rep.GetAll(xx => xx.TRXTypeID == TRXTypeID && xx.ViewID == ViewID);
            IList<CM00081> result = caseTypeEqFields;
            return result;
        }
        public KaizenResult AddCM00081(CM00081 caseTypeField)
        {
            CM00081Repository rep = new CM00081Repository(this.UserContext.CompanyID, this.UserContext.UserName, this.UserContext.Password);
            var result = rep.AddKaizenObject(caseTypeField);
            return result;
        }

        public KaizenResult Update(CM00081 fieldType)
        {
            CM00081Repository rep = new CM00081Repository(this.UserContext.CompanyID, this.UserContext.UserName, this.UserContext.Password);
            KaizenResult result = rep.UpdateKaizenObject(fieldType);
            return result;
        }

        public KaizenResult Delete(CM00081 fieldType)
        {
            CM00081Repository rep = new CM00081Repository(this.UserContext.CompanyID, this.UserContext.UserName, this.UserContext.Password);
            var result = rep.DeleteKaizenObject(fieldType);
            return result;
        }
        #endregion

        #region Case Type Lookup Field
        public IList<CM00070> GetAllCM00070()
        {
            CM00070Repository rep = new CM00070Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            var caseTypeFieldNames = rep.GetAll();
            IList<CM00070> result = caseTypeFieldNames;
            return result;
        }
        public IList<CM00070> GetFieldNames(int TRXTypeID)
        {
            CM00070Repository rep = new CM00070Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            var caseTypeFieldNames = rep.GetAll(xx => xx.TRXTypeID == TRXTypeID);
            IList<CM00070> result = caseTypeFieldNames;
            return result;
        }
        public IList<CM00050> GetCaseTypeLookupFields(int TRXTypeID)
        {
            CM00050Repository rep = new CM00050Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            var caseTypeEqFields = rep.GetAll(xx => xx.TRXTypeID == TRXTypeID);
            IList<CM00050> result = caseTypeEqFields;
            return result;
        }
        public KaizenResult AddCM00050(CM00050 caseTypeField)
        {
            CM00050Repository rep = new CM00050Repository(this.UserContext.CompanyID, this.UserContext.UserName, this.UserContext.Password);
            var result = rep.AddKaizenObject(caseTypeField);
            return result;
        }

        public KaizenResult Update(CM00050 fieldType)
        {
            CM00050Repository rep = new CM00050Repository(this.UserContext.CompanyID, this.UserContext.UserName, this.UserContext.Password);
            KaizenResult result = rep.UpdateKaizenObject(fieldType);
            return result;
        }

        public KaizenResult Delete(CM00050 fieldType)
        {
            CM00050Repository rep = new CM00050Repository(this.UserContext.CompanyID, this.UserContext.UserName, this.UserContext.Password);
            var result = rep.DeleteKaizenObject(fieldType);
            return result;
        }
        #endregion

        #region Case Type Field Order
        public KaizenResult UpdateStandardFieldOrder(IList<CM00074> standardFieldList, int callNumber)
        {
            CM00074 objCM00074 = standardFieldList.FirstOrDefault();
            int TRXTypeID = 0;
            if (objCM00074 != null)
                TRXTypeID = objCM00074.TRXTypeID;

            CM00074Repository rep = new CM00074Repository(this.UserContext.CompanyID, this.UserContext.UserName, this.UserContext.Password);
            IList<CM00074> standardFields = rep.GetAll(xx => xx.TRXTypeID == TRXTypeID);
            int order = 1;

            if (callNumber == 1)
            {
                foreach (CM00074 updateField in standardFieldList)
                {
                    foreach (CM00074 field in standardFields)
                    {
                        if (field.FieldName.Equals(updateField.FieldName, StringComparison.OrdinalIgnoreCase))
                        {
                            updateField.IsEmailable = field.IsEmailable;
                            updateField.IsFilterable = field.IsFilterable;
                            updateField.Islockable = field.Islockable;
                            updateField.Islocked = field.Islocked;
                            updateField.IsPrintable = field.IsPrintable;
                            updateField.IsRequired = field.IsRequired;
                            updateField.IsSortable = field.IsSortable;
                            updateField.FieldEquation = field.FieldEquation;
                            updateField.FieldOrder = order;
                            order++;
                        }
                    }
                }
            }
            else if (callNumber == 2)
            {
                foreach (CM00074 updateField in standardFieldList)
                {
                    foreach (CM00074 field in standardFields)
                    {
                        if (field.FieldName.Equals(updateField.FieldName, StringComparison.OrdinalIgnoreCase))
                        {
                            updateField.FieldDisplay = field.FieldDisplay;
                            updateField.FieldOrder = field.FieldOrder;
                            updateField.IsActive = field.IsActive;
                            updateField.FieldTypeID = field.FieldTypeID;
                            updateField.FieldWidth = field.FieldWidth;
                        }
                    }
                }
            }

            var result = rep.UpdateKaizenObject(standardFieldList.ToArray());
            return result;
        }
        #endregion

        #region CM00082
        public KaizenResult AddCM00082(CM00082 graphViewField)
        {
            CM00082Repository rep = new CM00082Repository(this.UserContext.CompanyID, this.UserContext.UserName, this.UserContext.Password);
            var result = rep.AddKaizenObject(graphViewField);
            return result;
        }
        public KaizenResult UpdateCM00082(IList<CM00082> graphViewFieldList)
        {
            CM00082Repository rep = new CM00082Repository(this.UserContext.CompanyID, this.UserContext.UserName, this.UserContext.Password);
            var result = rep.UpdateKaizenObject(graphViewFieldList.ToArray());
            return result;
        }
        public KaizenResult DeleteCM00082(CM00082 graphViewField)
        {
            CM00082Repository rep = new CM00082Repository(this.UserContext.CompanyID, this.UserContext.UserName, this.UserContext.Password);
            var result = rep.DeleteKaizenObject(graphViewField);
            return result;
        }
        #endregion

        #region For CM00083
        public IList<CM00047> GetViewFieldSummaryTypes()
        {
            CM00047Repository rep = new CM00047Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            var summaryTypes = rep.GetAll();
            IList<CM00047> result = summaryTypes;
            return result;
        }

        public KaizenResult AddCM00083(CM00083 graphViewField)
        {
            CM00083Repository rep = new CM00083Repository(this.UserContext.CompanyID, this.UserContext.UserName, this.UserContext.Password);
            var result = rep.AddKaizenObject(graphViewField);
            return result;
        }
        public KaizenResult UpdateCM00083(IList<CM00083> graphViewFieldList)
        {
            CM00083Repository rep = new CM00083Repository(this.UserContext.CompanyID, this.UserContext.UserName, this.UserContext.Password);
            var result = rep.UpdateKaizenObject(graphViewFieldList.ToArray());
            return result;
        }
        public KaizenResult DeleteCM00083(CM00083 graphViewField)
        {
            CM00083Repository rep = new CM00083Repository(this.UserContext.CompanyID, this.UserContext.UserName, this.UserContext.Password);
            var result = rep.DeleteKaizenObject(graphViewField);
            return result;
        }
        public IList<CM00082> GetCM00082(int ViewID)
        {
            CM00082Repository rep = new CM00082Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            var graphData = rep.GetAll(x => x.ViewID == ViewID);
            IList<CM00082> result = graphData;
            return result;
        }
        public IList<CM00083> GetCM00083(int ViewID)
        {
            CM00083Repository rep = new CM00083Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            var graphData = rep.GetAll(x => x.ViewID == ViewID);
            IList<CM00083> result = graphData;
            return result;
        }
        #endregion

        #region CM00084
        public KaizenResult AddCM00084(CM00084 graphViewField)
        {
            CM00084Repository rep = new CM00084Repository(this.UserContext.CompanyID, this.UserContext.UserName, this.UserContext.Password);
            var result = rep.AddKaizenObject(graphViewField);
            return result;
        }
        public KaizenResult UpdateCM00084(IList<CM00084> graphViewFieldList)
        {
            CM00084Repository rep = new CM00084Repository(this.UserContext.CompanyID, this.UserContext.UserName, this.UserContext.Password);
            var result = rep.UpdateKaizenObject(graphViewFieldList.ToArray());
            return result;
        }
        public KaizenResult DeleteCM00084(CM00084 graphViewField)
        {
            CM00084Repository rep = new CM00084Repository(this.UserContext.CompanyID, this.UserContext.UserName, this.UserContext.Password);
            var result = rep.DeleteKaizenObject(graphViewField);
            return result;
        }
        #endregion

        #region For CM00085

        public KaizenResult AddCM00085(CM00085 graphViewField)
        {
            CM00085Repository rep = new CM00085Repository(this.UserContext.CompanyID, this.UserContext.UserName, this.UserContext.Password);
            var result = rep.AddKaizenObject(graphViewField);
            return result;
        }
        public KaizenResult UpdateCM00085(IList<CM00085> graphViewFieldList)
        {
            CM00085Repository rep = new CM00085Repository(this.UserContext.CompanyID, this.UserContext.UserName, this.UserContext.Password);
            var result = rep.UpdateKaizenObject(graphViewFieldList.ToArray());
            return result;
        }
        public KaizenResult DeleteCM00085(CM00085 graphViewField)
        {
            CM00085Repository rep = new CM00085Repository(this.UserContext.CompanyID, this.UserContext.UserName, this.UserContext.Password);
            var result = rep.DeleteKaizenObject(graphViewField);
            return result;
        }
        public IList<CM00084> GetCM00084(int ViewID)
        {
            CM00084Repository rep = new CM00084Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            var graphData = rep.GetAll(x => x.ViewID == ViewID);
            IList<CM00084> result = graphData;
            return result;
        }
        public IList<CM00085> GetCM00085(int ViewID)
        {
            CM00085Repository rep = new CM00085Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            var graphData = rep.GetAll(x => x.ViewID == ViewID);
            IList<CM00085> result = graphData;
            return result;
        }
        #endregion

        #region For CM00086

        public KaizenResult AddCM00086(CM00086 graphViewField)
        {
            CM00086Repository rep = new CM00086Repository(this.UserContext.CompanyID, this.UserContext.UserName, this.UserContext.Password);
            var result = rep.AddKaizenObject(graphViewField);
            return result;
        }
        public KaizenResult UpdateCM00086(IList<CM00086> graphViewFieldList)
        {
            CM00086Repository rep = new CM00086Repository(this.UserContext.CompanyID, this.UserContext.UserName, this.UserContext.Password);
            var result = rep.UpdateKaizenObject(graphViewFieldList.ToArray());
            return result;
        }
        public KaizenResult DeleteCM00086(CM00086 graphViewField)
        {
            CM00086Repository rep = new CM00086Repository(this.UserContext.CompanyID, this.UserContext.UserName, this.UserContext.Password);
            var result = rep.DeleteKaizenObject(graphViewField);
            return result;
        }
        public IList<CM00086> GetCM00086(int ViewID)
        {
            CM00086Repository rep = new CM00086Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            var graphData = rep.GetAll(x => x.ViewID == ViewID);
            IList<CM00086> result = graphData;
            return result;
        }
        #endregion

        #region CM00079
        public KaizenResult AddCM00079(CM00079 graphViewField)
        { 
            CM00079Repository rep = new CM00079Repository(this.UserContext.CompanyID, this.UserContext.UserName, this.UserContext.Password);
            var result = rep.AddKaizenObject(graphViewField);
            return result;
        }
        public KaizenResult UpdateCM00079(IList<CM00079> graphViewFieldList)
        {
            CM00079Repository rep = new CM00079Repository(this.UserContext.CompanyID, this.UserContext.UserName, this.UserContext.Password);
            var result = rep.UpdateKaizenObject(graphViewFieldList.ToArray());
            return result;
        }
        public KaizenResult DeleteCM00079(CM00079 graphViewField)
        {
            CM00079Repository rep = new CM00079Repository(this.UserContext.CompanyID, this.UserContext.UserName, this.UserContext.Password);
            var result = rep.DeleteKaizenObject(graphViewField);
            return result;
        }
        public IList<CM00079> GetCM00079(int GraphID)
        {
            CM00079Repository rep = new CM00079Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            var graphData = rep.GetAll(x => x.GraphID == GraphID);
            IList<CM00079> result = graphData;
            return result;
        }
        #endregion

        #region CM00037
        public KaizenResult AddCM00037(CM00037 CM00037)
        {
            CM00037Repository rep = new CM00037Repository(this.UserContext.CompanyID, this.UserContext.UserName, this.UserContext.Password);
            var result = rep.AddKaizenObject(CM00037);
            return result;
        }
        public KaizenResult UpdateCM00037(CM00037 CM00037)
        {
            CM00037Repository rep = new CM00037Repository(this.UserContext.CompanyID, this.UserContext.UserName, this.UserContext.Password);
            var result = rep.UpdateKaizenObject(CM00037);
            return result;
        }
        public KaizenResult DeleteCM00037(CM00037 CM00037)
        {
            CM00037Repository rep = new CM00037Repository(this.UserContext.CompanyID, this.UserContext.UserName, this.UserContext.Password);
            var result = rep.DeleteKaizenObject(CM00037);
            return result;
        }
        public IList<CM00037> GetCM00037(string TableSource)
        {
            CM00037Repository rep = new CM00037Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            var obj = rep.GetAll(x => x.TableSource == TableSource);
            IList<CM00037> result = obj;
            return result;
        }
        public IList<CM00037> GetAllCM00037()
        {
            CM00037Repository rep = new CM00037Repository(UserContext.CompanyID, UserContext.UserName, UserContext.Password);
            var obj = rep.GetAll();
            IList<CM00037> result = obj;
            return result;
        }
        #endregion
    }
}
