app.controller('CMS_Trx_CasesAssignmentController', function ($scope, $http) {
    $scope.SelectedLookUp = {};
    $scope.SelectedLookUp.SelectedDocType = {};
    $scope.CM00202 = {};
    $scope.PagesCM00202 = [];
    $scope.toolbarOptions = {
        items: [
                 {
                     type: "button", text: "<span class=\"fa fa-plus-circle\"></span>New Transaction",
                     attributes: { "class": "btn-primary" },
                     click: function (e) {
                         $scope.$apply(function () {
                             $scope.AddCM00202();
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
    $scope.CaseDocType();
    $scope.CaseStatusList();

  
    $scope.LoadCM00202Page = function (ActionName) {
        removeEntityPage($scope.PagesCM00202);
        var URIPath = "/CMS/" + $scope.ToolBar.ActiveScreen.ID + "/" + ActionName + "?KaizenPublicKey=" + sessionStorage.PublicKey;
        var Page = { url: URIPath, ActionName: ActionName };
        $scope.PagesCM00202.push(Page);
    }

    $scope.AddCM00202 = function () {
        $scope.LoadCM00202Page('Create');
        $scope.Clear();
        $scope.CM00202.Status = 1;
        $http.get('/CMS_Trx_CasesAssignment/GetNextTransactionID?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.CM00202.AssigmentID = data;
            $scope.CM00202.AssignDate = new Date();
        }).finally(function () { });
    }
    $scope.EditCM00202 = function (AssigmentID) {
        $scope.LoadCM00202Page('FormView');
        $http.get('/CMS_Trx_CasesAssignment/GetSingle?AssigmentID=' + AssigmentID + '&KaizenPublicKey=' + sessionStorage.PublicKey)
            .success(function (data) {
                $scope.CM00202 = data;
                //alert(JSON.stringify($scope.CaseStatuses, null, 2));
            }).finally(function () { });
    };
    $scope.AgentBack = function (agent) {
        $scope.CM00202.AgentID = agent.AgentID;
        $scope.CM00202.AssignDate = new Date();
    };
    $scope.AssignCaseToAgent = function () {
        $scope.CM00206 = [];
        var items = $("#GridCMS_CaseAssign").data("kendoGrid").dataSource.data();
        for (i = 0; i < items.length; i++) {
            var item = items[i];
            if (item.IsSeleceted) {
                var temp = { CaseRef: item.CaseRef };
                $scope.CM00206.push(temp);
            }
        }
        if ($scope.CM00206.length == 0) {
            alert('cases selection is required');
            return;
        }
        $http.post('/CMS_Trx_CasesAssignment/SaveTransaction', {
            'CM00202': $scope.CM00202,
            'CM00206': $scope.CM00206,
            'KaizenPublicKey': sessionStorage.PublicKey
        })
            .success(function (data) {
                if (data.Status == true) {
                    $.bigBox({
                        title: data.Massage,
                        content: data.Description,
                        color: "#739E73",
                        timeout: 4000,
                        icon: "fa fa-check shake animated"
                    });
                    $scope.RefreshSMS();
                    //---------------------
                    $scope.Cancel();
                    $scope.GridRefresh('GridCMS_CM00202');
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
  
    $scope.Clear = function () {
        $scope.CM00202 = { Status: 1 };
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
                $http.post('/CMS_ClientClass/DeleteData', { 'CM00202': $scope.CM00202, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
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
        $scope.PagesCM00202 = [];
    };
});