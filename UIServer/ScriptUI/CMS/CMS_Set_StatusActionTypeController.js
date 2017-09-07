app.controller('CMS_Set_StatusActionTypeController', function ($scope, $http) {
    $scope.CM00003 = {};
    $scope.PagesCM00003 = [];
    $scope.toolbarOptions = {
        items: [
                 {
                     type: "button", text: "<span class=\"fa fa-plus-circle\"></span>  Set CMS Group ",
                     attributes: { "class": "btn-primary" },
                     click: function (e) {
                         $scope.$apply(function () {
                             $scope.AddCM00003();
                         });
                     }
                 },
                 {
                     type: "splitButton",
                     text: "Configuration",
                     menuButtons: [
                            {
                                text: "Status Group Role", click: function (e) {
                                    $scope.$apply(function () {
                                        $scope.StatusGroupRole();
                                    });
                                }
                            },
                              {
                                  text: "Status Group User", click: function (e) {
                                      $scope.$apply(function () {
                                          $scope.StatusGroupUser();
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
                 { type: "button", spriteCssClass: "k-tool-icon k-i-refresh" },
                 {
                     type: "button", text: " Filter", spriteCssClass: "k-tool-icon k-filter",
                     click: function (e) { $scope.openFilterWindow(); }
                 }
        ]
, resizable: true
    };
    $scope.LoadCM00003 = function (StatusActionTypeID) {
        if (angular.isUndefined($scope.CM00003.StatusActionTypeID)) {
            $http.get('/CMS_Set_StatusActionType/GetSingle?KaizenPublicKey=' + sessionStorage.PublicKey + "&StatusActionTypeID=" + StatusActionTypeID).success(function (data) {
                $scope.CM00003 = data;
            }).finally(function () { $scope.CM00003.Status = 2; });

        }
    };
    $scope.LoadCM00003Page = function (ActionName) {
        removeEntityPage($scope.PagesCM00003);
        var URIPath = "/CMS/" + $scope.ToolBar.ActiveScreen.ID + "/" + ActionName + "?KaizenPublicKey=" + sessionStorage.PublicKey;
        var Page = { url: URIPath, ActionName: ActionName };
        $scope.PagesCM00003.push(Page);
    }

    $scope.AddCM00003 = function () {
        $scope.LoadCM00003Page('Create');
        $scope.Clear();
        $scope.CM00003.Status = 1;
    }
    $scope.EditCM00003 = function (StatusActionTypeID) {
        $scope.LoadCM00003Page('Create');
        $scope.Clear();
        $scope.LoadCM00003(StatusActionTypeID);
    };
    $scope.SaveData = function () {
        $http.post('/CMS_Set_StatusActionType/SaveData', { 'CM00003': $scope.CM00003, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
            if (data.Status == true) {
                $.bigBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#739E73",
                    timeout: 3000,
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
    $scope.UpdateData = function () {
        $http.post('/CMS_Set_StatusActionType/UpdateData', { 'CM00003': $scope.CM00003, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
            if (data.Status == true) {
                $.bigBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#739E73",
                    timeout: 3000,
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
    $scope.Clear = function () {
        $scope.CM00003 = { Status: 1 };
        $scope.Roles = [];

        $scope.CM00053 = {};
        $scope.StatusGroupUsers = [];
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
                $http.post('/CMS_Set_StatusActionType/DeleteData', { 'CM00003': $scope.CM00003, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
                    if (data.Status == true) {
                        $.bigBox({
                            title: data.Massage,
                            content: data.Description,
                            color: "#739E73",
                            timeout: 3000,
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
        $scope.PagesCM00003 = [];
    };

    //--------------------- Status Group Role -------------------------------
    $scope.StatusGroupRole = function () {
        $scope.Roles = [];
        $scope.LoadCM00003Page('StatusGroupRole');
        $scope.Clear();

        $http.get('/CMS_Set_StatusActionType/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.StatusGroupActionTypes = data;
        }).finally(function () {
            $scope.StatusGroupHasOptions = {
                filter: "startswith",
                model: "SelectedStatusGroup",
                optionLabel: "-- Select Status Action Type --",
                dataTextField: "StatusActionTypeName",
                dataValueField: "StatusActionTypeID",
                dataSource: $scope.StatusGroupActionTypes,
                value: $scope.CM00003.StatusActionTypeID
            };
        });

        if ($scope.Roles == null || $scope.Roles.length == 0) {
            $http.get('/Sys_role/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
                $scope.Roles = data;
            }).finally(function () { });
        }
    };

    $scope.StatusGroupChangedForRoles = function () {
        if (angular.isDefined($scope.CM00003.SelectedStatusGroup)) {
            $scope.CM00003.StatusActionTypeID = $scope.CM00003.SelectedStatusGroup.StatusActionTypeID;
            $scope.LoadRoles_StatusGroup($scope.CM00003.StatusActionTypeID);
        }
    };

    $scope.LoadRoles_StatusGroup = function (StatusActionTypeID) {
        $http.get('/CMS_Set_StatusActionType/GetRolesByStatusActionType?KaizenPublicKey=' + sessionStorage.PublicKey,
            { params: { StatusActionTypeID: StatusActionTypeID } }).success(function (data) {
                if (data.length >= 0) {
                    if (data.length > 0)
                        $scope.StatusGroupRoles = data;
                    else
                        $scope.StatusGroupRoles = [];

                    $scope.Roles.forEach(function (eleRole, index) {
                        eleRole.isSelected = false;
                        $scope.StatusGroupRoles.forEach(function (eleStatusRole, index) {
                            if (eleRole.RoleID === eleStatusRole.RoleID) {
                                eleRole.isSelected = true;
                            }
                        });
                    });
                }
            });
    };

    $scope.UpdateStatusGroupRole = function (role) {
        if (!$scope.CM00003 || !$scope.CM00003.StatusActionTypeID) {
            $.bigBox({
                title: "Error",
                content: "Please select Status Action Type",
                color: "#C46A69",
                timeout: 8000,
                icon: "fa fa-bolt swing animated"
            });
            role.IsSelected = !role.isSelected;
            return;
        }
        if (role) {
            $scope.CM00054 = {};
            $scope.CM00054.RoleID = role.RoleID;
            $scope.CM00054.StatusActionTypeID = $scope.CM00003.StatusActionTypeID;

            var urlToUpdate = "";

            if (role.isSelected) {
                urlToUpdate = "/CMS_Set_StatusActionType/SaveRole?KaizenPublicKey=" + sessionStorage.PublicKey;
            } else {
                urlToUpdate = "/CMS_Set_StatusActionType/DeleteRole?KaizenPublicKey=" + sessionStorage.PublicKey;
            }

            $http({
                url: urlToUpdate,
                method: "POST",
                data: JSON.stringify($scope.CM00054),
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
                    content: "Unable to save role for the selected status",
                    color: "#C46A69",
                    timeout: 8000,
                    icon: "fa fa-bolt swing animated"
                });
            });
        }
    }

    //--------------------- Status Group User -------------------------------
    $scope.CM00053 = {};
    $scope.StatusGroupUsers = [];
    $scope.StatusGroupUser = function () {
        $scope.LoadCM00003Page('StatusGroupUser');
        $scope.Clear();

        $http.get('/CMS_Set_StatusActionType/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.StatusGroupActionTypes = data;
        }).finally(function () {
            $scope.StatusGroupHasOptions = {
                filter: "startswith",
                model: "SelectedStatusGroup",
                optionLabel: "-- Select Status Action Type --",
                dataTextField: "StatusActionTypeName",
                dataValueField: "StatusActionTypeID",
                dataSource: $scope.StatusGroupActionTypes,
                value: $scope.CM00003.StatusActionTypeID
            };
        });
    };

    $scope.UserBack = function (user) {
        if (user != null) {
            switch ($scope.CurrentControl) {
                case 'StatusGroupUsers':
                    $scope.StatusGroupUsers = [];
                    $scope.CM00053.UserName = user.UserName;
                    $http.get('/CMS_Set_StatusActionType/GetStatusGroupByUser?KaizenPublicKey=' + sessionStorage.PublicKey,
                        { params: { userName: user.UserName } }).success(function (data) {
                            if (data.length > 0) {
                                $scope.StatusGroupUsers = data;
                            }
                        });
                    break;
            }
        }
    };

    $scope.StatusGroupChangedForUsers = function () {
        if (angular.isDefined($scope.CM00003.SelectedStatusGroup)) {
            $scope.CM00003.StatusActionTypeID = $scope.CM00003.SelectedStatusGroup.StatusActionTypeID
        }
    };

    $scope.UpdateStatusGroupUser = function () {
        var statusGroupUser = {};
        if (!$scope.CM00053.UserName) {
            $.bigBox({
                title: "Error",
                content: "Username not found",
                color: "#C46A69",
                timeout: 3000,
                icon: "fa fa-bolt swing animated"
            });
            return;
        }
        if (!$scope.CM00003.StatusActionTypeID) {
            $.bigBox({
                title: "Error",
                content: "Status action type not found",
                color: "#C46A69",
                timeout: 3000,
                icon: "fa fa-bolt swing animated"
            });
            return;
        }

    statusGroupUser.UserName = $scope.CM00053.UserName;
    statusGroupUser.StatusActionTypeID = $scope.CM00003.StatusActionTypeID;

    $scope.StatusGroupUsers.push(statusGroupUser);
    $scope.AddStatusGroupByUser(statusGroupUser);
};

$scope.AddStatusGroupByUser = function (statusGroupUser) {
    $http({
        url: '/CMS_Set_StatusActionType/AddStatusGroupByUser?KaizenPublicKey=' + sessionStorage.PublicKey,
        method: "POST",
        data: JSON.stringify(statusGroupUser),
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
    }).error(function (data, status, headers, config) { alert(); });
}

$scope.RemoveStatusGroupUser = function (statusGroupUser, index) {

    $.SmartMessageBox({
        title: "Delete Record",
        content: "Are you sure?",
        buttons: '[No][Yes]'
    }, function (ButtonPressed) {
        if (ButtonPressed === "Yes") {
            if (statusGroupUser.status == 1)
                $scope.StatusGroupUsers.splice(index, 1);
            else
                statusGroupUser.status = 3;

            $scope.DeleteStatusUser(statusGroupUser);
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

$scope.DeleteStatusGroupUser = function (statusGroupUser) {
    $http({
        url: '/CMS_Set_StatusActionType/DeleteStatusGroupByUser?KaizenPublicKey=' + sessionStorage.PublicKey,
        method: "POST",
        data: JSON.stringify(statusGroupUser),
        dataType: "json",
        headers: {
            'Content-Type': 'application/json; charset=utf-8'
        }
    }).success(function (data) {
        if (data.Status == true) {
            $.bigBox({
                title: data.Massage,
                content: data.Description,
                color: "#C46A69",
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
        }
    }).error(function (data, status, headers, config) { alert(); });
};
});