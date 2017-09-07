﻿app.controller('CO_PartnerController', function ($scope, $http,$q) {
    $scope.CM00130 = {};
    $scope.SelectedView = {};
    $scope.PagesCM00130 = [];
    $scope.LoadLookUp = function () {
        $http.get('/CMS_ClientStatus/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.clientStatuses = data;
        }).finally(function () { });
        $http.get('/CO_Partner/GetParentClassList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.PartnerClasses = data;
        }).finally(function () { });

        $http.get('/CO_Partner/GetParentAddressTypes?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.PartnerAddressTypes = data;
        }).finally(function () { });

        $http.get('/CO_Partner/GetCountries?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.Countries = data;
        }).finally(function () { });

        $http.get('/CO_Partner/GetCities?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.Cities = data;
        }).finally(function () { });
    };
    $scope.LoadLookUp();

    $http.get('/Sys_View/GetViewsByScreen?ScreenID=22&KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
        $scope.ViewList = data;
    }).finally(function () {
        if ($scope.ViewList.length > 0) {
            $scope.CM00130.ViewID = $scope.ViewList[0].ViewID;
            $scope.SelectedView = $scope.ViewList[0];
            $scope.ViewList.forEach(function (element, index) {
                if (element.IsDefault) {
                    $scope.CM00130.ViewID = element.ViewID;
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
                            $scope.AddCM00130();
                        },
                        menuButtons: [
                            { attributes: { "class": "btn-primary" }, text: "<span class=\"fa fa-plus-circle\"></span> New Client", click: function (e) { $scope.AddCM00130(); } },
                            { text: "<span class=\"fa fa-upload\"></span> Upload" }
                        ]
                    },
                    {
                        type: "splitButton",
                        text: "GO",
                        click: function (e) {
                            $scope.ClientContact();
                        },
                        menuButtons: [
                            {
                                type: "button", text: "<span class=\"fa fa-user-md\"></span> Contact",
                                click: function (e) {
                                    $scope.$apply(function () {
                                        $scope.ClientContact();
                                    })
                                }
                            },
                            {
                                type: "button", text: "<span class=\"fa fa-building-o\"></span> Address",
                                click: function (e) {
                                    $scope.$apply(function () {
                                        $scope.ClientAddress();
                                    })
                                }
                            },
                            {
                                type: "button", text: "<span class=\"fa fa-file\"></span> Document",
                                click: function (e) {
                                    $scope.$apply(function () {
                                        $scope.ClientDocument();
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
                         click: function (e) { $scope.OpenFilterWindow("GridCMS_CO_Client", "CMS_CO_Client"); }
                     },
                     { type: "separator" },
                     { template: "<label>View:</label>" },
                     {
                         template: "<select kendo-drop-down-list k-ng-model='SelectedView' k-on-change='ViewChanged()' k-options='ClientViewOptions' id='GridViewoption_CMS_CO_Client' style='width: 150px;'></select>",
                         overflow: "never"
                     }
            ]
            , resizable: true
        };
        $scope.ClientViewOptions = {
            filter: "contains",
            model: "SelectedView",
            dataTextField: "ViewName",
            dataValueField: "ViewID",
            dataSource: $scope.ViewList,
            value: $scope.CM00130.ViewID
        };
    });
    $scope.LoadPage = function (ActionName) {
        removeEntityPage($scope.PagesCM00130);
        var URIPath = "/CMS/CO_Partner/" + ActionName + "?KaizenPublicKey=" + sessionStorage.PublicKey;
        var Page = { url: URIPath, ActionName: ActionName };
        $scope.PagesCM00130.push(Page);
    };
    $scope.ViewChanged = function () {
        $scope.CM00130.ViewID = $scope.SelectedView.ViewID;
        $scope.MainGridURL = "/CMS/CO_Partner/MainGrid?ViewID=" + $scope.SelectedView.ViewID + "&KaizenPublicKey=" + sessionStorage.PublicKey;
        //$scope.GridRefresh();
    };
    //------------------------Filter
    $scope.MYFilter.get = function (field) {
        var DS = {};
        if (field == 'StatusID') {
            DS.key = "StatusName";
            DS.source = $scope.clientStatuses;
        }
        if (field == 'CUSTCLAS') {
            DS.key = "CUSTCLASNAME";
            DS.source = $scope.PartnerClasses;
        }
        return DS;
    };
    //---------------------------------------------------------------------------------------------
    $scope.LoadCM00130 = function (PartnerID) {

        if (angular.isUndefined($scope.CM00130.PartnerID)) {
            $http.get('/CO_Partner/GetSingle?KaizenPublicKey=' + sessionStorage.PublicKey + "&PartnerID=" + PartnerID).success(function (data) {
                $scope.CM00130 = data;
                $scope.LoadCM00131ByPartnerID(PartnerID);

                var addrCodeList = [];

                if ($scope.CM00130.AddressCode != undefined && $scope.CM00130.AddressCode != null) 
                    addrCodeList.push($scope.CM00130.AddressCode);
                if ($scope.CM00130.BillAddressCode != undefined && $scope.CM00130.BillAddressCode != null)
                    addrCodeList.push($scope.CM00130.BillAddressCode);
                
                if (addrCodeList.length > 0) {
                    $scope.GetAddressData(addrCodeList);
                }

                $scope.CM00130.Status = 2;
                $scope.GetData_ByMainAddressType();
                $scope.GetData_ByBillAddressType();

            }).finally(function () {
                if ($scope.CM00130.PhotoExtension == null)
                    $scope.link = '/Photo/ClientPhoto/ClientID.jpg';
                else
                    $scope.link = '/Photo/KHCB/PartnerPhotoTemp/' + kaizenTrim($scope.CM00130.PartnerID) + "." + kaizenTrim($scope.CM00130.PhotoExtension) + "?c=" + Math.random();

                //$scope.CM00131 = { ClientID: $scope.CM00130.ClientID, Status: 1 };
            });
        }
    };

    $scope.GetAddressData = function (addrCodeList) {
        var paramObj = {};
        var params = {};
        params.KaizenPublicKey = sessionStorage.PublicKey;
        params.PartnerID = $scope.CM00130.PartnerID.trim();
        params.addressCodeTypeList = addrCodeList;
        paramObj.params = params;

        $http.get('/CO_Partner/GetPartnerAddressDataByAddressCodeList', paramObj).success(function (data) {
            if (data && data.length > 0) {
                //$scope.CM00131 = data[0];
                //$scope.CM00131.Status = 2;
            } 
        }).finally(function () {
            //$scope.CM00131.AddressCodeType = addrData.AddressCodeType;
        });
    };

    $scope.LoadCM00131ByPartnerID = function (PartnerID) {
        if ($scope.CM00130.PartnerID) {
            $http.get('/CO_Partner/GetPartnerAddressDataByPartnerID?KaizenPublicKey=' + sessionStorage.PublicKey + "&PartnerID=" + PartnerID).success(function (data) {
                if (data && data.length > 0) {
                    $scope.CM00131 = data[0];
                    //$scope.CM00130.MainAddress = data[0];
                    //$scope.CM00130.BillAddress = data[0];
                    $scope.CM00131.Status = 2;
                }
            }).finally(function () { });
        }
    };
    $scope.AddCM00130 = function () {
        $scope.LoadPage('Create');
        $scope.$apply();
        $scope.Clear();
        $scope.CM00130.Status = 1;
        $scope.CM00131.Status = 0;
        $http.get('/CMS_CO_Client/ClientAddressTypeFillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey + '&ContactSourceID=1').success(function (data) {
            $scope.ClientAddressTypes = data;
        }).finally(function () { });
        $http.get('/Adm_Country/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.Countries = data;
        }).finally(function () { });
        $http.get('/Adm_City/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.Cities = data;
        }).finally(function () { });
    };
    $scope.EditCM00130 = function (PartnerID) {
        $scope.LoadPage('Create');
        $scope.LoadCM00130(PartnerID);
        //$scope.AddressPathUrl = "/CMS_CO_Client/ClientAddress?KaizenPublicKey=" + sessionStorage.PublicKey;
        //$http.get('/CMS_CO_Client/ClientAddressTypeFillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey + '&ContactSourceID=1').success(function (data) {
        //    $scope.ClientAddressTypes = data;
        //}).finally(function () { });
        //$http.get('/Adm_Country/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
        //    $scope.Countries = data;
        //}).finally(function () { });
        //$http.get('/Adm_City/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
        //    $scope.Cities = data;
        //}).finally(function () { });
    };
    $scope.SaveData = function () {
        $http.post('/CO_Partner/SaveData', { 'CM00130': $scope.CM00130, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
            if (data.Status == true) {
                $scope.CM00131.Status = 1;
                $.bigBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#739E73",
                    timeout: 4000,
                    icon: "fa fa-check shake animated"
                });
                $scope.CM00131.PartnerID = $scope.CM00130.PartnerID;
                //$scope.SavePartnerAddressData();

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
    $scope.UpdateData = function () {
        $http.post('/CO_Partner/UpdateData', { 'CM00130': $scope.CM00130, 'KaizenPublicKey': sessionStorage.PublicKey, 'PhotoChanged': $scope.PhotoChanged }).success(function (data) {
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
    };
    $scope.Clear = function () {
        $scope.CM00130 = {};
        $scope.CM00131 = { Status: 0 };
        $scope.CM00112 = { Status: 0 };
        $scope.CM00115 = { Status: 0 };
        $scope.CM00130.Status = 1;
        $scope.CM00130.StatusID = '1'
        $scope.CM00130.IsActive = true;;
        $scope.link = '/Photo/ClientPhoto/ClientID.jpg';
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
                $http.post('/CO_Partner/DeleteData', { 'CM00130': $scope.CM00130, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
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
        $scope.PagesCM00130 = [];

        $scope.ClientAddresses = [];
    };

    $scope.PhotoChanged = false; // 1= new ; 2 Edit
    $scope.OnClassChanged = function () {
        //if (angular.isDefined($scope.CM00130.SelectedPartnerClass)) {
        //    $scope.CM00130.PartnerClassID = $scope.CM00130.SelectedPartnerClass.PartnerClassID;
        //}
    };
    $scope.ActiveChanged = function () {
        if ($scope.CM00130.IsActive) {
            $scope.CM00130.IsHold = false;
        } else {
            $scope.CM00130.IsHold = true;
        }
    };
    $scope.HoldChanged = function () {
        if ($scope.CM00130.IsHold) {
            $scope.CM00130.IsActive = false;
        } else {
            $scope.CM00130.IsActive = true;
        }
    };

    $scope.ClientBack = function (client) {
        if (client != null) {
            switch ($scope.CurrentControl) {
                case 'ParentClient':
                    $scope.CM00130.ParentClientID = client.ClientID;
                    break;
                case 'ClientAddress':
                    $scope.CM00130 = client;
                    $scope.CM00131 = { ClientID: client.ClientID, Status: 1 };
                    $scope.GridRefresh('GridCMS_ClientAddressByCLient');
                    break;
                case 'ClientDocument':
                    $scope.CM00130 = client;
                    $scope.CM00112 = { ClientID: client.ClientID, Status: 1 };
                    $scope.GridRefresh('GridCMS_ClientDocumentByCLient');
                    break;
                case 'ClientContact':
                    $scope.CM00130 = client;
                    $scope.CM00115 = { ClientID: client.ClientID, Status: 1 };
                    $scope.GridRefresh('GridCMS_CLientContactByCLient');
                    break;
            }
        }
    };
    $scope.ClientContactBack = function (contact) {
        if (contact != null) {
            $scope.CM00130.ContactTypeID = contact.ContactTypeID;
        }
    };
    $scope.ClientAddressBack = function (address) {
        if (address != null) {
            switch ($scope.CurrentControl) {
                case 'MainAddress':
                    $scope.CM00100.AddressCode = address.AddressCode;
                    $scope.CM00100.MainAddress = address;
                    break;
                case 'BillAddress':
                    $scope.CM00100.BillAddressCode = address.AddressCode;
                    $scope.CM00100.BillAddress = address;
                    break;
                case 'RemitAddress':
                    $scope.CM00100.RemitToAddressCode = address.AddressCode;
                    $scope.CM00100.RemitAddress = address;
                    break;
            }
        }
    };

    // Setting Photo path extension
    $scope.SetPhotoExtension = function (PhotoPath) {
        $scope.CM00130.PhotoExtension = PhotoPath;
        $scope.link = '/Photo/KHCB/PartnerPhotoTemp/' + kaizenTrim(PhotoPath);
        $scope.PhotoChanged = true;
        // $scope.$apply();
    };
    $scope.RemovePhotoExtension = function (PhotoPath) {
        $scope.CM00130.PhotoExtension = "";
    };

    //-------------------------- Contact Information
    $scope.CM00115 = {};
    $scope.ClientContact = function () {
        removeEntityPage($scope.PagesCM00130);
        $scope.LoadPage('ClientContact');
        $scope.CM00115.Status = 1;
        $http.get('/Admin_ContactType/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey + '&ContactSourceID=1').success(function (data) {
            $scope.ClientContactTypes = data;
        }).finally(function () { });

    };
    $scope.SaveClientContact = function () {
        var grid = $("#GridCMS_CLientContactByCLient").data("kendoGrid");
        var dataSourceData = grid.dataSource.data();

        if ($scope.functiontofindIndexByKeyValue(dataSourceData, "ContactTypeID", $scope.CM00115.ContactTypeID) != null) {
            $.smallBox({
                title: "No Changes have been made!!",
                content: "<i class='fa fa-clock-o'></i> <i>This Type is already has data in this client !!</i>",
                color: "#3276B1",
                iconSmall: "fa fa-times fa-2x fadeInRight animated",
                timeout: 4000
            });
            return;
        }
        $scope.CM00115.ClientID = $scope.CM00130.ClientID;
        $http.post('/CMS_CO_Client/SaveClientContact', { 'CM00115': $scope.CM00115, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
            if (data.Status == true) {
                $.bigBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#739E73",
                    timeout: 8000,
                    icon: "fa fa-check shake animated"
                });
                $scope.GridRefresh('GridCMS_CLientContactByCLient');
                $scope.CM00115 = { Status: 1 };
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
    $scope.UpdateClientContact = function () {
        $scope.CM00115.ClientID = $scope.CM00130.ClientID;
        $http.post('/CMS_CO_Client/UpdateClientContact', { 'CM00115': $scope.CM00115, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
            if (data.Status == true) {
                $.bigBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#739E73",
                    timeout: 8000,
                    icon: "fa fa-check shake animated"
                });
                $scope.GridRefresh('GridCMS_CLientContactByCLient');
                $scope.CM00115 = { Status: 1 };
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
    $scope.DeleteClientContact = function () {
        $.SmartMessageBox({
            title: "Delete Record",
            content: "Are you sure you want to delete this?",
            buttons: '[No][Yes]'
        }, function (ButtonPressed) {
            if (ButtonPressed === "Yes") {
                $http.post('/CMS_CO_Client/DeleteClientContact', { 'CM00115': $scope.CM00115, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
                    if (data.Status == true) {
                        $.bigBox({
                            title: data.Massage,
                            content: data.Description,
                            color: "#739E73",
                            timeout: 8000,
                            icon: "fa fa-check shake animated"
                        });
                        $scope.GridRefresh('GridCMS_CLientContactByCLient');
                        $scope.CM00115 = { Status: 1 };
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
                    color: "#3276B1",
                    iconSmall: "fa fa-times fa-2x fadeInRight animated",
                    timeout: 4000
                });
                $scope.CM00115 = { Status: 1 };
            }
        });
    };
    $scope.ContactTypeChanged = function () {
        var grid = $("#GridCMS_CLientContactByCLient").data("kendoGrid");
        var dataSourceData = [];
        angular.copy(grid.dataSource.data(), dataSourceData);
        var found = $scope.functiontofindObjectByKeyValue(dataSourceData, "ContactTypeID", $scope.CM00115.ContactTypeID);
        if (found != null) {
            $scope.CM00115 = found;
            $scope.CM00115.Status = 2;
            return;
        }
        else {
            $scope.CM00115 = { Status: 1, ContactTypeID: $scope.CM00115.ContactTypeID };
            return;
        }
    };

    //-------------------------- Address Information
    $scope.CM00131 = {};
    $scope.ClientAddresses = [];
    $scope.ClientAddress = function () {
        removeEntityPage($scope.PagesCM00130);
        $scope.LoadPage('ClientAddress');
        $scope.CM00131.Status = 1;
        $http.get('/CMS_CO_Client/ClientAddressTypeFillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey + '&ContactSourceID=1').success(function (data) {
            $scope.ClientAddressTypes = data;
        }).finally(function () { });

    };
    $scope.SaveClientAddress = function () {
        var grid = $("#GridCMS_ClientAddressByCLient").data("kendoGrid");
        var dataSourceData = grid.dataSource.data();

        if ($scope.functiontofindIndexByKeyValue(dataSourceData, "AddressCode", $scope.CM00131.AddressCode) != null) {
            $.smallBox({
                title: "No Changes have been made!!",
                content: "<i class='fa fa-clock-o'></i> <i>This Type is already has data in this client !!</i>",
                color: "#3276B1",
                iconSmall: "fa fa-times fa-2x fadeInRight animated",
                timeout: 4000
            });
            return;
        }
        $scope.CM00131.ClientID = $scope.CM00130.ClientID;
        $http.post('/CMS_CO_Client/SaveClientAddress', { 'CM00131': $scope.CM00131, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
            if (data.Status == true) {
                $.bigBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#739E73",
                    timeout: 8000,
                    icon: "fa fa-check shake animated"
                });
                $scope.GridRefresh('GridCMS_ClientAddressByCLient');
                $scope.CM00131 = { Status: 1 };
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
    $scope.UpdateClientAddress = function () {
        $scope.CM00131.ClientID = $scope.CM00130.ClientID;
        $http.post('/CMS_CO_Client/UpdateClientAddress', { 'CM00131': $scope.CM00131, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
            if (data.Status == true) {
                $.bigBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#739E73",
                    timeout: 8000,
                    icon: "fa fa-check shake animated"
                });
                $scope.GridRefresh('GridCMS_ClientAddressByCLient');
                $scope.CM00131 = { Status: 1 };
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
    $scope.DeleteClientAddress = function () {
        $.SmartMessageBox({
            title: "Delete Record",
            content: "Are you sure you want to delete this?",
            buttons: '[No][Yes]'
        }, function (ButtonPressed) {
            if (ButtonPressed === "Yes") {
                $http.post('/CMS_CO_Client/DeleteClientAddress', { 'CM00131': $scope.CM00131, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
                    if (data.Status == true) {
                        $.bigBox({
                            title: data.Massage,
                            content: data.Description,
                            color: "#739E73",
                            timeout: 8000,
                            icon: "fa fa-check shake animated"
                        });
                        $scope.GridRefresh('GridCMS_ClientAddressByCLient');
                        $scope.CM00131 = { Status: 1 };
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
                    color: "#3276B1",
                    iconSmall: "fa fa-times fa-2x fadeInRight animated",
                    timeout: 4000
                });
                $scope.CM00131 = { Status: 1 };
            }
        });
    };
    $scope.AddressTypeChanged = function () {
        if ($scope.CM00130.PartnerID) {
            var addrData = $scope.CM00131;
            $scope.CM00131 = {};
            $http.get('/CO_Partner/GetPartnerAddressData?KaizenPublicKey=' + sessionStorage.PublicKey + '&AddressCodeType=' + addrData.AddressCodeType + '&PartnerID=' + $scope.CM00130.PartnerID).success(function (data) {
                if (data && data.length > 0) {
                    $scope.CM00131 = data[0];
                    $scope.CM00131.Status = 2;
                } else {
                    $scope.CM00131.Status = 1;
                }
            }).finally(function () {
                $scope.CM00131.AddressCodeType = addrData.AddressCodeType;
            });
        }

    };
    $scope.MainAddressTypeChanged = function () {
        if ($scope.CM00130.PartnerID) {
            $scope.CM00130.MainAddress = {};
            $scope.GetData_ByMainAddressType();
        }
    };

    $scope.GetData_ByMainAddressType = function () {
        $http.get('/CO_Partner/GetPartnerAddressData?KaizenPublicKey=' + sessionStorage.PublicKey + '&AddressCodeType=' + $scope.CM00130.AddressCode + '&PartnerID=' + $scope.CM00130.PartnerID).success(function (data) {
            if (data && data.length > 0) {
                $scope.CM00130.MainAddress = data[0];
            }
        }).finally(function () {

        });
    };

    $scope.BillAddressTypeChanged = function () {
        if ($scope.CM00130.PartnerID) {
            if (!$scope.CM00130.BillAddress)
                $scope.CM00130.BillAddress = {};

            var found = $scope.GetSingle($scope.PartnerAddressTypes, "AddressCodeType", $scope.CM00130.BillAddressCode);
            if (found != null) {
                $scope.CM00130.BillAddress.AddressName = found.AddressTypeName;
            }
            $scope.CM00130.BillAddress = {};
            $scope.GetData_ByBillAddressType();
        }
    };

    $scope.GetData_ByBillAddressType = function () {
        $http.get('/CO_Partner/GetPartnerAddressData?KaizenPublicKey=' + sessionStorage.PublicKey + '&AddressCodeType=' + $scope.CM00130.BillAddressCode + '&PartnerID=' + $scope.CM00130.PartnerID).success(function (data) {
            if (data && data.length > 0) {
                $scope.CM00130.BillAddress = data[0];
            }
        }).finally(function () { });
    };

    $scope.AddNewAddress = function () {
        $scope.CM00131.ClientID = $scope.CM00130.ClientID;
        $scope.ClientAddresses.push($scope.CM00131);
        $scope.CM00131 = { Status: 1 };
    };
    $scope.UpdateAddress = function () {
        var index = $scope.GetSingleIndex($scope.ClientAddresses, "AddressCode", $scope.CM00131.AddressCode);
        $scope.ClientAddresses.splice(index, 1, $scope.CM00131);
        $scope.CM00131 = { Status: 1 };
    };
    $scope.RemoveAddress = function (address, index) {
        $scope.ClientAddresses.splice(index, 1);
    };
    $scope.OnAddressTabSelected = function () {
        //if ($scope.CM00130.Status == 2) {
        //    $scope.GridRefresh('GridCMS_ClientAddressByCLient');
        //}
    };

    //-------------------------- Document Information
    $scope.CM00112 = {};
    $scope.ClientDocument = function () {
        removeEntityPage($scope.PagesCM00130);
        $scope.LoadPage('ClientDocument');
        $scope.CM00112.Status = 1;
        $http.get('/Admin_DocumnetType/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey + '&ContactSourceID=1').success(function (data) {
            $scope.ClientDocumentTypes = data;
        }).finally(function () { });

    };
    $scope.SaveClientDocument = function () {
        var grid = $("#GridCMS_ClientDocumentByCLient").data("kendoGrid");
        var dataSourceData = grid.dataSource.data();
        $scope.CM00112.ClientID = $scope.CM00130.ClientID;
        $http.post('/CMS_CO_Client/SaveClientDocument', { 'CM00112': $scope.CM00112, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
            if (data.Status == true) {
                $.bigBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#739E73",
                    timeout: 8000,
                    icon: "fa fa-check shake animated"
                });
                $scope.GridRefresh('GridCMS_ClientDocumentByCLient');
                $scope.CM00112 = { Status: 1 };
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
    $scope.UpdateClientDocument = function () {
        $scope.CM00112.ClientID = $scope.CM00130.ClientID;
        $http.post('/CMS_CO_Client/UpdateClientDocument', { 'CM00112': $scope.CM00112, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
            if (data.Status == true) {
                $.bigBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#739E73",
                    timeout: 8000,
                    icon: "fa fa-check shake animated"
                });
                $scope.GridRefresh('GridCMS_ClientDocumentByCLient');
                $scope.CM00112 = { Status: 1 };
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
    $scope.DeleteClientDocument = function () {
        $.SmartMessageBox({
            title: "Delete Record",
            content: "Are you sure you want to delete this?",
            buttons: '[No][Yes]'
        }, function (ButtonPressed) {
            if (ButtonPressed === "Yes") {
                $http.post('/CMS_CO_Client/DeleteClientDocument', { 'CM00112': $scope.CM00112, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
                    if (data.Status == true) {
                        $.bigBox({
                            title: data.Massage,
                            content: data.Description,
                            color: "#739E73",
                            timeout: 8000,
                            icon: "fa fa-check shake animated"
                        });
                        $scope.GridRefresh('GridCMS_ClientDocumentByCLient');
                        $scope.CM00112 = { Status: 1 };
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
                    color: "#3276B1",
                    iconSmall: "fa fa-times fa-2x fadeInRight animated",
                    timeout: 4000
                });
                $scope.CM00112 = { Status: 1 };
            }
        });
    };
    $scope.SetClientFileExtension = function (PhotoPath) {
        $scope.CM00112.PhotoExtension = PhotoPath;
    };
    $scope.RemoveClientFileExtension = function (PhotoPath) {
        $scope.CM00112.PhotoExtension = "";
    };
    $scope.ClientDocumentFilePath = function (CM00112) {
        if (angular.isUndefined(CM00112.PhotoExtension)) return "";
        var FilePath;
        if (CM00112.Status == 0)
            FilePath = "ClientDocuments";
        else
            FilePath = "ClientDocumentsTemp";
        return "\\Photo\\" + FilePath + "\\" + CM00112.PhotoExtension;
    };


    //--------------------- Partner Address -------------------------------
    $scope.SavePartnerAddressData = function () {
        if ($scope.CM00130.PartnerID)
            $scope.CM00131.PartnerID = $scope.CM00130.PartnerID;

        $http.post('/CO_Partner/SavePartnerAddressData', { 'CM00131': $scope.CM00131, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
            if (data.Status == true) {
                $.bigBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#739E73",
                    timeout: 4000,
                    icon: "fa fa-check shake animated"
                });
                //$scope.CM00130.MainAddress = $scope.CM00131;
                //$scope.CM00130.BillAddress = $scope.CM00131;
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

    $scope.UpdatePartnerAddressData = function () {
        if ($scope.CM00130.PartnerID)
            $scope.CM00131.PartnerID = $scope.CM00130.PartnerID;

        $http.post('/CO_Partner/UpdatePartnerAddressData', { 'CM00131': $scope.CM00131, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
            if (data.Status == true) {
                $.bigBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#739E73",
                    timeout: 4000,
                    icon: "fa fa-check shake animated"
                });
                //$scope.CM00130.MainAddress = $scope.CM00131;
                //$scope.CM00130.BillAddress = $scope.CM00131;
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
});