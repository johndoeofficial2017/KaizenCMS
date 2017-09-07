app.controller('Sys_DashboardController', function ($scope, $http) {
    $scope.Kaizen00050 = {};
    $scope.PagesKaizen00050 = [];
    $scope.toolbarOptions = {
        items: [
                 {
                     type: "button", text: "<span class=\"fa fa-plus-circle\"></span> Set Source ",
                     attributes: { "class": "btn-primary" },
                     click: function (e) {
                         $scope.$apply(function () {
                             $scope.AddKaizen00050();
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
                 },
                 //{ type: "button", spriteCssClass: "k-tool-icon k-i-refresh" },
                 //{
                 //    type: "button", text: " Filter", spriteCssClass: "k-tool-icon k-filter",
                 //    click: function (e) { $scope.openFilterWindow(); }
                 //},
                 {
                     type: "button", text: " Users", spriteCssClass: "k-tool-icon k-filter",
                     click: function (e) { $scope.DashboardUser(); }
                 },
                 {
                     type: "button", text: " Role Security", spriteCssClass: "k-tool-icon k-filter",
                     click: function (e) { $scope.DashboardRoleSecurity(); }
                 },
                 {
                     type: "button", text: " Report Types", spriteCssClass: "k-tool-icon k-filter",
                     click: function (e) { $scope.DashboardReport(); }
                 }
        ]
 , resizable: true
    };
    $scope.SelectedLookUp = {};
    $scope.SourceUsers = [];
    $scope.Roles = [];
    $scope.SelectedDashboard = {};
    $scope.SelectedReportType = {};

    $scope.LoadKaizen00050 = function (DashboardCode) {
        if (angular.isUndefined($scope.Kaizen00050.DashboardCode)) {
            $http.get('/Sys_Dashboard/GetSingle?KaizenPublicKey=' + sessionStorage.PublicKey + "&DashboardCode=" + DashboardCode).success(function (data) {
                $scope.Kaizen00050 = data;
            }).finally(function () { $scope.Kaizen00050.Status = 2; });
        }
    }
    $scope.LoadKaizen00050Page = function (ActionName) {
        removeEntityPage($scope.PagesKaizen00050);
        var URIPath = "/" + $scope.ToolBar.ActiveScreen.ID + "/" + ActionName + "?KaizenPublicKey=" + sessionStorage.PublicKey;
        var Page = { url: URIPath, ActionName: ActionName };
        $scope.PagesKaizen00050.push(Page);
    };

    $scope.AddKaizen00050 = function () {
        $scope.LoadKaizen00050Page('Create');
        $scope.Clear();
        $scope.Kaizen00050.Status = 1;
        $scope.FillDropdownLists();
    };

    $scope.FillDropdownLists = function () {
        $http.get('/Sys_Company/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.CompanyList = data;
        }).finally(function () { });
    };

    $scope.EditKaizen00050 = function (DashboardCode) {
        $scope.LoadKaizen00050Page('Create');
        $scope.FillDropdownLists();
        $scope.LoadKaizen00050(DashboardCode);
    };
    $scope.SaveData = function () {
        $http.post('/Sys_Dashboard/SaveData', { 'Kaizen00050': $scope.Kaizen00050, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
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
                title: "Error",
                content: "Unable to save dashboard data",
                color: "#C46A69",
                timeout: 4000,
                icon: "fa fa-bolt swing animated"
            });
        });
    };
    $scope.UpdateData = function () {
        $http.post('/Sys_Dashboard/UpdateData', { 'Kaizen00050': $scope.Kaizen00050, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
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
    $scope.Clear = function () {
        $scope.Kaizen00050 = {};
        $scope.Kaizen00054 = {};
        $scope.Kaizen00055 = {};
        $scope.DashboardList = [];
        $scope.SelectedReportType = {};
        $scope.DashboardReportList = [];
        $scope.Kaizen00053List = [];
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
                $http.post('/Sys_Dashboard/DeleteData', { 'Kaizen00050': $scope.Kaizen00050, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
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
                    color: "#C46A69",
                    iconSmall: "fa fa-times fa-2x fadeInRight animated",
                    timeout: 4000
                });
            }
        });
    };
    $scope.Cancel = function () {
        $scope.Clear();
        $scope.PagesKaizen00050 = [];
    };

    $scope.ModuleChanged = function () {
        //if (angular.isDefined($scope.SelectedLookUp.SelectedModuleID)) {
        //    $scope.Kaizen00050.ModuleID = $scope.SelectedLookUp.SelectedModuleID;
        //}
    };

    $scope.MenueTypeChanged = function () {
        //if (angular.isDefined($scope.SelectedLookUp.SelectedMenueTypeID)) {
        //    $scope.Kaizen00050.MenueTypeID = $scope.SelectedLookUp.SelectedMenueTypeID;
        //}
    };

    $scope.CompanyChanged = function () {
        if (angular.isDefined($scope.SelectedDashboard.CompanyID)) {
            $scope.Kaizen00050.CompanyID = $scope.SelectedDashboard.CompanyID;
        }
    };

    $scope.ViewTypeChanged = function () {
        //if (angular.isDefined($scope.SelectedLookUp.SelectedViewType)) {
        //    $scope.Kaizen00050.ViewType = $scope.SelectedLookUp.SelectedViewType;
        //}
    };

    $scope.UserBack = function (user) {
        if (user != null) {
            switch ($scope.CurrentControl) {
                case 'DashboardUsers':
                    if (user != null) {
                        $scope.Kaizen00055.UserName = user.UserName;
                        $scope.UserChangedForDashboard($scope.Kaizen00055.UserName);
                    }
                    break;
            }
        }
    };

    $scope.LoadDashboardList = function () {
        $http.get('/Sys_Dashboard/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.DashboardList = data;
        }).finally(function () { });
    };

    //------------------------------ Dashboard User ---------------------------
    $scope.Kaizen00055 = {};
    $scope.DashboardUser = function () {
        $scope.LoadKaizen00050Page('DashboardUser');
        $scope.Clear();
        $scope.Kaizen00055.Status = 0;
        $scope.LoadDashboardList();
    };

    $scope.UserChangedForDashboard = function (userName) {
        if (angular.isDefined(userName)) {
            $http.get('/Sys_Dashboard/GetDashboardUsers?KaizenPublicKey=' + sessionStorage.PublicKey,
            { params: { userName: userName } }).success(function (data) {

                if (data.length >= 0) {
                    if (data.length > 0)
                        $scope.DashboardUsersList = data;
                    else
                        $scope.DashboardUsersList = [];

                    $scope.DashboardList.forEach(function (eleRole, index) {
                        eleRole.isSelected = false;
                        $scope.DashboardUsersList.forEach(function (eleSourceRole, index) {
                            if (eleRole.DashboardCode === eleSourceRole.DashboardCode) {
                                eleRole.isSelected = true;
                            }
                        });
                    });
                }
            }).finally(function () { });
        }
    };

    $scope.UpdateDashboardUser = function (dashboard) {
        if (!$scope.Kaizen00055 || !$scope.Kaizen00055.UserName) {
            $.bigBox({
                title: "Error",
                content: "Please select user",
                color: "#C46A69",
                timeout: 8000,
                icon: "fa fa-bolt swing animated"
            });
            dashboard.isSelected = !dashboard.isSelected;
            return;
        }
        if (dashboard) {
            $scope.Kaizen00055.DashboardCode = dashboard.DashboardCode;

            var urlToUpdate = "";

            if (dashboard.isSelected) {
                urlToUpdate = "/Sys_Dashboard/SaveDashboardUser?KaizenPublicKey=" + sessionStorage.PublicKey;
            } else {
                urlToUpdate = "/Sys_Dashboard/DeleteDashboardUser?KaizenPublicKey=" + sessionStorage.PublicKey;
            }

            $http({
                url: urlToUpdate,
                method: "POST",
                data: JSON.stringify($scope.Kaizen00055),
                dataType: "json",
                headers: {
                    'Content-Type': 'application/json; charset=utf-8'
                }
            }).success(function (data) {
                if (data.Status == true) {
                    $.bigBox({
                        title: data.Massage,
                        content: data.Description,
                        color: "#739E73",
                        timeout: 8000,
                        icon: "fa fa-bolt swing animated"
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
                    return;
                }
            }).error(function (data, status, headers, config) {
                $.bigBox({
                    title: "Error",
                    content: "Unable to save dashboard for the selected user",
                    color: "#C46A69",
                    timeout: 8000,
                    icon: "fa fa-bolt swing animated"
                });
            });
        }
    };

    //------------------------------ Dashboard Role Security ---------------------------
    $scope.Kaizen00054 = {};
    $scope.DashboardRoleSecurity = function () {
        $scope.LoadKaizen00050Page('DashboardRoleSecurity');
        $scope.Clear();
        $scope.Kaizen00054.Status = 0;
        $scope.LoadDashboardList();
        $scope.LoadRoles();
    };

    $scope.RoleChangedForDashboard = function () {
        if (angular.isDefined($scope.Kaizen00054.RoleID)) {
            $http.get('/Sys_Dashboard/GetDashboardRoles?KaizenPublicKey=' + sessionStorage.PublicKey,
            { params: { RoleID: $scope.Kaizen00054.RoleID } }).success(function (data) {

                if (data.length >= 0) {
                    if (data.length > 0)
                        $scope.DashboardRoleSecurityList = data;
                    else
                        $scope.DashboardRoleSecurityList = [];

                    $scope.DashboardList.forEach(function (eleRole, index) {
                        eleRole.isSelected = false;
                        $scope.DashboardRoleSecurityList.forEach(function (eleSourceRole, index) {
                            if (eleRole.DashboardCode === eleSourceRole.DashboardCode) {
                                eleRole.isSelected = true;
                            }
                        });
                    });
                }
            }).finally(function () { });
        }
    };

    $scope.LoadRoles = function () {
        if ($scope.Roles == null || $scope.Roles.length == 0) {
            $http.get('/Sys_role/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
                $scope.Roles = data;
            }).finally(function () { });
        }
    };

    $scope.UpdateDashboardRole = function (dashboard) {
        if (!$scope.Kaizen00054 || !$scope.Kaizen00054.RoleID) {
            $.bigBox({
                title: "Error",
                content: "Please select role",
                color: "#C46A69",
                timeout: 8000,
                icon: "fa fa-bolt swing animated"
            });
            dashboard.isSelected = !dashboard.isSelected;
            return;
        }
        if (dashboard) {
            $scope.Kaizen00054.DashboardCode = dashboard.DashboardCode;

            var urlToUpdate = "";

            if (dashboard.isSelected) {
                urlToUpdate = "/Sys_Dashboard/SaveDashboardRole?KaizenPublicKey=" + sessionStorage.PublicKey;
            } else {
                urlToUpdate = "/Sys_Dashboard/DeleteDashboardRole?KaizenPublicKey=" + sessionStorage.PublicKey;
            }

            $http({
                url: urlToUpdate,
                method: "POST",
                data: JSON.stringify($scope.Kaizen00054),
                dataType: "json",
                headers: {
                    'Content-Type': 'application/json; charset=utf-8'
                }
            }).success(function (data) {
                if (data.Status == true) {
                    $.bigBox({
                        title: data.Massage,
                        content: data.Description,
                        color: "#739E73",
                        timeout: 8000,
                        icon: "fa fa-bolt swing animated"
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
                    return;
                }
            }).error(function (data, status, headers, config) {
                $.bigBox({
                    title: "Error",
                    content: "Unable to save dashboard for the selected source",
                    color: "#C46A69",
                    timeout: 8000,
                    icon: "fa fa-bolt swing animated"
                });
            });
        }
    };

    //------------------------------ Dashboard Report ---------------------------
    $scope.Kaizen00053 = {};
    $scope.DashboardReport = function () {
        $scope.LoadKaizen00050Page('DashboardReport');
        $scope.Clear();
        $scope.Kaizen00053.Status = 0;
        $scope.LoadReportTypes();
        $scope.LoadDashboardList();
    };

    $scope.LoadReportTypes = function () {
        $http.get('/Sys_Dashboard/GetReportTypes?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.ReportTypes = data;
        }).finally(function () { });
    };

    $scope.ReportTypeChangedForDashboard = function () {
        if (angular.isDefined($scope.SelectedReportType.ReportTypeID)) {
            $http.get('/Sys_Dashboard/GetDashboardReports?KaizenPublicKey=' + sessionStorage.PublicKey,
            { params: { ReportTypeID: $scope.SelectedReportType.ReportTypeID } }).success(function (data) {

                if (data.length >= 0) {
                    if (data.length > 0) {
                        $scope.DashboardReportList = data;
                        $scope.LoadDashboardReportData($scope.SelectedReportType.ReportTypeID);
                    }
                    else
                        $scope.DashboardReportList = [];
                }
            }).finally(function () { });
        }
    };

    $scope.DashboardCodeChangedForReport = function () {

    };

    $scope.LoadRoles = function () {
        if ($scope.Roles == null || $scope.Roles.length == 0) {
            $http.get('/Sys_role/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
                $scope.Roles = data;
            }).finally(function () { });
        }
    };

    $scope.Kaizen00053List = [];
    $scope.LoadDashboardReportData = function (ReportTypeID) {
        $http.get('/Sys_Dashboard/GetKaizen00053ForReportType?KaizenPublicKey=' + sessionStorage.PublicKey,
            { params: { ReportTypeID: ReportTypeID } }).success(function (data) {

                if (data.length >= 0) {
                    if (data.length > 0) {
                        $scope.Kaizen00053List = data;

                        $scope.DashboardReportList.forEach(function (ele52, index) {
                            ele52.isSelected = false;
                            $scope.Kaizen00053List.forEach(function (ele53, index) {
                                if (ele52.ReportID === ele53.ReportID) {
                                    ele52.isSelected = true;
                                    ele52.DashboardCode = ele53.DashboardCode;
                                    ele52.IsDefault = ele53.IsDefault;
                                    ele52.Status = 2;
                                }
                            });
                        });
                    }
                    else
                        $scope.Kaizen00053List = [];
                }
            }).finally(function () { });
    };

    $scope.UpdateDashboardReportData = function (arrToUpdate, arrToDelete) {
        if (arrToUpdate.length > 0) {
            $http.post('/Sys_Dashboard/UpdateDashboardReportData', { 'Kaizen00053List': arrToUpdate, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
                if (data.Status == true) {
                    $.bigBox({
                        title: data.Massage,
                        content: data.Description,
                        color: "#739E73",
                        timeout: 4000,
                        icon: "fa fa-check shake animated"
                    });
                    if (arrToDelete.length > 0)
                        $scope.DeleteDashboardReportData(arrToDelete);
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
                    title: "Error",
                    content: "Unable to save dashboard report data",
                    color: "#C46A69",
                    timeout: 4000,
                    icon: "fa fa-bolt swing animated"
                });
            });
        }
        else
        {
            if (arrToDelete.length > 0)
                $scope.DeleteDashboardReportData(arrToDelete);
        }
    };

    $scope.DeleteDashboardReportData = function (arrToDelete) {
        $http.post('/Sys_Dashboard/DeleteDashboardReportData', { 'Kaizen00053List': arrToDelete, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
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
                title: "Error",
                content: "Unable to save dashboard report data",
                color: "#C46A69",
                timeout: 4000,
                icon: "fa fa-bolt swing animated"
            });
        });
    };

    $scope.SaveDashboardReport = function () {
        var arrToDelete = [], arrToAdd = [], arrToUpdate = [];
        var isFound = false;

        $scope.DashboardReportList.forEach(function (ele52, index) {
            isFound = false;
            if ($scope.Kaizen00053List.length > 0) {
                $scope.Kaizen00053List.forEach(function (ele53, index, object) {
                    if (ele52.ReportID === ele53.ReportID) {
                        isFound = true;
                        var objToUpdate = {};
                        objToUpdate.ReportID = ele52.ReportID;
                        objToUpdate.DashboardCode = ele52.DashboardCode;
                        objToUpdate.IsDefault = ele52.IsDefault;

                        if (ele52.isSelected) {
                            arrToUpdate.push(objToUpdate);
                        }
                        else {
                            object.splice(index, 1);
                            arrToDelete.push(objToUpdate);
                            ele52.Status = 1;
                            ele52.IsDefault = false;
                        }
                    }
                });
                if (!isFound) {
                    if (ele52.isSelected) {
                        var objToSave = {};
                        objToSave.ReportID = ele52.ReportID;
                        objToSave.DashboardCode = ele52.DashboardCode;
                        objToSave.IsDefault = ele52.IsDefault;

                        arrToAdd.push(objToSave);
                        ele52.Status = 2;
                    }
                }
            }
            else if (!isFound) {
                if (ele52.isSelected) {
                    var objToSave = {};
                    objToSave.ReportID = ele52.ReportID;
                    objToSave.DashboardCode = ele52.DashboardCode;
                    objToSave.IsDefault = ele52.IsDefault;

                    arrToAdd.push(objToSave);
                    ele52.Status = 2;
                }
            }
        });
                
        if (arrToAdd.length > 0) {
            $http.post('/Sys_Dashboard/AddDashboardReportData', { 'Kaizen00053List': arrToAdd, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
                if (data.Status == true) {
                    $.bigBox({
                        title: data.Massage,
                        content: data.Description,
                        color: "#739E73",
                        timeout: 4000,
                        icon: "fa fa-check shake animated"
                    });
                    debugger;
                    arrToAdd.forEach(function (ele53, index) {
                        $scope.Kaizen00053List.push(ele53);
                    });

                    $scope.UpdateDashboardReportData(arrToUpdate, arrToDelete);
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
                    title: "Error",
                    content: "Unable to save dashboard data",
                    color: "#C46A69",
                    timeout: 4000,
                    icon: "fa fa-bolt swing animated"
                });
            });
        }
        else {
            $scope.UpdateDashboardReportData(arrToUpdate, arrToDelete);
        }
    };

    $scope.AddorRemoveDashboardReport = function (dashboard) {
        if (dashboard && !dashboard.DashboardCode) {
            dashboard.isSelected = !dashboard.isSelected;
            $.bigBox({
                title: "Warning",
                content: "Select dashboard code",
                color: "#C46A69",
                timeout: 4000,
                icon: "fa fa-bolt swing animated"
            });
        }
    };
});