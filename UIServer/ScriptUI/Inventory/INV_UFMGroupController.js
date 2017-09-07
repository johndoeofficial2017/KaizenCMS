app.controller('INV_Set_UFMGroupController', function ($scope, $http) {
    $scope.IV00002 = {};
    $scope.PagesIV00002 = [];
    $scope.toolbarOptions = {
        items: [
                 {
                     type: "button", text: "<span class=\"fa fa-plus-circle\"></span> Set UFM Group ",
                     attributes: { "class": "btn-primary" },
                     click: function (e) { $scope.AddIV00002(); }
                 },
                 {
                     type: "splitButton",
                     text: "GO",
                     click: function (e) {
                     },
                     menuButtons: [
                         {
                             type: "button", text: "<span class=\"fa fa-cog\"></span> Unit Of Measure Setup", click: function (e) {
                                 $scope.$apply(function () {
                                     $scope.SetupUFM();
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
    $scope.LoadIV00002 = function (UFMGroupID) {
        if (angular.isUndefined($scope.IV00002.UFMGroupID)) {
            $http.get('/INV_Set_UFMGroup/GetSingle?KaizenPublicKey=' + sessionStorage.PublicKey + "&UFMGroupID=" + UFMGroupID).success(function (data) {
                $scope.IV00002 = data;
            }).finally(function () { $scope.IV00002.Status = 2; });
        }
    }
    $scope.LoadIV00002Page = function (ActionName) {
        removeEntityPage($scope.PagesIV00002);
        var URIPath = "/" + $scope.ToolBar.ActiveScreen.ID + "/" + ActionName + "?KaizenPublicKey=" + sessionStorage.PublicKey;
        var Page = {
            url: URIPath, ActionName: ActionName
        };
        $scope.PagesIV00002.push(Page);
    }

    $scope.AddIV00002 = function () {
        $scope.LoadIV00002Page('Create');
        $scope.$apply();
        $scope.Clear();
        $scope.IV00002.Status = 1;
    }
    $scope.EditIV00002 = function (UFMGroupID) {
        $scope.LoadIV00002Page('Create');
        $scope.LoadIV00002(UFMGroupID);
    };
    $scope.OpenUFM = function (UFMGroupID) {
        removeEntityPage($scope.PagesIV00002);
        $scope.LoadIV00002Page('UFM');
        if (UFMGroupID == null) {
            $scope.LoadIV00002(UFMGroupID);
            $scope.LoadUFMGroupINV_UFM(UFMGroupID);
        }
    }
    $scope.SaveData = function () {
        $http.post('/INV_Set_UFMGroup/SaveData', {
            'IV00002': $scope.IV00002, 'KaizenPublicKey': sessionStorage.PublicKey
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
        $http.post('/INV_Set_UFMGroup/UpdateData', {
            'IV00002': $scope.IV00002, 'KaizenPublicKey': sessionStorage.PublicKey
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
        $scope.IV00002 = {
        };
        $scope.IV00003 = {
        };
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
                $http.post('/INV_Set_UFMGroup/DeleteData', { 'IV00002': $scope.IV00002, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
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
        $scope.PagesIV00002 = [];

        $scope.InsertedUFMs = [];
        $scope.UpdatedUFMs = [];
        $scope.DeletedUFMs = [];

        $scope.UFMList = [];
    };

    //-------------------------- UFM
    $scope.IV00003 = {};
    $scope.SetupUFM = function () {
        removeEntityPage($scope.PagesIV00002);
        $scope.LoadIV00002Page('SetupUFM');
        $scope.IV00003.Status = 1;
    };
    $scope.UFMGroupBack = function (ufmgroup) {
        if (ufmgroup != null) {
            $scope.IV00002 = ufmgroup;
            $scope.IV00003.UFMGroupID = ufmgroup.UFMGroupID;
            $scope.GridRefresh('GridINV_Set_UFM');
        }
    };
    $scope.UFMBack = function (ufm) {
        if (ufm != null) {
            $scope.IV00002.UFMID = ufm.UFMID;
            $scope.IV00002.BaseUnit = ufm.BaesUnit;
        }
    };

    $scope.SaveUFM = function () {
        $scope.IV00003.UFMGroupID = $scope.IV00002.UFMGroupID;
        $http.post('/INV_Set_UFMGroup/SaveUFMData', { 'IV00003': $scope.IV00003, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
            if (data.Status == true) {
                $.bigBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#739E73",
                    timeout: 4000,
                    icon: "fa fa-check shake animated"
                });
                $scope.GridRefresh('GridINV_Set_UFM');
                $scope.IV00003 = { Status: 1 };
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
    $scope.UpdateUFM = function () {
        $scope.IV00003.UFMGroupID = $scope.IV00002.UFMGroupID;
        $http.post('/INV_Set_UFMGroup/UpdateUFMData', { 'IV00003': $scope.IV00003, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
            if (data.Status == true) {
                $.bigBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#739E73",
                    timeout: 4000,
                    icon: "fa fa-check shake animated"
                });
                $scope.GridRefresh('GridINV_Set_UFM');
                $scope.IV00003 = { Status: 1 };
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
    $scope.DeleteUFM = function () {
        $.SmartMessageBox({
            title: "Delete Record",
            content: "Are you sure you want to delete this?",
            buttons: '[No][Yes]'
        }, function (ButtonPressed) {
            if (ButtonPressed === "Yes") {
                $http.post('/INV_Set_UFMGroup/DeleteUFMData', { 'IV00003': $scope.IV00003, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
                    if (data.Status == true) {
                        $.bigBox({
                            title: data.Massage,
                            content: data.Description,
                            color: "#739E73",
                            timeout: 4000,
                            icon: "fa fa-check shake animated"
                        });
                        $scope.GridRefresh('GridINV_Set_UFM');
                        $scope.IV00003 = { Status: 1 };
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
});