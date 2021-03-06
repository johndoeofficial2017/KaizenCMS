﻿app.controller('Sys_DataSourceController', function ($scope, $http) {
    $scope.DT00100 = {};
    $scope.PagesDT00100 = [];
    $scope.toolbarOptions = {
        items: [
                 {
                     type: "button", text: "<span class=\"fa fa-plus-circle\"></span> Set Source ",
                     attributes: { "class": "btn-primary" },
                     click: function (e) {
                         $scope.$apply(function () {
                             $scope.AddDT00100();
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
                     click: function (e) { $scope.SourceUser(); }
                 },
                 {
                     type: "button", text: " Role Security", spriteCssClass: "k-tool-icon k-filter",
                     click: function (e) { $scope.SourceRoleSecurity(); }
                 },
                 {
                     type: "button", text: " Source Field", spriteCssClass: "k-tool-icon k-filter",
                     click: function (e) { $scope.SourceField(); }
                 }
        ]
 , resizable: true
    };
    $scope.SelectedLookUp = {};
    $scope.SourceUsers = [];
    $scope.Roles = [];

    $scope.LoadDT00100 = function (SourceID) {
        if (angular.isUndefined($scope.DT00100.SourceID)) {
            $http.get('/Sys_DataSource/GetSingle?KaizenPublicKey=' + sessionStorage.PublicKey + "&SourceID=" + SourceID).success(function (data) {
                $scope.DT00100 = data;
            }).finally(function () { $scope.DT00100.Status = 2; });
        }
    }
    $scope.LoadDT00100Page = function (ActionName) {
        removeEntityPage($scope.PagesDT00100);
        var URIPath = "/" + $scope.ToolBar.ActiveScreen.ID + "/" + ActionName + "?KaizenPublicKey=" + sessionStorage.PublicKey;
        var Page = { url: URIPath, ActionName: ActionName };
        $scope.PagesDT00100.push(Page);
    };

    $scope.AddDT00100 = function () {
        $scope.LoadDT00100Page('Create');
        $scope.Clear();
        $scope.DT00100.Status = 1;
        $scope.FillDropdownLists();
    };

    $scope.FillDropdownLists = function () {
        $http.get('/Sys_Company/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.CompanyList = data;
        }).finally(function () { });

        $http.get('/Sys_DataSource/GetViewTypes?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.ViewTypes = data;
        }).finally(function () { });

        $http.get('/Sys_KaizenModule/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.Modules = data;
        }).finally(function () { });

        $http.get('/Sys_DataSource/GetMenueTypes?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.MenueTypes = data;
        }).finally(function () { });
    };

    $scope.EditDT00100 = function (SourceID) {
        $scope.LoadDT00100Page('Create');
        $scope.FillDropdownLists();
        $scope.LoadDT00100(SourceID);
    };
    $scope.SaveData = function () {
        $http.post('/Sys_DataSource/SaveData', { 'DT00100': $scope.DT00100, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
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
    $scope.UpdateData = function () {
        $http.post('/Sys_DataSource/UpdateData', { 'DT00100': $scope.DT00100, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
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
        $scope.DT00100 = {};
        $scope.DT00101 = {};
        $scope.SelectedSourceField = {};
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
                $http.post('/Sys_DataSource/DeleteData', { 'DT00100': $scope.DT00100, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
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
        $scope.PagesDT00100 = [];
    };

    $scope.ModuleChanged = function () {
        //if (angular.isDefined($scope.SelectedLookUp.SelectedModuleID)) {
        //    $scope.DT00100.ModuleID = $scope.SelectedLookUp.SelectedModuleID;
        //}
    };

    $scope.MenueTypeChanged = function () {
        //if (angular.isDefined($scope.SelectedLookUp.SelectedMenueTypeID)) {
        //    $scope.DT00100.MenueTypeID = $scope.SelectedLookUp.SelectedMenueTypeID;
        //}
    };

    $scope.CompanyChanged = function () {
        //if (angular.isDefined($scope.SelectedLookUp.SelectedCompanyID)) {
        //    $scope.DT00100.CompanyID = $scope.SelectedLookUp.SelectedCompanyID;
        //}
    };

    $scope.ViewTypeChanged = function () {
        //if (angular.isDefined($scope.SelectedLookUp.SelectedViewType)) {
        //    $scope.DT00100.ViewType = $scope.SelectedLookUp.SelectedViewType;
        //}
    };

    $scope.UserBack = function (user) {
        if (user != null) {
            switch ($scope.CurrentControl) {
                case 'SourceUsers':
                    if (user != null) {
                        $scope.DT00103.UserName = user.UserName;
                    }
                    break;
            }
        }
    };

    //------------------------------ Source User ---------------------------
    $scope.DT00103 = {};
    $scope.SourceUser = function () {
        $scope.LoadDT00100Page('SourceUser');
        $scope.Clear();
        $scope.DT00103.Status = 0;
        $scope.LoadSourceList();
    };

    $scope.LoadSourceList = function () {
        $http.get('/Sys_DataSource/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.SourceList = data;
        }).finally(function () { });
    };

    $scope.RemoveSourceUser = function (sourceUser, index) {

        $.SmartMessageBox({
            title: "Delete Record",
            content: "Are you sure?",
            buttons: '[No][Yes]'
        }, function (ButtonPressed) {
            if (ButtonPressed === "Yes") {

                $http({
                    url: '/Sys_DataSource/DeleteSourceUser?KaizenPublicKey=' + sessionStorage.PublicKey,
                    method: "POST",
                    data: JSON.stringify(sourceUser),
                    dataType: "json",
                    headers: {
                        'Content-Type': 'application/json; charset=utf-8'
                    }
                }).success(function (data) {
                    if (data.Status == true) {
                        $scope.SourceUsers.splice(index, 1);
                        $.bigBox({
                            title: data.Massage,
                            content: data.Description,
                            color: "#C46A69",
                            timeout: 8000,
                            icon: "fa fa-bolt swing animated"
                        });
                    } else {
                        //Notify(data.Massage, 'bottom-right', '5000', 'danger', 'fa-bolt', true);
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
                        title: "Error",
                        content: "Unable to delete selected graph data",
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
                    color: "#C46A69",
                    iconSmall: "fa fa-times fa-2x fadeInRight animated",
                    timeout: 4000
                });
            }
        });
    };

    $scope.AddSourceUser = function () {
        $http({
            url: '/Sys_DataSource/SaveSourceUser?KaizenPublicKey=' + sessionStorage.PublicKey,
            method: "POST",
            data: JSON.stringify($scope.DT00103),
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

                var newObject = jQuery.extend(true, {}, $scope.DT00103);
                $scope.SourceUsers.push(newObject);
                $scope.DT00103.UserName = "";
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
        }).error(function (data, status, headers, config) {
            $.bigBox({
                title: "Error",
                content: "Unable to add user",
                color: "#C46A69",
                timeout: 8000,
                icon: "fa fa-bolt swing animated"
            });
        });
    };

    $scope.UpdateSourceUser = function (sourceUser) {
        $http({
            url: '/Sys_DataSource/UpdateSourceUser?KaizenPublicKey=' + sessionStorage.PublicKey,
            method: "POST",
            data: JSON.stringify(sourceUser),
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
        }).error(function (data, status, headers, config) {
            $.bigBox({
                title: "Error",
                content: "Unable to add user",
                color: "#C46A69",
                timeout: 8000,
                icon: "fa fa-bolt swing animated"
            });
        });
    };

    $scope.SourceIDChanged = function () {
        if (angular.isDefined($scope.DT00103.SourceID)) {
            $http.get('/Sys_DataSource/GetSourceUsers?KaizenPublicKey=' + sessionStorage.PublicKey,
            { params: { SourceID: $scope.DT00103.SourceID } }).success(function (data) {
                $scope.SourceUsers = data;
            }).finally(function () { });
        }
    };

    //------------------------------ Source Role Security ---------------------------
    $scope.DT00102 = {};
    $scope.SourceRoleSecurity = function () {
        $scope.LoadDT00100Page('SourceRoleSecurity');
        $scope.Clear();
        $scope.DT00102.Status = 0;
        $scope.LoadSourceList();
        $scope.LoadRoles();
    };

    $scope.SourceIDChangedForRoles = function () {
        if (angular.isDefined($scope.DT00102.SourceID)) {
            $http.get('/Sys_DataSource/GetSourceRoles?KaizenPublicKey=' + sessionStorage.PublicKey,
            { params: { SourceID: $scope.DT00102.SourceID } }).success(function (data) {

                if (data.length >= 0) {
                    if (data.length > 0)
                        $scope.SourceRoleSecurityList = data;
                    else
                        $scope.SourceRoleSecurityList = [];

                    $scope.Roles.forEach(function (eleRole, index) {
                        eleRole.isSelected = false;
                        $scope.SourceRoleSecurityList.forEach(function (eleSourceRole, index) {
                            if (eleRole.RoleID === eleSourceRole.RoleID) {
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

    $scope.UpdateSourceRole = function (role) {
        if (!$scope.DT00102 || !$scope.DT00102.SourceID) {
            $.bigBox({
                title: "Error",
                content: "Please select source id",
                color: "#C46A69",
                timeout: 8000,
                icon: "fa fa-bolt swing animated"
            });
            role.isSelected = !role.isSelected;
            return;
        }
        if (role) {
            $scope.DT00102.RoleID = role.RoleID;

            var urlToUpdate = "";

            if (role.isSelected) {
                urlToUpdate = "/Sys_DataSource/SaveSourceRole?KaizenPublicKey=" + sessionStorage.PublicKey;
            } else {
                urlToUpdate = "/Sys_DataSource/DeleteSourceRole?KaizenPublicKey=" + sessionStorage.PublicKey;
            }

            $http({
                url: urlToUpdate,
                method: "POST",
                data: JSON.stringify($scope.DT00102),
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
                    content: "Unable to save role for the selected source",
                    color: "#C46A69",
                    timeout: 8000,
                    icon: "fa fa-bolt swing animated"
                });
            });
        }
    };

    //---------------------------------------- Source Field ---------------------------------------
    $scope.DT00101 = {};
    $scope.SelectedSourceField = {};
    $scope.SourceField = function () {
        $scope.LoadDT00100Page('SourceField');
        $scope.Clear();
        $scope.DT00101.status = 0;
        $scope.LoadSourceList();
        $scope.LoadSourceFieldList();
    };

    $scope.LoadSourceFieldList = function () {
        $http.get('/Sys_DataSource/GetSourceFields?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.FieldTypes = data;
        }).finally(function () { });
    };

    $scope.SourceIDChangedForField = function () {
        if (angular.isDefined($scope.SelectedSourceField.SourceID)) {
            $scope.DT00101.SourceID = $scope.SelectedSourceField.SourceID;
            $scope.LoadSourceFields($scope.DT00101.SourceID);
        }
    };

    $scope.SourceFieldTypeChanged = function () {
        if (angular.isDefined($scope.SelectedSourceField.FieldTypeID)) {
            $scope.DT00101.FieldTypeID = $scope.SelectedSourceField.FieldTypeID;
        }
    };

    $scope.LoadSourceFields = function (SourceID) {
        $http.get('/Sys_DataSource/GetDT00101?KaizenPublicKey=' + sessionStorage.PublicKey,
            { params: { SourceID: SourceID } }).success(function (data) {
                if (data.length >= 0) {
                    if (data.length > 0)
                        $scope.SourceFieldList = data;
                    else
                        $scope.SourceFieldList = [];
                }
            });
    };

    $scope.AddSourceField = function () {
        $scope.DT00101.status = 1;
        if ($scope.DT00101) {
            $http({
                url: '/Sys_DataSource/SaveSourceField?KaizenPublicKey=' + sessionStorage.PublicKey,
                method: "POST",
                data: JSON.stringify($scope.DT00101),
                dataType: "json",
                headers: {
                    'Content-Type': 'application/json; charset=utf-8'
                }
            }).success(function (data) {
                if (data.Status == true) {
                    //$scope.CaseTypeEquationFieldsList.push($scope.DT00101);
                    $scope.DT00101 = {};
                    $scope.DT00101.SourceID = $scope.SelectedSourceField.SourceID;
                    $scope.DT00101.FieldTypeID = $scope.SelectedSourceField.FieldTypeID;
                    $scope.DT00101.status = 0;

                    $.bigBox({
                        title: data.Massage,
                        content: data.Description,
                        color: "#739E73",
                        timeout: 8000,
                        icon: "fa fa-bolt swing animated"
                    });

                    $scope.LoadSourceFields($scope.DT00101.SourceID);

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
            }).error(function (data, status, headers, config) {
                $.bigBox({
                    title: "Error",
                    content: "Unable to save case type field data",
                    color: "#C46A69",
                    timeout: 8000,
                    icon: "fa fa-bolt swing animated"
                });
            });
        }
    };

    $scope.EditSourceField = function (sourceField) {

        indx = $scope.SourceFieldList.indexOf(sourceField);
        var obj_extend = jQuery.extend({}, $scope.SourceFieldList[indx]);
        $scope.DT00101 = obj_extend;
        //var Obj_indx = $scope.functiontofindIndexByKeyValue($scope.SourceFieldList, "SourceID", $scope.DT00101.SourceID);
        //var obj = $scope.SourceFieldList[Obj_indx];
        $scope.SelectedSourceField.SourceID = obj_extend.SourceID;
        $scope.SelectedSourceField.FieldTypeID = obj_extend.FieldTypeID;
        //if ($scope.CM00080.status == 0 || angular.isUndefined($scope.CM00080.status)) {
        $scope.DT00101.status = 2;
    };

    $scope.UpdateSourceField = function () {
        if ($scope.DT00101.status == 0 || angular.isUndefined($scope.DT00101.status)) {
            $scope.DT00101.status = 2;
        }
        $scope.SourceFieldList.splice(indx, 1, $scope.DT00101);

        if ($scope.DT00101) {
            //if ($scope.SelectedFieldType && $scope.SelectedFieldType.FieldTypeID)
            //    $scope.DT00101.FieldTypeID = $scope.SelectedFieldType.FieldTypeID;

            $http({
                url: '/Sys_DataSource/UpdateSourceField?KaizenPublicKey=' + sessionStorage.PublicKey,
                method: "POST",
                data: $scope.DT00101,
                dataType: "json",
                headers: {
                    'Content-Type': 'application/json; charset=utf-8'
                }
            }).success(function (data) {
                if (data.Status == true) {
                    $scope.DT00101 = {};
                    $scope.DT00101.status = 0;

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
            }).error(function (data, status, headers, config) { });
        }
    };

    $scope.RemoveSourceField = function (sourceField, index) {

        $.SmartMessageBox({
            title: "Delete Record",
            content: "Are you sure?",
            buttons: '[No][Yes]'
        }, function (ButtonPressed) {
            if (ButtonPressed === "Yes") {

                $http({
                    url: '/Sys_DataSource/DeleteSourceField?KaizenPublicKey=' + sessionStorage.PublicKey,
                    method: "POST",
                    data: JSON.stringify(sourceField),
                    dataType: "json",
                    headers: {
                        'Content-Type': 'application/json; charset=utf-8'
                    }
                }).success(function (data) {
                    if (data.Status == true) {
                        if (sourceField.status == 1)
                            $scope.CaseTypeEquationFieldsList.splice(index, 1);
                        else
                            sourceField.status = 3;
                        //Notify(data.Massage, 'bottom-right', '5000', 'success', 'fa-check', true);
                        $.bigBox({
                            title: data.Massage,
                            content: data.Description,
                            color: "#C46A69",
                            timeout: 8000,
                            icon: "fa fa-bolt swing animated"
                        });
                    } else {
                        //Notify(data.Massage, 'bottom-right', '5000', 'danger', 'fa-bolt', true);
                        $.bigBox({
                            title: data.Massage,
                            content: data.Description,
                            color: "#C46A69",
                            timeout: 8000,
                            icon: "fa fa-bolt swing animated"
                        });
                    }
                }).error(function (data, status, headers, config) { alert(); });
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

    $scope.CancelSourceField = function () {
        $scope.DT00101.status = 1;
        $scope.DT00101.FieldName = "";
        $scope.DT00101.FieldDisplay = "";
        $scope.DT00101.FieldOrder = 0;
        $scope.DT00101.FieldWidth = 0;
        $scope.DT00101.IsColumn01 = false;
        $scope.DT00101.IsRequired = false;
    };

});