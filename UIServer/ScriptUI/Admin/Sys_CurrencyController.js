app.controller('Sys_CurrencyController', function ($scope, $http) {
    $scope.SysGL00102 = {};
    $scope.SelectedCompany = {};
    $scope.PagesSysGL00102 = [];
    $scope.LoadLookUp = function () {
        $http.get('/Sys_Company/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.Companies = data;
        }).finally(function () {
            $scope.SelectedCompany.GridCompanyID = $scope.MY.CompanyID;
            $scope.GridRefresh();
        });
    };
    $scope.LoadLookUp();
    $scope.toolbarOptions = {
        items: [
                {
                     type: "button", text: "<span class=\"fa fa-plus-circle\"></span>  Set Currency",
                     attributes: { "class": "btn-primary" },
                     click: function (e) {
                         $scope.$apply(function () {
                             $scope.AddSysGL00102();
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
                     click: function (e) { $scope.OpenFilterWindow("Sys_Currency", "Sys_Currency"); }
                 },
                { type: "separator" },
                { template: "<label>Company:</label>" },
                {
                    template: "<select kendo-drop-down-list k-ng-model='SelectedCompany.GridCompanyID' " +
                    "k-data-text-field=\"'CompanyName'\" k-data-value-field=\"'CompanyID'\" k-data-source=\"Companies\" k-value-primitive='true' " +
                    "k-on-change='GridCompanyChanged()' k-filter=\"'startswith'\" " +
                    "id='GridCompanyOption_Sys_Currency' style='width: 250px;'></select>",
                    overflow: "never"
                },
        ]
, resizable: true
    };

    $scope.LoadSysGL00102 = function (CurrencyCode, CompanyID) {
        if (angular.isUndefined($scope.SysGL00102.CurrencyCode)) {
            $http.get('/Sys_Currency/GetSingle?KaizenPublicKey=' + sessionStorage.PublicKey + "&CurrencyCode=" + CurrencyCode + "&CompanyID=" + CompanyID).success(function (data) {
                $scope.SysGL00102 = data;
                $scope.SysGL00102.CompanyID = CompanyID;
            }).finally(function () {
                $scope.SysGL00102.Status = 2;
            });
        }
    };
    $scope.LoadSysGL00102Page = function (ActionName) {
        removeEntityPage($scope.PagesSysGL00102);
        var URIPath = "/" + $scope.ToolBar.ActiveScreen.ID + "/" + ActionName + "?KaizenPublicKey=" + sessionStorage.PublicKey;
        var Page = {
            url: URIPath, ActionName: ActionName
        };
        $scope.PagesSysGL00102.push(Page);
    };
    $scope.GridCompanyChanged = function () {
        $scope.SysGL00102.GridCompanyID = $scope.SelectedCompany.CompanyID;
        $scope.GridRefresh();
    };

    $scope.AddSysGL00102 = function () {
        $scope.LoadSysGL00102Page('Create');
        $scope.Clear();
        $scope.SysGL00102.Status = 1;
    };
    $scope.EditSysGL00102 = function (CurrencyCode) {
        $scope.LoadSysGL00102Page('Create');
        $scope.LoadSysGL00102(CurrencyCode, $scope.SelectedCompany.GridCompanyID);
    };
    $scope.SaveData = function () {
        $http.post('/Sys_Currency/SaveData', {
            'SysGL00102': $scope.SysGL00102, 'CompanyID': $scope.SysGL00102.CompanyID, 'KaizenPublicKey': sessionStorage.PublicKey
        }).success(function (data) {
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
        $http.post('/Sys_Currency/UpdateData', {
            'SysGL00102': $scope.SysGL00102, 'CompanyID': $scope.SysGL00102.CompanyID, 'KaizenPublicKey': sessionStorage.PublicKey
        }).success(function (data) {
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
        $scope.SysGL00102 = {};
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
                $http.post('/Sys_Currency/DeleteData', { 'SysGL00102': $scope.SysGL00102, 'CompanyID': $scope.SysGL00102.CompanyID, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
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
        $scope.PagesSysGL00102 = [];
    };
});