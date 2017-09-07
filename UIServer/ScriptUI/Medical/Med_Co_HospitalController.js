app.controller('Med_Co_HospitalController', function ($scope, $http) {
    $scope.Med00300 = {};
    $scope.SelectedView = {};
    $scope.PagesMed00300 = [];
    $scope.LoadLookUp = function () {
        $http.get('/Med_Set_HospitalStatus/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.hospitalStatuses = data;
        }).finally(function () { });
        $http.get('/Med_Set_HospitalClass/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.hospitalClasses = data;
        }).finally(function () { });
    };
    $scope.LoadLookUp();

    $http.get('/Sys_View/GetViewsByScreen?ScreenID=1102&KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
        $scope.ViewList = data;
    }).finally(function () {
        if ($scope.ViewList.length > 0) {
            $scope.Med00300.ViewID = $scope.ViewList[0].ViewID;
            $scope.SelectedView = $scope.ViewList[0];
            $scope.ViewList.forEach(function (element, index) {
                if (element.IsDefault) {
                    $scope.Med00300.ViewID = element.ViewID;
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
                                $scope.AddMed00300();
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
                                $scope.HospitalAddress();
                            })
                        },
                        menuButtons: [
                            {
                                type: "button", text: "<span class=\"fa fa-building-o\"></span> Address",
                                click: function (e) {
                                    $scope.$apply(function () {
                                        $scope.HospitalAddress();
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
                        click: function (e) { $scope.OpenFilterWindow("GridMed_Co_Hospital", "Med_Co_Hospital"); }
                    },
                    { type: "separator" },
                    { template: "<label>View:</label>" },
                    {
                        template: "<select kendo-drop-down-list k-ng-model='SelectedView' k-on-change='ViewChanged()' k-options='HospitalViewOptions' id='GridViewoption_Med_Co_Hospital' style='width: 150px;'></select>",
                        overflow: "never"
                    }
            ]
            , resizable: true
        };
        $scope.HospitalViewOptions = {
            filter: "contains",
            model: "SelectedView",
            dataTextField: "ViewName",
            dataValueField: "ViewID",
            dataSource: $scope.ViewList,
            value: $scope.Med00300.ViewID
        };
    });

    $scope.LoadPage = function (ActionName) {
        removeEntityPage($scope.PagesMed00300);
        var URIPath = "/" + $scope.ToolBar.ActiveScreen.ID + "/" + ActionName + "?KaizenPublicKey=" + sessionStorage.PublicKey;
        var Page = { url: URIPath, ActionName: ActionName };
        $scope.PagesMed00300.push(Page);
    };
    $scope.ViewChanged = function () {
        $scope.Med00300.ViewID = $scope.SelectedView.ViewID;
        $scope.MainGridURL = "/Med_Co_Hospital/MainGrid?ViewID=" + $scope.SelectedView.ViewID + "&KaizenPublicKey=" + sessionStorage.PublicKey;
        //$scope.GridRefresh();
    };
    //------------------------Filter
    $scope.MYFilter.get = function (field) {
        var DS = {};
        if (field == 'StatusID') {
            DS.key = "StatusName";
            DS.source = $scope.hospitalStatuses;
        }
        if (field == 'CUSTCLAS') {
            DS.key = "CUSTCLASNAME";
            DS.source = $scope.hospitalClasses;
        }
        return DS;
    };
    //---------------------------------------------------------------------------------------------
    $scope.LoadMed00300 = function (HospitalID) {
        if (angular.isUndefined($scope.Med00300.HospitalID)) {
            $http.get('/Med_Co_Hospital/GetSingle?KaizenPublicKey=' + sessionStorage.PublicKey + "&HospitalID=" + HospitalID).success(function (data) {
                $scope.Med00300 = data;
            }).finally(function () {
                $scope.Med00300.Status = 2;
                if ($scope.Med00300.PhotoExtension == null)
                    $scope.link = '/Photo/HospitalPhoto/HospitalID.jpg';
                else
                    $scope.link = '/Photo/HospitalPhoto/' + kaizenTrim($scope.Med00300.HospitalID) + "." + kaizenTrim($scope.Med00300.PhotoExtension) + "?c=" + Math.random();

                $scope.Med00301 = { HospitalID: $scope.Med00300.HospitalID, Status: 1 };
            });
        }
    };
    $scope.AddMed00300 = function () {
        $scope.LoadPage('Create');
        $scope.Clear();
        $scope.Med00300.Status = 1;
        $scope.Med00301.Status = 1;
        $http.get('/Med_Co_Hospital/HospitalAddressTypeFillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey + '&ContactSourceID=1').success(function (data) {
            $scope.HospitalAddressTypes = data;
        }).finally(function () { });
        $http.get('/Adm_Country/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.Countries = data;
        }).finally(function () { });
        $http.get('/Adm_City/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.Cities = data;
        }).finally(function () { });
    };
    $scope.EditMed00300 = function (HospitalID) {
        $scope.LoadPage('Create');
        $scope.LoadMed00300(HospitalID);
        $http.get('/Med_Co_Hospital/InsuranceAddressTypeFillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey + '&ContactSourceID=1').success(function (data) {
            $scope.HospitalAddressTypes = data;
        }).finally(function () { });
        $http.get('/Adm_Country/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.Countries = data;
        }).finally(function () { });
        $http.get('/Adm_City/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.Cities = data;
        }).finally(function () { });
    };
    $scope.SaveData = function () {
        $http.post('/Med_Co_Hospital/SaveData', { 'Med00300': $scope.Med00300, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
            if (data.Status == true) {
                if ($scope.HospitalAddresses.length > 0){
                    $http.post('/Med_Co_Hospital/SaveInsuranceAddresses', { 'Med00301': $scope.HospitalAddresses, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
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
        $http.post('/Med_Co_Hospital/UpdateData', { 'Med00300': $scope.Med00300, 'KaizenPublicKey': sessionStorage.PublicKey, 'PhotoChanged': $scope.PhotoChanged }).success(function (data) {
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
        $scope.Med00300 = {};
        $scope.Med00301 = { Status: 1 };
        $scope.Med00300.Status = 1;
        $scope.Med00300.StatusID = '1'
        $scope.link = '/Photo/InsurancePhoto/HospitalID.jpg';
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
                $http.post('/Med_Co_Hospital/DeleteData', { 'Med00300': $scope.Med00300, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
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
        $scope.PagesMed00300 = [];

        $scope.HospitalAddresses = [];
    };

    $scope.PhotoChanged = false; // 1= new ; 2 Edit
    $scope.OnClassChanged = function () {
        //$http.get('/Med_Co_Hospital/GetNextHospital?KaizenPublicKey=' + sessionStorage.PublicKey, { params: { CUSTCLAS: $scope.Med00300.CUSTCLAS } })
        //.success(function (data) {
        //    $scope.Med00300.HospitalID = data;
        //})
        //.finally(function () {
        //});
    };

    $scope.HospitalBack = function (hospital) {
        if (hospital != null) {
            switch ($scope.CurrentControl) {
                case 'ParentInsurance':
                    $scope.Med00300.ParentInsuranceID = hospital.HospitalID;
                    break;
                case 'HospitalAddress':
                    $scope.Med00300 = hospital;
                    $scope.Med00301 = { HospitalID: hospital.HospitalID, Status: 1 };
                    $scope.GridRefresh('GridMed_HospitalAddressByHospital');
                    break;
            }
        }
    };

    // Setting Photo path extension
    $scope.SetPhotoExtension = function (PhotoPath) {
        $scope.Med00300.PhotoExtension = PhotoPath;
        $scope.link = '/Photo/HospitalPhotoTemp/' + kaizenTrim(PhotoPath);
        $scope.PhotoChanged = true;
        // $scope.$apply();
    };
    $scope.RemovePhotoExtension = function (PhotoPath) {
        $scope.Med00300.PhotoExtension = "";
    };

    //-------------------------- Address Information
    $scope.Med00301 = {};
    $scope.HospitalAddresses = [];
    $scope.HospitalAddress = function () {
        removeEntityPage($scope.PagesMed00300);
        $scope.LoadPage('HospitalAddress');
        $scope.Med00301.Status = 1;
        $http.get('/Med_Co_Hospital/InsuranceAddressTypeFillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey + '&ContactSourceID=1').success(function (data) {
            $scope.HospitalAddressTypes = data;
        }).finally(function () { });
        $http.get('/Adm_Country/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.Countries = data;
        }).finally(function () { });
        $http.get('/Adm_City/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.Cities = data;
        }).finally(function () { });
    };
    $scope.SaveHospitalAddress = function () {
        var grid = $("#GridMed_HospitalAddressByHospital").data("kendoGrid");
        var dataSourceData = grid.dataSource.data();

        if ($scope.functiontofindIndexByKeyValue(dataSourceData, "AddressTypeID", $scope.Med00301.AddressTypeID) != null) {
            $.smallBox({
                title: "No Changes have been made!!",
                content: "<i class='fa fa-clock-o'></i> <i>This Type is already has data in this Insurance !!</i>",
                color: "#3276B1",
                iconSmall: "fa fa-times fa-2x fadeInRight animated",
                timeout: 4000
            });
            return;
        }
        $scope.Med00301.HospitalID = $scope.Med00300.HospitalID;
        $http.post('/Med_Co_Hospital/SaveHospitalAddress', { 'Med00301': $scope.Med00301, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
            if (data.Status == true) {
                $.bigBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#739E73",
                    timeout: 4000,
                    icon: "fa fa-check shake animated"
                });
                $scope.GridRefresh('GridMed_HospitalAddressByHospital');
                $scope.Med00301 = { Status: 1 };
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
    $scope.UpdateHospitalAddress = function () {
        $scope.Med00301.HospitalID = $scope.Med00300.HospitalID;
        $http.post('/Med_Co_Hospital/UpdateHospitalAddress', { 'Med00301': $scope.Med00301, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
            if (data.Status == true) {
                $.bigBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#739E73",
                    timeout: 4000,
                    icon: "fa fa-check shake animated"
                });
                $scope.GridRefresh('GridMed_HospitalAddressByHospital');
                $scope.Med00301 = { Status: 1 };
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
    $scope.DeleteHospitalAddress = function () {
        $.SmartMessageBox({
            title: "Delete Record",
            content: "Are you sure you want to delete this?",
            buttons: '[No][Yes]'
        }, function (ButtonPressed) {
            if (ButtonPressed === "Yes") {
                $http.post('/Med_Co_Hospital/DeleteHospitalAddress', { 'Med00301': $scope.Med00301, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
                    if (data.Status == true) {
                        $.bigBox({
                            title: data.Massage,
                            content: data.Description,
                            color: "#739E73",
                            timeout: 4000,
                            icon: "fa fa-check shake animated"
                        });
                        $scope.GridRefresh('GridMed_HospitalAddressByHospital');
                        $scope.Med00301 = { Status: 1 };
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
                $scope.Med00301 = { Status: 1 };
            }
        });
    };
    $scope.AddressTypeChanged = function () {
        if ($scope.Med00300.Status == 1) {
            var found = $scope.functiontofindObjectByKeyValue($scope.HospitalAddresses, "AddressTypeID", $scope.Med00301.AddressTypeID);
            if (found != null) {
                $scope.Med00301 = found;
                $scope.Med00301.Status = 2;
            }
            else {
                $scope.Med00301 = { AddressTypeID: $scope.Med00301.AddressTypeID, Status: 1 };
                var addressName = $scope.functiontofindObjectByKeyValue($scope.HospitalAddressTypes, "AddressTypeID", $scope.Med00301.AddressTypeID);
                $scope.Med00301.AddressName = addressName.AddressTypeName;
            }
        }
        else {
            var grid = $("#GridMed_HospitalAddressByHospital").data("kendoGrid");
            var dataSourceData = [];
            angular.copy(grid.dataSource.data(), dataSourceData);
            var found = $scope.functiontofindObjectByKeyValue(dataSourceData, "AddressTypeID", $scope.Med00301.AddressTypeID);
            if (found != null) {
                $scope.Med00301 = found;
                $scope.Med00301.Status = 2;
                return;
            }
            else {
                $scope.Med00301 = { Status: 1, AddressTypeID: $scope.Med00301.AddressTypeID };
                return;
            }
        }
    };
    $scope.AddNewAddress = function () {
        $scope.Med00301.HospitalID = $scope.Med00300.HospitalID;
        $scope.HospitalAddresses.push($scope.Med00301);
        $scope.Med00301 = { Status: 1 };
    };
    $scope.UpdateAddress = function () {
        var index = $scope.GetSingleIndex($scope.HospitalAddresses, "AddressTypeID", $scope.Med00301.AddressTypeID);
        $scope.HospitalAddresses.splice(index, 1, $scope.Med00301);
        $scope.Med00301 = { Status: 1 };
    };
    $scope.RemoveAddress = function (address, index) {
        $scope.HospitalAddresses.splice(index, 1);
    };
    $scope.OnAddressTabSelected = function () {
        if ($scope.Med00300.Status == 2) {
            $scope.GridRefresh('GridMed_HospitalAddressByHospital');
        }
    };
});