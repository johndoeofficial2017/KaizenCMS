using Kaizen.BusinessLogic.Configuration;
using Kaizen.CMS;
using Kaizen.CMS.DAL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Web;    
using System.Web.Mvc;

namespace UIServer.Areas.CMS.Controllers
{
    public class CMS_CaseAsginController : Controller
    {
        // GET: CMS_CaseAsgin
        public ActionResult Index(string KaizenPublicKey)
        {
            if (string.IsNullOrEmpty(KaizenPublicKey)) return RedirectToAction("Login", "SysUser");
            if (KaizenPublicKey.Trim() == "undefined") return RedirectToAction("Login", "SysUser");
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("Login", "SysUser");
            return View();
        }
        public ActionResult SaveIntegrationManagerFil(IEnumerable<HttpPostedFileBase> IntegrationManagerFileAttachemnt)
        {
            var fileName = "";
            List<Kaizen.Configuration.ExcelColumns> lstExcelColumns = null;
            if (IntegrationManagerFileAttachemnt != null)
            {
                foreach (var file in IntegrationManagerFileAttachemnt)
                {
                    fileName = Guid.NewGuid().ToString() + Path.GetFileName(file.FileName);
                    var destinationPath = Path.Combine(Server.MapPath("~/Files"), fileName);
                    file.SaveAs(destinationPath);
                    Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(System.Web.HttpContext.Current.Request["KaizenPublicKey"]);
                    Kaizen.BusinessLogic.Configuration.CompanyacessServices srv = new Kaizen.BusinessLogic.Configuration.CompanyacessServices(KaizenUser);
                    lstExcelColumns = srv.ReadExcelColumnsWithoutInsert(destinationPath, fileName);
                }
            }
            TempTable o = new TempTable();
            o.TableName = fileName;
            o.ExcelColumns = lstExcelColumns;
            return Json(o, JsonRequestBehavior.AllowGet);
        }
        public ActionResult SaveUploadedData(string KaizenPublicKey, IList<Kaizen.Configuration.Kaizen00001> KaizenColumn
            , IList<Kaizen.Configuration.Kaizen00003> Kaizen00003, string TableName)
        {
            if (string.IsNullOrEmpty(KaizenPublicKey)) return RedirectToAction("Login", "SysUser");
            if (KaizenPublicKey.Trim() == "undefined") return RedirectToAction("Login", "SysUser");
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("Login", "SysUser");
            CompanyacessServices srv = new CompanyacessServices(KaizenUser);
            var destinationPath = Path.Combine(Server.MapPath("~/Files"), TableName);
            using (var context = new CMSContext(KaizenUser.CompanyID, KaizenUser.UserName, KaizenUser.Password))
            {
                using (SqlBulkCopy bulkCopy = new SqlBulkCopy(context.Database.Connection.ConnectionString.Replace("TWO", "ERP"), SqlBulkCopyOptions.KeepNulls & SqlBulkCopyOptions.Default))
                {
                    foreach (Kaizen.Configuration.Kaizen00001 collumn in KaizenColumn)
                    {
                        bulkCopy.ColumnMappings.Add(collumn.FieldValue, collumn.FieldName);
                    }
                    bulkCopy.BatchSize = 10000;
                    bulkCopy.DestinationTableName = "CM00206";
                    KaizenResult result = new KaizenResult();
                    try
                    {
                        bulkCopy.WriteToServer(srv.ReadExcelSheet(destinationPath).CreateDataReader());
                        result.Status = true;
                        result.Massage = "Data has been Uploaded Successfully";
                        return Json(result, JsonRequestBehavior.AllowGet);
                    }
                    catch (Exception ex)
                    {
                        result.Status = false;
                        result.Massage = "Failed to Upload Data";
                        return Json(result, JsonRequestBehavior.AllowGet);
                    }
                }
            }
        }
    }
}