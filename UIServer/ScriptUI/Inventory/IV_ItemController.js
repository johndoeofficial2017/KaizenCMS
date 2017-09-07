app.controller('IV_ItemController', function ($scope, $http) {
    $http.get('/Sys_View/GetViewsByScreen?ScreenID=1&KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
        $scope.ViewList = data;
    }).finally(function () {
        if ($scope.ViewList.length > 0) {
            $scope.IV00100.ViewID = $scope.ViewList[0].ViewID;
            $scope.SelectedView = $scope.ViewList[0];
            $scope.ViewList.forEach(function (element, index) {
                if (element.IsDefault) {
                    $scope.IV00100.ViewID = element.ViewID;
                    $scope.SelectedView = element;
                    return;
                }
            });
        }
        else {
            $.smallBox({
                title: "missing System Setup",
                content: "<i class='fa fa-clock-o'></i> <i>please contact system administrator...</i>",
                color: "#C46A69",
                iconSmall: "fa fa-times fa-2x fadeInRight animated",
                timeout: 4000
            });
        }
        $scope.IV_CO_ItemtoolbarOptions = {
            items: [
                    {
                        type: "button",
                        text: "<span class=\"fa fa-plus-circle\"></span> New Item",
                        attributes: { "class": "btn-primary" },
                        click: function (e) {
                            $scope.$apply(function () {
                                $scope.AddIV00100();
                            });
                        }
                    },
                    {
                        type: "splitButton",
                        text: "GO",
                        click: function (e) {
                            $scope.ContactInfo();
                        },
                        menuButtons: [
                            {
                                type: "button", text: "<span class=\"fa fa-user-md\"></span> Substitute", click: function (e) {
                                    $scope.$apply(function () {
                                        $scope.AddSubstituteItem();
                                    });
                                }
                            },
                            {
                                type: "button", text: "<span class=\"fa fa-building-o\"></span> Kit", click: function (e) {
                                    $scope.$apply(function () {
                                        $scope.AddItemKit();
                                    });
                                }
                            },
                            {
                                type: "button", text: "<span class=\"fa fa-file\"></span> Item Site", click: function (e) {
                                    $scope.$apply(function () {
                                        $scope.ItemSiteManagement();
                                    });
                                }
                            },
                            {
                                type: "button", text: "<span class=\"fa fa-file\"></span> Item Vendor", click: function (e) {
                                    $scope.$apply(function () {
                                        $scope.ItemVendorManagement();
                                    });
                                }
                            },
                            {
                                type: "button", text: "<span class=\"fa fa-file\"></span> Item Customer", click: function (e) {
                                    $scope.$apply(function () {
                                        $scope.ItemCustomerManagement();
                                    });
                                }
                            },
                            {
                                type: "button", text: "<span class=\"fa fa-file\"></span> Item Account", click: function (e) {
                                    $scope.$apply(function () {
                                        $scope.ItemAccountManagement();
                                    });
                                }
                            },
                            {
                                type: "button", text: "<span class=\"fa fa-file\"></span> Item Price", click: function (e) {
                                    $scope.$apply(function () {
                                        $scope.ItemPriceManagement();
                                    });
                                }
                            },
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
                     { type: "button", spriteCssClass: "k-tool-icon k-i-refresh" },
                     {
                         type: "button", text: " Filter", spriteCssClass: "k-tool-icon k-filter",
                         click: function (e) { $scope.openFilterWindow(); }
                     },
                     { type: "separator" },
                     { template: "<label>View:</label>" },
                     {
                         template: "<select kendo-drop-down-list k-ng-model='SelectedView' k-on-change='ViewChanged()' k-options='ItemViewOptions' id='GridViewoption_POP_Vendor' style='width: 150px;'></select>",
                         overflow: "never"
                     },
                     {
                         type: "button", text: " Lot View",
                         click: function (e) {
                             $scope.$apply(function () {
                                 $scope.ViewIV00140();
                             });
                         }
                     },
                     {
                         type: "button",
                         text: "<span class=\"fa fa-plus-circle\"></span> Week Price",
                         attributes: { "class": "btn-primary" },
                         click: function (e) {
                             $scope.$apply(function () {
                                 $scope.LoadIV00150Page('WeekPrice');
                             });
                         }
                     },
                     {
                         type: "button",
                         text: "<span class=\"fa fa-plus-circle\"></span> Price Minutes",
                         attributes: { "class": "btn-primary" },
                         click: function (e) {
                             $scope.$apply(function () {
                                 $scope.LoadPriceMinutes('PriceMinutes');
                             });
                         }
                     }
            ]
            , resizable: true
        };
        $scope.ItemViewOptions = {
            filter: "contains",
            model: "SelectedView",
            dataTextField: "ViewName",
            dataValueField: "ViewID",
            dataSource: $scope.ViewList,
            value: $scope.IV00100.ViewID
        };
        $scope.ViewChanged();
    });

    $scope.IV_CO_PriceMinutestoolbarOptions = {
        items: [
            {
                type: "button",
                text: "<span class=\"fa fa-arrow-left\"></span> Back",
                attributes: { "class": "btn-primary" },
                click: function (e) {
                    $scope.$apply(function () {
                        $scope.BackToIV00151();
                    });
                }
            },
                {
                    type: "button",
                    text: "<span class=\"fa fa-plus-circle\"></span> New Price Minute",
                    attributes: { "class": "btn-primary" },
                    click: function (e) {
                        $scope.$apply(function () {
                            $scope.AddIV00151();
                        });
                    }
                }

        ]
            , resizable: true
    };

    $scope.IV00100 = {};
    $scope.IV00101 = {};
    $scope.IV00106 = {};
    $scope.PagesIV00100 = [];
    $scope.PagesIV00151 = [];

    $scope.ExtraFields = [];
    $scope.LoadExtraField = function (FieldID) {
        if (FieldID == 0) return;
        // code from data base to Array of extra Fields
        $http.get('/Admin_ScreenExtendedField/GetByScreenAndField?KaizenPublicKey=' + sessionStorage.PublicKey, { params: { FieldID: FieldID, ScreenID: 3 } }).success(function (data) {
            if (data.length > 0) {
                $scope.ExtraFields.push(data);
                //alert(JSON.stringify($scope.ExtraFields));
            }
        }).finally(function () { });

    };
    $scope.LoadIV00100 = function (ItemID) {
        if (angular.isUndefined($scope.IV00100.ItemID)) {
            $http.get('/MasterItem/GetSingle?KaizenPublicKey=' + sessionStorage.PublicKey + "&ItemID=" + ItemID).success(function (data) {
                $scope.IV00100 = data;
                if (data.IV00101 != null) {
                    $scope.IV00101 = data.IV00101;
                }
                if ($scope.IV00100.PhotoExtension == null)
                    $scope.IV00100link = '/Photo/ItemPhoto/ItemID.jpg';
                else
                    $scope.IV00100link = '/Photo/ItemPhoto/' + kaizenTrim($scope.IV00100.ItemID) + '.' + kaizenTrim($scope.IV00100.PhotoExtension) + "?c=" + Math.random();
            }).finally(function () {
                $scope.IV00100.Status = 2;
                $scope.IV00100.CreatedDate = kendoparsetoDate($scope.IV00100.CreatedDate);
                $scope.IV00100.SelectedLot = $scope.functiontofindObjectByKeyValue($scope.LotNumbers, 'LotNumber', $scope.IV00100.LotNumber);

                if ($scope.IV00100.TrackTypeID == 2) {
                    $http.get('/MasterItem/GetItemTrackSerial?KaizenPublicKey=' + sessionStorage.PublicKey, { params: { ItemID: $scope.IV00100.ItemID } }).success(function (data) {
                        $scope.IV00106 = data;
                    }).finally(function () {
                        if (Object.keys($scope.IV00106).length > 0) {
                            $scope.IV00106.Status = 2;
                        }
                        else {
                            $scope.IV00106 = {
                                Status: 1,
                                ItemID: $scope.IV00100.ItemID
                            };
                        }
                    });
                }
            });
        }
    };
    $scope.LoadIV00100Page = function (ActionName) {
        removeEntityPage($scope.PagesIV00100);
        var URIPath = "/IVCommon/" + $scope.ToolBar.ActiveScreen.ID + "/" + ActionName + "?KaizenPublicKey=" + sessionStorage.PublicKey;
        var Page = { url: URIPath, ActionName: ActionName };
        $scope.PagesIV00100.push(Page);
    };

    $scope.LoadIV00151 = function (WeekDayID, SiteID, ItemID) {
        if (angular.isUndefined($scope.IV00151.ItemID)) {
            $http.get('/MasterItem/GetSingleIV00151?KaizenPublicKey=' + sessionStorage.PublicKey + "&WeekDayID=" + WeekDayID + "&ItemID=" + ItemID + "&SiteID=" + SiteID).success(function (data) {
                $scope.IV00151 = data;

                if ($scope.IV00151.StartTimeFrom) {
                    var startTime = $scope.ConvertToDate($scope.IV00151.StartTimeFrom);
                    if (startTime != null)
                        $scope.IV00151.StartTimeFrom = startTime;
                }
                if ($scope.IV00151.EndTimeTo) {
                    var endTime = $scope.ConvertToDate($scope.IV00151.EndTimeTo);
                    if (endTime != null)
                        $scope.IV00151.EndTimeTo = endTime;
                }

                $scope.SelectedLookup.SelectedWeekDay = $scope.IV00151.WeekDayID;

                if ($scope.IV00151.IV00152 && $scope.IV00151.IV00152.length > 0) {
                    $scope.IV00151.IV00152.forEach(function (ele, index) {
                        if (ele.StartTimeFrom) {
                            var startTime = $scope.ConvertToDate(ele.StartTimeFrom);
                            if (startTime != null)
                                ele.StartTimeFrom = startTime;
                        }
                        if (ele.EndTimeTo) {
                            var endTime = $scope.ConvertToDate(ele.EndTimeTo);
                            if (endTime != null)
                                ele.EndTimeTo = endTime;
                        }
                    });

                    $scope.IV00152List = $scope.IV00151.IV00152;
                }

            }).finally(function () {
                $scope.IV00151.Status = 2;
            });
        }
    };

    $scope.ConvertToDate = function (TimeStampString) {
        var start = TimeStampString.indexOf("(");
        var end = TimeStampString.indexOf(")");

        var dt;
        if (start != undefined && end != undefined) {
            dt = TimeStampString.substr(start + 1, (end - start) - 1);
            return new Date(Number(dt));
        }
        return null;
    };

    $scope.LoadIV00151Page = function (ActionName) {
        removeEntityPage($scope.PagesIV00151);
        var URIPath = "/IVCommon/" + $scope.ToolBar.ActiveScreen.ID + "/" + ActionName + "?KaizenPublicKey=" + sessionStorage.PublicKey;
        var Page = { url: URIPath, ActionName: ActionName };
        $scope.PagesIV00151.push(Page);
    };
    $scope.EditIV00151 = function (WeekDayID, SiteID, ItemID) {
        $scope.LoadIV00151Page('CreatePriceMinutes');
        $scope.Clear();
        $scope.LoadIV00151(WeekDayID, SiteID, ItemID);
    };

    $scope.LoadLookUp = function () {
        $http.get('/MasterItem/FillABCDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.ABCs = data;
        }).finally(function () { });
        $http.get('/INV_ValuationMethod/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.ValuationMethods = data;
        }).finally(function () { });
        $http.get('/INV_ItemType/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.ItemTypes = data;
        }).finally(function () { });
        $http.get('/INV_Set_ItemClass/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.ItemClasses = data;
        }).finally(function () { });
        $http.get('/IV_LOT/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.LotNumbers = data;
        }).finally(function () { });
        $http.get('/INV_PriceMethod/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.PriceMethods = data;
        }).finally(function () { });
        $http.get('/INV_PriceLevel/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.PriceLevels = data;
        }).finally(function () { });
        $http.get('/IV_Set_ItemGroup/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.ItemGroups = data;
        }).finally(function () { });
        $scope.TrackTypes = [{ 'TrackTypeID': 1, 'TrackTypeName': 'LOT' },
            { 'TrackTypeID': 2, 'TrackTypeName': 'Serial' },
            { 'TrackTypeID': 3, 'TrackTypeName': 'NONE' }];
    };
    $scope.LoadLookUp();

    $scope.AddIV00100 = function () {
        $scope.LoadIV00100Page('Create');
        $scope.Clear();
        $scope.IV00100link = '/Photo/ItemPhoto/ItemID.jpg';
    };
    $scope.AddIV00151 = function () {
        $scope.IV00151.SiteID = '';
        $scope.IV00151.ItemID = '';
        $scope.IV00151.DecimalDigit = $scope.MY.DecimalDigit;
        $scope.IV00151.CurrencyCode = $scope.MY.CurrencyCode;
        $scope.IV00151.ExchangeTableID = $scope.MY.ExchangeTableID;
        $scope.IV00151.IsMultiply = $scope.MY.IsMultiply;
        $scope.IV00151.ExchangeRateID = $scope.MY.ExchangeRateID;
        $scope.IV00151.ExchangeRate = $scope.MY.ExchangeRate;

        $scope.IV00151.Status = 1;

        $scope.LoadIV00151Page('CreatePriceMinutes');
        $scope.Clear();
    };
    $scope.AddNewCurrency = function () {
        $scope.LoadIV00100Page('AddCurrency');
        $scope.Clear();
    };
    $scope.ShowDetails = false;
    $scope.SelectedItem = {};
    $scope.ItemThapnailView = function () {
        $scope.LoadIV00100Page('ItemThapnailView')
        $scope.Clear();

    };
    $scope.ShowItemDetails = function (ItemID) {
        $scope.ShowDetails = true;
        $http.get('/MasterItem/GetSingle?KaizenPublicKey=' + sessionStorage.PublicKey + "&ItemID=" + ItemID).success(function (data) {
            $scope.SelectedItem = data;
            if ($scope.SelectedItem.PhotoExtension == null)
                $scope.SelectedItemlink = '/Photo/ItemPhoto/Item.ico';
            else
                $scope.SelectedItemlink = '/Photo/ItemPhoto/' + kaizenTrim($scope.SelectedItem.ItemID) + '.' + kaizenTrim($scope.SelectedItem.PhotoExtension);
        }).finally(function () { });
    };
    $scope.SwitchButton = function () {
        if ($scope.ShowDetails == true)
            $scope.ShowDetails = false;
        else
            $scope.ShowDetails = true;
    };
    $scope.EditIV00100 = function (ItemID) {
        $scope.LoadIV00100Page('Create');
        $scope.Clear();
        $scope.LoadIV00100(ItemID);
    };
    $scope.AddSubstituteItem = function (ItemID) {
        removeEntityPage($scope.PagesIV00100);
        $scope.LoadIV00100Page('ItemSubstitute');
        if (ItemID == null) {
            $scope.LoadIV00100(ItemID);
            $scope.LoadItemIV_SubstituteItem(ItemID);
        }
    };
    $scope.AddItemKit = function (ItemID) {
        removeEntityPage($scope.PagesIV00100);
        $scope.LoadIV00100Page('ItemKit');
        $scope.ItemKITObject.status = 0;
        if (ItemID == null) {
            $scope.LoadIV00100(ItemID);
            $scope.LoadItemIV_ItemKIT(ItemID);
        }
    };
    $scope.LotChanged = function () {
        $scope.IV00100.LotNumber = $scope.IV00100.SelectedLot.LotNumber;
        $scope.IV00100.IsExpiryDate = $scope.IV00100.SelectedLot.IsExpiryDate;
    };
    $scope.DeletePicture = function () {
        $.SmartMessageBox({
            title: "Delete Photo",
            content: "Are you sure you want to delete this photo?",
            buttons: '[No][Yes]'
        }, function (ButtonPressed) {
            if (ButtonPressed === "Yes") {
                $scope.IV00101.ItemID = $scope.IV00100.ItemID;
                $scope.IV00100.IV00106 = $scope.IV00106;
                $scope.IV00100.IV00101 = $scope.IV00101;
                $http.post('/MasterItem/DeleteItemPhoto', { 'IV00100': $scope.IV00100, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
                    if (data.Status == true) {
                        $scope.IV00100.PhotoExtension = null;
                        $scope.IV00100link = '/Photo/ItemPhoto/ItemID.jpg';
                        $.bigBox({
                            title: data.Massage,
                            content: data.Description,
                            color: "#739E73",
                            timeout: 4000,
                            icon: "fa fa-check shake animated"
                        });
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

    $scope.ViewChanged = function () {
        $scope.IV00100.ViewID = $scope.SelectedView.ViewID;
        $scope.MainGridURL = "/IVCommon/" + $scope.ToolBar.ActiveScreen.ID + "/MainGrid?ViewID=" + $scope.SelectedView.ViewID + "&KaizenPublicKey=" + sessionStorage.PublicKey;
    };
    $scope.ViewIV00140 = function () {
        $scope.LoadIV00100Page('LotView');
        $scope.Clear();
    };

    $scope.SaveDataPriceMinutes = function () {
        if ($scope.IV00151.ItemID == undefined || $scope.IV00151.ItemID == '') {
            $.bigBox({
                title: "",
                content: "Select Item",
                color: "#C46A69",
                timeout: 4000,
                icon: "fa fa-bolt swing animated"
            });
            return;
        }
        else if ($scope.IV00151.SiteID == undefined || $scope.IV00151.SiteID == '') {
            $.bigBox({
                title: "",
                content: "Select site",
                color: "#C46A69",
                timeout: 4000,
                icon: "fa fa-bolt swing animated"
            });
            return;
        }
        else if ($scope.IV00151.WeekDayID == undefined || $scope.IV00151.WeekDayID == '') {
            $.bigBox({
                title: "",
                content: "Select week day",
                color: "#C46A69",
                timeout: 4000,
                icon: "fa fa-bolt swing animated"
            });
            return;
        }

        $scope.IV00152List.forEach(function (ele, index) {
            ele.WeekDayID = $scope.IV00151.WeekDayID;
            ele.ItemID = $scope.IV00151.ItemID.trim();
            ele.SiteID = $scope.IV00151.SiteID.trim();
            ele.CurrencyCode = $scope.IV00151.CurrencyCode;
            ele.DecimalDigit = $scope.IV00151.DecimalDigit;
            ele.ExchangeRate = $scope.IV00151.ExchangeRate;
            ele.IsMultiply = $scope.IV00151.IsMultiply;
        });

        $scope.IV00151.IV00152 = $scope.IV00152List;
        $http.post('/MasterItem/SaveDataPriceMinutes', { 'IV00151': $scope.IV00151, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
            if (data.Status == true) {
                $.bigBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#739E73",
                    timeout: 4000,
                    icon: "fa fa-check shake animated"
                });
                $scope.CancelIV00151();
                $scope.GridRefresh("GridIV_ItemIV00151");
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

    $scope.DeleteDataPriceMinutes = function () {
        if ($scope.IV00151.ItemID == undefined || $scope.IV00151.ItemID == '') {
            $.bigBox({
                title: "",
                content: "Select Item",
                color: "#C46A69",
                timeout: 4000,
                icon: "fa fa-bolt swing animated"
            });
            return;
        }
        else if ($scope.IV00151.SiteID == undefined || $scope.IV00151.SiteID == '') {
            $.bigBox({
                title: "",
                content: "Select site",
                color: "#C46A69",
                timeout: 4000,
                icon: "fa fa-bolt swing animated"
            });
            return;
        }
        else if ($scope.IV00151.WeekDayID == undefined || $scope.IV00151.WeekDayID == '') {
            $.bigBox({
                title: "",
                content: "Select week day",
                color: "#C46A69",
                timeout: 4000,
                icon: "fa fa-bolt swing animated"
            });
            return;
        }

        $.SmartMessageBox({
            title: "Delete Record",
            content: "Are you sure you want to delete this?",
            buttons: '[No][Yes]'
        }, function (ButtonPressed) {
            if (ButtonPressed === "Yes") {
                $scope.IV00152List.forEach(function (ele, index) {
                    ele.WeekDayID = $scope.IV00151.WeekDayID;
                    ele.ItemID = $scope.IV00151.ItemID.trim();
                    ele.SiteID = $scope.IV00151.SiteID.trim();
                    ele.CurrencyCode = $scope.IV00151.CurrencyCode;
                    ele.DecimalDigit = $scope.IV00151.DecimalDigit;
                    ele.ExchangeRate = $scope.IV00151.ExchangeRate;
                    ele.IsMultiply = $scope.IV00151.IsMultiply;
                });

                $scope.IV00151.IV00152 = $scope.IV00152List;
                $http.post('/MasterItem/DeleteDataPriceMinutes', { 'IV00151': $scope.IV00151, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
                    if (data.Status == true) {
                        $.bigBox({
                            title: data.Massage,
                            content: data.Description,
                            color: "#739E73",
                            timeout: 4000,
                            icon: "fa fa-check shake animated"
                        });
                        $scope.CancelIV00151();
                        $scope.GridRefresh("GridIV_ItemIV00151");
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
                        title: "Error",
                        content: "Unable to delete price minutes",
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

    $scope.UpdateDataPriceMinutes = function () {
        if ($scope.IV00151.ItemID == undefined || $scope.IV00151.ItemID == '') {
            $.bigBox({
                title: "",
                content: "Select Item",
                color: "#C46A69",
                timeout: 4000,
                icon: "fa fa-bolt swing animated"
            });
            return;
        }
        else if ($scope.IV00151.SiteID == undefined || $scope.IV00151.SiteID == '') {
            $.bigBox({
                title: "",
                content: "Select site",
                color: "#C46A69",
                timeout: 4000,
                icon: "fa fa-bolt swing animated"
            });
            return;
        }
        else if ($scope.IV00151.WeekDayID == undefined || $scope.IV00151.WeekDayID == '') {
            $.bigBox({
                title: "",
                content: "Select week day",
                color: "#C46A69",
                timeout: 4000,
                icon: "fa fa-bolt swing animated"
            });
            return;
        }

        $scope.IV00152List.forEach(function (ele, index) {
            ele.WeekDayID = $scope.IV00151.WeekDayID;
            ele.ItemID = $scope.IV00151.ItemID.trim();
            ele.SiteID = $scope.IV00151.SiteID.trim();
            ele.CurrencyCode = $scope.IV00151.CurrencyCode;
            ele.DecimalDigit = $scope.IV00151.DecimalDigit;
            ele.ExchangeRate = $scope.IV00151.ExchangeRate;
            ele.IsMultiply = $scope.IV00151.IsMultiply;
        });

        $scope.IV00151.IV00152 = $scope.IV00152List;
        $http.post('/MasterItem/UpdateDataPriceMinutes', { 'IV00151': $scope.IV00151, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
            if (data.Status == true) {
                $.bigBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#739E73",
                    timeout: 4000,
                    icon: "fa fa-check shake animated"
                });
                $scope.CancelIV00151();
                $scope.GridRefresh("GridIV_ItemIV00151");
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

    $scope.SaveData = function () {
        $scope.IV00101.ItemID = $scope.IV00100.ItemID;
        $scope.IV00100.IV00106 = $scope.IV00106;
        $scope.IV00100.IV00101 = $scope.IV00101;
        if (angular.isUndefined($scope.IV00100.UFMGroupID)) {
            $.smallBox({
                title: "Missing Unit Of Measure",
                content: "<i class='fa fa-clock-o'></i> <i>please contact system administrator...</i>",
                color: "#C46A69",
                iconSmall: "fa fa-times fa-2x fadeInRight animated",
                timeout: 2000
            });
            return;
        }
        $http.post('/MasterItem/SaveData', { 'IV00100': $scope.IV00100, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
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
    $scope.UpdateData = function () {
        $scope.IV00101.ItemID = $scope.IV00100.ItemID;
        $scope.IV00100.IV00106 = $scope.IV00106;
        $scope.IV00100.IV00101 = $scope.IV00101;
        $http.post('/MasterItem/UpdateData', { 'IV00100': $scope.IV00100, 'KaizenPublicKey': sessionStorage.PublicKey, 'PhotoChanged': $scope.PhotoChanged }).success(function (data) {
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
        }).finally(function () {
            if ($scope.IV00106.Status == 1) {
                $http.post('/MasterItem/SaveTrackSerialData', { 'IV00106': $scope.IV00106, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
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
            else if ($scope.IV00106.Status == 2) {
                $http.post('/MasterItem/UpdateTrackSerialData', { 'IV00106': $scope.IV00106, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
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
            else if ($scope.IV00106.Status == 3) {
                $http.post('/MasterItem/DeleteTrackSerialData', { 'IV00106': $scope.IV00106, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
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
        $scope.IV00100 = {};
        $scope.IV00105 = {};
        $scope.IV00006 = {};
        $scope.IV00108 = {};
        $scope.IV00101 = {};
        $scope.IV00102 = { Status: 1 };
        $scope.IV00100.Status = 1;
        $scope.IV00100.IsinActive = false;
        $scope.IV00100.TrackTypeID = 3;
        $scope.IV00100.ItemTypeID = 1;
        $scope.IV00100.PriceMethodCode = 1;
        $scope.IV00100.ValuationMethodID = 1;
        $scope.IV00100link = '/Photo/ItemPhoto/ItemID.jpg';

        $scope.IV00150 = {};
        $scope.IV00150List = [];
        $scope.WeekDetails = {};
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
                $http.post('/MasterItem/DeleteData', { 'IV00100': $scope.IV00100, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
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
        $scope.PagesIV00100 = [];

        $scope.InsertedItems = [];
        $scope.UpdatedItems = [];
        $scope.DeletedItems = [];
        $scope.SubstituteItems = [];

        $scope.InsertedKitItems = [];
        $scope.UpdatedKitItems = [];
        $scope.DeletedKitItems = [];
        $scope.KitItems = [];

        $scope.InsertedItemPrices = [];
        $scope.UpdatedItemPrices = [];
        $scope.DeletedItemPrices = [];
        $scope.PriceList = [];
        min_value = 1;
    };
    $scope.BackToIV00151 = function () {
        $scope.PagesIV00100 = [];
        $scope.IV00151 = {};
    };
    $scope.CancelIV00151 = function () {
        $scope.PagesIV00151 = [];
        $scope.IV00151 = {};
        $scope.IV00152List = [];
    };

    $scope.Kaizen00003 = [];
    $scope.PhotoChanged = false; // 1= new ; 2 Edit
    $scope.OnClassChanged = function () {
        //$scope.LoadExtraField(FieldID);
        //alert($scope.IV00100.ClassID)
        $http.get('/MasterItem/GetNextItemID?KaizenPublicKey=' + sessionStorage.PublicKey, { params: { ClassID: $scope.IV00100.ClassID } })
        .success(function (data) {
            $scope.IV00100.ItemID = data;
        })
        .finally(function () {
        });
    };
    $scope.OnItemTypeIDChanged = function () {

        alert($scope.IV00100.ItemTypeID);

    };
    $scope.OnGeneralChange = function (FieldID) {
        $scope.LoadExtraField(FieldID);
    };
    $scope.TrackTypeChanged = function () {
        if ($scope.IV00100.TrackTypeID == 2) {
            $scope.IV00100.LotNumber = null;
        }
        else if ($scope.IV00100.TrackTypeID == 1) {
            if (Object.keys($scope.IV00106).length > 1) {
                $scope.IV00106.Status = 3;
            }
        }
    };

    $scope.CountryBack = function (country) {
        if (country != null) {
            $scope.IV00100.CountryID = country.CountryID;
        }
    };
    $scope.UFMGroupBack = function (ufmgroup) {
        if (ufmgroup != null) {
            if (angular.isUndefined(ufmgroup.UFMID) || ufmgroup.UFMID == null) {
                $.bigBox({
                    title: "Setup Error",
                    content: "UFM Not Configured , connect system Admin",
                    color: "#C46A69",
                    timeout: 4000,
                    icon: "fa fa-bolt swing animated"
                });
                $scope.IV00100.UFMGroupID = null;
                $scope.IV00100.DecimalDigitQuantity = null;
                $scope.IV00100.UFMID = null;
                $scope.IV00100.BaseUnit = null;
                $scope.IV00100.UFMPurchase = null;
                $scope.IV00100.BaseUnitPurchase = null;
                $scope.IV00100.UFMSale = null;
                $scope.IV00100.BaseUnitSale = null;
                return;
            }
            $scope.IV00100.UFMGroupID = ufmgroup.UFMGroupID;
            $scope.IV00100.DecimalDigitQuantity = ufmgroup.DecimalDigitQuantity;
            $scope.IV00100.UFMID = ufmgroup.UFMID;
            $scope.IV00100.BaseUnit = ufmgroup.BaseUnit;
            $scope.IV00100.UFMPurchase = ufmgroup.UFMID;
            $scope.IV00100.BaseUnitPurchase = ufmgroup.BaseUnit;
            $scope.IV00100.UFMSale = ufmgroup.UFMID;
            $scope.IV00100.BaseUnitSale = ufmgroup.BaseUnit;
        }
    };
    $scope.UFMBack = function (ufm) {
        if (ufm != null) {
            switch ($scope.CurrentControl) {
                case 'Sale':
                    $scope.IV00100.UFMSale = ufm.UFMID;
                    $scope.IV00100.BaseUnitSale = ufm.BaesUnit;
                    break;
                case 'Purchase':
                    $scope.IV00100.UFMPurchase = ufm.UFMID;
                    $scope.IV00100.BaseUnitPurchase = ufm.BaesUnit;
                    break;
                case 'ItemVendorPurchase':
                    $scope.IV00109.UFMPurchase = ufm.UFMID;
                    $scope.IV00109.BaseUnitPurchase = ufm.BaesUnit;
                    break;
                case 'Price':
                    $scope.IV00006.UFMID = ufm.UFMID;
                    break;
            }
        }
    };
    $scope.VendorBack = function (vendor) {
        if (vendor != null) {
            switch ($scope.CurrentControl) {
                case 'PrimaryVendor':
                    $scope.IV00100.PrimaryVendor = vendor.VendorID;
                    break;
                case 'SitePrimaryVendor':
                    $scope.IV00102.PrimaryVendor = vendor.VendorID;
                    break;
                case 'Vendor':
                    if ($scope.IV00100.ItemID && $scope.IV00100.ItemID != null) {
                        var grid = $("#GridPOP_VendorByItem").data("kendoGrid");
                        var dataSourceData = grid.dataSource.data();
                        var found = $scope.functiontofindObjectByKeyValue(dataSourceData, "VendorID", vendor.VendorID);
                        if (found != null) {
                            $scope.IV00109 = found;
                            $scope.IV00109.Status = 2;
                        }
                        else {
                            $scope.IV00109 = {
                                ItemID: $scope.IV00100.ItemID,
                                ItemName: $scope.IV00109.ItemName,
                                ShortDescription: $scope.IV00109.ShortDescription,
                                GenericDescription: $scope.IV00109.GenericDescription,
                                ItemDescription: $scope.IV00109.ItemDescription,
                                UFMGroupID: $scope.IV00109.UFMGroupID,
                                VendorID: vendor.VendorID,
                                VendorName: vendor.VendorName,
                                Status: 1
                            };
                        }
                    }
                    else {
                        $scope.IV00109.VendorID = vendor.VendorID;
                        $scope.IV00109.VendorName = vendor.VendorName;
                    }
                    break;
            }
        }
    };
    $scope.AccountBack = function (account) {
        if (account != null) {
            switch ($scope.CurrentControl) {
                case 'ItemMainPurchase':
                    $scope.IV00101.PurchaseID = account.AccountID;
                    $scope.IV00101.PurchaseName = account.AccountName;
                    $scope.IV00101.PurchaseNumber = account.ACTNUMBR;
                    break;
                case 'ItemMainPurchaseTradeDiscount':
                    $scope.IV00101.PurchaseTradDiscountID = account.AccountID;
                    $scope.IV00101.PurchaseTradDiscountName = account.AccountName;
                    $scope.IV00101.PurchaseTradDiscountNumber = account.ACTNUMBR;
                    break;
                case 'ItemMainPurchaseFreight':
                    $scope.IV00101.PurchaseFreightID = account.AccountID;
                    $scope.IV00101.PurchaseFreightName = account.AccountName;
                    $scope.IV00101.PurchaseFreightNumber = account.ACTNUMBR;
                    break;
                case 'ItemMainPurchaseTax':
                    $scope.IV00101.PurchaseTaxID = account.AccountID;
                    $scope.IV00101.PurchaseTaxName = account.AccountName;
                    $scope.IV00101.PurchaseTaxNumber = account.ACTNUMBR;
                    break;
                case 'ItemMainPurchaseMis':
                    $scope.IV00101.PurchaseMisID = account.AccountID;
                    $scope.IV00101.PurchaseMisName = account.AccountName;
                    $scope.IV00101.PurchaseMisNumber = account.ACTNUMBR;
                    break;
                case 'ItemSales':
                    $scope.IV00101.SalesAccount = account;
                    $scope.IV00101.SalesAcc = account.AccountID;
                    break;
                case 'ItemSalesReturn':
                    $scope.IV00101.SalesReturnAccount = account;
                    $scope.IV00101.SalesReturnAcc = account.AccountID;
                    break;
                case 'ItemMarkDown':
                    $scope.IV00101.MarkdownAccount = account;
                    $scope.IV00101.MarkdownAcc = account.AccountID;
                    break;
                case 'ItemInventory':
                    $scope.IV00101.InventoryAccount = account;
                    $scope.IV00101.InventoryAcc = account.AccountID;
                    break;
                case 'ItemInventoryReturn':
                    $scope.IV00101.InventoryReturnAccount = account;
                    $scope.IV00101.InventoryReturnAcc = account.AccountID;
                    break;
                case 'ItemInventoryOffset':
                    $scope.IV00101.InventoryOffsetAccount = account;
                    $scope.IV00101.InventoryOffsetAcc = account.AccountID;
                    break;
                case 'ItemPurchasePriceVariance':
                    $scope.IV00101.PurchasePriceVarianceAccount = account;
                    $scope.IV00101.PurchasePriceVariance = account.AccountID;
                    break;
                case 'ItemFreight':
                    $scope.IV00101.FreightAccount = account;
                    $scope.IV00101.FreightAcc = account.AccountID;
                    break;
                case 'ItemTradeDiscount':
                    $scope.IV00101.TradeDiscountAccount = account;
                    $scope.IV00101.TradeDiscountAcc = account.AccountID;
                    break;
                case 'ItemCostOfGoodsSold':
                    $scope.IV00101.CostOfGoodsSoldAccount = account;
                    $scope.IV00101.CostOfGoodsSold = account.AccountID;
                    break;
                case 'ItemTax':
                    $scope.IV00101.TaxAccount = account;
                    $scope.IV00101.TaxAcc = account.AccountID;
                    break;
                case 'CustomerSales':
                    $scope.IV00108.SalesAccount = account;
                    $scope.IV00108.SalesAcc = account.AccountID;
                    break;
                case 'CustomerSalesReturn':
                    $scope.IV00108.SalesReturnAccount = account;
                    $scope.IV00108.SalesReturnAcc = account.AccountID;
                    break;
                case 'CustomerMarkDown':
                    $scope.IV00108.MarkdownAccount = account;
                    $scope.IV00108.MarkdownAcc = account.AccountID;
                    break;
                case 'CustomerInventory':
                    $scope.IV00108.InventoryAccount = account;
                    $scope.IV00108.InventoryAcc = account.AccountID;
                    break;
                case 'CustomerInventoryReturn':
                    $scope.IV00108.InventoryReturnAccount = account;
                    $scope.IV00108.InventoryReturnAcc = account.AccountID;
                    break;
                case 'CustomerInventoryOffset':
                    $scope.IV00108.InventoryOffsetAccount = account;
                    $scope.IV00108.InventoryOffsetAcc = account.AccountID;
                    break;
                case 'CustomerFreight':
                    $scope.IV00108.FreightAccount = account;
                    $scope.IV00108.FreightAcc = account.AccountID;
                    break;
                case 'CustomerTradeDiscount':
                    $scope.IV00108.TradeDiscountAccount = account;
                    $scope.IV00108.TradeDiscountAcc = account.AccountID;
                    break;
                case 'CustomerCostOfGoodsSold':
                    $scope.IV00108.CostOfGoodsSoldAccount = account;
                    $scope.IV00108.CostOfGoodsSold = account.AccountID;
                    break;
                case 'CustomerTax':
                    $scope.IV00108.TaxAccount = account;
                    $scope.IV00108.TaxAcc = account.AccountID;
                    break;
                case 'VendorSales':
                    $scope.IV00109.SalesAccount = account;
                    $scope.IV00109.SalesAcc = account.AccountID;
                    break;
                case 'VendorSalesReturn':
                    $scope.IV00109.SalesReturnAccount = account;
                    $scope.IV00109.SalesReturnAcc = account.AccountID;
                    break;
                case 'VendorMarkDown':
                    $scope.IV00109.MarkdownAccount = account;
                    $scope.IV00109.MarkdownAcc = account.AccountID;
                    break;
                case 'VendorInventory':
                    $scope.IV00109.InventoryAccount = account;
                    $scope.IV00109.InventoryAcc = account.AccountID;
                    break;
                case 'VendorInventoryReturn':
                    $scope.IV00109.InventoryReturnAccount = account;
                    $scope.IV00109.InventoryReturnAcc = account.AccountID;
                    break;
                case 'VendorInventoryOffset':
                    $scope.IV00109.InventoryOffsetAccount = account;
                    $scope.IV00109.InventoryOffsetAcc = account.AccountID;
                    break;
                case 'VendorFreight':
                    $scope.IV00109.FreightAccount = account;
                    $scope.IV00109.FreightAcc = account.AccountID;
                    break;
                case 'VendorTradeDiscount':
                    $scope.IV00109.TradeDiscountAccount = account;
                    $scope.IV00109.TradeDiscountAcc = account.AccountID;
                    break;
                case 'VendorCostOfGoodsSold':
                    $scope.IV00109.CostOfGoodsSoldAccount = account;
                    $scope.IV00109.CostOfGoodsSold = account.AccountID;
                    break;
                case 'VendorTax':
                    $scope.IV00109.TaxAccount = account;
                    $scope.IV00109.TaxAcc = account.AccountID;
                    break;
                case 'SiteSales':
                    $scope.IV00102.SalesAccount = account;
                    $scope.IV00102.SalesAcc = account.AccountID;
                    break;
                case 'SiteSalesReturn':
                    $scope.IV00102.SalesReturnAccount = account;
                    $scope.IV00102.SalesReturnAcc = account.AccountID;
                    break;
                case 'SiteMarkDown':
                    $scope.IV00102.MarkdownAccount = account;
                    $scope.IV00102.MarkdownAcc = account.AccountID;
                    break;
                case 'SiteInventory':
                    $scope.IV00102.InventoryAccount = account;
                    $scope.IV00102.InventoryAcc = account.AccountID;
                    break;
                case 'SiteInventoryReturn':
                    $scope.IV00102.InventoryReturnAccount = account;
                    $scope.IV00102.InventoryReturnAcc = account.AccountID;
                    break;
                case 'SiteInventoryOffset':
                    $scope.IV00102.InventoryOffsetAccount = account;
                    $scope.IV00102.InventoryOffsetAcc = account.AccountID;
                    break;
                case 'SiteFreight':
                    $scope.IV00102.FreightAccount = account;
                    $scope.IV00102.FreightAcc = account.AccountID;
                    break;
                case 'SiteTradeDiscount':
                    $scope.IV00102.TradeDiscountAccount = account;
                    $scope.IV00102.TradeDiscountAcc = account.AccountID;
                    break;
                case 'SiteCostOfGoodsSold':
                    $scope.IV00102.CostOfGoodsSoldAccount = account;
                    $scope.IV00102.CostOfGoodsSold = account.AccountID;
                    break;
                case 'SiteTax':
                    $scope.IV00102.TaxAccount = account;
                    $scope.IV00102.TaxAcc = account.AccountID;
                    break;
            }
        }
    };

    // Setting Photo path extension
    $scope.SetPhotoExtension = function (PhotoPath) {
        $scope.IV00100.PhotoExtension = PhotoPath;
        $scope.IV00100link = '/Photo/ItemPhotoTemp/' + kaizenTrim(PhotoPath);
        $scope.PhotoChanged = true;
        // $scope.$apply();
    };
    $scope.RemovePhotoExtension = function (PhotoPath) {
        $scope.IV00100.PhotoExtension = "";
    };

    //-------------------------- Substitute Item
    $scope.SubstituteItemObject = {}; // -- 0 = original from database ; 1 Inserted New ; 2 Edited;3 = Deleted ; 4 File Chabged
    $scope.SubstituteItems = [];
    $scope.ItemBack = function (item) {
        if (item != null) {
            switch ($scope.CurrentControl) {
                case 'Item':
                    $scope.IV00100 = item;
                    $scope.LoadItemIV_SubstituteItem(item.ItemID);
                    break;
                case 'SubstituteItem':
                    $scope.SubstituteItemObject = item;
                    $scope.SubstituteItemObject.status = 0;
                    break;
                case 'ItemKIT':
                    $scope.ItemKITObject = item;
                    $scope.ItemKITObject.status = 0;
                    break;
                case 'Account':
                    $scope.IV00100 = item;
                    $scope.LoadItemIV_ItemAccount($scope.IV00100.ItemID);
                    break;
                case 'Customer':
                    $scope.IV00100 = item;
                    $scope.IV00108.ItemID = item.ItemID;
                    $scope.GridRefresh('GridSOP_CustomerByItem');
                    break;
                case 'Vendor':
                    $scope.IV00100 = item;
                    $scope.IV00109.ItemID = item.ItemID;
                    $scope.IV00109.ItemName = item.ItemName;
                    $scope.IV00109.ShortDescription = item.ShortDescription;
                    $scope.IV00109.GenericDescription = item.GenericDescription;
                    $scope.IV00109.ItemDescription = item.ItemDescription;
                    $scope.IV00109.UFMGroupID = item.UFMGroupID;
                    $scope.GridRefresh('GridPOP_VendorByItem');
                    break;
                case 'Price':
                    $scope.IV00100 = item;
                    $scope.IV00006.ItemID = item.ItemID;
                    $scope.IV00006.UFMGroupID = item.UFMGroupID;
                    $scope.IV00006.DecimalDigitQuantity = item.DecimalDigitQuantity;
                    $scope.GridRefresh('GridIV_PriceByItem');
                    break;
                case 'Site':
                    $scope.IV00100 = item;
                    $scope.IV00102.PrimaryVendor = item.PrimaryVendor;
                    $scope.IV00102.BarCode = item.ItemID;
                    $scope.IV00102.ShortDescription = item.ShortDescription;
                    $scope.IV00102.GenericDescription = item.GenericDescription;
                    $scope.IV00102.ItemDescription = item.ItemDescription;
                    $scope.IV00102.ItemID = item.ItemID;
                    $scope.IV00102.ItemName = item.ItemName;
                    $scope.GridRefresh('GridIV_SiteByItem');
                    break;
                case 'GroupItem':
                    $scope.IV00100.ItemParent = item.ItemID;
                    $scope.IV00100.ItemName = item.ItemName;
                    $scope.IV00100.ShortDescription = item.ShortDescription;
                    $scope.IV00100.GenericDescription = item.GenericDescription;
                    $scope.IV00100.ItemDescription = item.ItemDescription;
                    $scope.IV00100.ItemParent = item.ItemParent;
                    $scope.IV00100.BarCode = item.BarCode;
                    $scope.IV00100.IsinActive = item.IsinActive;
                    $scope.IV00100.IsHold = item.IsHold;
                    $scope.IV00100.WarrantyDays = item.WarrantyDays;
                    $scope.IV00100.ShelfLifeDays = item.ShelfLifeDays;
                    $scope.IV00100.ExpiryDays = item.ExpiryDays;
                    $scope.IV00100.UFMGroupID = item.UFMGroupID;
                    $scope.IV00100.DecimalDigitQuantity = item.DecimalDigitQuantity;
                    $scope.IV00100.UFMID = item.UFMID;
                    $scope.IV00100.BaseUnit = item.BaseUnit;
                    $scope.IV00100.PriceMethodCode = item.PriceMethodCode;
                    $scope.IV00100.ItemTypeID = item.ItemTypeID;
                    $scope.IV00100.ItemGroupID = item.ItemGroupID;
                    $scope.IV00100.ClassID = item.ClassID;
                    $scope.IV00100.ShippingWeight = item.ShippingWeight;
                    $scope.IV00100.ABCID = item.ABCID;
                    $scope.IV00100.UFMPurchase = item.UFMPurchase;
                    $scope.IV00100.BaseUnitPurchase = item.BaseUnitPurchase;
                    $scope.IV00100.UnitCost = item.UnitCost;
                    $scope.IV00100.LastUnitCost = item.LastUnitCost;
                    $scope.IV00100.PurchaseQTY = item.PurchaseQTY;
                    $scope.IV00100.UFMSale = item.UFMSale;
                    $scope.IV00100.BaseUnitSale = item.BaseUnitSale;
                    $scope.IV00100.PriceLevelCode = item.PriceLevelCode;
                    $scope.IV00100.ValuationMethodID = item.ValuationMethodID;
                    $scope.IV00100.TrackTypeID = item.TrackTypeID;
                    $scope.IV00100.LotNumber = item.LotNumber;
                    $scope.IV00100.IsExpiryDate = item.IsExpiryDate;
                    $scope.IV00100.UnitPrice = item.UnitPrice;
                    $scope.IV00100.PromotedPrice = item.PromotedPrice;
                    $scope.IV00100.LastUnitPrice = item.LastUnitPrice;
                    $scope.IV00100.SaleQTY = item.SaleQTY;
                    $scope.IV00100.PrimaryVendor = item.PrimaryVendor;
                    $scope.IV00100.CountryID = item.CountryID;
                    break;
                case 'WeekItem':
                    $scope.IV00150.ItemID = item.ItemID;
                    $scope.IV00150.ItemName = item.ItemName;
                    for (i = 0; i < $scope.WeekDayLength; i++) {
                        $scope.IV00150List[i].ItemID = item.ItemID;
                    }
                    $scope.LoadIV00150UpdateData();
                    break;
                case 'WeekItemPriceMinutes':
                    $scope.IV00151.ItemID = item.ItemID;
                    break;
            }
        }
    };
    $scope.ItemKitBack = function (ItemKit) {
        if (ItemKit != null) {
            $scope.IV00100 = ItemKit;
            $scope.LoadItemIV_ItemKIT(ItemKit.ItemID);
        }
    };

    $scope.LoadItemIV_SubstituteItem = function (ItemID) {
        if (ItemID != null) {
            $http.get('/MasterItem/GetSubstituteByItemID?KaizenPublicKey=' + sessionStorage.PublicKey, { params: { ItemID: ItemID } }).success(function (data) {
                if (data.length > 0) {
                    $scope.SubstituteItems = data;
                }
            }).finally(function () {
                $scope.SubstituteItemObject.status = 0;
            });
        }
    };

    $scope.InsertedItems = [];
    $scope.UpdatedItems = [];
    $scope.DeletedItems = [];

    $scope.AddNewItem = function () {
        $scope.SubstituteItemObject.status = 1;
        //$scope.IV00105.ItemID = $scope.IV00100.ItemID;
        $scope.SubstituteItems.push($scope.SubstituteItemObject);
        $scope.UpdateItem();
    };
    $scope.UpdateItem = function () {
        $scope.SubstituteItemObject = {};
        $scope.SubstituteItemObject.status = 0;
    };
    $scope.SaveSubstituteItem = function () {
        for (var i = 0; i < $scope.SubstituteItems.length; i++) {
            var item = $scope.SubstituteItems[i];
            if (item.status == 1) {
                var insert_tmp = {
                    ItemID: $scope.IV00100.ItemID,
                    SubstituteItem: item.ItemID,
                };
                $scope.InsertedItems.push(insert_tmp);
            }
            else if (item.status == 2) {
                var update_tmp = {
                    ItemID: $scope.IV00100.ItemID,
                    SubstituteItem: item.ItemID,
                };
                $scope.UpdatedItems.push(update_tmp);
            }
            else if (item.status == 3) {
                var delete_tmp = {
                    ItemID: $scope.IV00100.ItemID,
                    SubstituteItem: item.ItemID,
                };
                $scope.DeletedItems.push(delete_tmp);
            }
        }
        if ($scope.InsertedItems.length > 0) {
            $http({
                url: '/MasterItem/SaveItemSubstituteData?KaizenPublicKey=' + sessionStorage.PublicKey,
                method: "POST",
                data: $.param({ IV00105: $scope.InsertedItems }),
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
        if ($scope.UpdatedItems.length > 0) {
            $http({
                url: '/MasterItem/UpdateItemSubstituteData?KaizenPublicKey=' + sessionStorage.PublicKey,
                method: "POST",
                data: $.param({ IV00105: $scope.UpdatedItems }),
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
        if ($scope.DeletedItems.length > 0) {
            $http({
                url: '/MasterItem/DeleteItemSubstituteData?KaizenPublicKey=' + sessionStorage.PublicKey,
                method: "POST",
                data: $.param({ IV00105: $scope.DeletedItems }),
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
        $scope.Cancel();
    };
    $scope.Removeitem = function (item, index) {
        if ($scope.SubstituteItemObject.status == 1)
            $scope.SubstituteItems.splice(index, 1);
        else
            item.status = 3;
    };

    //-------------------------- Kit Item
    $scope.ItemKITObject = {}; // -- 0 = original from database ; 1 Inserted New ; 2 Edited;3 = Deleted ; 4 File Chabged
    $scope.KitItems = [];

    $scope.LoadItemIV_ItemKIT = function (ItemID) {
        if (ItemID != null) {
            $http.get('/MasterItem/GetKitByItemID?KaizenPublicKey=' + sessionStorage.PublicKey, { params: { ItemID: ItemID } }).success(function (data) {
                if (data.length > 0) {
                    $scope.KitItems = data;
                }
            }).finally(function () {
                $scope.ItemKITObject.status = 0;
            });
        }
    };

    $scope.InsertedKitItems = [];
    $scope.UpdatedKitItems = [];
    $scope.DeletedKitItems = [];

    $scope.AddNewKitItem = function () {
        $scope.ItemKITObject.status = 1;
        $scope.KitItems.push($scope.ItemKITObject);
        $scope.UpdateKitItem();
    };
    $scope.UpdateKitItem = function () {
        $scope.ItemKITObject = {};
        $scope.ItemKITObject.status = 0;
    };
    $scope.SaveKitItem = function () {
        for (var i = 0; i < $scope.KitItems.length; i++) {
            var item = $scope.KitItems[i];
            if (item.status == 1) {
                var insert_tmp = {
                    ItemID: $scope.IV00100.ItemID,
                    ItemKIT: item.ItemID,
                };
                $scope.InsertedKitItems.push(insert_tmp);
            }
            else if (item.status == 2) {
                var update_tmp = {
                    ItemID: $scope.IV00100.ItemID,
                    ItemKIT: item.ItemID,
                };
                $scope.UpdatedKitItems.push(update_tmp);
            }
            else if (item.status == 3) {
                var delete_tmp = {
                    ItemID: $scope.IV00100.ItemID,
                    ItemKIT: item.ItemID,
                };
                $scope.DeletedKitItems.push(delete_tmp);
            }
        }

        if ($scope.InsertedKitItems.length > 0) {
            $http({
                url: '/MasterItem/SaveItemKitData?KaizenPublicKey=' + sessionStorage.PublicKey,
                method: "POST",
                data: $.param({ IV00107: $scope.InsertedKitItems }),
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
        if ($scope.UpdatedKitItems.length > 0) {
            $http({
                url: '/MasterItem/UpdateItemKitData?KaizenPublicKey=' + sessionStorage.PublicKey,
                method: "POST",
                data: $.param({ IV00107: $scope.UpdatedKitItems }),
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
        if ($scope.DeletedKitItems.length > 0) {
            $http({
                url: '/MasterItem/DeleteItemKitData?KaizenPublicKey=' + sessionStorage.PublicKey,
                method: "POST",
                data: $.param({ IV00107: $scope.DeletedKitItems }),
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
        $scope.Cancel();
    };
    $scope.RemoveKititem = function (item, index) {
        if ($scope.ItemKITObject.status == 1)
            $scope.KitItems.splice(index, 1);
        else
            item.status = 3;
    };
    //------------------------- Item Prices
    $scope.OpenPriceList = function (ItemID) {
        removeEntityPage($scope.PagesIV00100);
        $scope.LoadIV00100Page('PriceList');
        $scope.LoadIV00100(ItemID);
        $scope.LoadItemIV_PriceLine(ItemID);
    };
    $scope.IV00006 = {}; // -- 0 = original from database ; 1 Inserted New ; 2 Edited;3 = Deleted ; 4 File Chabged
    $scope.ItemPriceManagement = function () {
        removeEntityPage($scope.PagesIV00100);
        $scope.LoadIV00100Page('ItemPrice');
        $scope.IV00006.Status = 1;
        $http.get('/INV_PriceLevel/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.PriceLevels = data;
        }).finally(function () { });
        $http.get('/INV_PriceGroup/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.PriceGroups = data;
        }).finally(function () { });
    };

    $scope.CurrencyBack = function (currency) {
        if (currency != null) {
            $scope.IV00006.CurrencyCode = currency.CurrencyCode;
            $scope.IV00006.DecimalDigit = currency.DecimalDigit;
            var kendoWindow = $("#MainDialog").data("kendoWindow");
            kendoWindow.close();
        }
    };

    $scope.SaveItemPrice = function () {
        $scope.IV00006.ItemID = $scope.IV00100.ItemID;
        $http.post('/MasterItem/SaveItemPriceData', { 'IV00006': $scope.IV00006, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
            if (data.Status == true) {
                $.bigBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#739E73",
                    timeout: 4000,
                    icon: "fa fa-check shake animated"
                });
                $scope.GridRefresh('GridIV_PriceByItem');
                $scope.IV00006 = { Status: 1 };
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
    $scope.UpdateItemPrice = function () {
        $scope.IV00006.ItemID = $scope.IV00100.ItemID;
        $http.post('/MasterItem/UpdateItemPriceData', { 'IV00006': $scope.IV00006, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
            if (data.Status == true) {
                $.bigBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#739E73",
                    timeout: 4000,
                    icon: "fa fa-check shake animated"
                });
                $scope.GridRefresh('GridIV_PriceByItem');
                $scope.IV00006 = { Status: 1 };
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
    $scope.DeleteItemPrice = function () {
        $.SmartMessageBox({
            title: "Delete Record",
            content: "Are you sure you want to delete this?",
            buttons: '[No][Yes]'
        }, function (ButtonPressed) {
            if (ButtonPressed === "Yes") {
                $http.post('/MasterItem/DeleteItemPriceData', { 'IV00006': $scope.IV00006, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
                    if (data.Status == true) {
                        $.bigBox({
                            title: data.Massage,
                            content: data.Description,
                            color: "#739E73",
                            timeout: 4000,
                            icon: "fa fa-check shake animated"
                        });
                        $scope.GridRefresh('GridIV_PriceByItem');
                        $scope.IV00006 = { Status: 1 };
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



    $scope.PriceList = [];
    $scope.launchPriceItems = function () {
        var dlg = dialogs.create('/MasterItem/PopUp?KaizenPublicKey=' + sessionStorage.PublicKey, 'IV_ItemPopUpController', {});
        dlg.result.then(function (item) {
            $scope.IV00100 = item;
            $scope.LoadItemIV_PriceLine(item.ItemID);
        });
    };
    $scope.launchPriceUFM = function () {
        var dlg = dialogs.create('/INV_Set_UFMGroup/UFMPopUp?KaizenPublicKey=' + sessionStorage.PublicKey, 'INV_UFMPopUpController', $scope.IV00100.UFMGroupID);
        dlg.result.then(function (ufm) {
            $scope.IV00006.UFMID = ufm.UFMID;
        });
    };
    var min_value = 1;
    $scope.LoadItemIV_PriceLine = function (ItemID) {

        $http.get('/IV_ItemPriceList/GetByItemID?KaizenPublicKey=' + sessionStorage.PublicKey, { params: { ItemID: ItemID } }).success(function (data) {
            if (data.length > 0) {
                $scope.PriceList = data;
                $scope.PriceList.forEach(function (element, index) {
                    if (element.QuantityTo > min_value) {
                        min_value = element.QuantityTo;
                    }
                })

                $("#slider").editRangeSlider({ bounds: { min: min_value, max: 99999 } });
            }
        }).finally(function () {
            $scope.IV00006.status = 0;
        });

    };

    $scope.InsertedItemPrices = [];
    $scope.UpdatedItemPrices = [];
    $scope.DeletedItemPrices = [];

    $scope.AddNewPrice = function () {
        $scope.IV00006.status = 1;
        $scope.IV00006.QuantityFrom = $("#slider").editRangeSlider("min");
        $scope.IV00006.QuantityTo = $("#slider").editRangeSlider("max");
        min_value = $scope.IV00006.QuantityTo;
        $scope.IV00006.ItemID = $scope.IV00100.ItemID;
        $scope.PriceList.push($scope.IV00006);
        $scope.UpdatePrice();
    };
    $scope.UpdatePrice = function () {
        $scope.IV00006 = {};
        $scope.IV00006.status = 0;
        $("#slider").editRangeSlider({ bounds: { min: min_value, max: 99999 } });
    };
    $scope.SavePriceList = function () {
        waitingDialog.show();
        for (var i = 0; i < $scope.PriceList.length; i++) {
            var price = $scope.PriceList[i];
            if (price.status == 1) {
                var insert_tmp = {
                    ItemID: $scope.IV00100.ItemID,
                    PriceLineID: price.PriceLineID,
                    PriceMethodCode: price.PriceMethodCode,
                    PriceLevelCode: price.PriceLevelCode,
                    PriceGroupID: price.PriceGroupID,
                    UFMID: price.UFMID,
                    CurrencyCode: price.CurrencyCode,
                    QuantityFrom: price.QuantityFrom,
                    QuantityTo: price.QuantityTo,
                    PriceValue: price.PriceValue
                };
                $scope.InsertedItemPrices.push(insert_tmp);
            }
            else if (price.status == 2) {
                var update_tmp = {
                    ItemID: $scope.IV00100.ItemID,
                    PriceLineID: price.PriceLineID,
                    PriceMethodCode: price.PriceMethodCode,
                    PriceLevelCode: price.PriceLevelCode,
                    PriceGroupID: price.PriceGroupID,
                    UFMID: price.UFMID,
                    CurrencyCode: price.CurrencyCode,
                    QuantityFrom: price.QuantityFrom,
                    QuantityTo: price.QuantityTo,
                    PriceValue: price.PriceValue
                };
                $scope.UpdatedItemPrices.push(update_tmp);
            }
            else if (price.status == 3) {
                var delete_tmp = {
                    ItemID: $scope.IV00100.ItemID,
                    PriceLineID: price.PriceLineID,
                    PriceMethodCode: price.PriceMethodCode,
                    PriceLevelCode: price.PriceLevelCode,
                    PriceGroupID: price.PriceGroupID,
                    UFMID: price.UFMID,
                    CurrencyCode: price.CurrencyCode,
                    QuantityFrom: price.QuantityFrom,
                    QuantityTo: price.QuantityTo,
                    PriceValue: price.PriceValue
                };
                $scope.DeletedItemPrices.push(delete_tmp);
            }
        }

        if ($scope.InsertedItemPrices.length > 0) {
            $http({
                url: '/IV_ItemPriceList/SaveData?KaizenPublicKey=' + sessionStorage.PublicKey,
                method: "POST",
                data: $.param({ IV00006: $scope.InsertedItemPrices }),
                headers: {
                    'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8'
                }
            }).success(function (data) {
            }).error(function (data, status, headers, config) { alert(); });
        }
        if ($scope.UpdatedItemPrices.length > 0) {
            $http({
                url: '/IV_ItemPriceList/UpdateData?KaizenPublicKey=' + sessionStorage.PublicKey,
                method: "POST",
                data: $.param({ IV00006: $scope.UpdatedItemPrices }),
                headers: {
                    'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8'
                }
            }).success(function (data) {

            }).error(function (data, status, headers, config) { alert(); });
        }
        if ($scope.DeletedItemPrices.length > 0) {
            $http({
                url: '/IV_ItemPriceList/DeleteData?KaizenPublicKey=' + sessionStorage.PublicKey,
                method: "POST",
                data: $.param({ IV00006: $scope.DeletedItemPrices }),
                headers: {
                    'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8'
                }
            }).success(function (data) {

            }).error(function (data, status, headers, config) { alert(); });
        }
        waitingDialog.hide();
        $scope.Cancel();
    };
    $scope.RemovePrice = function (price, index) {
        if ($scope.IV00006.status == 1)
            $scope.PriceList.splice(index, 1);
        else
            price.status = 3;
    };

    //------------------------- Item Account
    $scope.IV00101 = {};
    $scope.ItemAccountManagement = function () {
        removeEntityPage($scope.PagesIV00100);
        $scope.LoadIV00100Page('ItemAccount');
        $scope.IV00101.Status = 1;
    };
    $scope.LoadItemIV_ItemAccount = function (ItemID) {
        if (ItemID != null) {
            $http.get('/MasterItem/GetSingleItemAccount?KaizenPublicKey=' + sessionStorage.PublicKey, { params: { ItemID: ItemID } }).success(function (data) {
                if (Object.keys(data).length > 0) {
                    $scope.IV00101 = data;
                    $scope.IV00101.Status = 2;
                }
                else {
                    $scope.IV00101 = { Status: 1 };
                }
            }).finally(function () { });
        }
    };

    $scope.SaveItemAccount = function () {
        $scope.IV00101.ItemID = $scope.IV00100.ItemID;
        if ($scope.IV00101.ItemID) {
            $http.post('/MasterItem/SaveItemAccountData', { 'IV00101': $scope.IV00101, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
                if (data.Status == true) {
                    $.bigBox({
                        title: data.Massage,
                        content: data.Description,
                        color: "#739E73",
                        timeout: 4000,
                        icon: "fa fa-check shake animated"
                    });
                    $scope.GridRefresh();
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
        }
    };
    $scope.UpdateItemAccount = function () {
        $scope.IV00101.ItemID = $scope.IV00100.ItemID;
        if ($scope.IV00101.ItemID) {
            $http.post('/MasterItem/UpdateItemAccountData', { 'IV00101': $scope.IV00101, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
                if (data.Status == true) {
                    $.bigBox({
                        title: data.Massage,
                        content: data.Description,
                        color: "#739E73",
                        timeout: 4000,
                        icon: "fa fa-check shake animated"
                    });
                    $scope.GridRefresh();
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
        }
    };
    $scope.DeleteItemAccount = function () {
        $.SmartMessageBox({
            title: "Delete Record",
            content: "Are you sure you want to delete this?",
            buttons: '[No][Yes]'
        }, function (ButtonPressed) {
            if (ButtonPressed === "Yes") {
                $http.post('/MasterItem/DeleteItemAccountData', { 'IV00101': $scope.IV00101, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
                    if (data.Status == true) {
                        $.bigBox({
                            title: data.Massage,
                            content: data.Description,
                            color: "#739E73",
                            timeout: 4000,
                            icon: "fa fa-check shake animated"
                        });
                        $scope.GridRefresh();
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
    //------------------------- Item Customer
    $scope.IV00108 = {};
    $scope.ItemCustomerManagement = function () {
        removeEntityPage($scope.PagesIV00100);
        $scope.LoadIV00100Page('ItemCustomer');
        $scope.IV00108.Status = 1;
    };
    $scope.CustomerBack = function (customer) {
        if (customer != null) {
            if ($scope.IV00100.ItemID && $scope.IV00100.ItemID != null) {
                var grid = $("#GridSOP_CustomerByItem").data("kendoGrid");
                var dataSourceData = grid.dataSource.data();
                var found = $scope.functiontofindObjectByKeyValue(dataSourceData, "CUSTNMBR", customer.CUSTNMBR);
                if (found != null) {
                    $scope.IV00108 = found;
                    $scope.IV00108.Status = 2;
                }
                else {
                    $scope.IV00108 = {
                        ItemID: $scope.IV00100.ItemID,
                        CUSTNMBR: customer.CUSTNMBR,
                        CUSTNAME: customer.CUSTNAME,
                        Status: 1
                    };
                }
            }
            else {
                $scope.IV00108.CUSTNMBR = customer.CUSTNMBR;
                $scope.IV00108.CustomerName = customer.CUSTNAME;
            }
        }
    };

    $scope.SaveItemCustomer = function () {
        var grid = $("#GridSOP_CustomerByItem").data("kendoGrid");
        var dataSourceData = grid.dataSource.data();

        if ($scope.functiontofindIndexByKeyValue(dataSourceData, "CUSTNMBR", $scope.IV00108.CUSTNMBR) != null) {
            $.smallBox({
                title: "Already Allocated",
                content: "<i class='fa fa-clock-o'></i> <i>This Customer is already allocated to this Item before !!</i>",
                color: "#3276B1",
                iconSmall: "fa fa-times fa-2x fadeInRight animated",
                timeout: 4000
            });
            return;
        }
        $scope.IV00108.ItemID = $scope.IV00100.ItemID;
        $http.post('/MasterItem/SaveItemCustomerData', { 'IV00108': $scope.IV00108, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
            if (data.Status == true) {
                $.bigBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#739E73",
                    timeout: 4000,
                    icon: "fa fa-check shake animated"
                });
                $scope.GridRefresh('GridSOP_CustomerByItem');
                $scope.IV00108 = { Status: 1 };
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
    $scope.UpdateItemCustomer = function () {
        $scope.IV00108.ItemID = $scope.IV00100.ItemID;
        $http.post('/MasterItem/UpdateItemCustomerData', { 'IV00108': $scope.IV00108, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
            if (data.Status == true) {
                $.bigBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#739E73",
                    timeout: 4000,
                    icon: "fa fa-check shake animated"
                });
                $scope.GridRefresh('GridSOP_CustomerByItem');
                $scope.IV00108 = { Status: 1 };
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
    $scope.DeleteItemCustomer = function () {
        $.SmartMessageBox({
            title: "Delete Record",
            content: "Are you sure you want to delete this?",
            buttons: '[No][Yes]'
        }, function (ButtonPressed) {
            if (ButtonPressed === "Yes") {
                $http.post('/MasterItem/DeleteItemCustomerData', { 'IV00108': $scope.IV00108, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
                    if (data.Status == true) {
                        $.bigBox({
                            title: data.Massage,
                            content: data.Description,
                            color: "#739E73",
                            timeout: 4000,
                            icon: "fa fa-check shake animated"
                        });
                        $scope.GridRefresh('GridSOP_CustomerByItem');
                        $scope.IV00108 = { Status: 1 };
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
    //------------------------- Item Vendor
    $scope.IV00109 = {};
    $scope.ItemVendorManagement = function () {
        removeEntityPage($scope.PagesIV00100);
        $scope.LoadIV00100Page('ItemVendor');
        $scope.IV00109.Status = 1;
    };

    $scope.SaveItemVendor = function () {
        var grid = $("#GridPOP_VendorByItem").data("kendoGrid");
        var dataSourceData = grid.dataSource.data();

        if ($scope.functiontofindIndexByKeyValue(dataSourceData, "VendorID", $scope.IV00109.VendorID) != null) {
            $.smallBox({
                title: "Already Allocated",
                content: "<i class='fa fa-clock-o'></i> <i>This Vendor is already allocated to this Item before !!</i>",
                color: "#3276B1",
                iconSmall: "fa fa-times fa-2x fadeInRight animated",
                timeout: 4000
            });
            return;
        }
        $scope.IV00109.ItemID = $scope.IV00100.ItemID;
        $http.post('/MasterItem/SaveItemVendorData', { 'IV00109': $scope.IV00109, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
            if (data.Status == true) {
                $.bigBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#739E73",
                    timeout: 4000,
                    icon: "fa fa-check shake animated"
                });
                $scope.GridRefresh('GridPOP_VendorByItem');
                $scope.IV00109 = { Status: 1 };
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
    $scope.UpdateItemVendor = function () {
        $scope.IV00109.ItemID = $scope.IV00100.ItemID;
        $http.post('/MasterItem/UpdateItemVendorData', { 'IV00109': $scope.IV00109, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
            if (data.Status == true) {
                $.bigBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#739E73",
                    timeout: 4000,
                    icon: "fa fa-check shake animated"
                });
                $scope.GridRefresh('GridPOP_VendorByItem');
                $scope.IV00109 = { Status: 1 };
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
    $scope.DeleteItemVendor = function () {
        $.SmartMessageBox({
            title: "Delete Record",
            content: "Are you sure you want to delete this?",
            buttons: '[No][Yes]'
        }, function (ButtonPressed) {
            if (ButtonPressed === "Yes") {
                $http.post('/MasterItem/DeleteItemVendorData', { 'IV00109': $scope.IV00109, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
                    if (data.Status == true) {
                        $.bigBox({
                            title: data.Massage,
                            content: data.Description,
                            color: "#739E73",
                            timeout: 4000,
                            icon: "fa fa-check shake animated"
                        });
                        $scope.GridRefresh('GridPOP_VendorByItem');
                        $scope.IV00109 = { Status: 1 };
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
    //------------------------- Item Site
    $scope.IV00102 = {};
    $scope.ItemSiteManagement = function () {
        removeEntityPage($scope.PagesIV00100);
        $scope.LoadIV00100Page('ItemSite');
        $scope.IV00102.Status = 1;
    };
    $scope.SiteBack = function (site) {

        if (site != null) {
            switch ($scope.CurrentControl) {
                case 'WeekSite':
                    $scope.IV00150.SiteID = site.SiteID;
                    $scope.IV00150.SiteName = site.SiteName;
                    $scope.IV00150.BinTrack = site.BinTrack;
                    for (i = 0; i < $scope.WeekDayLength; i++) {
                        $scope.IV00150List[i].SiteID = site.SiteID;
                    }
                    $scope.LoadIV00150UpdateData();
                    break;
                case 'WeekSitePriceMinutes':
                    $scope.IV00151.SiteID = site.SiteID;
                    break;
                case 'ItemSite':
                    if ($scope.IV00100.ItemID && $scope.IV00100.ItemID != null) {
                        var grid = $("#GridIV_SiteByItem").data("kendoGrid");
                        var dataSourceData = grid.dataSource.data();
                        var found = $scope.functiontofindObjectByKeyValue(dataSourceData, "SiteID", site.SiteID);
                        if (found != null) {
                            $scope.IV00102 = found;
                            $scope.IV00102.Status = 2;
                        }
                        else {
                            $scope.IV00102 = {
                                ItemID: $scope.IV00100.ItemID,
                                SiteID: site.SiteID,
                                SiteName: site.SiteName,
                                BinTrack: site.BinTrack,
                                Status: 1
                            };
                        }
                    }
                    else {
                        $scope.IV00102.SiteID = site.SiteID;
                        $scope.IV00102.SiteName = site.SiteName;
                        $scope.IV00102.BinTrack = site.BinTrack;
                    }
            }
        }
    };

    $scope.SaveItemSite = function () {
        var grid = $("#GridIV_SiteByItem").data("kendoGrid");
        var dataSourceData = grid.dataSource.data();

        if ($scope.functiontofindIndexByKeyValue(dataSourceData, "SiteID", $scope.IV00102.SiteID) != null) {
            $.smallBox({
                title: "Already Allocated",
                content: "<i class='fa fa-clock-o'></i> <i>This Site is already allocated to this Item before !!</i>",
                color: "#3276B1",
                iconSmall: "fa fa-times fa-2x fadeInRight animated",
                timeout: 4000
            });
            return;
        }
        $scope.IV00102.ItemID = $scope.IV00100.ItemID;
        $http.post('/MasterItem/SaveItemSiteData', { 'IV00102': $scope.IV00102, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
            if (data.Status == true) {
                $.bigBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#739E73",
                    timeout: 4000,
                    icon: "fa fa-check shake animated"
                });
                $scope.GridRefresh('GridIV_SiteByItem');
                $scope.IV00102 = { Status: 1 };
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
    $scope.UpdateItemSite = function () {
        $scope.IV00102.ItemID = $scope.IV00100.ItemID;
        $http.post('/MasterItem/UpdateItemSiteData', { 'IV00102': $scope.IV00102, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
            if (data.Status == true) {
                $.bigBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#739E73",
                    timeout: 4000,
                    icon: "fa fa-check shake animated"
                });
                $scope.GridRefresh('GridIV_SiteByItem');
                $scope.IV00102 = { Status: 1 };
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
    $scope.DeleteItemSite = function () {
        $.SmartMessageBox({
            title: "Delete Record",
            content: "Are you sure you want to delete this?",
            buttons: '[No][Yes]'
        }, function (ButtonPressed) {
            if (ButtonPressed === "Yes") {
                $http.post('/MasterItem/DeleteItemSiteData', { 'IV00102': $scope.IV00102, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
                    if (data.Status == true) {
                        $.bigBox({
                            title: data.Massage,
                            content: data.Description,
                            color: "#739E73",
                            timeout: 4000,
                            icon: "fa fa-check shake animated"
                        });
                        $scope.GridRefresh('GridIV_SiteByItem');
                        $scope.IV00102 = { Status: 1 };
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


    //----------------------------- Start | IV00150
    $scope.IV00150 = {};
    $scope.IV00150List = [];

    $scope.LoadIV00150Page = function (Page) {
        $scope.PageStatus = 1;

        $scope.IV00150.SiteID = '';
        $scope.IV00150.ItemID = '';
        $scope.IV00150.DecimalDigit = $scope.MY.DecimalDigit;
        $scope.IV00150.CurrencyCode = $scope.MY.CurrencyCode;
        $scope.IV00150.ExchangeTableID = $scope.MY.ExchangeTableID;
        $scope.IV00150.IsMultiply = $scope.MY.IsMultiply;
        $scope.IV00150.ExchangeRateID = $scope.MY.ExchangeRateID;
        $scope.IV00150.ExchangeRate = $scope.MY.ExchangeRate;

        $scope.LoadIV00100Page(Page);
        $scope.LoadIV00022WeekDetails();
    }

    $scope.WeekDayLength = 0;

    $scope.WeekDetails = {};
    $scope.LoadIV00022WeekDetails = function () {
        $http.post('/ItemClass/LoadIV00022WeekDetails', {
            'KaizenPublicKey': sessionStorage.PublicKey
        }).success(function (data) {
            $scope.WeekDetails = data;
            $scope.WeekDayLength = $scope.WeekDetails.length;
            for (i = 0; i < $scope.WeekDayLength; i++) {
                $scope.IV00150obj = {};
                $scope.IV00150obj.ItemID = '';
                $scope.IV00150obj.SiteID = '';
                $scope.IV00150obj.WeekDayID = $scope.WeekDetails[i].WeekDayID;

                $scope.IV00150obj.CurrencyCode = $scope.MY.CurrencyCode;
                $scope.IV00150obj.DecimalDigit = $scope.MY.DecimalDigit;
                $scope.IV00150obj.ExchangeRate = $scope.MY.ExchangeRate;
                $scope.IV00150obj.IsMultiply = $scope.MY.IsMultiply;
                $scope.IV00150obj.UnitPrice = '';
                $scope.IV00150List.push($scope.IV00150obj);
            }
        }).error(function (data, status, headers, config) {

        });
    }


    $scope.LoadIV00150UpdateData = function () {
        if ($scope.IV00150.ItemID && $scope.IV00150.SiteID) {
            $http.post('/MasterItem/LoadIV00150UpdateData', {
                'ItemID': $scope.IV00150.ItemID, 'SiteID': $scope.IV00150.SiteID, 'KaizenPublicKey': sessionStorage.PublicKey
            }).success(function (data) {
                if (data.length) {
                    $scope.IV00150List = data;

                    $scope.IV00150.DecimalDigit = $scope.IV00150List[0].DecimalDigit;
                    $scope.IV00150.CurrencyCode = $scope.IV00150List[0].CurrencyCode;
                    $scope.IV00150.ExchangeTableID = $scope.MY.ExchangeTableID;
                    $scope.IV00150.IsMultiply = $scope.IV00150List[0].IsMultiply;
                    $scope.IV00150.ExchangeRateID = $scope.MY.ExchangeRateID;
                    $scope.IV00150.ExchangeRate = $scope.IV00150List[0].ExchangeRate;
                    $scope.PageStatus = 2;
                }
                else {
                    $scope.PageStatus = 1;
                    for (i = 0; i < $scope.WeekDayLength; i++) {
                        $scope.IV00150List[i].UnitPrice = '';
                    }
                    $scope.IV00150.DecimalDigit = $scope.MY.DecimalDigit;
                    $scope.IV00150.CurrencyCode = $scope.MY.CurrencyCode;
                    $scope.IV00150.ExchangeTableID = $scope.MY.ExchangeTableID;
                    $scope.IV00150.IsMultiply = $scope.MY.IsMultiply;
                    $scope.IV00150.ExchangeRateID = $scope.MY.ExchangeRateID;
                    $scope.IV00150.ExchangeRate = $scope.MY.ExchangeRate;
                }
            }).error(function (data, status, headers, config) {
                alert('---error---');
            });
        }
    }

    $scope.GetAllIV00150byClassID;

    $scope.SaveWeekData = function () {
        $http.post('/MasterItem/SaveWeekData', {
            'IV00150List': $scope.IV00150List, 'KaizenPublicKey': sessionStorage.PublicKey
        }).success(function (data) {

            if (data.Status == true) {
                $.bigBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#739E73",
                    timeout: 8000,
                    icon: "fa fa-check shake animated"
                });
                $scope.Cancel();
                $scope.Clear();
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

    $scope.UpdateWeekData = function () {
        $http.post('/MasterItem/UpdateWeekData', {
            'IV00150List': $scope.IV00150List, 'KaizenPublicKey': sessionStorage.PublicKey
        }).success(function (data) {

            if (data.Status == true) {
                $.bigBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#739E73",
                    timeout: 8000,
                    icon: "fa fa-check shake animated"
                });
                $scope.Cancel();
                $scope.Clear();
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

    $scope.CurrencyBack = function (currency) {
        if (currency != null) {
            $scope.IV00150.DecimalDigit = currency.DecimalDigit;
            $scope.IV00150.CurrencyCode = currency.CurrencyCode;
            $scope.IV00150.ExchangeTableID = currency.ExchangeTableID;
            $scope.IV00150.IsMultiply = currency.IsMultiply;
            $scope.IV00150.ExchangeRateID = currency.ExchangeRateID;
            $scope.IV00150.ExchangeRate = currency.ExchangeRate;
            var kendoWindow = $("#MainDialog").data("kendoWindow");
            kendoWindow.close();
            for (i = 0; i < $scope.WeekDayLength; i++) {
                $scope.IV00150List[i].DecimalDigit = currency.DecimalDigit;
                $scope.IV00150List[i].CurrencyCode = currency.CurrencyCode;
                $scope.IV00150List[i].ExchangeTableID = currency.ExchangeTableID;
                $scope.IV00150List[i].IsMultiply = currency.IsMultiply;
                $scope.IV00150List[i].ExchangeRateID = currency.ExchangeRateID;
                $scope.IV00150List[i].ExchangeRate = currency.ExchangeRate;
            }
        }
    };
    //----------------------------- End | IV00150

    // ---------------- Start Price Minutes -----------------
    $scope.IV00151 = {};
    $scope.SelectedLookup = {};
    $scope.IV00152List = [];
    $scope.IV00152 = {};
    $scope.IV00152Model = {
        PeriodName: '',
        StartTimeFrom: '',
        EndTimeTo: '',
        UnitMinutes: 0,
        UnitPrice: 0,
        LoyalityPoints: 0
    };

    $scope.LoadPriceMinutes = function (Page) {
        $scope.PageStatus = 1;

        $scope.LoadIV00100Page(Page);
        $scope.LoadWeekDays();
    };

    $scope.WeekDayChangedForPriceMeetings = function () {
        if (angular.isDefined($scope.SelectedLookup.SelectedWeekDay)) {
            $scope.IV00151.WeekDayID = $scope.SelectedLookup.SelectedWeekDay;
        }
    };

    $scope.LoadWeekDays = function () {
        $http.get('/MasterItem/GetWeekDays?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            if (data.length > 0)
                $scope.WeekDays = data;
        });
    };

    $scope.PeriodCountChanged = function () {
        if ($scope.IV00151.PeriodCount && $scope.IV00151.PeriodCount > 0) {
            $scope.IV00152List = [];
            for (var i = 0; i < $scope.IV00151.PeriodCount; i++) {
                $scope.IV00152Model = {
                    PeriodName: '',
                    StartTimeFrom: '',
                    EndTimeTo: '',
                    UnitMinutes: 0,
                    UnitPrice: 0,
                    LoyalityPoints: 0
                };
                $scope.IV00152List.push($scope.IV00152Model);
            }
        }
    };
});