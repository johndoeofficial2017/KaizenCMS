app.controller('CMS_ReminderController', function ($scope, $http) {
    $scope.SelectedAgent = {};
    //alert($scope.MY.AgentID);
    $scope.SelectedAgent.AgentID = $scope.MY.AgentID;
    $scope.FromReminder = new Date();
    $scope.ToReminder = new Date();
    $scope.maxDate = new Date(2050, 0, 1, 0, 0, 0);
    $scope.IsOverDue = false;
    $scope.AgentList = [];
    $scope.IndexPath = null;
    $scope.MainGridURL = null;
    $scope.DebtorPaymentPath = null;
    $scope.ManualPaymentPath = null;
    $scope.CaseViewPath = null; 
    $scope.GoTo = function (PagePath) {
        //alert(PagePath)
        if (PagePath == null || PagePath == undefined || angular.isUndefined(PagePath))
            PagePath = $scope.ToolBar.ActiveScreen.CurrentWindow;
        $scope.ToolBar.ActiveScreen.CurrentWindow = PagePath;
        switch (PagePath) {
            case "DebtorPayment":
                $scope.CM00207.Total_Amount = 0;
                if ($scope.DebtorPaymentPath != null) {
                    return;
                }
                $scope.DebtorPaymentPath = "/CMS_Reminder/DebtorPayment?KaizenPublicKey=" + sessionStorage.PublicKey;
                $scope.InitializeDebtorPayment();
                break;
            case "ManualPayment":
                $scope.CM00307.Total_Amount = 0;
                if ($scope.ManualPaymentPath != null) {

                    return;
                }
                $scope.ManualPaymentPath = "/CMS_Reminder/ManualPayment?KaizenPublicKey=" + sessionStorage.PublicKey;

                $scope.InitializeManualPayment();
                break;
            case "CaseView":
                if ($scope.CaseViewPath != null)
                    return;
                $scope.CaseView_toolbarOptions = {
                    items: [
                             {
                                 type: "splitButton",
                                 spriteCssClass: "k-tool-icon k-i-pdf",
                                 text: "PDF",
                                 click: function (e) {
                                     $scope.ExportScreen('CMS_Case_Create', 'PDF', "Case" + $scope.CM00203.CaseRef.trim());
                                 },
                                 menuButtons: [
                                        { spriteCssClass: "k-tool-icon k-i-pdf", text: "PDF", click: function (e) { $scope.ExportScreen('CMS_Case_Create', 'PDF', "Case" + $scope.CM00203.CaseRef.trim()); } },
                                        { spriteCssClass: "k-icon k-insertImage", text: "Image", click: function (e) { $scope.ExportScreen('CMS_Case_Create', 'Image', "Case" + $scope.CM00203.CaseRef.trim()); } },
                                        { spriteCssClass: "k-icon k-insertImage", text: "SVG", click: function (e) { $scope.ExportScreen('CMS_Case_Create', 'SVG', "Case" + $scope.CM00203.CaseRef.trim()); } },
                                        { spriteCssClass: "k-icon k-insertImage", text: "Send Email", click: function (e) { } }
                                 ]
                             },
                             {
                                 type: "button", text: "<span class=''></span>Cancel"
                                    , spriteCssClass: "k-tool-icon k-cancel",
                                 attributes: { "class": "btn btn-default btn-sm" },
                                 click: function (e) {
                                     $scope.$apply(function () {
                                         $scope.GoTo('MainGrid');
                                     });
                                 }
                             },
                             {
                                 type: "button", text: "<strong>Total: <span class='text-success'>" + $scope.CM00203.CurrencyCode != undefined && $scope.CM00203.CurrencyCode != '' ? $scope.MY.CurrencyCode.trim() : $scope.CM00203.CurrencyCode.trim() + " " + $scope.CM00203.ClaimAmount.toFixed($scope.CM00203.DecimalDigit) + "</span></strong>",
                                 click: function (e) {
                                     $scope.$apply(function () {
                                         $scope.CasePayment();
                                     });
                                 }
                             },
                             {
                                 type: "button", text: "Manual Payment",
                                 click: function (e) {
                                     $scope.$apply(function () {
                                         $scope.OpenManualPaymentWindow();
                                     });
                                 }
                             },
                    ]
, resizable: true
                };
                $scope.CaseViewPath = "/CMS_Reminder/CaseView?KaizenPublicKey=" + sessionStorage.PublicKey;
                $http.get('/Adm_Country/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
                    $scope.countries = data;
                }).finally(function () { });
                $http.get('/Adm_City/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
                    $scope.cities = data;

                }).finally(function () { });
                $scope.LoadLookUp();
                $http.get('/CMS_Debtor/DebtorAddressTypeFillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
                    $scope.AddressTypes = data;
                }).finally(function () {
                    //$scope.LoadScreen(URIPath, 'CaseView');
                });
                break;
            case "MainGrid":
                if ($scope.MainGridURL != null) {
                    //$scope.$apply();
                    return;
                }
                $scope.MainGridURL = "/CMS_Reminder/MainGrid?KaizenPublicKey=" + sessionStorage.PublicKey;
                $http.get('/CMS_Agent/GetAgentListBySupervisor?KaizenPublicKey=' + sessionStorage.PublicKey,
                {
                    params: {
                        AgentID: $scope.MY.AgentID,
                        StartDate: new Date($scope.FromReminder),
                        EndDate: new Date($scope.ToReminder)
                    }
                }).success(function (data) {
                $scope.AgentList = data;
                $scope.CMS_RemindertoolbarOptions = {
                    items: [
                        {
                            type: "splitButton",
                            text: "Schedule",
                            click: function (e) {
                                $scope.OpenSchedule();
                            },
                            menuButtons: [
                            {
                                text: "Schedule", click: function (e) {
                                    $scope.OpenSchedule();
                                }
                            },
                            {
                                text: "Tasks",
                                click: function (e) {
                                    $scope.OpenTaskSchedule();
                                }
                            },
                            {
                                text: "Team Schedule",
                                click: function (e) {
                                    $scope.OpenTeamSchedule();
                                }
                            }
                            ]
                        },
                        { type: "separator" },
                        {
                            template: "<span ng-hide='IsOverDue'><label>From:</label><input kendo-date-picker k-format='format_date' k-ng-model='FromReminder' k-on-change='onReminderDateFilterChange()' /></span>"
                        },
                        {
                            template: "<span><label>TO:</label><input kendo-date-picker k-format='format_date' k-max='maxDate' k-rebind='maxDate'  k-ng-model='ToReminder' k-on-change='onReminderDateFilterChange()' /></span>",
                            overflow: "never"
                        },
                        { type: "separator" },
                        { template: "<label>Agent:</label>" },
                        {
                            template: "<select kendo-drop-down-list k-ng-model='SelectedAgent' k-on-change='AgentChanged()' k-options='formatOptions' id='GridViewoption_CMS_Case' style='width: 120px;'></select>",
                            overflow: "never"
                        },
                        {
                            type: "button", text: "Over Due", togglable: true,
                            toggle: function (e) {
                                //alert(e.target.text());
                                //alert($scope.IsOverDue);
                                if (e.checked) {
                                    $scope.$apply(function () {
                                        $scope.IsOverDue = true;
                                        $scope.ToReminder = new Date();
                                        $scope.maxDate = new Date();
                                    });
                                }
                                else {
                                        $scope.$apply(function () {
                                        $scope.IsOverDue = false;
                                        $scope.maxDate = new Date(2050, 0, 1, 0, 0, 0);
                                    });
                                }
                                $scope.onReminderDateFilterChange();
                            }
                        },
                        {
                            type: "splitButton",
                            spriteCssClass: "k-tool-icon k-i-excel",
                            text: "Export",
                            click: function (e) {
                                $scope.SaveAsExcel('GridCMS_Reminder');
                            },
                            menuButtons: [
                            { spriteCssClass: "k-tool-icon k-i-excel", text: "Excel", click: function (e) { $scope.SaveAsExcel('GridCMS_Reminder'); } },
                            { spriteCssClass: "k-tool-icon k-i-pdf", text: "PDF", click: function (e) { $scope.SaveAsPdf('GridCMS_Reminder'); } }
                            ]
                        },
                        { type: "separator" },
                        {
                            type: "button", text: "<span class=\"fa fa-money\"></span> Payments",
                            click: function (e) {
                                $scope.$apply(function () {
                                    $scope.GoTo('DebtorPayment');
                                });
                            }
                        },
                        {
                            type: "button", text: "<span class=\"fa fa-money\"></span> Manual Payments",
                            click: function (e) {
                                $scope.$apply(function () {
                                    $scope.GoTo('ManualPayment');
                                });
                            }
                        },
                        { type: "separator" },
                        {
                            type: "button", text: "<span class=\"fa fa-filter\"></span> Search",
                            click: function (e) {
                                $scope.$apply(function () {
                                    $scope.OpenSearchWindow();
                                });
                            }
                        },
                    ]
                , resizable: true
                };
                $scope.formatOptions = {
                    filter: "contains",
                    model: "SelectedAgent",
                    dataTextField: "AgentID",
                    dataValueField: "AgentID",
                    dataSource: $scope.AgentList,
                    value: $scope.MY.AgentID
                };
            }).finally(function () { });
                break;
        }
    };
    $scope.GetNextPaymentNumber = function () {
        if ($scope.CM00207.CheckbookCode == null
            || $scope.CM00207.CheckbookCode == '') {
            return;
        }
        $http.get('/CMS_Payment/GetNextPaymentNumber?KaizenPublicKey=' + sessionStorage.PublicKey,
            {
                params: {
                    PaymentMethodID: $scope.CM00207.PaymentMethodID,
                    CheckbookCode: $scope.CM00207.CheckbookCode
                }
            })
            .success(function (data) {
                $scope.CM00207.PaymentNumber = data;
            }).finally(function () { });
    };
    $scope.GetNextPaymentTransactionNumber = function () {
        $http.get('/CMS_Payment/GetNextPaymentTransactionNumber?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.CM00207.PaymentID = data;
        }).finally(function () { });
    };
    //var found = $filter('filter')($scope.templates, { ID: $scope.ToolBar.ActiveScreen.CurrentWindow }, true);
    //var CurrentWindow = $scope.ToolBar.ActiveScreen.CurrentWindow;
    //if ($scope.ToolBar.ActiveScreen.CurrentWindow == 'CMS_Reminder')
    //    $scope.GoTo('MainGrid');


    $scope.onReminderDateFilterChange = function () {
        $scope.GridRefresh('GridCMS_Reminder');
    };
    $scope.AgentChanged = function () {
        $scope.onReminderDateFilterChange();
    };

    $scope.OpenSearchWindow = function () {

        $scope.SearchWindow.center().toFront().open();
        //var grid = $("#GridCMS_Reminder").data("kendoGrid");
        //var arr = [
        //    { operator: 'contains', field: 'DebtorID', value: $scope.SearchStr },
        //    { operator: 'contains', field: 'CaseAccountNo', value: $scope.SearchStr }
        //];
        //grid.dataSource.filter({ logic: "or", filters: arr });
    };

    $scope.CurrentScreenView = null;
    $scope.CaseBack = function (caseObj) {
        if (caseObj != null) {
            switch ($scope.CurrentControl) {
                case 'ReminderCase':
                    $scope.CM00203 = caseObj;
                    $scope.Event.CaseRef = caseObj.CaseRef;
                    $scope.Event.CaseStatusID = caseObj.CaseStatusID;
                    $scope.Event.CaseStatusComment = caseObj.CaseStatusComment;
                    $scope.Event.PTPAMT = caseObj.PTPAMT;
                    $scope.Event.ReminderDateTime = caseObj.ReminderDateTime;
                    $scope.Event.AgentID = caseObj.AgentID;
                    var indx = $scope.functiontofindIndexByKeyValue($scope.CaseStatuses, "CaseStatusID", $scope.Event.CaseStatusID);
                    var obj = $scope.CaseStatuses[indx];
                    $scope.Event.SelectedStatus = obj;
                    $scope.EvenDatatWindow.refresh();
                    break;
                case 'TaskCase':
                    $scope.Task.CaseRef = caseObj.CaseRef;
                    break;
                case 'ManualPayment':
                    $scope.CM00203 = caseObj;
                    $scope.CM00308.CaseRef = caseObj.CaseRef;
                    $scope.CM00308.CurrencyCode = caseObj.CurrencyCode;
                    $scope.CM00308.ClaimAmount = caseObj.ClaimAmount;
                    break;
                case 'Main':
                    $scope.CM00203 = caseObj;
                    var grid = $("#GridCMS_CaseRelatedToDebtor").data("kendoGrid");
                    grid.dataSource.read();
                    $scope.OpenDebtorDetails($scope.CM00203.DebtorID);
                    $scope.OpenDebtorAddressDetails($scope.CM00203.DebtorID, $scope.CM00203.AddressCode);
                    break;
                case 'CasePayment':
                    $scope.CM00203 = caseObj;
                    $scope.CM00207.CaseRef = caseObj.CaseRef;
                    $scope.CM00207.DebtorID = caseObj.DebtorID;
                    $scope.GridRefresh("GridCMS_Case_CasePayment");
                    break;
            }
        }
    };
    $scope.Test = function () {
        alert('dd');
        pageSetUp();
    };

    $scope.Screens = [];
    $scope.LoadScreen = function (Path, ActionName) {
        var Page = { url: Path, ActionName: ActionName };
        $scope.Screens.push(Page);
    };
    $scope.LoadReminderPage = function (ActionName) {
        removeEntityPage($scope.Screens);
        var URIPath = "/" + $scope.ToolBar.ActiveScreen.ID + "/" + ActionName + "?KaizenPublicKey=" + sessionStorage.PublicKey;
        var Page = { url: URIPath, ActionName: ActionName };
        $scope.Screens.push(Page);
    };
    $scope.LoadCM00203 = function (CaseRef) {
        $http.get('/CMS_Case/GetSingle?KaizenPublicKey=' + sessionStorage.PublicKey + "&CaseRef=" + CaseRef).success(function (data) {
            $scope.CM00203 = data;
            $scope.CM00203.DecimalDigit = data.DecimalDigit;
            $scope.CM00203.CreatedDate = kendoparsetoDate($scope.CM00203.CreatedDate);
            $scope.CM00203.AssignDate = kendoparsetoDate($scope.CM00203.AssignDate);
            if ($scope.CM00203.ClosingDate != null && $scope.CM00203.ClosingDate != '') {
                $scope.CM00203.ClosingDate = kendoparsetoDate($scope.CM00203.ClosingDate);
            }
            if ($scope.CM00203.TransactionDate != null && $scope.CM00203.TransactionDate != '') {
                $scope.CM00203.TransactionDate = kendoparsetoDate($scope.CM00203.TransactionDate);
            }
            if ($scope.CM00203.BookingDate != null && $scope.CM00203.BookingDate != '') {
                $scope.CM00203.BookingDate = kendoparsetoDate($scope.CM00203.BookingDate);
            }
            if ($scope.CM00203.ReminderDateTime != null && $scope.CM00203.ReminderDateTime != '') {
                $scope.CM00203.ReminderDateTime = kendoparsetoDate($scope.CM00203.ReminderDateTime);
            }
            if ($scope.CM00203.TransactionDate != null && $scope.CM00203.TransactionDate != '') {
                $scope.CM00203.TransactionDate = kendoparsetoDate($scope.CM00203.TransactionDate);
            }
        }).finally(function () {
            $scope.OpenDebtorDetails($scope.CM00203.DebtorID);
            $scope.OpenDebtorAddressDetails($scope.CM00203.DebtorID, $scope.CM00203.AddressCode);
            $scope.CaseView_toolbarOptions = {
                items: [
                         {
                             type: "splitButton",
                             spriteCssClass: "k-tool-icon k-i-pdf",
                             text: "PDF",
                             click: function (e) {
                                 $scope.ExportScreen('CMS_Case_Create', 'PDF', "Case" + $scope.CM00203.CaseRef.trim());
                             },
                             menuButtons: [
                                    { spriteCssClass: "k-tool-icon k-i-pdf", text: "PDF", click: function (e) { $scope.ExportScreen('CMS_Case_Create', 'PDF', "Case" + $scope.CM00203.CaseRef.trim()); } },
                                    { spriteCssClass: "k-icon k-insertImage", text: "Image", click: function (e) { $scope.ExportScreen('CMS_Case_Create', 'Image', "Case" + $scope.CM00203.CaseRef.trim()); } },
                                    { spriteCssClass: "k-icon k-insertImage", text: "SVG", click: function (e) { $scope.ExportScreen('CMS_Case_Create', 'SVG', "Case" + $scope.CM00203.CaseRef.trim()); } },
                                    { spriteCssClass: "k-icon k-insertImage", text: "Send Email", click: function (e) { } }
                             ]
                         },
                         {
                             type: "button", text: "<span class=''></span>Cancel"
                                , spriteCssClass: "k-tool-icon k-cancel",
                             attributes: { "class": "btn btn-default btn-sm" },
                             click: function (e) {
                                 $scope.$apply(function () {
                                     $scope.GoTo('MainGrid');
                                 });
                             }
                         },
                         {
                             type: "button", text: "<strong>Total: <span class='text-success'>" + $scope.CM00203.CurrencyCode.trim() + " " + $scope.CM00203.ClaimAmount.toFixed($scope.CM00203.DecimalDigit) + "</span></strong>",
                             click: function (e) {
                                 $scope.$apply(function () {
                                     $scope.CasePayment();
                                 });
                             }
                         },
                         {
                             type: "button", text: "Manual Payment",
                             click: function (e) {
                                 $scope.$apply(function () {
                                     $scope.OpenManualPaymentWindow();
                                 });
                             }
                         },
                ]
     , resizable: true
            };
        });
    };
    $scope.CM00203 = {};
    $scope.LoadLookUp = function () {
        $http.get('/CMS_CaseType/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.TRXTypes = data;
        }).finally(function () { });
        $http.get('/CMS_CaseStatus/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.CaseStatuses = data;
        }).finally(function () {
            $scope.CM00203.CaseStatusID = $scope.CaseStatuses[0].CaseStatusID;
            $scope.CaseStatusOptions = {
                filter: "startswith",
                model: "SelectedStatus",
                optionLabel: "-- Select Status --",
                dataTextField: "CaseStatusname",
                dataValueField: "CaseStatusID",
                dataSource: $scope.CaseStatuses,
                value: $scope.CM00203.CaseStatusID
            };
        });
        $http.get('/CMS_ActionPlan/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.Actionplans = data;
        }).finally(function () { });
        $http.get('/CMS_Jeckets/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.Jeckets = data;
        }).finally(function () {
            $scope.JecketsOptions = {
                filter: "startswith",
                model: "SelectedJecket",
                optionLabel: "-- Select Jecket --",
                dataTextField: "JecketName",
                dataValueField: "JecketsID",
                dataSource: $scope.Jeckets,
                value: $scope.CM00203.JecketsID
            };
        });
        $http.get('/CMS_Set_Reason/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.PaymentReasons = data;
        }).finally(function () { });
    };

    $scope.OpenCase = function (CaseRef) {
        $scope.LoadCM00203(CaseRef);
        $scope.GoTo('CaseView');
    };

    $scope.OpenDebtorDetails = function (DebtorID) {
        $http.get('/CMS_Debtor/GetSingle?KaizenPublicKey=' + sessionStorage.PublicKey + "&DebtorID=" + DebtorID).success(function (data) {
            $scope.DebtorObject = data;
        }).finally(function () { });
    };
    $scope.OpenDebtorAddressDetails = function (DebtorID, AddressCode) {
        $http.get('/CMS_Debtor/GetDebtorAddressSingle?KaizenPublicKey=' + sessionStorage.PublicKey + "&DebtorID=" + DebtorID + "&AddressCode=" + AddressCode).success(function (data) {
            if (data.AddressCode != '' && data.AddressCode != undefined) {
                $scope.AddressCode = data;
                $scope.AddressCode.AddressCode = AddressCode;
                $scope.AddressCode.Status = 2;
            }
            else {
                $scope.AddressCode = { Status: 1, AddressCode: AddressCode };
            }
        }).finally(function () { });
    };
    $scope.SaveDebtorAddress = function () {
        $http.post('/CMS_Debtor/SaveDebtorAddress', { 'CM00104': $scope.AddressCode, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
            if (data.Status == true) {
                $.bigBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#739E73",
                    timeout: 4000,
                    icon: "fa fa-check shake animated"
                });
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
    $scope.UpdateDebtorAddress = function () {
        $http.post('/CMS_Debtor/UpdateDebtorAddress', { 'CM00104': $scope.AddressCode, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
            if (data.Status == true) {
                $.bigBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#739E73",
                    timeout: 4000,
                    icon: "fa fa-check shake animated"
                });
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

    $scope.DateChanged = function () {
        $('#calendar').fullCalendar('gotoDate', $scope.CM00203.Date);
    };
    $scope.OpenSchedule = function () {
        removeEntityPage($scope.Screens);
        $scope.LoadReminderPage('FullCalendarScheduleView');
    };

    $scope.OpenTeamSchedule = function () {
        removeEntityPage($scope.Screens);
        $scope.MainGridURL = "/CMS_Reminder/TeamScheduleView?KaizenPublicKey=" + sessionStorage.PublicKey;
    };
    $scope.StatusChanged = function () {
        if (angular.isDefined($scope.Event.SelectedStatus)) {
            $scope.Event.CaseStatusID = $scope.Event.SelectedStatus.CaseStatusID;
            $scope.Event.CaseStatusname = $scope.Event.SelectedStatus.CaseStatusname;
            $http.get('/CMS_CaseStatus/GetAllByCaseStatusID?KaizenPublicKey=' + sessionStorage.PublicKey, { params: { CaseStatusID: $scope.Event.CaseStatusID } }).success(function (data) {
                if (data.length > 0) {
                    $scope.StatusTasks = data;
                } else {
                    $scope.StatusTasks = [];
                }
            }).finally(function () { });
        }
    };
    $scope.SaveCaseAction = function () {
        if ($scope.OrginalCM00203.CaseStatusID != $scope.CM00203.CaseStatusID) {
            $http.post('/CMS_Case/SaveCaseHistoryData', { 'CM00203': $scope.CM00203, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
                if (data.Status == true) {
                    $.bigBox({
                        title: data.Massage,
                        content: data.Description,
                        color: "#739E73",
                        timeout: 4000,
                        icon: "fa fa-check shake animated"
                    });
                    $scope.CloseActionWindow();
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
        else {
            $.smallBox({
                title: "No Changes had been done !",
                content: "<i class='fa fa-clock-o'></i> <i>Nothing Changed...</i>",
                color: "#3276B1",
                timeout: 4000,
                icon: "fa fa-bolt swing animated"
            });
            $scope.CloseActionWindow();
        }
    };
    var task_indx;
    $scope.OpenAgentPopUp = function (index) {
        task_indx = index;
        $scope.OpenkendoWindow('CMS_Agent', 'PopUp', null, 'AgentPredefinedTask');
    };
    $scope.AgentBack = function (agent) {
        if (agent != null) {
            switch ($scope.CurrentControl) {
                case 'AgentPredefinedTask':
                    $scope.StatusTasks[task_indx].AgentID = agent.AgentID;
                    break;
                case 'TaskAgent':
                    $scope.Task.AgentID = agent.AgentID;
                    break;
                case 'ManualPayment':
                    $scope.CM00307.AgentID = agent.AgentID;
                    break;
            }
        }
    };

    $scope.Cancel = function () {
        $scope.Screens = [];
        $scope.DebtorCases = [];
        $scope.DebtorCasesPayment = [];
    };


    ///////////////////////////////// Task Schedule
    $scope.OpenTaskSchedule = function () {
        removeEntityPage($scope.Screens);
        $scope.CaseTask = { Status: 1 };
        $scope.CM00203.Date = new Date();
        $http.get('/CMS_TaskType/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.TaskTypes = data;
            if ($scope.TaskTypes.length > 0) {
                $scope.CaseTask.TaskTypeID = $scope.TaskTypes[0].TaskTypeID
            }
        }).finally(function () { });
        $http.get('/CMS_TaskPriority/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.TaskPriorities = data;
            if ($scope.TaskPriorities.length > 0) {
                $scope.CaseTask.PriorityID = $scope.TaskPriorities[0].PriorityID
            }
        }).finally(function () { });

        $scope.LoadReminderPage('FullCalendarTaskScheduleView');
        /* initialize the calendar------------------------------------------*/
        $scope.casetasks = [];
        $http.get('/CMS_Task/GetAllAgentTasks?KaizenPublicKey=' + sessionStorage.PublicKey + '&AgentID=' + $scope.MY.AgentID).success(function (data) {
            data.forEach(function (element, index) {
                $scope.casetasks.push({
                    id: element.TaskID,
                    title: element.TaskTitle,
                    start: kendoparsetoDate(element.TaskStartDate),
                    end: kendoparsetoDate(element.TaskEndDate),
                    description: element.TaskDescription,
                    TaskID: element.TaskID,
                    CaseRef: element.CaseRef,
                    AgentID: element.AgentID,
                    TaskDescription: element.TaskDescription,
                    TaskTypeID: element.TaskTypeID,
                    PriorityID: element.PriorityID,
                    TaskStartDate: kendoparsetoDate(element.TaskStartDate),
                    TaskEndDate: kendoparsetoDate(element.TaskEndDate),
                    TaskProgress: element.TaskProgress,
                    TaskTitle: element.TaskTitle,
                    AssignDate: kendoparsetoDate(element.AssignDate),
                    allDay: false
                });
            })
        }).finally(function () {
            $('#calendar').fullCalendar('refetchEvents');
        });
    };
    //with this you can handle the events that generated by clicking the day(empty spot) in the calendar
    $scope.TaskdayClick = function (date, allDay, jsEvent, view) {
        $scope.$apply(function () {
            $scope.CaseTask = { Status: 1, start: date, end: date, title: 'New Task', TaskStartDate: date, TaskEndDate: date, AssignDate: date, allDay: false };
            $scope.TaskDatatWindow.center().toFront().open();
        });
    };

    //with this you can handle the click on the events
    $scope.taskClick = function (task) {
        $scope.CaseTask = task;
        $scope.CaseTask.Status = 2;
        $scope.TaskDatatWindow.center().toFront().open();
    };

    //Add & Update Task & Close Task Window
    $scope.AddCaseTask = function () {
        $http.post('/CMS_Task/SaveData', { 'CM00213': $scope.CaseTask, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
            if (data.Status == true) {
                $.bigBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#739E73",
                    timeout: 4000,
                    icon: "fa fa-check shake animated"
                });
                $('#calendar').fullCalendar('renderEvent', $scope.CaseTask, true); // stick? = true
                $('#calendar').fullCalendar('unselect');
                $scope.CloseTaskWindow();
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
    $scope.UpdateCaseTask = function () {
        alert(JSON.stringify($scope.CaseTask, null, 2));
        $http.post('/CMS_Task/UpdateData', { 'CM00213': $scope.CaseTask, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
            if (data.Status == true) {
                $.bigBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#739E73",
                    timeout: 4000,
                    icon: "fa fa-check shake animated"
                });
                $('#calendar').fullCalendar('updateEvent', $scope.CaseTask);
                $scope.CloseTaskWindow();
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
    $scope.CloseTaskWindow = function () {
        $scope.TaskDatatWindow.close();
        $scope.CaseTask = { Status: 1 };
    };

    //-------------------------- Payments By Wael
    $scope.CM00207 = {};
    $scope.CasePayment = function () {
        $scope.CurrentScreenView = "/CMS_Reminder/collectionPayment?KaizenPublicKey=" + sessionStorage.PublicKey;
        //$scope.LoadReminderPage('collectionPayment');

        $scope.CM00207 = {};
        $scope.CM00207.Status = 1;
        $scope.CM00207.AgentID = $scope.MY.AgentID;
        $scope.CM00207.PaymentMethodID = 1;
        $scope.CM00207.CurrencyCode = $scope.MY.CurrencyCode;
        $scope.CM00207.DecimalDigit = $scope.MY.DecimalDigit;
        $scope.CM00207.CurrencySymbol = $scope.MY.CurrencySymbol;
        $scope.CM00207.CheckbookCode = $scope.MY.CheckbookCode;
        $scope.CM00207.IsMultiply = $scope.MY.IsMultiply;
        $scope.CM00207.ExchangeTableID = $scope.MY.ExchangeTableID;
        $scope.CM00207.ExchangeRateID = $scope.MY.ExchangeRateID;
        $scope.CM00207.ExchangeRate = $scope.MY.ExchangeRate;
        $scope.CM00207.PaymentDate = new Date();

        var validator = $("#casepaymentform").data("kendoValidator");

        $scope.ReminderPaymenttoolbarOptions = {
            items: [
                {
                    type: "button",
                    text: "<span class=\"fa fa-plus-circle\"></span> Save & Close",
                    attributes: { "class": "btn-primary" },
                    hidden: $scope.CM00207.Status == 2,
                    click: function (e) {
                        $scope.SavePayment();
                    }
                },
                {
                    type: "button",
                    text: "<span class=\"fa fa-edit\"></span> Update & Close",
                    attributes: { "class": "btn-primary" }, hidden: $scope.CM00207.Status == 1,
                    click: function (e) {
                        $scope.UpdatePayment();
                    }
                },
                {
                    type: "button", text: "Back",
                    click: function (e) {
                        $scope.CurrentScreenView = null;
                    }
                },
                {
                    type: "button", text: "Clear",
                    click: function (e) {
                        $scope.ClearPayment();
                    }
                },
                {
                    type: "button", text: "Delete", hidden: $scope.CM00207.Status == 1,
                    click: function (e) {
                        //$scope.DeletePayment();
                    }
                },
                {
                    type: "splitButton",
                    spriteCssClass: "k-tool-icon k-i-excel",
                    text: "Export",
                    click: function (e) {
                        $scope.SaveAsExcel("GridCMS_Case_CasePayment");
                    },
                    menuButtons: [
                        {
                            spriteCssClass: "k-tool-icon k-i-excel", text: "Excel",
                            click: function (e) { $scope.SaveAsExcel("GridCMS_Case_CasePayment"); }
                        },
                        {
                            spriteCssClass: "k-tool-icon k-i-pdf", text: "PDF",
                            click: function (e) { $scope.SaveAsPdf("GridCMS_Case_CasePayment"); }
                        }
                    ]
                }
            ]
        };
        $http.get('/CMS_Payment/GetNextPaymentTransactionNumber?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.CM00207.PaymentID = data;
        }).finally(function () { });
        if (angular.isDefined($scope.CM00203.CaseRef)) {
            $scope.CM00207.CaseRef = $scope.CM00203.CaseRef;
            $scope.CM00207.ContractID = $scope.CM00203.ContractID;
            $scope.CM00207.ClientID = $scope.CM00203.ClientID;
            $scope.CM00207.DebtorID = $scope.CM00203.DebtorID;
        }
        if (angular.isDefined($scope.CM00207.CheckbookCode) && $scope.CM00207.CheckbookCode != '' && $scope.CM00207.CheckbookCode != null) {
            $scope.GetNextPaymentNumber();
        }
    };
    $scope.LoadCM00207 = function (PaymentID) {
        $http.get('/CMS_Payment/GetSingle?KaizenPublicKey=' + sessionStorage.PublicKey + "&PaymentID=" + PaymentID).success(function (data) {
            $scope.CM00207 = data;
            $scope.CM00207.PaymentDate = kendoparsetoDate($scope.CM00207.PaymentDate);
            $scope.CM00207.CreatedDate = kendoparsetoDate($scope.CM00207.CreatedDate);
        }).finally(function () {
            $scope.CM00207.Status = 2;
            if ($scope.CM00207.IsApproved) {
                $scope.ReminderPaymenttoolbarOptions.items[0].hidden = true;
                $scope.ReminderPaymenttoolbarOptions.items[1].hidden = true;
                $scope.ReminderPaymenttoolbarOptions.items[4].hidden = true;
                $.smallBox({
                    title: "User Validation",
                    content: "<i class='fa fa-clock-o'></i> <i>This Payment Already Approved...</i>",
                    color: "#3276B1",
                    iconSmall: "fa fa-times fa-2x fadeInRight animated",
                    timeout: 4000
                });
            } else {
                $scope.ReminderPaymenttoolbarOptions.items[1].hidden = false;
                $scope.ReminderPaymenttoolbarOptions.items[4].hidden = false;
            }
        });
    };

    $scope.UpdatePayment = function () {
        $http.post('/CMS_Payment/UpdateDataObject', {
            'CM00207': $scope.CM00207,
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
                    $scope.Cancel();
                    $scope.GridRefresh('GridCMS_Case_CasePayment');
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
    $scope.SavePayment = function () {
        var validator = $("#CMS_ReminderCasePaymentForm").data("kendoValidator");
        if (!validator.validate()) {
            $.smallBox({
                title: "User Error",
                content: "<i class='fa fa-clock-o'></i> <i>Missing Info...</i>",
                color: "#C46A69",
                iconSmall: "fa fa-times fa-2x fadeInRight animated",
                timeout: 4000
            });
            return;
        }
        if ($scope.CM00207.Amount <= 0) {
            $.smallBox({
                title: "User Error",
                content: "<i class='fa fa-clock-o'></i> <i>Amount Is Required...</i>",
                color: "#C46A69",
                iconSmall: "fa fa-times fa-2x fadeInRight animated",
                timeout: 4000
            });
            return;
        }
        var update_tmp = {
            PaymentID: $scope.CM00207.PaymentID,
            CaseRef: $scope.CM00207.CaseRef,
            ClientID: $scope.CM00207.ClientID,
            ContractID: $scope.CM00207.ContractID,
            CheckbookCode: $scope.CM00207.CheckbookCode,
            CurrencyCode: $scope.CM00207.CurrencyCode,
            ExchangeTableID: $scope.CM00207.ExchangeTableID,
            IsMultiply: $scope.CM00207.IsMultiply,
            ExchangeRateID: $scope.CM00207.ExchangeRateID,
            ExchangeRate: $scope.CM00207.ExchangeRate,
            DecimalDigit: $scope.CM00207.DecimalDigit,
            Amount: $scope.CM00207.Amount,
            PaymentDate: $scope.CM00207.PaymentDate,
            Description: $scope.CM00207.Description,
            PaymentMethodID: $scope.CM00207.PaymentMethodID,
            AgentID: $scope.CM00207.AgentID,
            PaymentNumber: $scope.CM00207.PaymentNumber,
            BankName: $scope.CM00207.BankName,
            CreatedDate: new Date($scope.CM00207.CreatedDate),
            CreatedBy: $scope.CM00207.CreatedBy,
            BankAccount: $scope.CM00207.BankAccount
        };
        $http.post('/CMS_Payment/SaveCasePayment', {
            'CM00207': $scope.CM00207, 'CaseRef': $scope.CM00203.CaseRef,
            'ClientID': $scope.CM00203.ClientID, 'ContractID': $scope.CM00203.ContractID, 'KaizenPublicKey': sessionStorage.PublicKey
        }).success(function (data) {
            if (data.Status == true) {
                $.bigBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#739E73",
                    timeout: 4000,
                    icon: "fa fa-check shake animated"
                });
                $scope.GridRefresh('GridCMS_Case_CasePayment');
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
    $scope.DeletePayment = function () {
        $.SmartMessageBox({
            title: "Delete Record",
            content: "Are you sure you want to delete this?",
            buttons: '[No][Yes]'
        }, function (ButtonPressed) {
            if (ButtonPressed === "Yes") {
                $http.post('/CMS_Payment/DeleteDataObject', { 'CM00207': $scope.CM00207, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
                    if (data.Status == true) {
                        $.bigBox({
                            title: data.Massage,
                            content: data.Description,
                            color: "#739E73",
                            timeout: 4000,
                            icon: "fa fa-check shake animated"
                        });
                        $scope.Cancel();
                        $scope.GridRefresh('GridCMS_CasePaymentHistory');
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


    $scope.CheckbookBack = function (checkbook) {
        if (checkbook == null) return;
        switch ($scope.CurrentControl) {
            case 'DebtorPayment':
                $scope.CM00207.CheckbookCode = checkbook.CheckbookCode;
                $scope.CM00207.CurrencyCode = checkbook.CurrencyCode;

                $scope.GetNextPaymentNumber();

                $http.get('/GL_Currency/GetSingle?KaizenPublicKey=' + sessionStorage.PublicKey,
                { params: { CurrencyCode: checkbook.CurrencyCode } })
                    .success(function (currency) {
                        $scope.CM00207.DecimalDigit = currency.DecimalDigit;
                        $scope.CM00207.CurrencyCode = currency.CurrencyCode;
                        $scope.CM00207.CurrencySymbol = currency.CurrencySymbol;
                        $scope.CM00207.ExchangeTableID = currency.ExchangeTableID;
                        $scope.CM00207.IsMultiply = currency.IsMultiply;
                        $scope.CM00207.ExchangeRateID = currency.ExchangeRateID;
                        $scope.CM00207.ExchangeRate = currency.ExchangeRate;
                    }).finally(function () { });
                break;
            case 'ManualPayment':
                $scope.CM00307.CheckbookCode = checkbook.CheckbookCode;
                $scope.GetNextManualPaymentNumber();
                if ($scope.CM00307.CurrencyCode == undefined || $scope.CM00307.CurrencyCode == '') {
                    $scope.CM00307.CurrencyCode = checkbook.CurrencyCode;
                    $http.get('/GL_Currency/GetSingle?KaizenPublicKey=' + sessionStorage.PublicKey,
                    { params: { CurrencyCode: checkbook.CurrencyCode } }).success(function (currency) {
                        $scope.CM00307.DecimalDigit = currency.DecimalDigit;
                        $scope.CM00307.CurrencyCode = currency.CurrencyCode;
                        $scope.CM00307.CurrencySymbol = currency.CurrencySymbol;
                        $scope.CM00307.ExchangeTableID = currency.ExchangeTableID;
                        $scope.CM00307.IsMultiply = currency.IsMultiply;
                        $scope.CM00307.ExchangeRateID = currency.ExchangeRateID;
                        $scope.CM00307.ExchangeRate = currency.ExchangeRate;
                    }).finally(function () { });
                }
        }
    };
    $scope.BankBack = function (bank) {
        if (bank != null) {
            switch ($scope.CurrentControl) {
                case 'DebtorPayment':
                    $scope.CM00207.BankCode = bank.BankCode;
                    break;
                case 'ManualPayment':
                    $scope.CM00307.BankCode = bank.BankCode;
                    break;
            }
        }
    };
    $scope.AmountChanged = function () {
        if (parseFloat($scope.CM00207.Amount) > parseFloat($scope.CM00203.ClaimAmount) && parseFloat($scope.CM00207.Amount) >= 0) {
            $scope.CM00207.Amount = 0;
        }
    };
    $scope.ExchangeTableChanged = function () {
        $scope.CM00207.ExchangeTableID = $scope.CM00207.SelectedExchangeTable.ExchangeTableID;
        $scope.CM00207.IsMultiply = $scope.CM00207.SelectedExchangeTable.IsMultiply;
        var grid = $("#GridGL_ExchangeRatePopUp").data("kendoGrid");
        grid.dataSource.data([]);
        grid.dataSource.read();
    };
    $scope.CurrencyBack = function (currency) {
        if (currency != null) {
            switch ($scope.CurrentControl) {
                case 'DebtorPayment':
                    $scope.CM00207.DecimalDigit = currency.DecimalDigit;
                    $scope.CM00207.CurrencyCode = currency.CurrencyCode;
                    $scope.CM00207.ExchangeTableID = currency.ExchangeTableID;
                    $scope.CM00207.IsMultiply = currency.IsMultiply;
                    $scope.CM00207.ExchangeRateID = currency.ExchangeRateID;
                    $scope.CM00207.ExchangeRate = currency.ExchangeRate;
                    var kendoWindow = $("#MainDialog").data("kendoWindow");
                    kendoWindow.close();
                    break;
                case 'ManualPayment':
                    $scope.CM00307.DecimalDigit = currency.DecimalDigit;
                    $scope.CM00307.CurrencyCode = currency.CurrencyCode;
                    $scope.CM00307.ExchangeTableID = currency.ExchangeTableID;
                    $scope.CM00307.IsMultiply = currency.IsMultiply;
                    $scope.CM00307.ExchangeRateID = currency.ExchangeRateID;
                    $scope.CM00307.ExchangeRate = currency.ExchangeRate;
                    var kendoWindow = $("#MainDialog").data("kendoWindow");
                    kendoWindow.close();
                    break;
            }
        }
    };
    $scope.ExchangeRateBack = function (rate) {
        if (rate != null) {
            switch ($scope.CurrentControl) {
                case 'DebtorPayment':
                    $scope.CM00207.ExchangeRateID = rate.ExchangeRateID;
                    $scope.CM00207.ExchangeRate = rate.ExchangeRate;
                    break;
                case 'ManualPayment':
                    $scope.CM00307.ExchangeRateID = rate.ExchangeRateID;
                    $scope.CM00307.ExchangeRate = rate.ExchangeRate;
            }
        }
    };
    $scope.ExchangeTableBack = function (table) {
        if (table != null) {
            switch ($scope.CurrentControl) {
                case 'DebtorPayment':
                    $scope.CM00207.ExchangeTableID = table.ExchangeTableID;
                    $scope.CM00207.IsMultiply = table.IsMultiply;
                    var kendoWindow = $("#MainDialog").data("kendoWindow");
                    kendoWindow.close();
                    break;
                case 'ManualPayment':
                    $scope.CM00307.ExchangeTableID = table.ExchangeTableID;
                    $scope.CM00307.IsMultiply = table.IsMultiply;
                    var kendoWindow = $("#MainDialog").data("kendoWindow");
                    kendoWindow.close();
            }
        }
    };
    $scope.SaveExchangeRate = function () {
        $scope.GL00012.ExchangeTableID = $scope.CM00207.ExchangeTableID;
        $scope.GL00012.CurrencyCode = $scope.CM00207.CurrencyCode;
        $http.post('/GL_ExchangeRate/SaveData', { 'GL00012': $scope.GL00012, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
            if (data.Status == true) {
                $.bigBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#739E73",
                    timeout: 4000,
                    icon: "fa fa-check shake animated"
                });
                $scope.ClearExchangeRate();
                $scope.GridRefresh('GridGL_ExchangeRatePopUp');
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
    $scope.ClearExchangeRate = function () {
        $scope.GL00012 = {};
    };

    $scope.OpenExchangeRateWindow = function () {
        $scope.GL00012 = {};
        $scope.ParmObject = $scope.CM00207;
        $scope.ExchangeRateWindow.center().toFront().open();
        $scope.ExchangeRateWindow.refresh({
            url: "/GL_ExchangeRate/PopUpRates?KaizenPublicKey=" + sessionStorage.PublicKey
        });
    };

    $scope.OpenManualPaymentWindow = function () {
        $scope.CM00307 = {};
        $scope.CM00307.AgentID = $scope.MY.AgentID;
        $scope.CM00307.PaymentMethodID = 1;
        $scope.CM00307.CurrencyCode = $scope.MY.CurrencyCode;
        $scope.CM00307.DecimalDigit = $scope.MY.DecimalDigit;
        $scope.CM00307.CurrencySymbol = $scope.MY.CurrencySymbol;
        $scope.CM00307.CheckbookCode = $scope.MY.CheckbookCode;
        $scope.CM00307.IsMultiply = $scope.MY.IsMultiply;
        $scope.CM00307.ExchangeTableID = $scope.MY.ExchangeTableID;
        $scope.CM00307.ExchangeRateID = $scope.MY.ExchangeRateID;
        $scope.CM00307.ExchangeRate = $scope.MY.ExchangeRate;
        $scope.CM00307.TransactionDate = new Date();
        $scope.CM00307.Total_CalimAmount = 0;
        $scope.CM00307.Total_Amount = 0;
        $scope.ManualPaymentWindow.center().toFront().open();
        $scope.ManualPaymentWindow.refresh({
            url: "/CMS_Reminder/ManualPaymentPopUp?KaizenPublicKey=" + sessionStorage.PublicKey
        });
    };
    $scope.ManualPaymentAmountChanged = function () {
        if ($scope.CM00307.Amount != '') {
            if (parseFloat($scope.CM00203.ClaimAmount) < parseFloat($scope.CM00307.Amount)) {
                $scope.CM00307.Amount = $scope.CM00307.ClaimAmount;
            }
        }
        else {
            $scope.CM00307.Amount = 0;
        }
    };
    $scope.SaveManualPayment = function () {
        $http.post('/CMS_TRX_ManualPayment/SaveCaseManualPayment', { 'CM00307': $scope.CM00307, 'CaseRef': $scope.CM00203.CaseRef, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
            if (data.Status == true) {
                $.bigBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#739E73",
                    timeout: 4000,
                    icon: "fa fa-check shake animated"
                });
                $scope.ManualPaymentWindow.close();
                $scope.CM00307 = {};

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

    //-------------------------- Debtor Payments By Mahmoud
    $scope.DebtorCasesPayment = [];
    $scope.InitializeDebtorPayment = function () {
        $scope.ClearPayment();
        var validator = $("#CMS_ReminderDebtorPaymentForm").data("kendoValidator");
        $scope.GetNextPaymentTransactionNumber();
        $scope.GetNextPaymentNumber();
    };
    $scope.ClearPayment = function () {
        var tempPaymentID = $scope.CM00207.PaymentID;
        $scope.CM00207 = {};
        $scope.DebtorCases = [];
        $scope.CM00207.PaymentID = tempPaymentID;
        $scope.CM00207.Amount = 0;
        $scope.CM00207.Total_Amount = 0;
        $scope.CM00207.Status = 1;
        $scope.CM00207.AgentID = $scope.MY.AgentID;
        $scope.CM00207.PaymentMethodID = 1;
        $scope.CM00207.CurrencyCode = $scope.MY.CurrencyCode;
        $scope.CM00207.DecimalDigit = $scope.MY.DecimalDigit;
        $scope.CM00207.CurrencySymbol = $scope.MY.CurrencySymbol;
        $scope.CM00207.CheckbookCode = $scope.MY.CheckbookCode;
        $scope.CM00207.IsMultiply = $scope.MY.IsMultiply;
        $scope.CM00207.ExchangeTableID = $scope.MY.ExchangeTableID;
        $scope.CM00207.ExchangeRateID = $scope.MY.ExchangeRateID;
        $scope.CM00207.ExchangeRate = $scope.MY.ExchangeRate;
        $scope.CM00207.PaymentDate = new Date();
    };
    $scope.SaveDebtorPayment = function () {
        if ($scope.CM00207.CheckbookCode == null || $scope.CM00207.CheckbookCode == '') {
            $.smallBox({
                title: "Check Book Code Is missing Field",
                content: "Please fill Check Book Code Field first",
                color: "#C46A69",
                iconSmall: "fa fa-times fa-2x fadeInRight animated",
                timeout: 4000
            });
            return;
        }
        $scope.DebtorCasesPayment = [];
        $scope.DebtorCases.forEach(function (element, index) {
            if (element.isSelected) {
                $scope.DebtorCasesPayment.push({
                    PaymentID: $scope.CM00207.PaymentID,
                    CaseRef: element.CaseRef,
                    ContractID: element.ContractID,
                    ClientID: element.ClientID,
                    Amount: element.AppliedAmount,
                });
            }
        })
        $http({
            url: '/CMS_Payment/SavePaymentsAgainstCase?KaizenPublicKey=' + sessionStorage.PublicKey,
            method: "POST",
            data: { 'CM00207': $scope.CM00207, 'CM00204': $scope.DebtorCasesPayment },
            dataType: "json",
            headers: {
                'Content-Type': 'application/json; charset=utf-8'
            }
        }).success(function (data) {
            if (data.Status == true) {
                $.smallBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#739E73",
                    iconSmall: "fa fa-times fa-2x fadeInRight animated",
                    timeout: 4000
                });
                $scope.GetNextPaymentTransactionNumber();
                $scope.GetNextPaymentNumber();
                $scope.CM00207.DebtorID = null;
                $scope.CM00207.Total_Amount = 0;
                $scope.CM00207.Amount = 0;
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
    $scope.LineCheckChange = function (line) {
        if (line.isSelected) {
            line.AppliedAmount = 0;
            line.Focus = true;
            line.isSelected = true;
        }
        else {
            $scope.CM00207.Total_Amount -= line.AppliedAmount;
            line.AppliedAmount = '';
        }
    };
    $scope.AmountApplied = function (line) {
        if (line.AppliedAmount != '') {
            if (parseFloat(line.ClaimAmount) < parseFloat(line.AppliedAmount)) {
                line.AppliedAmount = line.ClaimAmount;
                line.Focus = true;
            }
            $scope.CM00207.Total_Amount = $scope.CalculateAppliedAmount($scope.DebtorCases);
            if ($scope.CM00207.Total_Amount > $scope.CM00207.Amount) {
                $.smallBox({
                    title: "Validation",
                    content: "<i class='fa fa-clock-o'></i> <i>Cannot Save The Payment Cause Applied Amount Is More Than Payment Amount Itself !!</i>",
                    color: "#3276B1",
                    iconSmall: "fa fa-times fa-2x fadeInRight animated",
                    timeout: 4000
                });
            }

        } else {
            line.AppliedAmount = 0;
            line.Focus = true;
        }
    };
    $scope.DebtorAmountChanged = function () {
        if (parseFloat($scope.CM00207.Amount) > parseFloat($scope.CM00203.ClaimAmount) && parseFloat($scope.CM00207.Amount) >= 0) {
            $scope.CM00207.Amount = 0;
        }
        $scope.CM00207.Total_Amount = $scope.CalculateAppliedAmount($scope.DebtorCases);
        if ($scope.CM00207.Total_Amount > $scope.CM00207.Amount) {
            $.smallBox({
                title: "Validation",
                content: "<i class='fa fa-clock-o'></i> <i>Cannot Save The Payment Cause Applied Amount Is More Than Payment Amount Itself !!</i>",
                color: "#3276B1",
                iconSmall: "fa fa-times fa-2x fadeInRight animated",
                timeout: 4000
            });
        }
    };
    $scope.CalculateAppliedAmount = function (Lines) {
        var total = 0;
        Lines.forEach(function (element, index) {
            if (element.isSelected)
                total += parseFloat(element.AppliedAmount);
        })
        return total;
    };


    //////////////////////////// Manual Payment 
    $scope.CM00307 = {};
    $scope.InitializeManualPayment = function () {
        $http.get('/CMS_Set_Reason/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.PaymentReasons = data;
        }).finally(function () { });

        $scope.ClearManualPayment();
        $scope.GetNextManualPaymentTransactionNumber();
        $scope.GetNextManualPaymentNumber();
    };
    $scope.ClearManualPayment = function () {
        $scope.CM00307 = {};
        $scope.CM00308 = {};
        $scope.CM00307.Status = 1;
        $scope.CM00307.AgentID = $scope.MY.AgentID;
        $scope.CM00307.PaymentMethodID = 1;
        $scope.CM00307.CurrencyCode = $scope.MY.CurrencyCode;
        $scope.CM00307.DecimalDigit = $scope.MY.DecimalDigit;
        $scope.CM00307.CurrencySymbol = $scope.MY.CurrencySymbol;
        $scope.CM00307.CheckbookCode = $scope.MY.CheckbookCode;
        $scope.CM00307.IsMultiply = $scope.MY.IsMultiply;
        $scope.CM00307.ExchangeTableID = $scope.MY.ExchangeTableID;
        $scope.CM00307.ExchangeRateID = $scope.MY.ExchangeRateID;
        $scope.CM00307.ExchangeRate = $scope.MY.ExchangeRate;
        $scope.CM00307.TransactionDate = new Date();
        $scope.CM00307.Total_CalimAmount = 0;
        $scope.CM00307.Total_Amount = 0;
        $scope.TransactionLines = [];
        $scope.InsertedLines = [];
        $scope.UpdatedLines = [];
        $scope.DeletedLines = [];

    };
    $scope.GetNextManualPaymentTransactionNumber = function () {
        //$http.get('/CMS_TRX_ManualPayment/GetNextPaymentTransactionNumber?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
        //    $scope.CM00307.PaymentID = data;
        //}).finally(function () {
        //    if (angular.isDefined($scope.CM00307.CheckbookCode) && $scope.CM00307.CheckbookCode != '' && $scope.CM00307.CheckbookCode != null) {
        //        $scope.GetNextPaymentNumber();
        //    }
        //});
    };
    $scope.GetNextManualPaymentNumber = function () {
        //$http.get('/CMS_TRX_ManualPayment/GetNextPaymentNumber?KaizenPublicKey=' + sessionStorage.PublicKey,
        //    {
        //        params: {
        //            PaymentMethodID: $scope.CM00307.PaymentMethodID,
        //            CheckbookCode: $scope.CM00307.CheckbookCode
        //        }
        //    }).success(function (data) {
        //        $scope.CM00307.PaymentNumber = data;
        //    }).finally(function () { });
    };
    $scope.CalculateTotal = function (Lines) {
        var claim_total = 0;
        var applied_total = 0;
        for (var i = 0; i < Lines.length; i++) {
            if (Lines[i].status != 3) {
                claim_total += parseFloat(Lines[i].ClaimAmount);
                applied_total += parseFloat(Lines[i].Amount);
            }
        }
        $scope.CM00307.Total_CalimAmount = claim_total;
        $scope.CM00307.Total_Amount = applied_total;
    };
    /////////////////////////////////////////
    $scope.CM00308 = {};
    $scope.TransactionLines = [];
    $scope.InsertedLines = [];
    $scope.UpdatedLines = [];
    $scope.DeletedLines = [];
    $scope.IsOpenTransactionPanel = false;
    $scope.OpenTransactionPanel = function (isopen) {
        $scope.IsOpenTransactionPanel = isopen;
        $scope.CM00308 = {
            Amount: 0, status: 1, PaymentID: $scope.CM00307.PaymentID
        };
    };
    $scope.CancelTransaction = function () {
        $scope.IsOpenTransactionPanel = false;
    };
    $scope.AddNewTransaction = function () {
        if ($scope.CM00308.CaseRef != "" && $scope.CM00308.CaseRef != null) {
            $scope.CM00308.status = 1;
            $scope.TransactionLines.push($scope.CM00308);
            $scope.CM00308 = {
                Amount: 0, status: 1
            };
            $scope.CalculateTotal($scope.TransactionLines);
            $scope.CancelTransaction();
        }
    };
    $scope.RemoveTransaction = function (line, index) {
        if (line.status == 1)
            $scope.TransactionLines.splice(index, 1);
        else
            line.status = 3;

        $scope.CalculateTotal($scope.TransactionLines);
    };
    $scope.UndoDeleteTransaction = function (line, index) {
        line.status = 2;
        $scope.CalculateTotal($scope.TransactionLines);
    };
    $scope.AmountAppliedChanged = function () {
        if ($scope.CM00308.Amount != '') {
            if (parseFloat($scope.CM00308.ClaimAmount) < parseFloat($scope.CM00308.Amount)) {
                $scope.CM00308.Amount = $scope.CM00308.ClaimAmount;
            }
            $scope.CM00307.Total_Amount = $scope.CalculateAppliedAmount($scope.TransactionLines);
            if ($scope.CM00307.Total_Amount > $scope.CM00307.Amount) {
                $.smallBox({
                    title: "Validation",
                    content: "<i class='fa fa-clock-o'></i> <i>Cannot Save The Payment Cause Applied Amount Is More Than Payment Amount Itself !!</i>",
                    color: "#3276B1",
                    iconSmall: "fa fa-times fa-2x fadeInRight animated",
                    timeout: 4000
                });
            }

        }
        else {
            $scope.CM00308.Amount = 0;
        }
    };
    $scope.SaveManualPaymentData = function () {
        $http.post('/CMS_TRX_ManualPayment/SaveDataObject', { 'CM00307': $scope.CM00307, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
            if (data.Status == true) {
                $http.post('/CMS_TRX_ManualPayment/SaveLineData', { 'CM00308': $scope.TransactionLines, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
                    if (data.Status == true) {
                        $.bigBox({
                            title: data.Massage,
                            content: data.Description,
                            color: "#739E73",
                            timeout: 4000,
                            icon: "fa fa-check shake animated"
                        });
                        $scope.ClearManualPayment();
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


    //--------------------------END


    $scope.GoTo();
    $scope.DebtorBack = function (debtor) {
        if (debtor == null) return;
        switch ($scope.CurrentControl) {
            case 'ReminderCase':
                $scope.DebtorObject = debtor;
                $http.get('/CMS_Case/GetSingleByDebtor?KaizenPublicKey=' + sessionStorage.PublicKey + "&DebtorID=" + debtor.DebtorID).success(function (data) {
                    if (data.CaseRef != '' && data.CaseRef != undefined) {
                        $scope.CM00203 = data;
                    }
                    else {
                        $scope.CM00203 = { DebtorID: debtor.DebtorID };
                    }
                }).finally(function () {
                    var grid = $("#GridCMS_CaseRelatedToDebtor").data("kendoGrid");
                    grid.dataSource.read();
                    if ($scope.CM00203.CaseRef != '' && $scope.CM00203.CaseRef != undefined) {
                        $scope.OpenDebtorAddressDetails($scope.CM00203.DebtorID, $scope.CM00203.AddressCode);
                    }
                });
                break;
            case 'DebtorPayment':
                $scope.CM00207.DebtorID = debtor.DebtorID;
                $http.get('/CMS_Case/GetAllByDebtor?KaizenPublicKey=' + sessionStorage.PublicKey + "&DebtorID=" + debtor.DebtorID).success(function (data) {
                    if (data.length > 0) {
                        $scope.DebtorCases = data;
                    }
                    else {
                        $scope.DebtorCases = [];
                    }
                }).finally(function () {
                    var totalClaim = 0;
                    $scope.DebtorCases.forEach(function (element, index) {
                        totalClaim += parseFloat(element.ClaimAmount);
                    })
                    $scope.CM00207.Total_ClaimAmount = totalClaim;
                });
                break;
        }
    };
});
app.controller('CMS_ScheduleController', function ($scope, $http) {
    $scope.CM00203 = {};
    $scope.LoadLookUp = function () {
        $scope.Event = { Status: 1 };
        $scope.Event.SelectedStatus = {};
        $scope.CM00203.Date = new Date();
        $http.get('/CMS_CaseStatus/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.CaseStatuses = data;
        }).finally(function () {
            $scope.CaseStatusOptions = {
                filter: "startswith",
                model: "SelectedStatus",
                optionLabel: "-- Select Status --",
                dataTextField: "CaseStatusname",
                dataValueField: "CaseStatusID",
                dataSource: $scope.CaseStatuses,
                value: $scope.Event.CaseStatusID
            };
        });
        $scope.schedulerOptions = {
            date: $scope.ToReminder,
            startTime: $scope.ToReminder,
            height: 600,
            views: [
                "day",
                { type: "workWeek", selected: true },
                "week",
                "month",
            ],
            timezone: "Etc/UTC",
            dataSource: {
                batch: true,
                transport: {
                    read: {
                        //url: "//demos.telerik.com/kendo-ui/service/tasks",
                        url: "/CMS_Reminder/GetDataListGrid?IsOverDue=true&FromReminder=1/1/2016&ToReminder=1/1/2016&AgentID="
                            + $scope.SelectedAgent.AgentID
                            + "&KaizenPublicKey=" + sessionStorage.PublicKey,
                        dataType: "jsonp"
                    },
                    parameterMap: function (options, operation) {
                        if (operation !== "read" && options.models) {
                            return { models: kendo.stringify(options.models) };
                        }
                    }
                },
                schema: {
                    model: {
                        id: "CaseRef",
                        fields: {
                            CaseRef: { from: "CaseRef" },
                            start: { type: "date", from: "ReminderDateTime" },
                            end: { type: "date", from: "ReminderDateTime" },
                            title: { from: "CaseRef", defaultValue: "No title", validation: { required: true } },
                            ReminderDateTime: { type: "date", from: "ReminderDateTime" }
                        }
                    }
                }
            },
            resources: [
                {
                    field: "CaseRef",
                    title: "Owner",
                    dataSource: [
                        { text: "Alex", value: 1, color: "#f8a398" },
                        { text: "Bob", value: 2, color: "#51a0ed" },
                        { text: "Charlie", value: 3, color: "#56ca85" }
                    ]
                }
            ]
        };
        $http.get('/CMS_Case/GetAllCasesReminders?KaizenPublicKey=' + sessionStorage.PublicKey + '&AgentID=' + $scope.MY.AgentID).success(function (data) {
            data.forEach(function (element, index) {
                $scope.events.push({
                    id: element.StatusHistoryID,
                    title: "For Case : " + element.CaseRef,
                    start: kendoparsetoDate(element.ReminderDateTime),
                    end: kendoparsetoDate(element.ReminderDateTime),
                    description: element.CaseStatusComment,
                    StatusHistoryID: element.StatusHistoryID,
                    CaseRef: element.CaseRef,
                    AgentID: element.AgentID,
                    CaseStatusComment: element.CaseStatusComment,
                    CaseStatusID: element.CaseStatusID,
                    PTPAMT: element.PTPAMT,
                    ReminderDateTime: kendoparsetoDate(element.ReminderDateTime),
                    allDay: false
                });
            })
        }).finally(function () {
            $('#calendar').fullCalendar('refetchEvents');
        });
        $scope.events = [];
    };
    $scope.DateChanged = function () {
        $('#calendar').fullCalendar('gotoDate', $scope.CM00203.Date);
    };
    $scope.LoadLookUp();
    //with this you can handle the events that generated by clicking the day(empty spot) in the calendar
    $scope.dayClick = function (date, allDay, jsEvent, view) {
        $scope.$apply(function () {
            $scope.Event = { Status: 1, start: date, end: date, title: 'New Reminder', ReminderDateTime: date, allDay: false };
            $scope.EvenDatatWindow.center().toFront().open();
        });
    };

    //with this you can handle the events that generated by droping any event to different position in the calendar
    $scope.alertOnDrop = function (event, dayDelta, minuteDelta, allDay, revertFunc, jsEvent, ui, view) {
        $scope.$apply(function () {
            $scope.alertMessage = ('Event Droped to make dayDelta ' + dayDelta);
            $('#calendar').fullCalendar('updateEvent', event);
        });
    };

    //with this you can handle the events that generated by resizing any event to different position in the calendar
    $scope.alertOnResize = function (event, dayDelta, minuteDelta, revertFunc, jsEvent, ui, view) {
        $scope.$apply(function () {
            $scope.alertMessage = ('Event Resized to make dayDelta ' + minuteDelta);
        });
    };

    //with this you can handle the click on the events
    $scope.eventClick = function (event) {
        $scope.Event = event;
        $scope.Event.Status = 2;
        var indx = $scope.functiontofindIndexByKeyValue($scope.CaseStatuses, "CaseStatusID", $scope.Event.CaseStatusID);
        var obj = $scope.CaseStatuses[indx];
        $scope.Event.SelectedStatus = obj;
        $http.get('/CMS_CaseStatus/GetAllByCaseStatusID?KaizenPublicKey=' + sessionStorage.PublicKey, { params: { CaseStatusID: $scope.Event.SelectedStatus.CaseStatusID } }).success(function (data) {
            if (data.length > 0) {
                $scope.StatusTasks = data;
            } else {
                $scope.StatusTasks = [];
            }
        }).finally(function () {
            $scope.StatusTasks.forEach(function (element, index) {
                element.TaskStartDate = kendoparsetoDate(element.TaskStartDate);
                element.TaskEndDate = kendoparsetoDate(element.TaskEndDate);
                element.AssignDate = kendoparsetoDate(element.AssignDate);
            })
            $scope.EvenDatatWindow.center().toFront().open();
        });
    };

    //with this you can handle the events that generated by each page render process
    $scope.renderView = function (view) {
        var date = new Date(view.calendar.getDate());
        $scope.currentDate = date.toDateString();
        $scope.$apply(function () {
            $scope.alertMessage = ('Page render with date ' + $scope.currentDate);
        });
    };

    //with this you can handle the events that generated when we change the view i.e. Month, Week and Day
    $scope.changeView = function (view, calendar) {
        currentView = view;
        calendar.fullCalendar('changeView', view);
        $scope.$apply(function () {
            $scope.alertMessage = ('You are looking at ' + currentView);
        });
    };

    //Add & Update Event & Close Event Window
    $scope.AddEvent = function () {
        $scope.Event.allDay = false;
        $http.post('/CMS_Case/SaveCaseEventData', { 'CM10701': $scope.Event, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
            if (data.Status == true) {
                $.bigBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#739E73",
                    timeout: 4000,
                    icon: "fa fa-check shake animated"
                });
                $('#calendar').fullCalendar('renderEvent', $scope.Event, true); // stick? = true
                $('#calendar').fullCalendar('unselect');
                $scope.CloseEventWindow();
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
    $scope.UpdateEvent = function () {
        $scope.Event.allDay = false;
        $http.post('/CMS_Case/SaveCaseEventData', { 'CM10701': $scope.Event, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
            if (data.Status == true) {
                $.bigBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#739E73",
                    timeout: 4000,
                    icon: "fa fa-check shake animated"
                });
                $('#calendar').fullCalendar('updateEvent', $scope.Event);
                $scope.CloseEventWindow();
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
    $scope.CloseEventWindow = function () {
        $scope.EvenDatatWindow.close();
        $scope.Event = { Status: 1 };
    };



    $scope.OpenTeamSchedule = function () {
        removeEntityPage($scope.Screens);
        $scope.MainGridURL = "/CMS_Reminder/TeamScheduleView?KaizenPublicKey=" + sessionStorage.PublicKey;
    };
    $scope.StatusChanged = function () {
        if (angular.isDefined($scope.Event.SelectedStatus)) {
            $scope.Event.CaseStatusID = $scope.Event.SelectedStatus.CaseStatusID;
            $scope.Event.CaseStatusname = $scope.Event.SelectedStatus.CaseStatusname;
            $http.get('/CMS_CaseStatus/GetAllByCaseStatusID?KaizenPublicKey=' + sessionStorage.PublicKey, { params: { CaseStatusID: $scope.Event.CaseStatusID } }).success(function (data) {
                if (data.length > 0) {
                    $scope.StatusTasks = data;
                } else {
                    $scope.StatusTasks = [];
                }
            }).finally(function () { });
        }
    };
    $scope.SaveCaseAction = function () {
        if ($scope.OrginalCM00203.CaseStatusID != $scope.CM00203.CaseStatusID) {
            $http.post('/CMS_Case/SaveCaseHistoryData', { 'CM00203': $scope.CM00203, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
                if (data.Status == true) {
                    $.bigBox({
                        title: data.Massage,
                        content: data.Description,
                        color: "#739E73",
                        timeout: 4000,
                        icon: "fa fa-check shake animated"
                    });
                    $scope.CloseActionWindow();
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
        else {
            $.smallBox({
                title: "No Changes had been done !",
                content: "<i class='fa fa-clock-o'></i> <i>Nothing Changed...</i>",
                color: "#3276B1",
                timeout: 4000,
                icon: "fa fa-bolt swing animated"
            });
            $scope.CloseActionWindow();
        }
    };
    var task_indx;
    $scope.OpenAgentPopUp = function (index) {
        task_indx = index;
        $scope.OpenkendoWindow('CMS_Agent', 'PopUp', null, 'AgentPredefinedTask');
    };
    $scope.AgentBack = function (agent) {
        if (agent != null) {
            switch ($scope.CurrentControl) {
                case 'AgentPredefinedTask':
                    $scope.StatusTasks[task_indx].AgentID = agent.AgentID;
                    break;
                case 'TaskAgent':
                    $scope.Task.AgentID = agent.AgentID;
                    break;
            }
        }
    };

    $scope.Cancel = function () {
        $scope.Screens = [];
        $scope.DebtorCases = [];
        $scope.DebtorCasesPayment = [];
    };


    ///////////////////////////////// Task Schedule
    $scope.OpenTaskSchedule = function () {
        removeEntityPage($scope.Screens);
        $scope.CaseTask = { Status: 1 };
        $scope.CM00203.Date = new Date();
        $http.get('/CMS_TaskType/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.TaskTypes = data;
            if ($scope.TaskTypes.length > 0) {
                $scope.CaseTask.TaskTypeID = $scope.TaskTypes[0].TaskTypeID
            }
        }).finally(function () { });
        $http.get('/CMS_TaskPriority/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.TaskPriorities = data;
            if ($scope.TaskPriorities.length > 0) {
                $scope.CaseTask.PriorityID = $scope.TaskPriorities[0].PriorityID
            }
        }).finally(function () { });

        $scope.LoadReminderPage('FullCalendarTaskScheduleView');
        /* initialize the calendar------------------------------------------*/
        $scope.casetasks = [];
        $http.get('/CMS_Task/GetAllAgentTasks?KaizenPublicKey=' + sessionStorage.PublicKey + '&AgentID=' + $scope.MY.AgentID).success(function (data) {
            data.forEach(function (element, index) {
                $scope.casetasks.push({
                    id: element.TaskID,
                    title: element.TaskTitle,
                    start: kendoparsetoDate(element.TaskStartDate),
                    end: kendoparsetoDate(element.TaskEndDate),
                    description: element.TaskDescription,
                    TaskID: element.TaskID,
                    CaseRef: element.CaseRef,
                    AgentID: element.AgentID,
                    TaskDescription: element.TaskDescription,
                    TaskTypeID: element.TaskTypeID,
                    PriorityID: element.PriorityID,
                    TaskStartDate: kendoparsetoDate(element.TaskStartDate),
                    TaskEndDate: kendoparsetoDate(element.TaskEndDate),
                    TaskProgress: element.TaskProgress,
                    TaskTitle: element.TaskTitle,
                    AssignDate: kendoparsetoDate(element.AssignDate),
                    allDay: false
                });
            })
        }).finally(function () {
            $('#calendar').fullCalendar('refetchEvents');
        });
    };
    //with this you can handle the events that generated by clicking the day(empty spot) in the calendar
    $scope.TaskdayClick = function (date, allDay, jsEvent, view) {
        $scope.$apply(function () {
            $scope.CaseTask = { Status: 1, start: date, end: date, title: 'New Task', TaskStartDate: date, TaskEndDate: date, AssignDate: date, allDay: false };
            $scope.TaskDatatWindow.center().toFront().open();
        });
    };

    //with this you can handle the click on the events
    $scope.taskClick = function (task) {
        $scope.CaseTask = task;
        $scope.CaseTask.Status = 2;
        $scope.TaskDatatWindow.center().toFront().open();
    };

    //Add & Update Task & Close Task Window
    $scope.AddCaseTask = function () {
        $http.post('/CMS_Task/SaveData', { 'CM00213': $scope.CaseTask, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
            if (data.Status == true) {
                $.bigBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#739E73",
                    timeout: 4000,
                    icon: "fa fa-check shake animated"
                });
                $('#calendar').fullCalendar('renderEvent', $scope.CaseTask, true); // stick? = true
                $('#calendar').fullCalendar('unselect');
                $scope.CloseTaskWindow();
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
    $scope.UpdateCaseTask = function () {
        $http.post('/CMS_Task/UpdateData', { 'CM00213': $scope.CaseTask, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
            if (data.Status == true) {
                $.bigBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#739E73",
                    timeout: 4000,
                    icon: "fa fa-check shake animated"
                });
                $('#calendar').fullCalendar('updateEvent', $scope.CaseTask);
                $scope.CloseTaskWindow();
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
    $scope.CloseTaskWindow = function () {
        $scope.TaskDatatWindow.close();
        $scope.CaseTask = { Status: 1 };
    };

});