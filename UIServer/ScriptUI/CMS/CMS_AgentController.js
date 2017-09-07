app.controller('CMS_AgentController', function ($scope, $http, $window) {
    $scope.CM00105 = {};
    $scope.PagesCM00105 = [];
    $scope.CM00215 = {};
    $scope.SelectedLookUp = {};
    $scope.toolbarOptions = {
        items: [
                 {
                     type: "button", text: "<span class=\"fa fa-plus-circle\"></span> New Agent",
                     attributes: { "class": "btn-primary" },
                     click: function (e) {
                         $scope.$apply(function () {
                             $scope.AddCM00105();
                         });
                     }
                 },
                 {
                     type: "button", text: "<span class=\"fa fa-bar-chart-o\"></span> Orgnization Chart",
                     attributes: { "class": "btn-primary" },
                     click: function (e) {
                         $scope.$apply(function () {
                             $scope.OrgnizationChart();
                         });
                     }
                 },
                 {
                     type: "button", text: "<span class=\"fa fa-bars\"></span> Distribution rules",
                     attributes: { "class": "btn-primary" },
                     click: function (e) {
                         $scope.$apply(function () {
                             //$scope.AgentCondition();
                             $scope.DistributionRule();
                         });
                     }
                 },
                 {
                     type: "splitButton",
                     text: "Agent Target",
                     click: function (e) {
                         $scope.$apply(function () {
                             $scope.TargetAssignment();
                         });
                     },
                     menuButtons: [
                         {
                             text: "Target Assignment", click: function (e) {
                                 $scope.$apply(function () {
                                     $scope.TargetAssignment();
                                 });
                             }
                         },
                           {
                               text: "Agent Target Layer", click: function (e) {
                                   $scope.$apply(function () {
                                       $scope.TargetBYLayer();
                                   });
                               }
                           },
                         {
                             text: "Period Target", click: function (e) {
                                 $scope.$apply(function () {
                                     $scope.PeriodTarget();
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
                 },
                 {
                     type: "button", text: "<span class=\"fa fa-bar-chart-o\"></span> Agent Case Status",
                     attributes: { "class": "btn-primary" },
                     click: function (e) {
                         $scope.$apply(function () {
                             $scope.AgentCaseStatus();
                         });
                     }
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
                if ($scope.CM00105.TargetID != null || $scope.CM00105.TargetID != '') {
                    $scope.CM00105.SelectedTarget = $scope.GetSingle($scope.CM00108List, "TargetID", $scope.CM00105.TargetID);
                }
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
        $http.get('/CMS_Set_Target/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.CM00108List = data;
        }).finally(function () { });
        $http.get('/CMS_AgentGroup/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.AgentGroups = data;
        }).finally(function () { });
        $http.get('/CMS_Calendar/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.Calendars = data;
        }).finally(function () { });
        $http.get('/GL_Set_FiscalYears/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.Years = data;
        }).finally(function () { });
        $http.get('/HumanResources_Suffix/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.Suffixes = data;
        }).finally(function () { });
        $http.get('/Sys_Gender/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.agentGender = data;
        }).finally(function () { });
        $scope.GetBucketList();

    };
    $scope.LoadLookUp();
    $scope.SelectedTarget_Changed = function () {
        if ($scope.CM00105.SelectedTarget.TargetID == '') {
            $scope.CM00105.TargetID = null;
            $scope.CM00105.IsPercentTarget = null;
            $scope.CM00105.TargetTypeID = null;
        } else {
            $scope.CM00105.TargetID = $scope.CM00105.SelectedTarget.TargetID;
            $scope.CM00105.IsPercentTarget = $scope.CM00105.SelectedTarget.IsPercentTarget;
            $scope.CM00105.TargetTypeID = $scope.CM00105.SelectedTarget.TargetTypeID;
        }
    };
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
    // Setting Photo path extension
    $scope.SetPhotoExtension = function (PhotoPath) {
        $scope.CM00105.PhotoExtension = PhotoPath;
        $scope.link = '/Photo/AgentPhotoTemp/' + kaizenTrim(PhotoPath);
        $scope.PhotoChanged = true;
    };
    $scope.RemovePhotoExtension = function (PhotoPath) {
        $scope.CM00105.PhotoExtension = "";
    };

    $scope.SetSignatureExtension = function (PhotoPath) {
        $scope.CM00105.SignatureExtension = PhotoPath;
        $scope.Signaturelink = '/Photo/AgentSignatureTemp/' + kaizenTrim(PhotoPath);
        $scope.SignatureChanged = true;
    };
    $scope.RemoveSignatureExtension = function (PhotoPath) {
        $scope.CM00105.SignatureExtension = "";
    };
    //------------------------Filter
    $scope.MYFilter.get = function (field) {
        var DS = {};
        if (field == 'AgentGroup') {
            DS.key = "AgentGroupName";
            DS.source = $scope.AgentGroups;
        }
        //if (field == 'UserLevelID') {
        //    DS.key = "UserLevelName";
        //    DS.source = $scope.UserLevels;
        //}
        if (field == 'SuffixID') {
            DS.key = "SuffixINam";
            DS.source = $scope.Suffixes;
        }
        return DS;
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
                case 'TargetBYLayer':
                    $scope.CM00107.AgentID = agent.AgentID;
                    $scope.CM00107.FullName = agent.FullName;
                    $scope.CM00107.CalendarID = agent.CalendarID;
                    //alert($scope.SelectedLookUp.SelectedTarget.TargetID);
                    $http.get('/CMS_Agent/GetAgentTargetsInfo?KaizenPublicKey=' + sessionStorage.PublicKey,
                    {
                        params: {
                            AgentID: $scope.CM00107.AgentID
                            , YearCode: $scope.CM00107.SelectedYear.YearCode
                            , TargetID: $scope.SelectedLookUp.SelectedTarget.TargetID
                        }
                    })
                    .success(function (data) {
                        $scope.CM00107List = data;
                        //alert(JSON.stringify($scope.CM00107List, null, 2));
                        $http.get('/CMS_Calendar/GetCalendarPeriods?KaizenPublicKey=' + sessionStorage.PublicKey,
                         {
                             params: {
                                 CalendarID: $scope.CM00107.CalendarID
                                 , YearCode: $scope.CM00107.SelectedYear.YearCode
                             }
                         })
                         .success(function (result) {
                             if (result.length > 0) {
                                 $scope.Temp = result;
                                 $scope.CM00107List.forEach(function (element, index) {
                                     $scope.Temp.forEach(function (element2, index2) {
                                         if (element.PERIODID == element2.PERIODID) {
                                             element.StartDate = kendoparsetoDate(element2.StartDate);
                                             element.EndDate = kendoparsetoDate(element2.EndDate);
                                             element.AgentID = $scope.CM00107.AgentID;
                                             element.TargetAmount = 0;
                                             element.Status = 0; //New
                                             return;
                                         }
                                     });


                                 });
                             }
                         });
                    }).finally(function () { });
                    break;
                case 'Main':
                    $scope.CM00105.AgentID = agent.AgentID;
                    break;
                case 'Supervisor':
                    $scope.CM00105.SupervisorFullName = agent.FullName;
                    $scope.CM00105.SupervisorID = agent.AgentID;
                    break;
                case 'CM00107List':
                    $scope.CM00107.AgentID = agent.AgentID;
                    $scope.CM00107.FullName = agent.FullName;
                    $scope.CM00107.CalendarID = agent.CalendarID;
                    $scope.AgentPeriodTargetLayer_Changed();
                    //$scope.LoadTargetTypes_BYLayer();

                    $scope.LoadTargetAssignmentData($scope.CM00107.AgentID);

                    break;
                case 'CM10110List':
                    $scope.CM10110.AgentID = agent.AgentID;
                    $scope.CM10110.FullName = agent.FullName;
                    $scope.CM10110.CalendarID = agent.CalendarID;
                    $scope.CM10110.TargetTypeID = agent.TargetTypeID;
                    $scope.CM10110.IsPercentTarget = agent.IsPercentTarget;
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
                case 'AgentTargetAssignment':
                    $scope.CM00109.AgentID = agent.AgentID;
                    $scope.LoadTargetAssignmentData($scope.CM00109.AgentID);
                    break;
                case 'DistributionRuleUser':
                    $scope.CM00105.Status=0;
                    $scope.CM00105.AgentID = agent.AgentID;
                    $scope.LoadCM00105DistributionRule($scope.CM00105.AgentID);
                    break;                   
            }
        }
    };
    $scope.UserBack = function (user) {
        if (user == null) return;
        switch ($scope.CurrentControl) {
            case 'AgentID':
                $scope.CM00105.UserCode = user.UserName;
                $scope.CM00105.AgentID = user.UserName;
                break;
            case 'SupervisorID':
                $scope.CM00105.SupervisorID = user.UserName;
                break;
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
    ////////////// Target //////////////





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

    //Target Assignment
    $scope.TargetAssignment = function () {
        $scope.LoadCM00105Page('TargetAssignment');
        $scope.Clear();
        $http.get('/CMS_Set_Target/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.TargetTypes = data;
        }).finally(function () { });
    };
    $scope.TargetID_ChangedAssignment = function () {
        if ($scope.SelectedLookUp.SelectedTarget != undefined) {
            $scope.CM00109.TargetID = $scope.SelectedLookUp.SelectedTarget.TargetID;
            $scope.CM00109.TargetTypeID = $scope.SelectedLookUp.SelectedTarget.TargetTypeID;
            $scope.CM00109.IsPercentTarget = $scope.SelectedLookUp.SelectedTarget.IsPercentTarget;
            $scope.CM00109.SQLCondition = $scope.SelectedLookUp.SelectedTarget.SQLCondition;
        }
    };

    $scope.AddTargetAssignment = function () {
        $scope.CM00109.status = 1;
        if ($scope.CM00109) {
            if ($scope.CM00109.TargetID == undefined) {
                $.bigBox({
                    title: "Error",
                    content: "Target ID is missing",
                    color: "#C46A69",
                    timeout: 8000,
                    icon: "fa fa-bolt swing animated"
                });
                return;
            }
            if ($scope.CM00109.AgentID == undefined) {
                $.bigBox({
                    title: "Error",
                    content: "Agent ID is missing",
                    color: "#C46A69",
                    timeout: 8000,
                    icon: "fa fa-bolt swing animated"
                });
                return;
            }

            $http({
                url: '/CMS_Agent/SaveAgentCondition?KaizenPublicKey=' + sessionStorage.PublicKey,
                method: "POST",
                data: JSON.stringify($scope.CM00109),
                dataType: "json",
                headers: {
                    'Content-Type': 'application/json; charset=utf-8'
                }
            }).success(function (data) {
                if (data.Status == true) {
                    $scope.TargetAssignmentList.push($scope.CM00109);

                    $.bigBox({
                        title: data.Massage,
                        content: data.Description,
                        color: "#739E73",
                        timeout: 8000,
                        icon: "fa fa-bolt swing animated"
                    });

                } else {
                    $.bigBox({
                        title: data.Massage,
                        content: data.Description,
                        color: "#C46A69",
                        timeout: 8000,
                        icon: "fa fa-bolt swing animated"
                    });
                    return;
                }
            }).error(function (data, status, headers, config) {
                $.bigBox({
                    title: "Error",
                    content: "Unable to save data",
                    color: "#C46A69",
                    timeout: 8000,
                    icon: "fa fa-bolt swing animated"
                });
            });
        }
    };

    $scope.DeleteTargetAssignment = function (obj, index) {
        $.SmartMessageBox({
            title: "Delete Record",
            content: "Are you sure you want to delete this?",
            buttons: '[No][Yes]'
        }, function (ButtonPressed) {
            if (ButtonPressed === "Yes") {
                $http.post('/CMS_Agent/DeleteAgentCondition', { 'CM00109': obj, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
                    if (data.Status == true) {
                        $.bigBox({
                            title: data.Massage,
                            content: data.Description,
                            color: "#739E73",
                            timeout: 3000,
                            icon: "fa fa-check shake animated"
                        });
                        $scope.TargetAssignmentList.splice(index, 1);
                        $scope.GridRefresh('GridCMS_AgentConditionByAgent');
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
                    color: "#3276B1",
                    iconSmall: "fa fa-times fa-2x fadeInRight animated",
                    timeout: 4000
                });
            }
        });
    };

    $scope.LoadTargetAssignmentData = function (AgentID) {
        $http.get('/CMS_Agent/GetAgentConditions?KaizenPublicKey=' + sessionStorage.PublicKey,
            { params: { AgentID: AgentID } }).success(function (data) {
                if (data && data.length > 0)
                    $scope.TargetAssignmentList = data;
                else
                    $scope.TargetAssignmentList = [];
            });
    };

    //TargetBYLayer    
    $scope.TargetBYLayer = function () {
        $scope.LoadCM00105Page('TargetBYLayer');
        $scope.Clear();
        //$scope.LoadTargetAssignmentData();
        $http.get('/CMS_Set_Target/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.TargetLayers = data;
        }).finally(function () { });
    };
    $scope.YearTargetBYLayer_Changed = function () {
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
    $scope.SaveTargetInfo = function () {
        //$scope.CM00107List.forEach(function (element, index) {
        //    element.StartDate = kendoparsetoDate(element.StartDate);
        //    element.EndDate = kendoparsetoDate(element.EndDate);
        //    element.AgentID = $scope.CM00107.AgentID;
        //    element.TargetID = $scope.CM00107.SelectedTarget.TargetID;
        //    element.TargetName = $scope.CM00107.SelectedTarget.TargetName;
        //});
        $http({
            url: '/CMS_Agent/SaveTargetInfo?KaizenPublicKey=' + sessionStorage.PublicKey,
            method: "POST",
            data: JSON.stringify($scope.CM00107List),
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
                $scope.Clear();
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


    //ClientTarget 
    $scope.CM00212 = {};

    $scope.YearClient_Changed = function () {
        $scope.CM00212.YearCode = $scope.CM00212.SelectedYear.YearCode;
        $scope.CM00212.SelectedYear.PeriodFrom = kendoparsetoDate($scope.CM00212.SelectedYear.PeriodFrom);
        $scope.CM00212.SelectedYear.PeriodTo = kendoparsetoDate($scope.CM00212.SelectedYear.PeriodTo);
    };

    //TargetByPeriod 
    $scope.PeriodTarget = function () {
        $scope.LoadCM00105Page('TargetByPeriod');
        $scope.Clear();
    };
    $scope.YearPeriodTarget_Changed = function () {
        $scope.CM10110.YearCode = $scope.CM10110.SelectedYear.YearCode;
        $scope.CM10110.SelectedYear.PeriodFrom = kendoparsetoDate($scope.CM10110.SelectedYear.PeriodFrom);
        $scope.CM10110.SelectedYear.PeriodTo = kendoparsetoDate($scope.CM10110.SelectedYear.PeriodTo);
    };
    $scope.AgentPeriodTarget_Changed = function () {
        $http.get('/CMS_Calendar/GetCalendarPeriods?KaizenPublicKey=' + sessionStorage.PublicKey,
            {
                params: {
                    CalendarID: $scope.CM10110.CalendarID
                    , YearCode: $scope.CM10110.SelectedYear.YearCode
                }
            })
            .success(function (data) {
                if (data.length > 0) {
                    $scope.CM00007List = data;
                    $scope.CM00007List.forEach(function (element, index) {
                        element.StartDate = kendoparsetoDate(element.StartDate);
                        element.EndDate = kendoparsetoDate(element.EndDate);
                    });
                }
            }).finally(function () {

                $http.get('/CMS_Agent/GetTargetByAgentID?KaizenPublicKey=' + sessionStorage.PublicKey,
                   {
                       params: {
                           AgentID: $scope.CM10110.AgentID
                           , YearCode: $scope.CM10110.SelectedYear.YearCode
                       }
                   })
                   .success(function (data) {
                       if (data.length > 0) {
                           $scope.CM10110List = data;
                           $scope.CM10110List.forEach(function (element, index) {
                               //element.Status = 1; //Updateing
                               //element.AgentID = $scope.CM10110.AgentID;
                               //element.IsClosed = 1;
                               $scope.CM00007List.forEach(function (element2, index2) {
                                   if (element.PERIODID == element2.PERIODID) {
                                       element.StartDate = kendoparsetoDate(element2.StartDate);
                                       element.EndDate = kendoparsetoDate(element2.EndDate);
                                       return;
                                   }
                               });
                           });
                           $scope.temp = null;
                       }
                   });

            });
    };

    $scope.SavePeriodTargetInfo = function () {
        $http({
            url: '/CMS_Agent/SavePeriodTargetInfo?KaizenPublicKey=' + sessionStorage.PublicKey,
            method: "POST",
            data: JSON.stringify($scope.CM10110List),
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
        }).error(function (data, status, headers, config) { });
    };

    //------------------ Distribution Rule -----------
    $scope.DistributionRule = function () {
        //removeEntityPage($scope.PagesCM00105);
        $scope.LoadCM00105Page('DistributionRule');
        $scope.CM00109.Status = 0;
    };

    $scope.AddDistributionRule = function () {
        if ($scope.CM00105) {
            if($scope.CM00105.AgentID ==undefined || $scope.CM00105.AgentID == "")
            {
                $.bigBox({
                    title: "Error",
                    content: "Condition value is missing",
                    color: "#C46A69",
                    timeout: 8000,
                    icon: "fa fa-bolt swing animated"
                });
                return;
            }
            if($scope.CM00105.WhereCondition ==undefined || $scope.CM00105.WhereCondition == "")
            {
                $.bigBox({
                    title: "Error",
                    content: "Condition value is missing",
                    color: "#C46A69",
                    timeout: 8000,
                    icon: "fa fa-bolt swing animated"
                });
                return;
            }
            $http({
                url: '/CMS_Agent/SaveWhereCondition?KaizenPublicKey=' + sessionStorage.PublicKey,
                method: "POST",
                data: { "AgentID": $scope.CM00105.AgentID, "WhereCondition": $scope.CM00105.WhereCondition },
                dataType: "json",
                headers: {
                    'Content-Type': 'application/json; charset=utf-8'
                }
            }).success(function (data) {
                if (data.Status == true) {
                    $scope.CaseTypeGraphList.push($scope.CM00076);
                    $scope.CM00076 = {};
                    $scope.CM00076.status = 0;

                    $.bigBox({
                        title: data.Massage,
                        content: data.Description,
                        color: "#739E73",
                        timeout: 8000,
                        icon: "fa fa-bolt swing animated"
                    });
                } else {
                    $.bigBox({
                        title: data.Massage,
                        content: data.Description,
                        color: "#C46A69",
                        timeout: 8000,
                        icon: "fa fa-bolt swing animated"
                    });
                    return;
                }
            }).error(function (data, status, headers, config) {
                $.bigBox({
                    title: "Error",
                    content: "Unable to save data",
                    color: "#C46A69",
                    timeout: 8000,
                    icon: "fa fa-bolt swing animated"
                });
            });
        }
    };

    $scope.LoadCM00105DistributionRule = function(AgentID)
    {
        $http.get('/CMS_Agent/GetSingle?KaizenPublicKey=' + sessionStorage.PublicKey + "&AgentID=" + AgentID).success(function (data) {
            $scope.CM00105 = data;
            $scope.CM00105.Status=1;
        }).finally(function () {
            
        });
    };
    //-------------------------- Conditions Information
    $scope.CM00109 = {};
    $scope.AgentCondition = function () {
        removeEntityPage($scope.PagesCM00105);
        $scope.LoadCM00105Page('AgentCondition');
        $scope.CM00109.Status = 1;
        $scope.CaseKaizenColumns = [
            { FieldName: "CaseRef", FieldDisplay: "Case Reference", Type: "LookUp" },
            { FieldName: "CIFNumber", FieldDisplay: "CIF No.", Type: "LookUp" },
            { FieldName: "ClaimAmount", FieldDisplay: "Claim Amount", Type: "Number" },
            { FieldName: "TrxTypeName", FieldDisplay: "Product Name", Type: "DropDown" },
            { FieldName: "AgentID", FieldDisplay: "Staff ID", Type: "LookUp" },
            { FieldName: "CaseAccountNo", FieldDisplay: "CaseAccountNo", Type: "Text" },
            { FieldName: "BucketID", FieldDisplay: "BucketID", Type: "Text" },
            { FieldName: "InvoiceNumber", FieldDisplay: "InvoiceNumber", Type: "Text" },
            { FieldName: "PrincipleAmount", FieldDisplay: "Principle Amount", Type: "Number" },
            { FieldName: "OSAmount", FieldDisplay: "Principle OS", Type: "Number" },
            { FieldName: "CYCLEDAY", FieldDisplay: "Cycle Day", Type: "Number" },
            { FieldName: "CYCLE", FieldDisplay: "Cycle", Type: "Number" },
            { FieldName: "LastCallactedAMT", FieldDisplay: "Last Payment", Type: "Number" },
            { FieldName: "LastPaymentDate", FieldDisplay: "Last Payment Date", Type: "Date" },
            { FieldName: "ClosingDate", FieldDisplay: "Closing Date", Type: "Date" },
            { FieldName: "TransactionDate", FieldDisplay: "TransactionDate", Type: "Date" },
            { FieldName: "Remarks", FieldDisplay: "Remark", Type: "Text" },
            { FieldName: "BookingDate", FieldDisplay: "Booking Date", Type: "Date" },
            { FieldName: "CaseAddess", FieldDisplay: "CaseAddess", Type: "Text" },
            { FieldName: "WebSite", FieldDisplay: "WebSite", Type: "Text" },
            { FieldName: "CountryID", FieldDisplay: "CountryID", Type: "DropDown" },
            { FieldName: "CityID", FieldDisplay: "CityID", Type: "DropDown" },
            { FieldName: "Phone01", FieldDisplay: "Phone01", Type: "Text" },
            { FieldName: "Phone02", FieldDisplay: "Phone02", Type: "Text" },
            { FieldName: "Ext1", FieldDisplay: "Ext1", Type: "Text" },
            { FieldName: "Ext2", FieldDisplay: "Ext2", Type: "Text" },
            { FieldName: "MobileNo1", FieldDisplay: "MobileNo1", Type: "Text" },
            { FieldName: "MobileNo2", FieldDisplay: "MobileNo2", Type: "Text" },
            { FieldName: "POBox", FieldDisplay: "POBox", Type: "Text" },
            { FieldName: "Other01", FieldDisplay: "Other01", Type: "Text" },
            { FieldName: "Other02", FieldDisplay: "Other02", Type: "Text" },
            { FieldName: "Address1", FieldDisplay: "Address1", Type: "Text" },
            { FieldName: "Address2", FieldDisplay: "Address2", Type: "Text" },
            { FieldName: "Email01", FieldDisplay: "Email01", Type: "Text" },
            { FieldName: "Email02", FieldDisplay: "Email02", Type: "Text" },
            { FieldName: "CaseStatusID", FieldDisplay: "Case Status", Type: "DropDown" },
            { FieldName: "CaseStatusChild", FieldDisplay: "Case Status Child", Type: "DropDown" },
            { FieldName: "PTPAMT", FieldDisplay: "PTP Amount", Type: "Number" },
            { FieldName: "ClaimAmount", FieldDisplay: "Claim Amount", Type: "Number" },
            { FieldName: "FinanceCharge", FieldDisplay: "Finance Charge", Type: "Number" }
        ];
        $scope.CM00109.SelectedField = {};
        $http.get('/CMS_CaseType/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.TRXTypes = data;
        }).finally(function () { });
        $http.get('/CMS_CaseStatus/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.CaseStatuses = data;
        }).finally(function () { });
        $http.get('/Adm_Country/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.Countries = data;
        }).finally(function () { });
        $http.get('/Adm_City/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.Cities = data;
        }).finally(function () { });

    };
    $scope.SaveAgentCondition = function () {
        $scope.CM00109.AgentID = $scope.CM00105.AgentID;
        $http.post('/CMS_Agent/SaveAgentCondition', { 'CM00109': $scope.CM00109, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
            if (data.Status == true) {
                $.bigBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#739E73",
                    timeout: 3000,
                    icon: "fa fa-check shake animated"
                });
                $scope.GridRefresh('GridCMS_AgentConditionByAgent');
                $scope.CM00109 = { Status: 1, AgentID: $scope.CM00105.AgentID };
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
    $scope.UpdateAgentCondition = function () {
        $scope.CM00109.AgentID = $scope.CM00105.AgentID;
        $http.post('/CMS_Agent/UpdateAgentCondition', { 'CM00109': $scope.CM00109, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
            if (data.Status == true) {
                $.bigBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#739E73",
                    timeout: 3000,
                    icon: "fa fa-check shake animated"
                });
                $scope.GridRefresh('GridCMS_AgentConditionByAgent');
                $scope.CM00109 = { Status: 1, AgentID: $scope.CM00105.AgentID };
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
    $scope.DeleteAgentCondition = function () {
        $.SmartMessageBox({
            title: "Delete Record",
            content: "Are you sure you want to delete this?",
            buttons: '[No][Yes]'
        }, function (ButtonPressed) {
            if (ButtonPressed === "Yes") {
                $http.post('/CMS_Agent/DeleteAgentCondition', { 'CM00109': $scope.CM00109, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
                    if (data.Status == true) {
                        $.bigBox({
                            title: data.Massage,
                            content: data.Description,
                            color: "#739E73",
                            timeout: 3000,
                            icon: "fa fa-check shake animated"
                        });
                        $scope.GridRefresh('GridCMS_AgentConditionByAgent');
                        $scope.CM00109 = { Status: 1 };
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
                    color: "#3276B1",
                    iconSmall: "fa fa-times fa-2x fadeInRight animated",
                    timeout: 4000
                });
                $scope.CM00109 = { Status: 1 };
            }
        });
    };
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
    $scope.CaseBack = function (caseObj) {
        if (caseObj != null) {
            $scope.CM00109.FieldValue = caseObj.CaseRef
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

    //------------------------- Agent Case Status ---------------------------
    $scope.CM00203 = {};
    $scope.Agents = [];
    $scope.AgentCaseStatus = function () {
        $scope.LoadCM00105Page('AgentCaseStatus');
        $scope.Clear();
        $scope.CM00203.status = 0;

        $http.get('/CMS_Agent/GetAllAgents?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.Agents = data;

            var res = [];
            $.each(data, function (i, v) {
                res.push({ text: v.FirstName, value: v.AgentID });
            });
            var resource = [{
                field: "AgentID",
                name: "AgentID",
                dataSource: res,
                title: "Agent ID"
            }];

            $window.resources = resource;
        });
    };


});