app.controller('CMS_PaymentController', function ($scope, $http) {
    $scope.CM00207 = {};
    $scope.CM00207.SelectedExchangeTable = {};
    $scope.SelectedView = {};
    $scope.PagesCM00207 = [];
    $scope.PaidCases = [];
    $scope.DebtorCases = [];
    $scope.CaseDocType();
    $scope.SelectedLookUp = {};
    $scope.SelectedLookUp.SelectedType = $scope.TRXTypes[0];
    $scope.SelectedLookUp.BatchApprovalTRXTYpe = $scope.TRXTypes[0];

    $scope.LoadLookUp = function () {
        //$http.get('/CMS_Set_Reason/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
        //    $scope.PaymentReasons = data;
        //}).finally(function () { });
    };
    $scope.LoadLookUp();
    $http.get('/Sys_View/GetViewsByScreen?ScreenID=3&KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
        $scope.ViewList = data;
        $scope.IsApproved = 0;
        $scope.toolbarOptions = {
            items: [
                     {
                         type: "button", text: "<span class=\"fa fa-plus-circle\"></span> New",
                         attributes: { "class": "btn-primary" },
                         click: function (e) {
                             $scope.$apply(function () {
                                 $scope.AddCM00207();
                             });
                         }
                     },
                      {
                          type: "button", text: "<span class=\"fa fa-plus-circle\"></span>Batch",
                          attributes: { "class": "btn-primary" },
                          click: function (e) {
                              $scope.$apply(function () {
                                  $scope.BatchApproval();
                              });
                          }
                      },
                     {
                         type: "button", text: "<span class=\"fa fa-eye\"></span> Pivot ",
                         attributes: { "class": "btn-primary" },
                         click: function (e) {
                             $scope.$apply(function () {
                                 $scope.OpenPivotGrid();
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
                     {
                         type: "button", text: " Filter", spriteCssClass: "k-tool-icon k-filter",
                         click: function (e) { $scope.openFilterWindow(); }
                     },
                      {
                          template: "<select kendo-drop-down-list k-ng-model='SelectedLookUp.SelectedType' k-on-change='DataSource_Changed()' k-options='CaseTypeOptions' style='width: 200px;'></select>",
                          overflow: "never"
                      },
                     {
                         template: "<select kendo-drop-down-list k-ng-model='SelectedLookUp.SelectedView' k-on-change='ViewChanged()' k-options='formatOptions' id='GridViewoption_CMS_Payment' style='width: 150px;'></select>",
                         overflow: "never"
                     },
                      {
                                  type: "buttonGroup",
                                  buttons: [
                                      {
                                          text: "<span class=\"k-icon k-i-pin\"></span>", id: "radio1k",
                                          togglable: true, group: "radio", selected: false,
                                          toggle: function (e) {
                                              $scope.$apply(function () {
                                                  $scope.IsApproved = 1;
                                                  $scope.DataSource_Changed();
                                              });
                                          }
                                      },
                                      {
                                          text: "<span class=\"k-icon k-i-clock\"></span>", id: "radio2k",
                                          togglable: true, group: "radio", selected: true,
                                          toggle: function (e) {
                                              $scope.$apply(function () {
                                                  $scope.IsApproved = 0;
                                                  $scope.DataSource_Changed();
                                              });
                                          }
                                      }
                                  ]
                              }

            ]
, resizable: true
        };
    }).finally(function () {
        if ($scope.ViewList.length >= 0) {
            $scope.SelectedLookUp.SelectedView = $scope.ViewList[0];
            $scope.ViewList.forEach(function (element, index) {
                if (element.IsDefault) {
                    $scope.SelectedLookUp.SelectedView = element;
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
            value: $scope.SelectedLookUp.SelectedView.ViewID
        };
    });
    $scope.LoadCM00207 = function (PaymentID) {
        if (angular.isUndefined($scope.CM00207.PaymentID)) {
            $http.get('/CMS_Payment/GetSingle?KaizenPublicKey=' + sessionStorage.PublicKey + "&PaymentID=" + PaymentID).success(function (data) {
                $scope.CM00207 = data;
                $scope.CM00207.PaymentDate = kendoparsetoDate($scope.CM00207.PaymentDate);
                $scope.CM00207.CreatedDate = kendoparsetoDate($scope.CM00207.CreatedDate);
            }).finally(function () {
                $scope.CM00207.Status = 2;
                $scope.LoadDebtorCases($scope.CM00207.DebtorID);
            });
        }
    };
    $scope.LoadCM00207Page = function (ActionName) {
        removeEntityPage($scope.PagesCM00207);
        var URIPath = "/CMS/CMS_Payment/" + ActionName + "?KaizenPublicKey=" + sessionStorage.PublicKey;
        var Page = { url: URIPath, ActionName: ActionName };
        $scope.PagesCM00207.push(Page);
    };
    $scope.BatchApproval = function () {
        $scope.LoadCM00207Page('BatchApproval');
        $scope.Clear();
    };
    $scope.AddCM00207 = function () {
        $scope.LoadCM00207Page('Create');
        $scope.Clear();
        $scope.CM00207.Status = 1;
        $http.get('/CMS_Payment/GetNextPaymentTransactionNumber?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.CM00207.PaymentID = data;
        }).finally(function () {
            if (angular.isDefined($scope.CM00207.CheckbookCode) && $scope.CM00207.CheckbookCode != '' && $scope.CM00207.CheckbookCode != null) {
                $scope.GetNextPaymentNumber();
            }
        });

    };
    $scope.EditCM00207 = function (PaymentID) {
        $scope.LoadCM00207Page('Create');
        $scope.Clear();
        $scope.LoadCM00207(PaymentID);
        $scope.CM10207.Status = 2;
    };
    $scope.EditCM10207 = function (PaymentID) {
        $http.get('/CMS_Payment/GetSingle?KaizenPublicKey=' + sessionStorage.PublicKey + "&PaymentID=" + PaymentID).success(function (data) {
            $scope.CM10207 = data;
            $scope.CM10207.PaymentDate = kendoparsetoDate($scope.CM10207.PaymentDate);
            $scope.CM10207.CreatedDate = kendoparsetoDate($scope.CM10207.CreatedDate);
            $scope.LoadCM00207Page('CreateSinglePayment');
        }).finally(function () {
            $scope.CM10207.Status = 2;
        });
    };
    $scope.SavePaymentSingle = function () {
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
                $scope.Cancel();
            }
        }).error(function (data, status, headers, config) { alert(data); });
    };
    $scope.ApproveSingle = function () {
        $scope.CM10207.IsApproved = true;
        $scope.SavePaymentSingle();
    }
    $scope.PostSingle = function () {
        $http.get('/CMS_Payment/PostSingle?KaizenPublicKey=' + sessionStorage.PublicKey + "&PaymentID=" + $scope.CM10207.PaymentID).success(function (data) {
            $.bigBox({
                title: data.Massage,
                content: data.Description,
                color: "#739E73",
                timeout: 3000,
                icon: "fa fa-check shake animated"
            });
            $scope.Cancel();
        }).finally(function () { });
    }
    $scope.SaveData = function () {
        $http.post('/CMS_Payment/SaveDataObject', { 'CM00207': $scope.CM00207, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
            if (data.Status == true) {
                $scope.InsertedDebtorCasesPayment = [];
                $scope.DebtorCases.forEach(function (element, index) {
                    if (element.isSelected) {
                        $scope.InsertedDebtorCasesPayment.push({
                            PaymentID: $scope.CM00207.PaymentID,
                            CaseRef: element.CaseRef,
                            ContractID: element.ContractID,
                            ClientID: element.ClientID,
                            Amount: element.AppliedAmount,
                        });
                    }
                })
                $http.post('/CMS_Payment/SaveLineData', { 'CM00204': $scope.InsertedDebtorCasesPayment, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
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
        $http.post('/CMS_Payment/UpdateDataObject', { 'CM00207': $scope.CM00207, 'KaizenPublicKey': sessionStorage.PublicKey })
            .success(function (data) {
                if (data.Status == true) {
                    $scope.InsertedDebtorCasesPayment = [];
                    $scope.UpdatedDebtorCasesPayment = [];
                    $scope.DebtorCases.forEach(function (element, index) {
                        if (element.isSelected) {
                            var found = $scope.functiontofindObjectByKeyValue($scope.PaidCases, "CaseRef", element.CaseRef);
                            if (found != null) {
                                $scope.UpdatedDebtorCasesPayment.push({
                                    PaymentID: $scope.CM00207.PaymentID,
                                    CaseRef: element.CaseRef,
                                    ContractID: element.ContractID,
                                    ClientID: element.ClientID,
                                    Amount: element.AppliedAmount,
                                });
                            }
                            else {
                                $scope.InsertedDebtorCasesPayment.push({
                                    PaymentID: $scope.CM00207.PaymentID,
                                    CaseRef: element.CaseRef,
                                    ContractID: element.ContractID,
                                    ClientID: element.ClientID,
                                    Amount: element.AppliedAmount,
                                });
                            }
                        }
                    })
                    if ($scope.InsertedDebtorCasesPayment.length > 0) {
                        $http.post('/CMS_Payment/SaveLineData', { 'CM00204': $scope.InsertedDebtorCasesPayment, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
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
                    if ($scope.UpdatedDebtorCasesPayment.length > 0) {
                        $http.post('/CMS_Payment/UpdateLineData', { 'CM00204': $scope.UpdatedDebtorCasesPayment, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
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
    $scope.PostData = function () {
        $http.post('/CMS_Payment/PostDataObject', { 'CM00207': $scope.CM00207, 'KaizenPublicKey': sessionStorage.PublicKey })
            .success(function (data) {
                if (data.Status == true) {
                    $scope.InsertedDebtorCasesPayment = [];
                    $scope.UpdatedDebtorCasesPayment = [];
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
        $scope.IsOpenTransactionPanel = false;

    };
    $scope.Print = function () {
        if ($scope.CM00207.PrintType == "PDF") {
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
                    fileName: "Case Payment " + $scope.CM00207.CaseRef.trim() + ".pdf",
                    proxyURL: "@Url.Action('Pdf_Export_Save')"
                });
            });
        }
        else if ($scope.CM00207.PrintType == "Image") {
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
                    fileName: "Case Payment " + $scope.CM00207.CaseRef.trim() + ".png",
                    proxyURL: "@Url.Action('Pdf_Export_Save')"
                });
            });
        }
        else if ($scope.CM00207.PrintType == "SVG") {
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
                    fileName: "Case Payment " + $scope.CM00207.CaseRef.trim() + ".svg",
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
                $http.post('/CMS_Payment/DeleteDataObject', { 'CM00207': $scope.CM00207, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
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
        $scope.PagesCM00207 = [];

        $scope.TransactionLines = [];
        $scope.InsertedLines = [];
        $scope.UpdatedLines = [];
        $scope.DeletedLines = [];
    };

    $scope.OpenPivotGrid = function () {
        $scope.LoadCM00207Page('PivotGrid');
    };
    $scope.DataSource_Changed = function () {
        $scope.GridRefresh('GridCMS_Payment');
    };
    $scope.ViewChanged = function () {
        //alert($scope.SelectedView.ViewID);
        //$scope.CM00207.ViewID = $scope.SelectedView.ViewID;
        $scope.MainGridURL = "/CMS/CMS_Payment/MainGrid?ViewID=" + $scope.SelectedLookUp.SelectedView.ViewID + "&KaizenPublicKey=" + sessionStorage.PublicKey;
    };
    $scope.AmountChanged = function () {
        if (parseFloat($scope.CM00207.Amount) > parseFloat($scope.CM00203.ClaimAmount) && parseFloat($scope.CM00207.Amount) >= 0) {
            $scope.CM00207.Amount = 0;
        }
    };
    $scope.GetNextPaymentNumber = function () {
        $http.get('/CMS_Payment/GetNextPaymentNumber?KaizenPublicKey=' + sessionStorage.PublicKey,
            {
                params: {
                    PaymentMethodID: $scope.CM00207.PaymentMethodID,
                    CheckbookCode: $scope.CM00207.CheckbookCode
                }
            }).success(function (data) {
                $scope.CM00207.PaymentNumber = data;
            }).finally(function () { });
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
        $scope.ParmObject = $scope.CM00207;
        $scope.ExchangeRateWindow.center().toFront().open();
        $scope.ExchangeRateWindow.refresh({
            url: "/GL_ExchangeRate/PopUpRates?KaizenPublicKey=" + sessionStorage.PublicKey
        });
    };

    $scope.CurrencyBack = function (currency) {
        if (currency != null) {
            $scope.CM00207.DecimalDigit = currency.DecimalDigit;
            $scope.CM00207.CurrencyCode = currency.CurrencyCode;
            $scope.CM00207.ExchangeTableID = currency.ExchangeTableID;
            $scope.CM00207.IsMultiply = currency.IsMultiply;
            $scope.CM00207.ExchangeRateID = currency.ExchangeRateID;
            $scope.CM00207.ExchangeRate = currency.ExchangeRate;
            $scope.changeFormat(currency.CurrencyCode, currency.DecimalDigit, 'CMS_Payment-currency');
            var kendoWindow = $("#MainDialog").data("kendoWindow");
            kendoWindow.close();
        }
    };
    $scope.ExchangeRateBack = function (rate) {
        if (rate != null) {
            $scope.CM00207.ExchangeRateID = rate.ExchangeRateID;
            $scope.CM00207.ExchangeRate = rate.ExchangeRate;
        }
    };
    $scope.ExchangeTableBack = function (table) {
        if (table != null) {
            $scope.CM00207.ExchangeTableID = table.ExchangeTableID;
            $scope.CM00207.IsMultiply = table.IsMultiply;
            var kendoWindow = $("#MainDialog").data("kendoWindow");
            kendoWindow.close();
        }
    };
    $scope.DebtorBack = function (debtor) {
        if (debtor == null) return;
        $scope.CM00207.DebtorID = debtor.DebtorID;
        $http.get('/CMS_Case/GetAllByDebtor?KaizenPublicKey=' + sessionStorage.PublicKey + "&DebtorID=" + debtor.DebtorID).success(function (data) {
            $scope.DebtorCases = data;
        }).finally(function () {
            var totalClaim = 0;
            $scope.DebtorCases.forEach(function (element, index) {
                element.status = 0;
                totalClaim += parseFloat(element.ClaimAmount);
            })
            $scope.CM00207.Total_ClaimAmount = totalClaim;
        });
    };
    $scope.CheckbookBack = function (checkbook) {
        if (checkbook != null) {
            $scope.CM00207.CheckbookCode = checkbook.CheckbookCode;
            $scope.GetNextPaymentNumber();
            if ($scope.CM00207.CurrencyCode == undefined || $scope.CM00207.CurrencyCode == '') {
                $scope.CM00207.CurrencyCode = checkbook.CurrencyCode;
                $http.get('/GL_Currency/GetSingle?KaizenPublicKey=' + sessionStorage.PublicKey,
                { params: { CurrencyCode: checkbook.CurrencyCode } }).success(function (currency) {
                    $scope.CM00207.DecimalDigit = currency.DecimalDigit;
                    $scope.CM00207.CurrencyCode = currency.CurrencyCode;
                    $scope.CM00207.CurrencySymbol = currency.CurrencySymbol;
                    $scope.CM00207.ExchangeTableID = currency.ExchangeTableID;
                    $scope.CM00207.IsMultiply = currency.IsMultiply;
                    $scope.CM00207.ExchangeRateID = currency.ExchangeRateID;
                    $scope.CM00207.ExchangeRate = currency.ExchangeRate;
                }).finally(function () { });
            }
        }
    };
    $scope.AgentBack = function (agent) {
        if (agent != null) {
            $scope.CM00207.AgentID = agent.AgentID;
        }
    };
    $scope.BankBack = function (bank) {
        if (bank != null) {
            $scope.CM00207.BankCode = bank.BankCode;
        }
    };

    $scope.IsCash = true;
    $scope.LoadCases = function (DebtorID) {
        $http.get('/CMS_Case/GetAllByDebtor?KaizenPublicKey=' + sessionStorage.PublicKey + "&DebtorID=" + DebtorID).success(function (data) {
            $scope.DebtorCases = data;
        }).finally(function () {
            var totalClaim = 0;
            $scope.DebtorCases.forEach(function (element, index) {
                element.status = 0;
                totalClaim += parseFloat(element.ClaimAmount);
            })
            $scope.CM00207.Total_ClaimAmount = totalClaim;
        });
    };

    $scope.CheckPaymentMethod = function () {
        $http.get('/CMS_Payment/GetNextPaymentNumber?KaizenPublicKey=' + sessionStorage.PublicKey, { params: { PaymentMethodID: $scope.CM00207.PaymentMethodID } }).success(function (data) {
            $scope.CM00207.PaymentNumber = data;
        }).finally(function () { });

        if ($scope.CM00207.PaymentMethodID == 1) {
            $scope.IsCash = true;
        }
        else {
            $scope.IsCash = false;
        }
    };
    $scope.LoadDebtorCases = function (DebtorID) {
        $http.get('/CMS_Case/GetAllByDebtor?KaizenPublicKey=' + sessionStorage.PublicKey + "&DebtorID=" + DebtorID).success(function (data) {
            $scope.DebtorCases = data;
        }).finally(function () {
            $scope.LoadCM00204($scope.CM00207.PaymentID);
        });
    };
    $scope.LoadCM00204 = function (PaymentID) {
        $http.get('/CMS_Payment/GetPaidCases?KaizenPublicKey=' + sessionStorage.PublicKey + "&PaymentID=" + PaymentID).success(function (data) {
            $scope.PaidCases = data;
        }).finally(function () {
            var totalClaim = 0;
            var total = 0;
            $scope.DebtorCases.forEach(function (element, index) {
                element.status = 0;
                totalClaim += parseFloat(element.ClaimAmount);
                $scope.PaidCases.forEach(function (item, indx) {
                    if (element.CaseRef == item.CaseRef) {
                        element.isSelected = true;
                        element.AppliedAmount = item.Amount;
                        total += parseFloat(element.AppliedAmount);
                    }
                })
            })
            $scope.CM00207.Total_ClaimAmount = totalClaim;
            $scope.CM00207.Total_Amount = total;
        });
    };

    /////////////////////////////////////////
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
        }
        else {
            line.AppliedAmount = 0;
            line.Focus = true;
        }
        if (line.status == 0) {
            var found = $scope.functiontofindObjectByKeyValue($scope.PaidCases, "CaseRef", line.CaseRef);
            if (found != null)
                line.status == 2;
            else
                line.status == 1;
        }
    };
    $scope.DebtorAmountChanged = function () {
        $scope.CM00207.Total_Amount = $scope.CalculateAppliedAmount($scope.DebtorCases);
        if ($scope.CM00207.Total_Amount > $scope.CM00207.Amount) {
            $.smallBox({
                title: "Validation",
                content: "<i class='fa fa-clock-o'></i> <i>Cannot Save The Payment Cause Applied Amount Is More Than Payment Amount Itself !!</i>",
                color: "#3276B1",
                iconSmall: "fa fa-times fa-2x fadeInRight animated",
                timeout: 4000
            });
            $scope.CM00207.Amount = $scope.CM00207.Total_Amount;
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

    //------------------- Batch Approval -------------------------
    $scope.ApproveBatch = function () {
        debugger;
        var items = [];
        items = $("#GridCMS_CM10207BatchApproval").data("kendoGrid").dataSource.data();

        if (items.length > 0) {
            //var approvedBatches = [];
            //approvedBatches = items.filter(function (obj) {
            //    return (obj.IsApproved === true);
            //});

            $http.post('/CMS_Payment/ApproveDataObject', {
                'CM10207List': items,
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
            }).error(function (data, status, headers, config) { alert(data); });
        }
    };
    $scope.PostBatch = function () {
        var items = [];
        items = $("#GridCMS_CM10207BatchApproval").data("kendoGrid").dataSource.data();

        if (items.length > 0) {
            //var approvedBatches = [];
            //approvedBatches = items.filter(function (obj) {
            //    return (obj.IsApproved === true);
            //});

            $http.post('/CMS_Payment/PostBatch', {
                'CM10207List': items,
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
            }).error(function (data, status, headers, config) { alert(data); });
        }
    };
   
});
