app.controller('Sys_roleController', function ($scope, $http) {
    $scope.Kaizen00030 = {};
    $scope.PagesKaizen00030 = [];
    $scope.toolbarOptions = {
        items: [
                 {
                     type: "button", text: "<span class=\"fa fa-plus-circle\"></span> New Role",
                     attributes: { "class": "btn-primary" },
                     click: function (e) {
                         $scope.$apply(function () {
                             $scope.AddKaizen00030();
                         });
                     }
                 },
                 {
                     type: "button", text: "<span class=\"\"></span> Role Security",
                     attributes: { "class": "btn-primary" },
                     click: function (e) {
                         $scope.$apply(function () {
                             $scope.RoleSecurity();
                         });
                     }
                 },
                 {
                     type: "button", text: "<span class=\"\"></span> View Role Security",
                     attributes: { "class": "btn-primary" },
                     click: function (e) {
                         $scope.$apply(function () {
                             $scope.ViewRoleSecurity();
                         });
                     }
                 },
                 {
                     type: "button", text: "<span class=\"\"></span> Company",
                     attributes: { "class": "btn-primary" },
                     click: function (e) {
                         $scope.$apply(function () {
                             $scope.RoleCompany();
                         });
                     }
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

    $scope.LoadKaizen00030 = function (RoleID) {
        if (angular.isUndefined($scope.Kaizen00030.RoleID)) {
            $http.get('/Sys_role/GetSingle?KaizenPublicKey=' + sessionStorage.PublicKey + "&RoleID=" + RoleID).success(function (data) {
                $scope.Kaizen00030 = data;
            }).finally(function () { $scope.Kaizen00030.Status = 2; });
        }
    };
    $scope.LoadKaizen00030Page = function (ActionName) {
        removeEntityPage($scope.PagesKaizen00030);
        var URIPath = "/Sys_role/" + ActionName + "?KaizenPublicKey=" + sessionStorage.PublicKey;
        var Page = {
            url: URIPath, ActionName: ActionName
        };
        $scope.PagesKaizen00030.push(Page);
    };

    $scope.AddKaizen00030 = function () {
        $scope.LoadKaizen00030Page('Create');
        $scope.Clear();
        $scope.Kaizen00030.Status = 1;
    };
    $scope.EditKaizen00030 = function (RoleID) {
        $scope.LoadKaizen00030Page('Create');
        $scope.Clear();
        $scope.LoadKaizen00030(RoleID);
    };
    $scope.SaveData = function () {
        $http.post('/Sys_role/SaveData', {
            'Kaizen00030': $scope.Kaizen00030, 'KaizenPublicKey': sessionStorage.PublicKey
        }).success(function (data) {
            if (data.Status == true) {
                $.bigBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#739E73",
                    timeout: 8000,
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
    };
    $scope.UpdateData = function () {
        $http.post('/Sys_role/UpdateData', {
            'Kaizen00030': $scope.Kaizen00030, 'KaizenPublicKey': sessionStorage.PublicKey
        }).success(function (data) {
            if (data.Status == true) {
                $.bigBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#739E73",
                    timeout: 8000,
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
    };
    $scope.Clear = function () {
        $scope.Kaizen00030 = {};
        $scope.Kaizen00030.Status = 1;
        $scope.Kaizen004 = {};
        $scope.Kaizen005 = {};

        $scope.Roles = [];
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
                $http.post('/Sys_role/DeleteData', { 'Kaizen00030': $scope.Kaizen00030, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
                    if (data.Status == true) {
                        $.bigBox({
                            title: data.Massage,
                            content: data.Description,
                            color: "#739E73",
                            timeout: 8000,
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
        $scope.Clear();
        $scope.PagesKaizen00030 = [];

        $scope.Tasks = [];
        $scope.RoleTasks = [];
        $scope.NewRoleTasks = [];

        $scope.RoleViews = [];
        $scope.Views = [];
    };

    //------------- Role Security
    $scope.Kaizen004 = {};
    $scope.Tasks = [];
    $scope.RoleTasks = [];
    $scope.NewRoleTasks = [];

    $scope.RoleSecurity = function () {
        $scope.LoadKaizen00030Page('RoleSecurity');
        $scope.Clear();
        $http.get('/Sys_role/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.Roles = data;
        }).finally(function () { });
        $http.get('/Sys_KaizenModule/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.Modules = data;
        }).finally(function () { });
        $http.get('/Sys_Company/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.Companies = data;
        }).finally(function () {
        });

    };
    $scope.RoleModuleChanged = function () {
        if ($scope.Kaizen004.CompanyID != undefined && $scope.Kaizen004.CompanyID != ''
        && $scope.Kaizen004.ModuleID != undefined && $scope.Kaizen004.ModuleID != '') {
            $http.get('/Sys_role/GetModuleTasks?KaizenPublicKey=' + sessionStorage.PublicKey, { params: { ModuleID: $scope.Kaizen004.ModuleID } }).success(function (data) {
                $scope.Tasks = data;
            }).finally(function () {
                if ($scope.Kaizen004.RoleID != "" && $scope.Kaizen004.RoleID != undefined) {
                    $http.get('/Sys_role/GetTaskAccessByModule?KaizenPublicKey=' + sessionStorage.PublicKey, {
                        params: {
                            ModuleID: $scope.Kaizen004.ModuleID,
                            CompanyID: $scope.Kaizen004.CompanyID,
                            RoleID: $scope.Kaizen004.RoleID
                        }
                    }).success(function (data) {
                        $scope.RoleTasks = data;
                        for (var i = 0; i < $scope.Tasks.length; i++) {
                            var item = $scope.Tasks[i];
                            item.isSelected = false;
                            for (var j = 0; j < $scope.RoleTasks.length; j++) {
                                var obj = $scope.RoleTasks[j];
                                if (obj.TaskID === item.TaskID && obj.ModuleID === item.ModuleID && obj.MenueTypeID === item.MenueTypeID) {
                                    item.isSelected = true;
                                    break;
                                }
                            }
                        }
                    }).finally(function () { });
                }
            });
        }
    };
    $scope.RoleChanged = function () {
        $http.get('/Sys_role/GetTaskAccessByModule?KaizenPublicKey=' + sessionStorage.PublicKey, {
            params: {
                ModuleID: $scope.Kaizen004.ModuleID,
                CompanyID: $scope.Kaizen004.CompanyID,
                RoleID: $scope.Kaizen004.RoleID
            }
        }).success(function (data) {
            $scope.RoleTasks = data;
            for (var i = 0; i < $scope.Tasks.length; i++) {
                var item = $scope.Tasks[i];
                item.isSelected = false;
                for (var j = 0; j < $scope.RoleTasks.length; j++) {
                    var obj = $scope.RoleTasks[j];
                    if (obj.TaskID === item.TaskID && obj.ModuleID === item.ModuleID && obj.MenueTypeID === item.MenueTypeID) {
                        item.isSelected = true;
                        break;
                    }
                }
            }
        }).finally(function () { });
    };
    $scope.SaveRoleSecurityChanges = function () {
        for (var i = 0; i < $scope.Tasks.length; i++) {
            var item = $scope.Tasks[i];
            if (item.isSelected) {
                var isdelete = false;
                for (var j = 0; j < $scope.RoleTasks.length; j++) {
                    var obj = $scope.RoleTasks[j];
                    if (obj.TaskID === item.TaskID && obj.ModuleID === item.ModuleID
                                && obj.MenueTypeID === item.MenueTypeID) {
                        isdelete = true;
                        break;
                    }
                    else {
                        isdelete = false;
                    }
                }
                if (isdelete) {
                    $scope.RoleTasks.splice(j, 1);
                }
                else {
                    var tmp = {
                        TaskID: item.TaskID,
                        RoleID: $scope.Kaizen004.RoleID,
                        MenueTypeID: item.MenueTypeID,
                        CompanyID: $scope.Kaizen004.CompanyID,
                        MenuName: item.MenuName,
                        ScreenPath: item.ScreenPath,
                        IsCustomPage: false,
                        MenueOrder: 1,
                        WindowPath: item.WindowPath,
                        ModuleID: $scope.Kaizen004.ModuleID
                    };
                    $scope.NewRoleTasks.push(tmp);
                }
            }
        }
        if ($scope.RoleTasks.length > 0) {
            $http({
                url: '/Sys_role/DeleteRoleSecurityData?KaizenPublicKey=' + sessionStorage.PublicKey,
                method: "POST",
                data: $.param({
                    Kaizen004: $scope.RoleTasks
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
        if ($scope.NewRoleTasks.length > 0) {
            $http({
                url: '/Sys_role/SaveRoleSecurityData?KaizenPublicKey=' + sessionStorage.PublicKey,
                method: "POST",
                data: $.param({
                    Kaizen004: $scope.NewRoleTasks
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
        if ($scope.RoleTasks.length == 0 && $scope.NewRoleTasks.length == 0) {
            $.smallBox({
                title: "No Changes have been made!!",
                content: "<i class='fa fa-clock-o'></i> <i>You pressed No...</i>",
                color: "#3276B1",
                iconSmall: "fa fa-times fa-2x fadeInRight animated",
                timeout: 4000
            });
        }
        $scope.Cancel();
    };

    //------------- View Role Security
    $scope.Kaizen005 = {};
    $scope.RoleViews = [];
    $scope.Views = [];
    $scope.ViewRoleSecurity = function () {
        $scope.LoadKaizen00030Page('ViewRoleSecurity');
        $scope.Clear();
        $http.get('/Sys_role/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.Roles = data;
        }).finally(function () { });
        $http.get('/Admin_Screen/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.Screens = data;
        }).finally(function () { });
        $http.get('/Sys_Company/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.Companies = data;
        }).finally(function () { });
    };
    $scope.ScreenChanged = function () {
        if ($scope.Kaizen005.CompanyID != '' && $scope.Kaizen005.CompanyID != undefined
    && $scope.Kaizen005.ScreenID != '' && $scope.Kaizen005.ScreenID != undefined) {
            $http.get('/Sys_View/GetScreenViews?KaizenPublicKey=' + sessionStorage.PublicKey, {
                params: {
                    ScreenID: $scope.Kaizen005.ScreenID,
                    CompanyID: $scope.Kaizen005.CompanyID
                }
            }).success(function (data) {
                $scope.Views = data;
            }).finally(function () {
                if ($scope.Kaizen005.RoleID != undefined || $scope.Kaizen005.RoleID != null) {
                    $http.get('/Sys_role/GetViewAccessByRole?KaizenPublicKey=' + sessionStorage.PublicKey, {
                        params: {
                            RoleID: $scope.Kaizen005.RoleID
                        }
                    }).success(function (data) {
                        $scope.RoleViews = data;
                        for (var i = 0; i < $scope.Views.length; i++) {
                            var item = $scope.Views[i];
                            item.isSelected = false;
                            for (var j = 0; j < $scope.RoleViews.length; j++) {
                                var obj = $scope.RoleViews[j];
                                if (obj.ViewID === item.ViewID) {
                                    item.isSelected = true;
                                    break;
                                }
                            }
                        }
                    }).finally(function () { });
                }
            });
        }
    };
    $scope.ModuleCompanyChanged = function () {
        if ($scope.Kaizen005.CompanyID != null && $scope.Kaizen005.CompanyID != '') {
            $http.get('/Sys_Company/GetModuleAccessByCompany?CompanyID=' + $scope.Kaizen005.CompanyID + '&KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
                $scope.CompanyModules = data;
            }).finally(function () { });
        }
    };
    $scope.ModuleChanged = function () {
        $http.get('/Admin_Screen/GetScreensByModule?KaizenPublicKey=' + sessionStorage.PublicKey, {
            params: {
                ModuleID: $scope.Kaizen005.ModuleID
            }
        }).success(function (data) {
            $scope.ModuleScreens = data;
        }).finally(function () { });
    };

    $scope.ViewRoleChanged = function () {
        $http.get('/Sys_role/GetViewAccessByRole?KaizenPublicKey=' + sessionStorage.PublicKey, {
            params: {
                RoleID: $scope.Kaizen005.RoleID
            }
        }).success(function (data) {
            $scope.RoleViews = data;
            for (var i = 0; i < $scope.Views.length; i++) {
                var item = $scope.Views[i];
                item.isSelected = false;
                for (var j = 0; j < $scope.RoleViews.length; j++) {
                    var obj = $scope.RoleViews[j];
                    if (obj.ViewID === item.ViewID) {
                        item.isSelected = true;
                        break;
                    }
                }
            }
        }).finally(function () { });
    };
    $scope.CheckViewRole = function (view) {
        if (view.isSelected) {
            $http({
                url: '/Sys_role/SaveViewRole?KaizenPublicKey=' + sessionStorage.PublicKey,
                method: "POST",
                data: $.param({
                    ViewID: view.ViewID,
                    RoleID: $scope.Kaizen005.RoleID
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
        else {
            $http({
                url: '/Sys_role/DeleteViewRole?KaizenPublicKey=' + sessionStorage.PublicKey,
                method: "POST",
                data: $.param({
                    ViewID: view.ViewID,
                    RoleID: $scope.Kaizen005.RoleID
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
    };
    $scope.SaveAll = function () {
        $scope.Views.forEach(function (view, index) {
            if (!view.isSelected) {
                $http({
                    url: '/Sys_role/SaveViewRole?KaizenPublicKey=' + sessionStorage.PublicKey,
                    method: "POST",
                    data: $.param({
                        ViewID: view.ViewID,
                        RoleID: $scope.Kaizen005.RoleID
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
        })
    };

    //-------------- Roles Company ----------------
    $scope.Kaizen006 = {};
    $scope.RoleCompany = function () {
        $scope.LoadKaizen00030Page('RoleCompany');
        $scope.Clear();

        $http.get('/Sys_Company/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.Companies = data;
        }).finally(function () {
        });
        
        $http.get('/Sys_role/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.Roles = data;
        }).finally(function () { });
    };
    
    $scope.CompanyChanged = function () {
        if ($scope.Kaizen004.CompanyID != undefined && $scope.Kaizen004.CompanyID != '') {
            $scope.LoadRoles_ByCompany($scope.Kaizen004.CompanyID);
        }
    };

    $scope.LoadRoles_ByCompany = function (CompanyID) {
        $http.get('/Sys_role/GetRolesByCompany?KaizenPublicKey=' + sessionStorage.PublicKey,
            { params: { CompanyID: CompanyID } }).success(function (data) {
                if (data.length >= 0) {
                    if (data.length > 0)
                        $scope.CompanyRoles = data;
                    else
                        $scope.CompanyRoles = [];

                    $scope.Roles.forEach(function (eleRole, index) {
                        eleRole.isSelected = false;
                        $scope.CompanyRoles.forEach(function (eleCompanyRole, index) {
                            if (eleRole.RoleID === eleCompanyRole.RoleID) {
                                eleRole.isSelected = true;
                            }
                        });
                    });
                }
            });
    };

    $scope.UpdateCompanyRole = function(role) {
        if (!$scope.Kaizen004 || $scope.Kaizen004.CompanyID == undefined || $scope.Kaizen004.CompanyID === '') {
            $.bigBox({
                title: "Error",
                content: "Please select company",
                color: "#C46A69",
                timeout: 8000,
                icon: "fa fa-bolt swing animated"
            });
            role.isSelected = !role.isSelected;
            return;
        }
        if (role) {
            $scope.Kaizen006 = {};
            $scope.Kaizen006.RoleID = role.RoleID;
            $scope.Kaizen006.CompanyID = $scope.Kaizen004.CompanyID;

            var urlToUpdate = "";

            if (role.isSelected) {
                urlToUpdate = "/Sys_role/SaveCompanyRole?KaizenPublicKey=" + sessionStorage.PublicKey;
            } else {
                urlToUpdate = "/Sys_role/DeleteCompanyRole?KaizenPublicKey=" + sessionStorage.PublicKey;
            }

            $http({
                url: urlToUpdate,
                method: "POST",
                data: JSON.stringify($scope.Kaizen006),
                dataType: "json",
                headers: {
                    'Content-Type': 'application/json; charset=utf-8'
                }
            }).success(function(data) {
                if (data.Status == true) {
                    $.bigBox({
                        title: data.Massage,
                        content: data.Description,
                        color: "#739E73",
                        timeout: 8000,
                        icon: "fa fa-bolt swing animated"
                    });
                } else {
                    $.bigBox({
                        title: data.Massage,
                        content: data.Description,
                        color: "#C46A69",
                        timeout: 8000,
                        icon: "fa fa-bolt swing animated"
                    });
                    return;
                }
            }).error(function(data, status, headers, config) {
                $.bigBox({
                    title: "Error",
                    content: "Unable to save role for the selected company",
                    color: "#C46A69",
                    timeout: 8000,
                    icon: "fa fa-bolt swing animated"
                });
            });
        }
    };

});
