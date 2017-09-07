app.controller('INV_Set_ItemClassController', function ($scope, $http) {
    $scope.IV00001 = {};
    $scope.PagesIV00001 = [];
    $scope.toolbarOptions = {
        items: [
                 {
                     type: "button", text: "<span class=\"fa fa-plus-circle\"></span> Set Item Class ",
                     attributes: { "class": "btn-primary" },
                     click: function (e) { $scope.AddIV00001(); }
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
                     type: "button", text: "<span class=\"fa fa-plus-circle\"></span> Week Price ",
                     attributes: { "class": "btn-primary" },
                     click: function (e) { $scope.LoadIV00030Page('WeekPrice'); }
                 }
        ]
  , resizable: true
    };
    $scope.LoadIV00001 = function (ClassID) {
        if (angular.isUndefined($scope.IV00001.ClassID)) {
            $http.get('/ItemClass/GetSingle?KaizenPublicKey=' + sessionStorage.PublicKey + "&ClassID=" + ClassID).success(function (data) {
                $scope.IV00001 = data;
            }).finally(function () { $scope.IV00001.Status = 2; });
        }
    }
    $scope.LoadIV00001Page = function (ActionName) {
        removeEntityPage($scope.PagesIV00001);
        var URIPath = "/IVConfiguration/" + $scope.ToolBar.ActiveScreen.ID + "/" + ActionName + "?KaizenPublicKey=" + sessionStorage.PublicKey;
        var Page = {
            url: URIPath, ActionName: ActionName
        };
        $scope.PagesIV00001.push(Page);
    }


    $scope.AddIV00001 = function () {
        $scope.LoadIV00001Page('Create');
        $scope.$apply();
        $scope.Clear();
        $scope.IV00001.Status = 1;
    }
    $scope.EditIV00001 = function (ClassID) {
        $scope.LoadIV00001Page('Create');
        $scope.LoadIV00001(ClassID);
    };
    $scope.SaveData = function () {
        $http.post('/ItemClass/SaveData', {
            'IV00001': $scope.IV00001, 'KaizenPublicKey': sessionStorage.PublicKey
        }).success(function (data) {

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
        $http.post('/ItemClass/UpdateData', {
            'IV00001': $scope.IV00001, 'KaizenPublicKey': sessionStorage.PublicKey
        }).success(function (data) {
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
        $scope.IV00001 = {
        };

        $scope.IV00030 = {};
        $scope.IV00030List = {};
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
                $http.post('/ItemClass/DeleteData', { 'IV00001': $scope.IV00001, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
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
        $scope.PagesIV00001 = [];
    };

    //----------------------------- Start | IV00030
    $scope.IV00030 = {};
    $scope.IV00030List = [];

    $scope.LoadIV00030Page = function (Page) {
        $scope.PageStatus = 1;
        $scope.IV00030.DecimalDigit = $scope.MY.DecimalDigit;
        $scope.IV00030.CurrencyCode = $scope.MY.CurrencyCode;
        $scope.IV00030.ExchangeTableID = $scope.MY.ExchangeTableID;
        $scope.IV00030.IsMultiply = $scope.MY.IsMultiply;
        $scope.IV00030.ExchangeRateID = $scope.MY.ExchangeRateID;
        $scope.IV00030.ExchangeRate = $scope.MY.ExchangeRate;

        $scope.LoadIV00001Page(Page);
        $scope.LoadIV00001ClassDetails();
        $scope.LoadIV00022WeekDetails();
    }

    $scope.WeekDayLength = 0;

    $scope.ClassDataSource = [];
    $scope.LoadIV00001ClassDetails = function () {
        $http.post('/ItemClass/LoadIV00001ClassDetails', {
            'KaizenPublicKey': sessionStorage.PublicKey
        }).success(function (data) {
            $scope.ClassDataSource = data;

        }).error(function (data, status, headers, config) {

        });
    }
    $scope.WeekDetails = {};
    $scope.LoadIV00022WeekDetails = function () {
        $http.post('/ItemClass/LoadIV00022WeekDetails', {
            'KaizenPublicKey': sessionStorage.PublicKey
        }).success(function (data) {

            $scope.WeekDetails = data;
            $scope.WeekDayLength = $scope.WeekDetails.length;
            for (i = 0; i < $scope.WeekDayLength; i++) {
                $scope.IV00030obj = {};
                $scope.IV00030obj.ClassID = '';
                $scope.IV00030obj.WeekDayID = $scope.WeekDetails[i].WeekDayID;

                $scope.IV00030obj.CurrencyCode = '';
                $scope.IV00030obj.DecimalDigit = '';
                $scope.IV00030obj.ExchangeRate = '';
                $scope.IV00030obj.IsMultiply = '';
                $scope.IV00030obj.UnitPrice = '';
                $scope.IV00030List.push($scope.IV00030obj);
            }
        }).error(function (data, status, headers, config) {

        });
    }

    $scope.AssignClassID = function () {
        $scope.GetAllIV00030byClassID($scope.IV00030.ClassID);
        for (i = 0; i < $scope.WeekDayLength; i++) {
            $scope.IV00030List[i].ClassID = $scope.IV00030.ClassID;
        }
    }

    $scope.GetAllIV00030byClassID = function (ClassID) {
        $http.post('/ItemClass/GetAllIV00030byClassID', {
            'ClassID': ClassID, 'KaizenPublicKey': sessionStorage.PublicKey
        }).success(function (data) {
            debugger;
            if (data.length) {
                $scope.IV00030List = data;
                $scope.IV00030.DecimalDigit = $scope.IV00030List[0].DecimalDigit;
                $scope.IV00030.CurrencyCode = $scope.IV00030List[0].CurrencyCode;
                $scope.IV00030.ExchangeTableID = $scope.MY.ExchangeTableID;
                $scope.IV00030.IsMultiply = $scope.IV00030List[0].IsMultiply;
                $scope.IV00030.ExchangeRateID = $scope.MY.ExchangeRateID;
                $scope.IV00030.ExchangeRate = $scope.IV00030List[0].ExchangeRate;
                $scope.PageStatus = 2;
            }
            else {
                $scope.PageStatus = 1;
                for (i = 0; i < $scope.WeekDayLength; i++) {
                    $scope.IV00030List[i].UnitPrice = '';
                }
                $scope.IV00030.DecimalDigit = $scope.MY.DecimalDigit;
                $scope.IV00030.CurrencyCode = $scope.MY.CurrencyCode;
                $scope.IV00030.ExchangeTableID = $scope.MY.ExchangeTableID;
                $scope.IV00030.IsMultiply = $scope.MY.IsMultiply;
                $scope.IV00030.ExchangeRateID = $scope.MY.ExchangeRateID;
                $scope.IV00030.ExchangeRate = $scope.MY.ExchangeRate;
            }
        }).error(function (data, status, headers, config) {
            alert('---error---');
        });
    }

    $scope.SaveWeekData = function () {
        $http.post('/ItemClass/SaveWeekData', {
            'IV00030List': $scope.IV00030List, 'KaizenPublicKey': sessionStorage.PublicKey
        }).success(function (data) {

            if (data.Status == true) {
                $.bigBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#739E73",
                    timeout: 8000,
                    icon: "fa fa-check shake animated"
                });
                $scope.Cancel();
                $scope.Clear();
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

    $scope.UpdateWeekData = function () {
        $http.post('/ItemClass/UpdateWeekData', {
            'IV00030List': $scope.IV00030List, 'KaizenPublicKey': sessionStorage.PublicKey
        }).success(function (data) {

            if (data.Status == true) {
                $.bigBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#739E73",
                    timeout: 8000,
                    icon: "fa fa-check shake animated"
                });
                $scope.Cancel();
                $scope.Clear();
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

    $scope.CurrencyBack = function (currency) {
        if (currency != null) {
            $scope.IV00030.DecimalDigit = currency.DecimalDigit;
            $scope.IV00030.CurrencyCode = currency.CurrencyCode;
            $scope.IV00030.ExchangeTableID = currency.ExchangeTableID;
            $scope.IV00030.IsMultiply = currency.IsMultiply;
            $scope.IV00030.ExchangeRateID = currency.ExchangeRateID;
            $scope.IV00030.ExchangeRate = currency.ExchangeRate;
            var kendoWindow = $("#MainDialog").data("kendoWindow");
            kendoWindow.close();
            for (i = 0; i < $scope.WeekDayLength; i++) {
                $scope.IV00030List[i].DecimalDigit = currency.DecimalDigit;
                $scope.IV00030List[i].CurrencyCode = currency.CurrencyCode;
                $scope.IV00030List[i].ExchangeTableID = currency.ExchangeTableID;
                $scope.IV00030List[i].IsMultiply = currency.IsMultiply;
                $scope.IV00030List[i].ExchangeRateID = currency.ExchangeRateID;
                $scope.IV00030List[i].ExchangeRate = currency.ExchangeRate;
            }
        }
    };
    //----------------------------- Start | IV00030
});