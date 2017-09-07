using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.Mvc;
using System.IO;
using Kendo.Mvc.UI;
using Kaizen.BusinessLogic.CMS;
using Kaizen.CMS;
using Kaizen.BusinessLogic.Configuration;
     
namespace UIServer.Controllers
{
    public class IntegrationManagerController : Controller
    { 
        // GET: IntegrationManager
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SaveIntegrationManagerFil(IEnumerable<HttpPostedFileBase> IntegrationManagerFileAttachemnt)
        {
            var fileName = "";
            List<Kaizen.Configuration.ExcelColumns> lstExcelColumns = null;
            foreach (var file in IntegrationManagerFileAttachemnt)
            {
                fileName = Guid.NewGuid().ToString() + Path.GetFileName(file.FileName);
                var destinationPath = Path.Combine(Server.MapPath("~/Files"), fileName);
                file.SaveAs(destinationPath);
                Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(System.Web.HttpContext.Current.Request["KaizenPublicKey"]);
                Kaizen.BusinessLogic.Configuration.CompanyacessServices srv = new Kaizen.BusinessLogic.Configuration.CompanyacessServices(KaizenUser);
                lstExcelColumns = srv.ReadExcelColumns(destinationPath, fileName);
            }
            TempTable o = new TempTable();
            o.TableName = fileName;
            o.ExcelColumns = lstExcelColumns;
            return Json(o, JsonRequestBehavior.AllowGet);
        }
        public ActionResult SaveData(string KaizenPublicKey, IList<Kaizen.Configuration.Kaizen00001> KaizenColumn
            , IList<Kaizen.Configuration.Kaizen00003> Kaizen00003, string TableName)
        {
            if (string.IsNullOrEmpty(KaizenPublicKey)) return RedirectToAction("Login", "SysUser");
            if (KaizenPublicKey.Trim() == "undefined") return RedirectToAction("Login", "SysUser");
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("Login", "SysUser");
            string colname = string.Empty;
            string colValue = string.Empty;
            Kaizen.BusinessLogic.Configuration.CompanyacessServices srv = new Kaizen.BusinessLogic.Configuration.CompanyacessServices(KaizenUser);
            foreach (Kaizen.Configuration.Kaizen00001 collumn in KaizenColumn)
            {
                colValue += "'" + collumn.FieldValue + "',";
                colname += collumn.FieldName + ",";
            }
            colValue = colValue.Substring(0, colValue.Length - 1);
            colname = colname.Substring(0, colname.Length - 1);
            string sql = "insert into CM00203(" + colname + ")";
            sql += "select " + colValue + " from KaizenTemp" + TableName;
            sql += "drop table KaizenTemp" + TableName;
            srv.ExecuteSqlCommand(sql);
            return Json(null, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetKaizenTable(string KaizenPublicKey, int ScreenID)
        {
            List<Kaizen.Configuration.Kaizen00001> result = Kaizen.BusinessLogic.Master.GetScreenFields(KaizenPublicKey, ScreenID);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetListWithPaging(string KaizenPublicKey, string kaizenTableName, int Page, int PageSize, string orderBy, string SortDirection, string filter)
        {
            List<Kaizen.Configuration.ExcelColumns> result = new List<Kaizen.Configuration.ExcelColumns>();
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            DataSourceResult resultDataSourceResult = null;
            switch (kaizenTableName)
            {
                case "CM00029":
                    CM00029Services serviceCM00029 = new CM00029Services(KaizenUser);
                    DataCollection<CM00029> LCM00029 = serviceCM00029.GetAllBYSQLQuery(filter, PageSize, Page, orderBy, SortDirection);
                    foreach (CM00029 o in LCM00029.Items)
                    {
                        result.Add(new Kaizen.Configuration.ExcelColumns() { Index = o.TRXTypeID.ToString(), ColumnName = o.TrxTypeName });
                    }
                    resultDataSourceResult = new DataSourceResult()
                    {
                        Data = result,
                        Total = LCM00029.TotalItemCount
                    };
                    break;

                case "CM00200":
                    CM00200Services serviceCM00200 = new CM00200Services(KaizenUser);
                    IList<CM00200> LCM00200 = serviceCM00200.GetAll();
                    foreach (CM00200 o in LCM00200)
                    {
                        result.Add(new Kaizen.Configuration.ExcelColumns() { Index = o.ContractID, ColumnName = o.ContractName });
                    }
                    break;
                case "CM00110":
                    CM00110Services serviceCM00110 = new CM00110Services(KaizenUser);
                    IList<CM00110> CM00110L = serviceCM00110.GetAll();
                    foreach (CM00110 o in CM00110L)
                    {
                        result.Add(new Kaizen.Configuration.ExcelColumns() { Index = o.ClientID, ColumnName = o.ClientName });
                    }

                    break;
                case "CM00105":
                    CM00105Services serviceCM00105 = new CM00105Services(KaizenUser);
                    IList<CM00105> LCM00105 = serviceCM00105.GetAll();
                    foreach (CM00105 o in LCM00105)
                    {
                        result.Add(new Kaizen.Configuration.ExcelColumns() { Index = o.AgentID, ColumnName = o.FirstName.Trim() + o.LastName.Trim() });
                    }
                    break;
                case "CM00700":
                    CM00700Services serviceCM00700 = new CM00700Services(KaizenUser);
                    IList<CM00700> LCM00700 = serviceCM00700.GetAll();
                    foreach (CM00700 o in LCM00700)
                    {
                        result.Add(new Kaizen.Configuration.ExcelColumns() { Index = o.CaseStatusID.ToString(), ColumnName = o.CaseStatusname.Trim() });
                    }
                    break;
                //case "CM00030":
                //    CM00030Services serviceCM00030 = new CM00030Services(KaizenUser);
                //    IList<CM00030> LCM00030 = serviceCM00030.GetAll();
                //    foreach (CM00030 o in LCM00030)
                //    {
                //        result.Add(new Kaizen.Configuration.ExcelColumns() { Index = o.CaseTreeID.ToString(), ColumnName = o.TreeName.Trim() });
                //    }
                //    break;
                case "CM00015":
                    CM00015Services serviceCM00015 = new CM00015Services(KaizenUser);
                    IList<CM00015> LCM00015 = serviceCM00015.GetAll();
                    foreach (CM00015 o in LCM00015)
                    {
                        result.Add(new Kaizen.Configuration.ExcelColumns()
                        {
                            Index = o.BucketID.ToString(),
                            ColumnName = o.BucketName.Trim()
                        });
                    }
                    break;
                case "CM00100":
                    //CM00100Services serviceCM00100 = new CM00100Services(KaizenUser);
                    //DataCollection<CM00100> LCM00100 = serviceCM00100.GetAllBYSQLQuery(filter, PageSize, Page, orderBy, SortDirection);
                    //foreach (CM00100 o in LCM00100.Items)
                    //{
                    //    result.Add(new Kaizen.Configuration.ExcelColumns() { Index = o.DebtorID.Trim(), ColumnName = o.EnglishFirstName.Trim() });
                    //}
                    //resultDataSourceResult = new DataSourceResult()
                    //{
                    //    Data = result,
                    //    Total = LCM00100.TotalItemCount
                    //};
                    break;
                case "CM00016":
                    CM00055Services serviceCM00016 = new CM00055Services(KaizenUser);
                    IList<CM00055> LCM00016 = serviceCM00016.GetAll();
                    foreach (CM00055 o in LCM00016)
                    {
                        //result.Add(new Kaizen.Configuration.ExcelColumns() { Index = o.JecketsID.Trim(), ColumnName = o.JecketName.Trim() });
                    }
                    break;
            }

            return Json(resultDataSourceResult, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PopUp(string KaizenPublicKey)
        {
            if (string.IsNullOrEmpty(KaizenPublicKey)) return RedirectToAction("Login", "SysUser");
            if (KaizenPublicKey.Trim() == "undefined") return RedirectToAction("Login", "SysUser");
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("Login", "SysUser");
            if (!User.Identity.IsAuthenticated) return RedirectToAction("Login", "SysUser");

            return PartialView();
        }
        public ActionResult GetDynamicTableField(string KaizenPublicKey, string kaizenTableName, int ScreenID)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            Kaizen00003Services srv = new Kaizen00003Services(KaizenUser);
            List<Kaizen.Configuration.Kaizen00003> result = srv.GetDynamicTableField(ScreenID, kaizenTableName);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetKaizenDropDownTable(string KaizenPublicKey, string KaizenDropDownTable)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            CompanyacessServices srv = new CompanyacessServices(KaizenUser);
            DataTable dt = srv.GetDataTable("select * from " + KaizenDropDownTable, "ERP");
            List<Kaizen.Configuration.ExcelColumns> result = new List<Kaizen.Configuration.ExcelColumns>();
            foreach (DataRow row in dt.Rows)
            {
                result.Add(new Kaizen.Configuration.ExcelColumns() { Index = row[0].ToString(), ColumnName = row[1].ToString() });
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
    public class TempTable
    {
        public string TableName { get; set; }
        public List<Kaizen.Configuration.ExcelColumns> ExcelColumns { get; set; }
    }

}