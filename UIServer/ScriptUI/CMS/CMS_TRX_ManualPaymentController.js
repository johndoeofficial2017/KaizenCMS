app.controller('CMS_TRX_ManualPaymentController', function ($scope, $http) {
    $scope.CM00307 = {};
    $scope.CM00307.SelectedExchangeTable = {};
    $scope.SelectedView = {};
    $scope.PagesCM00307 = [];
    $scope.toolbarOptions = {
        items: [
                 {
                     type: "button", text: "<span class=\"fa fa-plus-circle\"></span> Setup Payment ",
                     attributes: { "class": "btn-primary" },
                     click: function (e) {
                         $scope.$apply(function () {
                             $scope.AddCM00307();
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
                 },
                 { type: "separator" },
                 { template: "<label>View:</label>" },
                 {
                     template: "<select kendo-drop-down-list k-ng-model='SelectedView' k-on-change='ViewChanged()' k-options='formatOptions' id='GridViewoption_CMS_TRX_ManualPayment' style='width: 150px;'></select>",
                     overflow: "never"
                 },
                 {
                     type: "button", text: "<span class=\"fa fa-plus-circle\"></span> Manual Payment ",
                     attributes: { "class": "btn-primary" },
                     click: function (e) {

                         $scope.LoadCM10307Page('SingleManualPayment');
                     }
                 },
        ]
, resizable: true
    };

    $scope.LoadLookUp = function () {
        $http.get('/CMS_Set_Reason/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.PaymentReasons = data;
        }).finally(function () { });

        //$http.get('/CMS_TRX_ManualPayment/GetNextPaymentTransactionNumber?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
        //    $scope.CM00307.PaymentID = data;
        //}).finally(function () { });
    };
    $scope.LoadLookUp();
    $http.get('/Sys_View/GetViewsByScreen?ScreenID=3&KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
        $scope.ViewList = data;
    }).finally(function () {
        if ($scope.ViewList.length >= 0) {
            $scope.ViewList.forEach(function (element, index) {
                if (element.IsDefault) {
                    $scope.CM00307.ViewID = element.ViewID;
                    $scope.SelectedView = element;
                    return;
                }
            });
        }
        $scope.ViewChanged();
        $scope.formatOptions = {
            filter: "contains",
            model: "SelectedView",
            dataTextField: "ViewName",
            dataValueField: "ViewID",
            dataSource: $scope.ViewList,
            value: $scope.CM00307.ViewID
        };
    });
    //------------------------Filter
    $scope.MYFilter.get = function (field) {
        var DS = {};
        if (field == 'PaymentMethodID') {
            DS.key = "PaymentMethodName";
            DS.source = $scope.Paymentmethods;
        }
        return DS;
    };

    //------------------- Start| CM10307 - Manual Payment for Single Case
    $scope.ManualPaymentReasons = [];
    $scope.CM00203 = {};
    $scope.LoadCM10307Page = function (page) {
        $scope.CM00203.CaseRef = '';
        $scope.CM00203.TRXTypeID = '';

        $scope.CM10307 = {};
        $scope.CM10307.AgentID = '';
        $scope.CM10307.PaymentMethodID = 1;
        $scope.CM10307.CurrencyCode = 'BHD';
        $scope.CM10307.DecimalDigit = 1;
        $scope.CM10307.CheckbookCode = 'Main';
        $scope.CM10307.IsMultiply = true;
        $scope.CM10307.ExchangeTableID = 'Default';
        $scope.CM10307.ExchangeRateID = 1;
        $scope.CM10307.ExchangeRate = 1;
        $scope.CM10307.TransactionDate = new Date();
        $scope.LoadCM00307Page(page);
        $scope.Clear();

       

        if ($scope.ManualPaymentReasons.length > 0) return;
        $http.get('/CMS/CMS_Set_Reason/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.ManualPaymentReasons = data;
        }).finally(function () { });
        //$scope.LoadLookUp();

        $scope.GetALLCaseRef();
       
    }

    $scope.EditCM10307 = function (PaymentID) {
        $scope.CM10307 = {};
        $scope.LoadCM00307Page('SingleManualPayment');
        $scope.Clear();
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
                    //  $scope.GridRefresh('GridCMS_TRX_ManualPayment');
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
                $scope.Cancel();
                //$scope.EditCase($scope.CM00203.CaseRef);
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

    $scope.GetCM00203FromCaseRef = function (CaseRef) {
        $http.get('/CMS_TRX_ManualPayment/GetCM00203FromCaseRef?KaizenPublicKey=' + sessionStorage.PublicKey + "&CaseRef=" + CaseRef).success(function (data) {
            $scope.CM00203 = data;
            //console.log($scope.CM00203);
        }).finally(function () {
            $scope.GridRefresh('GridCMS_TRX_ManualPayment');
        });
    }

    $scope.ManualAmountChanged = function () {

        if (parseFloat($scope.CM10307.Amount) > parseFloat($scope.CM00203.ClaimAmount) && parseFloat($scope.CM10307.Amount) >= 0) {
            $scope.CM10307.Amount = 0;
        }
    };

    $scope.CaseRefDataSource = [];
    //$scope.CaseRefDataSource = new kendo.data.DataSource({
    //    transport: {
    //        read: '/CMS_TRX_ManualPayment/GetALLCaseRef?KaizenPublicKey=' + sessionStorage.PublicKey,
            
    //    }
    //});
    $scope.GetALLCaseRef = function () {
        $http.get('/CMS_TRX_ManualPayment/GetALLCaseRef?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.CaseRefDataSource = data;
            //console.log($scope.CM00203);
        }).finally(function () {
         
        });
    }
    
   
    $scope.BindCaseRefGrid = function (data) {
        if(data){
            $scope.GetCM00203FromCaseRef(data);
        }
    }
    //------------------- End| CM10307 - Manual Payment for Single Case

    $scope.LoadCM00307 = function (PaymentID) {
        if (angular.isUndefined($scope.CM00307.PaymentID)) {
            $http.get('/CMS_TRX_ManualPayment/GetSingle?KaizenPublicKey=' + sessionStorage.PublicKey + "&PaymentID=" + PaymentID).success(function (data) {
                $scope.CM00307 = data;
                $scope.CM00307.TransactionDate = kendoparsetoDate($scope.CM00307.TransactionDate);
                $scope.CM00307.CreatedDate = kendoparsetoDate($scope.CM00307.CreatedDate);
                alert($CM00307.Total_Amount);
            }).finally(function () {
                $scope.CM00307.Status = 2;
                $scope.LoadCM00308(PaymentID);
            });
        }
    };
    $scope.LoadCM00307Page = function (ActionName) {
        removeEntityPage($scope.PagesCM00307);
        var URIPath = "/CMS/" + $scope.ToolBar.ActiveScreen.ID + "/" + ActionName + "?KaizenPublicKey=" + sessionStorage.PublicKey;
        var Page = { url: URIPath, ActionName: ActionName };
        $scope.PagesCM00307.push(Page);
    };

    $scope.UserBack = function (agent) {
        if (agent != null) {
            switch ($scope.CurrentControl) {
                case 'PaymentAgent':
                    $scope.CM00307.AgentID = agent.AgentID;
                    break;
            }
        }
    };

    $scope.AddCM00307 = function () {
        $scope.LoadCM00307Page('Create');
        $scope.Clear();
        $scope.CM00307.Status = 1;
        //$http.get('/CMS_TRX_ManualPayment/GetNextPaymentTransactionNumber?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
        //    $scope.CM00307.PaymentID = data;
        //}).finally(function () {
        //    if (angular.isDefined($scope.CM00307.CheckbookCode) && $scope.CM00307.CheckbookCode != '' && $scope.CM00307.CheckbookCode != null) {
        //        $scope.GetNextPaymentNumber();
        //    }
        //});
    };
    $scope.EditCM00307 = function (PaymentID) {
        $scope.LoadCM00307Page('Create');
        $scope.Clear();
        $scope.LoadCM00307(PaymentID);
    };
    $scope.SaveData = function () {
        $http.post('/CMS_TRX_ManualPayment/SaveDataObject', { 'CM00307': $scope.CM00307, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
            if (data.Status == true) {
                $http.post('/CMS_TRX_ManualPayment/SaveLineData', { 'CM00308': $scope.TransactionLines, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
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
        $http.post('/CMS_TRX_ManualPayment/UpdateDataObject', { 'CM00307': $scope.CM00307, 'KaizenPublicKey': sessionStorage.PublicKey })
            .success(function (data) {
                if (data.Status == true) {
                    $scope.InsertedLines = [];
                    $scope.UpdatedLines = [];
                    $scope.DeletedLines = [];
                    $scope.TransactionLines.forEach(function (element, index) {
                        if (element.status == 1) {
                            $scope.InsertedLines.push(element);
                        }
                        else if (element.status == 2) {
                            $scope.UpdatedLines.push(element);
                        }
                        else if (element.status == 3) {
                            $scope.DeletedLines.push(element);
                        }
                    })
                    if ($scope.InsertedLines.length > 0) {
                        $http.post('/CMS_TRX_ManualPayment/SaveLineData', { 'CM00308': $scope.InsertedLines, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
                            if (data.Status == true) {
                                $.bigBox({
                                    title: data.Massage,
                                    content: data.Description,
                                    color: "#739E73",
                                    timeout: 8000,
                                    icon: "fa fa-check shake animated"
                                });
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
                    if ($scope.UpdatedLines.length > 0) {
                        $http.post('/CMS_TRX_ManualPayment/UpdateLineData', { 'CM00308': $scope.UpdatedLines, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
                            if (data.Status == true) {
                                $.bigBox({
                                    title: data.Massage,
                                    content: data.Description,
                                    color: "#739E73",
                                    timeout: 8000,
                                    icon: "fa fa-check shake animated"
                                });
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
                    if ($scope.DeletedLines.length > 0) {
                        $http.post('/CMS_TRX_ManualPayment/DeleteLineData', { 'CM00308': $scope.DeletedLines, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
                            if (data.Status == true) {
                                $.bigBox({
                                    title: data.Massage,
                                    content: data.Description,
                                    color: "#739E73",
                                    timeout: 8000,
                                    icon: "fa fa-check shake animated"
                                });
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


    };
    $scope.Print = function () {
        if ($scope.CM00307.PrintType == "PDF") {
            // Convert the DOM element to a drawing using kendo.drawing.drawDOM
            kendo.drawing.drawDOM($(".widget-body"))
            .then(function (group) {
                // Render the result as a PDF file
                return kendo.drawing.exportPDF(group, {
                    paperSize: "auto",
                    margin: { left: "1cm", top: "1cm", right: "1cm", bottom: "1cm" }
                });
            })
            .done(function (data) {
                // Save the PDF file
                kendo.saveAs({
                    dataURI: data,
                    fileName: "Case Payment " + $scope.CM00307.CaseRef.trim() + ".pdf",
                    proxyURL: "@Url.Action('Pdf_Export_Save')"
                });
            });
        }
        else if ($scope.CM00307.PrintType == "Image") {
            // Convert the DOM element to a drawing using kendo.drawing.drawDOM
            kendo.drawing.drawDOM($(".widget-body"))
            .then(function (group) {
                // Render the result as a PDF file
                return kendo.drawing.exportImage(group, {
                    paperSize: "auto",
                    margin: { left: "1cm", top: "1cm", right: "1cm", bottom: "1cm" }
                });
            })
            .done(function (data) {
                // Save the PDF file
                kendo.saveAs({
                    dataURI: data,
                    fileName: "Case Payment " + $scope.CM00307.CaseRef.trim() + ".png",
                    proxyURL: "@Url.Action('Pdf_Export_Save')"
                });
            });
        }
        else if ($scope.CM00307.PrintType == "SVG") {
            // Convert the DOM element to a drawing using kendo.drawing.drawDOM
            kendo.drawing.drawDOM($(".widget-body"))
            .then(function (group) {
                // Render the result as a PDF file
                return kendo.drawing.exportSVG(group, {
                    paperSize: "auto",
                    margin: { left: "1cm", top: "1cm", right: "1cm", bottom: "1cm" }
                });
            })
            .done(function (data) {
                // Save the PDF file
                kendo.saveAs({
                    dataURI: data,
                    fileName: "Case Payment " + $scope.CM00307.CaseRef.trim() + ".svg",
                    proxyURL: "@Url.Action('Pdf_Export_Save')"
                });
            });
        }
    };
    $scope.Delete = function () {
        $.SmartMessageBox({
            title: "Delete Record",
            content: "Are you sure you want to delete this?",
            buttons: '[No][Yes]'
        }, function (ButtonPressed) {
            if (ButtonPressed === "Yes") {
                $http.post('/CMS_TRX_ManualPayment/DeleteDataObject', { 'CM00307': $scope.CM00307, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
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
        $scope.PagesCM00307 = [];

        $scope.TransactionLines = [];
        $scope.InsertedLines = [];
        $scope.UpdatedLines = [];
        $scope.DeletedLines = [];
        $scope.ManualPaymentReasons = [];
    };

    $scope.ViewChanged = function () {
        $scope.CM00307.ViewID = $scope.SelectedView.ViewID;
        $scope.MainGridURL = "/CMS/" + $scope.ToolBar.ActiveScreen.ID + "/MainGrid?ViewID=" + $scope.SelectedView.ViewID + "&KaizenPublicKey=" + sessionStorage.PublicKey;
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

    $scope.GetNextPaymentNumber = function () {
        $http.get('/CMS_TRX_ManualPayment/GetNextPaymentNumber?KaizenPublicKey=' + sessionStorage.PublicKey,
            {
                params: {
                    PaymentMethodID: $scope.CM00307.PaymentMethodID,
                    CheckbookCode: $scope.CM00307.CheckbookCode
                }
            }).success(function (data) {
                $scope.CM00307.PaymentNumber = data;
            }).finally(function () { });
    };
    $scope.OrginTransactionLines = [];
    $scope.LoadCM00308 = function (PaymentID) {
        $http.get('/CMS_TRX_ManualPayment/GetPaidCases?KaizenPublicKey=' + sessionStorage.PublicKey + "&PaymentID=" + PaymentID).success(function (data) {
            $scope.TransactionLines = data;
        }).finally(function () {
            var claim_total = 0;
            var applied_total = 0;
            $scope.TransactionLines.forEach(function (element, index) {
                element.status = 0;
                claim_total += parseFloat(element.ClaimAmount);
                applied_total += parseFloat(element.Amount);
            });
            $scope.CM00307.Total_CalimAmount = claim_total;
            $scope.CM00307.Total_Amount = applied_total;
            $scope.OrginTransactionLines = angular.copy($scope.TransactionLines);
        });
    };

    $scope.AgentBack = function (agent) {
        if (agent != null) {
            $scope.CM00307.AgentID = agent.AgentID;
            switch ($scope.CurrentControl) {
                case 'PaymentAgent':
                    $scope.CM00307.AgentID = agent.AgentID;
                    break;
                case 'SingleAgentPayment':
                    $scope.CM10307.AgentID = agent.AgentID;
                    break;

            }
        }
    };
    $scope.BankBack = function (bank) {
        if (bank != null) {
            switch($scope.CurrentControl){
                case 'ManualPayment':
                    $scope.CM00307.BankCode = bank.BankCode;
                    break;
                case 'SingleManualPayment':
                    $scope.CM10307.BankCode = bank.BankCode;
                    break;
        }

        }
    };
    $scope.CaseBack = function (caseObj) {
        if (caseObj != null) {
            switch ($scope.CurrentControl) {
                case 'PaymentAgent':
                    $scope.CM00203 = caseObj;
                    $scope.CM00308.CaseRef = caseObj.CaseRef;
                    $scope.CM00308.CurrencyCode = caseObj.CurrencyCode;
                    $scope.CM00308.ClaimAmount = caseObj.ClaimAmount;
                    break;
                case 'SingleCasePayment':
                    //$scope.CM00203 = caseObj;
                    $scope.CM10307.CaseRef = caseObj.CaseRef
                    $scope.GetCM00203FromCaseRef(caseObj.CaseRef);

                    break;
            }
        }
    };

    $scope.SaveExchangeRate = function () {
        $scope.GL00012.ExchangeTableID = $scope.CM00307.ExchangeTableID;
        $scope.GL00012.CurrencyCode = $scope.CM00307.CurrencyCode;

        $http.post('/GL_ExchangeRate/SaveData', { 'GL00012': $scope.GL00012, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
            if (data.Status == true) {
                $.bigBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#739E73",
                    timeout: 8000,
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
    $scope.ClearExchangeRate = function () {
        $scope.GL00012 = {};
    };
    $scope.OpenExchangeRateWindow = function () {
        $scope.GL00012 = {};
        $scope.ParmObject = $scope.CM00307;
        $scope.ExchangeRateWindow.center().toFront().open();
        $scope.ExchangeRateWindow.refresh({
            url: "/GL_ExchangeRate/PopUpRates?KaizenPublicKey=" + sessionStorage.PublicKey
        });
    };

    $scope.CurrencyBack = function (currency) {
        if (currency != null) {
            $scope.CM00307.DecimalDigit = currency.DecimalDigit;
            $scope.CM00307.CurrencyCode = currency.CurrencyCode;
            $scope.CM00307.ExchangeTableID = currency.ExchangeTableID;
            $scope.CM00307.IsMultiply = currency.IsMultiply;
            $scope.CM00307.ExchangeRateID = currency.ExchangeRateID;
            $scope.CM00307.ExchangeRate = currency.ExchangeRate;
            $scope.changeFormat(currency.CurrencyCode, currency.DecimalDigit, 'CMS_TRX_ManualPayment-currency');
            var kendoWindow = $("#MainDialog").data("kendoWindow");
            kendoWindow.close();
        }
    };
    $scope.ExchangeRateBack = function (rate) {
        if (rate != null) {
            $scope.CM00307.ExchangeRateID = rate.ExchangeRateID;
            $scope.CM00307.ExchangeRate = rate.ExchangeRate;
        }
    };
    $scope.ExchangeTableBack = function (table) {
        if (table != null) {
            $scope.CM00307.ExchangeTableID = table.ExchangeTableID;
            $scope.CM00307.IsMultiply = table.IsMultiply;
            var kendoWindow = $("#MainDialog").data("kendoWindow");
            kendoWindow.close();
        }
    };
    $scope.CheckbookBack = function (checkbook) {
        if (checkbook != null) {
            switch ($scope.CurrentControl) {
                case 'ManualPayment':
                    $scope.CM00307.CheckbookCode = checkbook.CheckbookCode;
                    $scope.GetNextPaymentNumber();
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
                    break;
                case 'SingleManualPayment':
                    $scope.CM10307.CheckbookCode = checkbook.CheckbookCode;
                    break;
            }
        }
    };

    $scope.IsCash = true;
    $scope.ClearPayment = function () {
        var tempPaymentID = $scope.CM00307.PaymentID;
        $scope.CM00307 = {};
        $scope.TransactionLines = [];
        $scope.CM00307.PaymentID = tempPaymentID;
        $scope.CM00307.Amount = 0;
        $scope.CM00307.Total_Amount = 0;
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
    };
    $scope.CheckPaymentMethod = function () {
        $http.get('/CMS_TRX_ManualPayment/GetNextPaymentNumber?KaizenPublicKey=' + sessionStorage.PublicKey, { params: { PaymentMethodID: $scope.CM00307.PaymentMethodID } }).success(function (data) {
            $scope.CM00307.PaymentNumber = data;
        }).finally(function () { });
        if ($scope.CM00307.PaymentMethodID == 1) {
            $scope.IsCash = true;
        }
        else {
            $scope.IsCash = false;
        }
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

});
