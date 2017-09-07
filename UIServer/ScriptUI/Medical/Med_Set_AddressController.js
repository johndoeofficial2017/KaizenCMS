app.controller('Med_Set_AddressController', function ($scope, $http) {
    $scope.Med00008 = {};
    $scope.PagesMed00008 = [];
    $scope.LoadMed00008 = function (AddressCode) {
        if (angular.isUndefined($scope.Med00008.AddressCode)) {
            $http.get('/Med_Set_Address/GetSingle?KaizenPublicKey=' + sessionStorage.PublicKey + "&AddressCode=" + AddressCode).success(function (data) {
                $scope.Med00008 = data;
            }).finally(function () { $scope.Med00008.Status = 2; });
        }
    };
    $scope.LoadMed00008Page = function (ActionName) {
        removeEntityPage($scope.PagesMed00008);
        var URIPath = "/" + $scope.ToolBar.ActiveScreen.ID + "/" + ActionName + "?KaizenPublicKey=" + sessionStorage.PublicKey;
        var Page = {
            url: URIPath, ActionName: ActionName
        };
        $scope.PagesMed00008.push(Page);
    };
    $scope.toolbarOptions = {
        items: [
                 {
                     type: "button", text: "<span class=\"fa fa-plus-circle\"></span> New Address Type",
                     attributes: { "class": "btn-primary" },
                     click: function (e) {
                         $scope.$apply(function () {
                             $scope.AddMed00008();
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
    $scope.LoadLookUp = function () {};
    $scope.LoadLookUp();

    $scope.AddMed00008 = function () {
        $scope.LoadMed00008Page('Create');
        $scope.Clear();
        $scope.Med00008.Status = 1;
    };
    $scope.EditMed00008 = function (AddressCode) {
        $scope.LoadMed00008Page('Create');
        $scope.Clear();
        $scope.LoadMed00008(AddressCode);
    };
    $scope.SaveData = function () {
        $http.post('/Med_Set_Address/SaveData', {
            'Med00008': $scope.Med00008, 'KaizenPublicKey': sessionStorage.PublicKey
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
        $http.post('/Med_Set_Address/UpdateData', { 'Med00008': $scope.Med00008, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
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
        $scope.Med00008 = {};
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
                $http.post('/Med_Set_Address/DeleteData', { 'Med00008': $scope.Med00008, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
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
        $scope.PagesMed00008 = [];
    };
});