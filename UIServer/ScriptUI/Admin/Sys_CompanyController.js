app.controller('Sys_CompanyController', function ($scope, $http) {
    $scope.Company = {};
    $scope.PagesCompany = [];
    $scope.LoadCompany = function (CompanyID) {
        if (angular.isUndefined($scope.Company.CompanyID) || $scope.Company.CompanyID != CompanyID) {
            $http.get('/Sys_Company/GetSingle?KaizenPublicKey=' + sessionStorage.PublicKey + "&CompanyID=" + CompanyID).success(function (data) {
                $scope.Company = data;
            }).finally(function () {
                $scope.Company.Status = 2;
            });
        }
    };
    $scope.LoadCompanyPage = function (ActionName) {
        removeEntityPage($scope.PagesCompany);
        var URIPath = "/" + $scope.ToolBar.ActiveScreen.ID + "/" + ActionName + "?KaizenPublicKey=" + sessionStorage.PublicKey;
        var Page = {
            url: URIPath, ActionName: ActionName
        };
        $scope.PagesCompany.push(Page);
    };
    $scope.LoadLookUp = function () {
        $http.get('/SOP_Invoice/FillSOPTypeDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.SOPTypes = data;
        }).finally(function () { });
        $http.get('/GL_Set_FiscalYears/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.Years = data;
            $scope.Years.forEach(function (element, index) {
                element.PeriodFrom = kendoparsetoDate(element.PeriodFrom);
                element.PeriodTo = kendoparsetoDate(element.PeriodTo);
            })
        }).finally(function () { });
    };
    $scope.LoadLookUp();

    $scope.toolbarOptions = {
        items: [
				 {
				     type: "button", text: "<span class=\"fa fa-plus-circle\"></span> Company",
				     attributes: { "class": "btn-primary" },
				     click:
						 function (e) {
						     $scope.LoadCompanyPage('Create');
						     $scope.Clear();
						     $scope.Company.Status = 1
						     $scope.$apply();
						 }
				 },
				  {
				      type: "button", text: "<span class=\"fa fa-plus-circle\"></span> Segment",
				      attributes: { "class": "btn-default" },
				      click: function (e) {
				          $scope.SegmentManagement();
				      }
				  },
                  {
                      type: "button", text: "<span class=\"fa fa-plus-circle\"></span> Module",
                      attributes: { "class": "btn-default" },
                      click: function (e) {
                          $scope.CompanyModuleManagement();
                      }
                  },
                  {
                      type: "button", text: "<span class=\"fa fa-plus-circle\"></span> SMS Account",
                      attributes: { "class": "btn-default" },
                      click: function (e) {
                          $scope.CompanySMSAccount();
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

    $scope.EditCompany = function (CompanyID) {
        $scope.LoadCompanyPage('Create');
        $scope.LoadCompany(CompanyID);
    };
    $scope.SaveData = function () {
        $http.post('/Sys_Company/SaveData', {
            'Company': $scope.Company, 'KaizenPublicKey': sessionStorage.PublicKey
        }).success(function (data) {
            if (data.Status == true) {
                $.bigBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#739E73",
                    timeout: 8000,
                    icon: "fa fa-check shake animated",
                    number: "4"
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
                    icon: "fa fa-bolt swing animated",
                    number: "4"
                });
            }
        }).error(function (data, status, headers, config) {
            $.bigBox({
                title: data.Massage,
                content: data.Description,
                color: "#C46A69",
                timeout: 8000,
                icon: "fa fa-bolt swing animated",
                number: "4"
            });
        });
    };
    $scope.UpdateData = function () {
        $http.post('/Sys_Company/UpdateData', {
            'Company': $scope.Company, 'KaizenPublicKey': sessionStorage.PublicKey
        }).success(function (data) {
            if (data.Status == true) {
                $.bigBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#739E73",
                    timeout: 8000,
                    icon: "fa fa-check shake animated",
                    number: "4"
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
                    icon: "fa fa-bolt swing animated",
                    number: "4"
                });
            }
        }).error(function (data, status, headers, config) {
            $.bigBox({
                title: data.Massage,
                content: data.Description,
                color: "#C46A69",
                timeout: 8000,
                icon: "fa fa-bolt swing animated",
                number: "4"
            });
        });
    };
    $scope.Clear = function () {
        $scope.Company = {};
        $scope.SMSAccountList = [];
        $scope.Kaizen00040 = {};
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
                $http.post('/Sys_Company/DeleteData', { 'Company': $scope.Company, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
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
        $scope.PagesCompany = [];
        $scope.Clear();
    };

    //-------------------------- Company Segment
    $scope.CompanySegment = {};
    $scope.SegmentEnable = true;
    $scope.SegmentManagement = function () {
        removeEntityPage($scope.PagesCompany);
        $scope.InsertedSegments = [];
        $scope.UpdatedSegments = [];
        $scope.DeletedSegments = [];
        $scope.CompanySegments = [];
        $scope.CompanySegment.Status = 1;
        $scope.LoadCompanyPage('CompanySegment');
        $http.get('/Sys_Company/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.Companies = data;
        }).finally(function () { });
    };

    $scope.SaveCompanySegment = function () {
        $scope.CompanySegment.CompanyID = $scope.Company.CompanyID;
        alert($scope.CompanySegment.Status);
        $http.post('/Sys_Company/SaveCompanySegment',
			{
			    'CompanySegment': $scope.CompanySegment,
			    'KaizenPublicKey': sessionStorage.PublicKey
			}).success(function (data) {
			    if (data.Status == true) {
			        $.bigBox({
			            title: data.Massage,
			            content: data.Description,
			            color: "#739E73",
			            timeout: 8000,
			            icon: "fa fa-check shake animated",
			            number: "4"
			        });
			        $scope.GridRefresh('GridSys_CompanyBySegment');
			        $scope.CompanySegment = { Status: 1 };
			    }
			    else {
			        $.bigBox({
			            title: data.Massage,
			            content: data.Description,
			            color: "#C46A69",
			            timeout: 8000,
			            icon: "fa fa-bolt swing animated",
			            number: "4"
			        });
			    }
			}).error(function (data, status, headers, config) {
			    alert('s')
			    $.bigBox({
			        title: data.Massage,
			        content: data.Description,
			        color: "#C46A69",
			        timeout: 8000,
			        icon: "fa fa-bolt swing animated",
			        number: "4"
			    });
			});
    };
    $scope.UpdateCompanySegment = function () {
        $scope.CompanySegment.CompanyID = $scope.Company.CompanyID;
        $http.post('/Sys_Company/UpdateCompanySegment', { 'CompanySegment': $scope.CompanySegment, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
            if (data.Status == true) {
                $.bigBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#739E73",
                    timeout: 8000,
                    icon: "fa fa-check shake animated",
                    number: "4"
                });
                $scope.GridRefresh('GridSys_CompanyBySegment');
                $scope.CompanySegment = { Status: 1 };

            }
            else {
                $.bigBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#C46A69",
                    timeout: 8000,
                    icon: "fa fa-bolt swing animated",
                    number: "4"
                });
            }
        }).error(function (data, status, headers, config) {
            $.bigBox({
                title: data.Massage,
                content: data.Description,
                color: "#C46A69",
                timeout: 8000,
                icon: "fa fa-bolt swing animated",
                number: "4"
            });
        });
    };
    $scope.DeleteCompanySegment = function () {
        $.SmartMessageBox({
            title: "Delete Record",
            content: "Are you sure you want to delete this?",
            buttons: '[No][Yes]'
        }, function (ButtonPressed) {
            if (ButtonPressed === "Yes") {
                $http.post('/Sys_Company/DeleteCompanySegment', { 'CompanySegment': $scope.CompanySegment, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
                    if (data.Status == true) {
                        $.bigBox({
                            title: data.Massage,
                            content: data.Description,
                            color: "#739E73",
                            timeout: 8000,
                            icon: "fa fa-check shake animated",
                            number: "4"
                        });
                        $scope.GridRefresh('GridSys_CompanyBySegment');
                        $scope.CompanySegment = { Status: 1 };
                    }
                    else {
                        $.bigBox({
                            title: data.Massage,
                            content: data.Description,
                            color: "#C46A69",
                            timeout: 8000,
                            icon: "fa fa-bolt swing animated",
                            number: "4"
                        });
                    }
                }).error(function (data, status, headers, config) {
                    $.bigBox({
                        title: data.Massage,
                        content: data.Description,
                        color: "#C46A69",
                        timeout: 8000,
                        icon: "fa fa-bolt swing animated",
                        number: "4"
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
    $scope.CompanyChanged = function () {
        if ($scope.Company.CompanyID != null && $scope.Company.CompanyID != '') {
            $scope.CompanySegment.CompanyID = $scope.Company.CompanyID;
            $scope.GridRefresh('GridSys_CompanyBySegment');
            $http.get('/GL_GL/GetTop5?CompanyID=' + $scope.CompanySegment.CompanyID + '&KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
                if (data.length > 0) {
                    $scope.SegmentEnable = false;
                    var grid = $("#GridSys_CompanyBySegment").data("kendoGrid");
                    grid.hideColumn(4);
                }
                else {
                    $scope.SegmentEnable = true;
                    var grid = $("#GridSys_CompanyBySegment").data("kendoGrid");
                    grid.showColumn(4);
                }

            }).finally(function () { });
        }
    };
    //////////////////
    $scope.CurrencyBack = function (currency) {
        if (currency != null) {
            $scope.Company.DecimalDigit = currency.DecimalDigit;
            $scope.Company.CurrencyCode = currency.CurrencyCode;
            $scope.Company.ExchangeTableID = currency.ExchangeTableID;
            $scope.Company.IsMultiply = currency.IsMultiply;
            $scope.Company.ExchangeRateID = currency.ExchangeRateID;
            $scope.Company.ExchangeRate = currency.ExchangeRate;
            var kendoWindow = $("#MainDialog").data("kendoWindow");
            kendoWindow.close();
        }
    };
    $scope.ExchangeRateBack = function (rate) {
        if (rate != null) {
            $scope.Company.ExchangeRateID = rate.ExchangeRateID;
            $scope.Company.ExchangeRate = rate.ExchangeRate;
        }
    };
    $scope.ExchangeTableBack = function (table) {
        if (table != null) {
            $scope.Company.ExchangeTableID = table.ExchangeTableID;
            $scope.Company.IsMultiply = table.IsMultiply;
            var kendoWindow = $("#MainDialog").data("kendoWindow");
            kendoWindow.close();
        }
    };
    $scope.SaveExchangeRate = function () {
        $scope.GL00012.ExchangeTableID = $scope.Company.ExchangeTableID;
        $scope.GL00012.CurrencyCode = $scope.Company.CurrencyCode;

        $http.post('/GL_ExchangeRate/SaveData', { 'GL00012': $scope.GL00012, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
            if (data.Status == true) {
                Notify(data.Massage, 'bottom-right', '5000', 'success', 'fa-check', true);
                $scope.ClearExchangeRate();
                $scope.GridRefresh('GridGL_ExchangeRatePopUp');
            }
            else {
                Notify(data.Massage, 'bottom-right', '5000', 'danger', 'fa-bolt', true);
            }
        }).error(function (data, status, headers, config) { alert(); });
    };
    $scope.ClearExchangeRate = function () {
        $scope.GL00012 = {};
    };

    $scope.OpenExchangeRateWindow = function () {
        $scope.GL00012 = {};
        $scope.ParmObject = $scope.Company;
        $scope.ExchangeRateWindow.center().toFront().open();
        $scope.ExchangeRateWindow.refresh({
            url: "/GL_ExchangeRate/PopUpRates?KaizenPublicKey=" + sessionStorage.PublicKey
        });
    };

    $scope.CheckbookBack = function (checkbook) {
        if (checkbook != null) {
            $scope.Company.CheckbookCode = checkbook.CheckbookCode;
        }
    };
    $scope.SiteBack = function (site) {
        if (site != null) {
            $scope.Company.SiteID = site.SiteID;
        }
    };
    $scope.SetupTypeBack = function (type) {
        if (type != null) {
            $scope.Company.SOPTypeSetupID = type.SOPTypeSetupID;
        }
    };

    //-------------------------- Company Module
    $scope.Kaizen00101 = {};
    $scope.CompanyModules = [];
    $scope.CompanyModuleManagement = function () {
        removeEntityPage($scope.PagesCompany);
        $scope.LoadCompanyPage('CompanyModule');
        $http.get('/Sys_Company/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.Companies = data;
        }).finally(function () { });
        $http.get('/Sys_KaizenModule/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.Modules = data;
        }).finally(function () { });
    };
    $scope.ModuleCompanyChanged = function () {
        if ($scope.Kaizen00101.CompanyID != null && $scope.Kaizen00101.CompanyID != '') {
            $http.get('/Sys_Company/GetModuleAccessByCompany?CompanyID=' + $scope.Kaizen00101.CompanyID + '&KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
                $scope.CompanyModules = data;
            }).finally(function () {
                for (var i = 0; i < $scope.Modules.length; i++) {
                    var item = $scope.Modules[i];
                    item.isSelected = false;
                    for (var j = 0; j < $scope.CompanyModules.length; j++) {
                        var obj = $scope.CompanyModules[j];
                        if (obj.ModuleID === item.ModuleID) {
                            item.isSelected = true;
                            break;
                        }
                    }
                }
            });
        }
    };
    $scope.CheckCompanyModule = function (module) {
        module.CompanyID = $scope.Kaizen00101.CompanyID;
        if (module.isSelected) {
            $http({
                url: '/Sys_Company/SaveCompanyModule?KaizenPublicKey=' + sessionStorage.PublicKey,
                method: "POST",
                data: $.param({
                    Kaizen00101: module
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
            $http({
                url: '/Sys_Company/DeleteCompanyModule?KaizenPublicKey=' + sessionStorage.PublicKey,
                method: "POST",
                data: $.param({
                    Kaizen00101: module
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

    //------------------------- Company SMS Account ------------------
    $scope.Kaizen00040 = {};
    $scope.CompanySMSAccount = function () {
        removeEntityPage($scope.PagesCompany);
        $scope.Kaizen00040.status = 0;
        $scope.LoadCompanyPage('CompanySMSAccount');
        $http.get('/Sys_Company/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.Companies = data;
        }).finally(function() {
            $scope.CompanyHasOptions = {
                filter: "startswith",
                model: "Company.SelectedCompany",
                optionLabel: "-- Select Company --",
                dataTextField: "CompanyName",
                dataValueField: "CompanyID",
                dataSource: $scope.Companies,
                value: $scope.Company.CompanyID
            };
        });
    };

    $scope.CompanyChangedForSMSAccount = function () {
        if (angular.isDefined($scope.Company.SelectedCompany)) {
            $scope.Company.CompanyID = $scope.Company.SelectedCompany.CompanyID;
            $scope.Kaizen00040.CompanyID = $scope.Company.SelectedCompany.CompanyID;
            $scope.LoadSMSAccount_ByCompany($scope.Company.CompanyID);
        }
    };

    $scope.LoadSMSAccount_ByCompany = function (CompanyID) {
        $http.get('/Sys_Company/GetSMSAccountByCompany?KaizenPublicKey=' + sessionStorage.PublicKey,
            { params: { CompanyID: CompanyID } }).success(function (data) {
                if (data.length >= 0) {
                    if (data.length > 0)
                        $scope.SMSAccountList = data;
                    else
                        $scope.SMSAccountList = [];
                }
            });
    };

    $scope.AddSMSAccount = function () {
        $scope.Kaizen00040.status = 1;

        if ($scope.Kaizen00040) {
            $http({
                url: '/Sys_Company/SaveCompanySMSAccount?KaizenPublicKey=' + sessionStorage.PublicKey,
                method: "POST",
                data: JSON.stringify($scope.Kaizen00040),
                dataType: "json",
                headers: {
                    'Content-Type': 'application/json; charset=utf-8'
                }
            }).success(function (data) {
                if (data.Status == true) {
                    $scope.SMSAccountList.push($scope.Kaizen00040);
                    $scope.Kaizen00040 = {};
                    $scope.Kaizen00040.status = 0;

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
                    content: "Unable to save company sms account data",
                    color: "#C46A69",
                    timeout: 8000,
                    icon: "fa fa-bolt swing animated"
                });
            });
        }
    };

    $scope.EditSMSAccount = function (companySmsAccount) {
        indx = $scope.SMSAccountList.indexOf(companySmsAccount);
        $scope.Kaizen00040 = companySmsAccount;
        var Obj_indx = $scope.functiontofindIndexByKeyValue($scope.SMSAccountList, "SMSAccountCode", $scope.Kaizen00040.SMSAccountCode);
        var obj = $scope.SMSAccountList[Obj_indx];
        //if ($scope.CM00070.status == 0 || angular.isUndefined($scope.CM00070.status)) {
        $scope.Kaizen00040.status = 2;
        //}
    };
    var indx;
    $scope.UpdateSMSAccount = function () {
        if ($scope.Kaizen00040.status == 0 || angular.isUndefined($scope.Kaizen00040.status)) {
            $scope.Kaizen00040.status = 2;
        }
        $scope.SMSAccountList.splice(indx, 1, $scope.Kaizen00040);

        if ($scope.Kaizen00040) {
            //if ($scope.CM00028.SelectedFieldType && $scope.CM00028.SelectedFieldType.FieldTypeID)
            //    $scope.Kaizen00040.FieldTypeID = $scope.CM00028.SelectedFieldType.FieldTypeID;

            $http({
                url: '/Sys_Company/UpdateSMSAccount?KaizenPublicKey=' + sessionStorage.PublicKey,
                method: "POST",
                data: $scope.Kaizen00040,
                dataType: "json",
                headers: {
                    'Content-Type': 'application/json; charset=utf-8'
                }
            }).success(function (data) {
                if (data.Status == true) {
                    $scope.Kaizen00040 = {};
                    $scope.Kaizen00040.status = 0;

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
                    content: "Unable to update company sms account data",
                    color: "#C46A69",
                    timeout: 8000,
                    icon: "fa fa-bolt swing animated"
                });
            });
        }
    };

    $scope.RemoveSMSAccount = function (companySmsAccount, index) {

        $.SmartMessageBox({
            title: "Delete Record",
            content: "Are you sure?",
            buttons: '[No][Yes]'
        }, function (ButtonPressed) {
            if (ButtonPressed === "Yes") {

                $http({
                    url: '/Sys_Company/DeleteSMSAccount?KaizenPublicKey=' + sessionStorage.PublicKey,
                    method: "POST",
                    data: JSON.stringify(companySmsAccount),
                    dataType: "json",
                    headers: {
                        'Content-Type': 'application/json; charset=utf-8'
                    }
                }).success(function (data) {
                    if (data.Status == true) {
                        if (companySmsAccount.status == 1)
                            $scope.SMSAccountList.splice(index, 1);
                        else
                            companySmsAccount.status = 3;
                        //Notify(data.Massage, 'bottom-right', '5000', 'success', 'fa-check', true);
                        $.bigBox({
                            title: data.Massage,
                            content: data.Description,
                            color: "#C46A69",
                            timeout: 8000,
                            icon: "fa fa-bolt swing animated"
                        });
                    } else {
                        //Notify(data.Massage, 'bottom-right', '5000', 'danger', 'fa-bolt', true);
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
                        title: "Error",
                        content: "Unable to delete company sms account",
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
});
