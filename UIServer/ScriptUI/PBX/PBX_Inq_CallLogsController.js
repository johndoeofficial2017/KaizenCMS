app.controller('PBX_Inq_CallLogsController', function ($scope, $http) {
    $scope.PBX00100 = {};
    $scope.SelectedUser = {};
    $scope.SelectedUser.UserName = $scope.MY.UserName;
    $scope.DateFrom = new Date();
    $scope.DateTo = new Date();
    $scope.maxDate = new Date(2050, 0, 1, 0, 0, 0);
    $scope.GridRefresh();

    $scope.PagesPBX00100 = [];
    $scope.LoadPBX00100 = function (SessionID) {
        if (angular.isUndefined($scope.PBX00100.SessionID)) {
            $http.get('/PBX_Inq_CallLogs/GetSingle?KaizenPublicKey=' + sessionStorage.PublicKey + "&SessionID=" + SessionID).success(function (data) {
                $scope.PBX00100 = data;
            }).finally(function () { $scope.PBX00100.Status = 2; });
        }
    };
    $scope.LoadPBX00100Page = function (ActionName) {
        removeEntityPage($scope.PagesPBX00100);
        var URIPath = "/" + $scope.ToolBar.ActiveScreen.ID + "/" + ActionName + "?KaizenPublicKey=" + sessionStorage.PublicKey;
        var Page = {
            url: URIPath, ActionName: ActionName
        };
        $scope.PagesPBX00100.push(Page);
    };
    $scope.toolbarOptions = {
        items: [
                 {
                     template: "<span><label>From:</label><input kendo-date-picker k-format='format_date' k-ng-model='DateFrom' k-on-change='onDateFilterChange()' /></span>"
                 },
                 {
                     template: "<span><label>To:</label><input kendo-date-picker k-format='format_date' k-max='maxDate' k-rebind='maxDate'  k-ng-model='DateTo' k-on-change='onDateFilterChange()' /></span>",
                     overflow: "never"
                 },
                 { template: "<label>User:</label>" },
                 {
                     template: "<input type='text' class='form-control' ng-model='SelectedUser.UserName' />",
                     overflow: "never"
                 },
                 {
                     type: "button", text: "<span class=\"fa fa-search\"></span>",
                     attributes: { "class": "btn-primary" },
                     click: function (e) {
                         $scope.$apply(function () {
                             $scope.OpenkendoWindow("SysUser", "PopUp");
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
                     click: function (e) { $scope.OpenFilterWindow("GridPBX_Inq_CallLogs", "PBX_Inq_CallLogs"); }
                 }
        ]
, resizable: true
    };

    $scope.LoadLookUp = function () {
    };
    $scope.LoadLookUp();
    $scope.onDateFilterChange = function () {
        $scope.GridRefresh();
    };

    $scope.AddPBX00100 = function () {
        $scope.LoadPBX00100Page('Create');
        $scope.Clear();
        $scope.PBX00100.Status = 1;
    };
    $scope.EditPBX00100 = function (SessionID) {
        $scope.LoadPBX00100Page('Create');
        $scope.Clear();
        $scope.LoadPBX00100(SessionID);
    };
    $scope.SaveData = function () {
        $http.post('/PBX_Inq_CallLogs/SaveData', {
            'PBX00100': $scope.PBX00100, 'KaizenPublicKey': sessionStorage.PublicKey
        }).success(function (data) {
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
        $http.post('/PBX_Inq_CallLogs/UpdateData', { 'PBX00100': $scope.PBX00100, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
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
        $scope.PBX00100 = {};
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
                $http.post('/PBX_Inq_CallLogs/DeleteData', { 'PBX00100': $scope.PBX00100, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
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
        $scope.PagesPBX00100 = [];
    };

    $scope.UserBack = function (user) {
        if (user != null) {
            $scope.SelectedUser.UserName = user.UserName;
            $scope.GridRefresh();
        }
    };

});