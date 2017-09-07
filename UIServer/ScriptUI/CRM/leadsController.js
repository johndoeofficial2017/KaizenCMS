app.controller('leadsController', function ($scope, $http) {
    $scope.CRM00200 = {};
    $scope.PagesCRM00200 = [];
    $scope.toolbarOptions = {
        items: [
                 {
                     type: "button", text: "<span class=\"fa fa-plus-circle\"></span> Add Lead ",
                     attributes: { "class": "btn-primary" },
                     click: function (e) {
                         $scope.$apply(function () {
                             $scope.AddCRM00200();
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
        ]
 , resizable: true
    };
    $scope.SelectedLookUp = {};
    $scope.SourceUsers = [];
    $scope.Roles = [];
    $scope.SelectedDashboard = {};
    $scope.SelectedReportType = {};

    $scope.LoadCRM00200 = function (DashboardCode) {
        if (angular.isUndefined($scope.CRM00200.DashboardCode)) {
            $http.get('/leads/GetSingle?KaizenPublicKey=' + sessionStorage.PublicKey + "&DashboardCode=" + DashboardCode).success(function (data) {
                $scope.CRM00200 = data;
            }).finally(function () { $scope.CRM00200.Status = 2; });
        }
    }
    $scope.LoadCRM00200Page = function (ActionName) {
        removeEntityPage($scope.PagesCRM00200);
        var URIPath = "/CRM/" + $scope.ToolBar.ActiveScreen.ID + "/" + ActionName + "?KaizenPublicKey=" + sessionStorage.PublicKey;
        var Page = { url: URIPath, ActionName: ActionName };
        $scope.PagesCRM00200.push(Page);
    };

    $scope.AddCRM00200 = function () {
        $scope.LoadCRM00200Page('Create');
        $scope.Clear();
        $scope.CRM00200.Status = 1;
        $scope.LoadCaseTypes_Leads();
    };

    $scope.EditCRM00200 = function (DashboardCode) {
        $scope.LoadCRM00200Page('Create');
        $scope.FillDropdownLists();
        $scope.LoadCRM00200(DashboardCode);
    };
    $scope.SaveLeadsData = function () {
        debugger;
        //$http({
        //    method: 'POST',
        //    url: '/leads/SaveLeadsData?KaizenPublicKey=' + sessionStorage.PublicKey,
        //    transformRequest: angular.identity,
        //    headers: { 'Content-Type': undefined },
        //    data: JSON.stringify($scope.KaizenColumn)
        //})
        //.then(success);

        $http.post('/leads/SaveLeadsData', { 'TRXTypeID': $scope.CRM00200.TRXTypeID, 'KaizenColumn': $scope.KaizenColumn, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
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
                title: "Error",
                content: "Unable to save dashboard data",
                color: "#C46A69",
                timeout: 4000,
                icon: "fa fa-bolt swing animated"
            });
        });
    };
    $scope.UpdateData = function () {
        $http.post('/leads/UpdateData', { 'CRM00200': $scope.CRM00200, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
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
        $scope.CRM00200 = {};
        $scope.Kaizen00054 = {};
        $scope.Kaizen00055 = {};
        $scope.DashboardList = [];
        $scope.SelectedReportType = {};
        $scope.DashboardReportList = [];
        $scope.Kaizen00053List = [];
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
                $http.post('/leads/DeleteData', { 'CRM00200': $scope.CRM00200, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
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
        $scope.PagesCRM00200 = [];
    };

    $scope.LoadCaseTypes_Leads = function () {
        $http.get('/CMS_CaseType/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.CaseTypes = data;
        }).finally(function () {
        });
    };

    $scope.CaseTypeChangedForLeads = function () {
        if (angular.isDefined($scope.SelectedLookUp.SelectedType)) {
            $scope.CRM00200.TRXTypeID = $scope.SelectedLookUp.SelectedType;
            $scope.LoadAllFieldsByTRXTypeID($scope.CRM00200.TRXTypeID);
        }
    };
    $scope.KaizenColumn = [];
    $scope.LeadColumn = {
        FieldName: "LeadID",
        FieldDisplay: "Lead ID",
        FieldValue: "",
        FieldTypeID:3,
        IsRequired: true
    };

    $scope.LoadAllFieldsByTRXTypeID = function (TRXTypeID) {
        $http.get('/leads/GetCaseFields?KaizenPublicKey=' + sessionStorage.PublicKey,
        { params: { TRXTypeID: TRXTypeID } }).success(function (data) {
            $scope.CaseTypeFieldColumns = data;

            $scope.KaizenColumn.push($scope.LeadColumn);
            $scope.CaseTypeFieldColumns.forEach(function (element, index) {
                var objField = {};
                objField.FieldName = element.FieldName;
                objField.FieldDisplay = element.FieldDisplay;
                objField.FieldValue = "";
                objField.FieldTypeID = element.FieldTypeID;
                objField.IsRequired = true;

                $scope.KaizenColumn.push(objField);
            });

            debugger;
            console.log("$scope.KaizenColumn:" + $scope.KaizenColumn);
        }).finally(function () {
        });
    };

    $scope.ModuleChanged = function () {
        //if (angular.isDefined($scope.SelectedLookUp.SelectedModuleID)) {
        //    $scope.CRM00200.ModuleID = $scope.SelectedLookUp.SelectedModuleID;
        //}
    };

    $scope.MenueTypeChanged = function () {
        //if (angular.isDefined($scope.SelectedLookUp.SelectedMenueTypeID)) {
        //    $scope.CRM00200.MenueTypeID = $scope.SelectedLookUp.SelectedMenueTypeID;
        //}
    };

    $scope.CompanyChanged = function () {
        if (angular.isDefined($scope.SelectedDashboard.CompanyID)) {
            $scope.CRM00200.CompanyID = $scope.SelectedDashboard.CompanyID;
        }
    };

    $scope.ViewTypeChanged = function () {
        //if (angular.isDefined($scope.SelectedLookUp.SelectedViewType)) {
        //    $scope.CRM00200.ViewType = $scope.SelectedLookUp.SelectedViewType;
        //}
    };

    $scope.UserBack = function (user) {
        if (user != null) {
            switch ($scope.CurrentControl) {
                case 'DashboardUsers':
                    if (user != null) {
                        $scope.Kaizen00055.UserName = user.UserName;
                        $scope.UserChangedForDashboard($scope.Kaizen00055.UserName);
                    }
                    break;
            }
        }
    };

    $scope.LoadDashboardList = function () {
        $http.get('/leads/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.DashboardList = data;
        }).finally(function () { });
    };

});