app.controller('CMS_Set_AddressCodeTypeLegalController', function ($scope, $http) {
    $scope.CM00008 = {};
    $scope.CM00009 = {};
    $scope.CM00033 = {};
    $scope.CM00034 = {};

    $scope.PagesCM00034 = [];
    $scope.GridPages = [];

    $scope.LoadCM00034 = function (AddressCode) {
        if (AddressCode == undefined || AddressCode == null || AddressCode === "")
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
            $scope.CM00034.Status = 2;
            screenController = $scope.ToolBar.ActiveScreen.ID;
            params.AddressCodeType = AddressCode;
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
                $scope.CM00034 = data;
            }
        }).finally(function () {

        });
    };
    $scope.LoadCM00034Page = function (ActionName) {
        removeEntityPage($scope.PagesCM00034);

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

        $scope.PagesCM00034.push(Page);
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
                             $scope.AddCM00034();
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
                             type: "button", text: "<span class=\"fa fa-cog\"></span> User PO Defaults Setup", click: function (e) {
                                 $scope.$apply(function () {
                                     $scope.SetupUserDefaultsPO();
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
                 {
                     template: "<select kendo-drop-down-list k-ng-model='selectedType' k-on-change='Type_Changed()' style=\"width: 200PX;\"><option>Client</option><option>Partner</option><option>Debtor</option><option>Legal</option></select>",
                     overflow: "never"
                 }
        ]
, resizable: true
    };
    $scope.LoadLookUp = function () {};
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
    $scope.selectedType.value = "Legal";
    $scope.Type_Changed();

    $scope.AddCM00034 = function () {
        $scope.LoadCM00034Page('Create');
        $scope.Clear();
        $scope.CM00008.Status = 1;
        $scope.CM00009.Status = 1;
        $scope.CM00033.Status = 1;
        $scope.CM00034.Status = 1;
    };
    $scope.EditAddressType = function (AddressCodeType) {
        $scope.LoadCM00034Page('Create');
        $scope.Clear();
        $scope.LoadCM00034(AddressCodeType);
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

                $http.post('/'+controllerNamePath+'/DeleteData', params).success(function (data) {
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
        $scope.PagesCM00034 = [];
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