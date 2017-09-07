using System;
using Kendo.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using Kaizen.BusinessLogic.Configuration;
using Kaizen.Configuration;
using UIServer.Models;
using System.IO;
using System.Web;

namespace UIServer.Controllers
{
    public class SysUserController : Controller
    {
        // GET: SysUser
        [Authorize]
        public ActionResult Index(string KaizenPublicKey)
        {
            KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("Login", "SysUser");
            return PartialView();
        }
        public ActionResult Create(string KaizenPublicKey)
        {
            KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("Login", "SysUser");
            return PartialView();
        }
        public ActionResult CreateEdit(string KaizenPublicKey)
        {
            KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("Login", "SysUser");
            return PartialView();
        }
        public ActionResult PopUp(string KaizenPublicKey)
        {
            KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("Login", "SysUser");
            return PartialView();
        }
        public ActionResult MyProfile(string KaizenPublicKey)
        {
            KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("Login", "SysUser");
            return PartialView();
        }
        public ActionResult ChangeUserPassword(string KaizenPublicKey)
        {
            KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("Login", "SysUser");
            return PartialView();
        }
        public ActionResult EncryptPassword(string KaizenPublicKey)
        {
            KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("Login", "SysUser");
            return PartialView();
        }
        public ActionResult PasswordEncryption(string KaizenPublicKey, string Password)
        {
            KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("Login", "SysUser");
            UserServices service = new UserServices(KaizenUser);
            return Json(service.PasswordEncryption(Password), JsonRequestBehavior.AllowGet);
        }
        public ActionResult PasswordDecryption(string KaizenPublicKey, string Password)
        {
            KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("Login", "SysUser");
            UserServices service = new UserServices(KaizenUser);
            return Json(service.PasswordDecryption(Password), JsonRequestBehavior.AllowGet);
        }


        #region Grid Actions
        private DataSourceResult CaseList([DataSourceRequest]DataSourceRequest request, DataCollection<User> L)
        {
            int tempPage = request.Page;
            DataSourceResult result;
            if (L != null && L.Items != null)
            {
                request.Page = 1;
                result = L.Items.ToDataSourceResult(request,
               o => new User
               {
                   UserName = o.UserName,
                   FirstName = string.IsNullOrEmpty(o.FirstName) ? "" : o.FirstName.Length > 15 ? o.FirstName.Substring(0, 15) : o.FirstName,
                   LastName = string.IsNullOrEmpty(o.LastName) ? "" : o.LastName.Length > 15 ? o.LastName.Substring(0, 15) : o.LastName,
                   //FullName = string.IsNullOrEmpty(o.FullName) ? "" : o.FullName.Length > 15 ? o.FullName.Substring(0, 15) : o.FullName,
                   IsDisabled = o.IsDisabled,
                   PhotoExtension = o.PhotoExtension,
                   IsCustomer = o.IsCustomer,
                   IsVendor = o.IsVendor,
                   IsDebtor = o.IsDebtor,
                   IsPartner = o.IsPartner,
                   IsAgent = o.IsAgent,
                   IsClient = o.IsClient,
                   IsEmployee = o.IsEmployee,
                   LastLogin = o.LastLogin,
                   IsPasswordchange = o.IsPasswordchange
               }
               );
                result.Total = L.TotalItemCount;
                request.Page = tempPage;
            }
            else
            {
                result = new DataSourceResult()
                {
                    Data = new List<User>(),
                    Total = 0
                };
            }
            return result;
        }
        public ActionResult GetListPopUpGrid([DataSourceRequest]DataSourceRequest request, string KaizenPublicKey
, string FieldID, int FltrOperator, string Searchcritaria)
        {
            KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("Login", "SysUser");
            UserServices invService = new UserServices(KaizenUser);
            DataCollection<User> L = new DataCollection<User>();
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
                SortMember = "UserName";
                IsAscending = true;
            }
            string SQLQuery = string.Empty;
            if (request.Filters.Count > 0)
                SQLQuery = Help.ApplyFilter(request.Filters[0]);

