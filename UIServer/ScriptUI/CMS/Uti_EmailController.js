﻿app.controller('Uti_EmailController', function ($scope, $http) {
    $scope.SelectedLookUp = {};
    $scope.SelectedLookUp.LetterType = {};
    $scope.SelectedLookUp.DebtorAddressCodeType = {};
    $scope.DemandTemplates = [];
    $scope.CM00230 = {};
    $scope.PagesCM00230 = [];
    $scope.toolbarOptions = {
        items: [
                 {
                     type: "button", text: "<span class=\"fa fa-plus-circle\"></span>New ",
                     attributes: { "class": "btn-primary" },
                     click: function (e) {
                         $scope.$apply(function () {
                             $scope.AddCM00230();
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
                 { type: "button", spriteCssClass: "k-tool-icon k-i-refresh" }
        ]
        , resizable: true
    };
    $scope.CaseDocType();
    $scope.CaseStatusList();
    
    $scope.LoadCM00230Page = function (ActionName) {
        removeEntityPage($scope.PagesCM00230);
        var URIPath = "/CMS/" + $scope.ToolBar.ActiveScreen.ID + "/" + ActionName + "?KaizenPublicKey=" + sessionStorage.PublicKey;
        var Page = { url: URIPath, ActionName: ActionName };
        $scope.PagesCM00230.push(Page);
    }
    $scope.DemandTemplateChanged = function () {
        $scope.CM00230.TemplateID = $scope.CM00230.SelectedTemplate.TemplateID;
        $scope.CM00230.TemplateContant = $scope.CM00230.SelectedTemplate.TemplateContant;
    };
    $scope.SaveCaseDemandLetter = function () {
        $scope.CM00231 = [];
        var items = $("#GridCMS_CaseDemandLetter").data("kendoGrid").dataSource.data();
        for (i = 0; i < items.length; i++) {
            var item = items[i];
            if (item.IsSeleceted) {
                var temp = { CaseRef: item.CaseRef };
                $scope.CM00231.push(temp);
            }
        }
        $scope.CM00230.AddressCode = $scope.SelectedLookUp.DebtorAddressCodeType.AddressCode;
        if ($scope.CM00231.length == 0)
        {
            alert('cases is required');
            return;
        }
        $http.post('/Uti_Email/SaveCaseDemandLetter', {
            'CM00230': $scope.CM00230,
            'CM00231': $scope.CM00231,
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
    $scope.CM00230TemplateContant = function () {
        $scope.FullEditorTools = [
           "bold",
           "italic",
           "underline",
           "strikethrough",
           "justifyLeft",
           "justifyCenter",
           "justifyRight",
           "justifyFull",
           "insertUnorderedList",
           "insertOrderedList",
           "indent",
           "outdent",
           //"createLink",
           //"unlink",
           //"insertImage",
           //"insertFile",
           "subscript",
           "superscript",
           "createTable",
           "addRowAbove",
           "addRowBelow",
           "addColumnLeft",
           "addColumnRight",
           "deleteRow",
           "deleteColumn",
           "viewHtml",
           "cleanFormatting",
           "fontName",
           "fontSize",
           "foreColor",
           "backColor",
           "print",
           {
               name: "insertHtml",
               items: [
                   { text: "Case Reference", value: "<span class='marker'>[CaseRef]</span>&nbsp" },
                   { text: "Currency Code", value: "<span class='marker'>[CurrencyCode]</span>&nbsp" },
                   { text: "Exchange Rate", value: "<span class='marker'>[ExchangeRate]</span>&nbsp" },
                   { text: "Data Source", value: "<span class='marker'>[TrxTypeName]</span>&nbsp" },
                   { text: "Product Class Code", value: "<span class='marker'>[ContractID]</span>&nbsp" },
                   { text: "Product Name", value: "<span class='marker'>[ContractName]</span>&nbsp" },
                   { text: "Category Code", value: "<span class='marker'>[ClientID]</span>&nbsp" },
                   { text: "Category Name", value: "<span class='marker'>[ClientName]</span>&nbsp" },
                   { text: "Batch ID", value: "<span class='marker'>[BatchID]</span>&nbsp" },
                   { text: "CIF", value: "<span class='marker'>[CIFNumber]</span>&nbsp" },
                   { text: "Customer ID", value: "<span class='marker'>[CPRCRNo]</span>&nbsp" },
                   { text: "Customer Name", value: "<span class='marker'>[CIFName]</span>&nbsp" },
                   { text: "Grade Code", value: "<span class='marker'>[PriorityID]</span>&nbsp" },
                   { text: "Grade Name", value: "<span class='marker'>[PriorityName]</span>&nbsp" },
                   { text: "NPA", value: "<span class='marker'>[NPA]</span>&nbsp" },
                   { text: "Task Complated Percentage", value: "<span class='marker'>[TaskComplated]</span>&nbsp" },
                   { text: "Case Status ID", value: "<span class='marker'>[CaseStatusID]</span>&nbsp" },
                   { text: "Case Status name ", value: "<span class='marker'>[CaseStatusname]</span>&nbsp" },
                   { text: "Sub Status ID ", value: "<span class='marker'>[CaseStatusChild]</span>&nbsp" },
                   { text: "Sub Status name ", value: "<span class='marker'>[CaseStatusChildName]</span>&nbsp" },
                   { text: "Case Remark", value: "<span class='marker'>[CaseStatusComment]</span>&nbsp" },
                   { text: "Reminder Date Time", value: "<span class='marker'>[ReminderDateTime]</span>&nbsp" },
                   { text: "PTP Amount", value: "<span class='marker'>[PTPAMT]</span>&nbsp" },
                   { text: "PTP Broken", value: "<span class='marker'>[PTPBroken]</span>&nbsp" },
                   { text: "PTP kept", value: "<span class='marker'>[PTPkept]</span>&nbsp" },

                   { text: "Condition Status ID", value: "<span class='marker'>[LastCaseStatusID]</span>&nbsp" },
                   { text: "Case Condition name ", value: "<span class='marker'>[LastCasStatusname]</span>&nbsp" },
                   { text: "Sub Condition ID ", value: "<span class='marker'>[LastCasStatusChldID]</span>&nbsp" },
                   { text: "Sub Condition name ", value: "<span class='marker'>[LastCasStatusChldNam]</span>&nbsp" },
                   { text: "Condition Remark", value: "<span class='marker'>[LastCasStatusComment]</span>&nbsp" },

                   { text: "Income Status ID", value: "<span class='marker'>[MidCasStatusID]</span>&nbsp" },
                   { text: "Income Status Name ", value: "<span class='marker'>[MidCasStatusChld]</span>&nbsp" },
                   { text: "Sub Income Status IS", value: "<span class='marker'>[MidCasStatusnam]</span>&nbsp" },
                   { text: "Sub Income Status Name", value: "<span class='marker'>[MidCasStatusChldName]</span>&nbsp" },
                   { text: "Income Status Remark", value: "<span class='marker'>[MidCasStatusComment]</span>&nbsp" },
                   { text: "CYCLEDAY", value: "<span class='marker'>[CYCLEDAY]</span>&nbsp" },
                   { text: "BucketPrev", value: "<span class='marker'>[BucketPrev]</span>&nbsp" },
                   { text: "BucketPrevName", value: "<span class='marker'>[BucketPrevName]</span>&nbsp" },
                   { text: "BucketID", value: "<span class='marker'>[BucketID]</span>&nbsp" },
                   { text: "BucketName", value: "<span class='marker'>[BucketName]</span>&nbsp" },
                   { text: "Eco Sector", value: "<span class='marker'>[TxtField01]</span>&nbsp" },
                   { text: "Collateral Cov.%", value: "<span class='marker'>[AMT02]</span>&nbsp" },
                   { text: "Collateral Value", value: "<span class='marker'>[AMT01]</span>&nbsp" },
                   { text: "Collateral Type", value: "<span class='marker'>[AMT03]</span>&nbsp" },
                   { text: "Facility Allocation", value: "<span class='marker'>[AMT04]</span>&nbsp" },
                   { text: "Collateral Coverage", value: "<span class='marker'>[AMT05]</span>&nbsp" },
                   { text: "Provision", value: "<span class='marker'>[AMT06]</span>&nbsp" },
                   { text: "Suspended", value: "<span class='marker'>[AMT07]</span>&nbsp" },
                   { text: "Finance AMT", value: "<span class='marker'>[PrincipleAmount]</span>&nbsp" },
                   { text: "O/S - Maturity", value: "<span class='marker'>[MaturityAmount]</span>&nbsp" },
                   { text: "O/S - Today", value: "<span class='marker'>[OutStandingToday]</span>&nbsp" },
                   { text: "Installment", value: "<span class='marker'>[InstallmentAmount]</span>&nbsp" },
                   { text: "Past Due", value: "<span class='marker'>[ClaimAmount]</span>&nbsp" },
                   { text: "Fees/Expenses ", value: "<span class='marker'>[FinanceCharge]</span>&nbsp" },
                   { text: "Waiver", value: "<span class='marker'>[WriteOffAMT]</span>&nbsp" },
                   { text: "Past Due Days", value: "<span class='marker'>[OverDueDays]</span>&nbsp" },
                   { text: "Disbursement", value: "<span class='marker'>[FirstDisburementDate]</span>&nbsp" },
                   { text: "Maturity", value: "<span class='marker'>[MATURITY_DATE]</span>&nbsp" },
                   { text: "Book Date", value: "<span class='marker'>[BookingDate]</span>&nbsp" },
                   { text: "Case Summary", value: "<span class='marker'>[Remarks]</span>&nbsp" },
                   { text: "Agent", value: "<span class='marker'>[AgentID]</span>&nbsp" },
                   { text: "Assign Date", value: "<span class='marker'>[AssignDate]</span>&nbsp" },
                   { text: "LastPaymentDate", value: "<span class='marker'>[LastPaymentDate]</span>&nbsp" },
                   { text: "LastCallactedAMT", value: "<span class='marker'>[LastCallactedAMT]</span>&nbsp" },
                   { text: "TotalCallactedAMT", value: "<span class='marker'>[TotalCallactedAMT]</span>&nbsp" },
                   { text: "Address Name", value: "<span class='marker'>[AddressName]</span>&nbsp" },
                   { text: "Case Addess", value: "<span class='marker'>[CaseAddess]</span>&nbsp" },

                   { text: "Phone 01", value: "<span class='marker'>[Phone01]</span>&nbsp" },
                   { text: "Phone 02", value: "<span class='marker'>[Phone02]</span>&nbsp" },
                   { text: "Phone 03", value: "<span class='marker'>[Phone03]</span>&nbsp" },
                   { text: "Phone 04", value: "<span class='marker'>[Phone04]</span>&nbsp" },
                   { text: "Extension 1", value: "<span class='marker'>[Ext1]</span>&nbsp" },
                   { text: "Extension 2", value: "<span class='marker'>[Ext2]</span>&nbsp" },
                   { text: "Mobile No1", value: "<span class='marker'>[MobileNo1]</span>&nbsp" },
                   { text: "Mobile No2", value: "<span class='marker'>[MobileNo2]</span>&nbsp" },
                   { text: "Mobile No3", value: "<span class='marker'>[MobileNo3]</span>&nbsp" },
                   { text: "Mobile No4", value: "<span class='marker'>[MobileNo4]</span>&nbsp" },
                   { text: "PO Box", value: "<span class='marker'>[POBox]</span>&nbsp" },
                   { text: "Other 01", value: "<span class='marker'>[Other01]</span>&nbsp" },
                   { text: "Other 02", value: "<span class='marker'>[Other02]</span>&nbsp" },
                   { text: "Other 03", value: "<span class='marker'>[Other03]</span>&nbsp" },
                   { text: "Other 04", value: "<span class='marker'>[Other04]</span>&nbsp" },
                   { text: "Address 1", value: "<span class='marker'>[Address1]</span>&nbsp" },
                   { text: "Address 2", value: "<span class='marker'>[Address2]</span>&nbsp" },
                   { text: "Address 3", value: "<span class='marker'>[Address3]</span>&nbsp" },
                   { text: "Address 4", value: "<span class='marker'>[Address4]</span>&nbsp" },
                   { text: "Email 01", value: "<span class='marker'>[Email01]</span>&nbsp" },
                   { text: "Email 02", value: "<span class='marker'>[Email02]</span>&nbsp" },
                   { text: "Email 03", value: "<span class='marker'>[Email03]</span>&nbsp" },
                   { text: "Email 04", value: "<span class='marker'>[Email04]</span>&nbsp" },
               ]
           },
           "formatting",
        ];
        $scope.messages = { insertHtml: "Insert Marker" };
        $scope.stylesheets = [
                    "../Content/editorStyles.css"
        ];
    }

    $scope.AddCM00230 = function () {
       
        $scope.Clear();
        $scope.CM00230 = {};
        $scope.CM00230.Status = 1;
        $scope.DebtorAddressCodeType();
        $http.get('/Uti_Email/GetNextTransactionID?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.CM00230.MessageTRXID = data;
        }).finally(function () { });
        $scope.CM00230.SelectedTemplate = {};
        $scope.CM00230TemplateContant();
        $http.get('/CO_Email/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey + '&UserName=' + $scope.MY.UserName).success(function (data) {
            $scope.DemandTemplates = data;
        }).finally(function () { });
        $http.get('/SysUser/GetUserEmails?KaizenPublicKey=' + sessionStorage.PublicKey + '&UserName=' + $scope.MY.UserName).success(function (data) {
            $scope.SenderEmails = data;
        }).finally(function () { });
        $scope.LoadCM00230Page('Create');
    }
    $scope.EditCM00230 = function (MessageTRXID) {
        $scope.DebtorAddressCodeType();
        $http.get('/Uti_Email/GetSingle?KaizenPublicKey=' + sessionStorage.PublicKey + "&MessageTRXID=" + MessageTRXID).success(function (data) {
            $scope.CM00230 = data;
        }).finally(function () {
            $http.get('/CMS_DemandLetter/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey + '&UserName=' + $scope.MY.UserName).success(function (data) {
                $scope.DemandTemplates = data;
            }).finally(function () {
                $scope.CM00230.SelectedTemplate = $scope.GetSingle($scope.DemandTemplates, "TemplateID", $scope.CM00230.TemplateID);
                $scope.SelectedLookUp.DebtorAddressCodeType = $scope.GetSingle($scope.CM00009List, "AddressCode", $scope.CM00230.AddressCode);
                $scope.LoadCM00230Page('LetterView');
                $scope.CM00230TemplateContant();
            });

            //alert($scope.CM00230.TemplateContant)

        });
    };
    $scope.AgentBack = function (agent) {
        if (agent != null) {
            switch ($scope.CurrentControl) {
                case 'AgentAssign':
                    $scope.CM00230.AgentID = agent.AgentID;
                    $scope.CM00230.AssignDate = new Date();
                    break;
            }
        }
    };
    $scope.SaveData = function () {
        $http.post('/CMS_ClientClass/SaveData', { 'CM00230': $scope.CM00230, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
            if (data.Status == true) {
                $.bigBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#739E73",
                    timeout: 4000,
                    icon: "fa fa-check shake animated"
                });
                $scope.Cancel();
                $scope.GridRefresh('GridCMS_CM00230');
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
        $http.post('/CMS_ClientClass/UpdateData', { 'CM00230': $scope.CM00230, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
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
        $scope.CM00230 = { Status: 1 };
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
                $http.post('/CMS_ClientClass/DeleteData', { 'CM00230': $scope.CM00230, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
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
        $scope.PagesCM00230 = [];
    };
});