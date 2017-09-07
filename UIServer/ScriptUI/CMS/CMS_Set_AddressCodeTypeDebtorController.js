app.controller('CMS_Set_AddressCodeTypeDebtorController', function ($scope, $http) {
    $scope.CM00008 = {};
    $scope.CM00009 = {};
    $scope.CM00033 = {};
    $scope.CM00034 = {};

    $scope.PagesCM00009 = [];
    $scope.GridPages = [];
    $scope.LoadCM00009 = function (AddressCode) {
        if (AddressCode == undefined || AddressCode == null || AddressCode==="")
            return;
        var paramObj = {};
        var params = {};
        paramObj.params = params;
        var screenController = "";

        if ($scope.selectedType.value == "Client") {
            $scope.CM00008.Status = 2;
            screenController = "CMS_Set_AddressCodeTypeClient";
            params.AddressCodeType = AddressCode;
        } else if ($scope.selectedType.value == "Partner") {
            $scope.CM00033.Status = 2;
            screenController = "CMS_Set_AddressCodeTypePartner";
            params.AddressCodeType = AddressCode;
        } else if ($scope.selectedType.value == "Legal") {
            $scope.CM00034.Status = 2;
            screenController = "CMS_Set_AddressCodeTypeLegal";
            params.AddressCodeType = AddressCode;
        } else if ($scope.selectedType.value == "Debtor") {
            $scope.CM00009.Status = 2;
            screenController = "CMS_Set_AddressCodeTypeDebtor";
            params.AddressCode = AddressCode;
        } else {
            $scope.CM00009.Status = 2;
            screenController = $scope.ToolBar.ActiveScreen.ID;
            params.AddressCode = AddressCode;
        }

        params.KaizenPublicKey = sessionStorage.PublicKey;

        $http.get('/' + screenController + '/GetSingle', paramObj).success(function (data) {
            data.Status = 2;
            if ($scope.selectedType.value == "Client") {
                $scope.CM00008 = data;
            } else if ($scope.selectedType.value == "Partner") {
                $scope.CM00033 = data;
            } else if ($scope.selectedType.value == "Legal") {
                $scope.CM00034 = data;
            } else if ($scope.selectedType.value == "Debtor") {
                $scope.CM00009 = data;
            } else {
                $scope.CM00009 = data;
            }
        }).finally(function() {
            
        });
    };
    $scope.LoadCM00009Page = function (ActionName) {
        removeEntityPage($scope.PagesCM00009);

        var screenController = "";
        if ($scope.selectedType.value == "Client") {
            screenController = "CMS_Set_AddressCodeTypeClient";
        } else if ($scope.selectedType.value == "Partner") {
            screenController = "CMS_Set_AddressCodeTypePartner";
        } else if ($scope.selectedType.value == "Legal") {
            screenController = "CMS_Set_AddressCodeTypeLegal";
        } else if ($scope.selectedType.value == "Debtor") {
            screenController = "CMS_Set_AddressCodeTypeDebtor";
        } else {
            screenController = $scope.ToolBar.ActiveScreen.ID;
        }

        var URIPath = "/CMS/" + screenController + "/" + ActionName + "?KaizenPublicKey=" + sessionStorage.PublicKey;
        var Page = {
            url: URIPath, ActionName: ActionName
        };
        $scope.PagesCM00009.push(Page);
    };
    $scope.LoadGridPage = function (ActionName) {
        removeEntityPage($scope.GridPages);
        var URIPath = "";

        if (ActionName == "DebtorGrid")
            URIPath = "/CMS/CMS_Set_AddressCodeTypeDebtor/" + ActionName + "?KaizenPublicKey=" + sessionStorage.PublicKey;
        else if (ActionName == "ClientGrid")
            URIPath = "/CMS/CMS_Set_AddressCodeTypeClient/" + ActionName + "?KaizenPublicKey=" + sessionStorage.PublicKey;
        else if (ActionName == "PartnerGrid")
            URIPath = "/CMS/CMS_Set_AddressCodeTypePartner/" + ActionName + "?KaizenPublicKey=" + sessionStorage.PublicKey;
        else if (ActionName == "LegalGrid")
            URIPath = "/CMS/CMS_Set_AddressCodeTypeLegal/" + ActionName + "?KaizenPublicKey=" + sessionStorage.PublicKey;

        var Page = {
            url: URIPath, ActionName: ActionName
        };
        $scope.GridPages.push(Page);
    };
    $scope.toolbarOptions = {
        items: [
                 {
                     type: "button", text: "<span class=\"fa fa-plus-circle\"></span> New Address Type",
                     attributes: { "class": "btn-primary" },
                     click: function (e) {
                         $scope.$apply(function () {
                             $scope.AddCM00009();
                         });
                     }
                 },
                 {
                     type: "splitButton",
                     text: "GO",
                     click: function (e) {
                     },
                     menuButtons: [
                         {
                             type: "button", text: "<span class=\"fa fa-cog\"></span> User PO Defauls Setup", click: function (e) {
                                 $scope.$apply(function () {
                                     $scope.SetupUserDefaultsPO();
                                 });
                             }
                         }
                     ]
                 },
                 {
                     type: "splitButton",
                     text: "Configuration",
                     menuButtons: [
                            {
                                text: "Debtor Address Roles", click: function (e) {
                                    $scope.$apply(function () {
                                        $scope.DebtorAddressRole();
                                    });
                                }
                            },
                              {
                                  text: "Debtor Address Users", click: function (e) {
                                      $scope.$apply(function () {
                                          $scope.DebtorAddressUser();
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
                 },
                 { template: "<label>Address Type:</label>" },
                {
                    template: "<select kendo-drop-down-list k-ng-model='selectedType' k-on-change='Type_Changed()' style=\"width: 200PX;\"><option>Client</option><option>Partner</option><option>Debtor</option><option>Legal</option></select>",
                    overflow: "never"
                }
        ]
, resizable: true
    };
    $scope.LoadLookUp = function () { };
    $scope.LoadLookUp();
    $scope.Type_Changed = function () {
        if ($scope.selectedType.value == "Client") {
            $scope.LoadGridPage('ClientGrid');
        } else if ($scope.selectedType.value == "Partner") {
            $scope.LoadGridPage('PartnerGrid');
        } else if ($scope.selectedType.value == "Legal") {
            $scope.LoadGridPage('LegalGrid');
        } else if ($scope.selectedType.value == "Debtor") {
            $scope.LoadGridPage('DebtorGrid');
        }
    };
    $scope.selectedType = {};
    $scope.selectedType.value = "Debtor";
    $scope.Type_Changed();

    $scope.UserBack = function (user) {
        if (user != null) {
            switch ($scope.CurrentControl) {
                case 'DebtorAddressUsers':
                    $scope.DebtorAddressUsers = [];
                    $scope.CM00031.UserName = user.UserName;
                    $http.get('/CMS_Set_AddressCodeTypeDebtor/GetDebtorAddressByUser?KaizenPublicKey=' + sessionStorage.PublicKey,
                        { params: { userName: user.UserName } }).success(function (data) {
                            if (data.length >= 0) {
                                $scope.DebtorAddressUsers = data;

                                $scope.AddressCodeList.forEach(function (ele, index) {
                                    ele.isSelected = false;
                                    $scope.DebtorAddressUsers.forEach(function (eleselected, index) {
                                        if (ele.AddressCode === eleselected.AddressCode) {
                                            ele.isSelected = true;
                                        }
                                    });
                                });
                            }
                        });
                    break;
                case 'CaseTypeUsers':
                    $scope.CaseTypeUsers = [];
                    $scope.CM00057.UserName = user.UserName;
                    $http.get('/CMS_CaseType/GetCaseTypeByUser?KaizenPublicKey=' + sessionStorage.PublicKey,
                        { params: { userName: user.UserName } }).success(function (data) {
                            if (data.length >= 0) {
                                $scope.CaseTypeUsers = data;

                                $scope.CaseTypes.forEach(function (ele, index) {
                                    ele.isSelected = false;
                                    $scope.CaseTypeUsers.forEach(function (eleselected, index) {
                                        if (ele.TRXTypeID === eleselected.TRXTypeID) {
                                            ele.isSelected = true;
                                        }
                                    });
                                });
                            }
                        });
                    break;
                case 'CaseTypeViewUsers':
                    $scope.CaseTypeViewUsers = [];
                    $scope.CM00073.UserName = user.UserName;
                    $http.get('/CMS_CaseType/GetCaseTypeViewsByUser?KaizenPublicKey=' + sessionStorage.PublicKey,
                        { params: { userName: user.UserName } }).success(function (data) {
                            if (data.length >= 0) {
                                $scope.CaseTypeViewUsers = data;

                                $scope.CaseTypeViewsList.forEach(function (ele, index) {
                                    ele.isSelected = false;
                                    $scope.CaseTypeViewUsers.forEach(function (eleselected, index) {
                                        if (ele.ViewID === eleselected.ViewID) {
                                            ele.isSelected = true;
                                        }
                                    });
                                });
                            }
                        });
                    break;
            }
        }
    };

    $scope.AddCM00009 = function () {
        $scope.LoadCM00009Page('Create');
        $scope.Clear();
        $scope.CM00008.Status = 1;
        $scope.CM00009.Status = 1;
        $scope.CM00033.Status = 1;
        $scope.CM00034.Status = 1;
    };
    $scope.EditAddressType = function (AddressCode) {
        $scope.LoadCM00009Page('Create');
        $scope.Clear();
        $scope.LoadCM00009(AddressCode);
    };
    $scope.SaveData = function () {
        var params = {};

        var controllerNamePath = "", paramName1 = "", paramValue1 = {};
        if ($scope.selectedType.value == "Client") {
            controllerNamePath = "CMS_Set_AddressCodeTypeClient";
            params.CM00008 = $scope.CM00008;
        } else if ($scope.selectedType.value == "Partner") {
            controllerNamePath = "CMS_Set_AddressCodeTypePartner";
            params.CM00033 = $scope.CM00033;
        } else if ($scope.selectedType.value == "Legal") {
            controllerNamePath = "CMS_Set_AddressCodeTypeLegal";
            params.CM00034 = $scope.CM00034;
        }
        else if ($scope.selectedType.value == "Debtor") {
            controllerNamePath = "CMS_Set_AddressCodeTypeDebtor";
            params.CM00009 = $scope.CM00009;
        }

        params.KaizenPublicKey = sessionStorage.PublicKey;

        $http.post('/' + controllerNamePath + '/SaveData', params).success(function (data) {
            if (data.Status == true) {
                $.bigBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#739E73",
                    timeout: 8000,
                    icon: "fa fa-check shake animated"
                });
                $scope.Cancel();
                $scope.GridRefresh_AddressType();
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
        var params = {};

        var controllerNamePath = "", paramName1 = "", paramValue1 = {};
        if ($scope.selectedType.value == "Client") {
            controllerNamePath = "CMS_Set_AddressCodeTypeClient";
            params.CM00008 = $scope.CM00008;
        } else if ($scope.selectedType.value == "Partner") {
            controllerNamePath = "CMS_Set_AddressCodeTypePartner";
            params.CM00033 = $scope.CM00033;
        } else if ($scope.selectedType.value == "Legal") {
            controllerNamePath = "CMS_Set_AddressCodeTypeLegal";
            params.CM00034 = $scope.CM00034;
        }
        else if ($scope.selectedType.value == "Debtor") {
            controllerNamePath = "CMS_Set_AddressCodeTypeDebtor";
            params.CM00009 = $scope.CM00009;
        }

        params.KaizenPublicKey = sessionStorage.PublicKey;

        $http.post('/' + controllerNamePath + '/UpdateData', params).success(function (data) {
            if (data.Status == true) {
                $.bigBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#739E73",
                    timeout: 8000,
                    icon: "fa fa-check shake animated"
                });
                $scope.Cancel();
                $scope.GridRefresh_AddressType();
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
        $scope.CM00008 = {};
        $scope.CM00009 = {};
        $scope.CM00033 = {};
        $scope.CM00034 = {};
        $scope.AddressCodeRoles = [];
        $scope.Roles = [];
        $scope.CM00031 = {};
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
                var params = {};

                var controllerNamePath = "", paramName1 = "", paramValue1 = {};
                if ($scope.selectedType.value == "Client") {
                    controllerNamePath = "CMS_Set_AddressCodeTypeClient";
                    params.CM00008 = $scope.CM00008;
                } else if ($scope.selectedType.value == "Partner") {
                    controllerNamePath = "CMS_Set_AddressCodeTypePartner";
                    params.CM00033 = $scope.CM00033;
                } else if ($scope.selectedType.value == "Legal") {
                    controllerNamePath = "CMS_Set_AddressCodeTypeLegal";
                    params.CM00034 = $scope.CM00034;
                }
                else if ($scope.selectedType.value == "Debtor") {
                    controllerNamePath = "CMS_Set_AddressCodeTypeDebtor";
                    params.CM00009 = $scope.CM00009;
                }

                params.KaizenPublicKey = sessionStorage.PublicKey;

                $http.post('/' + controllerNamePath + '/DeleteData', params).success(function (data) {
                    if (data.Status == true) {
                        $.bigBox({
                            title: data.Massage,
                            content: data.Description,
                            color: "#739E73",
                            timeout: 8000,
                            icon: "fa fa-check shake animated"
                        });
                        $scope.Cancel();
                        $scope.GridRefresh_AddressType();
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
        $scope.PagesCM00009 = [];
    };

    //--------------------- Debtor Address Role -------------------------------
    $scope.CM00032 = {};
    $scope.DebtorAddressRole = function () {
        $scope.LoadCM00009Page('DebtorAddressRole');
        $scope.Clear();
        $scope.LoadAddressCodes();
        $scope.CM00032.status = 0;

        if ($scope.Roles == null || $scope.Roles.length == 0) {
            $http.get('/Sys_role/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
                $scope.Roles = data;
            }).finally(function () { });
        }
    }

    $scope.LoadAddressCodes = function () {
        $scope.AddressCodeList = [];
        $http.get('/CMS_Set_AddressCodeTypeDebtor/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey)
            .success(function (data) {
                $scope.AddressCodeList = data;
            }).finally(function () {
                $scope.AddressHasOptions = {
                    filter: "startswith",
                    model: "CM00009.SelectedDebtorAddress",
                    optionLabel: "-- Select Address --",
                    dataTextField: "AddressName",
                    dataValueField: "AddressCode",
                    dataSource: $scope.AddressCodeList,
                    value: $scope.CM00009.AddressCode
                };
            });
    };

    $scope.AddressChangedForRoles = function () {
        if (angular.isDefined($scope.CM00009.SelectedDebtorAddress)) {
            $scope.CM00009.AddressCode = $scope.CM00009.SelectedDebtorAddress.AddressCode;
            $scope.LoadRoles_ByAddressCode($scope.CM00009.AddressCode);
        }
    };

    $scope.LoadRoles_ByAddressCode = function (AddressCode) {
        $http.get('/CMS_Set_AddressCodeTypeDebtor/GetRolesByAddressCode?KaizenPublicKey=' + sessionStorage.PublicKey,
            { params: { AddressCode: AddressCode } }).success(function (data) {
                if (data.length >= 0) {
                    if (data.length > 0)
                        $scope.AddressCodeRoles = data;
                    else
                        $scope.AddressCodeRoles = [];

                    $scope.Roles.forEach(function (eleRole, index) {
                        eleRole.isSelected = false;
                        $scope.AddressCodeRoles.forEach(function (eleViewRole, index) {
                            if (eleRole.RoleID === eleViewRole.RoleID) {
                                eleRole.isSelected = true;
                            }
                        });
                    });
                }
            });
    };

    $scope.UpdateAddressRole = function (role) {
        if (!$scope.CM00009 || !$scope.CM00009.AddressCode) {
            $.bigBox({
                title: "Error",
                content: "Please select address code",
                color: "#C46A69",
                timeout: 8000,
                icon: "fa fa-bolt swing animated"
            });
            role.isSelected = !role.isSelected;
            return;
        }
        if (role) {
            $scope.CM00032 = {};
            $scope.CM00032.RoleID = role.RoleID;
            $scope.CM00032.AddressCode = $scope.CM00009.AddressCode;

            var urlToUpdate = "";

            if (role.isSelected) {
                urlToUpdate = "/CMS_Set_AddressCodeTypeDebtor/SaveAddressRole?KaizenPublicKey=" + sessionStorage.PublicKey;
            } else {
                urlToUpdate = "/CMS_Set_AddressCodeTypeDebtor/DeleteAddressRole?KaizenPublicKey=" + sessionStorage.PublicKey;
            }

            $http({
                url: urlToUpdate,
                method: "POST",
                data: JSON.stringify($scope.CM00032),
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
                    content: "Unable to save role for the selected address",
                    color: "#C46A69",
                    timeout: 8000,
                    icon: "fa fa-bolt swing animated"
                });
            });
        }
    };

    //--------------------- Debtor Address User -------------------------------
    $scope.CM00031 = {};
    $scope.DebtorAddressUser = function () {
        $scope.LoadCM00009Page('DebtorAddressUser');
        $scope.Clear();
        $scope.LoadAddressCodes();
        $scope.CM00031.status = 0;

    };

    $scope.UpdateDebtorAddressUser = function (address) {
        if (!$scope.CM00031 || !$scope.CM00031.UserName) {
            $.bigBox({
                title: "Error",
                content: "Please select user name",
                color: "#C46A69",
                timeout: 8000,
                icon: "fa fa-bolt swing animated"
            });
            address.isSelected = !address.isSelected;
            return;
        }
        if (address) {
            var addressUser = {};
            addressUser.UserName = $scope.CM00031.UserName;
            addressUser.AddressCode = address.AddressCode;

            if (address.isSelected) {
                $scope.AddDebtorAddressByUser(addressUser);
            } else {
                $scope.DeleteDebtorAddressUser(addressUser);
            }
        }
    };
    $scope.AddDebtorAddressByUser = function (addressUser) {
        $http({
            url: '/CMS_Set_AddressCodeTypeDebtor/AddDebtorAddressByUser?KaizenPublicKey=' + sessionStorage.PublicKey,
            method: "POST",
            data: JSON.stringify(addressUser),
            dataType: "json",
            headers: {
                'Content-Type': 'application/json; charset=utf-8'
            }
        }).success(function (data) {
            if (data.Status == true) {
                //$scope.CM00073 = {};
                $scope.CM00031.status = 0;
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
                content: "Unable to save debtor address user",
                color: "#C46A69",
                timeout: 8000,
                icon: "fa fa-bolt swing animated"
            });
        });
    };

    $scope.DeleteDebtorAddressUser = function (addressUser) {
        $http({
            url: '/CMS_Set_AddressCodeTypeDebtor/DeleteDebtorAddressUser?KaizenPublicKey=' + sessionStorage.PublicKey,
            method: "POST",
            data: JSON.stringify(addressUser),
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
        }).error(function (data, status, headers, config) {
            $.bigBox({
                title: "Error",
                content: "Unable to delete debtor address user",
                color: "#C46A69",
                timeout: 8000,
                icon: "fa fa-bolt swing animated"
            });
        });
    };
    $scope.GridRefresh_AddressType = function () {
        if ($scope.selectedType.value == "Client") {
            $scope.GridRefresh("GridCMS_Set_AddressCodeTypeClient");
        } else if ($scope.selectedType.value == "Partner") {
            $scope.GridRefresh("GridCMS_Set_AddressCodeTypePartner");
        } else if ($scope.selectedType.value == "Legal") {
            $scope.GridRefresh("GridCMS_Set_AddressCodeTypeLegal");
        } else if ($scope.selectedType.value == "Debtor") {
            $scope.GridRefresh("GridCMS_Set_AddressCodeTypeDebtor");
        }
    };
});