app.controller('CMS_Trx_BatchRecoveryController', function ($scope, $http) {
    $scope.CM10100 = {};
    $scope.PagesCM10100 = [];
    $scope.toolbarOptions = {
        items: [
                 {
                     type: "button", text: "<span class=\"fa fa-plus-circle\"></span> Add Batch ",
                     attributes: { "class": "btn-primary" },
                     click: function (e) {
                         $scope.$apply(function () {
                             $scope.AddCM10100();
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
                     click: function (e) { $scope.OpenFilterWindow('GridCMS_Trx_BatchRecovery', 'CMS_Trx_BatchRecovery'); }
                 }
        ]
, resizable: true
    };
    $scope.LoadCM10100 = function (BatchID) {
        if (angular.isUndefined($scope.CM10100.BatchID)) {
            $http.get('/CMS/CMS_Trx_BatchRecovery/GetSingle?KaizenPublicKey=' + sessionStorage.PublicKey + "&BatchID=" + BatchID).success(function (data) {
                $scope.CM10100 = data;
            }).finally(function () {
                $scope.CM10100.BookingDate = kendoparsetoDate($scope.CM10100.BookingDate);
                $scope.CM10100.CreatedDate = kendoparsetoDate($scope.CM10100.CreatedDate);
                $scope.CM10100.Status = 2;
            });
        }
    };
    $scope.LoadCM10100Page = function (ActionName) {
        removeEntityPage($scope.PagesCM10100);
        var URIPath = "/CMS/" + $scope.ToolBar.ActiveScreen.ID + "/" + ActionName + "?KaizenPublicKey=" + sessionStorage.PublicKey;
        var Page = { url: URIPath, ActionName: ActionName };
        $scope.PagesCM10100.push(Page);
    };

    $scope.AddCM10100 = function () {
        $scope.LoadCM10100Page('Create');
        $scope.Clear();
        $scope.CM10100.Status = 1;
        $http.get('/CMS_Trx_BatchRecovery/GetNextBatchID?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.CM10100.BatchID = data;
        }).finally(function () { });
    }
    $scope.EditCM10100 = function (BatchID) {
        $scope.LoadCM10100Page('Create');
        $scope.Clear();
        $scope.LoadCM10100(BatchID);
    };
    $scope.SaveData = function () {
        $http.post('/CMS/CMS_Trx_BatchRecovery/SaveData', { 'CM10100': $scope.CM10100, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
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
        $http.post('/CMS/CMS_Trx_BatchRecovery/UpdateData', { 'CM10100': $scope.CM10100, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
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
        $scope.CM10100 = {};
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
                $http.post('/CMS/CMS_Trx_BatchRecovery/DeleteData', { 'CM10100': $scope.CM10100, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
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
        $scope.PagesCM10100 = [];
    };
    $scope.ContractBack = function (contract) {
        $scope.CM10100.ContractID = contract.ContractID;
        $scope.CM10100.ContractName = contract.ContractName;
    };
    $scope.ClientBack = function (client) {
        $scope.CM10100.ClientID = client.ClientID;
        $scope.CM10100.ClientName = client.ClientName;
    };
    $scope.CurrencyBack = function (currency) {
        if (currency != null) {
            $scope.CM10100.DecimalDigit = currency.DecimalDigit;
            $scope.CM10100.CurrencyCode = currency.CurrencyCode;
            $scope.CM10100.ExchangeTableID = currency.ExchangeTableID;
            $scope.CM10100.IsMultiply = currency.IsMultiply;
            $scope.CM10100.ExchangeRateID = currency.ExchangeRateID;
            $scope.CM10100.ExchangeRate = currency.ExchangeRate;
            var kendoWindow = $("#MainDialog").data("kendoWindow");
            kendoWindow.close();
        }
    };
    $scope.ExchangeRateBack = function (rate) {
        if (rate != null) {
            switch ($scope.CurrentControl) {
                case 'Main':
                    $scope.CM00207.ExchangeRateID = rate.ExchangeRateID;
                    $scope.CM00207.ExchangeRate = rate.ExchangeRate;
                    break;
                case 'UploadView':
                    $scope.UploadObject.ExchangeRateID = rate.ExchangeRateID;
                    $scope.UploadObject.ExchangeRate = rate.ExchangeRate;
                    break;
            }
        }
    };
    $scope.ExchangeTableBack = function (table) {
        if (table != null) {
            switch ($scope.CurrentControl) {
                case 'Main':
                    $scope.CM00207.ExchangeTableID = table.ExchangeTableID;
                    $scope.CM00207.IsMultiply = table.IsMultiply;
                    var kendoWindow = $("#MainDialog").data("kendoWindow");
                    kendoWindow.close();
                    break;
                case 'UploadView':
                    $scope.UploadObject.ExchangeTableID = table.ExchangeTableID;
                    $scope.UploadObject.IsMultiply = table.IsMultiply;
                    var kendoWindow = $("#MainDialog").data("kendoWindow");
                    kendoWindow.close();
                    break;
            }
        }
    };

});