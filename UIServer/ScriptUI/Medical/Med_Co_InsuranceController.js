app.controller('Med_Co_InsuranceController', function ($scope, $http) {
    $scope.Med00100 = {};
    $scope.SelectedView = {};
    $scope.PagesMed00100 = [];
    $scope.LoadLookUp = function () {
        $http.get('/Med_Set_InsuranceStatus/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.insuranceStatuses = data;
        }).finally(function () { });
        $http.get('/Med_Set_InsuranceClass/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.insuranceClasses = data;
        }).finally(function () { });
    };
    $scope.LoadLookUp();

    $http.get('/Sys_View/GetViewsByScreen?ScreenID=1102&KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
        $scope.ViewList = data;
    }).finally(function () {
        if ($scope.ViewList.length > 0) {
            $scope.Med00100.ViewID = $scope.ViewList[0].ViewID;
            $scope.SelectedView = $scope.ViewList[0];
            $scope.ViewList.forEach(function (element, index) {
                if (element.IsDefault) {
                    $scope.Med00100.ViewID = element.ViewID;
                    $scope.SelectedView = element;
                    return;
                }
            });
        }
        else {
            Notify('Missing User Setup', 'bottom-right', '5000', 'danger', 'fa-bolt', true);
            return;
        }
        $scope.ViewChanged();

        $scope.toolbarOptions = {
            items: [
                    {
                        type: "splitButton",
                        text: "<span class=\"fa fa-plus-circle\"></span> New",
                        attributes: { "class": "btn-primary" },
                        click: function (e) {
                            $scope.$apply(function () {
                                $scope.AddMed00100();
                            });
                        },
                        menuButtons: [
                            { attributes: { "class": "btn-primary" }, text: "<span class=\"fa fa-plus-circle\"></span> New Insurance", click: function (e) { $scope.AddMed00100(); } },
                            { text: "<span class=\"fa fa-upload\"></span> Upload" }
                        ]
                    },
                    {
                        type: "splitButton",
                        text: "GO",
                        click: function (e) {
                            $scope.$apply(function () {
                                $scope.InsuranceAddress();
                            })
                        },
                        menuButtons: [
                            {
                                type: "button", text: "<span class=\"fa fa-building-o\"></span> Address",
                                click: function (e) {
                                    $scope.$apply(function () {
                                        $scope.InsuranceAddress();
                                    })
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
                        click: function (e) { $scope.OpenFilterWindow("GridMed_Co_Insurance", "Med_Co_Insurance"); }
                    },
                    { type: "separator" },
                    { template: "<label>View:</label>" },
                    {
                        template: "<select kendo-drop-down-list k-ng-model='SelectedView' k-on-change='ViewChanged()' k-options='InsuranceViewOptions' id='GridViewoption_Med_Co_Insurance' style='width: 150px;'></select>",
                        overflow: "never"
                    }
            ]
            , resizable: true
        };
        $scope.InsuranceViewOptions = {
            filter: "contains",
            model: "SelectedView",
            dataTextField: "ViewName",
            dataValueField: "ViewID",
            dataSource: $scope.ViewList,
            value: $scope.Med00100.ViewID
        };
    });

    $scope.LoadPage = function (ActionName) {
        removeEntityPage($scope.PagesMed00100);
        var URIPath = "/" + $scope.ToolBar.ActiveScreen.ID + "/" + ActionName + "?KaizenPublicKey=" + sessionStorage.PublicKey;
        var Page = { url: URIPath, ActionName: ActionName };
        $scope.PagesMed00100.push(Page);
    };
    $scope.ViewChanged = function () {
        $scope.Med00100.ViewID = $scope.SelectedView.ViewID;
        $scope.MainGridURL = "/Med_Co_Insurance/MainGrid?ViewID=" + $scope.SelectedView.ViewID + "&KaizenPublicKey=" + sessionStorage.PublicKey;
        //$scope.GridRefresh();
    };
    //------------------------Filter
    $scope.MYFilter.get = function (field) {
        var DS = {};
        if (field == 'StatusID') {
            DS.key = "StatusName";
            DS.source = $scope.insuranceStatuses;
        }
        if (field == 'CUSTCLAS') {
            DS.key = "CUSTCLASNAME";
            DS.source = $scope.insuranceClasses;
        }
        return DS;
    };
    //---------------------------------------------------------------------------------------------
    $scope.LoadMed00100 = function (InsuranceID) {
        if (angular.isUndefined($scope.Med00100.InsuranceID)) {
            $http.get('/Med_Co_Insurance/GetSingle?KaizenPublicKey=' + sessionStorage.PublicKey + "&InsuranceID=" + InsuranceID).success(function (data) {
                $scope.Med00100 = data;
            }).finally(function () {
                $scope.Med00100.Status = 2;
                if ($scope.Med00100.PhotoExtension == null)
                    $scope.link = '/Photo/InsurancePhoto/InsuranceID.jpg';
                else
                    $scope.link = '/Photo/InsurancePhoto/' + kaizenTrim($scope.Med00100.InsuranceID) + "." + kaizenTrim($scope.Med00100.PhotoExtension) + "?c=" + Math.random();

                $scope.Med00101 = { InsuranceID: $scope.Med00100.InsuranceID, Status: 1 };
            });
        }
    };
    $scope.AddMed00100 = function () {
        $scope.LoadPage('Create');
        $scope.Clear();
        $scope.Med00100.Status = 1;
        $scope.Med00101.Status = 1;
        $http.get('/Med_Co_Insurance/InsuranceAddressTypeFillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey + '&ContactSourceID=1').success(function (data) {
            $scope.InsuranceAddressTypes = data;
        }).finally(function () { });
        $http.get('/Adm_Country/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.Countries = data;
        }).finally(function () { });
        $http.get('/Adm_City/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.Cities = data;
        }).finally(function () { });
    };
    $scope.EditMed00100 = function (InsuranceID) {
        $scope.LoadPage('Create');
        $scope.LoadMed00100(InsuranceID);
        $http.get('/Med_Co_Insurance/InsuranceAddressTypeFillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey + '&ContactSourceID=1').success(function (data) {
            $scope.InsuranceAddressTypes = data;
        }).finally(function () { });
        $http.get('/Adm_Country/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.Countries = data;
        }).finally(function () { });
        $http.get('/Adm_City/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.Cities = data;
        }).finally(function () { });
    };
    $scope.SaveData = function () {
        $http.post('/Med_Co_Insurance/SaveData', { 'Med00100': $scope.Med00100, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
            if (data.Status == true) {
                if ($scope.InsuranceAddresses.length > 0){
                    $http.post('/Med_Co_Insurance/SaveInsuranceAddresses', { 'Med00101': $scope.InsuranceAddresses, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
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
                else {
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
        $http.post('/Med_Co_Insurance/UpdateData', { 'Med00100': $scope.Med00100, 'KaizenPublicKey': sessionStorage.PublicKey, 'PhotoChanged': $scope.PhotoChanged }).success(function (data) {
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
        $scope.Med00100 = {};
        $scope.Med00101 = { Status: 1 };
        $scope.Med00100.Status = 1;
        $scope.Med00100.StatusID = '1'
        $scope.link = '/Photo/InsurancePhoto/InsuranceID.jpg';
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
                $http.post('/Med_Co_Insurance/DeleteData', { 'Med00100': $scope.Med00100, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
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
        $scope.PagesMed00100 = [];

        $scope.InsuranceAddresses = [];
    };

    $scope.PhotoChanged = false; // 1= new ; 2 Edit
    $scope.OnClassChanged = function () {
        //$http.get('/Med_Co_Insurance/GetNextInsurance?KaizenPublicKey=' + sessionStorage.PublicKey, { params: { CUSTCLAS: $scope.Med00100.CUSTCLAS } })
        //.success(function (data) {
        //    $scope.Med00100.InsuranceID = data;
        //})
        //.finally(function () {
        //});
    };

    $scope.InsuranceBack = function (Insurance) {
        if (Insurance != null) {
            switch ($scope.CurrentControl) {
                case 'ParentInsurance':
                    $scope.Med00100.ParentInsuranceID = Insurance.InsuranceID;
                    break;
                case 'InsuranceAddress':
                    $scope.Med00100 = Insurance;
                    $scope.Med00101 = { InsuranceID: Insurance.InsuranceID, Status: 1 };
                    $scope.GridRefresh('GridMed_InsuranceAddressByInsurance');
                    break;
            }
        }
    };

    // Setting Photo path extension
    $scope.SetPhotoExtension = function (PhotoPath) {
        $scope.Med00100.PhotoExtension = PhotoPath;
        $scope.link = '/Photo/InsurancePhotoTemp/' + kaizenTrim(PhotoPath);
        $scope.PhotoChanged = true;
        // $scope.$apply();
    };
    $scope.RemovePhotoExtension = function (PhotoPath) {
        $scope.Med00100.PhotoExtension = "";
    };

    //-------------------------- Address Information
    $scope.Med00101 = {};
    $scope.InsuranceAddresses = [];
    $scope.InsuranceAddress = function () {
        removeEntityPage($scope.PagesMed00100);
        $scope.LoadPage('InsuranceAddress');
        $scope.Med00101.Status = 1;
        $http.get('/Med_Co_Insurance/InsuranceAddressTypeFillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey + '&ContactSourceID=1').success(function (data) {
            $scope.InsuranceAddressTypes = data;
        }).finally(function () { });
        $http.get('/Adm_Country/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.Countries = data;
        }).finally(function () { });
        $http.get('/Adm_City/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.Cities = data;
        }).finally(function () { });
    };
    $scope.SaveInsuranceAddress = function () {
        var grid = $("#GridMed_InsuranceAddressByInsurance").data("kendoGrid");
        var dataSourceData = grid.dataSource.data();

        if ($scope.functiontofindIndexByKeyValue(dataSourceData, "AddressCode", $scope.Med00101.AddressCode) != null) {
            $.smallBox({
                title: "No Changes have been made!!",
                content: "<i class='fa fa-clock-o'></i> <i>This Type is already has data in this Insurance !!</i>",
                color: "#3276B1",
                iconSmall: "fa fa-times fa-2x fadeInRight animated",
                timeout: 4000
            });
            return;
        }
        $scope.Med00101.InsuranceID = $scope.Med00100.InsuranceID;
        $http.post('/Med_Co_Insurance/SaveInsuranceAddress', { 'Med00101': $scope.Med00101, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
            if (data.Status == true) {
                $.bigBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#739E73",
                    timeout: 4000,
                    icon: "fa fa-check shake animated"
                });
                $scope.GridRefresh('GridMed_InsuranceAddressByInsurance');
                $scope.Med00101 = { Status: 1 };
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
    $scope.UpdateInsuranceAddress = function () {
        $scope.Med00101.InsuranceID = $scope.Med00100.InsuranceID;
        $http.post('/Med_Co_Insurance/UpdateInsuranceAddress', { 'Med00101': $scope.Med00101, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
            if (data.Status == true) {
                $.bigBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#739E73",
                    timeout: 4000,
                    icon: "fa fa-check shake animated"
                });
                $scope.GridRefresh('GridMed_InsuranceAddressByInsurance');
                $scope.Med00101 = { Status: 1 };
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
    $scope.DeleteInsuranceAddress = function () {
        $.SmartMessageBox({
            title: "Delete Record",
            content: "Are you sure you want to delete this?",
            buttons: '[No][Yes]'
        }, function (ButtonPressed) {
            if (ButtonPressed === "Yes") {
                $http.post('/Med_Co_Insurance/DeleteInsuranceAddress', { 'Med00101': $scope.Med00101, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
                    if (data.Status == true) {
                        $.bigBox({
                            title: data.Massage,
                            content: data.Description,
                            color: "#739E73",
                            timeout: 4000,
                            icon: "fa fa-check shake animated"
                        });
                        $scope.GridRefresh('GridMed_InsuranceAddressByInsurance');
                        $scope.Med00101 = { Status: 1 };
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
                    color: "#3276B1",
                    iconSmall: "fa fa-times fa-2x fadeInRight animated",
                    timeout: 4000
                });
                $scope.Med00101 = { Status: 1 };
            }
        });
    };
    $scope.AddressTypeChanged = function () {
        if ($scope.Med00100.Status == 1) {
            var found = $scope.functiontofindObjectByKeyValue($scope.InsuranceAddresses, "AddressCode", $scope.Med00101.AddressCode);
            if (found != null) {
                $scope.Med00101 = found;
                $scope.Med00101.Status = 2;
            }
            else {
                $scope.Med00101 = { AddressCode: $scope.Med00101.AddressCode, Status: 1 };
                var addressName = $scope.functiontofindObjectByKeyValue($scope.InsuranceAddressTypes, "AddressCode", $scope.Med00101.AddressCode);
                $scope.Med00101.AddressName = addressName.AddressTypeName;
            }
        }
        else {
            var grid = $("#GridMed_InsuranceAddressByInsurance").data("kendoGrid");
            var dataSourceData = [];
            angular.copy(grid.dataSource.data(), dataSourceData);
            var found = $scope.functiontofindObjectByKeyValue(dataSourceData, "AddressCode", $scope.Med00101.AddressCode);
            if (found != null) {
                $scope.Med00101 = found;
                $scope.Med00101.Status = 2;
                return;
            }
            else {
                $scope.Med00101 = { Status: 1, AddressCode: $scope.Med00101.AddressCode };
                return;
            }
        }
    };
    $scope.AddNewAddress = function () {
        $scope.Med00101.InsuranceID = $scope.Med00100.InsuranceID;
        $scope.InsuranceAddresses.push($scope.Med00101);
        $scope.Med00101 = { Status: 1 };
    };
    $scope.UpdateAddress = function () {
        var index = $scope.GetSingleIndex($scope.InsuranceAddresses, "AddressCode", $scope.Med00101.AddressCode);
        $scope.InsuranceAddresses.splice(index, 1, $scope.Med00101);
        $scope.Med00101 = { Status: 1 };
    };
    $scope.RemoveAddress = function (address, index) {
        $scope.InsuranceAddresses.splice(index, 1);
    };
    $scope.OnAddressTabSelected = function () {
        if ($scope.Med00100.Status == 2) {
            $scope.GridRefresh('GridMed_InsuranceAddressByInsurance');
        }
    };


});