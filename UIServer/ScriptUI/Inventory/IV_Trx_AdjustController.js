app.controller('IV_Trx_AdjustController', function ($scope, $http) {
    $scope.IV00200 = {};
    $scope.IV00201 = {};
    $scope.IV00204 = {};
    $scope.IV00206 = {
        DEBITAMT: 0, CRDTAMNT: 0, ORDBTAMT: 0, ORCRDAMT: 0, AccountName: '', status: 1
    };
    $scope.MainFilter = {};
    $scope.PagesIV00200 = [];
    $scope.LoadIV00200 = function (TransactionID, TransactionTypeID) {
        if (TransactionID != null && TransactionTypeID != null) {
            $http.get('/IV_Trx_Adjust/GetSingle?KaizenPublicKey=' + sessionStorage.PublicKey,
                { params: { TransactionID: TransactionID, TransactionTypeID: TransactionTypeID } }).success(function (data) {
                    $scope.IV00200 = data;
                }).finally(function () {
                    $scope.IV00200.Status = 2;
                    $scope.IV00200.TransactionDate = kendoparsetoDate($scope.IV00200.TransactionDate);
                    $scope.IV00200.DecimalDigit = $scope.MY.DecimalDigit;
                });
        }
    };
    $scope.LoadIV00200Page = function (ActionName) {
        removeEntityPage($scope.PagesIV00200);
        var URIPath = "/Inventory/" + $scope.ToolBar.ActiveScreen.ID + "/" + ActionName + "?KaizenPublicKey=" + sessionStorage.PublicKey;
        var Page = { url: URIPath, ActionName: ActionName };
        $scope.PagesIV00200.push(Page);
    };
    $scope.LoadLookUp = function () {

        $http.get('/IV_Reasons/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.Reasons = data;
        }).finally(function () { });
        $scope.TransactionTypes = [
            { TransactionTypeID: 1, TransactionTypeName: "Increase Adjustment" },
            { TransactionTypeID: 2, TransactionTypeName: "Decrease Adjustment" },
            { TransactionTypeID: 3, TransactionTypeName: "Increase Variance" },
            { TransactionTypeID: 4, TransactionTypeName: "Decrease Variance" },
        ];
        $scope.IV_Trx_AdjusttoolbarOptions = {
            items: [
                    {
                        type: "button",
                        text: "<span class=\"fa fa-plus-circle\"></span> New Transaction",
                        attributes: { "class": "btn-primary" },
                        click: function (e) {
                            $scope.$apply(function () {
                                $scope.AddIV00200();
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

    };
    $scope.LoadLookUp();
    $scope.OnPosting = function () {
        $http.get('/IV_Trx_Adjust/PostTransaction',
            {
                params: {
                    TransactionTypeID: $scope.IV00200.TransactionTypeID,
                    TransactionID: $scope.IV00200.TransactionID
                   , KaizenPublicKey: sessionStorage.PublicKey
                }
            }).success(function (data) {
                if (data.Status == true) {
                    $.smallBox({
                        title: data.Massage,
                        content: data.Description,
                        color: "#739E73",
                        iconSmall: "fa fa-times fa-2x fadeInRight animated",
                        timeout: 5000
                    });
                    $scope.Clear();
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
            }).finally(function () { });

    };
    $scope.OnTransactionTypeID_Change = function () {
        //alert($scope.IV00200.TransactionTypeID);
        $http.get('/IV_Trx_Adjust/GetNextTransactionID', {
            params: {
                TransactionTypeID: $scope.IV00200.TransactionTypeID
               , KaizenPublicKey: sessionStorage.PublicKey
            }
        }).success(function (data) {
            if (typeof data.Status == "undefined") {
                $scope.IV00200.TransactionID = data;
            }
            else {
                $.bigBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#C46A69",
                    timeout: 4000,
                    icon: "fa fa-bolt shake animated",
                    number: "4"
                });
                $scope.IV00200.TransactionID = "";
            }
        }).finally(function () { });

    };
    $scope.AddIV00200 = function () {
        $scope.LoadIV00200Page('Create');
        $scope.Clear();
    };
    $scope.EditIV00200 = function (TransactionID, TransactionTypeID) {
        $scope.LoadIV00200Page('Create');
        $scope.Clear();
        $scope.LoadIV00200(TransactionID, TransactionTypeID);
        $scope.LoadOrderIV_Trx_AdjustLine(TransactionID, TransactionTypeID);
        $scope.IsOpenItemPanel = false;
        $scope.IV00201.status = 0;
        $scope.IV00200.Status = 2;
    };
    $scope.SaveData = function () {
        $scope.IV00200.IV00206 = $scope.AccountLines;
        $scope.InsertedLines = [];
        $scope.UpdatedLines = [];
        $scope.DeletedLines = [];
        var ActionName = 'SaveData';
        if ($scope.IV00200.Status == 2)
            ActionName = 'UpdateData';
        $http.post('/IV_Trx_Adjust/' + ActionName,
            {
                'IV00200': $scope.IV00200,
                'KaizenPublicKey': sessionStorage.PublicKey
            }).success(function (data) {
                if (data.Status == true) {
                    //
                    for (var i = 0; i < $scope.TransactionLines.length; i++) {
                        var item = $scope.TransactionLines[i];
                        $scope.IV00201 = {
                            LineID: item.LineID,
                            TransactionID: $scope.IV00200.TransactionID,
                            TransactionTypeID: $scope.IV00200.TransactionTypeID,
                            ItemID: item.ItemID,
                            ItemName: item.ItemName,
                            LotNumber: item.LotNumber,
                            UFMGroupID: item.UFMGroupID,
                            UFMID: item.UFMID,
                            SiteID: item.SiteID,
                            BinTrack: item.BinTrack,
                            ReasonID: item.ReasonID,
                            ItemDescription: item.ItemDescription,
                            ItemQuantity: item.ItemQuantity,
                            ItemUnitCost: item.ItemUnitCost
                        };
                        $scope.IV00201.IV00204 = [];
                        $scope.IV00201.IV00202 = item.IV00202;
                        alert(JSON.stringify($scope.IV00201, null, 2));
                        // $scope.Lines.push($scope.IV00201);
                        if (item.status == 1)
                            $scope.InsertedLines.push($scope.IV00201);
                        else if (item.status == 2)
                            $scope.UpdatedLines.push($scope.IV00201);
                        else if (item.status == 3)
                            $scope.DeletedLines.push($scope.IV00201);

                        for (var DUmy2015 = 0; DUmy2015 < item.IV00205.length; DUmy2015++) {
                            var element = item.IV00205[DUmy2015];
                            var isNewLine = true;
                            for (var New204 = 0; New204 < $scope.IV00201.IV00204.length; New204++) {
                                var New204Element = $scope.IV00201.IV00204[New204];
                                if (element.BinID == New204Element.BinID) {
                                    New204Element.ItemQuantity = parseFloat(element.ItemQuantity) + parseFloat(New204Element.ItemQuantity);
                                    IV00205Obj = {
                                        BinID: element.BinID,
                                        SubBinID: element.SubBinID,
                                        ItemQuantity: element.ItemQuantity
                                    };
                                    New204Element.IV00205.push(IV00205Obj);
                                    isNewLine = false;
                                    break;
                                }
                            }
                            if (isNewLine) {
                                $scope.IV00205Obj = {
                                    BinID: element.BinID,
                                    SubBinID: element.SubBinID,
                                    ItemQuantity: element.ItemQuantity
                                };
                                $scope.IV00204Obj = {
                                    BinID: element.BinID,
                                    IsBinGroup: element.IsBinGroup,
                                    ItemQuantity: element.ItemQuantity,
                                };
                                $scope.IV00204Obj.IV00205 = [];
                                $scope.IV00204Obj.IV00205.push($scope.IV00205Obj);
                                $scope.IV00201.IV00204.push($scope.IV00204Obj);
                            }
                        }
                    }
                    alert(JSON.stringify($scope.InsertedLines, null, 2));
                    //alert(JSON.stringify($scope.UpdatedLines, null, 2));
                    //alert(JSON.stringify($scope.DeletedLines, null, 2));

                    if ($scope.InsertedLines.length > 0) {
                        $http({
                            url: '/IV_Trx_Adjust/SaveLineData?KaizenPublicKey=' + sessionStorage.PublicKey,
                            method: "POST",
                            data: JSON.stringify($scope.InsertedLines),
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
                    if ($scope.UpdatedLines.length > 0) {
                        $http({
                            url: '/IV_Trx_Adjust/UpdateLineData?KaizenPublicKey=' + sessionStorage.PublicKey,
                            method: "POST",
                            data: JSON.stringify($scope.UpdatedLines),
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
                    if ($scope.DeletedLines.length > 0) {
                        $http({
                            url: '/IV_Trx_Adjust/DeleteLineData?KaizenPublicKey=' + sessionStorage.PublicKey,
                            method: "POST",
                            data: JSON.stringify($scope.DeletedLines),
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
                    //$scope.Cancel();
                    //$scope.GridRefresh();
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
        $scope.SaveData();
        return;
        $http.post('/IV_Trx_Adjust/UpdateData', { 'IV00200': $scope.IV00200, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
            if (data.Status == true) {
                for (var i = 0; i < $scope.TransactionLines.length; i++) {
                    var item = $scope.TransactionLines[i];
                    if (item.status == 1) {
                        var insert_tmp = {
                            LineID: item.LineID,
                            TransactionID: $scope.IV00200.TransactionID,
                            TransactionTypeID: $scope.IV00200.TransactionTypeID,
                            ItemID: item.ItemID,
                            ItemName: item.ItemName,
                            LotNumber: item.LotNumber,
                            UFMGroupID: item.UFMGroupID,
                            UFMID: item.UFMID,
                            SiteID: item.SiteID,
                            BinTrack: item.BinTrack,
                            ReasonID: item.ReasonID,
                            ItemDescription: item.ItemDescription,
                            ItemQuantity: item.ItemQuantity,
                            ItemUnitCost: item.ItemUnitCost,
                            IV00202: item.IV00202,
                            IV00204: item.IV00204
                        };
                        $scope.InsertedLines.push(insert_tmp);
                    }
                    else if (item.status == 2) {
                        var update_tmp = {
                            LineID: item.LineID,
                            TransactionID: $scope.IV00200.TransactionID,
                            TransactionTypeID: $scope.IV00200.TransactionTypeID,
                            ItemID: item.ItemID,
                            ItemName: item.ItemName,
                            LotNumber: item.LotNumber,
                            UFMGroupID: item.UFMGroupID,
                            UFMID: item.UFMID,
                            SiteID: item.SiteID,
                            BinTrack: item.BinTrack,
                            ReasonID: item.ReasonID,
                            ItemDescription: item.ItemDescription,
                            ItemQuantity: item.ItemQuantity,
                            ItemUnitCost: item.ItemUnitCost,
                            IV00202: item.IV00202,
                            IV00204: item.IV00204
                        };
                        $scope.UpdatedLines.push(update_tmp);
                    }
                    else if (item.status == 3) {
                        var delete_tmp = {
                            LineID: item.LineID,
                            TransactionID: $scope.IV00200.TransactionID,
                            TransactionTypeID: $scope.IV00200.TransactionTypeID,
                            ItemID: item.ItemID,
                            ItemName: item.ItemName,
                            LotNumber: item.LotNumber,
                            UFMGroupID: item.UFMGroupID,
                            UFMID: item.UFMID,
                            SiteID: item.SiteID,
                            BinTrack: item.BinTrack,
                            ReasonID: item.ReasonID,
                            ItemDescription: item.ItemDescription,
                            ItemQuantity: item.ItemQuantity,
                            ItemUnitCost: item.ItemUnitCost,
                            IV00202: item.IV00202,
                            IV00204: item.IV00204
                        };
                        $scope.DeletedLines.push(delete_tmp);
                    }
                }
                if ($scope.InsertedLines.length > 0) {
                    $http({
                        url: '/IV_Trx_Adjust/SaveLineData?KaizenPublicKey=' + sessionStorage.PublicKey,
                        method: "POST",
                        data: JSON.stringify($scope.InsertedLines),
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
                if ($scope.UpdatedLines.length > 0) {
                    $http({
                        url: '/IV_Trx_Adjust/UpdateLineData?KaizenPublicKey=' + sessionStorage.PublicKey,
                        method: "POST",
                        data: JSON.stringify($scope.UpdatedLines),
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
                if ($scope.DeletedLines.length > 0) {
                    $http({
                        url: '/IV_Trx_Adjust/DeleteLineData?KaizenPublicKey=' + sessionStorage.PublicKey,
                        method: "POST",
                        data: JSON.stringify($scope.DeletedLines),
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
        $scope.IV00200 = {
            Status: 1, TransactionDate: new Date()
        };
        $scope.IV00200.DecimalDigit = $scope.MY.DecimalDigit;

        $scope.IV00201 = { status: 0, IV00202: [], IV00204: [] };
        $scope.IV00201.ItemUnitCost = 0;
        $scope.IV00201.ExtendedCost = 0;
        $scope.IV00204 = {};
        $scope.POP10105 = {};
        var total = 0;
        $scope.IV00206 = {
            DEBITAMT: 0, CRDTAMNT: 0, ORDBTAMT: 0, ORCRDAMT: 0, AccountName: '', status: 1
        };
        $scope.IV00205 = [];
        $scope.FillSiteBins = [];
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
                $http.post('/IV_Trx_Adjust/DeleteData', { 'IV00200': $scope.IV00200, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
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
        $scope.PagesIV00200 = [];

        $scope.InsertedLines = [];
        $scope.UpdatedLines = [];
        $scope.DeletedLines = [];

        $scope.TransactionLines = [];
        $scope.FillSiteBins = [];
    };
    $scope.POPBatchBack = function (batch) {
        if (batch != null) {
            $scope.IV00200.BatchID = batch.BatchID;
        }
    };
    $scope.ItemBack = function (item) {
        if (item != null) {
            if (item.SiteID == null || item.SiteID == '') {
                $.SmartMessageBox({
                    title: "Add Item to site : " + $scope.IV00201.SiteID,
                    content: "Do you want to add this item in site :" + $scope.IV00201.SiteID + " ?",
                    buttons: '[No][Yes]'
                }, function (ButtonPressed) {
                    if (ButtonPressed === "Yes") {
                        var IV00102 = {
                            SiteID: $scope.IV00201.SiteID,
                            ItemID: item.ItemID,
                            ItemName: item.ItemName,
                            PrimaryVendor: item.PrimaryVendor,
                            ShortDescription: item.ShortDescription,
                            GenericDescription: item.GenericDescription,
                            ItemDescription: item.ItemDescription,
                            BarCode: item.BarCode,
                            BinTrack: item.BinTrack,
                            Inactive: false,
                        };
                        $http.post('/IV_Item/SaveItemSiteData', { 'IV00102': IV00102, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
                            if (data.Status == true) {
                                $.bigBox({
                                    title: data.Massage,
                                    content: data.Description,
                                    color: "#739E73",
                                    timeout: 4000,
                                    icon: "fa fa-check shake animated"
                                });
                                $scope.IV00201.status = 1;
                                $scope.IV00201.ItemID = item.ItemID;
                                $scope.IV00201.ItemName = item.ItemName;
                                $scope.IV00201.ItemDescription = item.ItemDescription;
                                $scope.IV00201.DecimalDigitQuantity = item.DecimalDigitQuantity;
                                $scope.IV00201.LotNumber = item.LotNumber;
                                $scope.IV00201.UFMGroupID = item.UFMGroupID;
                                $scope.IV00201.UFMID = item.UFMID;
                                $scope.IV00201.ItemUnitCost = item.LastUnitCost;
                                
                                $scope.IV00201.IsExpiryDate = item.IsExpiryDate;
                                if (item.LotNumber != null) {
                                    $http.get('/IV_LOT/GetByLotNumber?KaizenPublicKey=' + sessionStorage.PublicKey, { params: { LotNumber: item.LotNumber } }).success(function (data) {
                                        if (data.length > 0)
                                            $scope.IV00203 = data;
                                        else
                                            $scope.IV00203 = [];
                                    }).finally(function () { });
                                }
                                else {
                                    $scope.IV00203 = [];
                                    $scope.Fields = [];
                                    $scope.LotFields = {};
                                    $scope.LotFields.IV00203 = [];
                                    $scope.IV00201.IV00202 = [];
                                }
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
            }
            else {
                $scope.IV00201.status = 1;
                $scope.IV00201.ItemID = item.ItemID;
                $scope.IV00201.ItemName = item.ItemName;
                $scope.IV00201.ItemDescription = item.ItemDescription;
                $scope.IV00201.DecimalDigitQuantity = item.DecimalDigitQuantity;
                $scope.IV00201.LotNumber = item.LotNumber;
                $scope.IV00201.UFMGroupID = item.UFMGroupID;
                $scope.IV00201.UFMID = item.UFMID;
                $scope.IV00201.ItemUnitCost = item.LastUnitCost;
                $scope.IV00201.IsExpiryDate = item.IsExpiryDate;
                if (item.LotNumber != null) {
                    $http.get('/IV_LOT/GetByLotNumber?KaizenPublicKey=' + sessionStorage.PublicKey, { params: { LotNumber: item.LotNumber } }).success(function (data) {
                        if (data.length > 0)
                            $scope.IV00203 = data;
                        else
                            $scope.IV00203 = [];
                    }).finally(function () { });
                }
                else {
                    $scope.IV00203 = [];
                    $scope.Fields = [];
                    $scope.LotFields = {};
                    $scope.LotFields.IV00203 = [];
                    $scope.IV00201.IV00202 = [];
                }
            }

            if ($scope.IV00201.BinTrack == null && $scope.IV00201.BinTrack == false) return;
            $http.get('/INV_Site/GetBinBySiteID?KaizenPublicKey='
                + sessionStorage.PublicKey, { params: { SiteID: $scope.IV00201.SiteID } })
                .success(function (data) {
                    if (data.length > 0)
                        $scope.SiteBins = data;
                    else
                        $scope.SiteBins = [];
                }).finally(function () { });
        }
        $scope.IV00200.DecimalDigit = $scope.MY.DecimalDigit;
    };

    $scope.LotFields = {};
    $scope.LotFields.IV00203 = [];
    $scope.Fields = [];
    $scope.IV00201.IV00202 = [];
    $scope.IV00202 = {};
    $scope.GetItemFields = function () {
        var sum = 0;
        if ($scope.IV00201.IV00202 != undefined) {
            $scope.IV00201.IV00202.forEach(function (element, index) {
                sum += element.ItemQuantity;
            });
        }
        $scope.IV00201.QuantityBalanceLOT = parseFloat($scope.IV00201.ItemQuantity) - sum;
        if ($scope.IV00201.LotNumber != null) {
            $http.get('/IV_LOT/GetByLotNumber?KaizenPublicKey=' + sessionStorage.PublicKey, { params: { LotNumber: $scope.IV00201.LotNumber } })
                .success(function (data) {
                if (data.length > 0)
                    $scope.IV00203 = data;
                else
                    $scope.IV00203 = [];
            }).finally(function () { });
        }
    };
    $scope.AddLotField = function () {
        $scope.Fields = [];
        angular.copy($scope.IV00203, $scope.Fields);
        //alert($scope.IV00202.ItemQuantity);
        $scope.LotFields.ItemQuantity = parseFloat($scope.IV00202.ItemQuantity);
        $scope.LotFields.LOTLineCode = $scope.IV00202.LOTLineCode;
        $scope.LotFields.ExpiryDate = kendoparsetoDate($scope.IV00202.ExpiryDate);
        $scope.LotFields.BarCode = $scope.IV00202.BarCode;
        $scope.LotFields.status = 1;
        $scope.LotFields.IV00203 = $scope.Fields;
        $scope.IV00201.IV00202.push($scope.LotFields);

        var result = parseFloat($scope.IV00201.QuantityBalanceLOT) - parseFloat($scope.IV00202.ItemQuantity);
        $scope.IV00201.QuantityBalanceLOT = result;
        $scope.LotFields = {};
        $scope.LotFields.IV00203 = [];
        $scope.LotFields.ItemQuantity = 0;
        $scope.IV00202.ItemQuantity = 0;
        $scope.LotFields.status = 0;
        $scope.IV00203.forEach(function (element, index) {
            element.CollValue = '';
        });
    };
    $scope.RemoveRow = function (row, index) {
        if (row.status == 1) {
            $scope.IV00201.IV00202.splice(index, 1);
            var sum = 0;
            $scope.IV00201.IV00202.forEach(function (element, index) {
                sum += element.ItemQuantity;
            });
            $scope.IV00201.QuantityBalance = parseFloat($scope.IV00201.ItemQuantity) - sum;
        }
        else {
            $scope.IV00201.IV00202.splice(index, 1);
            $scope.IV00201.status = 2;
            var sum = 0;
            $scope.IV00201.IV00202.forEach(function (element, index) {
                sum += element.ItemQuantity;
            });
            $scope.IV00201.QuantityBalance = parseFloat($scope.IV00201.ItemQuantity) - sum;
        }
    };
    $scope.ShowActions = function () {
        //alert($scope.IV00201.LotNumber);
        if ($scope.IV00201.LotNumber != null && $scope.IV00201.LotNumber != '') {
            if ($scope.IV00201.QuantityBalance == 0) {
                return true;
            }
            else {
                return false;
            }
        }
        else {
            return true;
        }
    };

    $scope.UFMGroupBack = function (ufmgroup) {
        if (ufmgroup != null) {
            $scope.IV00201.UFMGroupID = ufmgroup.UFMGroupID;
            $scope.IV00201.DecimalDigitQuantity = ufmgroup.DecimalDigitQuantity;
        }
    };
    $scope.UFMBack = function (ufm) {
        if (ufm != null) {
            $scope.IV00201.UFMID = ufm.UFMID;
        }
    };

    $scope.SiteBack = function (site) {
        if (site != null) {
            switch ($scope.CurrentControl) {
                case 'Main':
                    $scope.IV00200.SiteID = site.SiteID;
                    $scope.IV00200.BinTrack = site.BinTrack;
                    break;
                case 'Line':
                    $scope.IV00201.SiteID = site.SiteID;
                    $scope.IV00201.BinTrack = site.BinTrack;
                    break;
            }
            if (site.BinTrack != null && site.BinTrack == true) {
                $http.get('/INV_Site/GetBinBySiteID?KaizenPublicKey=' + sessionStorage.PublicKey, { params: { SiteID: site.SiteID } }).success(function (data) {
                    if (data.length > 0)
                        $scope.SiteBins = data;
                    else
                        $scope.SiteBins = [];
                }).finally(function () { });
            }
        }
    };
    $scope.LotLineBack = function (lot_line) {
        if (lot_line != null) {
            $scope.IV00201.LOTLineCode = lot_line.LOTLineCode;
        }
    };

    $scope.CalculatePrice = function () {
        if ($scope.IV00201.ItemUnitCost != "" && $scope.IV00201.ItemQuantity != "") {
            $scope.IV00201.ExtendedCost = $scope.IV00201.ItemUnitCost * $scope.IV00201.ItemQuantity;
        }
    };

    $scope.LoadOrderIV_Trx_AdjustLine = function (TransactionID, TransactionTypeID) {
        $http.get('/IV_Trx_Adjust/GetLinesByTransactionID?KaizenPublicKey=' + sessionStorage.PublicKey, { params: { TransactionID: TransactionID, TransactionTypeID: TransactionTypeID } }).success(function (data) {
            if (data.length > 0) {
                $scope.TransactionLines = data;
            }
        }).finally(function () {

            $http.get('/IV_Trx_Adjust/GetLinesBin?KaizenPublicKey=' + sessionStorage.PublicKey,
             {
                 params: {
                     TransactionID: TransactionID,
                     TransactionTypeID: TransactionTypeID
                 }
             })
                .success(function (data) {
                    if (data.length > 0) {
                        $scope.TransactionLines.forEach(function (element, index) {
                            element.IV00205 = [];
                            for (var i = 0; i < data.length; i++) {
                                var elementTemp205 = data[i];
                                if (element.LineID == elementTemp205.LineID) {
                                    element.IV00205.push(elementTemp205);
                                }
                            }
                        });
                    }
                }).finally(function () { });
            //----------------------------------------
            $scope.CalculateTotal();
        });
    };
    $scope.CalculateTotal = function () {
        $scope.IV00200.DOCAMNT = 0;
        if ($scope.TransactionLines.length == 0) {
            $scope.IV00200.DOCAMNT = 0;
            return;
        }
        for (var i = 0; i < $scope.TransactionLines.length; i++) {
            var line = $scope.TransactionLines[i];
            line.ExtendedCost = parseFloat(line.ItemUnitCost) * parseFloat(line.ItemQuantity);
            $scope.IV00200.DOCAMNT = parseFloat($scope.IV00200.DOCAMNT) + parseFloat(line.ExtendedCost);

            if (line.PhotoExtension == null)
                line.IV00100link = '/Photo/ItemPhoto/ItemID.jpg';
            else
                line.IV00100link = '/Photo/ItemPhoto/' + kaizenTrim(line.ItemID) + '.' + kaizenTrim(element.PhotoExtension);
        }
    };

    ///////////////////////////////////////////
    $scope.InsertedLines = [];
    $scope.UpdatedLines = [];
    $scope.DeletedLines = [];

    $scope.TransactionLines = [];
    $scope.IV00205 = [];
    var index;
    $scope.IsOpenItemPanel = false;
    $scope.OpenItemPanel = function (isOpen) {
        $scope.IsOpenItemPanel = isOpen;
        $scope.IV00201 = {
            status: 0, IV00202: [],
            ReasonID: $scope.IV00200.ReasonID,
            SiteID: $scope.IV00200.SiteID,
            BinTrack: $scope.IV00200.BinTrack,
            ItemQuantity: 0, ItemUnitCost: 0, ExtendedCost: 0
        };
        $scope.IV00205 = [];
    };
    $scope.AddNewLine = function () {
        $scope.IV00201.IV00205 = $scope.IV00205;
        $scope.TransactionLines.push($scope.IV00201);
        alert(JSON.stringify($scope.TransactionLines, null, 2));
        $scope.CalculateTotal();
        $scope.IV00201 = {};
        $scope.IV00201.status = 0;
        $scope.IsOpenItemPanel = false;
        $scope.IV00205 = [];
    };
    $scope.EditLine = function (line) {
        index = $scope.TransactionLines.indexOf(line);
        $scope.IV00201 = line;
        $scope.IV00205 = $scope.IV00201.IV00205;
        $scope.IV00202 = $scope.IV00201.IV00202;
        //if (line.LotNumber != null) {
        //    $http.get('/IV_LOT/GetByLotNumber?KaizenPublicKey=' + sessionStorage.PublicKey, { params: { LotNumber: line.LotNumber } }).success(function (data) {
        //        if (data.length > 0)
        //            $scope.IV00203 = data;
        //        else
        //            $scope.IV00203 = [];
        //    }).finally(function () { $scope.GetItemFields });
        //}

        if ($scope.IV00201.status == 0 || angular.isUndefined($scope.IV00201.status)) {
            $scope.IV00201.status = 2;
        }
        $scope.IsOpenItemPanel = true;
        $scope.CalculatePrice();


        if ($scope.IV00201.BinTrack == null && $scope.IV00201.BinTrack == false) return;

        $http.get('/INV_Site/GetBinBySiteID?KaizenPublicKey='
            + sessionStorage.PublicKey, { params: { SiteID: $scope.IV00201.SiteID } })
            .success(function (data) {
                if (data.length > 0)
                    $scope.SiteBins = data;
                else
                    $scope.SiteBins = [];
            }).finally(function () { });
    };
    $scope.UpdateLine = function () {
        $scope.IV00201.status = 2;
        $scope.IV00201.IV00204 = $scope.FillSiteBins;
        $scope.TransactionLines.splice(index, 1, $scope.IV00201);
        $scope.IV00201 = {};
        $scope.IV00201.status = 0;
        $scope.IsOpenItemPanel = false;
        $scope.CalculateTotal();
        $scope.FillSiteBins = [];
    };
    $scope.RemoveLine = function (line, index) {
        if (line.status == 1)
            $scope.TransactionLines.splice(index, 1);
        else
            line.status = 3;
    };

    //---------- Site Bins

    $scope.LoadLineBins = function () {
        $scope.IV00204.QuantityBalance = parseFloat($scope.IV00201.ItemQuantity);
        for (var i = 0; i < $scope.IV00205.length; i++) {
            var element = $scope.IV00205[i];
            $scope.IV00204.QuantityBalance = parseFloat($scope.IV00204.QuantityBalance) - parseFloat(element.ItemQuantity);
        }
    };
    $scope.AddLineBin = function () {
        var IsNew = true;
        $scope.IV00204.QuantityBalance = parseFloat($scope.IV00201.ItemQuantity);
        for (var i = 0; i < $scope.IV00205.length; i++) {
            var element = $scope.IV00205[i];
            if (element.BinID == $scope.IV00204.BinID && element.SubBinID == $scope.IV00204.SubBinID) {
                IsNew = false;
                element.ItemQuantity = parseFloat($scope.IV00204.ItemQuantity);
            }
            $scope.IV00204.QuantityBalance = parseFloat($scope.IV00204.QuantityBalance) - parseFloat(element.ItemQuantity);
        }
        if (IsNew) {
            var IV00205Obj = {
                BinID: $scope.IV00204.BinID,
                SubBinID: $scope.IV00204.SubBinID,
                IsBinGroup: $scope.IV00204.IsBinGroup,
                ItemQuantity: $scope.IV00204.ItemQuantity
            };
            $scope.IV00205.push(IV00205Obj);
            var balance = parseFloat($scope.IV00204.QuantityBalance) - parseFloat($scope.IV00204.ItemQuantity);
            $scope.IV00204 = {};
            $scope.IV00204.QuantityBalance = balance;
        } else {
            $scope.IV00204.ItemQuantity = 0;
            $scope.IV00204.BinID = '';
            $scope.IV00204.SubBinID = '';

        }
    };
    $scope.RemoveLineBin = function (line, index) {
        $scope.IV00205.splice(index, 1);
        $scope.IV00204.QuantityBalance = parseFloat($scope.IV00201.ItemQuantity);
        for (var i = 0; i < $scope.IV00205.length; i++) {
            var element = $scope.IV00205[i];
            $scope.IV00204.QuantityBalance = parseFloat($scope.IV00204.QuantityBalance) - parseFloat(element.ItemQuantity);
        }
    };
    $scope.BinChanged = function () {
        //alert($scope.IV00204.BinID);
        var found = $scope.functiontofindObjectByKeyValue($scope.SiteBins, "BinID", $scope.IV00204.BinID);
        $scope.IV00204.IsBinGroup = found.IsBinGroup;
        if ($scope.IV00204.IsBinGroup) {
            $http.get('/INV_Site/GetSubBinByBinID?KaizenPublicKey=' + sessionStorage.PublicKey + "&BinID=" + $scope.IV00204.BinID)
                .success(function (data) {
                    $scope.SiteSubBins = data;
                }).finally(function () { });
        }
        $scope.IV00204.QuantityBalance = parseFloat($scope.IV00201.ItemQuantity);
    };
    $scope.SubBinChanged = function () {
        $scope.IV00204.QuantityBalance = parseFloat($scope.IV00201.ItemQuantity);
        for (var i = 0; i < $scope.IV00205.length; i++) {
            var element = $scope.IV00205[i];
            if (element.BinID == $scope.IV00204.BinID && element.SubBinID == $scope.IV00204.SubBinID) {
                $scope.IV00204.ItemQuantity = element.ItemQuantity;
                //element.ItemQuantity = 0;
            } else {
                $scope.IV00204.ItemQuantity = 0;
                $scope.IV00204.QuantityBalance = parseFloat($scope.IV00204.QuantityBalance) - parseFloat(element.ItemQuantity);
            }
        }
    };

    ////// Launch Accounts
    $scope.AccountLines = [];
    $scope.IV00206.Total_Debit = 0;
    $scope.IV00206.Total_Credit = 0;
    $scope.LoadLineDistribution = function (TransactionID, TransactionTypeID) {
        $http.get('/IV_Trx_Adjust/GetDistributionByTransaction?KaizenPublicKey=' + sessionStorage.PublicKey, { params: { TransactionID: TransactionID, TransactionTypeID: TransactionTypeID } }).success(function (data) {
            $scope.AccountLines = data;
        }).finally(function () {
            $scope.AccountLines.forEach(function (element, index) {
                element.status = 0;
            });
            CalculateTotalDistribution();
        });
    };
    $scope.AccountBack = function (account) {
        if (account != null) {
            $scope.IV00206.AccountID = account.AccountID;
            $scope.IV00206.ACTNUMBR = account.ACTNUMBR;
            $scope.IV00206.AccountName = account.AccountName;
        }
    };
    $scope.GLSourceTableBack = function (source) {
        if (source != null) {
            $scope.IV00206.SourceID = source.SourceID;
            $scope.IV00206.SourceName = source.SourceName;
        }
    };

    $scope.CalculateTotalDistribution = function () {
        var credit_total = 0;
        var debit_total = 0;
        for (var i = 0; i < $scope.AccountLines.length; i++) {
            debit_total += parseFloat($scope.AccountLines[i].DEBITAMT);
            credit_total += parseFloat($scope.AccountLines[i].CRDTAMNT);
        }
        $scope.IV00206.Total_Debit = debit_total;
        $scope.IV00206.Total_Credit = credit_total;
    };
    $scope.DisableCredit = function (line) {
        if (angular.isDefined(line.DEBITAMT) && parseFloat(line.DEBITAMT) != 0) {
            line.CreditDisabled = true;
            if ($scope.IV00206.IsMultiply) {
                line.ORDBTAMT = line.DEBITAMT * $scope.IV00206.ExchangeRate;
            }
            else {
                line.ORDBTAMT = line.DEBITAMT / $scope.IV00206.ExchangeRate;
            }
        } else {
            line.CreditDisabled = false;
        }
    };
    $scope.DisableDebit = function (line) {
        if (angular.isDefined(line.CRDTAMNT) && parseFloat(line.CRDTAMNT) != 0) {
            line.DebitDisabled = true;
            if ($scope.IV00206.IsMultiply) {
                line.ORCRDAMT = line.CRDTAMNT * $scope.IV00206.ExchangeRate;
            }
            else {
                line.ORCRDAMT = line.CRDTAMNT / $scope.IV00206.ExchangeRate;
            }
        } else {
            line.DebitDisabled = false;
        }
    };
    $scope.AddNewAccount = function () {
        if ($scope.IV00206.ACTNUMBR != "" && $scope.IV00206.ACTNUMBR != null) {
            $scope.IV00206.status = 1;
            $scope.AccountLines.push($scope.IV00206);
            $scope.IV00206 = {
                DEBITAMT: 0, CRDTAMNT: 0, ORDBTAMT: 0, ORCRDAMT: 0, AccountName: '', status: 1
            };
            $scope.CalculateTotalDistribution();
        }
    };
    $scope.RemoveAccountLine = function (line, index) {
        if (line.status == 1) {
            $scope.AccountLines.splice(index, 1);
            CalculateTotalDistribution()
        }
        else {
            $scope.AccountLines.splice(index, 1);
            line.status = 3;
        }
    };


 

});