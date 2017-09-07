using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using Kendo.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Kaizen.BusinessLogic.CMS;
using Kaizen.CMS;
using Kaizen.BusinessLogic.Configuration;
using Kaizen.Configuration;

namespace UIServer.Areas.CMS.Controllers
{
    public class CMS_CaseTypeController : Controller
    {
        // GET: CMS_CaseType
        public ActionResult Index(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null)
                return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }

        public ActionResult ViewsConditionBuilder(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null)
                return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }
        public ActionResult Create(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null)
                return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }
        #region
        private DataSourceResult CaseList([DataSourceRequest]DataSourceRequest request, Kaizen.CMS.DataCollection<CM00029> L)
        {
            int tempPage = request.Page;
            DataSourceResult result;
            if (L != null && L.Items != null)
            {

                request.Page = 1;
                result = L.Items.ToDataSourceResult(request,
               o => new CM00029
               {
                   TRXTypeID = o.TRXTypeID,
                   TrxTypeName = o.TrxTypeName,
                   NextDocumentNumber = o.NextDocumentNumber,
                   NextNumberlenth = o.NextNumberlenth,
                   NextNumberPrefix = o.NextNumberPrefix
               }
               );
                result.Total = L.TotalItemCount;
                request.Page = tempPage;
            }
            else
            {
                result = new DataSourceResult()
                {
                    Data = new List<CM00029>(),
                    Total = 0
                };
            }
            return result;
        }
        public JsonResult GetDataListGrid([DataSourceRequest]DataSourceRequest request, string Searchcritaria, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = Kaizen.BusinessLogic.Configuration.UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return Json(null, JsonRequestBehavior.AllowGet);

            CM00029Services serv = new CM00029Services(KaizenUser);
            Kaizen.CMS.DataCollection<CM00029> L = new Kaizen.CMS.DataCollection<CM00029>();
            string filters = string.Empty;
            string SortMember;
            bool IsAscending;
            SortDescriptor sortobject = request.Sorts.FirstOrDefault();
            if (sortobject != null)
            {
                SortMember = sortobject.Member;
                if (sortobject.SortDirection == System.ComponentModel.ListSortDirection.Ascending)
                    IsAscending = true;
                else
                    IsAscending = false;
            }
            else
            {
                SortMember = "TRXTypeID";
                IsAscending = true;
            }
            string SQLQuery = string.Empty;
            if (request.Filters.Count > 0)
                SQLQuery = Help.ApplyFilter(request.Filters[0]);

            L = serv.GetAllViewBYSQLQuery(SQLQuery, Searchcritaria
                , request.PageSize, request.Page, SortMember, IsAscending);
            DataSourceResult result = CaseList(request, L);
            var jsonResult = Json(result, JsonRequestBehavior.AllowGet);
            return jsonResult;
        }
        #endregion
        public ActionResult SaveData(CM00029 CM00029, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00029Services service = new CM00029Services(KaizenUser);
            return Json(service.AddCM00029(CM00029), JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateData(CM00029 CM00029, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00029Services service = new CM00029Services(KaizenUser);
            return Json(service.Update(CM00029), JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteData(CM00029 CM00029, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00029Services service = new CM00029Services(KaizenUser);
            return Json(service.Delete(CM00029), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetSingle(string KaizenPublicKey, int TRXTypeID)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00029Services service = new CM00029Services(KaizenUser);
            CM00029 L = service.GetSingle(TRXTypeID);
            return Json(L, JsonRequestBehavior.AllowGet);
        }
        public ActionResult FillDropDownList(string KaizenPublicKey)
        {
            KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            CM00029Services service = new CM00029Services(KaizenUser);
            IList<CM00029> L = service.GetAll();
            return Json(L, JsonRequestBehavior.AllowGet);
        }
        public ActionResult MYFillDropDownList(string KaizenPublicKey)
        {
            KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            CM00029Services service = new CM00029Services(KaizenUser);
            IList<CM00029> L = service.GetAllMe();
            return Json(L, JsonRequestBehavior.AllowGet);
        }
        #region Status Group Role
        public ActionResult CaseTypeRole(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }

        public ActionResult GetRolesByCaseType(string KaizenPublicKey, int TRXTypeID)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            CM00029Services service = new CM00029Services(KaizenUser);
            IList<CM00056> o = service.GetRolesByCaseType(TRXTypeID);
            return Json(o, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SaveRole(CM00056 CM00056, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00029Services service = new CM00029Services(KaizenUser);
            return Json(service.AddCM00056(CM00056), JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteRole(CM00056 CM00056, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00029Services service = new CM00029Services(KaizenUser);
            return Json(service.DeleteCM00056(CM00056), JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Status Group User
        public ActionResult CaseTypeUser(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }
        public ActionResult GetCaseTypeByUser(string KaizenPublicKey, string userName)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            CM00029Services service = new CM00029Services(KaizenUser);
            IList<CM00057> o = service.GetCaseTypeByUser(userName);
            return Json(o, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteCaseTypeByUser(CM00057 CM00057, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00029Services service = new CM00029Services(KaizenUser);
            return Json(service.DeleteCaseTypeByUser(CM00057), JsonRequestBehavior.AllowGet);
        }
        public ActionResult AddCaseTypeByUser(CM00057 CM00057, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00029Services service = new CM00029Services(KaizenUser);
            return Json(service.AddCM00057(CM00057), JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Case Type Product
        public ActionResult CaseTypeProduct(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }
        public ActionResult GetProductsByCaseType(string KaizenPublicKey, int TRXTypeID)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            CM00029Services service = new CM00029Services(KaizenUser);
            IList<CM00055> o = service.GetProductsByCaseType(TRXTypeID);
            return Json(o, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SaveProduct(CM00055 CM00055, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00055Services service = new CM00055Services(KaizenUser);
            return Json(service.AddCM00055(CM00055), JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateProduct(CM00055 CM00055, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00055Services service = new CM00055Services(KaizenUser);
            return Json(service.Update(CM00055), JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteProduct(CM00055 CM00055, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00055Services service = new CM00055Services(KaizenUser);
            return Json(service.Delete(CM00055), JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Case Type Fields
        public ActionResult CaseTypeFields(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null)
                return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }

        public ActionResult FillFieldTypeList(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            CM00029Services service = new CM00029Services(KaizenUser);
            IList<CM00028> list = service.GetFieldTypeList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SaveCaseTypeField(CM00070 CM00070, string KaizenPublicKey)
        {
            KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00029Services service = new CM00029Services(KaizenUser);
            return Json(service.AddCM00070(CM00070), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetFieldsByCaseType(string KaizenPublicKey, int TRXTypeID)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            CM00029Services service = new CM00029Services(KaizenUser);
            List<CM00070> o = service.GetFieldsByCaseType(TRXTypeID).OrderBy(x => x.FieldOrder).ToList();
            return Json(o, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UpdateField(CM00070 CM00070, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00029Services service = new CM00029Services(KaizenUser);
            return Json(service.Update(CM00070), JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteField(CM00070 CM00070, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00029Services service = new CM00029Services(KaizenUser);
            return Json(service.Delete(CM00070), JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Case Type Views
        public ActionResult CaseTypeViews(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null)
                return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }

        public ActionResult GetViews(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            CM00029Services service = new CM00029Services(KaizenUser);
            IList<CM00071> o = service.GetViews();
            return Json(o, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetViewsByCaseType(string KaizenPublicKey, int TRXTypeID)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            CM00029Services service = new CM00029Services(KaizenUser);
            IList<CM00071> o = service.GetViewsByCaseType(TRXTypeID);
            return Json(o, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SaveCaseTypeView(CM00071 CM00071, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00029Services service = new CM00029Services(KaizenUser);
            return Json(service.AddCM00071(CM00071), JsonRequestBehavior.AllowGet);
        }

        public ActionResult UpdateView(CM00071 CM00071, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00029Services service = new CM00029Services(KaizenUser);
            return Json(service.Update(CM00071), JsonRequestBehavior.AllowGet);
        }

        //public ActionResult UpdateViewList(IList<CM00071> CM00071, string KaizenPublicKey)
        //{
        //    Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
        //    if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
        //    CM00029Services service = new CM00029Services(KaizenUser);
        //    return Json(service.Update(CM00071), JsonRequestBehavior.AllowGet);
        //}
        public ActionResult DeleteView(CM00071 CM00071, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00029Services service = new CM00029Services(KaizenUser);
            return Json(service.Delete(CM00071), JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region Case Type Views Fields
        public ActionResult CaseTypeViewsFields(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null)
                return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }
        public ActionResult GetAllFieldsByCaseType(string KaizenPublicKey, int TRXTypeID)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            CM00029Services service = new CM00029Services(KaizenUser);
            IList<CM00074> o = service.GetAllFieldsByCaseType(TRXTypeID).OrderBy(x => x.FieldOrder).ToList();
            return Json(o, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetAllFieldsByView(string KaizenPublicKey, int ViewID)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            CM00029Services service = new CM00029Services(KaizenUser);
            IList<CM00075> o = service.GetAllFieldsByView(ViewID).OrderBy(x => x.FieldOrder).ToList();
            return Json(o, JsonRequestBehavior.AllowGet);
        }
        public ActionResult SaveViewsFields(CM00075 CM00075, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00029Services service = new CM00029Services(KaizenUser);
            return Json(service.AddCM00075(CM00075), JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateViewsFields(IList<CM00075> CM00075List, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00029Services service = new CM00029Services(KaizenUser);
            return Json(service.UpdateCM00075(CM00075List), JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteViewsFields(CM00075 CM00075, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00029Services service = new CM00029Services(KaizenUser);
            return Json(service.DeleteCM00075(CM00075), JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Case Type View User
        public ActionResult CaseTypeViewsUser(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null)
                return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }
        public ActionResult AddCaseTypeViewByUser(CM00073 CM00073, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00029Services service = new CM00029Services(KaizenUser);
            return Json(service.AddCM00073(CM00073), JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateCaseTypeViewByUser(CM00073 CM00073, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00029Services service = new CM00029Services(KaizenUser);
            return Json(service.UpdateCM00073(CM00073), JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteCaseTypeViewUser(CM00073 CM00073, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00029Services service = new CM00029Services(KaizenUser);
            return Json(service.DeleteCaseTypeViewUser(CM00073), JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetCaseTypeViewsByUser(string KaizenPublicKey, int TRXTypeID, string userName)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            CM00029Services service = new CM00029Services(KaizenUser);
            IList<CM00073> o = service.GetCaseTypeViewsByUser(TRXTypeID, userName);
            return Json(o, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region Case Type View Roles
        public ActionResult CaseTypeViewsRole(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null)
                return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }

        public ActionResult GetRolesByView(string KaizenPublicKey, int ViewID)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            CM00029Services service = new CM00029Services(KaizenUser);
            IList<CM00072> o = service.GetRolesByView(ViewID);
            return Json(o, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SaveViewRole(CM00072 CM00072, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00029Services service = new CM00029Services(KaizenUser);
            return Json(service.AddCM00072(CM00072), JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteViewRole(CM00072 CM00072, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00029Services service = new CM00029Services(KaizenUser);
            return Json(service.DeleteCM00072(CM00072), JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Case Type Standard fields
        public ActionResult CaseTypeStandardFields(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null)
                return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }
        public ActionResult GetStandardFieldsByCaseType(string KaizenPublicKey, int TRXTypeID)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            CM00029Services service = new CM00029Services(KaizenUser);
            IList<CM00074> o = service.GetStandardFieldsByCaseType(TRXTypeID).OrderBy(x => x.FieldOrder).ToList(); ;
            return Json(o, JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateStandardFields(IList<CM00074> CM00074List, string KaizenPublicKey, int callNumber)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00029Services service = new CM00029Services(KaizenUser);
            return Json(service.UpdateCM00074(CM00074List, callNumber), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetStandardFields(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            CM00029Services service = new CM00029Services(KaizenUser);
            IList<CM00038> result = service.GetStandardFields();

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteCM00074(CM00074 CM00074, string KaizenPublicKey)
        { 
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00029Services service = new CM00029Services(KaizenUser);
            return Json(service.DeleteCM00074(CM00074), JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Case Type Graph Setup
        public ActionResult CaseTypeGraphSetup(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null)
                return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }
        public ActionResult GetGraphTypes(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            CM00029Services service = new CM00029Services(KaizenUser);
            IList<CM00040> L = service.GetGraphTypes();
            return Json(L, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetGraphData(string KaizenPublicKey, int ViewID)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            CM00029Services service = new CM00029Services(KaizenUser);
            IList<CM00076> L = service.GetGraphData(ViewID);
            return Json(L, JsonRequestBehavior.AllowGet);
        }
        public ActionResult AddCM00076(CM00076 CM00076, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00029Services service = new CM00029Services(KaizenUser);
            return Json(service.AddCM00076(CM00076), JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateCM00076(CM00076 CM00076, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00029Services service = new CM00029Services(KaizenUser);
            return Json(service.UpdateCM00076(CM00076), JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteCM00076(CM00076 CM00076, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00029Services service = new CM00029Services(KaizenUser);
            return Json(service.DeleteCM00076(CM00076), JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Case Type Graph View
        public ActionResult CaseTypeGraphView(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null)
                return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }
        public ActionResult AddCM00077(CM00077 CM00077, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00029Services service = new CM00029Services(KaizenUser);
            return Json(service.AddCM00077(CM00077), JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateCM00077(IList<CM00077> CM00077List, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00029Services service = new CM00029Services(KaizenUser);
            return Json(service.UpdateCM00077(CM00077List), JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteCM00077(CM00077 CM00077, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00029Services service = new CM00029Services(KaizenUser);
            return Json(service.DeleteCM00077(CM00077), JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateGraphViewsFields(IList<CM00077> CM00077List, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00029Services service = new CM00029Services(KaizenUser);
            return Json(service.UpdateCM00077(CM00077List), JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region For CM00078
        public ActionResult GetSummaryTypes(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            CM00029Services service = new CM00029Services(KaizenUser);
            IList<CM00047> L = service.GetSummaryTypes();
            return Json(L, JsonRequestBehavior.AllowGet);
        }
        public ActionResult AddCM00078(CM00078 CM00078, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00029Services service = new CM00029Services(KaizenUser);
            return Json(service.AddCM00078(CM00078), JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateCM00078(IList<CM00078> CM00078List, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00029Services service = new CM00029Services(KaizenUser);
            return Json(service.UpdateCM00078(CM00078List), JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteCM00078(CM00078 CM00078, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00029Services service = new CM00029Services(KaizenUser);
            return Json(service.DeleteCM00078(CM00078), JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateGraphViewsFields(IList<CM00078> CM00078List, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00029Services service = new CM00029Services(KaizenUser);
            return Json(service.UpdateCM00078(CM00078List), JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetGraphViewsFields(string KaizenPublicKey, int GraphID)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            CM00029Services service = new CM00029Services(KaizenUser);
            IList<CM00077> L = service.GetGraphViewsFields(GraphID);
            return Json(L, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetGraphViewsFields_Summary(string KaizenPublicKey, int GraphID)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            CM00029Services service = new CM00029Services(KaizenUser);
            IList<CM00078> L = service.GetGraphViewsFields_Summary(GraphID);
            return Json(L, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region - Case Type Emails
        public ActionResult CaseTypeEmail(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null)
                return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }
        public ActionResult GetEmailSettings(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            Kaizen00020Services service = new Kaizen00020Services(KaizenUser);
            IList<Kaizen00020> L = service.GetEmailSettings();
            return Json(L, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Case Type Equation Fields
        public ActionResult CaseTypeEquationFields(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null)
                return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }

        public ActionResult FillFieldCodeList(string KaizenPublicKey, int FieldTypeID)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            CM00029Services service = new CM00029Services(KaizenUser);
            IList<CM00024> list = service.FillFieldCodeList(FieldTypeID);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetEquationFieldsByCaseType(string KaizenPublicKey, int TRXTypeID)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            CM00029Services service = new CM00029Services(KaizenUser);
            List<CM00080> o = service.GetEquationFieldsByCaseType(TRXTypeID).OrderBy(x => x.FieldOrder).ToList();
            return Json(o, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SaveEquationField(CM00080 CM00080, string KaizenPublicKey)
        {
            KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00029Services service = new CM00029Services(KaizenUser);
            return Json(service.AddCM00080(CM00080), JsonRequestBehavior.AllowGet);
        }

        public ActionResult UpdateEquationField(CM00080 CM00080, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00029Services service = new CM00029Services(KaizenUser);
            return Json(service.Update(CM00080), JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteEquationField(CM00080 CM00080, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00029Services service = new CM00029Services(KaizenUser);
            return Json(service.Delete(CM00080), JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Case Type Equation Fields By View
        public ActionResult CaseTypeEquationFieldsByView(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null)
                return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }
        public ActionResult GetViews_EquationFieldsByCaseType(string KaizenPublicKey, int TRXTypeID)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            CM00029Services service = new CM00029Services(KaizenUser);
            IList<CM00071> L = service.GetViews_EquationFieldsByCaseType(TRXTypeID);
            return Json(L, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetStandardFieldsByCaseTypeAndView(string KaizenPublicKey, int TRXTypeID, int ViewID)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            CM00029Services service = new CM00029Services(KaizenUser);
            IList<CM00075> o = service.GetStandardFieldsByCaseTypeAndView(TRXTypeID, ViewID).OrderBy(x => x.FieldOrder).ToList(); ;
            return Json(o, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetEquationFieldsByCaseTypeAndView(string KaizenPublicKey, int TRXTypeID, int ViewID)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            CM00029Services service = new CM00029Services(KaizenUser);
            List<CM00081> o = service.GetEquationFieldsByCaseTypeAndView(TRXTypeID, ViewID).OrderBy(x => x.FieldOrder).ToList();
            return Json(o, JsonRequestBehavior.AllowGet);
        }
        public ActionResult SaveEquationFieldByView(CM00081 CM00081, string KaizenPublicKey)
        {
            KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00029Services service = new CM00029Services(KaizenUser);
            return Json(service.AddCM00081(CM00081), JsonRequestBehavior.AllowGet);
        }

        public ActionResult UpdateEquationFieldByView(CM00081 CM00081, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00029Services service = new CM00029Services(KaizenUser);
            return Json(service.Update(CM00081), JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteEquationFieldByView(CM00081 CM00081, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00029Services service = new CM00029Services(KaizenUser);
            return Json(service.Delete(CM00081), JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Case Type Show Graph
        public ActionResult CaseTypeShowGraph(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null)
                return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }

        public ActionResult GetGraphs(string KaizenPublicKey,int ViewID)
        {
            KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            CM00029Services service = new CM00029Services(KaizenUser);
            IList<CM00076> o = service.GetGraphs(ViewID);
            List<CM00076> objToReturn = new List<CM00076>();
            foreach (CM00076 obj in o)
            {
                CM00076 CM00076Obj = new CM00076()
                {
                    GraphID = obj.GraphID,
                    GraphName = obj.GraphName,
                    GraphTitle = obj.GraphTitle,
                    GraphTypeID = obj.GraphTypeID,
                    ViewID = obj.ViewID
                };
                CM00040 CM00040Obj = new CM00040()
                {
                    GraphTypeID = obj.CM00040.GraphTypeID,
                    GraphTypeName = obj.CM00040.GraphTypeName
                };

                CM00076Obj.CM00040 = CM00040Obj;
                objToReturn.Add(CM00076Obj);
            }
            return Json(objToReturn, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GraphSettings(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            //CM00029Services service = new CM00029Services(KaizenUser);
            dynamic graphObj = new System.Dynamic.ExpandoObject();
            graphObj.legendPosition = "bottom";
            graphObj.seriesDefaultsstyle = "smooth";
            graphObj.valueAxisLineVisible = -10;
            return Json(graphObj, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GraphData(string KaizenPublicKey, int GraphID, string GraphTypeName)
        {
            KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            CM00029Services service = new CM00029Services(KaizenUser);

            System.Data.DataTable graphData = service.GetDynamicDataSource(GraphID, GraphTypeName);

            string[] catArr = graphData.Rows.Cast<System.Data.DataRow>()
                    .Select(row => row[0].ToString())
                    .ToArray();

            var series = new List<object>();

            string[] seriesNames = service.GetDynamicSeriesNames(GraphID).Cast<string>().ToArray();

            CM00076 graphDetails = service.GetGraphDataById(GraphID).FirstOrDefault();

            string[] categories = catArr;

            if (GraphTypeName.Equals("pie", StringComparison.OrdinalIgnoreCase)||
                GraphTypeName.Equals("funnel", StringComparison.OrdinalIgnoreCase))
            {
                for (int i = 0; i < graphData.Rows.Count; i++)
                {
                    object seriesObj = new { category = graphData.Rows[i][0], value = graphData.Rows[i][1] };
                    series.Add(seriesObj);
                }
            }
            else if (GraphTypeName.Equals("donut", StringComparison.OrdinalIgnoreCase))
            {
                for (int i = 0; i < graphData.Rows.Count; i++)
                {
                    object seriesObj = new { category = graphData.Rows[i][0], value = graphData.Rows[i][1] };
                    series.Add(seriesObj);
                }
            }
            else
            {
                for (int i = 0; i < graphData.Columns.Count - 1; i++)
                {
                    double[] seriesArr = graphData.Rows.Cast<System.Data.DataRow>()
                        .Select(row => Convert.ToDouble(row[i + 1]))
                        .ToArray();

                    object seriesObj = new { name = seriesNames[i], data = seriesArr };
                    series.Add(seriesObj);
                }
            }
            object res = new
            {
                GraphID = 1,
                GraphName = graphDetails.GraphName,
                GraphType = GraphTypeName,
                //TableName = "CM00040",
                Title = graphDetails.GraphTitle,
                Categories = categories,
                Series = series
            };

            return Json(res, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Case Type Lookup Field
        public ActionResult CaseTypeLookupField(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null)
                return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }
        public ActionResult GetAllCM00070(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            CM00029Services service = new CM00029Services(KaizenUser);
            IList<CM00070> o = service.GetAllCM00070();
            return Json(o, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetFieldNames(string KaizenPublicKey, int TRXTypeID)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            CM00029Services service = new CM00029Services(KaizenUser);
            IList<CM00070> o = service.GetFieldNames(TRXTypeID);
            return Json(o, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetCaseTypeLookupFields(string KaizenPublicKey, int TRXTypeID)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            CM00029Services service = new CM00029Services(KaizenUser);
            IList<CM00050> o = service.GetCaseTypeLookupFields(TRXTypeID);
            return Json(o, JsonRequestBehavior.AllowGet);
        }
        public ActionResult SaveLookupField(CM00050 CM00050, string KaizenPublicKey)
        {
            KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00029Services service = new CM00029Services(KaizenUser);
            return Json(service.AddCM00050(CM00050), JsonRequestBehavior.AllowGet);
        }

        public ActionResult UpdateLookupField(CM00050 CM00050, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00029Services service = new CM00029Services(KaizenUser);
            return Json(service.Update(CM00050), JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteLookupField(CM00050 CM00050, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00029Services service = new CM00029Services(KaizenUser);
            return Json(service.Delete(CM00050), JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Case Type Field Order
        public ActionResult CaseTypeFieldOrder(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null)
                return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }
        public ActionResult UpdateStandardFieldOrder(IList<CM00074> CM00074List, string KaizenPublicKey, int callNumber)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00029Services service = new CM00029Services(KaizenUser);
            return Json(service.UpdateStandardFieldOrder(CM00074List, callNumber), JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region View Field Summary CM00082
        public ActionResult ViewFieldSummary(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null)
                return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }
        public ActionResult AddCM00082(CM00082 CM00082, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00029Services service = new CM00029Services(KaizenUser);
            return Json(service.AddCM00082(CM00082), JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateCM00082(IList<CM00082> CM00082List, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00029Services service = new CM00029Services(KaizenUser);
            return Json(service.UpdateCM00082(CM00082List), JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteCM00082(CM00082 CM00082, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00029Services service = new CM00029Services(KaizenUser);
            return Json(service.DeleteCM00082(CM00082), JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateGraphViewsFields(IList<CM00082> CM00082List, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00029Services service = new CM00029Services(KaizenUser);
            return Json(service.UpdateCM00082(CM00082List), JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region For CM00083
        public ActionResult GetViewFieldSummaryTypes(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            CM00029Services service = new CM00029Services(KaizenUser);
            IList<CM00047> L = service.GetViewFieldSummaryTypes();
            return Json(L, JsonRequestBehavior.AllowGet);
        }
        public ActionResult AddCM00083(CM00083 CM00083, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00029Services service = new CM00029Services(KaizenUser);
            return Json(service.AddCM00083(CM00083), JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateCM00083(IList<CM00083> CM00083List, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00029Services service = new CM00029Services(KaizenUser);
            return Json(service.UpdateCM00083(CM00083List), JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteCM00083(CM00083 CM00083, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00029Services service = new CM00029Services(KaizenUser);
            return Json(service.DeleteCM00083(CM00083), JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateGraphViewsFields(IList<CM00083> CM00083List, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00029Services service = new CM00029Services(KaizenUser);
            return Json(service.UpdateCM00083(CM00083List), JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetCM00082(string KaizenPublicKey, int ViewID)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            CM00029Services service = new CM00029Services(KaizenUser);
            IList<CM00082> L = service.GetCM00082(ViewID);
            return Json(L, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetCM00083(string KaizenPublicKey, int ViewID)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            CM00029Services service = new CM00029Services(KaizenUser);
            IList<CM00083> L = service.GetCM00083(ViewID);
            return Json(L, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region CM00084
        public ActionResult ViewPivotTable(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null)
                return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }
        public ActionResult AddCM00084(CM00084 CM00084, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00029Services service = new CM00029Services(KaizenUser);
            return Json(service.AddCM00084(CM00084), JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateCM00084(IList<CM00084> CM00084List, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00029Services service = new CM00029Services(KaizenUser);
            return Json(service.UpdateCM00084(CM00084List), JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteCM00084(CM00084 CM00084, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00029Services service = new CM00029Services(KaizenUser);
            return Json(service.DeleteCM00084(CM00084), JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region For CM00085
        public ActionResult AddCM00085(CM00085 CM00085, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00029Services service = new CM00029Services(KaizenUser);
            return Json(service.AddCM00085(CM00085), JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateCM00085(IList<CM00085> CM00085List, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00029Services service = new CM00029Services(KaizenUser);
            return Json(service.UpdateCM00085(CM00085List), JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteCM00085(CM00085 CM00085, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00029Services service = new CM00029Services(KaizenUser);
            return Json(service.DeleteCM00085(CM00085), JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetCM00084(string KaizenPublicKey, int ViewID)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            CM00029Services service = new CM00029Services(KaizenUser);
            IList<CM00084> L = service.GetCM00084(ViewID);
            return Json(L, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetCM00085(string KaizenPublicKey, int ViewID)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            CM00029Services service = new CM00029Services(KaizenUser);
            IList<CM00085> L = service.GetCM00085(ViewID);
            return Json(L, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region For CM00086
        public ActionResult AddCM00086(CM00086 CM00086, string KaizenPublicKey)
        { 
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00029Services service = new CM00029Services(KaizenUser);
            return Json(service.AddCM00086(CM00086), JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateCM00086(IList<CM00086> CM00086List, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00029Services service = new CM00029Services(KaizenUser);
            return Json(service.UpdateCM00086(CM00086List), JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteCM00086(CM00086 CM00086, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00029Services service = new CM00029Services(KaizenUser);
            return Json(service.DeleteCM00086(CM00086), JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetCM00086(string KaizenPublicKey, int ViewID)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            CM00029Services service = new CM00029Services(KaizenUser);
            IList<CM00086> L = service.GetCM00086(ViewID);
            return Json(L, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region CM00079
        public ActionResult GraphCategory(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null)
                return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }
        public ActionResult AddCM00079(CM00079 CM00079, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00029Services service = new CM00029Services(KaizenUser);
            return Json(service.AddCM00079(CM00079), JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateCM00079(IList<CM00079> CM00079List, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00029Services service = new CM00029Services(KaizenUser);
            return Json(service.UpdateCM00079(CM00079List), JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteCM00079(CM00079 CM00079, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00029Services service = new CM00029Services(KaizenUser);
            return Json(service.DeleteCM00079(CM00079), JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateGraphViewsFields(IList<CM00079> CM00079List, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00029Services service = new CM00029Services(KaizenUser);
            return Json(service.UpdateCM00079(CM00079List), JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetCM00079(string KaizenPublicKey, int GraphID)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            CM00029Services service = new CM00029Services(KaizenUser);
            IList<CM00079> L = service.GetCM00079(GraphID);
            return Json(L, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region CM00037
        public ActionResult CaseTypeTables(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null)
                return RedirectToAction("LoginToCompany", "SysUser");
            return PartialView();
        }
        public ActionResult AddCM00037(CM00037 CM00037, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00029Services service = new CM00029Services(KaizenUser);
            return Json(service.AddCM00037(CM00037), JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateCM00037(CM00037 CM00037, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00029Services service = new CM00029Services(KaizenUser);
            return Json(service.UpdateCM00037(CM00037), JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteCM00037(CM00037 CM00037, string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("LoginToCompany", "SysUser");
            CM00029Services service = new CM00029Services(KaizenUser);
            return Json(service.DeleteCM00037(CM00037), JsonRequestBehavior.AllowGet);
        }
        
        public ActionResult GetCM00037(string KaizenPublicKey, string TableSource)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            CM00029Services service = new CM00029Services(KaizenUser);
            IList<CM00037> L = service.GetCM00037(TableSource);
            return Json(L, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetAllCM00037(string KaizenPublicKey)
        {
            Kaizen.Configuration.KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            CM00029Services service = new CM00029Services(KaizenUser);
            IList<CM00037> L = service.GetAllCM00037();
            return Json(L, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}