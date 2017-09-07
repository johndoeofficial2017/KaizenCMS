app.controller('CMS_CaseController', function ($scope, $http, $filter) {
    $scope.CM00203 = {};
    $scope.RelatedJointDebtors = [];
    $scope.SelectedLookUp = {};
    $scope.OrginalCM00203 = {};
    $scope.SelectedView = {};
    $scope.SelectedStatus = {};
    $scope.StatusChilds = [];
    $scope.StatusConditionChilds = [];
    $scope.SelectedLookUp.SelectedPriority = [];
    $scope.LoadLookUp = function () {
        $scope.CaseDocType();
        $scope.GetStatusType();
        $scope.DebtorAddressCodeType();
        $scope.DebtorPriority();
        $scope.CaseStatusList();
       
        $http.get('/Adm_Country/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.Countries = data;
        }).finally(function () { });
        $http.get('/Adm_City/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.Cities = data;
        }).finally(function () { });
        //$http.get('/Adm_Country/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
        //    $scope.Countries = data;
        //}).finally(function () { });
        //$http.get('/Adm_City/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
        //    $scope.Cities = data;
        //}).finally(function () { });

        $http.get('/Admin_DocumnetType/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey + '&ContactSourceID=6').success(function (data) {
            $scope.CaseDocumentTypes = data;
        }).finally(function () { });


        $http.get('/Set_Lookup01/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.Lookup01List = data;
            $scope.Lookup01ListOptions = {
                filter: "startswith",
                optionLabel: "-- Select Lookup01 --",
                dataTextField: "Lookup01Name",
                dataValueField: "Lookup01",
                dataSource: $scope.Lookup01List,
                value: $scope.CM00203.Lookup01
            };
        }).finally(function () { });
        $http.get('/Set_Lookup02/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.Lookup02List = data;
            $scope.Lookup02ListOptions = {
                filter: "startswith",
                optionLabel: "-- Select Type --",
                dataTextField: "Lookup02Name",
                dataValueField: "Lookup02",
                dataSource: $scope.Lookup02List,
                value: $scope.CM00203.Lookup02
            };
        }).finally(function () { });
    };
    $scope.LoadLookUp();
    $scope.CasePages = [];
    $scope.PageCaseStatus = 1;  // 1= new ; 2 Edit ; 3 FileAttachment


    $scope.LoadCasePage = function (ActionName) {
        var URIPath = "/" + $scope.ToolBar.ActiveScreen.ID + "/" + ActionName + "?KaizenPublicKey=" + sessionStorage.PublicKey;
        var Page = { url: URIPath, ActionName: ActionName };
        $scope.CasePages.push(Page);
    };
    $scope.openCaseActionPlanWindow = function () {
        var Jecket_indx = $scope.functiontofindIndexByKeyValue($scope.Jeckets, "JecketsID", $scope.CM00203.JecketsID);
        var Jecket_obj = $scope.Jeckets[Jecket_indx];
        $scope.CM00203.SelectedJecket = Jecket_obj;
        $scope.CaseActionPlanWindow.center().toFront().open();
        $scope.CaseActionPlanWindow.refresh({
            url: "/CMS_Case/CaseActionPlan?KaizenPublicKey=" + sessionStorage.PublicKey
        });
    };
    $scope.openCaseAssignHistoryWindow = function () {
        $scope.CaseAssignHistoryWindow.center().toFront().open();
        $scope.CaseAssignHistoryWindow.refresh({
            url: "/CMS/CMS_Case/CaseAssignHistory?KaizenPublicKey=" + sessionStorage.PublicKey
        });
    };


    var task_indx;
    $scope.OpenAgentPopUp = function (index) {
        task_indx = index;
        $scope.OpenkendoWindow('CMS_Agent', 'PopUp', null, 'AgentWithTask');
    };

    $scope.openComposeEmailWindow = function () {
        $scope.toEmails = [];
        $scope.ccEmails = [];
        $scope.bccEmails = [];
        $http.get('/SysUser/GetUserEmails?KaizenPublicKey=' + sessionStorage.PublicKey + '&UserName=' + $scope.MY.UserName).success(function (data) {
            $scope.SenderEmails = data;
        }).finally(function () { });

        $scope.ComposeEmailWindow.center().toFront().open();
        $scope.ComposeEmailWindow.refresh({
            url: "/Sys_ComposeEmail/Index?KaizenPublicKey=" + sessionStorage.PublicKey
        });
    };
    $scope.OpenTemplateEmailWindow = function () {
        $scope.toEmails = [];
        $scope.ccEmails = [];
        $scope.bccEmails = [];
        $http.get('/SysUser/GetUserEmails?KaizenPublicKey=' + sessionStorage.PublicKey + '&UserName=' + $scope.MY.UserName).success(function (data) {
            $scope.SenderEmails = data;
        }).finally(function () { });
        $http.get('/CMS_DemandLetter/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey + '&UserName=' + $scope.MY.UserName).success(function (data) {
            $scope.DemandTemplates = data;
        }).finally(function () { });
        $scope.TempalteEmailWindow.center().toFront().open();
        $scope.TempalteEmailWindow.refresh({
            url: "/CMS_DemandLetter/EmailTemplate?KaizenPublicKey=" + sessionStorage.PublicKey
        });
    };

    $scope.SendCaseEmail = function () {
        $http({
            url: '/CMS_Case/SendCaseByEmail?KaizenPublicKey=' + sessionStorage.PublicKey,
            method: "POST",
            data: {
                'CM00203': $scope.CM00203, 'toAddressList': $scope.toEmails, 'userEmailSetting': $scope.SelectedSender,
                'CcAddressList': $scope.ccEmails, 'BccAddressList': $scope.bccEmails
            },
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
                    timeout: 4000,
                    icon: "fa fa-check shake animated"
                });
                $scope.CloseComposeEmailWindow();
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
    $scope.SendCaseTemplateEmail = function () {
        $http({
            url: '/CMS_Case/SendTemplateEmail?KaizenPublicKey=' + sessionStorage.PublicKey,
            method: "POST",
            data: {
                'CM00203': $scope.CM00203, 'toAddressList': $scope.toEmails, 'userEmailSetting': $scope.SelectedSender,
                'CcAddressList': $scope.ccEmails, 'BccAddressList': $scope.bccEmails, TemplateContant: $scope.SelectedTemplate.TemplateContant
            },
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
                    timeout: 4000,
                    icon: "fa fa-check shake animated"
                });
                $scope.CloseTemplateEmailWindow();
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


    $scope.SaveCaseActionPlan = function () {
        $http.post('/CMS_Case/UpdateData', { 'CM00203': $scope.CM00203, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
            if (data.Status == true) {
                $.bigBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#739E73",
                    timeout: 4000,
                    icon: "fa fa-check shake animated"
                });
                $scope.CloseActionPlanWindow();
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
    $scope.SaveCaseAssign = function () {
        if ($scope.OrginalCM00203.AgentID != $scope.CM00203.AgentID) {
            $http.post('/CMS_Case/SaveCaseAssignHistoryData', { 'CM00203': $scope.CM00203, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
                if (data.Status == true) {
                    $.bigBox({
                        title: data.Massage,
                        content: data.Description,
                        color: "#739E73",
                        timeout: 4000,
                        icon: "fa fa-check shake animated"
                    });
                    $scope.CloseAssignHistoryWindow();
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
                title: "No Changes have been made!!",
                content: "",
                color: "#C46A69",
                iconSmall: "fa fa-times fa-2x fadeInRight animated",
                timeout: 4000
            });
            $scope.CloseAssignHistoryWindow();
        }
    };


    $scope.ApplyPivotFilter = function () {
        $scope.PivotGridRefresh('PivotGridCMS_Case');
    };
    $scope.PivotTable = function () {
        removeEntityPage($scope.CasePages);
        $scope.LoadCasePage('PivotTable');
        $scope.Clear();
    };

    $scope.AgentCases = function () {
        removeEntityPage($scope.CasePages);
        $scope.LoadCasePage('AgentCases');
        $scope.Clear();
    };
    $scope.ApplyCaseViewFilter = function () {
        $scope.GridRefresh('GridCMS_CaseView01');
    };

    $scope.CloseActionPlanWindow = function () {
        $scope.CaseActionPlanWindow.close();
    };
    $scope.CloseAssignHistoryWindow = function () {
        $scope.CaseAssignHistoryWindow.close();
    };

    $scope.CloseComposeEmailWindow = function () {
        $scope.toEmails = [];
        $scope.ccEmails = [];
        $scope.bccEmails = [];
        $scope.ComposeEmailWindow.close();
    };
    $scope.CloseTemplateEmailWindow = function () {
        $scope.toEmails = [];
        $scope.ccEmails = [];
        $scope.bccEmails = [];
        $scope.TempalteEmailWindow.close();
    };

    //------------------------Filter
    $scope.MYFilter.get = function (field) {
        var DS = {};
        if (field == 'CaseStatusID') {
            DS.key = "CaseStatusname";
            DS.source = $scope.CaseStatuses;
        }
        if (field == 'ActionPlanID') {
            DS.key = "ActionPlanName";
            DS.source = $scope.Actionplans;
        }
        if (field == 'JecketsID') {
            DS.key = "JecketName";
            DS.source = $scope.Jeckets;
        }
        if (field == 'TRXTypeID') {
            DS.key = "TrxTypeName";
            DS.source = $scope.TRXTypes;
        }
        return DS;
    };
    $scope.UpdateColumnChanged = function () {
        if ($scope.Filter.SelectedColumn.type == 'dropdown') {
            var DS = $scope.MYFilter.get($scope.Filter.SelectedColumn.field);
            $scope.Filter.SelectedColumn.key = DS.key;
            $scope.Filter.SelectedColumn.field = $scope.Filter.SelectedColumn.field;
            $scope.Filter.SelectedColumn.source = DS.source;
            $scope.CaseBulkUploadActionWindow.refresh();
        }
        $scope.Filter.field = $scope.Filter.SelectedColumn.field;
    };



    $scope.ChildStatusChanged = function () {
        if (angular.isDefined($scope.CM00203.SelectedChild)) {
            $scope.CM00203.CaseStatusChild = $scope.CM00203.SelectedChild.CaseStatusID;
        }
    };



    $scope.ViewManage = function () {
        $scope.MainGridURL = "/Sys_View/ScreenMangement?ScreenID=12";
    };
    $scope.JecketChanged = function () {
        if (angular.isDefined($scope.CM00203.SelectedJecket)) {
            $scope.CM00203.JecketsID = $scope.CM00203.SelectedJecket.JecketsID;
            $scope.CM00203.JecketName = $scope.CM00203.SelectedJecket.JecketName;
        }
    };


    $scope.openClient = function () {
        //var dlg = dialogs.create('/CMS_Client/Create', 'CMS_CO_ClientController', { CM00110: $scope.CM00110 });
        //dlg.result.then(function (client) {
        //    $scope.CM00110 = client;
        //    $scope.link = '/Photo/ClientTemp/' + kaizenTrim($scope.CM00110.PhotoExtension);
        //    $scope.CM00203.ClientID = client.ClientID;
        //    $scope.CM00203.ClientName = client.ClientName;
        //    $scope.checkClientName();
        //});
    };

    $scope.CaseAction = function (CaseRef) {
        $scope.LoadCasePage('CaseAction');
        $scope.EditCase(CaseRef);
    };


    $scope.Clear = function () {
        $scope.ExcelColumns = [];
        $scope.KaizenColumn = [];
        $scope.Kaizen00003 = [];
        $scope.GridFilters = [];

        $scope.Casedocuments = [];
        $scope.InsertedDocuments = [];
        $scope.UpdatedDocuments = [];
        $scope.DeletedDocuments = [];
        $scope.RelatedJointDebtors = [];

        $scope.AssignedCases = [];

        $scope.selectedStatusList = [];
        $scope.selectedAgents = [];
        $scope.selectedClients = [];

        $scope.AgentdataSource = new kendo.data.DataSource({ data: [] });
        $scope.ClientdataSource = new kendo.data.DataSource({ data: [] });

        $scope.CM00203 = {};
        $scope.OrginalCM00203 = {};
        $scope.CM10207 = { Status: 0 };
        $scope.CM00208 = { Status: 1 };
        $scope.CM00203.Status = 1;
        $scope.CM00203.IsJoint = false;
        $scope.CM00203.CurrencyCode = $scope.MY.CurrencyCode;
        $scope.CM00203.DecimalDigit = $scope.MY.DecimalDigit;
        $scope.CM00203.ExchangeTableID = $scope.MY.ExchangeTableID;
        $scope.CM00203.IsMultiply = $scope.MY.IsMultiply;
        $scope.CM00203.ExchangeRate = $scope.MY.ExchangeRate;
        $scope.CM00203.ClientID = '';
        $scope.CM00203.DebtorID = '';
        $scope.OpenDocument = false;
        $scope.ActionString = '';
        $scope.UploadObject = { UploadStep: 0 };


        $scope.SelectedLookUp.SelectedYear = null;
    };

    $scope.Delete = function () {
        $.SmartMessageBox({
            title: "Delete Record",
            content: "Are you sure you want to delete this?",
            buttons: '[No][Yes]'
        }, function (ButtonPressed) {
            if (ButtonPressed === "Yes") {
                $http.post('/CMS_Case/DeleteData', { 'CM00203': $scope.CM00203, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
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
    //$scope.PartnerBack = function (CM00130) {
    //    $scope.CM00203.PartnerID = CM00130.PartnerID;
    //    $scope.CM00203.PartnerName = CM00130.PartnerName;
    //}
    //$scope.LegalBack = function (CM00140) {
    //    alert('ddddddddd');
    //    $scope.CM00203.LegalID = CM00140.LegalID;
    //    $scope.CM00203.LegalName = CM00140.LegalName;
    //}
    $scope.ClientBack = function (client) {
        if (client == null) return;
        switch ($scope.CurrentControl) {
            case 'Main':
                $scope.CM00110 = client;
                $scope.link = '/Photo/ClientTemp/' + kaizenTrim($scope.CM00110.PhotoExtension);
                $scope.CM00203.ClientID = client.ClientID;
                $scope.CM00203.ClientName = client.ClientName;
                break;
            case 'ClientPivotGrid':
                $scope.ClientdataSource.add(client);
                $scope.selectedClients.push(client.ClientID);
                break;
            case 'ClientPivotTable':
                $scope.CM00203.ClientID = client.ClientID;
                break;
            case 'ClientCases':
                $scope.CM00203.ClientID = client.ClientID;
                $scope.CM00203.ClientName = client.ClientName;
                break;
            case 'UploadView':
                $scope.UploadObject.ClientID = client.ClientID;
                break;
        }
    };
    $scope.ContractBack = function (contract) {
        if (contract != null) {
            switch ($scope.CurrentControl) {
                case 'Main':
                    $scope.CM00203.ContractID = contract.ContractID;
                    $scope.CM00203.ContractName = contract.ContractName;
                    $scope.CM00203.CurrencyCode = contract.CurrencyCode;
                    $scope.CM00203.DecimalDigit = contract.DecimalDigit;
                    //if (angular.isDefined($scope.CM00203.CurrencyCode) && $scope.CM00203.CurrencyCode != "") {
                    //    $http.get('/GL_Currency/GetSingle?KaizenPublicKey=' + sessionStorage.PublicKey, { params: { CurrencyCode: $scope.CM00203.CurrencyCode } }).success(function (data) {
                    //        $scope.GL00102 = data;
                    //        $scope.CM00203.DecimalDigit = data.DecimalDigit;
                    //    }).finally(function () { });
                    //}
                    break;
                case 'ContractPivotTable':
                    $scope.CM00203.ContractID = contract.ContractID;
                    break;
                case 'UploadView':
                    $scope.UploadObject.ContractID = contract.ContractID;
                    break;
            }
        }
    };
    $scope.DebtorBack = function (debtor) {
        if (debtor == null) return;
        switch ($scope.CurrentControl) {
            case 'Main':
                $scope.CM00203.CIFNumber = debtor.DebtorID;
                $scope.CM00203.CIFName = debtor.EnglishFullName;
                $scope.CM00203.CPRCRNo = debtor.CPRCRNo;
                $scope.CM00203.CaseAddess = debtor.AddressEnglish;
                $scope.CM00203.NationalityID = debtor.NationalityID;
                $http.get('/Co_Debtor/GetJointAccount?CIFNumber=' + $scope.CM00203.CIFNumber + '&KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
                    $scope.RelatedJointDebtors = data;
                    if ($scope.RelatedJointDebtors.length > 0) {
                        $scope.CM00203.IsJoint = true;
                    }
                }).finally(function () { });

                $scope.GridRefresh("GridRelatedDebtorCases");
                $scope.DebtorContactTypeID();
                break;
            case 'ViewDebtor':
                $scope.CM00100 = debtor;
                $scope.CM00100.Status = 2;
                //alert($scope.CM00100.FirstNameEnglish)
                $scope.IsOpenItemPanel = true;
                break;
            case 'JointExisitng':
                $scope.CM00100 = debtor;
                $scope.CM00100.Status = 0;
                var Debtorindex = $scope.GetSingleIndex($scope.RelatedJointDebtors, "DebtorID", debtor.DebtorID);
                if (Debtorindex == null) {
                    $scope.RelatedJointDebtors.push($scope.CM00100);
                    $http.post('/Co_Debtor/SaveDataJointAccount',
                      {
                          'CIFNumber': $scope.CM00203.CIFNumber,
                          'DebtorID': $scope.CM00100.DebtorID,
                          'KaizenPublicKey': sessionStorage.PublicKey
                      }).success(function (data) {
                          if (data.Status == true) {
                              $.bigBox({
                                  title: data.Massage,
                                  content: data.Description,
                                  color: "#739E73",
                                  timeout: 3000,
                                  icon: "fa fa-check shake animated"
                              });
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
                else
                    $scope.RelatedJointDebtors.splice(Debtorindex, 1, $scope.CM00100);
                $scope.ClearDebtorDetails();
                $scope.IsOpenItemPanel = false;
                break;
        }

    };
    $scope.DebtorAddressBack = function (address) {
        if (address != null) {
            $scope.CM00203.AddressCode = address.AddressCode;
            $scope.CM00203.AddressName = address.AddressName;
            $scope.CM00203.WebSite = address.WebSite;
            $scope.CM00203.CountryID = address.CountryID;
            $scope.CM00203.CityID = address.CityID;
            $scope.CM00203.Phone01 = address.Phone01;
            $scope.CM00203.Phone02 = address.Phone02;
            $scope.CM00203.Ext1 = address.Ext1;
            $scope.CM00203.Ext2 = address.Ext2;
            $scope.CM00203.MobileNo1 = address.MobileNo1;
            $scope.CM00203.MobileNo2 = address.MobileNo2;
            $scope.CM00203.POBox = address.POBox;
            $scope.CM00203.Other01 = address.Other01;
            $scope.CM00203.Other02 = address.Other02;
            $scope.CM00203.Address1 = address.Address1;
            $scope.CM00203.Address2 = address.Address2;
            $scope.CM00203.Email01 = address.Email01;
            $scope.CM00203.Email02 = address.Email02;
        }
    };

    $scope.AgentBack = function (agent) {
        if (agent != null) {
            switch ($scope.CurrentControl) {
                case 'Main':
                    $scope.CM00203.AgentID = agent.AgentID;
                    $scope.CM00203.AssignDate = new Date();
                    break;
                case 'AssignActionPlan':
                    $scope.CM00203.JecketAssignTo = agent.AgentID;
                    break;
                case 'AgentAssign':
                    $scope.Assign.AgentID = agent.AgentID;
                    break;
                case 'TaskAssign':
                    $scope.CM00213.AgentID = agent.AgentID;
                    break;
                case 'AgentPayment':
                    $scope.CM10207.AgentID = agent.AgentID;
                    break;
                case 'AgentWithTask':
                    $scope.StatusTasks[task_indx].AgentID = agent.AgentID;
                    break;
                case 'AgentPivotGrid':
                    $scope.AgentdataSource.add(agent);
                    $scope.selectedAgents.push(agent.AgentID);
                    break;
                case 'AgentCases':
                    $scope.CM00203.AgentID = agent.AgentID;
                    break;
            }
        }
    };
    $scope.CaseBatchBack = function (batch) {
        if (batch == null) return;
        $scope.CM00203.BatchID = batch.BatchID;
    };
    $scope.CaseHistoryBack = function (caseobj) {
        if (caseobj != null) {
            removeEntityPage($scope.CasePages);
            $scope.LoadCasePage('CaseHistory');
            $scope.CM20203 = {};
            $scope.CM20203 = caseobj;
            $scope.CM20203.SelectedType = $scope.GetSingle($scope.TRXTypes, "TRXTypeID", $scope.CM20203.TRXTypeID);
            $scope.CM20203.SelectedStatus = $scope.GetSingle($scope.CM00700List, "CaseStatusID", $scope.CM20203.CaseStatusID);
            if ($scope.CM20203.SelectedStatus.IsHasChild) {
                $http.get('/CMS_CaseStatus/GetStatusChilds?KaizenPublicKey=' + sessionStorage.PublicKey, { params: { CaseStatusParent: $scope.CM00203.SelectedStatus.CaseStatusID } }).success(function (data) {
                    if (data.length > 0) {
                        $scope.StatusChilds = data;
                        $scope.HasChild = true;
                    } else {
                        $scope.StatusChilds = [];
                        $scope.HasChild = false;
                    }
                }).finally(function () {

                    $scope.CM20203.SelectedChild = $scope.GetSingle($scope.StatusChilds, "CaseStatusID", $scope.CM20203.CaseStatusChild);
                });
            }
            //$scope.progress = ($scope.CM20203.TotalCallactedAMT / $scope.CM20203.ClaimAmount) * 100;
            //$scope.onProgressChange = function (e) {
            //    var pr = e.sender;
            //    pr.progressWrapper.css({
            //        "background-image": "none",
            //        "border-image": "none"
            //    });
            //    if (e.value >= 0 && e.value <= 50) {
            //        pr.progressWrapper.css({
            //            "background-color": "#EE9F05",
            //            "border-color": "#EE9F05"
            //        });
            //    }
            //    else if (e.value > 50 && e.value <= 75) {
            //        pr.progressWrapper.css({
            //            "background-color": "#428bca",
            //            "border-color": "#428bca"
            //        });
            //    }
            //    else {
            //        pr.progressWrapper.css({
            //            "background-color": "#8EBC00",
            //            "border-color": "#8EBC00"
            //        });
            //    }
            //};
            $scope.GridRefresh("GridCMS_CaseHistroyDocumentByCase");
        }
    };




    $scope.AddressTypeChanged = function () {
        var grid = $("#GridCo_DebtorAddressByDebtor").data("kendoGrid");
        var dataSourceData = [];
        angular.copy(grid.dataSource.data(), dataSourceData);
        var found = $scope.GetSingle(dataSourceData, "AddressCode", $scope.AddressCode.AddressCode);
        if (found != null) {
            $scope.AddressCode = found;
            $scope.AddressCode.Status = 2;
            return;
        }
        else {
            $scope.AddressCode = { Status: 1, AddressCode: $scope.AddressCode.AddressCode };
            return;
        }
    };
    $scope.JointChanged = function () {
        if ($scope.CM00203.IsJoint) {
            $scope.CM00203.DebtorID = "";
            $scope.CM00203.DebtorName = "";
            $scope.CM00203.CIFNumber = "";
            $scope.CM00203.CIFName = "";
            $scope.CM00203.CPRCRNo = "";
            $scope.CM00203.CaseAddess = "";
            $scope.RelatedJointDebtors = [];
        }
    };

    $scope.CM00203.BookingDate = null;
    $scope.CM00203.ClosingDate = null;
    $scope.maxDate = new Date();
    $scope.minDate = new Date(1900, 0, 1, 0, 0, 0);
    $scope.fromDateChanged = function () {
        $scope.minDate = new Date($scope.CM00203.fromDateString);
    };
    $scope.toDateChanged = function () {
        $scope.maxDate = new Date($scope.CM00203.toDateString);
    };
    ///------------------------------------Integration Manager-----------------------------------
    $scope.KaizenColumn = [];
    $scope.Kaizen00003 = [];
    $scope.ExcelColumns = [];
    $scope.KaizenDropDownTable = []; // we have to delete items where  KaizenDropDownTable + run only if dynColumn.SourceValue == 2
    var TableName;
    $scope.UploadCase = function () {
        removeEntityPage($scope.CasePages);
        $scope.LoadCasePage('IntegrationManagerView');
        $http.get('/IntegrationManager/GetKaizenTable?KaizenPublicKey=' + sessionStorage.PublicKey, { params: { ScreenID: 12 } }).success(function (data) {
            $scope.KaizenColumn = data;
        }).finally(function () { });
    };
    $scope.CaseUploaded = function (Columns) {
        TableName = Columns.TableName
        $scope.ExcelColumns = Columns.ExcelColumns;
    };
    $scope.SaveIntegration = function () {
        $http({
            url: '/IntegrationManager/SaveData?KaizenPublicKey=' + sessionStorage.PublicKey,
            method: "POST",
            data: $.param({ KaizenColumn: $scope.KaizenColumn, Kaizen00003: $scope.Kaizen00003, TableName: TableName }),
            headers: {
                'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8'
            }
        }).success(function (data) {
            $scope.Cancel();
        }).error(function (data, status, headers, config) { alert(); });
    };
    $scope.launchIntegrationSourceValue = function (column) {
        var dlg = dialogs.create('/IntegrationManager/PopUp?KaizenPublicKey=' + sessionStorage.PublicKey, 'IntegrationSourceValue', column);
        dlg.result.then(function (data) {
            column.FieldValue = data.Index;
            if (column.DynamicTableName != null) {
                if (kaizenTrim(column.DynamicTableFieldValue) == kaizenTrim(column.FieldValue)) {
                    $http.get('/IntegrationManager/GetDynamicTableField?KaizenPublicKey=' + sessionStorage.PublicKey + "&DynamicTableName=" + column.DynamicTableName + "&ScreenID=12").success(function (data) {
                        data.forEach(function (element, index) {
                            var obj003 = {
                                FieldID: element.FieldID,
                                FieldName: element.FieldName,
                                FieldTitle: element.FieldTitle,
                                kaizenTableName: element.kaizenTableName,
                                KaizenDropDownTable: element.KaizenDropDownTable,
                                FieldType: element.FieldType
                            };
                            $scope.Kaizen00003.push(obj003);
                        })
                    }).finally(function () { });
                }
            }
        });
    };
    $scope.GetKaizenDropDownTable = function (dynColumn) {
        $http.get('/IntegrationManager/GetKaizenDropDownTable?KaizenPublicKey=' + sessionStorage.PublicKey + "&KaizenDropDownTable=" + dynColumn.KaizenDropDownTable).success(function (data) {
            data.forEach(function (element, index) {
                var objDyN = {
                    Index: element.Index,
                    ColumnName: element.ColumnName,
                    KaizenDropDownTable: dynColumn.KaizenDropDownTable
                };
                $scope.KaizenDropDownTable.push(objDyN);
            })
        }).finally(function () { });
    };
    $scope.ShowFixedValue = function (kaizenTableName) {
        if (kaizenTableName == null)
            return false;
        else
            return true;
    }
    //-------------------------- Payments By Wael
    $scope.CM10207 = {};
    $scope.CasePayment = function () {
        $scope.CM10207.Status = 1;
        $scope.CM10207.PaymentMethodID = 1;
        $scope.CM10207.CurrencyCode = $scope.MY.CurrencyCode;
        $scope.CM10207.DecimalDigit = $scope.MY.DecimalDigit;
        $scope.CM10207.CurrencySymbol = $scope.MY.CurrencySymbol;
        $scope.CM10207.CheckbookCode = $scope.MY.CheckbookCode;
        $scope.CM10207.IsMultiply = $scope.MY.IsMultiply;
        $scope.CM10207.ExchangeTableID = $scope.MY.ExchangeTableID;
        $scope.CM10207.ExchangeRateID = $scope.MY.ExchangeRateID;
        $scope.CM10207.ExchangeRate = $scope.MY.ExchangeRate;
        $scope.CM10207.PaymentDate = new Date();
        $scope.CM10207.IsApproved = false;
        $http.get('/CMS_Payment/GetNextPaymentTransactionNumber?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.CM10207.PaymentID = data;
        }).finally(function () { });
        if (angular.isDefined($scope.CM00203.CaseRef)) {
            $scope.CM10207.CaseRef = $scope.CM00203.CaseRef;
            $scope.CM10207.AgentID = $scope.CM00203.AgentID;
            $scope.GetNextPaymentNumber();
        }
    };
    $scope.GetNextPaymentNumber = function () {
        if (angular.isUndefined($scope.CM10207.CheckbookCode) || $scope.CM10207.CheckbookCode == null) {
            return;
        }
        $http.get('/CMS_Payment/GetNextPaymentNumber?KaizenPublicKey=' + sessionStorage.PublicKey,
            {
                params: {
                    PaymentMethodID: $scope.CM10207.PaymentMethodID,
                    CheckbookCode: $scope.CM10207.CheckbookCode
                }
            })
            .success(function (data) {
                $scope.CM10207.PaymentNumber = data;
            }).finally(function () { });
    };
    $scope.LoadCM10207 = function (payment) {
        if (payment.IsApproved) {
            kendo.alert("you can not Modify  approved Payment");
            return;
        }
        $http.get('/CMS/CMS_Payment/GetSingle?KaizenPublicKey=' + sessionStorage.PublicKey + "&PaymentID=" + payment.PaymentID).success(function (data) {
            $scope.CM10207 = data;
            $scope.CM10207.TRXTypeID = $scope.CM00203.TRXTypeID;
            $scope.CM10207.PaymentDate = kendoparsetoDate($scope.CM10207.PaymentDate);
            $scope.CM10207.CreatedDate = kendoparsetoDate($scope.CM10207.CreatedDate);
        }).finally(function () {
            $scope.CM10207.Status = 2;
        });
    };
    $scope.SavePayment = function () {
        $scope.CM10207.TRXTypeID = $scope.CM00203.TRXTypeID;
        var action = "SaveDataObject";
        if ($scope.CM10207.Status == 2)
            action = "UpdateDataObject";
        $http.post('/CMS_Payment/' + action, {
            'CM10207': $scope.CM10207,
            'KaizenPublicKey': sessionStorage.PublicKey
        }).success(function (data) {
            if (data.Status == true) {
                $.bigBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#739E73",
                    timeout: 3000,
                    icon: "fa fa-check shake animated"
                });
                $scope.EditCase($scope.CM00203.CaseRef);
            }
        }).error(function (data, status, headers, config) { alert(data); });
    };

    $scope.UpdatePayment = function () {
        $http.post('/CMS_Payment/UpdateDataObject', { 'CM10207': $scope.CM10207, 'KaizenPublicKey': sessionStorage.PublicKey })
            .success(function (data) {
                if (data.Status == true) {
                    Notify(data.Massage, 'bottom-right', '5000', 'success', 'fa-check', true);
                    $scope.Cancel();
                    $scope.GridRefresh('GridCMS_CasePaymentHistory');
                }
                else {
                    Notify(data.Massage, 'bottom-right', '5000', 'danger', 'fa-bolt', true);
                }
            }).error(function (data, status, headers, config) { alert(); });
    };

    $scope.DeletePayment = function (CM10207) {
        if (CM10207.IsApproved) {
            kendo.alert("you can not Delete  approved Payment");
            return;
        }
        bootbox.confirm("Are you sure?", function (result) {
            if (result) {
                $http.post('/CMS_Payment/DeleteDataObject', { 'PaymentID': CM10207.PaymentID, 'KaizenPublicKey': sessionStorage.PublicKey })
   .success(function (data) {
       if (data.Status == true) {
           Notify(data.Massage, 'bottom-right', '5000', 'success', 'fa-check', true);
           $scope.Cancel();
           $scope.GridRefresh('GridCMS_CasePaymentHistory');
       }
       else {
           Notify(data.Massage, 'bottom-right', '5000', 'danger', 'fa-bolt', true);
       }
   }).error(function (data, status, headers, config) { alert(); });
            }
        });
    };


    //------------------------- Sachin
    $scope.CM10307 = {};
    $scope.ManualPaymentReasons = [];
    $scope.ManualPayment = function () {

        $scope.CM10307.AgentID = $scope.MY.AgentID;
        $scope.CM10307.PaymentMethodID = 1;
        $scope.CM10307.CurrencyCode = $scope.MY.CurrencyCode;
        $scope.CM10307.DecimalDigit = $scope.MY.DecimalDigit;
        $scope.CM10307.CurrencySymbol = $scope.MY.CurrencySymbol;
        $scope.CM10307.CheckbookCode = $scope.MY.CheckbookCode;
        $scope.CM10307.IsMultiply = $scope.MY.IsMultiply;
        $scope.CM10307.ExchangeTableID = $scope.MY.ExchangeTableID;
        $scope.CM10307.ExchangeRateID = $scope.MY.ExchangeRateID;
        $scope.CM10307.ExchangeRate = $scope.MY.ExchangeRate;

        //$http.get('/CMS/CMS_Payment/GetNextManualPaymentTransactionNumber?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
        //    $scope.CM10307.PaymentID = data;
        //}).finally(function () { });
        if (angular.isDefined($scope.CM00203.CaseRef)) {
            $scope.CM10307.CaseRef = $scope.CM00203.CaseRef;
        }
        if (angular.isDefined($scope.CM10307.CheckbookCode)) {
            $scope.GetNextManualPaymentNumber();
        }
        if ($scope.ManualPaymentReasons.length > 0) return;
        $http.get('/CMS/CMS_Set_Reason/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.ManualPaymentReasons = data;
        }).finally(function () { });
    };

    //------------------------- Sachin
    $scope.SaveExchangeRate = function () {
        $scope.GL00012.ExchangeTableID = $scope.CM00203.ExchangeTableID;
        $scope.GL00012.CurrencyCode = $scope.CM00203.CurrencyCode;
        $scope.GL00012.DecimalDigit = $scope.CM00203.DecimalDigit;
        $http.post('/GL_ExchangeRate/SaveData', { 'GL00012': $scope.GL00012, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
            if (data.Status == true) {
                $scope.ClearExchangeRate();
                $scope.GridRefresh('GridGL_ExchangeRatePopUp');
                $.smallBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#739E73",
                    timeout: 4000,
                    icon: "fa fa-check shake animated"
                });
            }
            else {
                $.smallBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#C46A69",
                    timeout: 4000,
                    icon: "fa fa-bolt swing animated"
                });
            }
        }).error(function (data, status, headers, config) { alert(); });
    };
    $scope.ClearExchangeRate = function () {
        $scope.GL00012 = {};
    };
    $scope.OpenExchangeRateWindow = function (str) {
        $scope.GL00012 = {};
        if (str == "Main") {
            $scope.$parent.ParmObject = $scope.CM10207;
        }
        else if (str == "UploadView") {
            $scope.$parent.ParmObject = $scope.UploadObject;
        }
        else if (str == "Case") {
            $scope.$parent.ParmObject = {
                CurrencyCode: $scope.CM00203.CurrencyCode,
                ExchangeTableID: $scope.CM00203.ExchangeTableID
            };
            $scope.$parent.CurrentControl = "Case";
        }
        $scope.ExchangeRateWindow.center().toFront().open();
        $scope.ExchangeRateWindow.refresh({
            url: "/GL_ExchangeRate/PopUpRates?KaizenPublicKey=" + sessionStorage.PublicKey
        });
    };

    $scope.CurrencyBack = function (currency) {
        if (currency != null) {
            switch ($scope.CurrentControl) {
                case 'Case':
                    $scope.CM00203.DecimalDigit = currency.DecimalDigit;
                    $scope.CM00203.CurrencyCode = currency.CurrencyCode;
                    $scope.CM00203.IsMultiply = currency.IsMultiply;
                    $scope.CM00203.ExchangeRate = currency.ExchangeRate;
                    $scope.changeFormat(currency.CurrencyCode, currency.DecimalDigit, 'currency', false);
                    var kendoWindow = $("#MainDialog").data("kendoWindow");
                    kendoWindow.close();
                    break;
                case 'Main':
                    $scope.CM10207.DecimalDigit = currency.DecimalDigit;
                    $scope.CM10207.CurrencyCode = currency.CurrencyCode;
                    $scope.CM10207.ExchangeTableID = currency.ExchangeTableID;
                    $scope.CM10207.IsMultiply = currency.IsMultiply;
                    $scope.CM10207.ExchangeRateID = currency.ExchangeRateID;
                    $scope.CM10207.ExchangeRate = currency.ExchangeRate;
                    var kendoWindow = $("#MainDialog").data("kendoWindow");
                    kendoWindow.close();
                    break;
                case 'UploadView':
                    $scope.UploadObject.DecimalDigit = currency.DecimalDigit;
                    $scope.UploadObject.CurrencyCode = currency.CurrencyCode;
                    $scope.UploadObject.ExchangeTableID = currency.ExchangeTableID;
                    $scope.UploadObject.IsMultiply = currency.IsMultiply;
                    $scope.UploadObject.ExchangeRateID = currency.ExchangeRateID;
                    $scope.UploadObject.ExchangeRate = currency.ExchangeRate;
                    var kendoWindow = $("#MainDialog").data("kendoWindow");
                    kendoWindow.close();
                    break;
            }
        }
    };
    $scope.ExchangeRateBack = function (rate) {
        if (rate != null) {
            switch ($scope.CurrentControl) {
                case 'Main':
                    $scope.CM10207.ExchangeRateID = rate.ExchangeRateID;
                    $scope.CM10207.ExchangeRate = rate.ExchangeRate;
                    break;
                case 'UploadView':
                    $scope.UploadObject.ExchangeRateID = rate.ExchangeRateID;
                    $scope.UploadObject.ExchangeRate = rate.ExchangeRate;
                    break;
                case 'Case':
                    $scope.CM00203.ExchangeRateID = rate.ExchangeRateID;
                    $scope.CM00203.ExchangeRate = rate.ExchangeRate;
                    break;
            }
        }
    };
    $scope.ExchangeTableBack = function (table) {
        if (table != null) {
            switch ($scope.CurrentControl) {
                case 'Main':
                    $scope.CM10207.ExchangeTableID = table.ExchangeTableID;
                    $scope.CM10207.IsMultiply = table.IsMultiply;
                    break;
                case 'UploadView':
                    $scope.UploadObject.ExchangeTableID = table.ExchangeTableID;
                    $scope.UploadObject.IsMultiply = table.IsMultiply;
                    break;
                case 'Case':
                    $scope.CM00203.ExchangeTableID = table.ExchangeTableID;
                    $scope.CM00203.IsMultiply = table.IsMultiply;
                    break;
            }
            var kendoWindow = $("#MainDialog").data("kendoWindow");
            kendoWindow.close();
        }
    };


    $scope.CheckbookBack = function (checkbook) {

        if (checkbook != null) {
            if ($scope.CurrentControl == "ManualPayment") {

                $scope.CM10307.CheckbookCode = checkbook.CheckbookCode;
                $scope.GetNextManualPaymentNumber();
                if (angular.isUndefined($scope.CM10307.CurrencyCode)) {
                    $scope.CM10307.CurrencyCode = checkbook.CurrencyCode;
                    $http.get('/GL_Currency/GetSingleManual?KaizenPublicKey=' + sessionStorage.PublicKey,
                    { params: { CurrencyCode: checkbook.CurrencyCode } }).success(function (currency) {
                        $scope.CM10307.DecimalDigit = currency.DecimalDigit;
                        $scope.CM10307.CurrencyCode = currency.CurrencyCode;
                        $scope.CM10307.CurrencySymbol = currency.CurrencySymbol;
                        $scope.CM10307.ExchangeTableID = currency.ExchangeTableID;
                        $scope.CM10307.IsMultiply = currency.IsMultiply;
                        $scope.CM10307.ExchangeRateID = currency.ExchangeRateID;
                        $scope.CM10307.ExchangeRate = currency.ExchangeRate;
                    }).finally(function () { });
                }
            }
            else {
                $scope.CM10207.CheckbookCode = checkbook.CheckbookCode;
                $scope.CM10207.IsOneSerialNumberSale = checkbook.IsOneSerialNumberSale;
                $scope.GetNextPaymentNumber();
                if (angular.isUndefined($scope.CM10207.CurrencyCode)) {
                    $scope.CM10207.CurrencyCode = checkbook.CurrencyCode;
                    $http.get('/GL_Currency/GetSingle?KaizenPublicKey=' + sessionStorage.PublicKey,
                    { params: { CurrencyCode: checkbook.CurrencyCode } }).success(function (currency) {
                        $scope.CM10207.DecimalDigit = currency.DecimalDigit;
                        $scope.CM10207.CurrencyCode = currency.CurrencyCode;
                        $scope.CM10207.CurrencySymbol = currency.CurrencySymbol;
                        $scope.CM10207.ExchangeTableID = currency.ExchangeTableID;
                        $scope.CM10207.IsMultiply = currency.IsMultiply;
                        $scope.CM10207.ExchangeRateID = currency.ExchangeRateID;
                        $scope.CM10207.ExchangeRate = currency.ExchangeRate;
                    }).finally(function () { });
                }
            }
        }
    };
    $scope.BankBack = function (bank) {
        if (bank != null) {
            if ($scope.CurrentControl == "ManualPayment") {
                $scope.CM10307.BankCode = bank.BankCode;
            }
            else {
                alert(bank.BankName)
                $scope.CM10207.BankCode = bank.BankCode;
                $scope.CM10207.BankName = bank.BankName;
            }
        }
    };
    $scope.AmountChanged = function () {
        if (parseFloat($scope.CM10207.Amount) > parseFloat($scope.CM00203.ClaimAmount) && parseFloat($scope.CM10207.Amount) >= 0) {
            $scope.CM10207.Amount = 0;
        }
    };

    $scope.ManualAmountChanged = function () {

        if (parseFloat($scope.CM10307.Amount) > parseFloat($scope.CM00203.ClaimAmount) && parseFloat($scope.CM10307.Amount) >= 0) {
            $scope.CM10307.Amount = 0;
        }
    };

    $scope.ExchangeTableChanged = function () {
        $scope.CM10207.ExchangeTableID = $scope.CM10207.SelectedExchangeTable.ExchangeTableID;
        $scope.CM10207.IsMultiply = $scope.CM10207.SelectedExchangeTable.IsMultiply;
        var grid = $("#GridGL_ExchangeRatePopUp").data("kendoGrid");
        grid.dataSource.data([]);
        grid.dataSource.read();
    };




    $scope.GetNextManualPaymentNumber = function () {
        //uncomment below code to get autogenerated payment number

        //$http.get('/CMS_Payment/GetNextPaymentNumber?KaizenPublicKey=' + sessionStorage.PublicKey,
        //    {
        //        params: {
        //            PaymentMethodID: $scope.CM10307.PaymentMethodID,
        //            CheckbookCode: $scope.CM10307.CheckbookCode
        //        }
        //    })
        //    .success(function (data) {
        //        $scope.CM10307.PaymentNumber = data;
        //    }).finally(function () { });
    };

    $scope.LoadCM10307 = function (payment) {

        $http.get('/CMS/CMS_Payment/GetSingleManual?KaizenPublicKey=' + sessionStorage.PublicKey + "&PaymentID=" + payment.PaymentID).success(function (data) {
            $scope.CM10307 = data;
            $scope.CM10307.TRXTypeID = $scope.CM00203.TRXTypeID;
            $scope.CM10307.TransactionDate = kendoparsetoDate($scope.CM10307.TransactionDate);
            $scope.CM10307.CreatedDate = kendoparsetoDate($scope.CM10307.CreatedDate);
        }).finally(function () {
            $scope.CM10307.Status = 2;
        });
    };

    $scope.UpdateManualPayment = function () {

        $http.post('/CMS_Payment/UpdateManualDataObject', { 'CM10307': $scope.CM10307, 'KaizenPublicKey': sessionStorage.PublicKey })
            .success(function (data) {

                if (data.Status == true) {
                    $.bigBox({
                        title: "Success",
                        content: data.Massage,
                        color: "#739E73",
                        iconSmall: "fa fa-check fa-2x fadeInRight animated",
                        timeout: 4000
                    });
                    $scope.CM10307 = {};
                    $scope.Cancel();
                    $scope.GridRefresh('GridCMS_Case_ManualPayment');
                }
                else {
                    $.bigBox({
                        title: "Danger",
                        content: data.Massage,
                        color: "#C46A69",
                        iconSmall: "fa fa-bolt fa-2x fadeInRight animated",
                        timeout: 4000
                    });
                }
            }).error(function (data, status, headers, config) { alert(); });
    };
    $scope.SaveManualPayment = function () {

        $scope.CM10307.TRXTypeID = $scope.CM00203.TRXTypeID;
        //alert($scope.CM10307.TRXTypeID);
        var action = "SaveManualDataObject";
        if ($scope.CM10307.Status == 2)
            action = "UpdateManualDataObject";
        $http.post('/CMS_Payment/' + action, {
            'CM10307': $scope.CM10307,
            'KaizenPublicKey': sessionStorage.PublicKey
        }).success(function (data) {

            if (data.Status == true) {
                $.bigBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#739E73",
                    timeout: 3000,
                    icon: "fa fa-check shake animated"
                });
                $scope.CM10307 = {};
                $scope.EditCase($scope.CM00203.CaseRef);
            }
            else {

            }
        }).error(function (data, status, headers, config) { alert(data); });
    };
    $scope.DeleteManualPayment = function (CM10307) {
        $.SmartMessageBox({
            title: "Delete Record",
            content: "Are you sure?",
            buttons: '[No][Yes]'
        }, function (ButtonPressed) {
            if (ButtonPressed === "Yes") {
                $http.post('/CMS_Payment/DeleteManualDataObject', { 'PaymentID': CM10307.PaymentID, 'KaizenPublicKey': sessionStorage.PublicKey })
  .success(function (data) {
      if (data.Status == true) {

          $.bigBox({
              title: "Success",
              content: data.Massage,
              color: "#C46A69",
              iconSmall: "fa fa-check fa-2x fadeInRight animated",
              timeout: 4000
          });
          $scope.Cancel();
          $scope.GridRefresh('GridCMS_Case_ManualPayment');


      }
      else {
          $.bigBox({
              title: "Danger",
              content: data.Massage,
              color: "#C46A69",
              iconSmall: "fa fa-bolt fa-2x fadeInRight animated",
              timeout: 4000
          });
          //Notify(data.Massage, 'bottom-right', '5000', 'danger', 'fa-bolt', true);
      }
  }).error(function (data, status, headers, config) { alert(); });
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


    //------------------------------


    //-------------------------- Document Information
    $scope.CM00208 = {};
    $scope.CaseDocument = function () {
        removeEntityPage($scope.CasePages);
        $scope.LoadCasePage('CaseDocument');
        $scope.CM00208.Status = 1;
        $http.get('/Admin_DocumnetType/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey + '&ContactSourceID=6').success(function (data) {
            $scope.CaseDocumentTypes = data;
        }).finally(function () { });

    };

    $scope.UpdateCaseDocument = function () {
        $scope.CM00208.CaseRef = $scope.CM00203.CaseRef;
        $http.post('/CMS_Case/UpdateCaseDocument', { 'CM00208': $scope.CM00208, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
            if (data.Status == true) {
                $.bigBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#739E73",
                    timeout: 3000,
                    icon: "fa fa-check shake animated"
                });
                $scope.GridRefresh('GridCMS_CaseDocumentByCase');
                $scope.CM00208 = { Status: 1 };
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
    $scope.DeleteCaseDocument = function () {
        $.SmartMessageBox({
            title: "Delete Record",
            content: "Are you sure you want to delete this?",
            buttons: '[No][Yes]'
        }, function (ButtonPressed) {
            if (ButtonPressed === "Yes") {
                $http.post('/CMS_Case/DeleteCaseDocument', { 'CM00208': $scope.CM00208, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
                    if (data.Status == true) {
                        $.bigBox({
                            title: data.Massage,
                            content: data.Description,
                            color: "#739E73",
                            timeout: 3000,
                            icon: "fa fa-check shake animated"
                        });
                        $scope.GridRefresh('GridCMS_CaseDocumentByCase');
                        $scope.CM00208 = { Status: 1 };
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
                    timeout: 3000
                });
                $scope.CM00208 = { Status: 1 };
            }
        });
    };
    $scope.SetCaseFileExtension = function (PhotoPath) {
        $scope.CM00208.PhotoExtension = PhotoPath;
    };
    $scope.RemoveCaseFileExtension = function (PhotoPath) {
        $scope.CM00208.PhotoExtension = "";
    };
    $scope.CaseDocumentFilePath = function (CM00208) {
        if (angular.isUndefined(CM00208.PhotoExtension)) return "";
        var FilePath;
        if (CM00208.Status == 0)
            FilePath = "CaseDocuments";
        else
            FilePath = "CaseDocumentsTemp";
        return "\\Photo\\" + FilePath + "\\" + CM00208.PhotoExtension;
    };
    ///////////////////////////////// Case Assignment ///////////////////////////////////////////////////////////
    $scope.Filter = {};
    $scope.Assign = {};

    $scope.GridFilters = [];
    $scope.AssignedCases = [];
    $scope.AssignCaseToAgent = function () {
        var items = $("#GridCMS_CaseAssign").data("kendoGrid").dataSource.data();
        for (i = 0; i < items.length; i++) {
            var item = items[i];
            if (item.IsSeleceted) {
                $scope.AssignedCases.push(item);
            }
        }
        $http.post('/CMS_Case/AssignToAgent', { 'CM00203': $scope.AssignedCases, 'AgentID': $scope.Assign.AgentID, 'AssignDate': $scope.Assign.AssignHistoryDate, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
            if (data.Status == true) {
                Notify(data.Massage, 'bottom-right', '5000', 'success', 'fa-check', true);
                $scope.Cancel();
            }
            else {
                Notify(data.Massage, 'bottom-right', '5000', 'danger', 'fa-bolt', true);
            }
        }).error(function (data, status, headers, config) { alert(); });
    };
    $scope.openCaseAssignUploadWindow = function (str) {
        $scope.ActionString = str;
        switch ($scope.ActionString) {
            case 'CaseBulkUpload':
                $http.get('/IntegrationManager/GetKaizenTable?KaizenPublicKey=' + sessionStorage.PublicKey, { params: { ScreenID: 13 } }).success(function (data) {
                    $scope.KaizenColumn = data;
                }).finally(function () {
                    $scope.CaseAssignUploadWindow.center().toFront().open();
                    $scope.CaseAssignUploadWindow.refresh({
                        url: "/IntegrationManager/Index?KaizenPublicKey=" + sessionStorage.PublicKey
                    });
                });
                break;
            case 'CaseBulkDelete':
                $scope.KaizenColumn = [{ FieldName: "CaseRef" }];
                $scope.CaseAssignUploadWindow.center().toFront().open();
                $scope.CaseAssignUploadWindow.refresh({
                    url: "/IntegrationManager/Index?KaizenPublicKey=" + sessionStorage.PublicKey
                });
                break;
            case 'CaseBulkUpdate':
                $scope.KaizenColumn = [{ FieldName: "CaseRef" }];
                $scope.CaseAssignUploadWindow.center().toFront().open();
                $scope.CaseAssignUploadWindow.refresh({
                    url: "/IntegrationManager/Index?KaizenPublicKey=" + sessionStorage.PublicKey
                });
                break;
        }
    };
    $scope.UploadedData = function (Columns) {
        TableName = Columns.TableName;
        $scope.ExcelColumns = Columns.ExcelColumns;
    };
    $scope.SaveUploadedData = function () {
        if ($scope.ActionString == 'CaseBulkUpload') {
            $http({
                url: '/CMS_CaseAsgin/SaveUploadedData?KaizenPublicKey=' + sessionStorage.PublicKey,
                method: "POST",
                data: $.param({ KaizenColumn: $scope.KaizenColumn, Kaizen00003: $scope.Kaizen00003, TableName: TableName }),
                headers: {
                    'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8'
                }
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
                    $scope.CaseAssignUploadWindow.close();
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
        else if ($scope.ActionString == 'CaseBulkDelete') {
            $http({
                url: '/CMS_Case/SaveUploadedBulkDelete?KaizenPublicKey=' + sessionStorage.PublicKey,
                method: "POST",
                data: $.param({ KaizenColumn: $scope.KaizenColumn, TableName: TableName }),
                headers: {
                    'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8'
                }
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
                    $scope.CaseAssignUploadWindow.close();
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
    };
    $scope.UpdateUploadedData = function () {
        var value = '';
        if ($scope.Filter.SelectedColumn.type == 'string') {
            value = $scope.Filter.strvalue;
        }
        else if ($scope.Filter.SelectedColumn.type == 'number') {
            value = $scope.Filter.numvalue;
        }
        else if ($scope.Filter.SelectedColumn.type == 'date') {
            value = $scope.Filter.dtvalue;
        }
        else if ($scope.Filter.SelectedColumn.type == 'dropdown') {
            value = $scope.Filter.ddlvalue;
        }
        $http({
            url: '/CMS_Case/SaveUploadedBulkUpdate?KaizenPublicKey=' + sessionStorage.PublicKey,
            method: "POST",
            data: $.param({
                KaizenColumn: $scope.KaizenColumn,
                TableName: TableName,
                Field: $scope.Filter.SelectedColumn.field,
                Value: value,
                Type: $scope.Filter.SelectedColumn.type
            }),
            headers: {
                'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8'
            }
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
                $scope.CaseAssignUploadWindow.close();
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

    $scope.CaseBulkActions = function () {
        removeEntityPage($scope.CasePages);
        $scope.LoadCasePage('CaseBulkActions');
    };
    $scope.BulkDelete = function () {
        var items = $("#GridCMS_CaseBulkActions").data("kendoGrid").dataSource.data();
        for (i = 0; i < items.length; i++) {
            var item = items[i];
            if (item.IsSeleceted) {
                $scope.AssignedCases.push(item);
            }
        }
        $http.post('/CMS_Case/BulkDelete', { 'CM00203': $scope.AssignedCases, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
            if (data.Status == true) {
                $.bigBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#739E73",
                    timeout: 4000,
                    icon: "fa fa-check shake animated"
                });
                $scope.Cancel();
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
    $scope.BulkArchive = function () {
        var items = $("#GridCMS_CaseBulkActions").data("kendoGrid").dataSource.data();
        for (i = 0; i < items.length; i++) {
            var item = items[i];
            if (item.IsSeleceted) {
                $scope.AssignedCases.push(item);
            }
        }
        $http.post('/CMS_Case/BulkDelete', { 'CM00203': $scope.AssignedCases, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
            if (data.Status == true) {
                $.bigBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#739E73",
                    timeout: 4000,
                    icon: "fa fa-check shake animated"
                });
                $scope.Cancel();
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
    $scope.FilterBy = function () {
        var grid = $("#GridCMS_CaseBulkActions").data("kendoGrid");
        if ($scope.CM00203.ContractID == undefined || $scope.CM00203.ContractID == '')
            var filters = [
       { field: 'ClientID', operator: 'startswith', value: $scope.CM00203.ClientID },
            ];
        else
            var filters = [
            { field: 'ClientID', operator: 'startswith', value: $scope.CM00203.ClientID },
            { field: 'ContractID', operator: 'startswith', value: $scope.CM00203.ContractID }
            ];
        grid.dataSource.filter(filters);
    };

    ///////////////////////////////// Case Tasks /////////////////////////////////////////////////////
    $scope.CaseTasktoolbarOptions = {
        items: [
                {
                    type: "button", text: " Save & Close",
                    click: function (e) {
                        if ($scope.CM00213.Status == 0)
                            $scope.SaveCaseTask();
                        else
                            $scope.UpdateCaseTask();
                    }
                },
                    {
                        type: "splitButton",
                        spriteCssClass: "k-tool-icon k-i-excel",
                        text: "Excel",
                        click: function (e) {
                            $scope.SaveAsExcel("GridCMS_TaskHistory");
                        },
                        menuButtons: [
                            {
                                spriteCssClass: "k-tool-icon k-i-excel", text: "Excel",
                                click: function (e) {
                                    $scope.SaveAsExcel("GridCMS_TaskHistory");
                                }
                            },
                            {
                                spriteCssClass: "k-tool-icon k-i-pdf", text: "PDF",
                                click: function (e) {

                                }
                            }
                        ]
                    },
                 {
                     type: "button", text: "Cancel"
                        , spriteCssClass: "k-tool-icon k-cancel",
                     click: function (e) {
                         $scope.Cancel();
                     }
                 }
        ]
        , resizable: true
    };
    $scope.CM00213 = { Status: 0 };
    $scope.openCaseTaskWindow = function () {
        removeEntityPage($scope.CasePages);
        $scope.LoadCasePage('CaseTask');
        $scope.LoadCasePage('CaseView');
        $scope.CM00213.Status = 0;
        $http.get('/CMS_TaskType/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.TaskTypes = data;
            if ($scope.TaskTypes.length > 0) {
                $scope.CM00213.TaskTypeID = $scope.TaskTypes[0].TaskTypeID
            }
        }).finally(function () { });
        $http.get('/CMS_TaskPriority/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.TaskPriorities = data;
            if ($scope.TaskPriorities.length > 0) {
                $scope.CM00213.PriorityID = $scope.TaskPriorities[0].PriorityID
            }
        }).finally(function () { });

        $scope.CM00213.TaskStartDate = new Date();
        $scope.CM00213.TaskEndDate = new Date();
        $scope.CM00213.TaskProgress = 0;
        $scope.CM00213.Status = 0;
        $scope.CM00213.TaskTitle = "";
        $scope.CM00213.TaskDescription = "";
        if (angular.isDefined($scope.CM00203.CaseRef)) {
            $scope.CM00213.CaseRef = $scope.CM00203.CaseRef;
            $scope.CM00213.AgentID = $scope.CM00203.AgentID;
            //$scope.LoadCaseCMS_CasePayment($scope.CM00203.CaseRef);
        }
    };
    $scope.SaveCaseTask = function () {
        $http.post('/CMS_Case/SaveCaseTaskData', {
            'CM00213': $scope.CM00213,
            'KaizenPublicKey': sessionStorage.PublicKey
        }).success(function (data) {
            $scope.GridRefresh("GridCMS_TaskHistory");
            if (data.Status == true) {
                $.bigBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#739E73",
                    timeout: 4000,
                    icon: "fa fa-check shake animated"
                });
                $scope.CM00213.TaskStartDate = new Date();
                $scope.CM00213.TaskEndDate = new Date();
                $scope.CM00213.TaskProgress = 0;
                $scope.CM00213.Status = 0;
                $scope.CM00213.TaskTitle = "";
                $scope.CM00213.TaskDescription = "";
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
        $http.post('/CMS_Case/UpdateCaseTaskData', {
            'CM00213': $scope.CM00213,
            'KaizenPublicKey': sessionStorage.PublicKey
        }).success(function (data) {
            $scope.GridRefresh("GridCMS_TaskHistory");
            $scope.CM00203.Status = 0;
            if (data.Status == true) {
                $.bigBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#739E73",
                    timeout: 4000,
                    icon: "fa fa-check shake animated"
                });
                $scope.CM00213.TaskStartDate = new Date();
                $scope.CM00213.TaskEndDate = new Date();
                $scope.CM00213.TaskProgress = 0;
                $scope.CM00213.Status = 0;
                $scope.CM00213.TaskTitle = "";
                $scope.CM00213.TaskDescription = "";
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
    $scope.EditCaseTask = function (task) {
        $scope.CM00213 = task;
        if ($scope.CM00213.status == 0 || angular.isUndefined($scope.CM00213.status)) {
            $scope.CM00213.status = 2;
        }
    };

    $scope.CaseBack = function (caseObj) {
        if (caseObj != null) {
            switch ($scope.CurrentControl) {
                case 'Main':
                    $scope.CM00203 = caseObj;
                    $scope.SelectedLookUp.SelectedType = $scope.GetSingle($scope.TRXTypes, "TRXTypeID", $scope.CM00203.TRXTypeID);
                    $scope.CM00203.SelectedStatus = $scope.GetSingle($scope.CaseStatuses, "CaseStatusID", $scope.CM00203.CaseStatusID);
                    if ($scope.CM00203.SelectedStatus.IsHasChild) {
                        $http.get('/CMS_CaseStatus/GetStatusChilds?KaizenPublicKey=' + sessionStorage.PublicKey, { params: { CaseStatusParent: $scope.CM00203.SelectedStatus.CaseStatusID } }).success(function (data) {
                            if (data.length > 0) {
                                $scope.StatusChilds = data;
                                $scope.HasChild = true;
                            } else {
                                $scope.StatusChilds = [];
                                $scope.HasChild = false;
                            }
                        }).finally(function () {
                            $scope.CaseStatusChildOptions = {
                                filter: "startswith",
                                model: "SelectedChild",
                                optionLabel: "-- Select Status --",
                                dataTextField: "CaseStatusname",
                                dataValueField: "CaseStatusID",
                                dataSource: $scope.StatusChilds,
                                value: $scope.CM00203.CaseStatusChild
                            };
                            $scope.CM00203.SelectedChild = $scope.GetSingle($scope.StatusChilds, "CaseStatusID", $scope.CM00203.CaseStatusChild);
                        });
                    }
                    $scope.progress = ($scope.CM00203.TotalCallactedAMT / $scope.CM00203.ClaimAmount) * 100;
                    $scope.onProgressChange = function (e) {
                        var pr = e.sender;
                        pr.progressWrapper.css({
                            "background-image": "none",
                            "border-image": "none"
                        });

                        if (e.value >= 0 && e.value <= 50) {
                            pr.progressWrapper.css({
                                "background-color": "#EE9F05",
                                "border-color": "#EE9F05"
                            });
                        }
                        else if (e.value > 50 && e.value <= 75) {
                            pr.progressWrapper.css({
                                "background-color": "#428bca",
                                "border-color": "#428bca"
                            });
                        }
                        else {
                            pr.progressWrapper.css({
                                "background-color": "#8EBC00",
                                "border-color": "#8EBC00"
                            });
                        }
                    };
                    $scope.CaseCreat_toolbarOptions = {
                        items: [
                                {
                                    type: "button", text: " Save",
                                    click: function (e) {
                                        if ($scope.CM00203.Status == 1)
                                            $scope.SaveCase();
                                        else
                                            $scope.UpdateCase();
                                    }
                                },
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
                                        { spriteCssClass: "k-icon k-insertImage", text: "Send Email", click: function (e) { $scope.openComposeEmailWindow(); } },
                                        { spriteCssClass: "k-icon k-insertImage", text: "Send Email", click: function (e) { $scope.OpenTemplateEmailWindow(); } }
                                    ]
                                },
                                {
                                    type: "splitButton",
                                    text: "Utility",
                                    click: function (e) {
                                        // $scope.ExportScreen('CMS_Case_Create', 'PDF', "Case" + $scope.CM00203.CaseRef.trim());
                                    },
                                    menuButtons: [
                                        {
                                            text: "Archive", click: function (e) {
                                                alert('Archive not configureed')
                                            }
                                        },
                                        {
                                            text: "Reconcile", click: function (e) {
                                                alert('Reconcile not configureed')
                                            }
                                        }
                                    ]
                                },
                                {
                                    type: "button", text: "Cancel"
                                        , spriteCssClass: "k-tool-icon",
                                    click: function (e) {
                                        $scope.$apply(function () {
                                            $scope.Cancel();
                                        });
                                    }
                                },
                                {
                                    type: "button", text: "Task",
                                    click: function (e) {
                                        $scope.openCaseTaskWindow();
                                    }
                                },
                                {
                                    type: "button", text: "Assign",
                                    click: function (e) {
                                        $scope.openCaseAssignHistoryWindow();
                                    }
                                },
                                {
                                    type: "button", text: "<strong><span class='bold text-success'>Balance =" + $scope.CM00203.Balance == undefined ? 0 : $scope.CM00203.Balance.toFixed($scope.CM00203.DecimalDigit) + " " + $scope.CM00203.CurrencyCode + "</span></strong>",
                                    click: function (e) {
                                        return;
                                    }
                                },
                                {
                                    type: "button", text: "Actions",
                                    click: function (e) {
                                        $scope.GoBack('CaseAction');

                                    }
                                },
                                {
                                    type: "button", text: "History", hidden: $scope.CM00203.IsHasHistory,
                                    click: function (e) {
                                        $scope.openCaseHistoryWindow();
                                    }
                                },
                                {
                                    type: "button", text: "Clear",
                                    click: function (e) {
                                        $scope.$apply(function () {
                                            $scope.Clear();
                                        });
                                    }
                                },
                                {
                                    type: "button", text: "Delete",
                                    click: function (e) {
                                        $scope.Delete();
                                    }
                                },
                                {
                                    template: "<div kendo-progress-bar='progressBar' k-min='0' k-max='100'" +
                                    "ng-model='progress' style='width: 100px;' k-on-change='onProgressChange(kendoEvent)'></div>"
                                , attributes: { style: "float: right" }
                                },
                                { template: "<label>Progress</label>", attributes: { style: "float: right" } },
                        ]
                        , resizable: true
                    };
                    $scope.CM00203.Status = 2;
                    $scope.OrginalCM00203 = angular.copy($scope.CM00203);
                    break;
                case 'CasePayment':
                    $scope.CM00203 = caseObj;
                    $scope.CM10207.CaseRef = caseObj.CaseRef
                    $scope.GridRefresh("GridCMS_Case_CasePayment");
                    break;
                case 'CaseDocument':
                    $scope.CM00203 = caseObj;
                    $scope.CM00208.CaseRef = caseObj.CaseRef
                    $scope.GridRefresh("GridCMS_CaseDocumentByCase");
                    break;
            }
        }
    };

    $scope.UploadCaseView = function () {
        removeEntityPage($scope.CasePages);
        $scope.LoadCasePage('UploadView');
    };
    //----------------------------------------------------------
    $scope.ArchiveCase = function () {
        $http.post('/CMS_Case/ArchiveData', { 'CM00203': $scope.CM00203, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
            if (data.Status == true) {
                $.bigBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#739E73",
                    timeout: 3000,
                    icon: "fa fa-check shake animated"
                });
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
    $scope.HoldCase = function () {
        alert('Not COnfigured contact System Admin')
    };
    //-----------------------------------------------------------------------------------------------------------------
    $scope.PagePath = [];
    $scope.GoTo = function (PagePath) {
        if (PagePath == null || PagePath == undefined || angular.isUndefined(PagePath))
            PagePath = $scope.ToolBar.ActiveScreen.CurrentWindow;
        //alert(PagePath);
        $scope.ToolBar.ActiveScreen.CurrentWindow = PagePath;
        switch (PagePath) {
            case "MainGrid":
                $scope.LoadMainView();
                break;
            case "CasePayment":
                $scope.LoadPage(PagePath);
                $scope.CasePayment();
                break;
            case "ManualPayment":
                $scope.LoadPage(PagePath);
                $scope.ManualPayment();
                break;
            case "ClientCases":
                $scope.LoadPage(PagePath);
                $http.get('/CMS_ClientClass/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
                    $scope.clientClasses = data;
                }).finally(function () { });
                break;
            case "CreateNew":
                $scope.LoadPage(PagePath);
                $scope.AddCase();
                break;

            case "ActionManamgment":
                $scope.LoadPage(PagePath);
                $scope.OpenActionManamgmentWindows();
                break;
            case "ActionManamgmentSummery":
                $scope.LoadPage(PagePath);
                break;
            case "ActionManamgmentSummeryAdvance":
                $scope.LoadPage(PagePath);
                $scope.ActionManamgmentMainGridURLReport = null;
                $scope.GetGL00002List();
                break;
            case "Create":
                $scope.LoadPage(PagePath);
                break;
            case "CaseAction":
                $scope.LoadPage(PagePath);
                $scope.openCaseActionWindow();
                break;
            case "CaseActionHistory":
                $scope.LoadPage(PagePath);
                $scope.openCaseActionWindow();
                break;
            case "Reminder":
                //$scope.LoadPage(PagePath);
                $scope.LoadReminderData();
                break;
            case "TasksIndex":
                $scope.LoadPage(PagePath);
                $scope.LoadTasksData();
                break;
            case "TaskView":
                $scope.LoadPage("TaskView");
                break;
            case "CaseDocuments":
                $scope.LoadPage(PagePath);
                $scope.openCaseDocumentWindow();
                break;
            case "PivotGrid":
                $scope.LoadPage(PagePath);
                break;
            case "DebtorIndex":
                $scope.LoadPage(PagePath);
                $scope.LoadDebtorPage();
                break;
        }
        //if (PagePath != "MainGrid" && PagePath != "Reminder")
        $scope.PagePath.push(PagePath);
        //alert($scope.PagePath.length);
    }
    $scope.LoadPage = function (pageName) {
        $scope.MainURL = '';
        $scope.MainURL = "/CMS/CMS_Case/" + pageName + "?KaizenPublicKey=" + sessionStorage.PublicKey;
        //alert($scope.MainURL);
    };
    $scope.Cancel = function () {
        $scope.GoTo($scope.ToolBar.ActiveScreen.LastWindow);
    };
    $scope.GoBack = function () {
        if ($scope.CM10307) {
            $scope.CM10307 = {};
        }
        //alert(JSON.stringify($scope.PagePath, null, 2));
        $scope.PagePath.splice($scope.PagePath.length - 1, 1);
        //alert($scope.PagePath.length);
        if ($scope.PagePath.length > 0) {
            var temp = $scope.PagePath[$scope.PagePath.length - 1];

            if (temp == "MainGrid" || temp == "Reminder") {
                $scope.ToolBar.ActiveScreen.CurrentWindow = temp;
                $scope.MainURL = '';
                return;
            }
            $scope.PagePath.splice($scope.PagePath.length - 1, 1);
            $scope.GoTo(temp);
        }
        else
            $scope.MainURL = '';
    };
    // Main Grid -----------------------------------
    $scope.SelectedLookUp.SelectedStatus = {};
    $scope.ViewList = [];
    $scope.LoadMainView = function () {
        if ($scope.CM00203TRXTPE.length == 0) {
            alert('Missing User Setup');
            return;
        }
        $scope.SelectedLookUp.SelectedType = $scope.CM00203TRXTPE[0];
        $scope.SelectedLookUp.IsGrap = true;
        $scope.CMS_CaseMaintoolbarOptions = {
            items: [
                     {
                         type: "button", text: "<span class=\"fa fa-plus-circle\"></span> New"
                         , click: function (e) {
                             $scope.$apply(function () {
                                 $scope.GoTo("CreateNew");
                             });
                         }
                     },
                       {
                           type: "splitButton",
                           spriteCssClass: "k-tool-icon k-i-excel",
                           text: "Export",
                           click: function (e) {
                               $scope.SaveAsExcel("GridCMS_Case");
                           },
                           menuButtons: [
                               { spriteCssClass: "k-tool-icon k-i-excel", text: "Excel", click: function (e) { $scope.SaveAsExcel('GridCMS_Case'); } },
                               { spriteCssClass: "k-tool-icon k-i-pdf", text: "PDF", click: function (e) { $scope.SaveAsPdf('GridCMS_Case'); } },
                               {
                                   spriteCssClass: "k-tool-icon k-i-pdf", text: "Print",
                                   click: function (e) {
                                       //$scope.OpenkendoWindow('Sys_View', 'DynamicReport', $scope.SelectedLookUp.SelectedView.ViewID);
                                       //wael wael
                                       alert($scope.GetGridFilters("GridCMS_Case"));
                                       return;
                                   }
                               }
                           ]
                       },
                         //{
                         //    type: "splitButton",
                         //    text: "RMS",
                         //    click: function (e) {
                         //        $scope.$apply(function () {
                         //            $scope.GoTo("Reminder");
                         //        });
                         //    },
                         //    menuButtons: [
                         //         {
                         //             text: "Workable List", click: function (e) {
                         //                 $scope.$apply(function () {
                         //                     $scope.GoTo("ActionManamgment");
                         //                 });
                         //             }
                         //         }
                         //    ]
                         //},
                       {
                           type: "splitButton",
                           text: "Utility",
                           click: function (e) {
                               // $scope.ExportScreen('CMS_Case_Create', 'PDF', "Case" + $scope.CM00203.CaseRef.trim());
                           },
                           menuButtons: [
                               {
                                   text: "Delete",
                                   click: function (e) {
                                       $scope.$apply(function () {
                                           $scope.CaseBulkActions();
                                       });
                                   }
                               },
                               {
                                   text: "Update", click: function (e) {
                                       $scope.$apply(function () {
                                           $scope.KaizenColumn = [{ FieldName: "CaseRef" }];
                                           $scope.CaseBulkUploadActionWindow.center().toFront().open();
                                           $scope.Filter = { field: -1, operator: '', value: '' };
                                           var grid = $("#GridCMS_Case").data("kendoGrid");
                                           $scope.Filter.SelectedColumn = {};
                                           $scope.Columns = grid.columns;
                                           var dataSource = grid.dataSource;
                                           // Removing CHeck BOx Collumns 
                                           if ($scope.GetSingleIndex($scope.Columns, "title", "Select") != null) {
                                               var removedcolumn = $scope.functiontofindIndexByKeyValue($scope.Columns, "title", "Select");
                                               $scope.Columns.splice(removedcolumn, 1);
                                           }
                                           $scope.Columns.forEach(function (element, index) {
                                               if (angular.isDefined(element.field)) {
                                                   if (angular.isDefined(element.filterable)) {
                                                       element.type = "dropdown";
                                                   } else {
                                                       element.type = getFieldType(dataSource, element.field);
                                                   }
                                               }
                                           })
                                       });
                                   }
                               },
                               {
                                   text: "Archive", click: function (e) {
                                       alert('Archive not configureed')
                                   }
                               },
                               {
                                   text: "Reconcile", click: function (e) {
                                       alert('Reconcile not configureed')
                                   }
                               }
                           ]
                       },
                       {
                           type: "splitButton",
                           text: "Summary",
                           click: function (e) {
                               $scope.$apply(function () {
                                   $scope.ClientCases();
                               });
                           },
                           menuButtons: [
                               {
                                   text: "Portfolio Grading",
                                   click: function (e) {
                                       $scope.$apply(function () {
                                           $scope.GoTo("ClientCases");
                                       });
                                   }
                               },
                                {
                                    text: "Case Cycle",
                                    click: function (e) {
                                        $scope.$apply(function () {
                                            $scope.GoTo("ClientCases");
                                        });
                                    }
                                },
                                {
                                    text: "Case Collection",
                                    click: function (e) {
                                        $scope.$apply(function () {
                                            $scope.GoTo("ClientCases");
                                        });
                                    }
                                },
                               {
                                   text: "Agent",
                                   click: function (e) {
                                       $scope.$apply(function () {
                                           $scope.AgentCases();
                                       });
                                   }
                               },
                           {
                               text: "Pivot",
                               click: function (e) {
                                   $scope.$apply(function () {
                                       $scope.GoTo("PivotGrid");
                                   });
                               }
                           }
                           ]
                       },
                       {
                           type: "button", text: "<span class=\"fa fa-file\"></span>", click: function (e) {
                               $scope.$apply(function () {
                                   $scope.CaseDocument();
                               });
                           }
                       },
                       {
                           type: "button", text: " ", spriteCssClass: "k-tool-icon k-filter",
                           click: function (e) {
                               $scope.OpenFilterWindow("GridCMS_Case", "CMS_Case");
                           }
                       },
                       { type: "separator" },
                       { template: "<label>View:</label>" },
                       {
                           template: "<kendo-button ng-click='ShowDataGraph()' ng-show='SelectedLookUp.SelectedView.IsPivotTable && SelectedLookUp.IsGrap'>G</kendo-button><kendo-button ng-click='ShowDataGraph()' ng-show='SelectedLookUp.SelectedView.IsPivotTable && !SelectedLookUp.IsGrap'>T</kendo-button>",
                           overflow: "never"
                       },
                       {
                           template: "<select kendo-drop-down-list k-ng-model='SelectedLookUp.SelectedView' k-on-change='ViewChanged()' k-options='CaseViewformatOptions' k-rebind='CaseViewformatOptions' style='width: 200px;'></select>",
                           overflow: "never"
                       },
                       {
                           template: "<select kendo-drop-down-list k-ng-model='SelectedLookUp.SelectedType' k-on-change='CaseTRXTypeID_Changed()' k-options='CaseTypeOptions' style='width: 150px;'></select>",
                           overflow: "never"
                       },
                    {
                        type: "splitButton",
                        text: "Cases",
                        click: function (e) {
                            // $scope.ExportScreen('CMS_Case_Create', 'PDF', "Case" + $scope.CM00203.CaseRef.trim());
                        },
                        menuButtons: [
                            {
                                text: "Asgined",
                                click: function (e) {
                                    $scope.$apply(function () {
                                        $scope.ToolBar.ActiveScreen.CurrentWindow = "CurrentWindow";
                                        $scope.ViewChanged();
                                    });
                                }
                            },
                            {
                                text: "Un-Asgined"
                                , click: function (e) {
                                    $scope.$apply(function () {
                                        $scope.ToolBar.ActiveScreen.CurrentWindow = "UnAssignedCases";
                                        $scope.ViewChanged();
                                    });
                                }
                            },
                             {
                                 text: "Legal"
                                , click: function (e) {
                                    $scope.$apply(function () {
                                        $scope.ToolBar.ActiveScreen.CurrentWindow = "UnAssignedCases";
                                        $scope.ViewChanged();
                                    });
                                }
                             },

                             {
                                 text: "Partner"
                                , click: function (e) {
                                    $scope.$apply(function () {
                                        $scope.ToolBar.ActiveScreen.CurrentWindow = "UnAssignedCases";
                                        $scope.ViewChanged();
                                    });
                                }
                             },
                              {
                                  text: "Regularized"
                                , click: function (e) {
                                    $scope.$apply(function () {

                                    });
                                }
                              },
                               {
                                   text: "Newly Rolled"
                                , click: function (e) {
                                    $scope.$apply(function () {

                                    });
                                }
                               },
                               {
                                   text: "Tread milling"
                                , click: function (e) {
                                    $scope.$apply(function () {

                                    });
                                }
                               },
                               {
                                   text: "Backward Roll"
                                , click: function (e) {
                                    $scope.$apply(function () {

                                    });
                                }
                               },
                               {
                                   text: "Forward Roll"
                                , click: function (e) {
                                    $scope.$apply(function () {

                                    });
                                }
                               }
                        ]
                    },
                           {
                               template: "<span class='k-textbox k-space-right' style='width:150px;'><input type='text' ng-model='SelectedLookUp.Searchcritaria'/>"
                                      + "<a href='#' class='k-icon k-i-search'>&nbsp;</a></span>",
                               overflow: "never"
                           },
                    //{
                    //    type: "button", text: "Customers"
                    //     , click: function (e) {
                    //         $scope.$apply(function () {
                    //             $scope.GoTo("DebtorIndex");
                    //         });
                    //     }
                    //}
            ]
                 , resizable: true
        };
        $scope.ToolBar.ActiveScreen.CurrentWindow = "MainGrid";
        $scope.CaseTRXTypeID_Changed();
    }
    $scope.CaseTRXTypeID_Changed = function () {
        $scope.SelectedLookUp.SelectedView = null;
        $scope.MainGridURL = null;
        $scope.MainGridURL = '';
        $http.get('/CMS_Case/GetMyViewsByTRXTypeID?TRXTypeID=' + $scope.SelectedLookUp.SelectedType.TRXTypeID + '&KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.ViewList = data;
        }).finally(function () {
            if ($scope.ViewList.length > 0) {
                $scope.SelectedLookUp.SelectedView = $scope.ViewList[0];
                $scope.ViewList.forEach(function (element, index) {
                    if (element.IsDefault) {
                        $scope.SelectedLookUp.SelectedView = element;
                        return;
                    }
                });

                $scope.ViewChanged();
                //$http.get('/CMS_CaseType/GetProductsByCaseType?TRXTypeID=' + $scope.SelectedLookUp.SelectedType.TRXTypeID + '&KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
                //    $scope.CM00055List = data;
                //}).finally(function () { });
            } else {
                alert('Missing User Setup')
                $scope.MainGridURL = null;
                $scope.MainGridURL = '';
                $scope.SelectedLookUp.SelectedView = null;
                $scope.ViewList = [];
            }
            $scope.CaseViewformatOptions = {
                filter: "contains",
                //valuePrimitive: true,
                optionLabel: "-- ALL View --",
                dataTextField: "ViewName",
                dataValueField: "ViewID",
                dataSource: $scope.ViewList,
                //value: $scope.MainFilter.PERIODID
            };
        });
    };
    $scope.ShowDataGraph = function () {
        $scope.MainGridURL = null;
        $scope.MainGridURL = '';
        //alert($scope.SelectedLookUp.IsGrap);
        if ($scope.SelectedLookUp.IsGrap) {
            $scope.MainGridURL = "/CMS/CMS_Case/CaseGraph?KaizenPublicKey=" + sessionStorage.PublicKey;
            $scope.LoadGraphs_ByCaseTypeView();
        } else {
            $scope.ViewChanged();
        }
        $scope.SelectedLookUp.IsGrap = !$scope.SelectedLookUp.IsGrap;
    };
    $scope.ViewChanged = function () {
        //alert($scope.SelectedLookUp.SelectedView)
        //alert($scope.SelectedLookUp.SelectedView.IsPivotTable)
        if ($scope.SelectedLookUp.SelectedView == undefined || $scope.SelectedLookUp.SelectedView.ViewID == undefined) {
            alert('Missing User Setup');
            return;
        }
        $scope.MainGridURL = null;
        $scope.MainGridURL = '';
        if ($scope.SelectedLookUp.SelectedView.IsPivotTable && $scope.SelectedLookUp.IsGrap) {
            $scope.ShowDataGraph();
        } else {
            $scope.MainGridURL = "/CMS/CMS_Case/MainGrid?ViewID=" + $scope.SelectedLookUp.SelectedView.ViewID + "&KaizenPublicKey=" + sessionStorage.PublicKey;
        }
    };
    $scope.LoadGraphs_ByCaseTypeView = function () {
        $http.get('/CMS_CaseType/GetGraphs?KaizenPublicKey=' + sessionStorage.PublicKey,
                { params: { ViewID: $scope.SelectedLookUp.SelectedView.ViewID } }).success(function (data) {
                    if (data.length > 0) {
                        $scope.Graphs = data;
                    }
                }).error(function (data, status, headers, config) { });
    };
    $scope.LoadCM00203 = function (CaseRef) {
        //alert(CaseRef)
        $http.get('/CMS_Case/GetSingle?KaizenPublicKey=' + sessionStorage.PublicKey
            + "&CaseRef=" + CaseRef).success(function (data) {
                $scope.CM00203 = data;

                //$scope.SelectedLookUp.SelectedType = $scope.GetSingle($scope.TRXTypes, "TRXTypeID", $scope.CM00203.TRXTypeID);
                //$scope.SelectedLookUp.SelectedLookup01 = $scope.GetSingle($scope.Lookup01List, "Lookup01", $scope.CM00203.Lookup01);
                //$scope.SelectedLookUp.SelectedLookup02 = $scope.GetSingle($scope.Lookup02List, "Lookup02", $scope.CM00203.Lookup02);
                if ($scope.CM00203.ReminderDateTime != null && $scope.CM00203.ReminderDateTime != '')
                    $scope.CM00203.ReminderDateTime = kendoparsetoDate($scope.CM00203.ReminderDateTime);
                if ($scope.CM00203.FirstDisburementDate != null && $scope.CM00203.FirstDisburementDate != '')
                    $scope.CM00203.FirstDisburementDate = kendoparsetoDate($scope.CM00203.FirstDisburementDate);
                if ($scope.CM00203.MATURITY_DATE != null && $scope.CM00203.MATURITY_DATE != '')
                    $scope.CM00203.MATURITY_DATE = kendoparsetoDate($scope.CM00203.MATURITY_DATE);
                if ($scope.CM00203.TransactionDate != null && $scope.CM00203.TransactionDate != '')
                    $scope.CM00203.TransactionDate = kendoparsetoDate($scope.CM00203.TransactionDate);
                if ($scope.CM00203.OverDueDate != null && $scope.CM00203.OverDueDate != '')
                    $scope.CM00203.OverDueDate = kendoparsetoDate($scope.CM00203.OverDueDate);
                if ($scope.CM00203.LastPaymentDateUplod != null && $scope.CM00203.LastPaymentDateUplod != '')
                    $scope.CM00203.LastPaymentDateUplod = kendoparsetoDate($scope.CM00203.LastPaymentDateUplod);
                if ($scope.CM00203.ClosingDate != null && $scope.CM00203.ClosingDate != '')
                    $scope.CM00203.ClosingDate = kendoparsetoDate($scope.CM00203.ClosingDate);
                if ($scope.CM00203.FirstLifeOverDueDate != null && $scope.CM00203.FirstLifeOverDueDate != '')
                    $scope.CM00203.FirstLifeOverDueDate = kendoparsetoDate($scope.CM00203.FirstLifeOverDueDate);
                if ($scope.CM00203.LastPaymentDate != null && $scope.CM00203.LastPaymentDate != '')
                    $scope.CM00203.LastPaymentDate = kendoparsetoDate($scope.CM00203.LastPaymentDate);
                if ($scope.CM00203.BookingDate != null && $scope.CM00203.BookingDate != '')
                    $scope.CM00203.BookingDate = kendoparsetoDate($scope.CM00203.BookingDate);
                if ($scope.CM00203.AssignDate != null && $scope.CM00203.AssignDate != '')
                    $scope.CM00203.AssignDate = kendoparsetoDate($scope.CM00203.AssignDate);
                //alert($scope.CM00203.AddressCode);
                if ($scope.CM00203.AddressCode != null && $scope.CM00203.AddressCode != '') {
                    var AddressCode_indx = $scope.functiontofindIndexByKeyValue($scope.CM00009List, "AddressCode", $scope.CM00203.AddressCode);
                    $scope.SelectedLookUp.AddressTypes = $scope.CM00009List[AddressCode_indx];
                    $scope.DebtorAddressChanged();
                } else
                    $scope.SelectedLookUp.AddressTypes = null;
                //alert($scope.CM00203.PriorityID);
                //if ($scope.CM00203.PriorityID != null && $scope.CM00203.PriorityID != '') {
                //    var PriorityID_indx = $scope.functiontofindIndexByKeyValue($scope.CM00009List, "AddressCode", $scope.CM00203.AddressCode);
                //    $scope.SelectedLookUp.AddressTypes = $scope.CM00009List[AddressCode_indx];
                //    $scope.DebtorAddressChanged();
                //} else
                //    $scope.SelectedLookUp.AddressTypes = null;

            }).finally(function () {
                $scope.CM00203.Status = 2;
                $scope.SelectedLookUp.SelectedConidtionStatus = {};
                $scope.SelectedLookUp.SelectedStatus = {};
                $scope.SelectedLookUp.SelectedStatus = $scope.GetSingle($scope.CM00700List, "CaseStatusID", $scope.CM00203.CaseStatusID);
                $scope.SelectedLookUp.SelectedConidtionStatus = $scope.GetSingle($scope.CM00700List, "CaseStatusID", $scope.CM00203.LastCaseStatusID);
                $scope.StatusChilds = [];
                $scope.StatusConditionChilds = [];

                for (var T = 0; T < $scope.CM00700List.length; T++) {
                    if ($scope.CM00700List[T].CaseStatusParent == $scope.CM00203.CaseStatusID) {
                        $scope.StatusChilds.push($scope.CM00700List[T]);
                    }
                    if ($scope.CM00700List[T].CaseStatusParent == $scope.CM00203.LastCaseStatusID) {
                        $scope.StatusConditionChilds.push($scope.CM00700List[T]);
                    }
                }
                $scope.CaseStatusChildOptions = {
                    filter: "startswith",
                    //model: "SelectedChild",
                    optionLabel: "-- Select Status --",
                    dataTextField: "CaseStatusname",
                    dataValueField: "CaseStatusID",
                    dataSource: $scope.StatusChilds,
                    value: $scope.CM00203.CaseStatusChild
                };
                $scope.CaseConditionChildOptions = {
                    filter: "startswith",
                    //model: "SelectedChild",
                    optionLabel: "-- Select Status --",
                    dataTextField: "CaseStatusname",
                    dataValueField: "CaseStatusID",
                    dataSource: $scope.StatusConditionChilds,
                    value: $scope.CM00203.LastCasStatusChldID
                };
                //alert($scope.CM00203.LastCasStatusChldID)
                $scope.SelectedLookUp.SelectedChild = $scope.GetSingle($scope.CM00700List, "CaseStatusID", $scope.CM00203.CaseStatusChild);
                $scope.SelectedLookUp.SelectedChildCondition = $scope.GetSingle($scope.CM00700List, "CaseStatusID", $scope.CM00203.LastCasStatusChldID);
                //alert($scope.StatusChilds.length);
                //alert($scope.StatusConditionChilds.length);
                // $scope.OrginalCM00203 = angular.copy($scope.CM00203);

            });
    };
    $scope.AddCase = function () {
        $scope.CM00203 = {};
        $scope.CM00203.Status = 1;

        $scope.CM00203.CurrencyCode = $scope.MY.CurrencyCode;
        $scope.CM00203.DecimalDigit = $scope.MY.DecimalDigit;
        $scope.CM00203.ExchangeTableID = $scope.MY.ExchangeTableID;
        $scope.CM00203.IsMultiply = $scope.MY.IsMultiply;
        $scope.CM00203.ExchangeRate = $scope.MY.ExchangeRate;
        $scope.CM00203.BookingDate = new Date();
        //$scope.SelectedLookUp.SelectedType = null;
    };
    $scope.TypeChanged = function () {

        $scope.CM00203.TRXTypeID = $scope.SelectedLookUp.SelectedType.TRXTypeID;
        //$scope.CM00203.TrxTypeName = $scope.CM00203.SelectedType.TrxTypeName;
        $scope.SelectedLookUp.SelectedType = $scope.GetSingle($scope.TRXTypes, "TRXTypeID", $scope.CM00203.TRXTypeID);
        //alert(JSON.stringify($scope.CM00203, null, 2));
        //$http.get('/CMS_Case/GetNextNumber?KaizenPublicKey=' + sessionStorage.PublicKey, { params: { TRXTypeID: $scope.CM00203.TRXTypeID } })
        //.success(function (data) {
        //    $scope.CM00203.CaseRef = data;
        //})
        //.finally(function () {
        //});
    };
    //$scope.OnPriorityID_Changed = function () {
    //    $scope.CM00203.PriorityID = $scope.SelectedLookUp.SelectedPriority.PriorityID;
    //    $scope.CM00203.PriorityName = $scope.SelectedLookUp.SelectedPriority.PriorityName;
    //};
    $scope.SaveCase = function () {
        //alert($scope.CM00203.ProductID);
        //if (angular.isDefined($scope.SelectedLookUp.SelectedCM00055) && $scope.SelectedLookUp.SelectedCM00055 != undefined && $scope.CM00203.AssignDate != null && $scope.CM00203.AssignDate != '') {
        //    $scope.CM00203.ProductID = $scope.SelectedLookUp.SelectedCM00055.ProductID;
        //}
        if (angular.isDefined($scope.CM00203.AssignDate) && $scope.CM00203.AssignDate != undefined && $scope.CM00203.AssignDate != null && $scope.CM00203.AssignDate != '') {
            $scope.CM00203.AssignDate = kendoparsetoDate($scope.CM00203.AssignDate);
        }
        $scope.CM00203.TRXTypeID = $scope.SelectedLookUp.SelectedType.TRXTypeID;

        if ($scope.CM00203.ClosingDate != null && $scope.CM00203.ClosingDate != '')
            $scope.CM00203.ClosingDate = kendoparsetoDate($scope.CM00203.ClosingDate);
        if ($scope.CM00203.TransactionDate != null && $scope.CM00203.TransactionDate != '')
            $scope.CM00203.TransactionDate = kendoparsetoDate($scope.CM00203.TransactionDate);
        if ($scope.CM00203.BookingDate != null && $scope.CM00203.BookingDate != '')
            $scope.CM00203.BookingDate = kendoparsetoDate($scope.CM00203.BookingDate);
        //alert($scope.CM00203.CaseStatusID);
        //alert(JSON.stringify($scope.CM00203, null, 2));
        //alert($scope.SelectedLookUp.AddressTypes);

        if (angular.isDefined($scope.SelectedLookUp.AddressTypes) && $scope.SelectedLookUp.AddressTypes != undefined && $scope.SelectedLookUp.AddressTypes != null && $scope.SelectedLookUp.AddressTypes != '') {
            $scope.CM00203.AddressCode = $scope.SelectedLookUp.AddressTypes.AddressCode;
            //alert('ssssssssssss');
        }
        var SaveActionPath = 'SaveData';
        if ($scope.CM00203.Status == 2)
            SaveActionPath = 'UpdateData';
        $http.post('/CMS_Case/' + SaveActionPath, { 'CM00203': $scope.CM00203, 'KaizenPublicKey': sessionStorage.PublicKey })
            .success(function (data) {
                if (data.Status == true) {
                    $.bigBox({
                        title: data.Massage,
                        content: data.Description,
                        color: "#739E73",
                        timeout: 4000,
                        icon: "fa fa-check shake animated"
                    });
                    //$scope.GridRefresh('GridCMS_Case');
                    $scope.GoBack();
                    $scope.CM00203.Status = 2;
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
    $scope.EditCase = function (CaseRef) {
        $scope.Clear();
        $scope.GoTo("Create");
        $scope.LoadCM00203(CaseRef);
        $scope.CM00203.Status = 2;
    };
    $scope.CM00104 = {};
    $scope.DebtorAddressChanged = function () {
        $scope.CM00104 = {};
        $http.get('/Co_Debtor/GetDebtorAddressSingle?KaizenPublicKey=' + sessionStorage.PublicKey
            + "&DebtorID=" + $scope.CM00203.CIFNumber
            + "&AddressCode=" + $scope.SelectedLookUp.AddressTypes.AddressCode)
            .success(function (data) {
                //alert(data)
                if (angular.isUndefined(data) || data == null || data == '') {

                    $scope.CM00104.ISEdit = false;
                } else {
                    $scope.CM00104 = data;
                    $scope.CM00104.ISEdit = true;
                }
            }).finally(function () { });
    };
    $scope.SaveDebtorAddress = function () {
        $scope.CM00104.DebtorID = $scope.CM00203.CIFNumber;
        $scope.CM00104.AddressCode = $scope.SelectedLookUp.AddressTypes.AddressCode;
        var actionMethd = '';
        if ($scope.CM00104.ISEdit)
            actionMethd = 'UpdateDebtorAddress';
        else
            actionMethd = 'SaveDebtorAddress';
        $http.post('/Co_Debtor/' + actionMethd, { 'oCM00104': $scope.CM00104, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
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

    $scope.Contact = {};
    $scope.OpenDebtorContactDetails = function (DebtorID, ContactTypeID) {
        $http.get('/Co_Debtor/GetDebtorContactSingle?KaizenPublicKey=' + sessionStorage.PublicKey + "&DebtorID=" + DebtorID.trim() + "&ContactTypeID=" + ContactTypeID)
            .success(function (data) {
                if (data != '' && data.ContactTypeID != '' && data.ContactTypeID != undefined) {
                    $scope.Contact = data;
                    $scope.Contact.ContactTypeID = ContactTypeID;
                    $scope.Contact.Status = 2;
                }
                else {
                    $scope.Contact = { Status: 1, ContactTypeID: ContactTypeID };
                }
            }).finally(function () { });
    };
    $scope.SaveDebtorContact = function () {
        $scope.Contact.DebtorID = $scope.CM00203.CIFNumber
        var actionTpe = '';
        if ($scope.Contact.Status == 1)
            actionTpe = 'SaveDebtorContact';
        else
            actionTpe = 'UpdateDebtorContact';
        $http.post('/Co_Debtor/' + actionTpe, { 'CM00106': $scope.Contact, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
            if (data.Status == true) {
                $.bigBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#739E73",
                    timeout: 4000,
                    icon: "fa fa-check shake animated"
                });
                $scope.Contact = { Status: 1 };
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


    $scope.ToggleCaseDetails = function (obj) {
        //alert(obj)
        if ($('#K' + obj).hasClass('fa-plus'))
            $('#K' + obj).toggleClass('fa-minus');//.toggleClass('fa-minus');
        else
            $('#K' + obj).toggleClass('fa-plus');

        $('#' + obj).toggle('show');
    }


    $scope.OpenDebtorDetails = function (DebtorID) {
        $scope.ClearDebtorDetails();
        $scope.DetailsWindow.center().toFront().open();
        $scope.DetailsWindow.refresh({
            url: "/CMS/Co_Debtor/AddDebtorDetails?KaizenPublicKey=" + sessionStorage.PublicKey
        });
        //$scope.DetailsWindow.center().toFront().open();
        $scope.GetNationality();
        $scope.DebtorClass();
        $scope.DebtorPriority();
        $scope.DebtorGroup();
        $scope.DebtorStatus();
        if (DebtorID == null) return;

        $http.get('/Co_Debtor/GetSingle?KaizenPublicKey=' + sessionStorage.PublicKey + "&DebtorID=" + DebtorID).success(function (data) {
            $scope.CM00100 = data;
            if (data.CM00102 != null)
                $scope.CM00102 = data.CM00102;
            if ($scope.CM00100.BirthDate != null && $scope.CM00100.BirthDate != '')
                $scope.CM00100.BirthDate = kendoparsetoDate($scope.CM00100.BirthDate);
            if ($scope.CM00100.CPRExpiryDate != null && $scope.CM00100.CPRExpiryDate != '')
                $scope.CM00100.CPRExpiryDate = kendoparsetoDate($scope.CM00100.CPRExpiryDate);
            if ($scope.CM00100.CPRIssueDate != null && $scope.CM00100.CPRIssueDate != '')
                $scope.CM00100.CPRIssueDate = kendoparsetoDate($scope.CM00100.CPRIssueDate);
            if ($scope.CM00100.PassportExpiryDate != null && $scope.CM00100.PassportExpiryDate != '')
                $scope.CM00100.PassportExpiryDate = kendoparsetoDate($scope.CM00100.PassportExpiryDate);
            if ($scope.CM00100.PassportIssueDate != null && $scope.CM00100.PassportIssueDate != '')
                $scope.CM00100.PassportIssueDate = kendoparsetoDate($scope.CM00100.PassportIssueDate);
            if ($scope.CM00100.VisaExpiryDate != null && $scope.CM00100.VisaExpiryDate != '')
                $scope.CM00100.VisaExpiryDate = kendoparsetoDate($scope.CM00100.VisaExpiryDate);
            if ($scope.CM00100.ResidentPermitExpiryDate != null && $scope.CM00100.ResidentPermitExpiryDate != '')
                $scope.CM00100.ResidentPermitExpiryDate = kendoparsetoDate($scope.CM00100.ResidentPermitExpiryDate);
            $scope.CM00100.Status = 2;
        }).finally(function () {
            if ($scope.CM00100.SignatureExtension == null)
                $scope.Signaturelink = '/Photo/DebtorSignature/DebtorSignature.png';
            else
                $scope.Signaturelink = '/Photo/DebtorSignature/' + kaizenTrim($scope.CM00100.DebtorID) + "." + kaizenTrim($scope.CM00100.SignatureExtension) + "?c=" + Math.random();

            $scope.CM00104 = {};
        });
    };
    $scope.OpenJointDebtorDetails = function () {
        $scope.ClearDebtorDetails();
        $scope.DetailsWindow.center().toFront().open();
        $scope.DetailsWindow.refresh({
            url: "/CMS/Co_Debtor/JointDebtorDetails?KaizenPublicKey=" + sessionStorage.PublicKey
        });
    };
    $scope.ClearDebtorDetails = function () {
        $scope.CM00100 = {};
        $scope.CM00100.Status = 1;
        $scope.CM00100.IsActive = true;
        $scope.CM00100.DebtorStatusID = 1;
        $scope.link = '/Photo/DebtorPhoto/DebtorID.jpg';
        $scope.Signaturelink = '/Photo/DebtorSignature/DebtorSignature.png';
        $scope.IsOpenItemPanel = false;
    };
    $scope.SaveDebtorDetailsData = function () {
        if ($scope.CM00100.BirthDate != null && $scope.CM00100.BirthDate != '')
            $scope.CM00100.BirthDate = kendoparsetoDate($scope.CM00100.BirthDate);
        $http.post('/Co_Debtor/SaveData', { 'CM00100': $scope.CM00100, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
            if (data.Status == true) {
                $.bigBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#739E73",
                    timeout: 3000,
                    icon: "fa fa-check shake animated"
                });

                $scope.CM00203.CIFNumber = $scope.CM00100.DebtorID;
                $scope.CM00203.CIFName = $scope.CM00100.FirstNameEnglish;
                $scope.CM00203.CPRCRNo = $scope.CM00100.CPRCRNo;
                $scope.CM00203.CaseAddess = $scope.CM00100.AddressEnglish;
                $scope.DetailsWindow.close();
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
    $scope.UpdateDebtorDetailsData = function () {
        $http.post('/Co_Debtor/UpdateData', {
            'CM00100': $scope.CM00100,
            'PhotoChanged': false,
            'SignatureChanged': false,
            'KaizenPublicKey': sessionStorage.PublicKey
        }).success(function (data) {
            if (data.Status == true) {
                $.bigBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#739E73",
                    timeout: 3000,
                    icon: "fa fa-check shake animated"
                });
                $scope.CM00203.CIFNumber = $scope.CM00100.DebtorID;
                $scope.CM00203.CIFName = $scope.CM00100.FirstNameEnglish;
                $scope.CM00203.CPRCRNo = $scope.CM00100.CPRCRNo;
                $scope.CM00203.CaseAddess = $scope.CM00100.AddressEnglish;
                $scope.ClearDebtorDetails();
                $scope.DetailsWindow.close();
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
    $scope.DeleteJointDebtor = function (debtor) {
        $.SmartMessageBox({
            title: "Delete Record",
            content: "Are you sure you want to delete this?",
            buttons: '[No][Yes]'
        }, function (ButtonPressed) {
            if (ButtonPressed === "Yes") {
                var Debtorindex = $scope.GetSingleIndex($scope.RelatedJointDebtors, "DebtorID", debtor.DebtorID);

                $http.post('/Co_Debtor/DeleteJointAccount',
                          {
                              'CIFNumber': $scope.CM00203.CIFNumber,
                              'DebtorID': debtor.DebtorID,
                              'KaizenPublicKey': sessionStorage.PublicKey
                          }).success(function (data) {
                              if (data.Status == true) {
                                  $.bigBox({
                                      title: data.Massage,
                                      content: data.Description,
                                      color: "#739E73",
                                      timeout: 3000,
                                      icon: "fa fa-check shake animated"
                                  });
                                  $scope.RelatedJointDebtors.splice(Debtorindex, 1);
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
    //-----------Debtor-------------
    $scope.LoadDebtorPage = function () {
        $http.get('/Sys_View/GetViewsByScreen?ScreenID=6&KaizenPublicKey='
            + sessionStorage.PublicKey)
       .success(function (data) {
           $scope.DebtorViews = data;
       })
   .finally(function () {
       //alert($scope.DebtorViews.length);
       if ($scope.DebtorViews.length > 0) {
           $scope.SelectedLookUp.SelectedDebtorView = $scope.DebtorViews[0];
           $scope.DebtorViews.forEach(function (element, index) {
               if (element.IsDefault) {
                   $scope.SelectedLookUp.SelectedDebtorView = element;
                   return;
               }
           });
           $scope.DebtorCasetoolbarOptions = {
               items: [
                    { template: "<label>View:</label>" },
                    {
                        template: "<select kendo-drop-down-list k-ng-model='SelectedLookUp.SelectedDebtorView' k-options='DebtorToolBarOptions' style='width: 150px;'></select>",
                        overflow: "never"
                    }
               ]
           };
           $scope.DebtorToolBarOptions = {
               filter: "startswith",
               model: "SelectedView",
               dataTextField: "ViewName",
               dataValueField: "ViewID",
               dataSource: $scope.DebtorViews,
               value: $scope.SelectedLookUp.SelectedDebtorView.ViewID
           };
           $scope.DebtorMainGridChanged();
       }
       else {
           $.bigBox({
               title: "Missing User Setup",
               content: "Missing User Setup, call SYstem Administartor",
               color: "#C46A69",
               timeout: 4000,
               icon: "fa fa-bolt swing animated"
           });
           return;
       }

   });

    };
    $scope.DebtorMainGridChanged = function () {
        $scope.DebtorMainGrid = "/CMS/CMS_Case/DebtorMainGrid?ViewID=" + $scope.SelectedLookUp.SelectedDebtorView.ViewID + "&KaizenPublicKey=" + sessionStorage.PublicKey;
    }
    //-----------ActionManamgment-------------wael
    $scope.CM10701 = {};
    $scope.ActionMngObject = {};
    $scope.ActionMngObject.SelectedStatus = {};
    $scope.ActionMngObject.SelectedView = {};
    $scope.CaseStatusViewList = [];
    $scope.DateFrom = new Date();
    $scope.DateTo = new Date();
    $scope.OpenActionManamgmentWindows = function () {
        $scope.CaseStatusestemp = [];
        for (var T = 0; T < $scope.CM00700List.length; T++) {
            if ($scope.CM00700List[T].StatusActionTypeID == 4 && $scope.CM00700List[T].CaseStatusParent == null) {
                $scope.CaseStatusestemp.push($scope.CM00700List[T]);
            }
        }
        //if (angular.isUndefined($scope.ActionMngObject.CaseStatusID))
        $scope.ActionMngObject.SelectedStatus.CaseStatusID = $scope.CaseStatusestemp[0].CaseStatusID;
        //alert(JSON.stringify($scope.CaseStatusestemp, null, 2))

        $scope.CMS_Case_ActionManamgmenttoolbarOptions = {
            items: [
                       { template: "<label>Status:</label>" },
                       {
                           template: "<select kendo-drop-down-list k-ng-model='ActionMngObject.SelectedStatus' k-on-change='ActionMangmtStatusChanged()' k-options='ActionMngObjectCaseStatusIDOptions' id='ActionMngObjectCaseStatusIDOptionsDrop' style='width: 150px;'></select>",
                           overflow: "never"
                       },
                        {
                            template: "<select kendo-drop-down-list k-ng-model='ActionMngObject.SelectedView' k-on-change='ActionMangViewChanged()' k-options='ActionMngViewOptions' k-rebind='ActionMngViewOptions' style='width: 150px;'></select>",
                            overflow: "never"
                        },
                        { type: "separator" },
                        {
                            template: "<span><label>From:</label><input kendo-date-picker k-format='format_date' k-ng-model='DateFrom' k-on-change=\"GridRefresh(\'GridCMS_Case_ActionManamgment\')\" /></span>"
                        },
                        {
                            template: "<span><label>TO:</label><input kendo-date-picker k-format='format_date' k-max='maxDate' k-rebind='maxDate'  k-ng-model='DateTo' k-on-change='GridRefresh(\"GridCMS_Case_ActionManamgment\")' /></span>",
                            overflow: "never"
                        },
                        {
                            type: "button", text: "UP"
                                    , spriteCssClass: "k-tool-icon",
                            click: function (e) {
                                $scope.$apply(function () {
                                    $scope.GoBack();
                                });
                            }
                        },
                          {
                              type: "button", text: "Summary", click: function (e) {
                                  $scope.$apply(function () {
                                      $scope.GoTo("ActionManamgmentSummery");
                                  });
                              }
                          },
                           {
                               type: "button", text: "Advanced Summary", click: function (e) {
                                   $scope.$apply(function () {
                                       $scope.GoTo("ActionManamgmentSummeryAdvance");
                                   });
                               }
                           },
                           {
                               type: "splitButton",
                               spriteCssClass: "k-tool-icon k-i-excel",
                               text: "Export",
                               click: function (e) {
                                   $scope.SaveAsExcel("GridCMS_Case_ActionManamgment");
                               },
                               menuButtons: [
                                   { spriteCssClass: "k-tool-icon k-i-excel", text: "Excel", click: function (e) { $scope.SaveAsExcel("GridCMS_Case_ActionManamgment"); } },
                                   { spriteCssClass: "k-tool-icon k-i-pdf", text: "PDF", click: function (e) { $scope.SaveAsPdf("GridCMS_Case_ActionManamgment"); } },
                                   {
                                       spriteCssClass: "k-tool-icon k-i-pdf", text: "Print",
                                       click: function (e) {
                                           $scope.OpenkendoWindow('Sys_View', 'DynamicReport', $scope.SelectedView.ViewID);
                                       }
                                   }
                               ]
                           }
            ]
          , resizable: true
        };
        $scope.ActionMngObjectCaseStatusIDOptions = {
            filter: "contains",
            model: "ActionMngObject.SelectedStatus",
            dataTextField: "CaseStatusname",
            dataValueField: "CaseStatusID",
            dataSource: $scope.CaseStatusestemp,
            //value: $scope.ActionMngObject.ViewID
        };
        $scope.ActionMangmtStatusChanged();
    }
    $scope.ActionMangmtStatusChanged = function () {
        $scope.CaseStatusViewList = [];
        if (angular.isDefined($scope.ActionMngObject.SelectedStatus.CaseStatusID)) {
            $http.get('/CMS_Case/GetViewsByCaseStatusID?CaseStatusID='
                + $scope.ActionMngObject.SelectedStatus.CaseStatusID + '&KaizenPublicKey=' + sessionStorage.PublicKey)
                .success(function (data) {
                    if (data.length > 0) {
                        $scope.CaseStatusViewList = data;
                        $scope.ActionMngObject.SelectedView.ViewID = $scope.CaseStatusViewList[0].ViewID;
                        $scope.ActionMangViewChanged();
                    }
                    $scope.ActionMngViewOptions = {
                        filter: "contains",
                        model: "ActionMngObject.SelectedView",
                        dataTextField: "ViewName",
                        dataValueField: "ViewID",
                        dataSource: $scope.CaseStatusViewList,
                        //value: $scope.ActionMngObject.ViewID
                    };
                }).finally(function () { });

        }
    };
    $scope.ActionMangViewChanged = function () {
        // alert('ActionMangViewChanged' + $scope.ActionMngObject.ViewID);
        //alert($scope.ActionMngObject.ViewID);
        $scope.ActionManamgmentMainGridURL = "/CMS/CMS_Case/ActionManamgmentMainGrid?ViewID=" + $scope.ActionMngObject.SelectedView.ViewID + "&KaizenPublicKey=" + sessionStorage.PublicKey;
    }
    $scope.StatusActionAdvancedSumery_Change = function () {
        $scope.StatusChanged();
        $scope.CaseLookUps = [];
        //alert($scope.SelectedLookUp.SelectedStatusID);
        $http.get('/CMS_CaseStatus/GetAllLookupFields?CaseStatusID=' + $scope.SelectedLookUp.SelectedStatusID + '&KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.CaseLookUps = data;
        }).finally(function () { });
    }
    $scope.YearsStatusActionAdvancedSumery_Change = function () {
        $scope.ActionManamgmentMainGridURLReport = "/CMS/CMS_Case/ActionManamgmentSummeryAdvanceReport?ViewID=1&KaizenPublicKey=" + sessionStorage.PublicKey;
    }
    //----------CaseAction----------------
    $scope.CaseStatusestemp = [];
    $scope.StatusDynamicChilds = [];
    $scope.CM00060List = [];
    $scope.LookupList = [];
    $scope.StatusScripts = [];
    $scope.StatusChilds = [];
    $scope.openCaseActionWindow = function () {
        $scope.CM00203.WithHistory = false;
        $scope.CaseStatusestemp = [];
        $scope.StatusDynamicChilds = [];
        $scope.CM00060List = [];
        $scope.LookupList = [];
        $scope.CM10701 = {};
        $scope.SelectedLookUp.SelectedStatus = {};
        $scope.SelectedLookUp.SelectedStatus.IsReminder = false;
        $scope.SelectedLookUp.SelectedStatus.IsPTP = false;
        $scope.SelectedLookUp.StatusActionTypeID = null;
        $scope.StatusScripts = [];
        $scope.StatusTasks = [];
    };
    $scope.ActionTypeChanged = function () {
        $scope.SelectedLookUp.SelectedStatus = {};
        $scope.StatusTasks = [];
        $scope.CaseStatusestemp = [];
        for (var T = 0; T < $scope.CM00700List.length; T++) {
            if ($scope.CM00700List[T].StatusActionTypeID == $scope.SelectedLookUp.StatusActionTypeID
                && $scope.CM00700List[T].CaseStatusParent == null
                ) {
                $scope.CaseStatusestemp.push($scope.CM00700List[T]);
            }
        }
    };
    $scope.StatusChanged = function () {
        $scope.StatusDynamicChilds = [];
        $scope.CM00060List = [];
        $scope.LookupList = [];
        $scope.StatusScripts = [];

        $scope.StatusTasks = [];

        $scope.SelectedLookUp.SelectedStatus = $scope.functiontofindObjectByKeyValue($scope.CM00700List, "CaseStatusID", $scope.SelectedLookUp.SelectedStatusID);
        //alert(JSON.stringify($scope.SelectedLookUp.SelectedStatus, null, 2))
        $scope.GetDynamicSubStatus($scope.SelectedLookUp.SelectedStatus.CaseStatusID);

        $scope.GetScripts($scope.SelectedLookUp.SelectedStatus.CaseStatusID);

        if ($scope.SelectedLookUp.SelectedStatus.IsScripting) {

        }
        if ($scope.SelectedLookUp.SelectedStatus.IsTaskList) {
            $scope.LoadTasksData();
            $http.get('/CMS_CaseStatus/GetAllByCaseStatusID?KaizenPublicKey=' + sessionStorage.PublicKey, {
                params: {
                    CaseStatusID: $scope.SelectedLookUp.SelectedStatus.CaseStatusID
                }
            }).success(function (data) {
                if (data.length > 0)
                    $scope.StatusTasks = data;
                $scope.StatusTasks.forEach(function (element, index) {
                    element.TaskStartDate = new Date();
                    element.AssignDate = new Date();
                    var dt = new Date();
                    dt.setDate(dt.getDate() + element.RequiredDays);
                    element.TaskEndDate = dt;
                    element.TaskEndMaxDate = dt;
                    if (element.AgentID == null) {
                        element.AgentID = $scope.MY.UserName;
                        //alert('s');
                    }
                })
            }).finally(function () { });
        }
        if ($scope.SelectedLookUp.SelectedStatus.StatusHTML != '' && $scope.SelectedLookUp.SelectedStatus.StatusHTML != null) {
            $http.get('/CMS_CaseStatus/GetWorkableFields?KaizenPublicKey=' + sessionStorage.PublicKey,
            {
                params: {
                    CaseStatusID: $scope.SelectedLookUp.SelectedStatus.CaseStatusID
                }
            }).success(function (data) {
                if (data.length > 0)
                    $scope.CM00060List = data;
            }).finally(function () {
                for (var T = 0; T < $scope.CM00060List.length; T++) {
                    if ($scope.CM00060List[T].FieldTypeID == 1) {
                        $http.get('/CMS_CaseStatus/GetAllLookup?KaizenPublicKey=' + sessionStorage.PublicKey, {
                            params: {
                                CaseStatusID: $scope.SelectedLookUp.SelectedStatus.CaseStatusID
                            }
                        }).success(function (data) {
                            if (data.length > 0)
                                $scope.LookupList = data;
                        }).finally(function () {
                            //alert(JSON.stringify($scope.LookupList, null, 2));
                        });
                        break;
                    }
                }
            });
        }
    };
    $scope.GetDynamicSubStatus = function (CaseStatusID) {
        for (var T = 0; T < $scope.StatusDynamicChilds.length; T++) {
            if (angular.isUndefined($scope.StatusDynamicChilds[T].CaseStatusChildID)) continue;
            if ($scope.StatusDynamicChilds[T].CaseStatusChildID == CaseStatusID) {
                T += 1;
                for (var i = T; i < $scope.StatusDynamicChilds.length; i++) {
                    $scope.StatusDynamicChilds.splice(T, 1);
                }
            }
        }
        for (var T = 0; T < $scope.CaseStatusestemp.length; T++) {
            if ($scope.CaseStatusestemp[T].CaseStatusID == CaseStatusID) {
                if ($scope.CaseStatusestemp[T].IsHasChild) {
                    $scope.temp = { CaseStatusID: CaseStatusID };
                    $scope.temp.StatusChilds = [];
                    for (var I = 0; I < $scope.CM00700List.length; I++) {
                        if ($scope.CM00700List[I].CaseStatusParent == CaseStatusID) {
                            var temm = $scope.CM00700List[I];
                            var tempStstus = {
                                CaseStatusID: temm.CaseStatusID
                                , IsHasChild: temm.IsHasChild
                                , CaseStatusname: temm.CaseStatusname
                            };
                            $scope.temp.StatusChilds.push(tempStstus);
                        }
                    }
                    $scope.StatusDynamicChilds.push($scope.temp);
                }
                return;
            }
        }
        //var Status_indx = $scope.functiontofindIndexByKeyValue($scope.CaseStatuses, "CaseStatusID", CaseStatusID);
        //$scope.temp = $scope.CaseStatuses[Status_indx];
        //$scope.temp = $scope.GetSingle($scope.CaseStatuses, "CaseStatusID", CaseStatusID);
    }
    $scope.CaseStatusChildChanged = function (CaseStatusID) {
        $scope.GetDynamicSubStatus(CaseStatusID);
        $scope.GetScripts(CaseStatusID);
    };

    $scope.ScriptStatusChanged = function () {
        if (angular.isDefined($scope.CM10701.StatusScriptID)) {
            for (var I = 0; I < $scope.StatusScripts.length; I++) {
                if ($scope.StatusScripts[I].StatusScriptID == $scope.CM10701.StatusScriptID) {
                    $scope.CM10701.CaseStatusComment = $scope.StatusScripts[I].StatusScriptMain;
                    return;
                }
            }
        }
    };
    $scope.GetScripts = function (CaseStatusID) {
        for (var T = 0; T < $scope.CM00700List.length; T++) {
            var tt = $scope.CM00700List[T];
            if (tt.IsScripting) {
                if (tt.CaseStatusID == CaseStatusID) {
                    $http.get('/CMS_CaseStatus/GetAllScriptsByCaseStatusID?KaizenPublicKey=' + sessionStorage.PublicKey, {
                        params: {
                            CaseStatusID: CaseStatusID
                        }
                    }).success(function (data) {
                        if (data.length > 0) {
                            for (var I = 0; I < data.length; I++) {
                                var tempIsExited = false;
                                for (var T = 0; T < $scope.StatusScripts.length; T++) {
                                    if ($scope.StatusScripts[T].StatusScriptID == data[I].StatusScriptID) {
                                        tempIsExited = true;
                                    }
                                }
                                if (!tempIsExited) {
                                    $scope.temp = {
                                        StatusScriptID: data[I].StatusScriptID
                                        , StatusScriptName: data[I].StatusScriptName
                                        , StatusScriptMain: data[I].StatusScriptMain
                                    };
                                    $scope.StatusScripts.push($scope.temp);
                                }
                            }
                            return;
                        }
                    }).finally(function () { });
                }
            }
        }
    };
    $scope.SaveCaseAction = function () {
        $scope.CM10701.CaseRef = $scope.CM00203.CaseRef;
        $scope.CM10701.StatusActionTypeID = $scope.SelectedLookUp.StatusActionTypeID;
        $scope.CM10701.TRXTypeID = $scope.CM00203.TRXTypeID;
        $scope.CM10701.LastCaseStatusID = $scope.CM00203.CaseStatusID;
        $scope.CM10701.CurrencyCode = $scope.CM00203.CurrencyCode;
        //alert($scope.CM10701.LastCaseStatusID);
        //alert($scope.CM00203.CaseStatusID);
        $scope.CM10701.CaseStatusID = $scope.SelectedLookUp.SelectedStatus.CaseStatusID;
        $scope.CM10701.CaseStatusname = $scope.SelectedLookUp.SelectedStatus.CaseStatusname;
        //alert($scope.StatusDynamicChilds.length);
        if ($scope.StatusDynamicChilds.length > 0) {
            var tempp = $scope.StatusDynamicChilds[$scope.StatusDynamicChilds.length - 1];
            if (angular.isUndefined(tempp.CaseStatusChildID)) {
                alert('Sub Status not defind')
                return;
            }
            for (var T = 0; T < $scope.CM00700List.length; T++) {
                if ($scope.CM00700List[T].CaseStatusID == tempp.CaseStatusChildID) {
                    var dd = $scope.CM00700List[T];
                    $scope.CM10701.CaseStatusChild = dd.CaseStatusID;
                    $scope.CM10701.CaseStatusChildName = dd.CaseStatusname;
                    break;
                }
            }
        }

        if ($scope.SelectedLookUp.SelectedStatus.IsPTPRequired) {
            if ($scope.CM10701.PTPAMT == '' || $scope.CM10701.PTPAMT == parseFloat(0)) {
                $.smallBox({
                    title: "Required Data",
                    content: "Amount Field is Required for this Status !",
                    color: "#3276B1",
                    iconSmall: "fa fa-times fa-2x fadeInRight animated",
                    timeout: 4000
                });
                return;
            }
        }
        if ($scope.SelectedLookUp.SelectedStatus.IsReminder) {
            if ($scope.CM10701.ReminderDateTime == '' || $scope.CM10701.ReminderDateTime == null) {
                $.smallBox({
                    title: "Required Data",
                    content: "Reminder Date Time Field is Required for this Status !",
                    color: "#3276B1",
                    iconSmall: "fa fa-times fa-2x fadeInRight animated",
                    timeout: 4000
                });
                return;
            }
        }

        if ($scope.SelectedLookUp.SelectedStatus.IsCaseCondition) {
            $scope.CM00203.LastCaseStatusID = $scope.SelectedLookUp.SelectedStatus.CaseStatusID;
            $scope.CM00203.LastCasStatusname = $scope.SelectedLookUp.SelectedStatus.CaseStatusname;
            $scope.CM00203.LastCasStatusChldID = $scope.CM10701.CaseStatusChild;
            $scope.CM00203.LastCasStatusChldNam = $scope.CM10701.CaseStatusChildName;
            $scope.CM00203.LastCasStatusComment = $scope.CM10701.CaseStatusComment;
        }
        else if ($scope.SelectedLookUp.SelectedStatus.IsMidCaseStatus) {
            $scope.CM00203.MidCasStatusID = $scope.SelectedLookUp.SelectedStatus.CaseStatusID;
            $scope.CM00203.MidCasStatusnam = $scope.SelectedLookUp.SelectedStatus.CaseStatusname;
            $scope.CM00203.MidCasStatusChld = $scope.CM10701.CaseStatusChild;
            $scope.CM00203.MidCasStatusChldName = $scope.CM10701.CaseStatusChildName;
            $scope.CM00203.MidCasStatusComment = $scope.CM10701.CaseStatusComment;
        } else {
            $scope.CM00203.CaseStatusID = $scope.SelectedLookUp.SelectedStatus.CaseStatusID;
            $scope.CM00203.CaseStatusname = $scope.SelectedLookUp.SelectedStatus.CaseStatusname;
            $scope.CM00203.CaseStatusChild = $scope.CM10701.CaseStatusChild;
            $scope.CM00203.CaseStatusChildName = $scope.CM10701.CaseStatusChildName;
            $scope.CM00203.CaseStatusComment = $scope.CM10701.CaseStatusComment;
        }

        if (angular.isDefined($scope.CM00203.SelectedScript)) {
            $scope.CM10701.StatusScriptID = $scope.CM00203.SelectedScript.StatusScriptID;
        }

        $scope.CM10701.ClaimAmount = $scope.CM00203.ClaimAmount;
        $scope.CM10701.AgentID = $scope.MY.UserName;
        $scope.CM00203.ReminderDateTime = $scope.CM10701.ReminderDateTime;
        $scope.CM00203.PTPAMT = $scope.CM10701.PTPAMT;

        $scope.CM00060List.forEach(function (element, index) {
            element.FieldValue = $scope.CM10701[kaizenTrim(element.FieldCode)];
            if (element.FieldTypeID == 1) {
                for (var i = 0; i < $scope.LookupList.length; i++) {
                    if (kaizenTrim($scope.LookupList[i]["FieldCode"]) == kaizenTrim(element.FieldCode)) {
                        if (kaizenTrim($scope.LookupList[i]["LookupID"]) == kaizenTrim(element.FieldValue)) {
                            element.FieldName = $scope.LookupList[i]["LookupName"];
                            break;
                        }
                    }
                }
            } else if (element.FieldTypeID == 2) {
                element.FieldDateValue = element.FieldValue;
            }
        });
        $http({
            url: '/CMS_Case/SaveCaseHistoryData?KaizenPublicKey=' + sessionStorage.PublicKey,
            method: "POST",
            data: {
                'CM00060List': $scope.CM00060List
                , 'CM10701': $scope.CM10701
            },
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
                    timeout: 4000,
                    icon: "fa fa-check shake animated"
                });
                $scope.GoBack();
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
        }).error(function (data, status, headers, config) { });
        if ($scope.SelectedLookUp.SelectedStatus.IsTaskList) {
            $scope.StatusTasks.forEach(function (element, index) {
                element.TaskStartDate = kendoparsetoDate(element.TaskStartDate);
                element.TaskEndDate = kendoparsetoDate(element.TaskEndDate);
                element.AssignDate = kendoparsetoDate(element.AssignDate);
            })

            $http({
                url: '/CMS_Case/SaveCaseEventData?KaizenPublicKey=' + sessionStorage.PublicKey,
                method: "POST",
                data: {
                    'CaseRef': $scope.CM10701.CaseRef,
                    'CM00025': $scope.StatusTasks
                },
                dataType: "json",
                headers: {
                    'Content-Type': 'application/json; charset=utf-8'
                }
            }).success(function (data) {
                if (data.Status == true) {

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
            }).error(function (data, status, headers, config) { });
        }
    };
    $scope.DateTaskChanged = function (task) {
        var dt = new Date(task.TaskStartDate);
        dt.setDate(dt.getDate() + task.RequiredDays);
        task.TaskEndMaxDate = dt;
        task.TaskEndDate = dt;
    };
    $scope.DeleteActionTask = function (TaskID) {
        angular.forEach($scope.StatusTasks, function (story, key) {
            if (story.TaskID === TaskID) {
                $scope.StatusTasks.splice(key, 1);
                return;
            }
        });
    };
    $scope.ComplateTask = function (TaskID) {
        TaskID.TaskProgress = 100;
    };
    // Reminder -----------------------------------
    $scope.Reminder = {};
    $scope.Reminder.FromReminder = new Date();
    $scope.Reminder.ToReminder = new Date();
    $scope.maxDate = new Date(2050, 0, 1, 0, 0, 0);
    $scope.IsOverDue = false;
    $scope.AgentList = [];
    $scope.SelectedLookUp.SelectedAgent = {};

    $scope.LoadReminderData = function () {
        $scope.CaseReminderFilterationbytoggle = 2;
        $http.get('/CMS_Agent/GetMyListBySupervisor?KaizenPublicKey=' + sessionStorage.PublicKey,
            {
                params: {
                    StartDate: new Date($scope.Reminder.FromReminder),
                    EndDate: new Date($scope.Reminder.ToReminder)
                }
            }).success(function (data) {
                if (data.length > 0) {
                    $scope.AgentList = data;
                    $scope.SelectedLookUp.SelectedType = $scope.TRXTypes[0];

                    $scope.CMS_RemindertoolbarOptions = {
                        items: [
                           {
                               template: "<select kendo-drop-down-list k-ng-model='SelectedLookUp.SelectedType' k-on-change='ReminderTRXTypeID_Changed()' k-options='CaseTypeOptions' style='width: 150px;'></select>",
                               overflow: "never"
                           },
                           {
                               template: "<select kendo-drop-down-list k-ng-model='SelectedLookUp.SelectedView' k-on-change='ReminderView_Change()' k-options='CaseViewformatOptions' k-rebind='CaseViewformatOptions' style='width: 150px;'></select>",
                               overflow: "never"
                           },
                            {
                                template: "<select kendo-drop-down-list k-ng-model='SelectedLookUp.SelectedAgent' k-on-change='GridRefresh_Reminder()' k-options='AgentformatOptions' style='width: 110px;'></select>",
                                overflow: "never"
                            },
                              {
                                  type: "buttonGroup",
                                  buttons: [
                                      {
                                          text: "<span class=\"k-icon k-i-pin\"></span>", id: "radio1",
                                          togglable: true, group: "radio", selected: false,
                                          toggle: function (e) {
                                              $scope.$apply(function () {
                                                  $scope.CaseReminderFilterationbytoggle = 1;
                                                  $scope.GridRefresh_Reminder();
                                              });
                                          }
                                      },
                                      {
                                          text: "<span class=\"k-icon k-i-clock\"></span>", id: "radio2",
                                          togglable: true, group: "radio", selected: true,
                                          toggle: function (e) {
                                              $scope.$apply(function () {
                                                  $scope.CaseReminderFilterationbytoggle = 2;
                                                  $scope.GridRefresh_Reminder();
                                              });
                                          }
                                      },
                                      {
                                          text: "<span class=\"k-icon k-i-note\"></span>", id: "radio3",
                                          togglable: true, group: "radio",
                                          toggle: function (e) {
                                              $scope.$apply(function () {
                                                  $scope.CaseReminderFilterationbytoggle = 3;
                                                  $scope.GridRefresh_Reminder();
                                              });
                                          }
                                      }
                                  ]
                              },

                            { type: "separator" },
                            {
                                template: "<span ng-hide='IsOverDue || CaseReminderFilterationbytoggle !=2'><label>From:</label><input style='width:110px;' kendo-date-picker k-format='format_date' k-ng-model='Reminder.FromReminder' k-on-change='GridRefresh_Reminder()' /></span>"
                            },
                            {
                                template: "<span ng-hide='CaseReminderFilterationbytoggle !=2'><label>TO:</label><input style='width:110px;' kendo-date-picker k-format='format_date' k-max='maxDate' k-rebind='maxDate'  k-ng-model='Reminder.ToReminder' k-on-change='GridRefresh_Reminder()' /></span>",
                                overflow: "never"
                            },
                            {
                                template: "<kendo-button ng-click='OverDue_Chnaged()'ng-hide='CaseReminderFilterationbytoggle !=2'><div ng-if='!IsOverDue'>Over Due</div><div ng-if='IsOverDue'>Reminder</div></kendo-button>",
                                overflow: "never"
                            },
                             {
                                 template: "<span class='k-textbox k-space-right' style='width: 100%;'><input type='text' ng-model='SelectedLookUp.Searchcritaria'/>"
                                        + "<a ng-click='GridRefresh_Reminder()' href='#' class='k-icon k-i-search'>&nbsp;</a></span>",
                             },
                              {
                                  type: "splitButton",
                                  text: "Schedule",
                                  click: function (e) {
                                      //$scope.OpenSchedule();
                                  },
                                  menuButtons: [
                                  {
                                      text: "Schedule", click: function (e) {
                                          //$scope.OpenSchedule();
                                      }
                                  },
                                  {
                                      text: "Tasks",
                                      click: function (e) {
                                          //$scope.OpenTaskSchedule();
                                      }
                                  },
                                  {
                                      text: "Team Schedule",
                                      click: function (e) {
                                          //$scope.OpenTeamSchedule();
                                      }
                                  }
                                  ]
                              }
                        ]
                    , resizable: true
                    };
                    $scope.AgentformatOptions = {
                        filter: "contains",
                        dataTextField: "AgentID",
                        dataValueField: "AgentID",
                        dataSource: $scope.AgentList,
                        value: $scope.MY.UserName
                    };
                    $scope.SelectedLookUp.SelectedAgent = $scope.functiontofindObjectByKeyValue($scope.AgentList, "AgentID", $scope.MY.UserName);
                    //waell
                    $scope.ReminderTRXTypeID_Changed();
                }
            }).finally(function () { });
    };
    $scope.GridRefresh_Reminder = function () {
        $scope.GridRefresh('GridCMS_Reminder');
    }
    $scope.OverDue_Chnaged = function () {
        if ($scope.IsOverDue) {
            $scope.IsOverDue = false;
            $scope.ToReminder = new Date();
            $scope.maxDate = new Date();
        }
        else {
            $scope.IsOverDue = true;
            $scope.maxDate = new Date(2050, 0, 1, 0, 0, 0);
        }
        $scope.GridRefresh_Reminder();
    }
    $scope.ReminderView_Change = function () {
        if ($scope.SelectedLookUp.SelectedView == undefined || $scope.SelectedLookUp.SelectedView.ViewID == undefined) {
            alert('Missing User Setup');
            return;
        }
        $scope.ReminderMainGridPath = null;
        $scope.ReminderMainGridPath = '';
        //alert($scope.SelectedLookUp.SelectedView.ViewID);
        var temp = "/CMS/CMS_Case/ReminderMainGrid?ViewID=" + $scope.SelectedLookUp.SelectedView.ViewID + "&KaizenPublicKey=" + sessionStorage.PublicKey;
        $scope.ReminderMainGridPath = temp;

    };
    $scope.ReminderTRXTypeID_Changed = function () {
        $scope.SelectedLookUp.SelectedView = null;
        $scope.ReminderMainGridPath = null;
        $scope.ReminderMainGridPath = '';
        $http.get('/CMS_Case/GetMyViewsByTRXTypeID?TRXTypeID=' + $scope.SelectedLookUp.SelectedType.TRXTypeID + '&KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.ViewList = data;
        }).finally(function () {
            if ($scope.ViewList.length > 0) {
                $scope.SelectedLookUp.SelectedView = $scope.ViewList[0];
                $scope.ViewList.forEach(function (element, index) {
                    if (element.IsDefault) {
                        $scope.SelectedLookUp.SelectedView = element;
                        return;
                    }
                });
                $scope.ReminderView_Change();
            } else {
                alert('Missing User Setup')
                $scope.MainGridURL = null;
                $scope.MainGridURL = '';
                $scope.SelectedLookUp.SelectedView = null;
                $scope.ViewList = [];
            }
            $scope.CaseViewformatOptions = {
                filter: "contains",
                optionLabel: "-- ALL View --",
                dataTextField: "ViewName",
                dataValueField: "ViewID",
                dataSource: $scope.ViewList,
            };
        });
    };

    // Tasks -----------------------------------
    $scope.LoadTasksData = function () {
        $http.get('/CMS_Case/FillTaskTypesList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.TaskTypes = data;
        }).finally(function () { });
        $http.get('/CMS_Case/FillTaskPrioritiesList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.TaskPriorities = data;
        }).finally(function () { });

        $scope.CMS_TaskstoolbarOptions = {
            items: [
                {
                    template: "<span ng-hide='IsOverDue'><label>From:</label><input kendo-date-picker k-format='format_date' k-ng-model='FromReminder' k-on-change='onReminderDateFilterChange()' /></span>",
                    overflow: "never"
                },
                {
                    template: "<span><label>TO:</label><input kendo-date-picker k-format='format_date' k-max='maxDate' k-rebind='maxDate'  k-ng-model='ToReminder' k-on-change='onReminderDateFilterChange()' /></span>",
                    overflow: "never"
                },
                { template: "<label>Agent:</label>" },
                {
                    template: "<select kendo-drop-down-list k-ng-model='SelectedLookUp.SelectedAgent' k-on-change='AgentTaskChanged()' k-options='AgentformatOptions' style='width: 120px;'></select>",
                    overflow: "never"
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
                {
                    type: "button", text: "Cancel",
                    click: function (e) {
                        $scope.$apply(function () {
                            $scope.GoBack();
                        });
                    }
                }
            ]
            , resizable: true
        };
    };
    $scope.AgentTaskChanged = function () {
        $scope.GridRefresh('GridTasks');
    };
    $scope.EditSys00100 = function (TaskID) {
        $scope.GoTo('TaskView');
        $scope.LoadTask(TaskID);
    };
    $scope.LoadTask = function (TaskID) {
        $http.get('/CMS_Case/GetSingleTask?KaizenPublicKey=' + sessionStorage.PublicKey
           + "&TaskID=" + TaskID).success(function (data) {
               $scope.CM00213 = data;
               $scope.CM00214 = {};
               $scope.CM00214.TaskID = $scope.CM00213.TaskID;
               $scope.CM00214.AgentID = $scope.MY.UserName;
               $scope.CM00214.TaskDate = new Date();
               $scope.SelectedLookUp.TaskTypeID = $scope.CM00213.TaskTypeID;
               $scope.SelectedLookUp.PriorityID = $scope.CM00213.PriorityID;
           }).finally(function () { });

        $http.get('/CMS_Case/GetTaskActionList?KaizenPublicKey=' + sessionStorage.PublicKey
          + "&TaskID=" + TaskID).success(function (data) {
              $scope.CM00214List = data;
              angular.forEach($scope.CM00214List, function (tempUser, key) {
                  tempUser.PhotoExtension = GetUserPhoto(tempUser.AgentID, tempUser.PhotoExtension);
              });
          }).finally(function () { });
    };

    $scope.SaveTaskAction = function (WithClosing) {
        var tt = $scope.MY.PhotoExtension;
        $scope.CM00214.PhotoExtension = tt.substring(tt.lastIndexOf('.') + 1, tt.length);
        $http.post('/CMS_Case/SaveTaskAction', { 'oCM00214': $scope.CM00214, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
            if (data.Status == true) {
                $.bigBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#739E73",
                    timeout: 3000,
                    icon: "fa fa-check shake animated"
                });
                if (WithClosing) {
                    $scope.GoBack();
                } else {
                    $scope.LoadTask($scope.CM00214.TaskID);
                }
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

    // Documents -----------------------------------
    $scope.openCaseDocumentWindow = function () {
        $http.get('/Admin_DocumnetType/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey + '&ContactSourceID=6').success(function (data) {
            $scope.CaseDocumentTypes = data;
        }).finally(function () {
            $scope.GridRefresh('GridCMS_CaseDocumentByCase');
        });
    };
    $scope.SaveCaseDocument = function () {
        //var grid = $("#GridCMS_CaseDocumentByCase").data("kendoGrid");
        //var dataSourceData = grid.dataSource.data();
        $scope.CM00208.CaseRef = $scope.CM00203.CaseRef;
        $http.post('/CMS_Case/SaveCaseDocument', { 'CM00208': $scope.CM00208, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
            if (data.Status == true) {
                $.bigBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#739E73",
                    timeout: 3000,
                    icon: "fa fa-check shake animated"
                });
                $scope.GridRefresh('GridCMS_CaseDocumentByCase');
                $scope.CM00208 = { Status: 1 };
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

    $scope.GoTo();

    //------------------ Show Graph ----------------
    $scope.GraphTypeName = "";
    $scope.GraphIdChanged = function () {
        alert($scope.SelectedLookUp.SelectedGraph.GraphTypeID);
        var graph;
        if ($scope.SelectedLookUp.SelectedGraph) {
            graph = $scope.SelectedLookUp.SelectedGraph;
            var Chart = new KendoGraph();
            Chart.divId = "chart";
            Chart.serviceURL = "/CMS_CaseType/GraphSettings?KaizenPublicKey=" + sessionStorage.PublicKey;
            Chart.type = graph.CM00040.GraphTypeName;
            //barChart.dataURL = "/CMS_CaseType/GraphData?KaizenPublicKey=" + sessionStorage.PublicKey + "";
            //var data = $scope.SetGraphData(graph.GraphID, graph.CM00040.GraphTypeName, Chart);
            $http.get('/CMS_CaseType/GraphData?KaizenPublicKey=' + sessionStorage.PublicKey,
                    { params: { GraphID: graph.GraphID, GraphTypeName: graph.CM00040.GraphTypeName } })
                .success(function (data) {
                    if (data) {
                        graphData = data;
                        Chart.categories = data.Categories;
                        Chart.title = data.Title;
                        Chart.LoadSeries(data.Series);
                        if (Chart.type.toLowerCase() === "pie" || Chart.type.toLowerCase() === "donut" || Chart.type.toLowerCase() === "funnel") {
                            Chart.categoryAxis = {};
                        }
                        Chart.Load();
                    }
                });
        }
        $scope.GraphTypeName = graph.CM00040.GraphTypeName;
    };
    //$scope.SetGraphData = function (GraphID, GraphTypeName, Chart) {
    //    var graphData = [];


    //    return graphData;
    //};
});