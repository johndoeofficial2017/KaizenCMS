app.controller('Set_AccountingController', function ($scope, $http) {
    $scope.CM00089 = {};
    $scope.CM00090 = {};
    $scope.CM00091 = {};
    $scope.PagesCM00089 = [];
    $scope.CM00089List = [];
    $scope.SelectedLookUp = {};
    $scope.toolbarOptions = {
        items: [
                 {
                     type: "button", text: "<span class=\"fa fa-plus-circle\"></span> Users ",
                     attributes: { "class": "btn-primary" },
                     click: function (e) {
                         $scope.$apply(function () {
                             $scope.AccountUser();
                         });
                     }
                 },
                 {
                     type: "button", text: "<span class=\"fa fa-plus-circle\"></span> Roles ",
                     attributes: { "class": "btn-primary" },
                     click: function (e) {
                         $scope.$apply(function () {
                             $scope.AccountRole();
                         });
                     }
                 }
        ]
 , resizable: true
    };
    $scope.Users = [];
    $scope.Roles = [];

    $scope.LoadCM00089Page = function (ActionName) {
        removeEntityPage($scope.PagesCM00089);
        var URIPath = "/CMS/" + $scope.ToolBar.ActiveScreen.ID + "/" + ActionName + "?KaizenPublicKey=" + sessionStorage.PublicKey;
        var Page = { url: URIPath, ActionName: ActionName };
        $scope.PagesCM00089.push(Page);
    }

    $scope.AccountUser = function () {
        $scope.LoadCM00089Page('AccountUser');
        $scope.Clear();
        $scope.LoadAllUsers();
        $scope.LoadAllCM00089();
    };
        
    $scope.LoadAllUsers = function () {
        $http.get('/SysUser/GetAllUsers?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            if (data) {
                $scope.Users = data;
            }
        });
    };
       
    $scope.CheckbookBack = function (checkbook) {
        if (checkbook != null) {
            if ($scope.CurrentControl == "Accounting") {
                $scope.CM00089.CheckbookCode = checkbook.CheckbookCode;
                $scope.CM00089.CurrencyCode = checkbook.CurrencyCode;
                $scope.CM00089.CheckbookName = checkbook.CheckbookName;

                $scope.GetSingleCM00089($scope.CM00089.CheckbookCode, $scope.CM00089.CurrencyCode, $scope.CM00089.CheckbookName);
            }
            else if ($scope.CurrentControl == "AccountUser") {
                $scope.CM00090.CheckbookCode = checkbook.CheckbookCode;
                $scope.CM00090.CurrencyCode = checkbook.CurrencyCode;

                $scope.LoadCM00090ByCheckbook($scope.CM00090.CheckbookCode, $scope.CM00090.CurrencyCode);
            }
            else if ($scope.CurrentControl == "AccountRole") {
                $scope.CM00091.CheckbookCode = checkbook.CheckbookCode;
                $scope.CM00091.CurrencyCode = checkbook.CurrencyCode;

                $scope.LoadCM00091ByCheckbook($scope.CM00091.CheckbookCode, $scope.CM00091.CurrencyCode);
            }
        }
    };

    $scope.GetCM00090ByUser = function (UserName) {
        $http.get('/Set_Accounting/GetCM00090ByUser?KaizenPublicKey=' + sessionStorage.PublicKey,
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

    $scope.UpdateAccountUser = function (user) {
        if (!$scope.CM00090.UserName) {
            $.bigBox({
                title: "Error",
                content: "UserName is missing",
                color: "#C46A69",
                timeout: 8000,
                icon: "fa fa-bolt swing animated"
            });
            user.isSelected = !user.isSelected;
            return;
        }

        if (user) {
            $scope.CM00090.CheckbookCode = user.CheckbookCode;
            $scope.CM00090.CurrencyCode = user.CurrencyCode;

            var urlToUpdate = "";

            if (user.isSelected) {
                urlToUpdate = "/Set_Accounting/Saveuser?KaizenPublicKey=" + sessionStorage.PublicKey;
            } else {
                urlToUpdate = "/Set_Accounting/Deleteuser?KaizenPublicKey=" + sessionStorage.PublicKey;
            }

            $http({
                url: urlToUpdate,
                method: "POST",
                data: JSON.stringify($scope.CM00090),
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
                    content: "Unable to save user",
                    color: "#C46A69",
                    timeout: 8000,
                    icon: "fa fa-bolt swing animated"
                });
            });
        }
    };

    //--------------- Account Role
    $scope.AccountRole = function () {
        $scope.LoadCM00089Page('AccountRole');
        $scope.Clear();
        $scope.LoadAllRoles();
        $scope.LoadAllCM00089();
    };

    $scope.LoadAllRoles = function () {
        $http.get('/Sys_role/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.Roles = data;
        }).finally(function () { });
    };

    $scope.LoadAllCM00089 = function () {
        $http.get('/Set_Accounting/GetAll?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.CM00089List = data;
        }).finally(function () { });
    };

    $scope.GetCM00091ByRole = function (RoleID) {
        $http.get('/Set_Accounting/GetCM00091ByRole?KaizenPublicKey=' + sessionStorage.PublicKey,
            { params: { RoleID: RoleID } }).success(function (data) {
                if (data.length >= 0) {
                    if (data.length > 0)
                        $scope.AccountRoles = data;
                    else
                        $scope.AccountRoles = [];

                    $scope.CM00089List.forEach(function (eleRole, index) {
                        eleRole.isSelected = false;
                        $scope.AccountRoles.forEach(function (eleAccountRole, index) {
                            if (eleRole.CheckbookCode === eleAccountRole.CheckbookCode && 
                                eleRole.CurrencyCode === eleAccountRole.CurrencyCode) {
                                eleRole.isSelected = true;
                            }
                        });
                    });
                }
            });
    };

    $scope.AccountRoleChanged = function () {
        if (angular.isDefined($scope.SelectedLookUp.SelectedRole)) {
            $scope.CM00091.RoleID = $scope.SelectedLookUp.SelectedRole;
            $scope.GetCM00091ByRole($scope.CM00091.RoleID);
        }
    };

    $scope.UpdateAccountRole = function (role) {
        if (!$scope.CM00091.RoleID) {
            $.bigBox({
                title: "Error",
                content: "Role is missing",
                color: "#C46A69",
                timeout: 8000,
                icon: "fa fa-bolt swing animated"
            });
            role.isSelected = !role.isSelected;
            return;
        }

        if (role) {
            $scope.CM00091.CheckbookCode = role.CheckbookCode;
            $scope.CM00091.CurrencyCode = role.CurrencyCode;

            var urlToUpdate = "";

            if (role.isSelected) {
                urlToUpdate = "/Set_Accounting/SaveRole?KaizenPublicKey=" + sessionStorage.PublicKey;
            } else {
                urlToUpdate = "/Set_Accounting/DeleteRole?KaizenPublicKey=" + sessionStorage.PublicKey;
            }

            $http({
                url: urlToUpdate,
                method: "POST",
                data: JSON.stringify($scope.CM00091),
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
                    content: "Unable to save role",
                    color: "#C46A69",
                    timeout: 8000,
                    icon: "fa fa-bolt swing animated"
                });
            });
        }
    };

    //----------------------------------------------

    $scope.AgentBack = function (agent) {
        if (agent != null) {
            switch ($scope.CurrentControl) {
                case 'AgentCheckbook':
                    $scope.CM00089.ToAgentID = agent.AgentID;
                    break;
            }
        }
    };
    $scope.UserBack = function (user) {
        if (user != null) {
            switch ($scope.CurrentControl) {
                case 'AccountUser':
                    $scope.CM00090.UserName = user.UserName;
                    $scope.GetCM00090ByUser($scope.CM00090.UserName);
                    break;
            }
        }
    };

    $scope.GetSingleCM00089 = function (CheckbookCode, CurrencyCode, CheckbookName) {
        $http.get('/Set_Accounting/GetSingle?KaizenPublicKey=' + sessionStorage.PublicKey,
            { params: { CheckbookCode: CheckbookCode, CurrencyCode: CurrencyCode, CheckbookName: CheckbookName } }).success(function (data) {
                if (data) {
                    $scope.CM00089.ToAgent = false;
                    $scope.CM00089 = data;
                    $scope.CM00089.Status = 2;

                    $scope.SelectedLookUp.SelectedPaymentType = $scope.CM00089.TrxPaymentType;
                }
                else {
                    $scope.CM00089.ToAgent = false;
                    $scope.CM00089.Status = 1;
                    $scope.SelectedLookUp.SelectedPaymentType = 0;
                }
            });
    };

    $scope.SaveData = function () {
        $http.post('/Set_Accounting/SaveData', { 'CM00089': $scope.CM00089, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
            if (data.Status == true) {
                $.bigBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#739E73",
                    timeout: 4000,
                    icon: "fa fa-check shake animated"
                });

                $scope.CM00089.Status = 2;
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
        $http.post('/Set_Accounting/UpdateData', { 'CM00089': $scope.CM00089, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
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
    $scope.DeleteData = function () {
        if (!$scope.CM00089.CheckbookCode) {
            $.bigBox({
                title: "Error",
                content: "Select Checkbook Code",
                color: "#C46A69",
                timeout: 4000,
                icon: "fa fa-bolt swing animated"
            });
            return;
        }
        else if (!$scope.CM00089.CurrencyCode) {
            $.bigBox({
                title: "Error",
                content: "Select Currency Code",
                color: "#C46A69",
                timeout: 4000,
                icon: "fa fa-bolt swing animated"
            });
            return;
        }
        else if (!$scope.CM00089.CheckbookName) {
            $.bigBox({
                title: "Error",
                content: "Select Checkbook Name",
                color: "#C46A69",
                timeout: 4000,
                icon: "fa fa-bolt swing animated"
            });
            return;
        }
        $.SmartMessageBox({
            title: "Delete Record",
            content: "Are you sure you want to delete this?",
            buttons: '[No][Yes]'
        }, function (ButtonPressed) {
            if (ButtonPressed === "Yes") {
                $http.post('/Set_Accounting/DeleteData', { 'CM00089': $scope.CM00089, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
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

    $scope.Cancel = function () {
        $scope.Clear();
        $scope.PagesCM00089 = [];
    };

    $scope.Clear = function () {
        $scope.StandardFieldsList = [];
        $scope.CM00090 = {};
        $scope.CM00091 = {};
        $scope.SelectedLookUp = {};
    };

    $scope.PaymentTypes = [];
    $scope.LoadPaymentTypes = function () {
        $http.get('/Set_Accounting/GetPaymentTypes?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            if (data.length >= 0) {
                if (data.length > 0)
                    $scope.PaymentTypes = data;
                else
                    $scope.PaymentTypes = [];
            }
        });
    };
    $scope.PaymentTypeChangedForAccounting = function () {
        if (angular.isDefined($scope.SelectedLookUp.SelectedPaymentType)) {
            $scope.CM00089.TrxPaymentType = $scope.SelectedLookUp.SelectedPaymentType;
        }
    };

    $scope.LoadPaymentTypes();
});