app.controller('CMS_ContractController', function ($scope, $http) {
    $scope.CM00200 = {};
    $scope.PagesCM00200 = [];
    $scope.toolbarOptions = {
        items: [
                 {
                     type: "button", text: "<span class=\"fa fa-plus-circle\"></span>  Setup Contract",
                     attributes: { "class": "btn-primary" },
                     click: function (e) {
                         $scope.$apply(function () {
                             $scope.AddCM00200();
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
                 { type: "button", spriteCssClass: "k-tool-icon k-i-refresh" },
                 {
                     type: "button", text: " Filter", spriteCssClass: "k-tool-icon k-filter",
                     click: function (e) { $scope.openFilterWindow(); }
                 }
        ]
, resizable: true
    };
    $scope.LoadCM00200 = function (ContractID) {
        if (angular.isUndefined($scope.CM00200.ContractID)) {
            $http.get('/CMS_Contract/GetSingle?KaizenPublicKey=' + sessionStorage.PublicKey + "&ContractID=" + ContractID).success(function (data) {
                $scope.CM00200 = data;
                if ($scope.CM00200.StartDate != null && $scope.CM00200.StartDate != '') {
                    $scope.CM00200.StartDate = kendoparsetoDate($scope.CM00200.StartDate);
                }
                if ($scope.CM00200.EndDate != null && $scope.CM00200.EndDate != '') {
                    $scope.CM00200.EndDate = kendoparsetoDate($scope.CM00200.EndDate);
                }
                if ($scope.CM00200.LastPaymentDate != null && $scope.CM00200.LastPaymentDate != '') {
                    $scope.CM00200.LastPaymentDate = kendoparsetoDate($scope.CM00200.LastPaymentDate);
                }
            }).finally(function () { $scope.CM00200.Status = 2; });
        }
    };
    $scope.LoadCM00200Page = function (ActionName) {
        removeEntityPage($scope.PagesCM00200);
        var URIPath = "/" + $scope.ToolBar.ActiveScreen.ID + "/" + ActionName + "?KaizenPublicKey=" + sessionStorage.PublicKey;
        var Page = { url: URIPath, ActionName: ActionName };
        $scope.PagesCM00200.push(Page);
    };
    $scope.LoadLookUp = function () {
        $http.get('/CMS_Set_ContractStatus/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.Statuses = data;
        }).finally(function () { });
        $http.get('/CMS_Set_PaymentBase/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.PaymentBases = data;
        }).finally(function () { });
        $http.get('/CMS_Set_BillingFrequency/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.Frequencies = data;
        }).finally(function () { });
    };
    $scope.LoadLookUp();

    $scope.AddCM00200 = function () {
        $scope.LoadCM00200Page('Create')
        $scope.Clear();
        $scope.CM00200.Status = 1;
    };
    $scope.EditCM00200 = function (ContractID) {
        $scope.LoadCM00200Page('Create')
        $scope.Clear();
        $scope.LoadCM00200(ContractID);
    };
    $scope.SaveData = function () {
        if ($scope.CM00200.StartDate != null && $scope.CM00200.StartDate != '') {
            $scope.CM00200.StartDate = kendoparsetoDate($scope.CM00200.StartDate);
        }
        if ($scope.CM00200.EndDate != null && $scope.CM00200.EndDate != '') {
            $scope.CM00200.EndDate = kendoparsetoDate($scope.CM00200.EndDate);
        }
        if ($scope.CM00200.LastPaymentDate != null && $scope.CM00200.LastPaymentDate != '') {
            $scope.CM00200.LastPaymentDate = kendoparsetoDate($scope.CM00200.LastPaymentDate);
        }
        $http.post('/CMS_Contract/SaveData', { 'CM00200': $scope.CM00200, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
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
        $http.post('/CMS_Contract/UpdateData', { 'CM00200': $scope.CM00200, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
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
        $scope.CM00200 = {};
        $scope.CM00020 = {};
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
                $http.post('/CMS_Contract/DeleteData', { 'CM00200': $scope.CM00200, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
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
        $scope.PagesCM00200 = [];

        $scope.InsertedLayers = [];
        $scope.UpdatedLayers = [];
        $scope.DeletedLayers = [];
        $scope.ContractLayers = [];
    };

    $scope.ClientBack = function (client) {
        if (client != null) {
            $scope.CM00200.ClientID = client.ClientID;
            if (client.BillAddressCode != null) {
                $http.get('/CMS_ClientAddressCode/GetSingle?KaizenPublicKey=' + sessionStorage.PublicKey, { params: { ClientID: client.ClientID, AddressCode: client.BillAddressCode } })
                     .success(function (data) {
                         $scope.CM00200.BillAddress = data;
                         $scope.CM00200.BilltoCustomer = data.AddressCode;
                     }).finally(function () { });
            }
        }
    };
    $scope.CurrencyBack = function (currency) {
        if (currency != null) {
            $scope.CM00200.CurrencyCode = currency.CurrencyCode;
            $scope.CM00200.DecimalDigit = currency.DecimalDigit;
            $scope.changeFormat(currency.CurrencyCode, currency.DecimalDigit, 'CMS_Contract-currency');
            var kendoWindow = $("#MainDialog").data("kendoWindow");
            kendoWindow.close();
        }
    };
    $scope.launchlayer = function () {
        var dlg = dialogs.create('/CMS_ContractLayer/PopUp?KaizenPublicKey=' + sessionStorage.PublicKey, 'CMS_ContractLayerPopUpController', $scope.CM00200.CM00020);
        dlg.result.then(function (layers) {
            $scope.CM00200.CM00020 = layers;
        });
    };
    $scope.ContractBack = function (contract) {
        if (contract != null) {
            $scope.CM00200 = contract;
            $scope.LoadContractCMS_ContractLayer(contract.ContractID);
        }
    };


    $scope.Select = function (address) {
        $scope.CM00200.BillAddress = address;
        $scope.CM00200.BilltoCustomer = address.AddressCode;
        $('#AddressesModal').modal('hide');
    };
    $scope.CM00200.fromDateString;
    $scope.CM00200.StartDate = null;
    $scope.CM00200.toDateString;
    $scope.CM00200.EndDate = null;
    $scope.maxDate = new Date();
    $scope.minDate = new Date(1900, 0, 1, 0, 0, 0);
    $scope.fromDateChanged = function () {
        $scope.minDate = new Date($scope.CM00200.fromDateString);
    };
    $scope.toDateChanged = function () {
        $scope.maxDate = new Date($scope.CM00200.toDateString);
    };

    //-------------------------- Contract Layer
    $scope.CM00020 = {}; // -- 0 = original from database ; 1 Inserted New ; 2 Edited;3 = Deleted ; 4 File Chabged
    $scope.ContractLayers = [];
    $scope.ContractLayer = function () {
        $scope.LoadCM00200Page('ContractLayer')
        $scope.Clear();
        $scope.LoadContractCMS_ContractLayer();
    };

    $scope.LoadContractCMS_ContractLayer = function (ContractID) {
        $http.get('/CMS_ContractLayer/GetAll?KaizenPublicKey=' + sessionStorage.PublicKey, { params: { ContractID: ContractID } }).success(function (data) {
            if (data.length > 0) {
                $scope.ContractLayers = data;
            }
        }).finally(function () {
            $scope.CM00020.status = 0;
        });
    };

    $scope.InsertedLayers = [];
    $scope.UpdatedLayers = [];
    $scope.DeletedLayers = [];
    var index;
    $scope.AddNewLayer = function () {
        $scope.CM00020.status = 1;
        $scope.CM00020.ContractID = $scope.CM00200.ContractID;
        $scope.ContractLayers.push($scope.CM00020);
        $scope.CM00020 = {};
        $scope.CM00020.status = 0;
    };
    $scope.UpdateLayer = function () {
        $scope.CM00020.status = 2;
        $scope.ContractLayers.splice(index, 1, $scope.CM00020);
        $scope.CM00020 = {};
        $scope.CM00020.status = 0;
    };
    $scope.EditLayer = function (Layer) {
        $scope.CM00020 = Layer;
        if ($scope.CM00020.status == 0 || angular.isUndefined($scope.CM00020.status)) {
            $scope.CM00020.status = 2;
        }
    }
    $scope.SaveContractLayer = function () {
        waitingDialog.show();
        for (var i = 0; i < $scope.ContractLayers.length; i++) {
            var item = $scope.ContractLayers[i];
            if (item.status == 1) {
                var insert_tmp = {
                    ContractID: $scope.CM00200.ContractID,
                    PaymentLayerID: item.PaymentLayerID,
                    LayerFrom: item.LayerFrom,
                    LayerTo: item.LayerTo,
                    LayerRate: item.LayerRate,
                };
                $scope.InsertedLayers.push(insert_tmp);
            }
            else if (item.status == 2) {
                var update_tmp = {
                    ContractID: $scope.CM00200.ContractID,
                    PaymentLayerID: item.PaymentLayerID,
                    LayerFrom: item.LayerFrom,
                    LayerTo: item.LayerTo,
                    LayerRate: item.LayerRate,
                };
                $scope.UpdatedLayers.push(update_tmp);
            }
            else if (item.status == 3) {
                var delete_tmp = {
                    ContractID: $scope.CM00200.ContractID,
                    PaymentLayerID: item.PaymentLayerID,
                    LayerFrom: item.LayerFrom,
                    LayerTo: item.LayerTo,
                    LayerRate: item.LayerRate,
                };
                $scope.DeletedLayers.push(delete_tmp);
            }
        }

        if ($scope.InsertedLayers.length > 0) {
            $http({
                url: '/CMS_ContractLayer/SaveData?KaizenPublicKey=' + sessionStorage.PublicKey,
                method: "POST",
                data: $.param({ CM00020: $scope.InsertedLayers }),
                headers: {
                    'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8'
                }
            }).success(function (data) {
            }).error(function (data, status, headers, config) { alert(); });
        }
        if ($scope.UpdatedLayers.length > 0) {
            $http({
                url: '/CMS_ContractLayer/UpdateData?KaizenPublicKey=' + sessionStorage.PublicKey,
                method: "POST",
                data: $.param({ CM00020: $scope.UpdatedLayers }),
                headers: {
                    'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8'
                }
            }).success(function (data) {

            }).error(function (data, status, headers, config) { alert(); });
        }
        if ($scope.DeletedLayers.length > 0) {
            $http({
                url: '/CMS_ContractLayer/DeleteData?KaizenPublicKey=' + sessionStorage.PublicKey,
                method: "POST",
                data: $.param({ CM00020: $scope.DeletedLayers }),
                headers: {
                    'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8'
                }
            }).success(function (data) {

            }).error(function (data, status, headers, config) { alert(); });
        }
        waitingDialog.hide();
        $scope.Cancel();
    };
    $scope.RemoveLayer = function (Layer, index) {
        if (Layer.status == 1)
            $scope.ContractLayers.splice(index, 1);
        else
            Layer.status = 3;
    };
});
