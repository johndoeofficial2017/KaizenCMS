app.controller('Inq_PaymentReceiptController', function ($scope, $http) {
    $scope.CM30207 = {};
    $scope.CM30207.SelectedExchangeTable = {};
    $scope.SelectedView = {};
    $scope.PagesCM30207 = [];
    $scope.PaidCases = [];
    $scope.DebtorCases = [];
    $scope.toolbarOptions = {
        items: [
                 {
                     type: "button", text: "<span class=\"fa fa-eye\"></span> Pivot Payment ",
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
                 { type: "button", spriteCssClass: "k-tool-icon k-i-refresh" },
                 {
                     type: "button", text: " Filter", spriteCssClass: "k-tool-icon k-filter",
                     click: function (e) { $scope.openFilterWindow(); }
                 },
                 { type: "separator" },
                 { template: "<label>View:</label>" },
                 {
                     template: "<select kendo-drop-down-list k-ng-model='SelectedView' k-on-change='ViewChanged()' k-options='formatOptions' id='GridViewoption_CMS_Payment' style='width: 150px;'></select>",
                     overflow: "never"
                 }
        ]
, resizable: true
    };

    $scope.LoadLookUp = function () {
        //$http.get('/CMS_Payment/GetNextPaymentTransactionNumber?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
        //    $scope.CM30207.PaymentID = data;
        //}).finally(function () { });
        $http.get('/CMS_Set_Reason/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.PaymentReasons = data;
        }).finally(function () { });
    };
    $scope.LoadLookUp();
    $http.get('/Sys_View/GetViewsByScreen?ScreenID=3&KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
        $scope.ViewList = data;
    }).finally(function () {
        if ($scope.ViewList.length >= 0) {
            $scope.SelectedView = $scope.ViewList[0];
            $scope.ViewList.forEach(function (element, index) {
                if (element.IsDefault) {
                    $scope.CM30207.ViewID = element.ViewID;
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
            value: $scope.CM30207.ViewID
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

    $scope.LoadCM30207 = function (PaymentID) {
        if (angular.isUndefined($scope.CM30207.PaymentID)) {
            $http.get('/CMS_Payment/GetSingle?KaizenPublicKey=' + sessionStorage.PublicKey + "&PaymentID=" + PaymentID).success(function (data) {
                $scope.CM30207 = data;
                $scope.CM30207.PaymentDate = kendoparsetoDate($scope.CM30207.PaymentDate);
                $scope.CM30207.CreatedDate = kendoparsetoDate($scope.CM30207.CreatedDate);
            }).finally(function () {
                $scope.CM30207.Status = 2;
                $scope.LoadDebtorCases($scope.CM30207.DebtorID);
            });
        }
    };
    $scope.LoadCM30207Page = function (ActionName) {
        removeEntityPage($scope.PagesCM30207);
        var URIPath = "/CMS/Inq_PaymentReceipt/" + ActionName + "?KaizenPublicKey=" + sessionStorage.PublicKey;
        var Page = { url: URIPath, ActionName: ActionName };
        $scope.PagesCM30207.push(Page);
    };
    $scope.EditCM30207 = function (PaymentID) {
        $scope.LoadCM30207Page('Create');
        $scope.Clear();
        $scope.LoadCM30207(PaymentID);
    };
    $scope.Clear = function () {
        $scope.CM30207 = {};
        $scope.CM30207.Status = 1;
        $scope.CM30207.AgentID = $scope.MY.AgentID;
        $scope.CM30207.PaymentMethodID = 1;
        $scope.CM30207.CurrencyCode = $scope.MY.CurrencyCode;
        $scope.CM30207.DecimalDigit = $scope.MY.DecimalDigit;
        $scope.CM30207.CurrencySymbol = $scope.MY.CurrencySymbol;
        $scope.CM30207.CheckbookCode = $scope.MY.CheckbookCode;
        $scope.CM30207.IsMultiply = $scope.MY.IsMultiply;
        $scope.CM30207.ExchangeTableID = $scope.MY.ExchangeTableID;
        $scope.CM30207.ExchangeRateID = $scope.MY.ExchangeRateID;
        $scope.CM30207.ExchangeRate = $scope.MY.ExchangeRate;
        $scope.CM30207.PaymentDate = new Date();
        $scope.IsOpenTransactionPanel = false;

    };
    $scope.Print = function () {
        if ($scope.CM30207.PrintType == "PDF") {
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
                    fileName: "Case Payment " + $scope.CM30207.CaseRef.trim() + ".pdf",
                    proxyURL: "@Url.Action('Pdf_Export_Save')"
                });
            });
        }
        else if ($scope.CM30207.PrintType == "Image") {
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
                    fileName: "Case Payment " + $scope.CM30207.CaseRef.trim() + ".png",
                    proxyURL: "@Url.Action('Pdf_Export_Save')"
                });
            });
        }
        else if ($scope.CM30207.PrintType == "SVG") {
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
                    fileName: "Case Payment " + $scope.CM30207.CaseRef.trim() + ".svg",
                    proxyURL: "@Url.Action('Pdf_Export_Save')"
                });
            });
        }
    };
    $scope.Cancel = function () {
        $scope.Clear();
        $scope.PagesCM30207 = [];

        $scope.TransactionLines = [];
        $scope.InsertedLines = [];
        $scope.UpdatedLines = [];
        $scope.DeletedLines = [];
    };

    $scope.OpenPivotGrid = function () {
        $scope.LoadCM30207Page('PivotGrid');
    };
    $scope.ViewChanged = function () {
        //alert($scope.SelectedView.ViewID);
        $scope.CM30207.ViewID = $scope.SelectedView.ViewID;
        $scope.MainGridURL = "/CMS/Inq_PaymentReceipt/MainGrid?ViewID=" + $scope.SelectedView.ViewID + "&KaizenPublicKey=" + sessionStorage.PublicKey;
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
            $scope.CM30207.Total_ClaimAmount = totalClaim;
        });
    };

    $scope.CheckPaymentMethod = function () {
        $http.get('/CMS_Payment/GetNextPaymentNumber?KaizenPublicKey=' + sessionStorage.PublicKey, { params: { PaymentMethodID: $scope.CM30207.PaymentMethodID } }).success(function (data) {
            $scope.CM30207.PaymentNumber = data;
        }).finally(function () { });

        if ($scope.CM30207.PaymentMethodID == 1) {
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
            $scope.LoadCM00204($scope.CM30207.PaymentID);
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
            $scope.CM30207.Total_ClaimAmount = totalClaim;
            $scope.CM30207.Total_Amount = total;
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
            $scope.CM30207.Total_Amount -= line.AppliedAmount;
            line.AppliedAmount = '';
        }
    };
    $scope.AmountApplied = function (line) {
        if (line.AppliedAmount != '') {
            if (parseFloat(line.ClaimAmount) < parseFloat(line.AppliedAmount)) {
                line.AppliedAmount = line.ClaimAmount;
                line.Focus = true;
            }
            $scope.CM30207.Total_Amount = $scope.CalculateAppliedAmount($scope.DebtorCases);
            if ($scope.CM30207.Total_Amount > $scope.CM30207.Amount) {
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
        $scope.CM30207.Total_Amount = $scope.CalculateAppliedAmount($scope.DebtorCases);
        if ($scope.CM30207.Total_Amount > $scope.CM30207.Amount) {
            $.smallBox({
                title: "Validation",
                content: "<i class='fa fa-clock-o'></i> <i>Cannot Save The Payment Cause Applied Amount Is More Than Payment Amount Itself !!</i>",
                color: "#3276B1",
                iconSmall: "fa fa-times fa-2x fadeInRight animated",
                timeout: 4000
            });
            $scope.CM30207.Amount = $scope.CM30207.Total_Amount;
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

});
