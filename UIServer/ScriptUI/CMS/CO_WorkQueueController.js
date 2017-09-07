app.controller('CO_WorkQueueController', function ($scope, $http) {
    $scope.CM00105 = {};
    $scope.PagesCM00105 = [];
    $scope.CM00215 = {};
    $scope.toolbarOptions = {
        items: [
                 {
                     type: "button", text: "<span class=\"fa fa-plus-circle\"></span> New ",
                     attributes: { "class": "btn-primary" },
                     click: function (e) {
                         $scope.$apply(function () {
                             $scope.AddCM00105();
                         });
                     }
                 },
                 {
                     type: "button", text: "<span class=\"fa fa-bars\"></span> Distribution rules",
                     attributes: { "class": "btn-primary" },
                     click: function (e) {
                         $scope.$apply(function () {
                             $scope.AgentCondition();
                         });
                     }
                 },
                 {
                     type: "splitButton",
                     text: "Agent Target",
                     click: function (e) {
                         $scope.$apply(function () {
                             $scope.Target();
                         });
                     },
                     menuButtons: [
                         {
                             text: "Agent Target", click: function (e) {
                                 $scope.$apply(function () {
                                     $scope.TargetBYLayer();
                                 });
                             }
                         },
                         {
                             text: "Client Target", click: function (e) {
                                 $scope.$apply(function () {
                                     $scope.ClientTarget();
                                 });
                             }
                         },
                         {
                             text: "Period Target", click: function (e) {
                                 $scope.$apply(function () {
                                     $scope.PeriodTarget();
                                 });
                             }
                         },
                         {
                             text: "Cycle Target", click: function (e) {
                                 $scope.$apply(function () {
                                     $scope.CycleTarget();
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
                 { type: "button", spriteCssClass: "k-tool-icon k-i-refresh" }
                 ,
                 {
                     type: "button", text: " Filter", spriteCssClass: "k-tool-icon k-filter",
                     click: function (e) { $scope.OpenFilterWindow("GridCMS_Agent", "CMS_Agent"); }
                 }
        ]
        , resizable: true
    };
    $scope.PhotoChanged = false;
    $scope.SignatureChanged = false;
    $scope.MainGridURL = "/CMS/CMS_Agent/MainGrid?KaizenPublicKey=" + sessionStorage.PublicKey;


    $scope.LoadCM00105 = function (AgentID) {
        if (angular.isUndefined($scope.CM00105.AgentID)) {
            $http.get('/CMS_Agent/GetSingle?KaizenPublicKey=' + sessionStorage.PublicKey + "&AgentID=" + AgentID).success(function (data) {
                $scope.CM00105 = data;
            }).finally(function () {
                $scope.CM00105.Status = 2;
                if ($scope.CM00105.PhotoExtension == null)
                    $scope.link = '/Photo/AgentPhoto/AgentID.jpg';
                else
                    $scope.link = '/Photo/AgentPhoto/' + kaizenTrim($scope.CM00105.AgentID) + "." + kaizenTrim($scope.CM00105.PhotoExtension) + "?c=" + Math.random();

                if ($scope.CM00105.SignatureExtension == null)
                    $scope.Signaturelink = '/Photo/AgentSignature/AgentSignature.png';
                else
                    $scope.Signaturelink = '/Photo/AgentSignature/' + kaizenTrim($scope.CM00105.AgentID) + "." + kaizenTrim($scope.CM00105.SignatureExtension) + "?c=" + Math.random();

            });
        }
    }
    $scope.LoadCM00105Page = function (ActionName) {
        removeEntityPage($scope.PagesCM00105);
        var URIPath = "/CMS/CMS_Agent/" + ActionName + "?KaizenPublicKey=" + sessionStorage.PublicKey;
        var Page = { url: URIPath, ActionName: ActionName };
        $scope.PagesCM00105.push(Page);
    }
    $scope.LoadLookUp = function () {
        $http.get('/CMS_AgentGroup/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.AgentGroups = data;
        }).finally(function () { });
    };
    $scope.LoadLookUp();
    $scope.OnGroupChanged = function () {
        $http.get('/CMS_Agent/GetNextAgent?KaizenPublicKey=' + sessionStorage.PublicKey,
            { params: { CUSTCLAS: $scope.CM00105.AgentGroup } })
        .success(function (data) {
            $scope.CM00105.AgentID = data;
        })
        .finally(function () {
        });
    };
    $scope.AddCM00105 = function () {
        $scope.LoadCM00105Page('Create');
        $scope.Clear();
        $scope.CM00105.Status = 1;
    };
    $scope.EditCM00105 = function (AgentID) {
        $scope.Clear();
        $scope.LoadCM00105Page('Create');
        $scope.LoadCM00105(AgentID);
        $scope.CM00105.Status = 2;
    };
    $scope.SaveData = function () {
        $http.post('/CMS_Agent/SaveData', { 'CM00105': $scope.CM00105, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
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
        $http.post('/CMS_Agent/UpdateData', { 'CM00105': $scope.CM00105, 'PhotoChanged': $scope.PhotoChanged, 'SignatureChanged': $scope.SignatureChanged, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
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
        $scope.CM00105 = {};
        $scope.CM00212 = {};
        $scope.CM00107 = {};
        $scope.CM10110 = {};
        $scope.CM00215List = [];
        $scope.CM00215 = {};
        $scope.CM10110List = [];
        $scope.CM10110 = {};

        $scope.CM00107List = [];
        $scope.CM00107 = {};
        $scope.link = '/Photo/AgentPhoto/AgentID.jpg';
        $scope.Signaturelink = '/Photo/AgentSignature/AgentSignature.png';
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
                $http.post('/CMS_Agent/DeleteData', { 'CM00105': $scope.CM00105, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
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
        $scope.PagesCM00105 = [];
        $scope.AgentTargets = [];




    };

    $scope.OrgnizationChart = function () {
        $scope.LoadCM00105Page('OrgnizationChartPage');
    };
    $scope.OrgAgentGroupChanged = function () {
        var diagram = $("#diagram").data("kendoDiagram");
        diagram.dataSource.read();
    };

    $scope.AgentBack = function (agent) {
        if (agent != null) {
            switch ($scope.CurrentControl) {
                case 'Main':
                    $scope.CM00105.AgentID = agent.AgentID;
                    break;
                case 'Supervisor':
                    $scope.CM00105.SupervisorFullName = agent.FullName;
                    $scope.CM00105.SupervisorID = agent.AgentID;
                    break;
                case 'CLientTargets':
                    $scope.CM00212.AgentID = agent.AgentID;
                    $scope.CM00212.FullName = agent.FullName;
                    $scope.CM00212.CalendarID = agent.CalendarID;
                    $http.get('/CMS_Calendar/GetCalendarPeriods?KaizenPublicKey=' + sessionStorage.PublicKey,
                      {
                          params: {
                              CalendarID: $scope.CM00212.CalendarID
                              , YearCode: $scope.CM00212.SelectedYear.YearCode
                          }
                      })
                      .success(function (data) {
                          if (data.length > 0) {
                              $scope.CM00212List = data;
                              $scope.CM00212List.forEach(function (element, index) {
                                  element.StartDate = kendoparsetoDate(element.StartDate);
                                  element.EndDate = kendoparsetoDate(element.EndDate);
                                  element.AgentID = $scope.CM00212.AgentID;
                                  element.TargetAmount = 0;
                                  element.Status = 0; //New
                              });
                          }
                      }).finally(function () { });
                    break;
                case 'CM00107List':
                    $scope.CM00107.AgentID = agent.AgentID;
                    $scope.CM00107.FullName = agent.FullName;
                    $scope.CM00107.CalendarID = agent.CalendarID;
                    $scope.AgentPeriodTargetLayer_Changed();

                    break;
                case 'CM10110List':
                    $scope.CM10110.AgentID = agent.AgentID;
                    $scope.CM10110.FullName = agent.FullName;
                    $scope.CM10110.CalendarID = agent.CalendarID;
                    $scope.AgentPeriodTarget_Changed();

                    break;
                case 'AgentCondition':
                    $scope.CM00105 = agent;
                    $scope.CM00109.AgentID = agent.AgentID;
                    $scope.GridRefresh('GridCMS_AgentConditionByAgent');
                    break;
                case 'AgentConditionField':
                    $scope.CM00109.FieldValue = agent.AgentID;
                    break;
                case 'CycleTarget':
                    $scope.CM00215.AgentID = agent.AgentID;
                    $scope.CM00215.FullName = agent.FullName;
                    $scope.CM00215.CalendarID = agent.CalendarID;
                    $http.get('/CMS_Calendar/GetCalendarPeriods?KaizenPublicKey=' + sessionStorage.PublicKey,
                    {
                        params: {
                            CalendarID: $scope.CM00215.CalendarID
                            , YearCode: $scope.CM00215.SelectedYear.YearCode
                        }
                    })
                    .success(function (data) {
                        if (data.length > 0) {
                            $scope.CM00215List = data;
                            $scope.CM00215List.forEach(function (element, index) {
                                element.StartDate = kendoparsetoDate(element.StartDate);
                                element.EndDate = kendoparsetoDate(element.EndDate);
                                element.AgentID = $scope.CM00215.AgentID;
                                element.TargetAmount = 0;
                                element.Status = 0; //New
                            });
                            $scope.CM00215.SelectedBucket = null;
                        }
                    }).finally(function () { });
                    break;
            }
        }
    };
    $scope.UserBack = function (user) {
        if (user != null) {
            $scope.CM00105.UserCode = user.UserName;
        }
    };
    $scope.EmployeeBack = function (employee) {
        if (employee != null) {
            $scope.CM00105.EmployeeFullName = employee.FullName;
            $scope.CM00105.EmployeeID = employee.EmployeeID;
        }
    };
    $scope.ClientBack = function (client) {
        if (client != null) {
            switch ($scope.CurrentControl) {
                case 'AgentClientTarget':
                    $scope.CM00212.ClientID = client.ClientID;
                    $scope.CM00212.ClientName = client.ClientName;
                    $scope.CM00212List.forEach(function (element, index) {
                        element.StartDate = kendoparsetoDate(element.StartDate);
                        element.EndDate = kendoparsetoDate(element.EndDate);
                        element.AgentID = $scope.CM00212.AgentID;
                        element.ClientID = client.ClientID;
                        element.ClientName = client.ClientName;
                        element.TargetAmount = 0;
                        element.Status = 0; //New
                    });
                    $http.get('/CMS_Agent/GetAgentTargetByCLient?KaizenPublicKey=' + sessionStorage.PublicKey,
                    {
                        params: {
                            AgentID: $scope.CM00212.AgentID
                            , YearCode: $scope.CM00212.SelectedYear.YearCode
                            , ClientID: client.ClientID
                        }
                    })
                    .success(function (data) {
                        //alert(data.length);
                        if (data.length > 0) {
                            $scope.temp = data;
                            $scope.CM00212List.forEach(function (element, index) {
                                element.Status = 1; //Updateing
                                $scope.temp.forEach(function (element2, index2) {
                                    if (element.PERIODID == element2.PERIODID) {
                                        element.TargetAmount = element2.TargetAmount;
                                        return;
                                    }
                                });
                            });
                            $scope.temp = null;
                        }
                    }).finally(function () { });
                    break;
                case 'AgentCondition':
                    $scope.CM00109.FieldValue = client.ClientID;
                    break;
            }
        }
    };

    ////////////// Period Target //////////////


    $scope.PeriodTargetAmountChanged = function (target) {
        if (target.TargetAmount > target.BalanceTarget) {
            target.TargetAmount = target.BalanceTarget;
        }
        if (target.status == 0) {
            target.status = 2;
        }
    };

    //-----------------------------------------------------
    $scope.CM00107 = {};
    $scope.CM10110 = {};
    //Cycle
    $scope.YearPeriodTargetCycle_Changed = function () {
        $scope.CM00215.YearCode = $scope.CM00215.SelectedYear.YearCode;
        $scope.CM00215.SelectedYear.PeriodFrom = kendoparsetoDate($scope.CM00215.SelectedYear.PeriodFrom);
        $scope.CM00215.SelectedYear.PeriodTo = kendoparsetoDate($scope.CM00215.SelectedYear.PeriodTo);
    };
    $scope.BucketID_Changed = function () {
        $scope.CM00215List.forEach(function (element, index) {
            element.StartDate = kendoparsetoDate(element.StartDate);
            element.EndDate = kendoparsetoDate(element.EndDate);
            element.AgentID = $scope.CM00215.AgentID;
            element.BucketID = $scope.CM00215.SelectedBucket.BucketID;
            element.BucketName = $scope.CM00215.SelectedBucket.BucketName;
            element.TargetAmount = 0;
            element.Status = 0; //New
        });
        $http.get('/CMS_Agent/GetCyclePeriodsInfo?KaizenPublicKey=' + sessionStorage.PublicKey,
        {
            params: {
                AgentID: $scope.CM00215.AgentID
                , YearCode: $scope.CM00215.SelectedYear.YearCode
                , BucketID: $scope.CM00215.SelectedBucket.BucketID
            }
        })
        .success(function (data) {
            if (data.length > 0) {
                $scope.temp = data;
                $scope.CM00215List.forEach(function (element, index) {
                    element.Status = 1; //Updateing
                    $scope.temp.forEach(function (element2, index2) {
                        if (element.PERIODID == element2.PERIODID) {
                            element.TargetAmount = element2.TargetAmount;
                            return;
                        }
                    });
                });
                $scope.temp = null;
            }
        }).finally(function () { });
    };
    $scope.SaveCycleTarget = function () {
        $http({
            url: '/CMS_Agent/SavePeriodCycleTargetInfo?KaizenPublicKey=' + sessionStorage.PublicKey,
            method: "POST",
            data: JSON.stringify($scope.CM00215List),
            dataType: "json",
            headers: {
                'Content-Type': 'application/json; charset=utf-8'
            }
        }).success(function (data) {
            if (data.Status == true) {
                $.bigBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#739E73",
                    timeout: 3000,
                    icon: "fa fa-check shake animated"
                });
                $scope.CM00215List = [];
                $scope.CM00215 = {};
                $scope.Cancel();
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
    //TargetBYLayer
    $scope.TargetBYLayer = function () {
        $scope.LoadCM00105Page('TargetBYLayer');
        $scope.Clear();
        $http.get('/CMS_Set_Target/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.TargetLayers = data;
        }).finally(function () { });
    };
    $scope.YearTargetsBYLayer_Changed = function () {
        $scope.CM00107.YearCode = $scope.CM00107.SelectedYear.YearCode;
        $scope.CM00107.SelectedYear.PeriodFrom = kendoparsetoDate($scope.CM00107.SelectedYear.PeriodFrom);
        $scope.CM00107.SelectedYear.PeriodTo = kendoparsetoDate($scope.CM00107.SelectedYear.PeriodTo);
    };
    $scope.AgentPeriodTargetLayer_Changed = function () {

        $http.get('/CMS_Calendar/GetCalendarPeriods?KaizenPublicKey=' + sessionStorage.PublicKey,
              {
                  params: {
                      CalendarID: $scope.CM00107.CalendarID
                      , YearCode: $scope.CM00107.SelectedYear.YearCode
                  }
              })
              .success(function (data) {
                  if (data.length > 0) {
                      $scope.CM00107List = data;
                      $scope.CM00107List.forEach(function (element, index) {
                          element.StartDate = kendoparsetoDate(element.StartDate);
                          element.EndDate = kendoparsetoDate(element.EndDate);
                          element.AgentID = $scope.CM00107.AgentID;
                          element.TargetAmount = 0;
                          element.Status = 0; //New
                      });
                  }
              });
    };
    $scope.TargetLayer_Changed = function () {
        $scope.CM00107List.forEach(function (element, index) {
            element.StartDate = kendoparsetoDate(element.StartDate);
            element.EndDate = kendoparsetoDate(element.EndDate);
            element.AgentID = $scope.CM00107.AgentID;
            element.TargetID = $scope.CM00107.SelectedTarget.TargetID;
            element.TargetName = $scope.CM00107.SelectedTarget.TargetName;
            element.TargetAmount = 0;
        });
        $http.get('/CMS_Agent/GetAgentTargetsInfo?KaizenPublicKey=' + sessionStorage.PublicKey,
     {
         params: {
             AgentID: $scope.CM00107.AgentID
             , YearCode: $scope.CM00107.SelectedYear.YearCode
             , TargetID: $scope.CM00107.SelectedTarget.TargetID
         }
     })
     .success(function (data) {
         if (data.length > 0) {
             $scope.temp = data;
             $scope.CM00107List.forEach(function (element, index) {
                 element.Status = 1; //Updateing
                 $scope.temp.forEach(function (element2, index2) {
                     if (element.PERIODID == element2.PERIODID) {
                         element.TargetAmount = element2.TargetAmount;
                     }
                 });
             });
             $scope.temp = null;
         }
     }).finally(function () { });

    };
    //-------------------------- Conditions Information
    $scope.CM00109 = {};
    $scope.SourceChanged = function () {
        $scope.CM00109.FieldName = $scope.CM00109.SelectedField.FieldName;
        if ($scope.CM00109.SelectedField.Type == 'LookUp') {
            if ($scope.CM00109.SelectedField.FieldName == 'CaseRef') {
                $scope.CM00109.SelectedField.html = "<div class='input-group'><input type='text' class='form-control' readonly " +
                    "placeholder='Case Reference' ng-model='CM00109.FieldValue' />" +
                    "<span class='input-group-btn input-group-sm'>" +
                    "<button class='btn btn-default' ng-click=\"OpenkendoWindow('CMS_Case','PopUp')\">" +
                    "<span class='glyphicon glyphicon-search'></span></button></span></div>";
            }
            else if ($scope.CM00109.SelectedField.FieldName == 'ContractID') {
                $scope.CM00109.SelectedField.html = "<div class='input-group'><input type='text' class='form-control' readonly " +
                    "placeholder='Contract' ng-model='CM00109.FieldValue' />" +
                    "<span class='input-group-btn input-group-sm'>" +
                    "<button class='btn btn-default' ng-click=\"OpenkendoWindow('CMS_Contract','PopUp')\">" +
                    "<span class='glyphicon glyphicon-search'></span></button></span></div>";
            }
            else if ($scope.CM00109.SelectedField.FieldName == 'ClientID') {
                $scope.CM00109.SelectedField.html = "<div class='input-group'><input type='text' class='form-control' readonly " +
                    "placeholder='Client' ng-model='CM00109.FieldValue' />" +
                    "<span class='input-group-btn input-group-sm'>" +
                    "<button class='btn btn-default' ng-click=\"OpenkendoWindow('CMS_CO_Client','PopUp',null,'AgentCondition')\">" +
                    "<span class='glyphicon glyphicon-search'></span></button></span></div>";
            }
            else if ($scope.CM00109.SelectedField.FieldName == 'AgentID') {
                $scope.CM00109.SelectedField.html = "<div class='input-group'><input type='text' class='form-control' readonly " +
                    "placeholder='Client' ng-model='CM00109.FieldValue' />" +
                    "<span class='input-group-btn input-group-sm'>" +
                    "<button class='btn btn-default' ng-click=\"OpenkendoWindow('CMS_Agent','PopUp',null,'AgentConditionField')\">" +
                    "<span class='glyphicon glyphicon-search'></span></button></span></div>";
            }
            else if ($scope.CM00109.SelectedField.FieldName == 'CIFNumber') {
                $scope.CM00109.SelectedField.html = "<div class='input-group'><input type='text' class='form-control' readonly " +
                    "placeholder='Client' ng-model='CM00109.FieldValue' />" +
                    "<span class='input-group-btn input-group-sm'>" +
                    "<button class='btn btn-default' ng-click=\"OpenkendoWindow('CMS_Debtor','PopUp')\">" +
                    "<span class='glyphicon glyphicon-search'></span></button></span></div>";
            }

        }
        else if ($scope.CM00109.SelectedField.Type == 'DropDown') {
            if ($scope.CM00109.SelectedField.FieldName == 'TRXTypeID') {
                $scope.CM00109.SelectedField.html = "<select kendo-drop-down-list k-filter=\"'startswith'\" k-option-label=\"'-- Select Product --'\" " +
                "k-ng-model=\"CM00109.FieldValue\" " +
                "k-data-text-field=\"'TrxTypeName'\" " +
                "k-value-primitive=\"true\" " +
                "k-data-value-field=\"'TRXTypeID'\" " +
                "k-data-source=\"TRXTypes\" " +
                "style=\"width: 100%\"></select>";
            }
            else if ($scope.CM00109.SelectedField.FieldName == 'TrxTypeName') {
                $scope.CM00109.SelectedField.html = "<select kendo-drop-down-list k-filter=\"'startswith'\" k-option-label=\"'-- Select Product --'\" " +
                "k-ng-model=\"CM00109.FieldValue\" " +
                "k-data-text-field=\"'TrxTypeName'\" " +
                "k-value-primitive=\"true\" " +
                "k-data-value-field=\"'TrxTypeName'\" " +
                "k-data-source=\"TRXTypes\" " +
                "style=\"width: 100%\"></select>";
            }
            else if ($scope.CM00109.SelectedField.FieldName == 'CaseStatusID') {
                $scope.CM00109.SelectedField.html = "<select kendo-drop-down-list k-filter=\"'startswith'\" k-option-label=\"'-- Select Status --'\" " +
                "k-ng-model=\"CM00109.FieldValue\" " +
                "k-data-text-field=\"'CaseStatusname'\" " +
                "k-value-primitive=\"true\" " +
                "k-data-value-field=\"'CaseStatusID'\" " +
                "k-data-source=\"CaseStatuses\" " +
                "style=\"width: 100%\"></select>";
            }
            else if ($scope.CM00109.SelectedField.FieldName == 'CaseStatusChild') {
                $scope.CM00109.SelectedField.html = "<select kendo-drop-down-list k-filter=\"'startswith'\" k-option-label=\"'-- Select Status --'\" " +
                "k-ng-model=\"CM00109.FieldValue\" " +
                "k-data-text-field=\"'CaseStatusname'\" " +
                "k-value-primitive=\"true\" " +
                "k-data-value-field=\"'CaseStatusID'\" " +
                "k-data-source=\"CaseStatuses\" " +
                "style=\"width: 100%\"></select>";
            }

        }
        else if ($scope.CM00109.SelectedField.Type == 'Text') {
            $scope.CM00109.SelectedField.html = "<input type='text' class='form-control' " +
    "placeholder='' ng-model='CM00109.FieldValue' />";
        }
        else if ($scope.CM00109.SelectedField.Type == 'Date') {
            $scope.CM00109.SelectedField.html = "<input kendo-date-picker k-format=\"'dd/MM/yyyy'\" type='text' class='form-control' " +
    "placeholder='' k-ng-model='CM00109.FieldValue' style='width: 100%;' />";
        }
        else if ($scope.CM00109.SelectedField.Type == 'Number') {
            $scope.CM00109.SelectedField.html = "<input type='number' class='form-control' " +
    "placeholder='' ng-model='CM00109.FieldValue' />";
        }
    };
    $scope.ContractBack = function (contract) {
        if (contract != null) {
            $scope.CM00109.FieldValue = contract.ContractID;
        }
    };
    $scope.DebtorBack = function (debtor) {
        if (debtor != null) {
            $scope.CM00109.FieldValue = debtor.DebtorID;
        }
    };

});