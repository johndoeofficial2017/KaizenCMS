app.controller('Sys_Viewcontroller', function ($scope, $rootScope, $http) {
    $scope.Kaizen00011 = {};
    $scope.SelectedCompany = {};
    $scope.PagesKaizen00011 = [];
    $scope.LoadLookUp = function () {
        $http.get('/Sys_Company/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.Companies = data;
        }).finally(function () {
            $scope.CompanyOptions = {
                filter: "startswith",
                model: "SelectedCompany",
                dataTextField: "CompanyName",
                dataValueField: "CompanyID",
                dataSource: $scope.Companies,
                value: $scope.Kaizen00011.GridCompanyID
            };
            $scope.Kaizen00011.GridCompanyID = $scope.MY.CompanyID;
            $scope.GridRefresh();
        });
        $http.get('/Admin_Screen/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.Screens = data;
        }).finally(function () { });
        $http.get('/Sys_KaizenModule/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.Modules = data;
        }).finally(function () {
            $scope.ModuleOptions = {
                filter: "startswith",
                model: "SelectedModule",
                dataTextField: "ModuleName",
                dataValueField: "ModuleID",
                dataSource: $scope.Modules,
                value: $scope.Kaizen00011.GridModuleID
            };
        });
    };
    $scope.toolbarOptionsSys_View = {
        items: [
                {
                    type: "button",
                    text: "<span class=\"fa fa-plus-circle\"></span> Add View",
                    attributes: { "class": "btn-primary" },
                    click: function (e) {
                        $scope.LoadKaizen00011Page('Create');
                        $scope.$apply();
                        $scope.Clear();
                        $scope.Kaizen00011.Status = 1;
                    }
                },
                 {
                     type: "splitButton",
                     spriteCssClass: "k-tool-icon",
                     text: "Actions",
                     click: function (e) {
                     },
                     menuButtons: [
                        {
                            text: "View Access", click: function (e) {
                                $scope.$apply(function () {
                                    $scope.LoadKaizen00011Page('ViewAccess');
                                    $scope.Clear();
                                });
                            }
                        },
                        {
                            text: "View Conditions", click: function (e) {
                                $scope.$apply(function () {
                                    removeEntityPage($scope.PagesKaizen00011);
                                    $scope.LoadKaizen00011Page('ViewCondition');
                                    $scope.Kaizen00014.Status = 1;
                                    $scope.Kaizen00014.SelectedField = {};
                                });
                            }
                        },
                        {
                            text: "View Management", click: function (e) {
                                $scope.$apply(function () {
                                    $scope.LoadKaizen00011Page('ViewMangement');
                                    $scope.Clear();
                                });
                            }
                        },
                        {
                            text: "Screen Management", click: function (e) {
                                $scope.$apply(function () {
                                    $scope.LoadKaizen00011Page('ScreenMangement');
                                    $scope.Clear();
                                });
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
                },
                { type: "separator" },
                { template: "<label>Company:</label>" },
                {
                    template: "<select kendo-drop-down-list k-ng-model='SelectedCompany' k-on-change='GridCompanyChanged()' k-options='CompanyOptions' id='GridCompanyOption_Sys_View' style='width: 250px;'></select>",
                    overflow: "never"
                },
                { template: "<label>Module:</label>" },
                {
                    template: "<select kendo-drop-down-list k-ng-model='SelectedModule' k-on-change='GridModuleChanged()' k-options='ModuleOptions' id='GridModuleOption_Sys_View' style='width: 250px;'></select>",
                    overflow: "never"
                },
                {
                    type: "button", text: " Filter", spriteCssClass: "k-tool-icon k-filter",
                    click: function (e) {
                        $scope.OpenFilterWindow("GridSys_View", "Sys_View");
                    }
                }
        ]
, resizable: true
    };
    $scope.LoadLookUp();
    $scope.LoadKaizen00011 = function (ViewID) {
        if (angular.isUndefined($scope.Kaizen00011.ViewID)) {
            $http.get('/Sys_View/GetSingle?KaizenPublicKey=' + sessionStorage.PublicKey + "&ViewID=" + ViewID).success(function (data) {
                $scope.Kaizen00011 = data;
            }).finally(function () { $scope.Kaizen00011.Status = 2; });
        }
    };
    $scope.LoadKaizen00011Page = function (ActionName) {
        removeEntityPage($scope.PagesKaizen00011);
        var URIPath = "/" + $scope.ToolBar.ActiveScreen.ID + "/" + ActionName + "?KaizenPublicKey=" + sessionStorage.PublicKey;
        var Page = {
            url: URIPath, ActionName: ActionName
        };
        $scope.PagesKaizen00011.push(Page);
    };


    $scope.EditKaizen00011 = function (ViewID) {
        $scope.LoadKaizen00011Page('Create');
        $scope.LoadKaizen00011(ViewID);
    };
    $scope.SaveData = function () {
        $http.post('/Sys_View/SaveData', {
            'Kaizen00011': $scope.Kaizen00011, 'KaizenPublicKey': sessionStorage.PublicKey
        }).success(function (data) {
            if (data.Status == true) {
                $.smallBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#739E73",
                    iconSmall: "fa fa-times fa-2x fadeInRight animated",
                    timeout: 4000
                });
                $scope.Cancel();
                $scope.GridRefresh();
            }
            else {
                //alert('ssssss')
            }
        }).error(function (data, status, headers, config) {
            //alert();
        });
    };
    $scope.UpdateData = function () {
        $http.post('/Sys_View/UpdateData', {
            'Kaizen00011': $scope.Kaizen00011, 'KaizenPublicKey': sessionStorage.PublicKey
        }).success(function (data) {
            if (data.Status == true) {
                $.smallBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#739E73",
                    iconSmall: "fa fa-times fa-2x fadeInRight animated",
                    timeout: 4000
                });
                $scope.Cancel();
                $scope.GridRefresh();
            }
            else {
                alert(data.Massage);
            }
        }).error(function (data, status, headers, config) {
            alert();
        });
    };
    $scope.Clear = function () {
        $scope.Kaizen00011 = {};
        $scope.KaizenGridViewAccess = {
        };
        $scope.selectedUser = {};
        $scope.lastUser = {};
        $scope.Kaizen00026 = {
        };
    };
    $scope.Print = function () {
        alert('Printer not configured');
    };
    $scope.Delete = function () {
        bootbox.confirm("Are you sure?", function (result) {
            if (result) {
                $http.post('/Sys_View/DeleteData', {
                    'Kaizen00011': $scope.Kaizen00011, 'KaizenPublicKey': sessionStorage.PublicKey
                }).success(function (data) {
                    if (data.Status == true) {
                        Notify(data.Massage, 'bottom-right', '5000', 'success', 'fa-check', true);
                        $scope.Cancel();
                        $scope.GridRefresh();
                    }
                    else {
                        Notify(data.Massage, 'bottom-right', '5000', 'danger', 'fa-bolt', true);
                    }
                }).error(function (data, status, headers, config) {
                    alert();
                });
            }
        });
    };
    $scope.Cancel = function () {
        $scope.Clear();
        $scope.PagesKaizen00011 = [];

        $scope.Views = [];
        $scope.UserViews = [];
        $scope.NewUserViews = [];

        $scope.NewViewSetting = [];
        $scope.DefaultViewSettings = [];
        $scope.RelatedViews = [];
        $scope.ViewFields = [];
        $scope.UpdateViewSetting = [];
        $scope.OrginalViewSettings = [];
        
    };

    $scope.GridCompanyChanged = function () {
        $scope.Kaizen00011.GridCompanyID = $scope.SelectedCompany.CompanyID;
        $scope.GridRefresh();
    };
    $scope.GridModuleChanged = function () {
        $scope.Kaizen00011.GridModuleID = $scope.SelectedModule.ModuleID;
        $scope.GridRefresh();
    };

    $scope.UserBack = function (user) {
        //alert($scope.CurrentControl);
        if (user != null) {
            switch ($scope.CurrentControl) {
                case 'EmailSetup':
                    $scope.User = user;
                    $scope.Kaizen00020.UserName = user.UserName;
                    $scope.GridRefresh('GridSysUserByEmail');
                    break;
                case 'UserDefaults':
                    $scope.User = user;
                    $scope.CompanyAccess.UserName = user.UserName;
                    $scope.GridRefresh('GridSysUserByCompanyAccess');
                    $http.get('/Sys_Company/GetUserCompanies?KaizenPublicKey=' + sessionStorage.PublicKey, { params: { UserName: $scope.CompanyAccess.UserName } }).success(function (data) {
                        $scope.Companies = data;
                    }).finally(function () { });
                    break;
                case 'CompanyAccess':
                    $scope.User = user;
                    $scope.CompanyAccess.UserName = user.UserName;
                    $http.get('/SysUser/GetCompanyAccessByuser?KaizenPublicKey=' + sessionStorage.PublicKey, { params: { UserName: $scope.CompanyAccess.UserName } }).success(function (data) {
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
                    $scope.KaizenUserRole.UserName = user.UserName;
                    $http.get('/SysUser/GetRoleAccessByUser?KaizenPublicKey=' + sessionStorage.PublicKey, { params: { UserName: $scope.KaizenUserRole.UserName } }).success(function (data) {
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
                    $scope.Views = [];
                    alert($scope.KaizenGridViewAccess.ScreenID);
                    if ($scope.KaizenGridViewAccess.ScreenID != "" && $scope.KaizenGridViewAccess.ScreenID != undefined) {
                        $scope.ScreenChanged();
                    }
                    break;
            }
        }
    };

    ///////////////////////////////// View Access /////////////////////////
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
        alert('aaaaaaaaaaaaaaaaaa')
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

    ///////////////////////////////// Screen & View Fields /////////////////////////
    $scope.Kaizen00026 = {};
    $scope.NewViewSetting = [];
    $scope.UpdateViewSetting = [];
    $scope.OrginalViewSettings = [];
    $scope.ModuleCompanyChanged = function () {
        if ($scope.Kaizen00026.CompanyID != null && $scope.Kaizen00026.CompanyID != '') {
            $http.get('/Sys_Company/GetModuleAccessByCompany?CompanyID=' + $scope.Kaizen00026.CompanyID + '&KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
                $scope.CompanyModules = data;
            }).finally(function () { });
        }
    };
    $scope.ModuleChanged = function () {
        $http.get('/Admin_Screen/GetScreensByModule?KaizenPublicKey=' + sessionStorage.PublicKey, {
            params: {
                ModuleID: $scope.Kaizen00026.ModuleID
            }
        }).success(function (data) {
            $scope.ModuleScreens = data;
        }).finally(function () { });
    };
    $scope.CompanyScreenChanged = function () {
        if ($scope.Kaizen00026.CompanyID != '' && $scope.Kaizen00026.CompanyID != undefined
            && $scope.Kaizen00026.ModuleID != '' && $scope.Kaizen00026.ModuleID != undefined
            && $scope.Kaizen00026.ScreenID != '' && $scope.Kaizen00026.ScreenID != undefined) {
            $http.get('/Sys_View/GetScreenViews?KaizenPublicKey=' + sessionStorage.PublicKey, {
                params: {
                    ScreenID: $scope.Kaizen00026.ScreenID
                    , CompanyID: $scope.Kaizen00026.CompanyID
                }
            }).success(function (data) {
                $scope.RelatedViews = data;
            }).finally(function () {
                $http.get('/Admin_Screen/GetFieldsByScreen?KaizenPublicKey=' + sessionStorage.PublicKey, { params: { ScreenID: $scope.Kaizen00026.ScreenID } }).success(function (data) {
                    $scope.DefaultViewSettings = data;
                }).finally(function () { });
            });
        }
    };
    $scope.ViewChanged = function () {
        $http.get('/Admin_Screen/GetFieldsByView?KaizenPublicKey=' + sessionStorage.PublicKey, {
            params: {
                ViewID: $scope.Kaizen00026.ViewID
            }
        }).success(function (data) {
            $scope.ViewFields = data;
        }).finally(function () {
            for (var i = 0; i < $scope.DefaultViewSettings.length; i++) {
                var item = $scope.DefaultViewSettings[i];
                item.isSelected = false;
                for (var j = 0; j < $scope.ViewFields.length; j++) {
                    var obj = $scope.ViewFields[j];
                    if (obj.field === item.field) {
                        item.isSelected = true;
                        item.title = obj.title;
                        item.width = obj.width;
                        item.ColumnOrder = obj.ColumnOrder;
                        item.IsActive = obj.IsActive;
                        item.locked = obj.locked;
                        item.lockable = obj.lockable;
                        item.encoded = obj.encoded;
                        item.filterable = obj.filterable;
                        item.sortable = obj.sortable;
                        item.hidden = obj.hidden;
                        item.menu = obj.menu;
                        item.format = obj.format;
                        item.template = obj.template;
                        item.FieldEquation = obj.FieldEquation;
                        break;
                    }
                    else {
                        item.isSelected = false;
                    }
                }
            }
            $scope.OrginalViewSettings = angular.copy($scope.DefaultViewSettings);
        });
    };

    $scope.SaveViewManagement = function () {
        for (var i = 0; i < $scope.DefaultViewSettings.length; i++) {
            var item = $scope.DefaultViewSettings[i];
            item.ViewID = $scope.Kaizen00026.ViewID;
            if (item.isSelected) {
                var isdelete = false;
                var isupdate = false;
                for (var j = 0; j < $scope.ViewFields.length; j++) {
                    var obj = $scope.ViewFields[j];
                    if (obj.field === item.field) {
                        var indx = $scope.functiontofindIndexByKeyValue($scope.OrginalViewSettings, "field", obj.field);
                        if (angular.equals(item, $scope.OrginalViewSettings[indx])) {
                            isdelete = true;
                            isupdate = false;
                            break;
                        } else {
                            isdelete = false;
                            isupdate = true;
                            break;
                        }
                    }
                    else {
                        isdelete = false;
                    }
                }
                //alert(item.FieldEquation);
                if (isdelete && !isupdate) {
                    $scope.ViewFields.splice(j, 1);
                }
                else if (isupdate && !isdelete) {
                    var Update_tmp = {
                        ViewID: item.ViewID,
                        field: item.field, title: item.title,
                        width: item.width, ColumnOrder: item.ColumnOrder,
                        IsActive: item.IsActive, locked: item.locked,
                        lockable: item.lockable, encoded: item.encoded,
                        filterable: item.filterable, sortable: item.sortable,
                        hidden: item.hidden, menu: item.menu,
                        format: item.format, template: item.template
                        , FieldEquation: item.FieldEquation
                    };
                    $scope.UpdateViewSetting.push(Update_tmp);
                    $scope.ViewFields.splice(j, 1);
                }
                else {
                    var tmp = {
                        ViewID: item.ViewID,
                        field: item.field, title: item.title,
                        width: item.width, ColumnOrder: item.ColumnOrder,
                        IsActive: item.IsActive, locked: item.locked,
                        lockable: item.lockable, encoded: item.encoded,
                        filterable: item.filterable, sortable: item.sortable,
                        hidden: item.hidden, menu: item.menu,
                        format: item.format, template: item.template
                        , FieldEquation: item.FieldEquation
                    };
                    $scope.NewViewSetting.push(tmp);
                }
            }
        }
        if ($scope.ViewFields.length > 0) {
            $http({
                url: '/Admin_Screen/DeleteKaizen00026Data?KaizenPublicKey=' + sessionStorage.PublicKey,
                method: "POST",
                data: $.param({
                    Kaizen00026: $scope.ViewFields
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
        if ($scope.NewViewSetting.length > 0) {
            $http({
                url: '/Admin_Screen/SaveKaizen00026Data?KaizenPublicKey=' + sessionStorage.PublicKey,
                method: "POST",
                data: $.param({
                    Kaizen00026: $scope.NewViewSetting
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
        if ($scope.UpdateViewSetting.length > 0) {
            $http({
                url: '/Admin_Screen/UpdateKaizen00026Data?KaizenPublicKey=' + sessionStorage.PublicKey,
                method: "POST",
                data: $.param({
                    Kaizen00026: $scope.UpdateViewSetting
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
    };

    //----------------------- View Condtions --------------------------
    $scope.Kaizen00014 = {};

    $scope.CompanyChangedViaCondition = function () {
        $http.get('/Sys_Company/GetModuleAccessByCompany?CompanyID=' + $scope.Kaizen00014.CompanyID + '&KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.ConditionCompanyModules = data;
        }).finally(function () { });
    };
    $scope.ModuleChangedViaCondition = function () {
        $http.get('/Admin_Screen/GetScreensByModule?KaizenPublicKey=' + sessionStorage.PublicKey, {
            params: {
                ModuleID: $scope.Kaizen00014.ModuleID
            }
        }).success(function (data) {
            $scope.ConditionModuleScreens = data;
        }).finally(function () { });
    };
    $scope.ScreenChangedViaCondition = function () {
        $http.get('/Sys_View/GetScreenViews?KaizenPublicKey=' + sessionStorage.PublicKey, {
            params: {
                ScreenID: $scope.Kaizen00014.ScreenID,
                CompanyID: $scope.Kaizen00014.CompanyID
            }
        }).success(function (data) {
            $scope.ConditionViews = data;
        });
    };
    $scope.ViewChangedViaCondition = function () {
        if (angular.isDefined($scope.Kaizen00014.ScreenID) && $scope.Kaizen00014.ScreenID != '') {
            $http.get('/Sys_View/GetScreenConditionFields?KaizenPublicKey=' + sessionStorage.PublicKey, {
                params: {
                    ScreenID: $scope.Kaizen00014.ScreenID
                }
            }).success(function (data) {
                $scope.ConditionViewFields = data;
                $scope.GridRefresh('GridSys_ViewConditionByView');
            });
        }
    };

    $scope.SaveViewCondition = function () {
        $http.post('/Sys_View/SaveViewCondition', { 'Kaizen00014': $scope.Kaizen00014, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
            if (data.Status == true) {
                $.bigBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#739E73",
                    timeout: 3000,
                    icon: "fa fa-check shake animated"
                });
                $scope.GridRefresh('GridSys_ViewConditionByView');
                $scope.Kaizen00014 = {
                    Status: 1, ViewID: $scope.Kaizen00014.ViewID,
                    CompanyID: $scope.Kaizen00014.CompanyID,
                    ModuleID: $scope.Kaizen00014.ModuleID,
                    ScreenID: $scope.Kaizen00014.ScreenID
                };
            }
            else {
                $.bigBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#C46A69",
                    timeout: 3000,
                    icon: "fa fa-bolt swing animated"
                });
            }
        }).error(function (data, status, headers, config) {
            $.bigBox({
                title: data.Massage,
                content: data.Description,
                color: "#C46A69",
                timeout: 3000,
                icon: "fa fa-bolt swing animated"
            });
        });
    };
    $scope.DeleteViewCondition = function (dataItem) {
        $http.post('/Sys_View/DeleteViewCondition', { 'Kaizen00014': dataItem, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
            if (data.Status == true) {
                $.bigBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#739E73",
                    timeout: 3000,
                    icon: "fa fa-check shake animated"
                });
                $scope.GridRefresh('GridSys_ViewConditionByView');
            }
            else {
                $.bigBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#C46A69",
                    timeout: 3000,
                    icon: "fa fa-bolt swing animated"
                });
            }
        }).error(function (data, status, headers, config) {
            $.bigBox({
                title: data.Massage,
                content: data.Description,
                color: "#C46A69",
                timeout: 3000,
                icon: "fa fa-bolt swing animated"
            });
        });
    };

    //9	 => Location
    //10 => DateTime
    //11 => Time
    $scope.SourceChanged = function () {
        $scope.Kaizen00014.FieldName = $scope.Kaizen00014.SelectedField.FieldName;
        //1	 => Number
        if ($scope.Kaizen00014.SelectedField.FieldTypeID == 1) {
            $scope.Kaizen00014.SelectedField.html = "<input type='number' class='form-control' " +
    "placeholder='' ng-model='Kaizen00014.FieldValue' />";
        }
            //2	 => String
        else if ($scope.Kaizen00014.SelectedField.FieldTypeID == 2) {
            $scope.Kaizen00014.SelectedField.html = "<input type='text' class='form-control' " +
    "placeholder='' ng-model='Kaizen00014.FieldValue' />";
        }
            //3	 => Date
        else if ($scope.Kaizen00014.SelectedField.FieldTypeID == 3) {
            $scope.Kaizen00014.SelectedField.html = "<input kendo-date-picker k-format=\"'dd/MM/yyyy'\" type='text' class='form-control' " +
    "placeholder='' k-ng-model='Kaizen00014.FieldValue' style='width: 100%;' />";
        }
            //4	 => LookUp
        else if ($scope.Kaizen00014.SelectedField.FieldTypeID == 4) {
            if ($scope.Kaizen00014.SelectedField.FieldName == 'CaseRef') {
                $scope.Kaizen00014.SelectedField.html = "<div class='input-group'><input type='text' class='form-control' readonly " +
                    "placeholder='Case Reference' ng-model='Kaizen00014.FieldValue' />" +
                    "<span class='input-group-btn input-group-sm'>" +
                    "<button class='btn btn-default' ng-click=\"OpenkendoWindowForm('CMS_Case','PopUp')\">" +
                    "<span class='glyphicon glyphicon-search'></span></button></span></div>";
            }
            else if ($scope.Kaizen00014.SelectedField.FieldName == 'ContractID') {
                $scope.Kaizen00014.SelectedField.html = "<div class='input-group'><input type='text' class='form-control' readonly " +
                    "placeholder='Contract ID' ng-model='Kaizen00014.FieldValue' />" +
                    "<span class='input-group-btn input-group-sm'>" +
                    "<button class='btn btn-default' ng-click=\"OpenkendoWindowForm('CMS_Contract','PopUp')\">" +
                    "<span class='glyphicon glyphicon-search'></span></button></span></div>";
            }
            else if ($scope.Kaizen00014.SelectedField.FieldName == 'ClientID') {
                $scope.Kaizen00014.SelectedField.html = "<div class='input-group'><input type='text' class='form-control' readonly " +
                    "placeholder='Client ID' ng-model='Kaizen00014.FieldValue' />" +
                    "<span class='input-group-btn input-group-sm'>" +
                    "<button class='btn btn-default' ng-click=\"OpenkendoWindowForm('CMS','CMS_CO_Client','PopUp')\">" +
                    "<span class='glyphicon glyphicon-search'></span></button></span></div>";
            }
            else if ($scope.Kaizen00014.SelectedField.FieldName == 'CIFNumber') {
                $scope.Kaizen00014.SelectedField.html = "<div class='input-group'><input type='text' class='form-control' readonly " +
                    "placeholder='CIF Number' ng-model='Kaizen00014.FieldValue' />" +
                    "<span class='input-group-btn input-group-sm'>" +
                    "<button class='btn btn-default' ng-click=\"OpenkendoWindowForm('CMS','CMS_Debtor','PopUp')\">" +
                    "<span class='glyphicon glyphicon-search'></span></button></span></div>";
            }
            else if ($scope.Kaizen00014.SelectedField.FieldName == 'AgentID') {
                $scope.Kaizen00014.SelectedField.html = "<div class='input-group'><input type='text' class='form-control' readonly " +
                    "placeholder='Agent ID' ng-model='Kaizen00014.FieldValue' />" +
                    "<span class='input-group-btn input-group-sm'>" +
                    "<button class='btn btn-default' ng-click=\"OpenkendoWindowForm('CMS','CMS_Agent','PopUp')\">" +
                    "<span class='glyphicon glyphicon-search'></span></button></span></div>";
            }

        }
            //5	 => DropDown
        else if ($scope.Kaizen00014.SelectedField.FieldTypeID == 5) {
            $scope.DropDownDataSource = {
                transport: {
                    read: {
                        url: "/Sys_View/DynamicFillDropDownList",
                        data: {
                            KaizenPublicKey: sessionStorage.PublicKey,
                            kaizenTableName: $scope.Kaizen00014.SelectedField.kaizenTableName
                        },
                        dataType: "json",
                        type: "GET"
                    }
                }
            };
            if ($scope.Kaizen00014.SelectedField.FieldName == 'TRXTypeID') {
                $scope.Kaizen00014.SelectedField.html = "<select kendo-drop-down-list k-filter=\"'startswith'\" k-option-label=\"'-- Select Product --'\" " +
                "k-ng-model=\"Kaizen00014.FieldValue\" " +
                "k-data-text-field=\"'TrxTypeName'\" " +
                "k-value-primitive=\"true\" " +
                "k-data-value-field=\"'TRXTypeID'\" " +
                "k-data-source=\"DropDownDataSource\" " +
                "style=\"width: 100%\"></select>";
            }
            else if ($scope.Kaizen00014.SelectedField.FieldName == 'CaseStatusID') {
                $scope.Kaizen00014.SelectedField.html = "<select kendo-drop-down-list k-filter=\"'startswith'\" k-option-label=\"'-- Select Status --'\" " +
                "k-ng-model=\"Kaizen00014.FieldValue\" " +
                "k-data-text-field=\"'CaseStatusname'\" " +
                "k-value-primitive=\"true\" " +
                "k-data-value-field=\"'CaseStatusID'\" " +
                "k-data-source=\"DropDownDataSource\" " +
                "style=\"width: 100%\"></select>";
            }

        }
            //6	 => Long Text
        else if ($scope.Kaizen00014.SelectedField.FieldTypeID == 6) {
            $scope.Kaizen00014.SelectedField.html = "<textarea class='form-control' rows='4' ng-model='Kaizen00014.FieldValue'><textarea/>";
        }
            //7	 => Email
        else if ($scope.Kaizen00014.SelectedField.FieldTypeID == 7) {
            $scope.Kaizen00014.SelectedField.html = "<input type='email' class='form-control' " +
    "placeholder='' ng-model='Kaizen00014.FieldValue' />";
        }
            //8	 => Phone
        else if ($scope.Kaizen00014.SelectedField.FieldTypeID == 8) {
            $scope.Kaizen00014.SelectedField.html = "<input type='phone' class='form-control' " +
    "placeholder='' ng-model='Kaizen00014.FieldValue' />";
        }

    };
    $scope.OpenGenericPopUp = function () {
        $scope.CurrentControl = $scope.Kaizen00014.SelectedField.kaizenTableName;
        $scope.TransferObject = { ScreenID: $scope.Kaizen00014.SelectedField.SourceScreenID };
        $scope.GenericPopUpWindow.center().toFront().open();
        $scope.GenericPopUpWindow.refresh({
            url: "/Sys_PopUp/PopUp?KaizenPublicKey=" + sessionStorage.PublicKey
        });

    };

    $scope.CaseBack = function (caseObj) {
        if (caseObj != null) {
            $scope.Kaizen00014.FieldValue = caseObj.CaseRef
        }
    };
    $scope.ContractBack = function (contract) {
        if (contract != null) {
            $scope.Kaizen00014.FieldValue = contract.ContractID;
        }
    };
    $scope.DebtorBack = function (debtor) {
        if (debtor != null) {
            $scope.Kaizen00014.FieldValue = debtor.DebtorID;
        }
    };
    $scope.ClientBack = function (client) {
        if (client != null) {
            $scope.Kaizen00014.FieldValue = client.ClientID;
        }
    };
    $scope.AgentBack = function (agent) {
        if (agent != null) {
            $scope.Kaizen00014.FieldValue = agent.AgentID;
        }
    };

    ///////////////////////////////// Screen & View Fields /////////////////////////
    $scope.Kaizen00013 = {};
    $scope.ScreenFieldSettings = [];
    $scope.ScreenModuleCompanyChanged = function () {
        if ($scope.Kaizen00013.CompanyID != null && $scope.Kaizen00013.CompanyID != '') {
            $http.get('/Sys_Company/GetModuleAccessByCompany?CompanyID=' + $scope.Kaizen00013.CompanyID + '&KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
                $scope.CompanyModules = data;
            }).finally(function () { });
        }
    };
    $scope.ScreenModuleChanged = function () {
        $http.get('/Admin_Screen/GetScreensByModule?KaizenPublicKey=' + sessionStorage.PublicKey, {
            params: {
                ModuleID: $scope.Kaizen00013.ModuleID
            }
        }).success(function (data) {
            $scope.ModuleScreens = data;
        }).finally(function () { });
    };
    $scope.ScreenCompanyChanged = function () {
        if ($scope.Kaizen00013.CompanyID != '' && $scope.Kaizen00013.CompanyID != undefined
            && $scope.Kaizen00013.ModuleID != '' && $scope.Kaizen00013.ModuleID != undefined
            && $scope.Kaizen00013.ScreenID != '' && $scope.Kaizen00013.ScreenID != undefined) {
            $http.get('/Sys_View/GetScreenConditionFields?KaizenPublicKey=' + sessionStorage.PublicKey, {
                params: {
                    ScreenID: $scope.Kaizen00013.ScreenID
                }
            }).success(function (data) {
                $scope.ScreenFieldSettings = data;
            });
        }
    };

    $scope.UpdateScreenField = function (item) {
        $http.post('/Sys_View/UpdateScreenField', { 'Kaizen00013': item, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
            if (data.Status == true) {
                $.bigBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#739E73",
                    timeout: 3000,
                    icon: "fa fa-check shake animated"
                });
            }
            else {
                $.bigBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#C46A69",
                    timeout: 3000,
                    icon: "fa fa-bolt swing animated"
                });
            }
        }).error(function (data, status, headers, config) {
            $.bigBox({
                title: data.Massage,
                content: data.Description,
                color: "#C46A69",
                timeout: 3000,
                icon: "fa fa-bolt swing animated"
            });
        });

    };

});