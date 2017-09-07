app.controller('IV_CO_StoreMangerController', function ($scope, $http) {
    $scope.IV00120 = {};
    $scope.PagesIV00120 = [];
    $scope.LoadIV00120 = function (StoreManagerID) {
        if (angular.isUndefined($scope.IV00120.StoreManagerID)) {
            $http.get('/IV_CO_StoreManger/GetSingle?KaizenPublicKey=' + sessionStorage.PublicKey + "&StoreManagerID=" + StoreManagerID).success(function (data) {
                $scope.IV00120 = data;
            }).finally(function () {
                $scope.IV00120.Status = 2;
                if ($scope.IV00120.BinTrack != null && $scope.IV00120.BinTrack == true) {
                    $http.get('/INV_Site/GetBinBySiteID?KaizenPublicKey=' + sessionStorage.PublicKey, { params: { SiteID: $scope.IV00120.SiteID } }).success(function (data) {
                        if (data.length > 0)
                            $scope.SiteBins = data;
                        else
                            $scope.SiteBins = [];
                    }).finally(function () { $scope.BinChanged(); });
                }
            });
        }
    };
    $scope.LoadIV00120Page = function (ActionName) {
        removeEntityPage($scope.PagesIV00120);
        var URIPath = "/" + $scope.ToolBar.ActiveScreen.ID + "/" + ActionName + "?KaizenPublicKey=" + sessionStorage.PublicKey;
        var Page = {
            url: URIPath, ActionName: ActionName
        };
        $scope.PagesIV00120.push(Page);
    };
    $scope.toolbarOptions = {
        items: [
                 {
                     type: "button", text: "<span class=\"fa fa-plus-circle\"></span> New Store Manger",
                     attributes: { "class": "btn-primary" },
                     click: function (e) {
                         $scope.$apply(function () {
                             $scope.AddIV00120();
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
    $scope.LoadLookUp = function () {
    };
    $scope.LoadLookUp();

    $scope.AddIV00120 = function () {
        $scope.LoadIV00120Page('Create');
        $scope.Clear();
        $scope.IV00120.Status = 1;
    };
    $scope.EditIV00120 = function (StoreManagerID) {
        $scope.LoadIV00120Page('Create');
        $scope.Clear();
        $scope.LoadIV00120(StoreManagerID);
    };
    $scope.SaveData = function () {
        $http.post('/IV_CO_StoreManger/SaveData', {
            'IV00120': $scope.IV00120, 'KaizenPublicKey': sessionStorage.PublicKey
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
        $http.post('/IV_CO_StoreManger/UpdateData', { 'IV00120': $scope.IV00120, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
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
        $scope.IV00120 = {};
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
                $http.post('/IV_CO_StoreManger/DeleteData', { 'IV00120': $scope.IV00120, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
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
        $scope.PagesIV00120 = [];
    };

    $scope.SiteBack = function (site) {
        if (site != null) {
            $scope.IV00120.SiteID = site.SiteID;
            $scope.IV00120.BinTrack = site.BinTrack;
            if (site.BinTrack != null && site.BinTrack == true) {
                $http.get('/INV_Site/GetBinBySiteID?KaizenPublicKey=' + sessionStorage.PublicKey, { params: { SiteID: site.SiteID } }).success(function (data) {
                    if (data.length > 0)
                        $scope.SiteBins = data;
                    else
                        $scope.SiteBins = [];
                }).finally(function () { });
            }
        }
    };
    $scope.BinChanged = function () {
        var found = $scope.functiontofindObjectByKeyValue($scope.SiteBins, "BinID", $scope.IV00120.BinID);
        $scope.IV00120.IsBinGroup = found.IsBinGroup;
        if ($scope.IV00120.IsBinGroup) {
            $http.get('/INV_Site/GetSubBinByBinID?KaizenPublicKey=' + sessionStorage.PublicKey + "&BinID=" + $scope.IV00120.BinID)
                .success(function (data) {
                    $scope.SiteSubBins = data;
                }).finally(function () { });
        }
    };

});