app.controller('TableSourceController', function ($scope, $http) {
    $scope.Ext00002 = {};
    $scope.PagesExt00002 = [];
    $scope.toolbarOptions = {
        items: [
                 {
                     type: "button", text: "<span class=\"fa fa-plus-circle\"></span> Add Source ",
                     attributes: { "class": "btn-primary" },
                     click: function (e) {
                         $scope.$apply(function () {
                             $scope.AddExt00002();
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
                     click: function (e) { $scope.DBSourceUser(); }
                 },
                 {
                     type: "button", text: " Role Security", spriteCssClass: "k-tool-icon k-filter",
                     click: function (e) { $scope.DBSourceRoleSecurity(); }
                 },
                 {
                     type: "button", text: "DB Source Field", spriteCssClass: "k-tool-icon k-filter",
                     click: function (e) { $scope.DBSourceField(); }
                 }
        ]
 , resizable: true
    };
    $scope.SelectedLookUp = {};
    $scope.SelectedSource = {};
    $scope.SourceUsers = [];
    $scope.Roles = [];

    $scope.LoadExt00002 = function (DataBaseSourceID, ComTableName) {
        $http.get('/Extender/Ext00002/GetSingle?KaizenPublicKey=' + sessionStorage.PublicKey + "&DataBaseSourceID=" + DataBaseSourceID + "&ComTableName=" + ComTableName).success(function (data) {
            $scope.Ext00002 = data;
            if (angular.isDefined($scope.Ext00002.DataBaseSourceID)) {
                $scope.SelectedSource.DataBaseSourceID = $scope.Ext00002.DataBaseSourceID;
            }
        }).finally(function () { $scope.Ext00002.Status = 2; });
    };
    $scope.LoadExt00002Page = function (ActionName) {
        removeEntityPage($scope.PagesExt00002);
        var URIPath = "/Extender/" + $scope.ToolBar.ActiveScreen.ID + "/" + ActionName + "?KaizenPublicKey=" + sessionStorage.PublicKey;
        var Page = { url: URIPath, ActionName: ActionName };
        $scope.PagesExt00002.push(Page);
    };

    $scope.FillDropdownLists = function () {
        $scope.LoadSourceList();

        $http.get('/Sys_Company/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.CompanyList = data;
        }).finally(function () { });

        $http.get('/Sys_KaizenModule/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.Modules = data;
        }).finally(function () { });

        $http.get('/Extender/Ext00002/GetMenueTypes?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.MenueTypes = data;
        }).finally(function () { });
    };

    $scope.AddExt00002 = function () {
        $scope.LoadExt00002Page('Create');
        $scope.Clear();
        $scope.Ext00002.Status = 1;
        $scope.FillDropdownLists();
    };

    $scope.EditExt00002 = function (DataBaseSourceID, ComTableName) {
        $scope.LoadExt00002Page('Create');
        $scope.FillDropdownLists();
        $scope.LoadExt00002(DataBaseSourceID, ComTableName);
    };
    $scope.SaveData = function () {
        $http.post('/Extender/Ext00002/SaveData', { 'Ext00002': $scope.Ext00002, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
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
        $http.post('/Extender/Ext00002/UpdateData', { 'Ext00002': $scope.Ext00002, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
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
        $scope.Ext00002 = {};
        $scope.Ext00003 = {};
        $scope.Ext00004 = {};
        $scope.SelectedSourceField = {};
        $scope.Roles = [];
        $scope.SourceUsers = [];
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
                $http.post('/Extender/Ext00002/DeleteData', { 'Ext00002': $scope.Ext00002, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
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
        $scope.PagesExt00002 = [];
    };

    $scope.ModuleChanged = function () {
        //if (angular.isDefined($scope.SelectedLookUp.SelectedModuleID)) {
        //    $scope.Ext00002.ModuleID = $scope.SelectedLookUp.SelectedModuleID;
        //}
    };

    $scope.MenueTypeChanged = function () {
        //if (angular.isDefined($scope.SelectedLookUp.SelectedMenueTypeID)) {
        //    $scope.Ext00002.MenueTypeID = $scope.SelectedLookUp.SelectedMenueTypeID;
        //}
    };

    $scope.CompanyChanged = function () {
        //if (angular.isDefined($scope.SelectedLookUp.SelectedCompanyID)) {
        //    $scope.Ext00002.CompanyID = $scope.SelectedLookUp.SelectedCompanyID;
        //}
    };

    $scope.DataBaseSourceChanged = function () {
        if (angular.isDefined($scope.SelectedSource.DataBaseSourceID)) {
            $scope.Ext00002.DataBaseSourceID = $scope.SelectedSource.DataBaseSourceID;
        }
    };

    $scope.UserBack = function (user) {
        if (user != null) {
            switch ($scope.CurrentControl) {
                case 'SourceUsers':
                    if (user != null) {
                        $scope.Ext00005.UserName = user.UserName;
                    }
                    break;
            }
        }
    };

    //------------------------------ Source User ---------------------------
    $scope.Ext00005 = {};
    $scope.DBSourceUser = function () {
        $scope.LoadExt00002Page('DBSourceUser');
        $scope.Clear();
        $scope.Ext00005.Status = 0;
        $scope.LoadSourceList();
    };

    $scope.LoadSourceList = function () {
        $http.get('/Extender/Ext00002/GetDataBaseSourceList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.DataBaseSourceList = data;
        }).finally(function () { });
    };

    $scope.DatabaseSourceIDChangedForUsers = function () {
        if (angular.isDefined($scope.SelectedSource.DataBaseSourceID)) {
            $scope.Ext00005.DataBaseSourceID = $scope.SelectedSource.DataBaseSourceID;
            $scope.LoadComTableNameListForUsers($scope.Ext00005.DataBaseSourceID);
        }
    };

    $scope.LoadComTableNameListForUsers = function (DataBaseSourceID) {
        $http.get('/Extender/Ext00002/GetComTableNameList?KaizenPublicKey=' + sessionStorage.PublicKey,
            { params: { DataBaseSourceID: DataBaseSourceID } }).success(function (data) {
                $scope.ComTableNameList = data;
            }).finally(function () { });
    };

    $scope.ComTableNameChangedForUsers = function () {
        if (angular.isDefined($scope.SelectedSource.ComTableName)) {
            $scope.Ext00005.ComTableName = $scope.SelectedSource.ComTableName;
            $scope.LoadDBSourceFieldsForUsers($scope.Ext00005.DataBaseSourceID, $scope.Ext00005.ComTableName);
        }
    };

    $scope.LoadDBSourceFieldsForUsers = function (DataBaseSourceID, ComTableName) {
        $http.get('/Extender/Ext00002/GetSourceUsers?KaizenPublicKey=' + sessionStorage.PublicKey,
            { params: { DataBaseSourceID: DataBaseSourceID, ComTableName: ComTableName } }).success(function (data) {
                $scope.SourceUsers = data;
            });
    };

    $scope.RemoveSourceUser = function (sourceUser, index) {

        $.SmartMessageBox({
            title: "Delete Record",
            content: "Are you sure?",
            buttons: '[No][Yes]'
        }, function (ButtonPressed) {
            if (ButtonPressed === "Yes") {

                $http({
                    url: '/Extender/Ext00002/DeleteSourceUser?KaizenPublicKey=' + sessionStorage.PublicKey,
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
                        content: "Unable to delete selected source user",
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
            url: '/Extender/Ext00002/SaveSourceUser?KaizenPublicKey=' + sessionStorage.PublicKey,
            method: "POST",
            data: JSON.stringify($scope.Ext00005),
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

                var newObject = jQuery.extend(true, {}, $scope.Ext00005);
                $scope.SourceUsers.push(newObject);
                $scope.Ext00005.UserName = "";
                $scope.Ext00005.IsDefault = false;
                
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
            url: '/Extender/Ext00002/UpdateSourceUser?KaizenPublicKey=' + sessionStorage.PublicKey,
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

    //------------------------------ Source Role Security ---------------------------
    $scope.Ext00004 = {};
    $scope.DBSourceRoleSecurity = function () {
        $scope.LoadExt00002Page('DBSourceRoleSecurity');
        $scope.Clear();
        $scope.Ext00004.Status = 0;
        $scope.LoadSourceList();
        $scope.LoadRoles();
    };

    $scope.SourceIDChangedForRoles = function () {
        if (angular.isDefined($scope.SelectedSource.DataBaseSourceID)) {
            $scope.Ext00004.DataBaseSourceID = $scope.SelectedSource.DataBaseSourceID;
            $scope.LoadComTableNameListForRoles($scope.Ext00004.DataBaseSourceID);
        }
    };

    $scope.LoadComTableNameListForRoles = function (DataBaseSourceID) {
        $http.get('/Extender/Ext00002/GetComTableNameList?KaizenPublicKey=' + sessionStorage.PublicKey,
            { params: { DataBaseSourceID: DataBaseSourceID } }).success(function (data) {
                $scope.ComTableNameList = data;
            }).finally(function () { });
    };

    $scope.ComTableNameChangedForRoles = function () {
        if (angular.isDefined($scope.SelectedSource.ComTableName)) {
            $scope.Ext00004.ComTableName = $scope.SelectedSource.ComTableName;
            $scope.LoadDBSourceFieldsForRoles($scope.Ext00004.DataBaseSourceID, $scope.Ext00004.ComTableName);
        }
    };

    $scope.LoadDBSourceFieldsForRoles = function (DataBaseSourceID,ComTableName) {
        $http.get('/Extender/Ext00002/GetSourceRoles?KaizenPublicKey=' + sessionStorage.PublicKey,
            { params: { DataBaseSourceID: DataBaseSourceID, ComTableName: ComTableName } }).success(function (data) {
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
            });
    };

    $scope.LoadRoles = function () {
        if ($scope.Roles == null || $scope.Roles.length == 0) {
            $http.get('/Sys_role/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
                $scope.Roles = data;
            }).finally(function () { });
        }
    };

    $scope.UpdateSourceRole = function (role) {
        if (!$scope.Ext00004 || !$scope.Ext00004.DataBaseSourceID) {
            $.bigBox({
                title: "Error",
                content: "Please select database source id",
                color: "#C46A69",
                timeout: 8000,
                icon: "fa fa-bolt swing animated"
            });
            role.isSelected = !role.isSelected;
            return;
        }
        if (role) {
            $scope.Ext00004.RoleID = role.RoleID;

            var urlToUpdate = "";

            if (role.isSelected) {
                urlToUpdate = "/Extender/Ext00002/SaveSourceRole?KaizenPublicKey=" + sessionStorage.PublicKey;
            } else {
                urlToUpdate = "/Extender/Ext00002/DeleteSourceRole?KaizenPublicKey=" + sessionStorage.PublicKey;
            }

            $http({
                url: urlToUpdate,
                method: "POST",
                data: JSON.stringify($scope.Ext00004),
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
    $scope.Ext00003 = {};
    $scope.SelectedSourceField = {};
    $scope.DBSourceField = function () {
        $scope.LoadExt00002Page('DBSourceField');
        $scope.Clear();
        $scope.Ext00003.Status = 0;
        $scope.LoadSourceList();
        $scope.LoadSourceFieldList();
    };

    $scope.LoadSourceFieldList = function () {
        $http.get('/Extender/Ext00002/GetSourceFields?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.FieldTypes = data;
        }).finally(function () { });
    };

    $scope.LoadComTableNameList = function (DataBaseSourceID) {
        $http.get('/Extender/Ext00002/GetComTableNameList?KaizenPublicKey=' + sessionStorage.PublicKey,
            { params: { DataBaseSourceID: DataBaseSourceID } }).success(function (data) {
                $scope.ComTableNameList = data;

                $scope.LoadDBSourceFieldsByID($scope.Ext00003.DataBaseSourceID);

            }).finally(function () { });
    };

    $scope.SourceIDChangedForField = function () {
        if (angular.isDefined($scope.SelectedSource.DataBaseSourceID)) {
            $scope.Ext00003.DataBaseSourceID = $scope.SelectedSource.DataBaseSourceID;
            $scope.LoadComTableNameList($scope.Ext00003.DataBaseSourceID);
        }
    };

    $scope.SourceFieldTypeChanged = function () {
        if (angular.isDefined($scope.SelectedSource.FieldTypeID)) {
            $scope.Ext00003.FieldTypeID = $scope.SelectedSource.FieldTypeID;
        }
    };

    $scope.ComTableNameChanged = function () {
        if (angular.isDefined($scope.SelectedSource.ComTableName)) {
            $scope.Ext00003.ComTableName = $scope.SelectedSource.ComTableName;
            $scope.LoadDBSourceFields($scope.Ext00003.DataBaseSourceID,$scope.Ext00003.ComTableName);
        }
    };

    $scope.LoadDBSourceFieldsByID = function (DataBaseSourceID) {
        $http.get('/Extender/Ext00002/GetExt00003ByID?KaizenPublicKey=' + sessionStorage.PublicKey,
            { params: { DataBaseSourceID: DataBaseSourceID } }).success(function (data) {
                if (data.length >= 0) {
                    if (data.length > 0)
                        $scope.SourceFieldList = data;
                    else
                        $scope.SourceFieldList = [];
                }
            });
    };

    $scope.LoadDBSourceFields = function (DataBaseSourceID,ComTableName) {
        $http.get('/Extender/Ext00002/GetExt00003?KaizenPublicKey=' + sessionStorage.PublicKey,
            { params: { DataBaseSourceID: DataBaseSourceID, ComTableName: ComTableName } }).success(function (data) {
                if (data.length >= 0) {
                    if (data.length > 0)
                        $scope.SourceFieldList = data;
                    else
                        $scope.SourceFieldList = [];
                }
            });
    };

    $scope.AddSourceField = function () {
        $scope.Ext00003.Status = 1;
        if ($scope.Ext00003) {
            $http({
                url: '/Extender/Ext00002/SaveSourceField?KaizenPublicKey=' + sessionStorage.PublicKey,
                method: "POST",
                data: JSON.stringify($scope.Ext00003),
                dataType: "json",
                headers: {
                    'Content-Type': 'application/json; charset=utf-8'
                }
            }).success(function (data) {
                if (data.Status == true) {
                    //$scope.CaseTypeEquationFieldsList.push($scope.Ext00003);
                    $scope.Ext00003 = {};
                    $scope.Ext00003.DataBaseSourceID = $scope.SelectedSource.DataBaseSourceID;
                    $scope.Ext00003.ComTableName = $scope.SelectedSource.ComTableName;
                    $scope.Ext00003.FieldTypeID = $scope.SelectedSource.FieldTypeID;
                    $scope.Ext00003.Status = 0;

                    $.bigBox({
                        title: data.Massage,
                        content: data.Description,
                        color: "#739E73",
                        timeout: 8000,
                        icon: "fa fa-bolt swing animated"
                    });

                    $scope.LoadDBSourceFields($scope.Ext00003.DataBaseSourceID,$scope.Ext00003.ComTableName);

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
                    content: "Unable to save database source field data",
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
        $scope.Ext00003 = obj_extend;
        //var Obj_indx = $scope.functiontofindIndexByKeyValue($scope.SourceFieldList, "SourceID", $scope.Ext00003.DataBaseSourceID);
        //var obj = $scope.SourceFieldList[Obj_indx];
        $scope.SelectedSource.DataBaseSourceID = obj_extend.DataBaseSourceID;
        $scope.SelectedSource.ComTableName = obj_extend.ComTableName;
        $scope.SelectedSource.FieldTypeID = obj_extend.FieldTypeID;
        //if ($scope.CM00080.status == 0 || angular.isUndefined($scope.CM00080.status)) {
        $scope.Ext00003.Status = 2;
    };

    $scope.UpdateSourceField = function () {
        if ($scope.Ext00003.Status == 0 || angular.isUndefined($scope.Ext00003.Status)) {
            $scope.Ext00003.Status = 2;
        }
        $scope.SourceFieldList.splice(indx, 1, $scope.Ext00003);

        if ($scope.Ext00003) {
            //if ($scope.SelectedFieldType && $scope.SelectedFieldType.FieldTypeID)
            //    $scope.Ext00003.FieldTypeID = $scope.SelectedFieldType.FieldTypeID;

            $http({
                url: '/Extender/Ext00002/UpdateSourceField?KaizenPublicKey=' + sessionStorage.PublicKey,
                method: "POST",
                data: $scope.Ext00003,
                dataType: "json",
                headers: {
                    'Content-Type': 'application/json; charset=utf-8'
                }
            }).success(function (data) {
                if (data.Status == true) {
                    $scope.Ext00003 = {};
                    $scope.Ext00003.Status = 0;

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
                    url: '/Extender/Ext00002/DeleteSourceField?KaizenPublicKey=' + sessionStorage.PublicKey,
                    method: "POST",
                    data: JSON.stringify(sourceField),
                    dataType: "json",
                    headers: {
                        'Content-Type': 'application/json; charset=utf-8'
                    }
                }).success(function (data) {
                    if (data.Status == true) {
                            $scope.SourceFieldList.splice(index, 1);
                            sourceField.Status = 3;
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
        $scope.Ext00003.Status = 1;
        $scope.Ext00003.FieldName = "";
        $scope.Ext00003.FieldDisplay = "";
        $scope.Ext00003.FieldOrder = 0;
        $scope.Ext00003.FieldWidth = 0;
        $scope.Ext00003.IsColumn01 = false;
        $scope.Ext00003.IsRequired = false;
    };

});