            L = invService.GetAllViewBYSQLQuery(SQLQuery, FieldID, FltrOperator, Searchcritaria
                , request.PageSize, request.Page, SortMember, IsAscending);
            DataSourceResult result = CaseList(request, L);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetDataListGrid([DataSourceRequest]DataSourceRequest request, string Searchcritaria, string KaizenPublicKey)
        {
            KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("Login", "SysUser");
            UserServices serv = new UserServices(KaizenUser);
            DataCollection<User> L = new DataCollection<User>();
            string filters = string.Empty;
            string SortMember = "UserName";
            bool IsAscending = false;
            if (request.Sorts != null)
            {
                SortDescriptor sortobject = request.Sorts.FirstOrDefault();
                if (sortobject != null)
                {
                    SortMember = sortobject.Member;
                    if (sortobject.SortDirection == System.ComponentModel.ListSortDirection.Ascending)
                        IsAscending = true;
                    else
                        IsAscending = false;
                }
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

        #region Authenticate
        [AllowAnonymous]
        [HttpGet]
        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                KaizenSession KaizenUser = UserServices.GetMySession(User.Identity.Name);
                if (KaizenUser != null)
                    return RedirectToAction("LoginToCompany", "SysUser");
            }
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(UserLogin loginViewModel)
        {
            UserServices srv = new UserServices();

            var authenticationErrorMessage = string.Empty;
            var isAuthenticUser = false;

            try
            {
                isAuthenticUser = srv.AuthenticateUser(loginViewModel.UserName, loginViewModel.Password);
            }
            catch (Exception e)
            {
                authenticationErrorMessage = e.Message;
            }


            if (!ModelState.IsValid || isAuthenticUser == false)
            {
                Kaizen.BusinessLogic.ExceptionUtility.LogInDataFailed(loginViewModel.UserName);
                ModelState.AddModelError(string.Empty, authenticationErrorMessage);
                return View();
            }
            Kaizen.BusinessLogic.ExceptionUtility.LogInData(loginViewModel.UserName);
            //var tempUser = Kaizen.BusinessLogic.Master.OnlineKaizenUser.Find(ss => ss.UserName == loginViewModel.UserName.Trim());
            //if (tempUser != null)
            //{
            //    tempUser = Kaizen.BusinessLogic.Master.OnlineKaizenUser.Find(ss => ss.Status == 1);
            //    if (tempUser == null)
            //    {

            //    }
            //    tempUser.Password = loginViewModel.Password;
            //}
            //else
            //{

            //}
            //KaizenSession UserKaizen = new KaizenSession()
            //{
            //    UserName = loginViewModel.UserName.Trim(),
            //    Password = loginViewModel.Password,
            //    LoginDate = DateTime.Now
            //};
            //Kaizen.BusinessLogic.Master.OnlineKaizenUser.Add(UserKaizen);
            FormsAuthentication.SetAuthCookie(loginViewModel.UserName, false);
            return RedirectToAction("LoginToCompany", "SysUser");
        }
        [Authorize]
        public ActionResult LoginToCompany()
        {
            //KaizenSession KaizenUser = UserServices.GetMySession(User.Identity.Name);
            //if (KaizenUser == null)
            //{
            //    FormsAuthentication.SignOut();
            //    return RedirectToAction("Login", "SysUser");
            //}
            UserServices srv = new UserServices();
            User U = srv.GetSingleFromKaizen(User.Identity.Name);
            if (U == null)
            {
                UserServices.RemoveFreeKaizenSession(User.Identity.Name);
                FormsAuthentication.SignOut();
                return RedirectToAction("Login", "SysUser");
            }
            return View(U);
        }
        public ActionResult LogKaizenSession(string CompanyID)
        {
            KaizenSession ouser = null;
            UserServices srv = new UserServices();
            List<KaizenSession> ouserList = UserServices.GetUserSessions(User.Identity.Name);
            if (ouserList.Count > 0)
            {
                ouser = ouserList.Find(ss => ss.CompanyID == CompanyID);
            }
            if (ouser == null)
            {
                User oUser = srv.GetSingleFromKaizen(User.Identity.Name);
                ouser = new KaizenSession();
                ouser.Password = "lw131";
                //ouser.Password = srv.PasswordDecryption(oUser.UserPassword);

            }
            else
            {
                ouser.KaizenPublicKey = Guid.NewGuid();
            }
            ouser.CompanyID = CompanyID;
            //ouser.UserName = User.Identity.Name;
            ouser.UserName = "sa";
            ouser.KaizenPublicKey = Guid.NewGuid();
            srv.CreateSession(ouser);
            return Json(ouser.KaizenPublicKey, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Logout(string KaizenPublicKey)
        {
            UserServices.RemoveSessionByKaizenPublicKey(KaizenPublicKey);
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "SysUser");
        }
        public ActionResult CompanySignOut()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Login", "SysUser");
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult CompanyChangePassword()
        {
            UserServices srv = new UserServices();
            User U = srv.GetSingleFromKaizen(User.Identity.Name);
            if (U == null)
            {
                FormsAuthentication.SignOut();
                return RedirectToAction("Login", "SysUser");
            }
            ChangePasswordViewModel vm = new ChangePasswordViewModel()
            {
                UserName = U.UserName,
                FirstName = U.FirstName,
                LastName = U.LastName,
                PhotoExtension = U.PhotoExtension,
            };
            return View(vm);
        }
        [AllowAnonymous]
        [HttpPost]
        //public ActionResult CompanyChangePassword(ChangePasswordViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        List<KaizenSession> ouserList = UserServices.GetUserSessions(User.Identity.Name);
        //        UserServices srve = new UserServices(ouserList[0]);
        //        User temp = srve.GetSingleFromKaizen(model.UserName);
        //        if (srve.Authenticate(model.UserName, model.CurrentPassword))
        //        {
        //            if (model.NewPassword == model.ConfirmPassword)
        //            {
        //                temp.UserPassword = model.NewPassword;
        //                if (srve.CompanyChangePassword(temp).Status)
        //                {
        //                    FormsAuthentication.SignOut();
        //                    return RedirectToAction("Login", "SysUser");
        //                }
        //                else
        //                    ModelState.AddModelError(string.Empty, "Change password failed!");
        //            }
        //            else
        //            {
        //                ModelState.AddModelError(string.Empty, "NewPassword and ConfirmPassword missmach");
        //            }
        //        }

        //        ModelState.AddModelError(string.Empty, "Change password failed!");
        //    }
        //    ChangePasswordViewModel vm = new ChangePasswordViewModel()
        //    {
        //        UserName = model.UserName,
        //        FirstName = model.FirstName,
        //        LastName = model.LastName,
        //        PhotoExtension = model.PhotoExtension,
        //    };
        //    return View(vm);
        //}
        public ActionResult Logs()
        {
            UserServices srv = new UserServices();
            User U = srv.GetSingleFromKaizen(User.Identity.Name);
            if (U == null)
            {
                FormsAuthentication.SignOut();
                return RedirectToAction("Login", "SysUser");
            }
            return View(U);
        }
        public ActionResult GetLogListGridWithCompany([DataSourceRequest]DataSourceRequest request, string CompanyID)
        {
            UserServices srv = new UserServices();
            User U = srv.GetSingleFromKaizen(User.Identity.Name);
            KaizenSession KaizenUser = new KaizenSession() { UserName = U.UserName, CompanyID = CompanyID };
            if (KaizenUser == null) return RedirectToAction("Login", "SysUser");
            KaizenSessionServices serv = new KaizenSessionServices();
            DataCollection<KaizenSession> L = new DataCollection<KaizenSession>();
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
                SortMember = "CompanyID";
                IsAscending = true;
            }
            string SQLQuery = string.Empty;
            if (request.Filters.Count > 0)
                SQLQuery = Help.ApplyFilter(request.Filters[0]);

            L = serv.GetListWithPagingFromKaizen(SQLQuery, U.UserName, CompanyID
                   , request.PageSize, request.Page, SortMember, IsAscending);

            DataSourceResult result = UserLogList(request, L);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult UserProfile()
        {
            Kaizen.BusinessLogic.ExceptionUtility.LogData("UserProfile");
            UserServices srv = new UserServices();
            User U = srv.GetSingleFromKaizen(User.Identity.Name);
            Kaizen.BusinessLogic.ExceptionUtility.LogData(U.UserName);
            if (U == null)
            {
                FormsAuthentication.SignOut();
                return RedirectToAction("Login", "SysUser");
            }
            var vm = new ProfileViewModel()
            {
                FirstName = U.FirstName,
                LastName = U.LastName,
                ImageUrl = "/Photo/UserLogin/" + U.UserName.Trim() + "." + U.PhotoExtension.Trim(),
                UserName = U.UserName
            };
            return View(vm);
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult UserProfile(ProfileViewModel model)
        {
            UserServices srv = new UserServices();
            User current = new User();
            current.UserName = User.Identity.Name;
            current.FirstName = model.FirstName;
            current.LastName = model.LastName;
            if (srv.UpdateNameFromKaizen(current).Status)
            {
                current = srv.GetSingleFromKaizen(User.Identity.Name);
                if (!string.IsNullOrEmpty(current.PhotoExtension))
                    current.PhotoExtension = current.PhotoExtension.Trim();
            }
            var vm = new ProfileViewModel()
            {
                FirstName = current.FirstName,
                LastName = current.LastName,
                ImageUrl = "/Photo/UserLogin/" + current.UserName.Trim() + "." + current.PhotoExtension,
                UserName = current.UserName
            };
            KaizenResult result = new KaizenResult();
            result.Status = true;
            result.Massage = "Save OK";
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        #endregion

        public ActionResult MyCompanies(string KaizenPublicKey)
        {
            KaizenSession ouserList = UserServices.GetFreeKaizenSession(User.Identity.Name);
            CompanyacessServices service = new CompanyacessServices();
            IList<Company> L = service.GetMyCompanies(User.Identity.Name);
            return Json(L, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult SaveUserImageTemp(IEnumerable<HttpPostedFileBase> userImageattachments)
        {
            var fileName = "";
            foreach (var file in userImageattachments)
            {
                fileName = Path.GetFileName(file.FileName);
                var destinationPath = Path.Combine(Server.MapPath("~/Photo/UserLoginTemp"), fileName);
                file.SaveAs(destinationPath);
            }
            return Json(fileName, JsonRequestBehavior.AllowGet);
        }
        public ActionResult RemoveUserImagetemp(string[] fileNames)
        {
            foreach (var fullName in fileNames)
            {
                var fileName = Path.GetFileName(fullName);
                var physicalPath = Path.Combine(Server.MapPath("~/Photo/UserLoginTemp"), fileName);
                if (System.IO.File.Exists(physicalPath))
                {
                    System.IO.File.Delete(physicalPath);
                }
            }
            return Content("");
        }


        public ActionResult SaveData(string KaizenPublicKey, NewUserViewModel User)
        {
            if (User.NewPassword != User.ConfirmPassword)
            {
                KaizenResult re = new KaizenResult();
                re.Status = false;
                re.Massage = "Password is mismaching";
                return Json(re, JsonRequestBehavior.AllowGet);
            }
            if (!ModelState.IsValid)
            {
                KaizenResult re = new KaizenResult();
                re.Status = false;
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                foreach (ModelError dd in errors)
                {
                    re.Massage += dd.ErrorMessage;
                }
                return Json(re, JsonRequestBehavior.AllowGet);
            }
            KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("Login", "SysUser");
            UserServices srv = new UserServices(KaizenUser);
            if (!string.IsNullOrEmpty(User.PhotoExtension))
            {
                string PhotoPath = @"\\Photo\\UserLoginTemp\\" + User.PhotoExtension.Trim();

                int startindex = User.PhotoExtension.LastIndexOf('.');
                startindex += 1;
                User.PhotoExtension = User.PhotoExtension.Substring(startindex, User.PhotoExtension.Length - startindex);
                if (System.IO.File.Exists(Server.MapPath(PhotoPath)))
                {
                    string Destination = Server.MapPath(@"\\Photo\\UserLogin\\" + User.UserName.Trim() + "." + User.PhotoExtension);
                    if (System.IO.File.Exists(Destination))
                    {
                        System.IO.File.Delete(Destination);
                    }
                    System.IO.File.Move(Server.MapPath(PhotoPath), Destination);
                }
            }
            User u = new User()
            {
                UserName = User.UserName,
                UserPassword = User.NewPassword,
                FirstName = User.FirstName,
                LastName = User.LastName,
                IsDisabled = false,
                PhotoExtension = User.PhotoExtension,
                IsCustomer = User.IsCustomer,
                IsPasswordchange = User.IsPasswordchange,
                IsVendor = User.IsVendor,
                IsDebtor = User.IsDebtor,
                IsPartner = User.IsPartner,
                IsAgent = User.IsAgent,
                IsEmployee = User.IsEmployee,
                IsClient = User.IsClient,
                IsChangePassRequired = User.IsChangePassRequired,
            };
            return Json(srv.AddUser(u), JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateData(NewUserViewModel User, Boolean PhotoChanged, string KaizenPublicKey)
        {
            KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("Login", "SysUser");
            UserServices service = new UserServices(KaizenUser);
            int startindex;
            if (PhotoChanged)
            {
                if (!string.IsNullOrEmpty(User.PhotoExtension))
                {
                    string PhotoPath = @"\\Photo\\UserLoginTemp\\" + User.PhotoExtension.Trim();
                    startindex = User.PhotoExtension.LastIndexOf('.');
                    startindex += 1;
                    User.PhotoExtension = User.PhotoExtension.Substring(startindex, User.PhotoExtension.Length - startindex);
                    if (System.IO.File.Exists(Server.MapPath(PhotoPath)))
                    {
                        string Destination = Server.MapPath(@"\\Photo\\UserLogin\\" + User.UserName.Trim() + "." + User.PhotoExtension);
                        if (System.IO.File.Exists(Destination))
                        {
                            System.IO.File.Delete(Destination);
                        }
                        System.IO.File.Move(Server.MapPath(PhotoPath), Destination);
                    }
                }
            }
            User u = new User()
            {
                UserName = User.UserName,
                UserPassword = User.NewPassword,
                FirstName = User.FirstName,
                LastName = User.LastName,
                IsDisabled = false,
                PhotoExtension = User.PhotoExtension,
                IsCustomer = User.IsCustomer,
                IsPasswordchange = User.IsPasswordchange,
                IsVendor = User.IsVendor,
                IsDebtor = User.IsDebtor,
                IsPartner = User.IsPartner,
                IsAgent = User.IsAgent,
                IsEmployee = User.IsEmployee,
                IsClient = User.IsClient,
                IsChangePassRequired = User.IsChangePassRequired,
            };
            return Json(service.Update(u), JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteData(string KaizenPublicKey, User User)
        {
            KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("Login", "SysUser");
            UserServices srv = new UserServices(KaizenUser);
            if (!string.IsNullOrEmpty(User.PhotoExtension))
            {
                string PhotoPath = @"\\Photo\\UserLogin\\" + User.UserName.Trim() + "." + User.PhotoExtension;
                if (System.IO.File.Exists(Server.MapPath(PhotoPath)))
                {
                    System.IO.File.Delete(Server.MapPath(PhotoPath));
                }
            }
            return Json(srv.Delete(User.UserName), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAllUsers(string KaizenPublicKey)
        {
            KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("Login", "SysUser");
            UserServices srv = new UserServices(KaizenUser);
            IList<User> List = srv.GetAllUsers();
            JsonResult result = Json(List);
            result.MaxJsonLength = int.MaxValue;
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return result;
        }

        public ActionResult UpdateProfile(User User, Boolean PhotoChanged, string KaizenPublicKey)
        {
            KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("Login", "SysUser");
            UserServices service = new UserServices(KaizenUser);
            int startindex;
            if (PhotoChanged)
            {
                if (!string.IsNullOrEmpty(User.PhotoExtension))
                {
                    string PhotoPath = @"\\Photo\\UserLoginTemp\\" + User.PhotoExtension.Trim();
                    startindex = User.PhotoExtension.LastIndexOf('.');
                    startindex += 1;
                    User.PhotoExtension = User.PhotoExtension.Substring(startindex, User.PhotoExtension.Length - startindex);
                    if (System.IO.File.Exists(Server.MapPath(PhotoPath)))
                    {
                        string Destination = Server.MapPath(@"\\Photo\\UserLogin\\" + User.UserName.Trim() + "." + User.PhotoExtension);
                        if (System.IO.File.Exists(Destination))
                        {
                            System.IO.File.Delete(Destination);
                        }
                        System.IO.File.Move(Server.MapPath(PhotoPath), Destination);
                    }
                }
            }
            User current = service.GetSingle(User.UserName);
            User.UserPassword = current.UserPassword;
            return Json(service.UpdateProfile(User), JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdatePassword(string UserName, string OldPassword, string NewPassword, string KaizenPublicKey)
        {
            KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("Login", "SysUser");
            UserServices service = new UserServices(KaizenUser);
            User current = service.GetSingle(UserName);
            if (service.Verify(OldPassword, current.UserPassword))
            {
                current.UserPassword = NewPassword;
                return Json(service.Update(current), JsonRequestBehavior.AllowGet);
            }
            KaizenResult res = new KaizenResult() { Status = false, Massage = "Please Verify Old Password !" };
            return Json(res, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetMy(string KaizenPublicKey)
        {
            KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("Login", "SysUser");
            KaizenJson KaizenJsonresult = new KaizenJson();
            if (KaizenUser == null)
            {
                KaizenJsonresult.Status = false;
                KaizenJsonresult.Massage = "Fail";
                return Json(KaizenJsonresult, JsonRequestBehavior.AllowGet);
            }
            if (string.IsNullOrEmpty(KaizenUser.SegmentMark))
            {
                KaizenJsonresult.Status = false;
                KaizenJsonresult.Massage = "Fail";
                return Json(KaizenJsonresult, JsonRequestBehavior.AllowGet);
            }
            return Json(KaizenUser, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetSingle(string KaizenPublicKey, string UserName)
        {
            KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("Login", "SysUser");
            UserServices service = new UserServices(KaizenUser);
            return Json(service.GetSingle(UserName), JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetCurrentUserLog(string KaizenPublicKey)
        {
            KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("Login", "SysUser");
            UserServices service = new UserServices(KaizenUser);
            return Json(service.GetCurrentUserLog(), JsonRequestBehavior.AllowGet);
        }

        #region ChangeUserPassword
        public ActionResult SaveUserPassword(string KaizenPublicKey, string UserName, string NewPassword, string ConfirmPassword)
        {
            if (NewPassword != ConfirmPassword)
            {
                KaizenResult reslt = new KaizenResult();
                reslt.Massage = "Password mis match";
                reslt.Status = false;
                return Json(reslt, JsonRequestBehavior.AllowGet);
            }
            KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            UserServices srve = new UserServices(KaizenUser);
            //ChangePasswordViewModel vm = new ChangePasswordViewModel()
            //{
            //    UserName = UserName,
            //    FirstName = temp.FirstName,
            //    LastName = temp.LastName,
            //    ConfirmPassword = ConfirmPassword,
            //    CurrentPassword = temp.UserPassword,
            //    NewPassword = NewPassword
            //};
            return Json(srve.CompanyChangePassword(UserName, NewPassword), JsonRequestBehavior.AllowGet);
        }
        public ActionResult CompanyChangePassword(ChangePasswordViewModel model)
        {
            model.CurrentPassword = model.NewPassword;
            //TryUpdateModel(model);
            //if (ModelState.IsValid)
            //{

            List<KaizenSession> ouserList = UserServices.GetUserSessions(User.Identity.Name);
            UserServices srve = new UserServices(ouserList[0]);

            if (model.NewPassword == model.ConfirmPassword)
            {
                if (srve.CompanyChangePassword(model.UserName, model.NewPassword).Status)
                {
                    FormsAuthentication.SignOut();
                    return RedirectToAction("Login", "SysUser");
                }
                else
                    ModelState.AddModelError("ConfirmPassword", "Change password failed!");
            }
            else
            {
                ModelState.AddModelError("ConfirmPassword", "NewPassword and ConfirmPassword missmach");
            }


            ModelState.AddModelError("ConfirmPassword", "Change password failed!");
            //}
            //ChangePasswordViewModel vm = new ChangePasswordViewModel()
            //{
            //    UserName = model.UserName,
            //    FirstName = model.FirstName,
            //    LastName = model.LastName,
            //    PhotoExtension = model.PhotoExtension,
            //};
            return View(model);
        }
        #endregion

        //public ActionResult PasswordEncryption(string KaizenPublicKey, string Password)
        //{
        //    KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
        //    if (KaizenUser == null) return RedirectToAction("Login", "SysUser");
        //    UserServices service = new UserServices(KaizenUser);
        //    return Json(service.PasswordEncryption(Password), JsonRequestBehavior.AllowGet);
        //}
        //public ActionResult PasswordDecryption(string KaizenPublicKey, string Password)
        //{
        //    KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
        //    if (KaizenUser == null) return RedirectToAction("Login", "SysUser");
        //    UserServices service = new UserServices(KaizenUser);
        //    return Json(service.PasswordDecryption(Password), JsonRequestBehavior.AllowGet);
        //}


        #region User Log
        public ActionResult UserLogs(string KaizenPublicKey)
        {
            KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("Login", "SysUser");
            return PartialView();
        }
        private DataSourceResult UserLogList([DataSourceRequest]DataSourceRequest request, DataCollection<KaizenSession> L)
        {
            int tempPage = request.Page;
            DataSourceResult result;
            if (L != null && L.Items != null)
            {
                request.Page = 1;
                result = L.Items.ToDataSourceResult(request,
               o => new KaizenSession
               {
                   CompanyID = o.CompanyID,
                   UserName = o.UserName,
                   KaizenPublicKey = o.KaizenPublicKey,
                   LoginDate = o.LoginDate,
                   LoginDateOut = o.LoginDateOut
               }
               );
                result.Total = L.TotalItemCount;
                request.Page = tempPage;
            }
            else
            {
                result = new DataSourceResult()
                {
                    Data = new List<KaizenSession>(),
                    Total = 0
                };
            }
            return result;
        }
        public ActionResult GetLogListGridWithUser([DataSourceRequest]DataSourceRequest request, string KaizenPublicKey
, DateTime FromDate, DateTime ToDate, string UserName, string CompanyID, string Searchcritaria)
        {
            KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("Login", "SysUser");
            KaizenSessionServices serv = new KaizenSessionServices(KaizenUser);
            DataCollection<KaizenSession> L = new DataCollection<KaizenSession>();
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
                SortMember = "CompanyID";
                IsAscending = true;
            }
            string SQLQuery = string.Empty;
            if (request.Filters.Count > 0)
                SQLQuery = Help.ApplyFilter(request.Filters[0]);
            //if(!string.IsNullOrEmpty(SQLQuery))
            //{
            //    string[] tt = SQLQuery.Split('=');
            //    tt[0] = "DATEADD(dd, 0, DATEDIFF(dd, 0, " + tt[0] + "))";
            //    SQLQuery = tt[0] + "=" + tt[1];
            //}
            if (!string.IsNullOrEmpty(UserName))
            {
                L = serv.GetAllViewBYSQLQuery(SQLQuery, FromDate, ToDate
                    , Searchcritaria, UserName, CompanyID
                  , request.PageSize, request.Page, SortMember, IsAscending);
            }

            DataSourceResult result = UserLogList(request, L);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region User Email
        public ActionResult UserEmails(string KaizenPublicKey)
        {
            KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("Login", "SysUser");
            return PartialView();
        }
        private DataSourceResult UserEmailList([DataSourceRequest]DataSourceRequest request, DataCollection<Kaizen00020> L)
        {
            int tempPage = request.Page;
            DataSourceResult result;
            if (L != null && L.Items != null)
            {

                request.Page = 1;
                result = L.Items.ToDataSourceResult(request,
               o => new Kaizen00020
               {
                   EmailID = o.EmailID,
                   EmailTitle = o.EmailTitle,
                   EmailIPassword = o.EmailIPassword,
                   UserName = o.UserName,
                   InComingPort = o.InComingPort,
                   IncomingProtocal = o.IncomingProtocal,
                   OutGoingPort = o.OutGoingPort,
                   OutGoingProtocal = o.OutGoingProtocal,
                   IsSSL = o.IsSSL
               }
               );
                result.Total = L.TotalItemCount;
                request.Page = tempPage;
            }
            else
            {
                result = new DataSourceResult()
                {
                    Data = new List<Kaizen00020>(),
                    Total = 0
                };
            }
            return result;
        }
        public ActionResult GetEmailListGridWithUser([DataSourceRequest]DataSourceRequest request, string KaizenPublicKey
            , string UserName, string Searchcritaria)
        {
            KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("Login", "SysUser");
            Kaizen00020Services serv = new Kaizen00020Services(KaizenUser);
            DataCollection<Kaizen00020> L = new DataCollection<Kaizen00020>();
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
                SortMember = "EmailID";
                IsAscending = true;
            }
            string SQLQuery = string.Empty;
            if (request.Filters.Count > 0)
                SQLQuery = Help.ApplyFilter(request.Filters[0]);

            L = serv.GetAllViewBYSQLQuery(SQLQuery, Searchcritaria, UserName
                , request.PageSize, request.Page, SortMember, IsAscending);
            DataSourceResult result = UserEmailList(request, L);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetUserEmails(string KaizenPublicKey, string UserName)
        {
            KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            Kaizen00020Services service = new Kaizen00020Services(KaizenUser);
            IList<Kaizen00020> o = service.GetEmailsByUserName(UserName);
            return Json(o, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SaveUserEmail(Kaizen00020 Kaizen00020, string KaizenPublicKey)
        {
            KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("Login", "SysUser");
            Kaizen00020Services service = new Kaizen00020Services(KaizenUser);
            return Json(service.AddKaizen00020(Kaizen00020), JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateUserEmail(Kaizen00020 Kaizen00020, string KaizenPublicKey)
        {
            KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("Login", "SysUser");
            Kaizen00020Services service = new Kaizen00020Services(KaizenUser);
            return Json(service.Update(Kaizen00020), JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteUserEmail(Kaizen00020 Kaizen00020, string KaizenPublicKey)
        {
            KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("Login", "SysUser");
            Kaizen00020Services service = new Kaizen00020Services(KaizenUser);
            return Json(service.Delete(Kaizen00020), JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Company Access
        public ActionResult CompanyAccess(string KaizenPublicKey)
        {
            KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("Login", "SysUser");
            return PartialView();
        }
        public ActionResult DeleteCompanyAccess(IList<CompanyAccess> CompanyAccess, string KaizenPublicKey)
        {
            KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("Login", "SysUser");
            CompanyAccessServices serv = new CompanyAccessServices(KaizenUser);
            return Json(serv.Delete(CompanyAccess), JsonRequestBehavior.AllowGet);
        }
        #endregion 

        #region User Defaults & Company Access
        public ActionResult UserDefaults(string KaizenPublicKey)
        {
            KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("Login", "SysUser");
            return PartialView();
        }
        private DataSourceResult UserCompanyAccessList([DataSourceRequest]DataSourceRequest request, DataCollection<CompanyAccess> L)
        {
            int tempPage = request.Page;
            DataSourceResult result;
            if (L != null && L.Items != null)
            {

                request.Page = 1;
                result = L.Items.ToDataSourceResult(request,
               o => new CompanyAccess
               {
                   CompanyID = o.CompanyID,
                   UserName = o.UserName,
                   //AgentID = o.AgentID,
                   CheckbookCode = o.CheckbookCode,
                   CurrencyCode = o.CurrencyCode,
                   CurrentDate = o.CurrentDate,
                   DecimalDigit = o.DecimalDigit,
                   ExchangeRate = o.ExchangeRate,
                   ExchangeRateID = o.ExchangeRateID,
                   ExchangeTableID = o.ExchangeTableID,
                   IsMultiply = o.IsMultiply,
                   SiteID = o.SiteID,
                   SOPTYPE = o.SOPTYPE,
                   SOPTypeSetupID = o.SOPTypeSetupID,
                   YearCode = o.YearCode
               }
               );
                result.Total = L.TotalItemCount;
                request.Page = tempPage;
            }
            else
            {
                result = new DataSourceResult()
                {
                    Data = new List<Kaizen00020>(),
                    Total = 0
                };
            }
            return result;
        }
        public ActionResult GetCompanyAccessListGridWithUser([DataSourceRequest]DataSourceRequest request, string KaizenPublicKey
, string FieldID, int FltrOperator, string UserName, string Searchcritaria)
        {
            KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("Login", "SysUser");
            CompanyAccessServices serv = new CompanyAccessServices(KaizenUser);
            DataCollection<CompanyAccess> L = new DataCollection<CompanyAccess>();
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
                SortMember = "UserName";
                IsAscending = true;
            }
            string SQLQuery = string.Empty;
            if (request.Filters.Count > 0)
                SQLQuery = Help.ApplyFilter(request.Filters[0]);

            if (!string.IsNullOrEmpty(UserName))
            {
                L = serv.GetAllViewBYSQLQuery(SQLQuery, FieldID, FltrOperator, Searchcritaria, UserName
                  , request.PageSize, request.Page, SortMember, IsAscending);
            }

            DataSourceResult result = UserCompanyAccessList(request, L);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetCompanyAccessByuser(string KaizenPublicKey, string UserName)
        {
            KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("Login", "SysUser");
            CompanyAccessServices service = new CompanyAccessServices(KaizenUser);
            return Json(service.GetByUserName(UserName), JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetSingleCompanyAccess(string KaizenPublicKey, string UserName, string CompanyID)
        {
            KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("Login", "SysUser");
            CompanyAccessServices service = new CompanyAccessServices(KaizenUser);
            return Json(service.GetSingle(CompanyID, UserName), JsonRequestBehavior.AllowGet);
        }

        public ActionResult SaveUserCompanyAccess(CompanyAccess CompanyAccess, string KaizenPublicKey)
        {
            KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("Login", "SysUser");
            CompanyAccessServices serv = new CompanyAccessServices(KaizenUser);
            return Json(serv.AddCompanyAccess(CompanyAccess), JsonRequestBehavior.AllowGet);
        }
        public ActionResult SaveCompanyAccess(IList<CompanyAccess> CompanyAccess, string KaizenPublicKey)
        {
            KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("Login", "SysUser");
            CompanyAccessServices serv = new CompanyAccessServices(KaizenUser);
            return Json(serv.AddCompanyAccess(CompanyAccess), JsonRequestBehavior.AllowGet);
        }

        public ActionResult UpdateUserCompanyAccess(CompanyAccess CompanyAccess, string KaizenPublicKey)
        {
            KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("Login", "SysUser");
            CompanyAccessServices serv = new CompanyAccessServices(KaizenUser);
            return Json(serv.Update(CompanyAccess), JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteUserCompanyAccess(CompanyAccess CompanyAccess, string KaizenPublicKey)
        {
            KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("Login", "SysUser");
            CompanyAccessServices serv = new CompanyAccessServices(KaizenUser);
            return Json(serv.Delete(CompanyAccess), JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Role Access
        public ActionResult RolesAccess(string KaizenPublicKey)
        {
            KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("Login", "SysUser");
            return PartialView();
        }
        public ActionResult SaveKaizenUserRole(IList<KaizenUserRole> KaizenUserRole, string KaizenPublicKey)
        {
            KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("Login", "SysUser");
            KaizenUserRoleServices serv = new KaizenUserRoleServices(KaizenUser);
            return Json(serv.AddKaizenUserRole(KaizenUserRole), JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteKaizenUserRole(IList<KaizenUserRole> KaizenUserRole, string KaizenPublicKey)
        {
            KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("Login", "SysUser");
            KaizenUserRoleServices serv = new KaizenUserRoleServices(KaizenUser);
            return Json(serv.Delete(KaizenUserRole), JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetRoleAccessByUser(string UserName, string KaizenPublicKey)
        {
            KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("Login", "SysUser");
            KaizenUserRoleServices srv = new KaizenUserRoleServices(KaizenUser);
            return Json(srv.GetByUserName(UserName), JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region View Access
        public ActionResult ViewsAccess(string KaizenPublicKey)
        {
            KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("Login", "SysUser");
            return PartialView();
        }
        public ActionResult SaveKaizenGridViewAccess(IList<KaizenGridViewAccess> KaizenGridViewAccess, string KaizenPublicKey)
        {
            KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("Login", "SysUser");
            KaizenGridViewAccessServices serv = new KaizenGridViewAccessServices(KaizenUser);
            return Json(serv.AddKaizenGridViewAccess(KaizenGridViewAccess), JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteKaizenGridViewAccess(IList<KaizenGridViewAccess> KaizenGridViewAccess, string KaizenPublicKey)
        {
            KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("Login", "SysUser");
            KaizenGridViewAccessServices serv = new KaizenGridViewAccessServices(KaizenUser);
            return Json(serv.Delete(KaizenGridViewAccess), JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetViewAccessByUser(string UserName, int ScreenID, string KaizenPublicKey)
        {
            KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("Login", "SysUser");
            KaizenGridViewAccessServices srv = new KaizenGridViewAccessServices(KaizenUser);
            List<KaizenGridViewAccess> list = new List<KaizenGridViewAccess>();
            var result = srv.GetUserViews(UserName, ScreenID);
            foreach (var item in result)
            {
                list.Add(new KaizenGridViewAccess()
                {
                    ViewID = item.ViewID,
                    UserName = item.UserName,
                    IsDefault = item.IsDefault
                });
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region User Audit
        public ActionResult UserAudit(string KaizenPublicKey)
        {
            KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("Login", "SysUser");
            return PartialView();
        }
        private DataSourceResult UserAuditList([DataSourceRequest]DataSourceRequest request, DataCollection<KaizenAudit> L)
        {
            int tempPage = request.Page;
            DataSourceResult result;
            if (L != null && L.Items != null)
            {

                request.Page = 1;
                result = L.Items.ToDataSourceResult(request,
               o => new KaizenAudit
               {
                   Kaizen_USER = o.Kaizen_USER,
                   Kaizen_Table = o.Kaizen_Table,
                   Kaizen_HOST = o.Kaizen_HOST,
                   Kaizen_DB = o.Kaizen_DB,
                   Kaizen_DATE = o.Kaizen_DATE,
                   Ins = o.Ins,
                   Del = o.Del
               }
               );
                result.Total = L.TotalItemCount;
                request.Page = tempPage;
            }
            else
            {
                result = new DataSourceResult()
                {
                    Data = new List<KaizenAudit>(),
                    Total = 0
                };
            }
            return result;
        }
        public ActionResult GetAuditListGridWithFilter([DataSourceRequest]DataSourceRequest request, string KaizenPublicKey,
            DateTime From, DateTime To, string Kaizen_DB, string Searchcritaria)
        {
            KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("Login", "SysUser");
            KaizenAuditServices serv = new KaizenAuditServices(KaizenUser);
            DataCollection<KaizenAudit> L = new DataCollection<KaizenAudit>();
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
                SortMember = "Kaizen_DATE";
                IsAscending = true;
            }
            string SQLQuery = string.Empty;
            if (request.Filters.Count > 0)
                SQLQuery = Help.ApplyFilter(request.Filters[0]);

            L = serv.GetWhereListWithPaging(SQLQuery, Searchcritaria, From, To, Kaizen_DB
                 , request.PageSize, request.Page, SortMember, IsAscending);
            DataSourceResult result = UserAuditList(request, L);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteAuditData(string KaizenPublicKey, IList<KaizenAudit> KaizenAudit)
        {
            KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("Login", "SysUser");
            KaizenAuditServices serv = new KaizenAuditServices(KaizenUser);
            if (KaizenAudit != null)
                return Json(serv.Delete(KaizenAudit), JsonRequestBehavior.AllowGet);
            else
                return Json(new KaizenResult() { Status = false, Massage = "No data to delete !!" }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        public ActionResult GetOnlineUsers(string KaizenPublicKey)
        {
            KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            if (KaizenUser == null) return RedirectToAction("Login", "SysUser");
            List<KaizenSession> L = UserServices.GetSessions(KaizenUser.CompanyID);
            L = L.FindAll(ss => ss.KaizenPublicKey != KaizenUser.KaizenPublicKey && ss.ConnectionIds.Count > 0);
            List<User> result = new List<User>();
            foreach (KaizenSession obj in L)
            {
                result.Add(new User()
                {
                    UserName = obj.UserName
                    ,
                    PhotoExtension = obj.PhotoExtension
                    ,
                    FirstName = obj.FirstName
                    ,
                    LastName = obj.LastName
                    ,
                    LastLogin = obj.LoginDate
                    //,ConnectionId = obj.ConnectionId 
                });
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}