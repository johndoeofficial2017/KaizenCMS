app.controller('SysUserController', function ($scope, $http) {
    $scope.SysUserMaintoolbarOptions = {
        items: [
               {
                   type: "button", text: "<span class=\"fa fa-plus-circle\"></span> New User",
                   attributes: { "class": "k-primary" },
                   click: function (e) {
                       $scope.$apply(function () {
                           $scope.LoadUserPage('Create');
                           $scope.User.Status = 1;
                           $scope.Clear();
                       });
                   }
               },
               {
                   type: "button", text: "Email Setup",
                   click: function (e) {
                       $scope.$apply(function () {
                           $scope.LoadUserPage('UserEmails');
                           $scope.Clear();
                           $scope.Kaizen00020.Status = 1;
                       });
                   }
               },
               {
                   type: "button", text: "User Log",
                   click: function (e) {
                       $scope.$apply(function () {
                           $scope.LoadUserPage('UserLogs');
                           $scope.Clear();
                           $http.get('/Sys_Company/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
                               $scope.Companies = data;
                           }).finally(function () { });
                       });
                   }
               },
               {
                   type: "button", text: "User Audit",
                   click: function (e) {
                       $scope.$apply(function () {
                           $scope.LoadUserPage('UserAudit');
                           $scope.Clear();
                           $http.get('/Sys_Company/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
                               $scope.Companies = data;
                           }).finally(function () {
                               //$scope.Audit.Kaizen_DB = $scope.MY.CompanyID;
                               $scope.Audit.From = new Date();
                               $scope.Audit.To = new Date();
                               $scope.Audit.Kaizen_DB = $scope.Companies[0];
                           });
                       });
                   }
               },
               {
                   type: "button", text: "User Defaults",
                   click: function (e) {
                       $scope.UserDefaults();
                   }
               },
               {
                   type: "button", text: "Change Password",
                   click: function (e) {
                       $scope.$apply(function () {
                           $scope.LoadUserPage('ChangeUserPassword');
                       });
                   }
               },
               {
                   type: "splitButton",
                   text: "Security",
                   click: function (e) {
                       //$scope.OpenSchedule();
                   },
                   menuButtons: [
                       {
                           text: "Companies",
                           click: function (e) {

                               $scope.LoadUserPage('CompanyAccess');
                               if ($scope.Companies == null || $scope.Companies.length == 0) {
                                   $http.get('/Sys_Company/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
                                       $scope.Companies = data;
                                   }).finally(function () { });
                               } else
                                   $scope.$apply();
                               $scope.Clear();
                           }
                       },
                       {
                           text: "Roles",
                           click: function (e) {
                               $scope.LoadUserPage('RolesAccess');
                               if ($scope.Roles == null || $scope.Roles.length == 0) {
                                   $http.get('/Sys_role/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
                                       $scope.Roles = data;
                                   }).finally(function () { });
                               } else
                                   $scope.$apply();
                               $scope.Clear();
                           }
                       },
                       {
                           text: "Views",
                           click: function (e) {
                               $scope.LoadUserPage('ViewsAccess');
                               $http.get('/Sys_Company/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
                                   $scope.Companies = data;
                               }).finally(function () { });
                               $http.get('/Admin_Screen/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
                                   $scope.Screens = data;
                               }).finally(function () { });
                               $scope.Clear();
                           }
                       }
                   ]
               },
            {
                type: "splitButton",
                spriteCssClass: "k-tool-icon k-i-excel",
                text: "Export",
                click: function (e) {
                    $scope.SaveAsExcel();
                },
                menuButtons: [
                    { spriteCssClass: "k-tool-icon k-i-excel", text: "Excel", click: function (e) { $scope.SaveAsExcel(); } },
                    { spriteCssClass: "k-tool-icon k-i-pdf", text: "PDF", click: function (e) { $scope.SaveAsPdf(); } }
                ]
            }
        ]
       , resizable: true
    };
    $scope.User = {};
    $scope.UserPages = [];
    $scope.PhotoChanged = false;
    $scope.Clear = function () {
        $scope.User = {};
        $scope.Kaizen00020 = {};
        //$scope.CompanyAccess = {};
        $scope.KaizenGridViewAccess = {};
        $scope.Encryption = {};
        $scope.Audit = {};
        $scope.link = '/Photo/UserLogin/EmpCard.jpg';
        $scope.User.Status = 1;

        $scope.UserRoles = [];
        $scope.NewUserRoles = [];
        if ($scope.Roles != null || (angular.isDefined($scope.Roles) && $scope.Roles.length > 0)) {
            for (var i = 0; i < $scope.Roles.length; i++) {
                var item = $scope.Roles[i];
                item.isSelected = false;
            }
        }
        $scope.UserCompanies = [];
        $scope.NewUserCompanies = [];
        if ($scope.Companies != null || (angular.isDefined($scope.Companies) && $scope.Companies.length > 0)) {
            for (var i = 0; i < $scope.Companies.length; i++) {
                var item = $scope.Companies[i];
                item.isSelected = false;
            }
        }
        $scope.UserViews = [];
        $scope.NewUserViews = [];
        if ($scope.Views != null || (angular.isDefined($scope.Views) && $scope.Views.length > 0)) {
            for (var i = 0; i < $scope.Views.length; i++) {
                var item = $scope.Views[i];
                item.isSelected = false;
            }
        }
    };
    $scope.LoadUser = function (UserName) {
        if (angular.isUndefined($scope.User.UserName) || $scope.User.UserName != UserName) {
            $http.get('/SysUser/GetSingle?KaizenPublicKey=' + sessionStorage.PublicKey + "&UserName=" + UserName).success(function (data) {
                $scope.User = data;
            }).finally(function () {
                $scope.User.Status = 2;
                if ($scope.User.PhotoExtension == null)
                    $scope.link = '/Photo/UserLogin/EmpCard.jpg';
                else
                    $scope.link = '/Photo/UserLogin/' + kaizenTrim($scope.User.UserName) + "." + kaizenTrim($scope.User.PhotoExtension) + "?c=" + Math.random();
            });
        }
    };
    $scope.LoadUserPage = function (ActionName) {
        removeEntityPage($scope.UserPages);
        var URIPath = "/" + $scope.ToolBar.ActiveScreen.ID + "/" + ActionName + "?KaizenPublicKey=" + sessionStorage.PublicKey;
        var Page = { url: URIPath, ActionName: ActionName };
        $scope.UserPages.push(Page);

    };
    $scope.EditUser = function (UserName) {
        $scope.LoadUserPage('CreateEdit')
        $scope.LoadUser(UserName);
    };
    $scope.GetMyProfile = function () {
        $http.get('/SysUser/GetMy?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.User = data;
            $scope.link = '/Photo/UserLogin/' + kaizenTrim($scope.User.UserName) + '.' + kaizenTrim($scope.User.PhotoExtension);
        }).finally(function () {
            $scope.User.Status = 2;
            $http.get('/Sys_Company/GetUserCompanies?KaizenPublicKey=' + sessionStorage.PublicKey, { params: { UserName: $scope.User.UserName } }).success(function (data) {
                $scope.Companies = data;
            }).finally(function () { });
            $http.get('/SOP_Invoice/FillSOPTypeDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
                $scope.SOPTypes = data;
            }).finally(function () { });
            $http.get('/GL_Set_FiscalYears/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
                $scope.Years = data;
                $scope.Years.forEach(function (element, index) {
                    var parsedstart = new Date(parseInt(element.PeriodFrom.toString().substr(6)));
                    element.PeriodFrom = new Date(parsedstart);

                    var parsedend = new Date(parseInt(element.PeriodTo.toString().substr(6)));
                    element.PeriodTo = new Date(parsedend);
                })
            }).finally(function () { });
            $http.get('/SysUser/GetCurrentUserLog?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
                $scope.UserLogs = data;
                $scope.UserLogs.forEach(function (element, index) {
                    if (element.LoginDate != null) {
                        var parsedlogin = new Date(parseInt(element.LoginDate.toString().substr(6)));
                        element.LoginDate = new Date(parsedlogin);
                    }
                    if (element.LoginDateOut != null) {
                        var parsedlogout = new Date(parseInt(element.LoginDateOut.toString().substr(6)));
                        element.LoginDateOut = new Date(parsedlogout);
                    }
                })
            }).finally(function () { });
        });
    };
    $scope.ChangeUserPassword = function (UserName) {
        $scope.LoadUserPage('ChangeUserPassword');
        $scope.Clear();
        $scope.LoadUser(UserName);
    };
    $scope.UpdatePassword = function () {
        $http.post('/SysUser/SaveUserPassword',
            {
                'UserName': $scope.User.UserName, 'NewPassword': $scope.User.NewPassword
                , 'ConfirmPassword': $scope.User.ConfirmPassword
                , 'KaizenPublicKey': sessionStorage.PublicKey
            }).success(function (data) {
                if (data.Status == true) {
                    $.bigBox({
                        title: data.Massage,
                        content: data.Description,
                        color: "#739E73",
                        timeout: 4000,
                        icon: "fa fa-check shake animated"
                    });
                    $scope.Cancel();
                    $scope.GridRefresh();
                }
                else {
                    $.bigBox({
                        title: data.Massage,
                        content: data.Description,
                        color: "#C46A69",
                        timeout: 4000,
                        icon: "fa fa-bolt swing animated"
                    });
                }
            }).error(function (data, status, headers, config) {
                $.bigBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#C46A69",
                    timeout: 4000,
                    icon: "fa fa-bolt swing animated"
                });
            });
    };
    $scope.SaveData22 = function () {
        alert('sssssssssssssssss');
    }
    $scope.notf1Options = {
        templates: [{
            type: "ngTemplate",
            template: $("#notificationTemplate").html()
        }]
    };

    //$scope.ngValue = "Angular scope value";
    //$scope.showPopup = function () {
    //    var date = new Date();
    //    //date = kendo.toString(date, "HH:MM:ss.") + kendo.toString(date.getMilliseconds(), "000");

    //    $scope.notf1.show({
    //        kValue: date
    //    }, "ngTemplate");
    //}

    $scope.SaveData = function () {
        //$scope.GridRefresh();
        $http.post('/SysUser/SaveData', { 'User': $scope.User, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
            if (data.Status == true) {
                $.bigBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#739E73",
                    timeout: 4000,
                    icon: "fa fa-check shake animated"
                });
                $scope.Cancel();
                $scope.GridRefresh('GridSysUser');
            }
            else {
                $.bigBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#C46A69",
                    timeout: 4000,
                    icon: "fa fa-bolt swing animated"
                });
            }
        }).error(function (data, status, headers, config) {
            alert('error')
        });
    };
    $scope.UpdateData = function () {
        //alert('UpdateData');
        $http.post('/SysUser/UpdateData', { 'User': $scope.User, PhotoChanged: $scope.PhotoChanged, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
            if (data.Status == true) {
                //$scope.showPopup();

                $.bigBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#739E73",
                    timeout: 4000,
                    icon: "fa fa-check shake animated"
                });
                $scope.Cancel();
                //$scope.notf1.hide();
                $scope.GridRefresh('GridSysUser');
            }
            else {
                //$.bigBox({
                //    title: data.Massage,
                //    content: data.Description,
                //    color: "#C46A69",
                //    timeout: 4000,
                //    icon: "fa fa-bolt swing animated"
                //});
            }
        }).error(function (data, status, headers, config) {
            //$.bigBox({
            //    title: data.Massage,
            //    content: data.Description,
            //    color: "#C46A69",
            //    timeout: 4000,
            //    icon: "fa fa-bolt swing animated"
            //});
        });
    };
    $scope.UpdateProfile = function () {
        $http.post('/SysUser/UpdateProfile', { 'User': $scope.User, PhotoChanged: $scope.PhotoChanged, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
            if (data.Status == true) {
                $.bigBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#739E73",
                    timeout: 4000,
                    icon: "fa fa-check shake animated"
                });
            }
            else {
                $.bigBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#C46A69",
                    timeout: 4000,
                    icon: "fa fa-bolt swing animated"
                });
            }
        }).error(function (data, status, headers, config) {
            $.bigBox({
                title: data.Massage,
                content: data.Description,
                color: "#C46A69",
                timeout: 4000,
                icon: "fa fa-bolt swing animated"
            });
        });
    };

    $scope.ChangePasswordClick = function () {
        $http.post('/SysUser/UpdatePassword', { 'UserName': $scope.User.UserName, 'OldPassword': $scope.User.UserPassword, 'NewPassword': $scope.User.NewPassword, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
            if (data.Status == true) {
                $.bigBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#739E73",
                    timeout: 4000,
                    icon: "fa fa-check shake animated"
                });
            }
            else {
                $.bigBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#C46A69",
                    timeout: 4000,
                    icon: "fa fa-bolt swing animated"
                });
            }
        }).error(function (data, status, headers, config) {
            $.bigBox({
                title: data.Massage,
                content: data.Description,
                color: "#C46A69",
                timeout: 4000,
                icon: "fa fa-bolt swing animated"
            });
        });
    };
    $scope.UpdateProfileDefaults = function () {
        $scope.CompanyAccess.UserName = $scope.User.UserName;
        $http.post('/Sys_CompanyAccess/UpdateCompanyAccess?KaizenPublicKey=' + sessionStorage.PublicKey, { 'CompanyAccess': $scope.CompanyAccess }).success(function (data) {
            if (data.Status == true) {
                $.bigBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#739E73",
                    timeout: 4000,
                    icon: "fa fa-check shake animated"
                });
            }
            else {
                $.bigBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#C46A69",
                    timeout: 4000,
                    icon: "fa fa-bolt swing animated"
                });
            }
        }).error(function (data, status, headers, config) {
            $.bigBox({
                title: data.Massage,
                content: data.Description,
                color: "#C46A69",
                timeout: 4000,
                icon: "fa fa-bolt swing animated"
            });
        });
    };
    $scope.ProfileCompanyChanged = function () {
        if (angular.isDefined($scope.User.UserName)) {
            $http.get('/Sys_CompanyAccess/GetCompanyAccess?KaizenPublicKey=' + sessionStorage.PublicKey, { params: { UserName: $scope.User.UserName, CompanyID: $scope.CompanyAccess.CompanyID } }).success(function (data) {
                $scope.CompanyAccess = data;
            }).finally(function () { });
        }
    };

    $scope.Print = function () {
        alert('Printer not configured');
    };
    $scope.Delete = function () {
        $.SmartMessageBox({
            title: "Delete Record",
            content: "Are you sure you want to delete this?",
            buttons: '[No][Yes]'
        }, function (ButtonPressed) {
            if (ButtonPressed === "Yes") {
                $http.post('/SysUser/DeleteData', { 'User': $scope.User, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
                    if (data.Status == true) {
                        $.bigBox({
                            title: data.Massage,
                            content: data.Description,
                            color: "#739E73",
                            timeout: 4000,
                            icon: "fa fa-check shake animated"
                        });
                        $scope.Cancel();
                        $scope.GridRefresh();
                    }
                    else {
                        $.bigBox({
                            title: data.Massage,
                            content: data.Description,
                            color: "#C46A69",
                            timeout: 4000,
                            icon: "fa fa-bolt swing animated"
                        });
                    }
                }).error(function (data, status, headers, config) {
                    $.bigBox({
                        title: data.Massage,
                        content: data.Description,
                        color: "#C46A69",
                        timeout: 4000,
                        icon: "fa fa-bolt swing animated"
                    });
                });
            }
            if (ButtonPressed === "No") {
                $.smallBox({
                    title: "No Changes have been made!!",
                    content: "<i class='fa fa-clock-o'></i> <i>You pressed No...</i>",
                    color: "#3276B1",
                    iconSmall: "fa fa-times fa-2x fadeInRight animated",
                    timeout: 4000
                });
            }
        });
    };
    $scope.Cancel = function () {
        //alert('Cancel');
        $scope.UserPages = [];
        $scope.Views = [];
        $scope.KaizenGridViewAccess = {};
        $scope.User = {};
    };
    $scope.SetUserPhotoExtension = function (PhotoPath) {
        $scope.User.PhotoExtension = PhotoPath;
        $scope.link = '/Photo/UserLoginTemp/' + kaizenTrim(PhotoPath);
        $scope.PhotoChanged = true;
    };
    $scope.RemoveUserPhotoExtension = function (PhotoPath) {
        $scope.User.PhotoExtension = "";
    };
    $scope.UserBack = function (user) {
        if (user != null) {
            switch ($scope.CurrentControl) {
                case 'EmailSetup':
                    $scope.User = user;
                    $scope.Kaizen00020.UserName = user.UserName;
                    $scope.GridRefresh('GridSysUserByEmail');
                    break;
                case 'UserLog':
                    $scope.User.UserName = user.UserName;
                    $scope.GridRefresh('GridSysUserLog');
                    break;
                case 'UserDefaults':
                    $scope.User = user;
                    $scope.CompanyAccess.UserName = user.UserName;
                    $http.get('/Sys_Company/GetUserCompanies?KaizenPublicKey=' + sessionStorage.PublicKey, { params: { UserName: $scope.CompanyAccess.UserName } }).success(function (data) {
                        $scope.Companies = data;
                    }).finally(function () {
                        $("#Companies").data("kendoDropDownList").dataSource.read();
                    });
                    break;
                case 'CompanyAccess':
                    $scope.User = user;
                    $http.get('/SysUser/GetCompanyAccessByuser?KaizenPublicKey=' + sessionStorage.PublicKey, { params: { UserName: $scope.User.UserName } }).success(function (data) {
                        $scope.UserCompanies = data;
                    }).finally(function () {
                        for (var i = 0; i < $scope.Companies.length; i++) {
                            var item = $scope.Companies[i];
                            item.isSelected = false;
                            for (var j = 0; j < $scope.UserCompanies.length; j++) {
                                var obj = $scope.UserCompanies[j];
                                if (obj.CompanyID === item.CompanyID) {
                                    item.isSelected = true;
                                    break;
                                }
                            }
                        }
                    });
                    break;
                case 'RoleAccess':
                    $scope.User = user;
                    $http.get('/SysUser/GetRoleAccessByUser?KaizenPublicKey=' + sessionStorage.PublicKey, { params: { UserName: $scope.User.UserName } }).success(function (data) {
                        $scope.UserRoles = data;
                    }).finally(function () {
                        for (var i = 0; i < $scope.Roles.length; i++) {
                            var item = $scope.Roles[i];
                            item.isSelected = false;
                            for (var j = 0; j < $scope.UserRoles.length; j++) {
                                var obj = $scope.UserRoles[j];
                                if (obj.RoleID === item.RoleID) {
                                    item.isSelected = true;
                                    break;
                                }
                            }
                        }
                    });
                    break;
                case 'ViewAccess':
                    $scope.User = user;
                    $scope.KaizenGridViewAccess.UserName = user.UserName;
                    if ($scope.KaizenGridViewAccess.ScreenID != "" && $scope.KaizenGridViewAccess.ScreenID != undefined) {
                        $scope.ScreenChanged();
                    }
                    break;
                case 'StatusUsers':
                    $scope.User = user;
                    break;
            }
        }
    };
    $scope.LogCompanyChanged = function () {
        if (angular.isDefined($scope.User.UserName)) {
            $scope.GridRefresh('GridSysUserLog');
        }
    };

    //-------------------------- User Emails
    $scope.Kaizen00020 = {};
    $scope.SaveKaizen00020 = function () {
        //$scope.Kaizen00020.UserName = $scope.User.UserName;

        $http.post('/SysUser/SaveUserEmail', {
            'Kaizen00020': $scope.Kaizen00020, 'KaizenPublicKey': sessionStorage.PublicKey
        }).success(function (data) {
            if (data.Status == true) {
                $.bigBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#739E73",
                    timeout: 4000,
                    icon: "fa fa-check shake animated"
                });
                $scope.GridRefresh('GridSysUserByEmail');
                $scope.Kaizen00020 =
                {
                    Status: 1
                    , UserName: $scope.Kaizen00020.UserName
                };
            }
            else {
                $.bigBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#C46A69",
                    timeout: 4000,
                    icon: "fa fa-bolt swing animated"
                });
            }
        }).error(function (data, status, headers, config) {
            $.bigBox({
                title: data.Massage,
                content: data.Description,
                color: "#C46A69",
                timeout: 4000,
                icon: "fa fa-bolt swing animated"
            });
        });
    };
    $scope.UpdateKaizen00020 = function () {
        //alert($scope.Kaizen00020.UserName)
        //$scope.Kaizen00020.UserName = $scope.User.UserName;
        $http.post('/SysUser/UpdateUserEmail',
            {
                'Kaizen00020': $scope.Kaizen00020,
                'KaizenPublicKey': sessionStorage.PublicKey
            }).success(function (data) {
                if (data.Status == true) {
                    $.bigBox({
                        title: data.Massage,
                        content: data.Description,
                        color: "#739E73",
                        timeout: 4000,
                        icon: "fa fa-check shake animated"
                    });
                    $scope.GridRefresh('GridSysUserByEmail');
                    $scope.Kaizen00020 =
                        {
                            Status: 1
                            , UserName: $scope.Kaizen00020.UserName
                        };
                }
                else {
                    $.bigBox({
                        title: data.Massage,
                        content: data.Description,
                        color: "#C46A69",
                        timeout: 4000,
                        icon: "fa fa-bolt swing animated"
                    });
                }
            }).error(function (data, status, headers, config) {
                $.bigBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#C46A69",
                    timeout: 4000,
                    icon: "fa fa-bolt swing animated"
                });
            });
    };
    $scope.DeleteKaizen00020 = function () {
        $.SmartMessageBox({
            title: "Delete Record",
            content: "Are you sure you want to delete this?",
            buttons: '[No][Yes]'
        }, function (ButtonPressed) {
            if (ButtonPressed === "Yes") {
                $http.post('/SysUser/DeleteUserEmail', { 'Kaizen00020': $scope.Kaizen00020, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
                    if (data.Status == true) {
                        $.bigBox({
                            title: data.Massage,
                            content: data.Description,
                            color: "#739E73",
                            timeout: 4000,
                            icon: "fa fa-check shake animated"
                        });
                        $scope.GridRefresh('GridSysUserByEmail');
                        $scope.Kaizen00020 = { Status: 1 };
                    }
                    else {
                        $.bigBox({
                            title: data.Massage,
                            content: data.Description,
                            color: "#C46A69",
                            timeout: 4000,
                            icon: "fa fa-bolt swing animated"
                        });
                    }
                }).error(function (data, status, headers, config) {
                    $.bigBox({
                        title: data.Massage,
                        content: data.Description,
                        color: "#C46A69",
                        timeout: 4000,
                        icon: "fa fa-bolt swing animated"
                    });
                });
            }
            if (ButtonPressed === "No") {
                $.smallBox({
                    title: "No Changes have been made!!",
                    content: "<i class='fa fa-clock-o'></i> <i>You pressed No...</i>",
                    color: "#C46A69",
                    iconSmall: "fa fa-times fa-2x fadeInRight animated",
                    timeout: 4000
                });
            }
        });
    };
    ////////////////////////////////////// USER DEFAULS
    $scope.CompanyAccess = {};
    $scope.UserCompanies = [];
    $scope.NewUserCompanies = [];
    $scope.UserDefaults = function () {
        $scope.LoadUserPage('UserDefaults')
        $scope.Clear();
        $http.get('/SOP_Invoice/FillSOPTypeDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.SOPTypes = data;
        }).finally(function () { });
        $http.get('/GL_Years/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.Years = data;
            $scope.Years.forEach(function (element, index) {
                var parsedstart = new Date(parseInt(element.PeriodFrom.toString().substr(6)));
                element.PeriodFrom = new Date(parsedstart);

                var parsedend = new Date(parseInt(element.PeriodTo.toString().substr(6)));
                element.PeriodTo = new Date(parsedend);
            })
        }).finally(function () { });
    };

    $scope.CompanyChanged = function () {
        if (angular.isDefined($scope.User.UserName)) {
            $http.get('/SysUser/GetSingleCompanyAccess?KaizenPublicKey=' + sessionStorage.PublicKey, { params: { UserName: $scope.User.UserName, CompanyID: $scope.CompanyAccess.CompanyID } }).success(function (data) {
                $scope.CompanyAccess = data;
            }).finally(function () {
                if (Object.keys($scope.CompanyAccess).length > 0)
                    $scope.CompanyAccess.Status = 2;
                else
                    $scope.CompanyAccess = { Status: 1 };
            });
        }
    };
    $scope.UpdateCompanyAccess = function () {
        $scope.CompanyAccess.UserName = $scope.User.UserName;
        $http.post('/SysUser/UpdateUserCompanyAccess', { 'CompanyAccess': $scope.CompanyAccess, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
            if (data.Status == true) {
                $.bigBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#739E73",
                    timeout: 4000,
                    icon: "fa fa-check shake animated"
                });
                $scope.CompanyAccess = { Status: 1 };
            }
            else {
                $.bigBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#C46A69",
                    timeout: 4000,
                    icon: "fa fa-bolt swing animated"
                });
            }
        }).error(function (data, status, headers, config) {
            $.bigBox({
                title: data.Massage,
                content: data.Description,
                color: "#C46A69",
                timeout: 4000,
                icon: "fa fa-bolt swing animated"
            });
        });
    };
    $scope.DeleteCompanyAccess = function () {
        $.SmartMessageBox({
            title: "Delete Record",
            content: "Are you sure you want to delete this?",
            buttons: '[No][Yes]'
        }, function (ButtonPressed) {
            if (ButtonPressed === "Yes") {
                $http.post('/SysUser/DeleteUserCompanyAccess', { 'CompanyAccess': $scope.CompanyAccess, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
                    if (data.Status == true) {
                        $.bigBox({
                            title: data.Massage,
                            content: data.Description,
                            color: "#739E73",
                            timeout: 4000,
                            icon: "fa fa-check shake animated"
                        });
                        $scope.CompanyAccess = { Status: 1 };
                    }
                    else {
                        $.bigBox({
                            title: data.Massage,
                            content: data.Description,
                            color: "#C46A69",
                            timeout: 4000,
                            icon: "fa fa-bolt swing animated"
                        });
                    }
                }).error(function (data, status, headers, config) {
                    $.bigBox({
                        title: data.Massage,
                        content: data.Description,
                        color: "#C46A69",
                        timeout: 4000,
                        icon: "fa fa-bolt swing animated"
                    });
                });
            }
            if (ButtonPressed === "No") {
                $.smallBox({
                    title: "No Changes have been made!!",
                    content: "<i class='fa fa-clock-o'></i> <i>You pressed No...</i>",
                    color: "#C46A69",
                    iconSmall: "fa fa-times fa-2x fadeInRight animated",
                    timeout: 4000
                });
            }
        });
    };

    $scope.OpenExchangeRateWindow = function () {
        $scope.GL00012 = {};
        $scope.ParmObject = $scope.CompanyAccess;
        $scope.ExchangeRateWindow.center().toFront().open();
        $scope.ExchangeRateWindow.refresh({
            url: "/GL_ExchangeRate/PopUpRates?KaizenPublicKey=" + sessionStorage.PublicKey
        });
    };
    $scope.SaveExchangeRate = function () {
        $scope.GL00012.ExchangeTableID = $scope.CM00207.ExchangeTableID;
        $scope.GL00012.CurrencyCode = $scope.CM00207.CurrencyCode;
        $http.post('/GL_ExchangeRate/SaveData', { 'GL00012': $scope.GL00012, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
            if (data.Status == true) {
                $.bigBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#739E73",
                    timeout: 4000,
                    icon: "fa fa-check shake animated",
                    number: "4"
                });
                $scope.ClearExchangeRate();
                $scope.GridRefresh('GridGL_ExchangeRatePopUp');
            }
            else {
                $.bigBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#C46A69",
                    timeout: 4000,
                    icon: "fa fa-bolt swing animated",
                    number: "4"
                });
            }
        }).error(function (data, status, headers, config) {
            $.bigBox({
                title: data.Massage,
                content: data.Description,
                color: "#C46A69",
                timeout: 4000,
                icon: "fa fa-bolt shake animated",
                number: "4"
            });
        });
    };
    $scope.ClearExchangeRate = function () {
        $scope.GL00012 = {};
    };

    $scope.CurrencyBack = function (currency) {
        if (currency != null) {
            $scope.CompanyAccess.DecimalDigit = currency.DecimalDigit;
            $scope.CompanyAccess.CurrencyCode = currency.CurrencyCode;
            $scope.CompanyAccess.ExchangeTableID = currency.ExchangeTableID;
            $scope.CompanyAccess.IsMultiply = currency.IsMultiply;
            $scope.CompanyAccess.ExchangeRateID = currency.ExchangeRateID;
            $scope.CompanyAccess.ExchangeRate = currency.ExchangeRate;
            var kendoWindow = $("#MainDialog").data("kendoWindow");
            kendoWindow.close();
        }
    };
    $scope.ExchangeRateBack = function (rate) {
        if (rate != null) {
            $scope.CompanyAccess.ExchangeRateID = rate.ExchangeRateID;
            $scope.CompanyAccess.ExchangeRate = rate.ExchangeRate;
        }
    };
    $scope.ExchangeTableBack = function (table) {
        if (table != null) {
            $scope.CompanyAccess.ExchangeTableID = table.ExchangeTableID;
            $scope.CompanyAccess.IsMultiply = table.IsMultiply;
            var kendoWindow = $("#MainDialog").data("kendoWindow");
            kendoWindow.close();
        }
    };
    $scope.CheckbookBack = function (checkbook) {
        if (checkbook != null) {
            $scope.CompanyAccess.CheckbookCode = checkbook.CheckbookCode;
            $scope.CompanyAccess.CurrencyCode = checkbook.CurrencyCode;
        }
    };
    $scope.AgentBack = function (agent) {
        if (agent != null) {
            $scope.CompanyAccess.AgentID = agent.AgentID;
        }
    };
    $scope.SetupTypeBack = function (type) {
        if (type != null) {
            $scope.CompanyAccess.SOPTypeSetupID = type.SOPTypeSetupID;
        }
    };
    $scope.SiteBack = function (site) {
        if (site != null) {
            $scope.CompanyAccess.SiteID = site.SiteID;
        }
    };
    /// Company ACCESS
    $scope.SaveCompanyAccess = function () {
        var temp = angular.copy($scope.UserCompanies);
        for (var i = 0; i < $scope.Companies.length; i++) {
            var item = $scope.Companies[i];
            if (item.isSelected) {
                var IsNotNew = false;
                for (var j = 0; j < $scope.UserCompanies.length; j++) {
                    var obj = $scope.UserCompanies[j];
                    if (obj.CompanyID === item.CompanyID) {
                        IsNotNew = true;
                        break;
                    }
                    else {
                        IsNotNew = false;
                    }
                }
                if (IsNotNew) {
                    $scope.UserCompanies.splice(j, 1);
                }
                else {
                    var tmp = {
                        CompanyID: item.CompanyID, UserName: $scope.User.UserName
                    };
                    $scope.NewUserCompanies.push(tmp);
                }
            }
        }
        if ($scope.UserCompanies.length > 0) {
            $http({
                url: '/SysUser/DeleteCompanyAccess?KaizenPublicKey=' + sessionStorage.PublicKey,
                method: "POST",
                data: $.param({
                    CompanyAccess: $scope.UserCompanies
                }),
                headers: {
                    'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8'
                }
            }).success(function (data) {
                if (data.Status == true) {
                    $.bigBox({
                        title: data.Massage,
                        content: data.Description,
                        color: "#739E73",
                        timeout: 4000,
                        icon: "fa fa-check shake animated"
                    });
                }
                else {
                    $.bigBox({
                        title: data.Massage,
                        content: data.Description,
                        color: "#C46A69",
                        timeout: 4000,
                        icon: "fa fa-bolt swing animated"
                    });
                }
            }).error(function (data, status, headers, config) {
                $.bigBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#C46A69",
                    timeout: 4000,
                    icon: "fa fa-bolt swing animated"
                });
            });
        }
        if ($scope.NewUserCompanies.length > 0) {
            $http({
                url: '/SysUser/SaveCompanyAccess?KaizenPublicKey=' + sessionStorage.PublicKey,
                method: "POST",
                data: $.param({
                    CompanyAccess: $scope.NewUserCompanies
                }),
                headers: {
                    'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8'
                }
            }).success(function (data) {
                if (data.Status == true) {
                    $.bigBox({
                        title: data.Massage,
                        content: data.Description,
                        color: "#739E73",
                        timeout: 4000,
                        icon: "fa fa-check shake animated"
                    });
                    $scope.CompanyAccess = { Status: 1 };
                }
                else {
                    $.bigBox({
                        title: data.Massage,
                        content: data.Description,
                        color: "#C46A69",
                        timeout: 4000,
                        icon: "fa fa-bolt swing animated"
                    });
                }
            }).error(function (data, status, headers, config) {
                $.bigBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#C46A69",
                    timeout: 4000,
                    icon: "fa fa-bolt swing animated"
                });
            });
        }
        if ($scope.NewUserCompanies.length == 0 && $scope.UserCompanies.length == 0) {
            $.smallBox({
                title: "No Changes have been made!!",
                content: "<i class='fa fa-clock-o'></i> <i>...</i>",
                color: "#739E73",
                iconSmall: "fa fa-times fa-2x fadeInRight animated",
                timeout: 4000
            });
            $scope.UserCompanies = temp;
        } else {
            $scope.Clear();
        }
    };
    $scope.UserAccessAllCompanies = function () {
        $scope.Companies.forEach(function (element, index) {
            if (!element.isSelected) {
                element.isSelected = true;
            }
        })
    };

    /// ROLE ACCESS
    $scope.Roles = [];
    $scope.UserRoles = [];
    $scope.NewUserRoles = [];
    $scope.SaveKaizenUserRole = function () {
        var temp = angular.copy($scope.UserRoles);
        for (var i = 0; i < $scope.Roles.length; i++) {
            var item = $scope.Roles[i];
            if (item.isSelected) {
                var isdelete = false;
                for (var j = 0; j < $scope.UserRoles.length; j++) {
                    var obj = $scope.UserRoles[j];
                    if (obj.RoleID === item.RoleID) {
                        isdelete = true;
                        break;
                    }
                    else {
                        isdelete = false;
                    }
                }
                if (isdelete) {
                    $scope.UserRoles.splice(j, 1);
                }
                else {
                    var tmp = {
                        RoleID: item.RoleID, UserName: $scope.User.UserName
                    };
                    $scope.NewUserRoles.push(tmp);
                }
            }
        }

        if ($scope.UserRoles.length > 0) {
            $http({
                url: '/SysUser/DeleteKaizenUserRole?KaizenPublicKey=' + sessionStorage.PublicKey,
                method: "POST",
                data: $.param({
                    KaizenUserRole: $scope.UserRoles
                }),
                headers: {
                    'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8'
                }
            }).success(function (data) {
                if (data.Status == true) {
                    $.bigBox({
                        title: data.Massage,
                        content: data.Description,
                        color: "#739E73",
                        timeout: 4000,
                        icon: "fa fa-check shake animated"
                    });
                }
                else {
                    $.bigBox({
                        title: data.Massage,
                        content: data.Description,
                        color: "#C46A69",
                        timeout: 4000,
                        icon: "fa fa-bolt swing animated"
                    });
                }
            }).error(function (data, status, headers, config) {
                $.bigBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#C46A69",
                    timeout: 4000,
                    icon: "fa fa-bolt swing animated"
                });
            });
        }
        if ($scope.NewUserRoles.length > 0) {
            $http({
                url: '/SysUser/SaveKaizenUserRole?KaizenPublicKey=' + sessionStorage.PublicKey,
                method: "POST",
                data: $.param({
                    KaizenUserRole: $scope.NewUserRoles
                }),
                headers: {
                    'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8'
                }
            }).success(function (data) {
                if (data.Status == true) {
                    $.bigBox({
                        title: data.Massage,
                        content: data.Description,
                        color: "#739E73",
                        timeout: 4000,
                        icon: "fa fa-check shake animated"
                    });
                    $scope.CompanyAccess = { Status: 1 };
                }
                else {
                    $.bigBox({
                        title: data.Massage,
                        content: data.Description,
                        color: "#C46A69",
                        timeout: 4000,
                        icon: "fa fa-bolt swing animated"
                    });
                }
            }).error(function (data, status, headers, config) {
                $.bigBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#C46A69",
                    timeout: 4000,
                    icon: "fa fa-bolt swing animated"
                });
            });
        }
        if ($scope.NewUserRoles.length == 0 && $scope.UserRoles.length == 0) {
            $.smallBox({
                title: "No Changes have been made!!",
                content: "<i class='fa fa-clock-o'></i> <i>...</i>",
                color: "#739E73",
                iconSmall: "fa fa-times fa-2x fadeInRight animated",
                timeout: 4000
            });
            $scope.UserRoles = temp;
        }
        else {
            $scope.Clear();
        }
    };
    $scope.UserAccessAllRoles = function () {
        $scope.Roles.forEach(function (element, index) {
            if (!element.isSelected) {
                element.isSelected = true;
            }
        })
    };

    /// VIEW ACCESS
    $scope.KaizenGridViewAccess = {};
    $scope.Views = [];
    $scope.UserViews = [];
    $scope.NewUserViews = [];
    $scope.ViewModuleCompanyChanged = function () {
        if ($scope.KaizenGridViewAccess.CompanyID != null && $scope.KaizenGridViewAccess.CompanyID != '') {
            $http.get('/Sys_Company/GetModuleAccessByCompany?CompanyID=' + $scope.KaizenGridViewAccess.CompanyID + '&KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
                $scope.CompanyModules = data;
            }).finally(function () { });
        }
    };
    $scope.ViewModuleChanged = function () {
        $http.get('/Admin_Screen/GetScreensByModule?KaizenPublicKey=' + sessionStorage.PublicKey, {
            params: {
                ModuleID: $scope.KaizenGridViewAccess.ModuleID
            }
        }).success(function (data) {
            $scope.ModuleScreens = data;
        }).finally(function () { });
    };

    $scope.ScreenChanged = function () {
        $http.get('/Sys_View/GetScreenViews?KaizenPublicKey=' + sessionStorage.PublicKey, {
            params: {
                ScreenID: $scope.KaizenGridViewAccess.ScreenID,
                CompanyID: $scope.KaizenGridViewAccess.CompanyID
            }
        }).success(function (data) {
            $scope.Views = data;
        }).finally(function () {
            if ($scope.KaizenGridViewAccess.UserName != "") {
                $http.get('/SysUser/GetViewAccessByUser?KaizenPublicKey=' + sessionStorage.PublicKey, {
                    params: {
                        UserName: $scope.KaizenGridViewAccess.UserName, ScreenID: $scope.KaizenGridViewAccess.ScreenID
                    }
                }).success(function (data) {
                    $scope.UserViews = data;
                }).finally(function () {
                    for (var i = 0; i < $scope.Views.length; i++) {
                        var item = $scope.Views[i];
                        item.isSelected = false;
                        for (var j = 0; j < $scope.UserViews.length; j++) {
                            var obj = $scope.UserViews[j];
                            if (obj.ViewID === item.ViewID) {
                                item.isSelected = true;
                                item.IsDefault = obj.IsDefault;
                                break;
                            }
                        }
                    }
                });
            }
        });
    };
    $scope.SetDefault = function (view) {
        for (var i = 0; i < $scope.Views.length; i++) {
            var item = $scope.Views[i];
            item.IsDefault = false;
        }
        view.IsDefault = true;
    };
    $scope.SaveKaizenGridViewAccess = function () {
        $scope.KaizenGridViewAccess.UserName = $scope.User.UserName;
        for (var i = 0; i < $scope.Views.length; i++) {
            var item = $scope.Views[i];
            if (item.isSelected) {
                var isdelete = false;
                for (var j = 0; j < $scope.UserViews.length; j++) {
                    var obj = $scope.UserViews[j];
                    if (obj.ViewID === item.ViewID) {
                        isdelete = true;
                        break;
                    }
                    else {
                        isdelete = false;
                    }
                }
                if (isdelete) {
                    $scope.UserViews.splice(j, 1);
                }
                else {
                    var tmp = {
                        ViewID: item.ViewID, UserName: $scope.KaizenGridViewAccess.UserName
                        , IsDefault: item.IsDefault
                    };
                    $scope.NewUserViews.push(tmp);
                }
            }
        }
        if ($scope.UserViews.length > 0) {
            $http({
                url: '/SysUser/DeleteKaizenGridViewAccess?KaizenPublicKey=' + sessionStorage.PublicKey,
                method: "POST",
                data: $.param({
                    KaizenGridViewAccess: $scope.UserViews
                }),
                headers: {
                    'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8'
                }
            }).success(function (data) {
                if (data.Status == true) {
                    $.bigBox({
                        title: data.Massage,
                        content: data.Description,
                        color: "#739E73",
                        timeout: 8000,
                        icon: "fa fa-check shake animated"
                    });
                }
                else {
                    $.bigBox({
                        title: data.Massage,
                        content: data.Description,
                        color: "#C46A69",
                        timeout: 8000,
                        icon: "fa fa-bolt swing animated"
                    });
                }
            }).error(function (data, status, headers, config) {
                $.bigBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#C46A69",
                    timeout: 8000,
                    icon: "fa fa-bolt swing animated"
                });
            });
        }
        if ($scope.NewUserViews.length > 0) {
            $http({
                url: '/SysUser/SaveKaizenGridViewAccess?KaizenPublicKey=' + sessionStorage.PublicKey,
                method: "POST",
                data: $.param({
                    KaizenGridViewAccess: $scope.NewUserViews
                }),
                headers: {
                    'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8'
                }
            }).success(function (data) {
                if (data.Status == true) {
                    $.bigBox({
                        title: data.Massage,
                        content: data.Description,
                        color: "#739E73",
                        timeout: 8000,
                        icon: "fa fa-check shake animated"
                    });
                    $scope.CompanyAccess = { Status: 1 };
                }
                else {
                    $.bigBox({
                        title: data.Massage,
                        content: data.Description,
                        color: "#C46A69",
                        timeout: 8000,
                        icon: "fa fa-bolt swing animated"
                    });
                }
            }).error(function (data, status, headers, config) {
                $.bigBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#C46A69",
                    timeout: 8000,
                    icon: "fa fa-bolt swing animated"
                });
            });
        }
        $scope.Cancel();
    };
    $scope.UserAccessAllViews = function () {
        $scope.Views.forEach(function (element, index) {
            if (!element.isSelected) {
                element.isSelected = true;
            }
        })
    };

    //--------------------------Encryption------------------------
    $scope.Encryption = {};
    $scope.EncryptPassword = function () {
        $http.get('/SysUser/PasswordEncryption?KaizenPublicKey=' + sessionStorage.PublicKey, {
            params: {
                Password: $scope.Encryption.EncryptPassword
            }
        }).success(function (data) {
            $scope.Encryption.DecryptPassword = data;
        }).finally(function () { });
    };
    $scope.DecryptPassword = function () {
        $http.get('/SysUser/PasswordDecryption?KaizenPublicKey=' + sessionStorage.PublicKey, {
            params: {
                Password: $scope.Encryption.DecryptPassword
            }
        }).success(function (data) {
            $scope.Encryption.EncryptPassword = data;
        }).finally(function () { });
    };


    $scope.DeleteFilterData = function () {
        $.SmartMessageBox({
            title: "Delete Record",
            content: "Are you sure you want to delete this?",
            buttons: '[No][Yes]'
        }, function (ButtonPressed) {
            if (ButtonPressed === "Yes") {
                var grid = $('#GridSysUserAudit').data("kendoGrid");
                var datasource = grid.dataSource.data();
                $http.post('/SysUser/DeleteAuditData', { 'KaizenAudit': datasource, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
                    if (data.Status == true) {
                        $.bigBox({
                            title: data.Massage,
                            content: data.Description,
                            color: "#739E73",
                            timeout: 4000,
                            icon: "fa fa-check shake animated"
                        });
                        $scope.GridRefresh('GridSysUserAudit');
                    }
                    else {
                        $.bigBox({
                            title: data.Massage,
                            content: data.Description,
                            color: "#C46A69",
                            timeout: 4000,
                            icon: "fa fa-bolt swing animated"
                        });
                    }
                }).error(function (data, status, headers, config) {
                    $.bigBox({
                        title: data.Massage,
                        content: data.Description,
                        color: "#C46A69",
                        timeout: 4000,
                        icon: "fa fa-bolt swing animated"
                    });
                });
            }
            if (ButtonPressed === "No") {
                $.smallBox({
                    title: "No Changes have been made!!",
                    content: "<i class='fa fa-clock-o'></i> <i>You pressed No...</i>",
                    color: "#3276B1",
                    iconSmall: "fa fa-times fa-2x fadeInRight animated",
                    timeout: 4000
                });
            }
        });
    };

});