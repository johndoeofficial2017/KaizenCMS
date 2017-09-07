app.controller('CMS_SecurityController', function ($scope, $http) {
    $scope.CM00029 = {};
    $scope.SecurityPages = [];
    $scope.YearCodeList = [];
    $scope.AgentList = [];
    $scope.PeriodList = [];
    $scope.TargetList = [];
    $scope.SelectedLookUp = {};
    $scope.Roles = [];

    $scope.toolbarOptions = {
        items: [
                 {
                     type: "button", text: "<span class=\"fa fa-plus-circle\"></span> Debtor Role ",
                     attributes: { "class": "btn-primary" },
                     click: function (e) {
                         $scope.$apply(function () {
                             $scope.DebtorRole();
                         });
                     }
                 },
                 {
                     type: "button", text: "<span class=\"fa fa-plus-circle\"></span> Debtor User ",
                     attributes: { "class": "btn-primary" },
                     click: function (e) {
                         $scope.$apply(function () {
                             $scope.DebtorUser();
                         });
                     }
                 }
        ]
, resizable: true
    };
    $scope.DebtortoolbarOptions = {
        items: [
                 {
                     type: "button", text: "<span class=\"fa fa-plus-circle\"></span> Commission ",
                     attributes: { "class": "btn-primary" },
                     click: function (e) {
                         $scope.$apply(function () {
                             $scope.DebtorCommissionSecurity();
                         });
                     }
                 },
                 {
                     type: "button", text: "<span class=\"fa fa-plus-circle\"></span> Debtor Role ",
                     attributes: { "class": "btn-primary" },
                     click: function (e) {
                         $scope.$apply(function () {
                             $scope.DebtorRoles();
                         });
                     }
                 }
        ]
, resizable: true
    };

    $scope.LoadPage = function (TRXTypeID) {
        if (angular.isUndefined($scope.CM00029.TRXTypeID)) {
            $http.get('/CMS_Security/GetSingle?KaizenPublicKey=' + sessionStorage.PublicKey + "&TRXTypeID=" + TRXTypeID).success(function (data) {
                $scope.CM00029 = data;
                if ($scope.CM00029.TableSource) {
                    $scope.SelectedLookUp.SelectedTableSource = $scope.CM00029.TableSource;
                }
            }).finally(function () { $scope.CM00029.Status = 2; });
        }
    }
    $scope.LoadSecurityPage = function (ActionName) {
        removeEntityPage($scope.SecurityPages);
        var URIPath = "/CMS/" + $scope.ToolBar.ActiveScreen.ID + "/" + ActionName + "?KaizenPublicKey=" + sessionStorage.PublicKey;
        var Page = { url: URIPath, ActionName: ActionName };
        $scope.SecurityPages.push(Page);
    };

    $scope.DebtorSecurity = function () {
        $scope.LoadSecurityPage('DebtorSecurity');
        $scope.Clear();
    };

    $scope.DebtorCommissionSecurity = function () {
        $scope.LoadSecurityPage('DebtorCommissionSecurity');
        $scope.Clear();
        $scope.LoadAllCM00107();
    };

    $scope.DebtorRoles = function () {
        $scope.LoadSecurityPage('DebtorRoles');
        $scope.Clear();
    };

    $scope.LoadAllCM00107 = function () {
        $http.get('/CMS_Security/GetAllCM00107?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.CM00107List = data;

            $scope.YearCodeList = $scope.CM00107List.map(function (a) { return { YearCode: a.YearCode}; });
            
            $scope.AgentList = $scope.CM00107List.map(function (a) { return { AgentID: a.AgentID}; });
                
            $scope.PeriodList = $scope.CM00107List.map(function (a) { return { PERIODID: a.PERIODID }; });
            
            $scope.TargetList = $scope.CM00107List.map(function (a) { return { TargetID: a.TargetID }; });

            $scope.YearCodeList = $scope.YearCodeList.filter(function (itm, i, a) {
                return i == $scope.YearCodeList.findIndex(x => x.YearCode==itm.YearCode);
            });

            $scope.AgentList = $scope.AgentList.filter(function (itm, i, a) {
                return i == $scope.AgentList.findIndex(x => x.AgentID == itm.AgentID);
            });

            $scope.PeriodList = $scope.PeriodList.filter(function (itm, i, a) {
                return i == $scope.PeriodList.findIndex(x => x.PERIODID == itm.PERIODID);
            });

            $scope.TargetList = $scope.TargetList.filter(function (itm, i, a) {
                return i == $scope.TargetList.findIndex(x => x.TargetID == itm.TargetID);
            });
        }).finally(function () {
            
        });
    };

    $scope.UserBack = function (user) {
        if (user != null) {
            switch ($scope.CurrentControl) {
                case 'DebtorUsers':
                    $scope.CM00118.UserName = user.UserName;
                    $scope.GetCM00118ByUser($scope.CM00118.UserName);
                    break;
            }
        }
    };

    //--------------------- Debtor Role -------------------------------
    $scope.CM00119 = {};
    $scope.DebtorRole = function () {
        $scope.Roles = [];
        $scope.LoadSecurityPage('DebtorRole');
        $scope.Clear();

        $http.get('/CMS_Security/GetAllCM00100?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.DebtorList = data;
        }).finally(function () {
            
        });

        if ($scope.Roles == null || $scope.Roles.length == 0) {
            $http.get('/Sys_role/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
                $scope.Roles = data;
            }).finally(function () { });
        }
    };

    $scope.DebtorIDChangedForRoles = function () {
        //alert($scope.CM00029.SelectedDebtor);
        if (angular.isDefined($scope.SelectedLookUp.SelectedDebtorID)) {
            $scope.CM00119.DebtorID = $scope.SelectedLookUp.SelectedDebtorID;
            $scope.LoadRoles_StatusGroup($scope.CM00119.DebtorID);
        }
    };

    $scope.LoadRoles_StatusGroup = function (TRXTypeID) {
        $http.get('/CMS_Security/GetRolesByDebtor?KaizenPublicKey=' + sessionStorage.PublicKey,
            { params: { TRXTypeID: TRXTypeID } }).success(function (data) {
                if (data.length >= 0) {
                    if (data.length > 0)
                        $scope.DebtorRoles = data;
                    else
                        $scope.DebtorRoles = [];

                    $scope.Roles.forEach(function (eleRole, index) {
                        eleRole.isSelected = false;
                        $scope.DebtorRoles.forEach(function (eleStatusRole, index) {
                            if (eleRole.RoleID === eleStatusRole.RoleID) {
                                eleRole.isSelected = true;
                            }
                        });
                    });
                }
            });
    };

    $scope.UpdateCM00119 = function (role) {
        if (!$scope.CM00119 || !$scope.CM00119.DebtorID) {
            $.bigBox({
                title: "Error",
                content: "Please select debtor",
                color: "#C46A69",
                timeout: 8000,
                icon: "fa fa-bolt swing animated"
            });
            role.isSelected = !role.isSelected;
            return;
        }
        if (role) {
            $scope.CM00119.RoleID = role.RoleID;

            var urlToUpdate = "";

            if (role.isSelected) {
                urlToUpdate = "/CMS_Security/SaveRole?KaizenPublicKey=" + sessionStorage.PublicKey;
            } else {
                urlToUpdate = "/CMS_Security/DeleteRole?KaizenPublicKey=" + sessionStorage.PublicKey;
            }

            $http({
                url: urlToUpdate,
                method: "POST",
                data: JSON.stringify($scope.CM00119),
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

    //--------------------- Debtor User -------------------------------
    $scope.CM00118 = {};
    $scope.DebtorUsers = [];
    $scope.DebtoreUser = function () {
        $scope.LoadCM00029Page('DebtoreUser');
        $scope.Clear();

        $http.get('/CMS_Security/GetAllCM00100?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.DebtorList = data;
        });
    };

    $scope.GetCM00118ByUser = function (UserName) {
        $http.get('/CMS_Security/GetCM00118ByUser?KaizenPublicKey=' + sessionStorage.PublicKey,
            { params: { UserName: UserName } }).success(function (data) {
                if (data.length >= 0) {
                    if (data.length > 0)
                        $scope.AccountUsers = data;
                    else
                        $scope.AccountUsers = [];

                    $scope.CM00089List.forEach(function (eleUser, index) {
                        eleUser.isSelected = false;
                        $scope.AccountUsers.forEach(function (eleAccountUser, index) {
                            if (eleUser.CheckbookCode === eleAccountUser.CheckbookCode &&
                                eleUser.CurrencyCode === eleAccountUser.CurrencyCode) {
                                eleUser.isSelected = true;
                            }
                        });
                    });
                }
            });
    };

    $scope.UpdateDebtorUser = function (debtor) {
        if (!$scope.CM00118 || !$scope.CM00118.UserName) {
            $.bigBox({
                title: "Error",
                content: "Please select user name",
                color: "#C46A69",
                timeout: 8000,
                icon: "fa fa-bolt swing animated"
            });
            debtor.isSelected = !debtor.isSelected;
            return;
        }
        if (debtor) {
            var debtorUser = {};
            debtorUser.UserName = $scope.CM00118.UserName;
            debtorUser.TRXTypeID = debtor.TRXTypeID;

            if (debtor.isSelected) {
                $scope.AddDebtorByUser(debtorUser);
            } else {
                $scope.DebtorUser_Delete(debtorUser);
            }
        }
    };
    $scope.DebtorUser_Delete = function (debtorUser) {
        $http({
            url: '/CMS_Security/DeleteDebtorByUser?KaizenPublicKey=' + sessionStorage.PublicKey,
            method: "POST",
            data: JSON.stringify(debtorUser),
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
    $scope.AddDebtorByUser = function (debtorUser) {
        $http({
            url: '/CMS_Security/AddDebtorByUser?KaizenPublicKey=' + sessionStorage.PublicKey,
            method: "POST",
            data: JSON.stringify(debtorUser),
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

    //--------------- Debtor Security ------------


    $scope.EditCM00029 = function (TRXTypeID) {
        $scope.LoadSecurityPage('Create');
        $scope.LoadPage(TRXTypeID);
        $scope.LoadTableSource();
    };
    $scope.SaveData = function () {
        $http.post('/CMS_Security/SaveData', { 'CM00029': $scope.CM00029, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
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
        $http.post('/CMS_Security/UpdateData', { 'CM00029': $scope.CM00029, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
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
        $scope.CM00029 = {};
        $scope.CM00055 = {};

        $scope.Roles = [];
        $scope.CM00118 = {};
        $scope.DebtorUsers = [];

        $scope.CM00070 = {};
        $scope.CM00071 = {};
        $scope.CM00073 = {};
        $scope.DebtorViewUsers = [];
        $scope.DebtorViewsList = [];
        $scope.DebtorViewsFieldsList = [];

        $scope.DebtorGraphList = [];

        //---- Case Type Graph View
        $scope.FieldsListByView = [];
        $scope.FieldsListByView_GraphSummary = [];
        $scope.FieldSummaryListByView = [];
        $scope.FieldSummaryListByView_GraphSummary = [];

        $scope.FieldListCM00084 = [];
        $scope.FieldListCM00085 = [];
        $scope.FieldListCM00086 = [];

        $scope.GraphCategoryList = [];

        $scope.CM00076 = {};

        $scope.CM00080 = {};
        $scope.CM00081 = {};
        $scope.SelectedLookUp = {};
        $scope.DebtorEquationFieldsList = [];
        $scope.DebtorStandardFieldsList = [];

        //---- Case Type Lookup Field
        $scope.CM00050 = {};
        $scope.DebtorLookupFieldsList = [];

        $scope.DebtorTableList = [];
        $scope.CM00037 = {};
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
                $http.post('/CMS_Security/DeleteData', { 'CM00029': $scope.CM00029, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
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
                    color: "#C46A69",
                    iconSmall: "fa fa-times fa-2x fadeInRight animated",
                    timeout: 4000
                });
            }
        });
    };

    $scope.InsertedProducts = [];
    $scope.UpdatedProducts = [];
    $scope.DeletedProducts = [];

    $scope.Cancel = function () {
        $scope.Clear();
        $scope.SecurityPages = [];

        $scope.InsertedProducts = [];
        $scope.UpdatedProducts = [];
        $scope.DeletedProducts = [];
        $scope.DebtorProducts = [];
    };

    $scope.DebtorHasOptions = {
        filter: "startswith",
        optionLabel: "-- Select Case Type --"
    };

});