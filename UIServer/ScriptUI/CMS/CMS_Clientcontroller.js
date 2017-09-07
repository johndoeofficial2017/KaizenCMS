app.controller('CMS_CO_ClientController', function ($scope, $http) {
    $scope.CM00110 = {};
    $scope.SelectedView = {};
    $scope.PagesCM00110 = [];
    $scope.LoadLookUp = function () {
        $http.get('/CMS_ClientStatus/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.clientStatuses = data;
        }).finally(function () { });
        $http.get('/CMS_ClientClass/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.clientClasses = data;
        }).finally(function () { });

        $http.get('/CMS_CO_Client/GetClientAddressTypes?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.PartnerAddressTypes = data;
        }).finally(function () { });
    };
    $scope.LoadLookUp();

    $http.get('/Sys_View/GetViewsByScreen?ScreenID=10&KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
        $scope.ViewList = data;
    }).finally(function () {
        if ($scope.ViewList.length > 0) {
            $scope.CM00110.ViewID = $scope.ViewList[0].ViewID;
            $scope.SelectedView = $scope.ViewList[0];
            $scope.ViewList.forEach(function (element, index) {
                if (element.IsDefault) {
                    $scope.CM00110.ViewID = element.ViewID;
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
                            $scope.AddCM00110();
                        },
                        menuButtons: [
                            { attributes: { "class": "btn-primary" }, text: "<span class=\"fa fa-plus-circle\"></span> New Client", click: function (e) { $scope.AddCM00110(); } },
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
                     },
                     {
                         type: "button", text: "<span class=\"fa fa-plus-circle\"></span>   Add Parent",
                         click: function (e) { $scope.OpenClientParent(); }
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
            value: $scope.CM00110.ViewID
        };
    });

    $scope.LoadPage = function (ActionName) {
        removeEntityPage($scope.PagesCM00110);
        var URIPath = "/CMS/" + $scope.ToolBar.ActiveScreen.ID + "/" + ActionName + "?KaizenPublicKey=" + sessionStorage.PublicKey;
        var Page = { url: URIPath, ActionName: ActionName };
        $scope.PagesCM00110.push(Page);
    };
    $scope.ViewChanged = function () {
        $scope.CM00110.ViewID = $scope.SelectedView.ViewID;
        $scope.MainGridURL = "/CMS/CMS_CO_Client/MainGrid?ViewID=" + $scope.SelectedView.ViewID + "&KaizenPublicKey=" + sessionStorage.PublicKey;
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
            DS.source = $scope.clientClasses;
        }
        return DS;
    };
    //---------------------------------------------------------------------------------------------
    $scope.LoadCM00110 = function (ClientID) {
        if (angular.isUndefined($scope.CM00110.ClientID)) {
            $http.get('/CMS_CO_Client/GetSingle?KaizenPublicKey=' + sessionStorage.PublicKey + "&ClientID=" + ClientID).success(function (data) {
                $scope.CM00110 = data;


                var addrCodeList = [];

                if ($scope.CM00110.AddressCode != undefined && $scope.CM00110.AddressCode != null) {
                    addrCodeList.push($scope.CM00110.AddressCode);
                    $scope.GetMainAddressData();
                }
                if ($scope.CM00110.BillAddressCode != undefined && $scope.CM00110.BillAddressCode != null) {
                    addrCodeList.push($scope.CM00110.BillAddressCode);
                    $scope.GetBillAddressData();
                }
                if ($scope.CM00110.RemitToAddressCode != undefined && $scope.CM00110.RemitToAddressCode != null) {
                    addrCodeList.push($scope.CM00110.RemitToAddressCode);
                    $scope.GetRemitToAddressData();
                }

                if (addrCodeList.length > 0) {
                    $scope.GetAddressData(addrCodeList);
                }

                $scope.CM00110.Status = 2;

            }).finally(function () {
                $scope.CM00110.Status = 2;
                if ($scope.CM00110.PhotoExtension == null)
                    $scope.link = '/Photo/ClientPhoto/ClientID.jpg';
                else
                    $scope.link = '/Photo/ClientPhoto/' + kaizenTrim($scope.CM00110.ClientID) + "." + kaizenTrim($scope.CM00110.PhotoExtension) + "?c=" + Math.random();

                //$scope.CM00111 = { ClientID: $scope.CM00110.ClientID, Status: 1 };
            });
        }
    };

    $scope.LoadCM00111ByPartnerID = function (ClientID) {
        if ($scope.CM00110.ClientID) {
            $http.get('/CMS_CO_Client/GetClientAddressDataByPartnerID?KaizenPublicKey=' + sessionStorage.PublicKey + "&ClientID=" + ClientID).success(function (data) {
                if (data && data.length > 0) {
                    $scope.CM00131 = data[0];
                    //$scope.CM00130.MainAddress = data[0];
                    //$scope.CM00130.BillAddress = data[0];
                    $scope.CM00131.Status = 2;
                }
            }).finally(function () { });
        }
    };

    $scope.GetAddressData = function (addrCodeList) {
        var paramObj = {};
        var params = {};
        params.KaizenPublicKey = sessionStorage.PublicKey;
        params.ClientID = $scope.CM00110.ClientID.trim();
        params.addressCodeTypeList = addrCodeList;
        paramObj.params = params;

        $http.get('/CMS_CO_Client/GetClientAddressDataByAddressCodeList', paramObj).success(function (data) {
            if (data && data.length > 0) {
                //$scope.CM00131 = data[0];
                //$scope.CM00131.Status = 2;
            }
        }).finally(function () {
            //$scope.CM00131.AddressCodeType = addrData.AddressCodeType;
        });
    };

    $scope.GetBillAddressData = function () {
        $scope.CM00110.BillAddress = {};
        $http.get('/CMS_CO_Client/GetClientAddressData?KaizenPublicKey=' + sessionStorage.PublicKey + '&AddressCode=' + $scope.CM00110.BillAddressCode + '&ClientID=' + $scope.CM00110.ClientID).success(function (data) {
            if (data && data.length > 0) {
                $scope.CM00110.BillAddress = data[0];
            }
        }).finally(function () { });
    }

    $scope.GetRemitToAddressData = function () {
        $scope.CM00110.RemitAddress = {};
        $http.get('/CMS_CO_Client/GetClientAddressData?KaizenPublicKey=' + sessionStorage.PublicKey + '&AddressCode=' + $scope.CM00110.RemitToAddressCode + '&ClientID=' + $scope.CM00110.ClientID).success(function (data) {
            if (data && data.length > 0) {
                $scope.CM00110.RemitAddress = data[0];
            }
        }).finally(function () { });
    }

    $scope.GetMainAddressData = function () {
        $scope.CM00110.MainAddress = {};
        $http.get('/CMS_CO_Client/GetClientAddressData?KaizenPublicKey=' + sessionStorage.PublicKey + '&AddressCode=' + $scope.CM00110.AddressCode + '&ClientID=' + $scope.CM00110.ClientID).success(function (data) {
            if (data && data.length > 0) {
                $scope.CM00110.MainAddress = data[0];
            }
        }).finally(function () {

        });
    }

    $scope.MainAddressTypeChanged = function () {
        if ($scope.CM00110.ClientID) {
            $scope.GetMainAddressData();
        }

    };
    $scope.BillAddressTypeChanged = function () {

        if ($scope.CM00110.ClientID) {
            if (!$scope.CM00110.BillAddress)
                $scope.CM00110.BillAddress = {};

            var found = $scope.GetSingle($scope.ClientAddressTypes, "AddressCodeType", $scope.CM00110.BillAddressCode);
            if (found != null) {
                $scope.CM00110.BillAddress.AddressName = found.AddressTypeName;
            }
            $scope.GetBillAddressData();
        }
    };

    $scope.RemitToAddressTypeChanged = function () {

        if ($scope.CM00110.ClientID) {
            if (!$scope.CM00110.RemitAddress)
                $scope.CM00110.RemitAddress = {};

            var found = $scope.GetSingle($scope.ClientAddressTypes, "AddressCodeType", $scope.CM00110.RemitToAddressCode);
            if (found != null) {
                $scope.CM00110.RemitAddress.AddressName = found.AddressTypeName;
            }
            $scope.GetRemitToAddressData();
        }
    };

    $scope.AddCM00110 = function () {
        $scope.LoadPage('Create');
        $scope.$apply();
        $scope.Clear();
        $scope.CM00110.Status = 1;
        //$scope.CM00111.Status = 1;
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
    $scope.EditCM00110 = function (ClientID) {
        $scope.LoadPage('Create');
        $scope.LoadCM00110(ClientID);
        $scope.AddressPathUrl = "/CMS_CO_Client/ClientAddress?KaizenPublicKey=" + sessionStorage.PublicKey;
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
    $scope.SaveData = function () {
        $http.post('/CMS_CO_Client/SaveData', { 'CM00110': $scope.CM00110, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
            if (data.Status == true) {
                if ($scope.ClientAddresses.length > 0) {
                    $http.post('/CMS_CO_Client/SaveClientAddresses', { 'CM00111': $scope.ClientAddresses, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
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
        $http.post('/CMS_CO_Client/UpdateData', { 'CM00110': $scope.CM00110, 'KaizenPublicKey': sessionStorage.PublicKey, 'PhotoChanged': $scope.PhotoChanged }).success(function (data) {
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
        $scope.CM00110 = {};
        $scope.CM00111 = { status: 0 };
        $scope.CM00112 = { status: 0 };
        $scope.CM00115 = { status: 0 };
        $scope.CM00110.Status = 1;
        $scope.CM00110.StatusID = '1'
        $scope.CM00110.IsActive = true;;
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
                $http.post('/CMS_CO_Client/DeleteData', { 'CM00110': $scope.CM00110, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
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
        $scope.PagesCM00110 = [];

        $scope.ClientAddresses = [];
    };

    $scope.PhotoChanged = false; // 1= new ; 2 Edit
    $scope.OnClassChanged = function () {
        
        $http.get('/CMS_CO_Client/GetNextClient?KaizenPublicKey=' + sessionStorage.PublicKey,
            { params: { CUSTCLAS: $scope.CM00110.CUSTCLAS } })
        .success(function (data) {
            $scope.CM00110.ClientID = data;
        })
        .finally(function () {
        });
    };
    $scope.ActiveChanged = function () {
        if ($scope.CM00110.IsActive) {
            $scope.CM00110.IsHold = false;
        } else {
            $scope.CM00110.IsHold = true;
        }
    };
    $scope.HoldChanged = function () {
        if ($scope.CM00110.IsHold) {
            $scope.CM00110.IsActive = false;
        } else {
            $scope.CM00110.IsActive = true;
        }
    };

    $scope.ClientBack = function (client) {
        if (client != null) {
            switch ($scope.CurrentControl) {
                case 'ParentClient':
                    $scope.CM00110.ParentClientID = client.ClientID;
                    break;
                case 'ClientAddress':
                   
                    $scope.CM00110 = client;
                    $scope.CM00111 = { ClientID: client.ClientID, Status: 1 };
                    $scope.GridRefresh('GridCMS_ClientAddressByCLient');
                    break;
                case 'ClientDocument':
                    $scope.CM00110 = client;
                    $scope.CM00112 = { ClientID: client.ClientID, Status: 1 };
                    $scope.GridRefresh('GridCMS_ClientDocumentByCLient');
                    break;
                case 'ClientContact':
                    $scope.CM00110 = client;
                    $scope.CM00115 = { ClientID: client.ClientID, Status: 1 };
                    $scope.GridRefresh('GridCMS_CLientContactByCLient');
                    break;
                case 'ParentClientAssign':
                    $scope.CM00110.ParentClientID = client.ClientID;
                    $scope.LoadChildClients();
                    break;
                case 'ChildClient':
                    $scope.CM00110.ClientID = client.ClientID;
                    $scope.tempCM00110 = client;
                    $scope.tempCM00110.ParentClientID = $scope.CM00110.ParentClientID;
                    break;
            }
        }
    };
    $scope.ClientContactBack = function (contact) {
        if (contact != null) {
            $scope.CM00110.ContactTypeID = contact.ContactTypeID;
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
        $scope.CM00110.PhotoExtension = PhotoPath;
        $scope.link = '/Photo/ClientPhotoTemp/' + kaizenTrim(PhotoPath);
        $scope.PhotoChanged = true;
        // $scope.$apply();
    };
    $scope.RemovePhotoExtension = function (PhotoPath) {
        $scope.CM00110.PhotoExtension = "";
    };

    //-------------------------- Contact Information
    $scope.CM00115 = {};
    $scope.ClientContact = function () {
        removeEntityPage($scope.PagesCM00110);
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
        $scope.CM00115.ClientID = $scope.CM00110.ClientID;
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
        $scope.CM00115.ClientID = $scope.CM00110.ClientID;
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
    $scope.CM00111 = {};
    $scope.ClientAddresses = [];
    $scope.ClientAddress = function () {
        removeEntityPage($scope.PagesCM00110);
        $scope.LoadPage('ClientAddress');
        $scope.CM00111.Status = 1;
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
    $scope.SaveClientAddress = function () {
        //var grid = $("#GridCMS_ClientAddressByCLient").data("kendoGrid");
        //var dataSourceData = grid.dataSource.data();

        //if ($scope.functiontofindIndexByKeyValue(dataSourceData, "AddressCode", $scope.CM00111.AddressCode) != null) {
        //    $.smallBox({
        //        title: "No Changes have been made!!",
        //        content: "<i class='fa fa-clock-o'></i> <i>This Type is already has data in this client !!</i>",
        //        color: "#3276B1",
        //        iconSmall: "fa fa-times fa-2x fadeInRight animated",
        //        timeout: 4000
        //    });
        //    return;
        //}
        $scope.CM00111.ClientID = $scope.CM00110.ClientID;
        $http.post('/CMS_CO_Client/SaveClientAddress', { 'CM00111': $scope.CM00111, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
            if (data.Status == true) {
                $.bigBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#739E73",
                    timeout: 8000,
                    icon: "fa fa-check shake animated"
                });
                $scope.GridRefresh('GridCMS_ClientAddressByCLient');
                $scope.CM00111 = { Status: 1 };
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
        $scope.CM00111.ClientID = $scope.CM00110.ClientID;
        $http.post('/CMS_CO_Client/UpdateClientAddress', { 'CM00111': $scope.CM00111, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
            if (data.Status == true) {
                $.bigBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#739E73",
                    timeout: 8000,
                    icon: "fa fa-check shake animated"
                });
                $scope.GridRefresh('GridCMS_ClientAddressByCLient');
                $scope.CM00111 = { Status: 1 };
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
                $http.post('/CMS_CO_Client/DeleteClientAddress', { 'CM00111': $scope.CM00111, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
                    if (data.Status == true) {
                        $.bigBox({
                            title: data.Massage,
                            content: data.Description,
                            color: "#739E73",
                            timeout: 8000,
                            icon: "fa fa-check shake animated"
                        });
                        $scope.GridRefresh('GridCMS_ClientAddressByCLient');
                        $scope.CM00111 = { Status: 1 };
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
                $scope.CM00111 = { Status: 1 };
            }
        });
    };
    $scope.AddressTypeChanged = function () {
      
        if ($scope.CM00110.ClientID) {
            var addrData = $scope.CM00111;
            $scope.CM00111 = {};
            $http.get('/CMS_CO_Client/GetClientAddressData?KaizenPublicKey=' + sessionStorage.PublicKey + '&AddressCode=' + addrData.AddressCode + '&ClientID=' + $scope.CM00110.ClientID).success(function (data) {
                if (data && data.length > 0) {
                    $scope.CM00111 = data[0];
                    $scope.CM00111.Status = 2;
                } else {
                    $scope.CM00111.Status = 1;
                }
            }).finally(function () {
                $scope.CM00111.AddressCode = addrData.AddressCode;
            });
        }

        //if ($scope.CM00110.Status == 1) {
        //    var found = $scope.GetSingle($scope.ClientAddresses, "AddressCode", $scope.CM00111.AddressCode);
        //    if (found != null) {
        //        $scope.CM00111 = found;
        //        $scope.CM00111.Status = 2;
        //    }
        //    else {
        //        $scope.CM00111 = { AddressCode: $scope.CM00111.AddressCode, Status: 1 };
        //        var addressName = $scope.GetSingle($scope.ClientAddressTypes, "AddressCodeType", $scope.CM00111.AddressCode);
        //        $scope.CM00111.AddressName = addressName.AddressTypeName;
        //    }
        //}
        //else {
        //    var grid = $("#GridCMS_ClientAddressByCLient").data("kendoGrid");
        //    var dataSourceData = [];
        //    angular.copy(grid.dataSource.data(), dataSourceData);
        //    var found = $scope.functiontofindObjectByKeyValue(dataSourceData, "AddressCode", $scope.CM00111.AddressCode);
        //    if (found != null) {
        //        $scope.CM00111 = found;
        //        $scope.CM00111.Status = 2;
        //        return;
        //    }
        //    else {
        //        var addressName = $scope.GetSingle($scope.ClientAddressTypes, "AddressCodeType", $scope.CM00111.AddressCode);
        //        $scope.CM00111 = { Status: 1, AddressCode: $scope.CM00111.AddressCode, AddressName: addressName.AddressTypeName };
        //        return;
        //    }
        //}
    };
    $scope.AddNewAddress = function () {
        $scope.CM00111.ClientID = $scope.CM00110.ClientID;
        $scope.ClientAddresses.push($scope.CM00111);
        $scope.CM00111 = { Status: 1 };
    };
    $scope.UpdateAddress = function () {
        var index = $scope.GetSingleIndex($scope.ClientAddresses, "AddressCode", $scope.CM00111.AddressCode);
        $scope.ClientAddresses.splice(index, 1, $scope.CM00111);
        $scope.CM00111 = { Status: 1 };
    };
    $scope.RemoveAddress = function (address, index) {
        $scope.ClientAddresses.splice(index, 1);
    };
    $scope.OnAddressTabSelected = function () {
        if ($scope.CM00110.Status == 2) {
            $scope.GridRefresh('GridCMS_ClientAddressByCLient');
        }
    };


    //-------------------------- Document Information
    $scope.CM00112 = {};
    $scope.ClientDocument = function () {
        removeEntityPage($scope.PagesCM00110);
        $scope.LoadPage('ClientDocument');
        $scope.CM00112.Status = 1;
        $http.get('/Admin_DocumnetType/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey + '&ContactSourceID=1').success(function (data) {
            $scope.ClientDocumentTypes = data;
        }).finally(function () { });

    };
    $scope.SaveClientDocument = function () {
        var grid = $("#GridCMS_ClientDocumentByCLient").data("kendoGrid");
        var dataSourceData = grid.dataSource.data();
        $scope.CM00112.ClientID = $scope.CM00110.ClientID;
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
        $scope.CM00112.ClientID = $scope.CM00110.ClientID;
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


    //---------------------------------  Add Client Parent ----------------------------------
    $scope.OpenClientParent = function () {
        removeEntityPage($scope.PagesCM00110);
        $scope.LoadPage('ClientParent');
        $http.get('/Admin_DocumnetType/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey + '&ContactSourceID=1').success(function (data) {
            $scope.ClientDocumentTypes = data;
        }).finally(function () { });

    };

    $scope.LoadChildClients = function () {
        if ($scope.CM00110 && $scope.CM00110.ParentClientID) {
            $http.get('/CMS_CO_Client/GetChildClientsByParentID?KaizenPublicKey=' + sessionStorage.PublicKey + '&ParentClientID=' + $scope.CM00110.ParentClientID).success(function (data) {
                $scope.ClientParentList = data;
            }).finally(function () { });
        }
    };

    $scope.UpdateClientData = function () {
        $http.post('/CMS_CO_Client/UpdateClientData', { 'ParentClientID': $scope.CM00110.ParentClientID, 'ClientID': $scope.CM00110.ClientID, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
            if (data.Status == true) {
                $.bigBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#739E73",
                    timeout: 8000,
                    icon: "fa fa-check shake animated"
                });

                $scope.ClientParentList.push($scope.tempCM00110);
                $scope.CM00110.ClientID = "";
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

    $scope.DeleteParentClientID = function (clientParent, index) {

        if (clientParent && clientParent.ClientID != '') {
            $.SmartMessageBox({
                title: "Delete Record",
                content: "Are you sure?",
                buttons: '[No][Yes]'
            }, function (ButtonPressed) {
                if (ButtonPressed === "Yes") {
                    //if (clientParent.status == 1)
                    //    $scope.clientParents.splice(index, 1);
                    //else
                    //    clientParent.status = 3;

                    $http({
                        url: '/CMS_CO_Client/DeleteParentClientID?KaizenPublicKey=' + sessionStorage.PublicKey,
                        method: "POST",
                        data: { ClientID: clientParent.ClientID },
                        dataType: "json",
                        headers: {
                            'Content-Type': 'application/json; charset=utf-8'
                        }
                    }).success(function (data) {
                        if (data.Status == true) {
                            //Notify(data.Massage, 'bottom-right', '5000', 'success', 'fa-check', true);
                            $.bigBox({
                                title: data.Massage,
                                content: data.Description,
                                color: "#739E73",
                                timeout: 8000,
                                icon: "fa fa-bolt swing animated"
                            });

                            $scope.ClientParentList.splice(index, 1);
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
        }
    };
});