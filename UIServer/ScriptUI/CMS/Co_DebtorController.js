app.controller('Co_DebtorController', function ($scope, $http) {
    $scope.CM00100 = {};
    $scope.SelectedLookUp = {};
    $scope.LoadLookUp = function () {
        $http.get('/HR_Set_Marital/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey)
            .success(function (data) {
            $scope.Maritals = data;
        }).finally(function () { });
    };
    $scope.LoadLookUp();
 
    $scope.CM00102 = {};
    $scope.CM00100.MainAddress = {};
    $scope.CM00100.BillAddress = {};
    $scope.CM00100.ShipAddress = {};
    $scope.CM00100.StatementAddress = {};
    $scope.SelectedView = {};
    $scope.PagesCM00100 = [];
    $scope.PhotoChanged = false;
    $scope.SignatureChanged = false;
    $http.get('/Sys_View/GetViewsByScreen?ScreenID=6&KaizenPublicKey=' + sessionStorage.PublicKey)
        .success(function (data) {
        $scope.DebtorViews = data;
    })
    .finally(function () {
        $scope.DebtorAddressCodeType();
        if ($scope.DebtorViews.length > 0) {
            //$scope.CM00100.ViewID = $scope.DebtorViews[0].ViewID;
            $scope.SelectedLookUp.SelectedDebtorView = $scope.DebtorViews[0];
            $scope.DebtorViews.forEach(function (element, index) {
                if (element.IsDefault) {
                    $scope.SelectedLookUp.SelectedDebtorView = element;
                    return;
                }
            });
            $scope.DebtorMaintoolbarOptions = {
                items: [
                       {
                           type: "button",
                           text: "<span class=\"fa fa-plus-circle\"></span> New Debtor",
                           attributes: { "class": "btn-primary" },
                           click: function (e) {
                               $scope.AddCM00100();
                           }
                       },
                       {
                           type: "button",
                           text: "<span class=\"fa fa-upload\"></span> Upload Debtor",
                           attributes: { "class": "btn-primary" },
                           click: function (e) {
                               $scope.$apply(function () {
                                   $scope.UploadDebtor();
                               });
                           }
                       },
                       {
                           type: "splitButton",
                           text: "GO",
                           menuButtons: [
                                {
                                    type: "button", text: "<span class=\"fa fa-user-md\"></span> Contact",
                                    click: function (e) {
                                        $scope.$apply(function () {
                                            $scope.DebtorContact();
                                        })
                                    }
                                },
                              {
                                  type: "button", text: "<span class=\"fa fa-building-o\"></span> Address",
                                  click: function (e) {
                                      $scope.$apply(function () {
                                          $scope.DebtorAddress();
                                      })
                                  }
                              },
                              {
                                  type: "button", text: "<span class=\"fa fa-file\"></span> Document",
                                  click: function (e) {
                                      $scope.$apply(function () {
                                          $scope.DebtorDocument();
                                      })
                                  }
                              }
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
                     {
                         type: "button", text: " Filter", spriteCssClass: "k-tool-icon k-filter",
                         click: function (e) {
                             $scope.OpenFilterWindow("GridCo_Debtor", "Co_Debtor");
                         }
                     },
                         { type: "separator" },
                         { template: "<label>View:</label>" },
                         {
                             template: "<select kendo-drop-down-list k-ng-model='SelectedLookUp.SelectedDebtorView' k-on-change='DebtorViewChanged()' k-options='Co_DebtorViewIDOptions' style='width: 150px;'></select>",
                             overflow: "never"
                         },
                          //{
                          //    template: "<select kendo-drop-down-list k-ng-model='SelectedLookUp.AddressTypes' k-on-change='DebtorViewChanged()' k-options='DebtorAddressCodeTypeOptions' style='width: 200px;'></select>",
                          //    overflow: "never"
                          //},
                       { type: "separator" },
                       {
                           type: "button",
                           text: "<span class=\"fa fa-envelope\"></span> Send Email",
                           attributes: { "class": "k-button btn-warning" },
                           click: function (e) {
                               $scope.SendEmails();
                           }
                       }
                ]
              , resizable: true
            };
            $scope.Co_DebtorViewIDOptions = {
                filter: "startswith",
                model: "SelectedView",
                dataTextField: "ViewName",
                dataValueField: "ViewID",
                dataSource: $scope.DebtorViews
            };
            $scope.DebtorViewChanged();
        }
        else {
            $.bigBox({
                title: "Missing User Setup",
                content: "Missing User Setup, call SYstem Administartor",
                color: "#C46A69",
                timeout: 4000,
                icon: "fa fa-bolt swing animated"
            });
        }
    });
    $scope.DebtorViewChanged = function () {
        //var AddressCode;
        //alert($scope.SelectedLookUp.AddressTypes);
        //if (angular.isUndefined($scope.SelectedLookUp.AddressTypes))
        //    AddressCode = "KaizenSystem";
        //else
        //    AddressCode = $scope.SelectedLookUp.AddressTypes.AddressCode;
        //alert(AddressCode);
        //$scope.CM00100.ViewID = $scope.SelectedLookUp.SelectedDebtorView.ViewID;
        $scope.DebtorMainGridURL = "/CMS/Co_Debtor/MainGrid?ViewID=" + $scope.SelectedLookUp.SelectedDebtorView.ViewID + "&KaizenPublicKey=" + sessionStorage.PublicKey;
        //alert($scope.DebtorMainGridURL);
    };
    $scope.LoadCM00100 = function (DebtorID) {
        if (angular.isUndefined($scope.CM00100.DebtorID)) {
            $http.get('/Co_Debtor/GetSingle?KaizenPublicKey=' + sessionStorage.PublicKey + "&DebtorID=" + DebtorID).success(function (data) {
                $scope.CM00100 = data;
                if (data.CM00102 != null) {
                    $scope.CM00102 = data.CM00102;
                }
            }).finally(function () {
                $scope.CM00100.Status = 2;
                if ($scope.CM00100.BirthDate != null && $scope.CM00100.BirthDate != '') {
                    $scope.CM00100.BirthDate = kendoparsetoDate($scope.CM00100.BirthDate);
                }
                if ($scope.CM00100.CPRExpiryDate != null && $scope.CM00100.CPRExpiryDate != '') {
                    $scope.CM00100.CPRExpiryDate = kendoparsetoDate($scope.CM00100.CPRExpiryDate);
                }
                if ($scope.CM00100.CPRIssueDate != null && $scope.CM00100.CPRIssueDate != '') {
                    $scope.CM00100.CPRIssueDate = kendoparsetoDate($scope.CM00100.CPRIssueDate);
                }
                if ($scope.CM00100.PassportExpiryDate != null && $scope.CM00100.PassportExpiryDate != '') {
                    $scope.CM00100.PassportExpiryDate = kendoparsetoDate($scope.CM00100.PassportExpiryDate);
                }
                if ($scope.CM00100.PassportIssueDate != null && $scope.CM00100.PassportIssueDate != '') {
                    $scope.CM00100.PassportIssueDate = kendoparsetoDate($scope.CM00100.PassportIssueDate);
                }
                if ($scope.CM00100.VisaExpiryDate != null && $scope.CM00100.VisaExpiryDate != '') {
                    $scope.CM00100.VisaExpiryDate = kendoparsetoDate($scope.CM00100.VisaExpiryDate);
                }
                if ($scope.CM00100.ResidentPermitExpiryDate != null && $scope.CM00100.ResidentPermitExpiryDate != '') {
                    $scope.CM00100.ResidentPermitExpiryDate = kendoparsetoDate($scope.CM00100.ResidentPermitExpiryDate);
                }

                if ($scope.CM00100.PhotoExtension == null)
                    $scope.link = '/Photo/DebtorPhoto/DebtorID.jpg';
                else
                    $scope.link = '/Photo/DebtorPhoto/' + kaizenTrim($scope.CM00100.DebtorID) + "." + kaizenTrim($scope.CM00100.PhotoExtension) + "?c=" + Math.random();
                if ($scope.CM00100.SignatureExtension == null)
                    $scope.Signaturelink = '/Photo/DebtorSignature/DebtorSignature.png';
                else
                    $scope.Signaturelink = '/Photo/DebtorSignature/' + kaizenTrim($scope.CM00100.DebtorID) + "." + kaizenTrim($scope.CM00100.SignatureExtension) + "?c=" + Math.random();

                $scope.CM00104 = { DebtorID: $scope.CM00100.DebtorID, Status: 1 };
            });
        }
    };
    $scope.LoadCM00100Page = function (ActionName) {
        removeEntityPage($scope.PagesCM00100);
        var URIPath = "/CMS/" + $scope.ToolBar.ActiveScreen.ID + "/" + ActionName + "?KaizenPublicKey=" + sessionStorage.PublicKey;
        var Page = { url: URIPath, ActionName: ActionName };
        $scope.PagesCM00100.push(Page);
    };

    //------------------------Filter
    $scope.MYFilter.get = function (field) {
        var DS = {};
        if (field == 'CaseStatusID') {
            DS.key = "CaseStatusname";
            DS.source = $scope.CaseStatuses;
        }
        if (field == 'ActionPlanID') {
            DS.key = "ActionPlanName";
            DS.source = $scope.Actionplans;
        }
        if (field == 'JecketsID') {
            DS.key = "JecketName";
            DS.source = $scope.Jeckets;
        }
        if (field == 'TRXTypeID') {
            DS.key = "TrxTypeName";
            DS.source = $scope.TRXTypes;
        }
        //alert(JSON.stringify($scope.Filter.source));
        return DS;
    };
   

    $scope.AddCM00100 = function () {
        $scope.LoadCM00100Page('Create');
        $scope.$apply();
        $scope.Clear();
        $scope.CM00100.Status = 1;
        $scope.CM00104.Status = 1;
        $scope.CM00100.IsActive = true;
        $scope.CM00100.DebtorStatusID = 1;
        $http.get('/Adm_Country/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.Countries = data;
        }).finally(function () { });
        $http.get('/Adm_City/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.Cities = data;
        }).finally(function () { });
        $http.get('/Co_Debtor/DebtorAddressTypeFillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey + '&ContactSourceID=1').success(function (data) {
            $scope.DebtorAddressTypes = data;
        }).finally(function () { });
    };
    $scope.EditCM00100 = function (DebtorID) {
        $scope.LoadCM00100Page('Create');
        $scope.LoadCM00100(DebtorID);
        $scope.AddressPathUrl = "/Co_Debtor/DebtorAddress?KaizenPublicKey=" + sessionStorage.PublicKey;
        $http.get('/Adm_Country/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.Countries = data;
        }).finally(function () { });
        $http.get('/Adm_City/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.Cities = data;
        }).finally(function () { });
        $http.get('/Co_Debtor/DebtorAddressTypeFillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey + '&ContactSourceID=1').success(function (data) {
            $scope.DebtorAddressTypes = data;
        }).finally(function () { });
        $scope.CM00100.Status = 2;
    };
    $scope.SaveData = function () {
        $scope.CM00102.DebtorID = $scope.CM00100.DebtorID;
        $scope.CM00100.CM00102 = $scope.CM00102;
        $http.post('/Co_Debtor/SaveData', { 'CM00100': $scope.CM00100, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
            if (data.Status == true) {
                if ($scope.DebtorAddresses.length > 0) {
                    $http.post('/Co_Debtor/SaveDebtorAddresses', { 'CM00104': $scope.DebtorAddresses, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
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
                    timeout: 3000,
                    icon: "fa fa-bolt swing animated"
                });
            }
        }).error(function (data, status, headers, config) {
            $.bigBox({
                title: data.Massage,
                content: data.Description,
                color: "#C46A69",
                timeout: 3000,
                icon: "fa fa-bolt swing animated"
            });
        });
    };
    $scope.UpdateData = function () {
        $scope.CM00102.DebtorID = $scope.CM00100.DebtorID;
        $scope.CM00100.CM00102 = $scope.CM00102;
        $http.post('/Co_Debtor/UpdateData', { 'CM00100': $scope.CM00100, 'PhotoChanged': $scope.PhotoChanged, 'SignatureChanged': $scope.SignatureChanged, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
            if (data.Status == true) {
                $.bigBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#739E73",
                    timeout: 3000,
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
                    timeout: 3000,
                    icon: "fa fa-bolt swing animated"
                });
            }
        }).error(function (data, status, headers, config) {
            $.bigBox({
                title: data.Massage,
                content: data.Description,
                color: "#C46A69",
                timeout: 3000,
                icon: "fa fa-bolt swing animated"
            });
        });
    };
    $scope.Clear = function () {
        $scope.CM00100 = {};
        $scope.CM00100.MainAddress = {};
        $scope.CM00100.BillAddress = {};
        $scope.CM00100.ShipAddress = {};
        $scope.CM00100.StatementAddress = {};
        $scope.CM00104 = { status: 0 };
        $scope.CM00101 = { status: 0 };
        $scope.CM00106 = { status: 0 };
        $scope.Message = {};
        $scope.MS_000003 = {};


        $scope.addresslist = [];
        $scope.DebtorsAddress = [];
        $scope.MS_000004 = [];

        $scope.InsertedAddresses = [];
        $scope.UpdatedAddresses = [];
        $scope.DeletedAddresses = [];
        $scope.DebtorAddresses = [];

        $scope.InsertedContacts = [];
        $scope.UpdatedContacts = [];
        $scope.DeletedContacts = [];
        $scope.DebtorContacts = [];

        $scope.InsertedDocuments = [];
        $scope.UpdatedDocuments = [];
        $scope.DeletedDocuments = [];
        $scope.Debtordocuments = [];

        $scope.CM00100.Status = 1;
        $scope.CM00100.IsActive = true;
        $scope.CM00100.DebtorStatusID = 1;
        $scope.link = '/Photo/DebtorPhoto/DebtorID.jpg';
        $scope.Signaturelink = '/Photo/DebtorSignature/DebtorSignature.png';
    };
    $scope.Print = function () {
        if ($scope.CM00100.PrintType == "PDF") {
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
                    fileName: "Debtor ID " + $scope.CM00100.DebtorID.trim() + ".pdf",
                    proxyURL: "@Url.Action('Pdf_Export_Save')"
                });
            });
        }
        else if ($scope.CM00100.PrintType == "Image") {
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
                    fileName: "Debtor ID " + $scope.CM00100.DebtorID.trim() + ".png",
                    proxyURL: "@Url.Action('Pdf_Export_Save')"
                });
            });
        }
        else if ($scope.CM00100.PrintType == "SVG") {
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
                    fileName: "Debtor ID " + $scope.CM00100.DebtorID.trim() + ".svg",
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
                $http.post('/Co_Debtor/DeleteData', { 'CM00100': $scope.CM00100, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
                    if (data.Status == true) {
                        $.bigBox({
                            title: data.Massage,
                            content: data.Description,
                            color: "#739E73",
                            timeout: 3000,
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
                            timeout: 3000,
                            icon: "fa fa-bolt swing animated"
                        });
                    }
                }).error(function (data, status, headers, config) {
                    $.bigBox({
                        title: data.Massage,
                        content: data.Description,
                        color: "#C46A69",
                        timeout: 3000,
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
        $scope.PagesCM00100 = [];

        $scope.DebtorAddresses = [];
    };

    $scope.ClassChanged = function () {
        $http.get('/Co_Debtor/GetNextDebtor?KaizenPublicKey=' + sessionStorage.PublicKey, { params: { CUSTCLAS: $scope.CM00100.CUSTCLAS } }).success(function (data) {
            $scope.CM00100.DebtorID = data;
        }).finally(function () { });
    };
    $scope.ActiveChanged = function () {
        if ($scope.CM00100.IsActive) {
            $scope.CM00100.IsHold = false;
        } else {
            $scope.CM00100.IsHold = true;
        }
    };
    $scope.HoldChanged = function () {
        if ($scope.CM00100.IsHold) {
            $scope.CM00100.IsActive = false;
        } else {
            $scope.CM00100.IsActive = true;
        }
    };

    $scope.DebtorAddressCodeBack = function (addresscode) {
        if (addresscode != null) {
            switch ($scope.CurrentControl) {
                case 'MainAddress':
                    $scope.CM00100.AddressCode = addresscode.AddressCode;
                    break;
                case 'BillAddress':
                    $scope.CM00100.BillTo = addresscode.AddressCode;
                    break;
                case 'ShipAddress':
                    $scope.CM00100.ShipTo = addresscode.AddressCode;
                    break;
                case 'StatementAddress':
                    $scope.CM00100.StatementTo = addresscode.AddressCode;
                    break;
            }
        }
    };
    $scope.NationalityBack = function (nationality) {
        if (nationality != null) {
            $scope.CM00100.NationalityID = nationality.NationalityID;
            $scope.CM00100.NationalityName = nationality.NationalityName;
        }
    };
    $scope.DebtorBack = function (debtor) {
        if (debtor != null) {
            switch ($scope.CurrentControl) {
                case 'ParentDebtor':
                    $scope.CM00100.ParentDebtor = debtor.DebtorID;
                    break;
                case 'DebtorAddress':
                    $scope.CM00100 = debtor;
                    $scope.CM00104 = { DebtorID: debtor.DebtorID, Status: 1 };
                    $scope.GridRefresh('GridCo_DebtorAddressByDebtor');
                    break;
                case 'DebtorDocument':
                    $scope.CM00100 = debtor;
                    $scope.CM00101 = { DebtorID: debtor.DebtorID, Status: 1 };
                    $scope.GridRefresh('GridCo_DebtorDocumentByDebtor');
                    break;
                case 'DebtorContact':
                    $scope.CM00100 = debtor;
                    $scope.CM00106 = { DebtorID: debtor.DebtorID, Status: 1 };
                    $scope.GridRefresh('GridCo_DebtorContactByDebtor');
                    break;
            }
        }
    };
    $scope.DebtorAddressBack = function (address) {
        if (address != null) {
            switch ($scope.CurrentControl) {
                case 'MainAddress':
                    $scope.CM00100.AddressCode = address.AddressCode;
                    $scope.CM00100.MainAddress = address;
                    break;
                case 'BillAddress':
                    $scope.CM00100.BillTo = address.AddressCode;
                    $scope.CM00100.BillAddress = address;
                    break;
                case 'StatementAddress':
                    $scope.CM00100.StatementTo = address.AddressCode;
                    $scope.CM00100.StatementAddress = address;
                    break;
            }
        }
    };

    // Setting Photo path extension
    $scope.SetPhotoExtension = function (PhotoPath) {
        $scope.CM00100.PhotoExtension = PhotoPath;
        $scope.link = '/Photo/DebtorPhotoTemp/' + kaizenTrim(PhotoPath);
        $scope.PhotoChanged = true;
    };
    $scope.RemovePhotoExtension = function (PhotoPath) {
        $scope.CM00100.PhotoExtension = "";
    };

    $scope.SetSignatureExtension = function (PhotoPath) {
        $scope.CM00100.SignatureExtension = PhotoPath;
        $scope.Signaturelink = '/Photo/DebtorSignatureTemp/' + kaizenTrim(PhotoPath);
        $scope.SignatureChanged = true;
    };
    $scope.RemoveSignatureExtension = function (PhotoPath) {
        $scope.CM00100.SignatureExtension = "";
    };

    $scope.Message = {};
    $scope.DebtorsAddress = [];
    $scope.MS_000004 = [];
    $scope.SendMessage = function () {
        $scope.LoadCM00100Page('SendMessage')
        $scope.Clear();
        $scope.CM00100.Status = 1;
        $http.get('/Admin_Massage/FillSourceDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.MessageSources = data;
        }).finally(function () { });
        $http.get('/Admin_Massage/FillMessageTypeDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.MessageTypes = data;
        }).finally(function () { });
        $http.get('/Co_DebtorAddressType/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.AddressTypes = data;
        }).finally(function () {
        });

    };
    $scope.OnTemplateChanged = function () {
        $scope.Templates.forEach(function (element, index) {
            if (element.TemplateID === $scope.CM00104.TemplateID) {
                $scope.CM00104.TemplateContant = element.TemplateContant
            }
        })

    };
    $scope.OnAddressTypeChanged = function () {
        $http.get('/CMS_Set_AddressCodeTypeDebtor/GetByAddressCode?KaizenPublicKey=' + sessionStorage.PublicKey, { params: { AddressCode: $scope.Message.AddressCode } }).success(function (data) {
            $scope.DebtorsAddress = data;
        }).finally(function () { });
    };
    $scope.OnSourceChanged = function () {
        if (angular.isDefined($scope.Message.MsgTypeID)) {
            $http.get('/Admin_MassageTemplate/GetBySourceAndType?KaizenPublicKey=' + sessionStorage.PublicKey, { params: { MsgSourceID: $scope.Message.MsgSourceID, MsgTypeID: $scope.Message.MsgTypeID } }).success(function (data) {
                $scope.MsgTemplates = data;
            }).finally(function () { });
        }
    };
    $scope.OnTypeChanged = function () {
        if (angular.isDefined($scope.Message.MsgSourceID)) {
            $http.get('/Admin_MassageTemplate/GetBySourceAndType?KaizenPublicKey=' + sessionStorage.PublicKey, { params: { MsgSourceID: $scope.Message.MsgSourceID, MsgTypeID: $scope.Message.MsgTypeID } }).success(function (data) {
                $scope.MsgTemplates = data;
            }).finally(function () { });
        }
    };
    $scope.SendTextMessage = function () {
        for (var i = 0; i < $scope.DebtorsAddress.length; i++) {
            var item = $scope.DebtorsAddress[i];
            if (item.isSelected) {
                var insert_MS_000004 = {
                    ReceiverID: item.DebtorID,
                    Receiver01: item.MobileNo1,
                    Receiver02: item.MobileNo2,
                    Receiver03: item.MobileNo3,
                    Receiver04: item.MobileNo4,
                };
                $scope.MS_000004.push(insert_MS_000004);
            }
        }
        $scope.MS_000003 = {
            MsgTemplateID: $scope.Message.MsgTemplateID,
            TrxName: $scope.Message.MsgTemplateName,
            TrxDescription: $scope.Message.MsgTemplateContant,
            MS_000004: $scope.MS_000004
        };
        $http.post('/Admin_Massage/SendMessage',
             { 'MS_000003': $scope.MS_000003, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {

             }).finally(function () { });
    };

    // Send Emails
    $scope.SendEmails = function () {
        removeEntityPage($scope.PagesCM00100);
        $scope.FullEditorTools = [
{
    name: "insertHtml",
    items: [
        { text: "Case Reference", value: "<span class='marker'>%CaseRef%</span>&nbsp" },
        { text: "Currency Code", value: "<span class='marker'>%CurrencyCode%</span>&nbsp" }
    ]
},
"bold",
"italic",
"underline",
"strikethrough",
"justifyLeft",
"justifyCenter",
"justifyRight",
"justifyFull",
"insertUnorderedList",
"insertOrderedList",
"indent",
"outdent",
"createLink",
"unlink",
"insertImage",
"insertFile",
"subscript",
"superscript",
"createTable",
"addRowAbove",
"addRowBelow",
"addColumnLeft",
"addColumnRight",
"deleteRow",
"deleteColumn",
"viewHtml",
"formatting",
"cleanFormatting",
"fontName",
"fontSize",
"foreColor",
"backColor",
"print"
        ];
        $scope.messages = { insertHtml: "Insert Marker" };
        $scope.stylesheets = [
                    "../Content/editorStyles.css"
        ];

        $scope.LoadCM00100Page('SendEmails');
        $scope.selectedIds = ["Email01"];
        $scope.CM00104.status = 0;
        $http.get('/Co_Debtor/DebtorAddressTypeFillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.AddressTypes = data;
        }).finally(function () { });
        $http.get('/CMS_DemandLetter/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.Templates = data;
        }).finally(function () {

        });
    };

    //-------------------------- Contact Information
    $scope.CM00106 = {};
    $scope.DebtorContact = function () {
        removeEntityPage($scope.PagesCM00100);
        $scope.LoadCM00100Page('DebtorContact');
        $scope.CM00106.Status = 1;
        $http.get('/Admin_ContactType/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey + '&ContactSourceID=2').success(function (data) {
            $scope.DebtorContactTypes = data;
        }).finally(function () { });

    };
    $scope.SaveDebtorContact = function () {
        var grid = $("#GridCo_DebtorContactByDebtor").data("kendoGrid");
        var dataSourceData = grid.dataSource.data();

        if ($scope.functiontofindIndexByKeyValue(dataSourceData, "ContactTypeID", $scope.CM00106.ContactTypeID) != null) {
            $.smallBox({
                title: "No Changes have been made!!",
                content: "<i class='fa fa-clock-o'></i> <i>This Type is already has data in this Debtor !!</i>",
                color: "#3276B1",
                iconSmall: "fa fa-times fa-2x fadeInRight animated",
                timeout: 4000
            });
            return;
        }
        $scope.CM00106.DebtorID = $scope.CM00100.DebtorID;
        $http.post('/Co_Debtor/SaveDebtorContact', { 'CM00106': $scope.CM00106, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
            if (data.Status == true) {
                $.bigBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#739E73",
                    timeout: 3000,
                    icon: "fa fa-check shake animated"
                });
                $scope.GridRefresh('GridCo_DebtorContactByDebtor');
                $scope.CM00106 = { Status: 1 };
            }
            else {
                $.bigBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#C46A69",
                    timeout: 3000,
                    icon: "fa fa-bolt swing animated"
                });
            }
        }).error(function (data, status, headers, config) {
            $.bigBox({
                title: data.Massage,
                content: data.Description,
                color: "#C46A69",
                timeout: 3000,
                icon: "fa fa-bolt swing animated"
            });
        });
    };
    $scope.UpdateDebtorContact = function () {
        $scope.CM00106.DebtorID = $scope.CM00100.DebtorID;
        $http.post('/Co_Debtor/UpdateDebtorContact', { 'CM00106': $scope.CM00106, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
            if (data.Status == true) {
                $.bigBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#739E73",
                    timeout: 3000,
                    icon: "fa fa-check shake animated"
                });
                $scope.GridRefresh('GridCo_DebtorContactByDebtor');
                $scope.CM00106 = { Status: 1 };
            }
            else {
                $.bigBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#C46A69",
                    timeout: 3000,
                    icon: "fa fa-bolt swing animated"
                });
            }
        }).error(function (data, status, headers, config) {
            $.bigBox({
                title: data.Massage,
                content: data.Description,
                color: "#C46A69",
                timeout: 3000,
                icon: "fa fa-bolt swing animated"
            });
        });
    };
    $scope.DeleteDebtorContact = function () {
        $.SmartMessageBox({
            title: "Delete Record",
            content: "Are you sure you want to delete this?",
            buttons: '[No][Yes]'
        }, function (ButtonPressed) {
            if (ButtonPressed === "Yes") {
                $http.post('/Co_Debtor/DeleteDebtorContact', { 'CM00106': $scope.CM00106, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
                    if (data.Status == true) {
                        $.bigBox({
                            title: data.Massage,
                            content: data.Description,
                            color: "#739E73",
                            timeout: 3000,
                            icon: "fa fa-check shake animated"
                        });
                        $scope.GridRefresh('GridCo_DebtorContactByDebtor');
                        $scope.CM00106 = { Status: 1 };
                    }
                    else {
                        $.bigBox({
                            title: data.Massage,
                            content: data.Description,
                            color: "#C46A69",
                            timeout: 3000,
                            icon: "fa fa-bolt swing animated"
                        });
                    }
                }).error(function (data, status, headers, config) {
                    $.bigBox({
                        title: data.Massage,
                        content: data.Description,
                        color: "#C46A69",
                        timeout: 3000,
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
                $scope.CM00106 = { Status: 1 };
            }
        });
    };
    $scope.ContactTypeChanged = function () {
        var grid = $("#GridCo_DebtorContactByDebtor").data("kendoGrid");
        var dataSourceData = [];
        angular.copy(grid.dataSource.data(), dataSourceData);
        var found = $scope.functiontofindObjectByKeyValue(dataSourceData, "ContactTypeID", $scope.CM00106.ContactTypeID);
        if (found != null) {
            $scope.CM00106 = found;
            $scope.CM00106.Status = 2;
            return;
        }
        else {
            $scope.CM00106 = { Status: 1, ContactTypeID: $scope.CM00106.ContactTypeID };
            return;
        }
    };

    //-------------------------- Address Information
    $scope.CM00104 = {};
    $scope.DebtorAddresses = [];
    $scope.DebtorAddress = function () {
        removeEntityPage($scope.PagesCM00100);
        $scope.LoadCM00100Page('DebtorAddress');
        $scope.CM00104.Status = 1;
        $http.get('/Co_Debtor/DebtorAddressTypeFillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey + '&ContactSourceID=1').success(function (data) {
            $scope.DebtorAddressTypes = data;
        }).finally(function () { });
        $http.get('/Adm_Country/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.Countries = data;
        }).finally(function () { });
        $http.get('/Adm_City/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.Cities = data;
        }).finally(function () { });
    };
    $scope.SaveDebtorAddress = function () {
        var grid = $("#GridCo_DebtorAddressByDebtor").data("kendoGrid");
        var dataSourceData = grid.dataSource.data();

        if ($scope.functiontofindIndexByKeyValue(dataSourceData, "AddressCode", $scope.CM00104.AddressCode) != null) {
            $.smallBox({
                title: "No Changes have been made!!",
                content: "<i class='fa fa-clock-o'></i> <i>This Type is already has data in this Debtor !!</i>",
                color: "#3276B1",
                iconSmall: "fa fa-times fa-2x fadeInRight animated",
                timeout: 4000
            });
            return;
        }
        $scope.CM00104.DebtorID = $scope.CM00100.DebtorID;
        $http.post('/Co_Debtor/SaveDebtorAddress', { 'CM00104': $scope.CM00104, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
            if (data.Status == true) {
                $.bigBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#739E73",
                    timeout: 3000,
                    icon: "fa fa-check shake animated"
                });
                $scope.GridRefresh('GridCo_DebtorAddressByDebtor');
                $scope.CM00104 = { Status: 1 };
            }
            else {
                $.bigBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#C46A69",
                    timeout: 3000,
                    icon: "fa fa-bolt swing animated"
                });
            }
        }).error(function (data, status, headers, config) {
            $.bigBox({
                title: data.Massage,
                content: data.Description,
                color: "#C46A69",
                timeout: 3000,
                icon: "fa fa-bolt swing animated"
            });
        });
    };
    $scope.UpdateDebtorAddress = function () {
        $scope.CM00104.DebtorID = $scope.CM00100.DebtorID;
        $http.post('/Co_Debtor/UpdateDebtorAddress', { 'CM00104': $scope.CM00104, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
            if (data.Status == true) {
                $.bigBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#739E73",
                    timeout: 3000,
                    icon: "fa fa-check shake animated"
                });
                $scope.GridRefresh('GridCo_DebtorAddressByDebtor');
                $scope.CM00104 = { Status: 1 };
            }
            else {
                $.bigBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#C46A69",
                    timeout: 3000,
                    icon: "fa fa-bolt swing animated"
                });
            }
        }).error(function (data, status, headers, config) {
            $.bigBox({
                title: data.Massage,
                content: data.Description,
                color: "#C46A69",
                timeout: 3000,
                icon: "fa fa-bolt swing animated"
            });
        });
    };
    $scope.DeleteDebtorAddress = function () {
        $.SmartMessageBox({
            title: "Delete Record",
            content: "Are you sure you want to delete this?",
            buttons: '[No][Yes]'
        }, function (ButtonPressed) {
            if (ButtonPressed === "Yes") {
                $http.post('/Co_Debtor/DeleteDebtorAddress', { 'CM00104': $scope.CM00104, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
                    if (data.Status == true) {
                        $.bigBox({
                            title: data.Massage,
                            content: data.Description,
                            color: "#739E73",
                            timeout: 3000,
                            icon: "fa fa-check shake animated"
                        });
                        $scope.GridRefresh('GridCo_DebtorAddressByDebtor');
                        $scope.CM00104 = { Status: 1 };
                    }
                    else {
                        $.bigBox({
                            title: data.Massage,
                            content: data.Description,
                            color: "#C46A69",
                            timeout: 3000,
                            icon: "fa fa-bolt swing animated"
                        });
                    }
                }).error(function (data, status, headers, config) {
                    $.bigBox({
                        title: data.Massage,
                        content: data.Description,
                        color: "#C46A69",
                        timeout: 3000,
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
                $scope.CM00104 = { Status: 1 };
            }
        });
    };
    $scope.AddressTypeChanged = function () {
        if ($scope.CM00100.Status == 1) {
            var found = $scope.GetSingle($scope.DebtorAddresses, "AddressCode", $scope.CM00104.AddressCode);
            if (found != null) {
                $scope.CM00104 = found;
                $scope.CM00104.Status = 2;
            }
            else {
                $scope.CM00104 = { AddressCode: $scope.CM00104.AddressCode, Status: 1 };
                var addressName = $scope.GetSingle($scope.DebtorAddressTypes, "AddressCode", $scope.CM00104.AddressCode);
                $scope.CM00104.AddressName = addressName.AddressName;
            }
        }
        else {
            var grid = $("#GridCo_DebtorAddressByDebtor").data("kendoGrid");
            var dataSourceData = [];
            angular.copy(grid.dataSource.data(), dataSourceData);
            var found = $scope.GetSingle(dataSourceData, "AddressCode", $scope.CM00104.AddressCode);
            if (found != null) {
                $scope.CM00104 = found;
                $scope.CM00104.Status = 2;
                return;
            }
            else {
                var addressName = $scope.GetSingle($scope.DebtorAddressTypes, "AddressCode", $scope.CM00104.AddressCode);
                $scope.CM00104 = { Status: 1, AddressCode: $scope.CM00104.AddressCode, AddressName: addressName.AddressName };
                return;
            }
        }
    };
    $scope.AddNewAddress = function () {
        $scope.CM00104.DebtorID = $scope.CM00100.DebtorID;
        $scope.DebtorAddresses.push($scope.CM00104);
        $scope.CM00104 = { Status: 1 };
    };
    $scope.UpdateAddress = function () {
        var index = $scope.GetSingleIndex($scope.DebtorAddresses, "AddressCode", $scope.CM00104.AddressCode);
        $scope.DebtorAddresses.splice(index, 1, $scope.CM00104);
        $scope.CM00104 = { Status: 1 };
    };
    $scope.RemoveAddress = function (address, index) {
        $scope.DebtorAddresses.splice(index, 1);
    };
    $scope.OnAddressTabSelected = function () {
        if ($scope.CM00100.Status == 2) {
            $scope.GridRefresh('GridCo_DebtorAddressByDebtor');
        }
    };

    //-------------------------- Document Information
    $scope.CM00101 = {};
    $scope.DebtorDocument = function () {
        removeEntityPage($scope.PagesCM00100);
        $scope.LoadCM00100Page('DebtorDocument');
        $scope.CM00101.Status = 1;
        $http.get('/Admin_DocumnetType/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey + '&ContactSourceID=2').success(function (data) {
            $scope.DebtorDocumentTypes = data;
        }).finally(function () { });

    };
    $scope.SaveDebtorDocument = function () {
        var grid = $("#GridCo_DebtorDocumentByDebtor").data("kendoGrid");
        var dataSourceData = grid.dataSource.data();
        $scope.CM00101.DebtorID = $scope.CM00100.DebtorID;
        $http.post('/Co_Debtor/SaveDebtorDocument', { 'CM00101': $scope.CM00101, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
            if (data.Status == true) {
                $.bigBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#739E73",
                    timeout: 3000,
                    icon: "fa fa-check shake animated"
                });
                $scope.GridRefresh('GridCo_DebtorDocumentByDebtor');
                $scope.CM00101 = { Status: 1 };
            }
            else {
                $.bigBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#C46A69",
                    timeout: 3000,
                    icon: "fa fa-bolt swing animated"
                });
            }
        }).error(function (data, status, headers, config) {
            $.bigBox({
                title: data.Massage,
                content: data.Description,
                color: "#C46A69",
                timeout: 3000,
                icon: "fa fa-bolt swing animated"
            });
        });
    };
    $scope.UpdateDebtorDocument = function () {
        $scope.CM00101.DebtorID = $scope.CM00100.DebtorID;
        $http.post('/Co_Debtor/UpdateDebtorDocument', { 'CM00101': $scope.CM00101, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
            if (data.Status == true) {
                $.bigBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#739E73",
                    timeout: 3000,
                    icon: "fa fa-check shake animated"
                });
                $scope.GridRefresh('GridCo_DebtorDocumentByDebtor');
                $scope.CM00101 = { Status: 1 };
            }
            else {
                $.bigBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#C46A69",
                    timeout: 3000,
                    icon: "fa fa-bolt swing animated"
                });
            }
        }).error(function (data, status, headers, config) {
            $.bigBox({
                title: data.Massage,
                content: data.Description,
                color: "#C46A69",
                timeout: 3000,
                icon: "fa fa-bolt swing animated"
            });
        });
    };
    $scope.DeleteDebtorDocument = function () {
        $.SmartMessageBox({
            title: "Delete Record",
            content: "Are you sure you want to delete this?",
            buttons: '[No][Yes]'
        }, function (ButtonPressed) {
            if (ButtonPressed === "Yes") {
                $http.post('/Co_Debtor/DeleteDebtorDocument', { 'CM00101': $scope.CM00101, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
                    if (data.Status == true) {
                        $.bigBox({
                            title: data.Massage,
                            content: data.Description,
                            color: "#739E73",
                            timeout: 3000,
                            icon: "fa fa-check shake animated"
                        });
                        $scope.GridRefresh('GridCo_DebtorDocumentByDebtor');
                        $scope.CM00101 = { Status: 1 };
                    }
                    else {
                        $.bigBox({
                            title: data.Massage,
                            content: data.Description,
                            color: "#C46A69",
                            timeout: 3000,
                            icon: "fa fa-bolt swing animated"
                        });
                    }
                }).error(function (data, status, headers, config) {
                    $.bigBox({
                        title: data.Massage,
                        content: data.Description,
                        color: "#C46A69",
                        timeout: 3000,
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
                $scope.CM00101 = { Status: 1 };
            }
        });
    };
    $scope.SetDebtorFileExtension = function (PhotoPath) {
        $scope.CM00101.PhotoExtension = PhotoPath;
    };
    $scope.RemoveDebtorFileExtension = function (PhotoPath) {
        $scope.CM00101.PhotoExtension = "";
    };
    $scope.DebtorDocumentFilePath = function (CM00101) {
        if (angular.isUndefined(CM00101.PhotoExtension)) return "";
        var FilePath;
        if (CM00101.Status == 0)
            FilePath = "DebtorDocuments";
        else
            FilePath = "DebtorDocumentsTemp";
        return "\\Photo\\" + FilePath + "\\" + CM00101.PhotoExtension;
    };
    //---------------------------UPLOAD--------------------------------
    var TableName;
    $scope.ExcelColumns = [];
    $scope.UploadDebtor = function () {
        $scope.LoadCM00100Page('UploadDebtor');
        $scope.Clear();
    };

    $scope.UploadedData = function (Columns) {
        TableName = Columns.TableName;
        $scope.ExcelColumns = Columns.ExcelColumns;
        $scope.ExcelColumns.push({ Index: "NULL", ColumnName: "N/A" });
        $scope.KaizenColumn = [
        { FieldName: "DebtorID", FieldDisplay: "CPR Number", required: true, FieldValue: "NULL" },
        { FieldName: "EnglishFullName", FieldDisplay: "Full Name", FieldValue: "NULL" },
        { FieldName: "BirthDate", FieldDisplay: "Birth Date", FieldValue: "NULL" },
        { FieldName: "AddressEnglish", FieldDisplay: "Address", FieldValue: "NULL" },
        { FieldName: "VisaNo", FieldDisplay: "Visa No", FieldValue: "NULL" },
        { FieldName: "VisaExpiryDate", FieldDisplay: "Visa Expiry Date", FieldValue: "NULL" },
        { FieldName: "PassportNumber", FieldDisplay: "Passport No", FieldValue: "NULL" },
        { FieldName: "PassportIssueDate", FieldDisplay: "Passport Issue Date", FieldValue: "NULL" },
        { FieldName: "PassportExpiryDate", FieldDisplay: "Passport Expiry Date", FieldValue: "NULL" },
        { FieldName: "CPRIssueDate", FieldDisplay: "CPR Issue Date", FieldValue: "NULL" },
        { FieldName: "CPRExpiryDate", FieldDisplay: "CPR Expiry Date", FieldValue: "NULL" },
        { FieldName: "CPRSerialNumber", FieldDisplay: "CPR Serial No", FieldValue: "NULL" },
        { FieldName: "SponsorName", FieldDisplay: "Sponsor Name", FieldValue: "NULL" },
        { FieldName: "ResidenceNo", FieldDisplay: "Residence No", FieldValue: "NULL" },
        { FieldName: "EmployerName1", FieldDisplay: "Employer Name", FieldValue: "NULL" },
        { FieldName: "NationalityID", FieldDisplay: "Nationality", FieldValue: "NULL" },
        { FieldName: "BlockNo", FieldDisplay: "Block No", FieldValue: "NULL" },
        { FieldName: "BlockName", FieldDisplay: "Block Name", FieldValue: "NULL" },
        { FieldName: "RoadNo", FieldDisplay: "Road No", FieldValue: "NULL" },
        { FieldName: "RoadName", FieldDisplay: "Road Name", FieldValue: "NULL" },
        { FieldName: "BuildingNo", FieldDisplay: "Building No", FieldValue: "NULL" },
        { FieldName: "BuildingAlpha", FieldDisplay: "Building Name", FieldValue: "NULL" },
        { FieldName: "FlatNo", FieldDisplay: "Flat No", FieldValue: "NULL" },
        { FieldName: "ContactNo", FieldDisplay: "Contact No", FieldValue: "NULL" },
        { FieldName: "Email", FieldDisplay: "Email", FieldValue: "NULL" },
        { FieldName: "OccupationEnglish", FieldDisplay: "Occupation", FieldValue: "NULL" },
        { FieldName: "OccupationDescription1", FieldDisplay: "Occupation Description", FieldValue: "NULL" },

        ];
    };
    $scope.OpenMapFieldsWindow = function () {
        $scope.MapFieldstWindow.center().toFront().open();
    };

    $scope.CurrentStep = -1;
    $scope.UploadData = function () {
        if ($scope.CurrentStep == -1) {
            $http.post('/CMS_TRX_CaseUploading/UploadDebtorData', {
                'TableName': TableName, 'KaizenColumn': $scope.KaizenColumn,
                'KaizenPublicKey': sessionStorage.PublicKey
            }).success(function (data) {
                if (data.Status == true) {
                    $.bigBox({
                        title: data.Massage,
                        content: data.Description,
                        color: "#739E73",
                        timeout: 4000,
                        icon: "fa fa-check shake animated"
                    });
                    $scope.CurrentStep = 0;
                    $scope.CheckStep($scope.CurrentStep);
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

    $scope.CheckStep = function (step) {
        if ($scope.CurrentStep == 0) {
            $http.get('/CMS_TRX_CaseUploading/CM_UploadValidate00?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (DebtorNamedata) {
                if (DebtorNamedata.length > 0) {
                    $scope.GridRefresh("GridCM_UploadValidate00");
                    return;
                }
                else {
                    $scope.CurrentStep = parseInt($scope.CurrentStep) + 1;
                    $scope.CheckStep($scope.CurrentStep);
                }
            });
        }
        else if ($scope.CurrentStep == 1) {
            $http.get('/CMS_TRX_CaseUploading/CM_UploadValidate01?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (EmployerNamedata) {
                if (EmployerNamedata.length > 0) {
                    $scope.GridRefresh("GridCM_UploadValidate01");
                }
                else {
                    $scope.CurrentStep = parseFloat($scope.CurrentStep) + 1;
                    $scope.CheckStep($scope.CurrentStep);
                }
            });
        }
        else if ($scope.CurrentStep == 2) {
            $http.get('/CMS_TRX_CaseUploading/CM_UploadValidate02?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (BirthDatedata) {
                if (BirthDatedata.length > 0) {
                    $scope.GridRefresh("GridCM_UploadValidate02");
                }
                else {
                    $scope.CurrentStep = parseFloat($scope.CurrentStep) + 1;
                    $scope.CheckStep($scope.CurrentStep);
                }
            });
        }
        else if ($scope.CurrentStep == 3) {
            $http.get('/CMS_TRX_CaseUploading/CM_UploadValidate03?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (Address1data) {
                if (Address1data.length > 0) {
                    $scope.GridRefresh("GridCM_UploadValidate03");
                    return;
                }
                else {
                    $scope.CurrentStep = parseFloat($scope.CurrentStep) + 1;
                    $scope.CheckStep($scope.CurrentStep);
                }
            });

        }
        else if ($scope.CurrentStep == 4) {
            $http.get('/CMS_TRX_CaseUploading/CM_UploadValidate04?KaizenPublicKey=' + sessionStorage.PublicKey)
             .success(function (Address2data) {
                 if (Address2data.length > 0) {
                     $scope.GridRefresh("CM_UploadValidate04");
                 }
                 else {
                     $scope.CurrentStep = parseFloat($scope.CurrentStep) + 1;
                     $scope.CheckStep($scope.CurrentStep);
                 }
             });
        }
        else if ($scope.CurrentStep == 5) {
            $http.get('/CMS_TRX_CaseUploading/CM_UploadValidateEmail01?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (Email01data) {
                if (Email01data.length > 0) {
                    $scope.GridRefresh("GridCM_UploadValidateEmail01");
                }
                else {
                    $scope.CurrentStep = parseFloat($scope.CurrentStep) + 1;
                    $scope.CheckStep($scope.CurrentStep);
                }
            });
        }
        else if ($scope.CurrentStep == 6) {
            $http.get('/CMS_TRX_CaseUploading/CM_UploadValidateMobileNo1?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (MobileNo1data) {
                if (MobileNo1data.length > 0) {
                    $scope.GridRefresh("GridCM_UploadValidateMobileNo1");
                }
                else {
                    $scope.CurrentStep = parseFloat($scope.CurrentStep) + 1;
                    $scope.CheckStep($scope.CurrentStep);
                }
            });
        }
        else if ($scope.CurrentStep == 7) {
            $http.get('/CMS_TRX_CaseUploading/CM_UploadValidateNationalityID?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (NationalityIDdata) {
                if (NationalityIDdata.length > 0) {
                    $scope.GridRefresh("GridCM_UploadValidateNationalityID");
                    return;
                } else {
                    $scope.CurrentStep = parseFloat($scope.CurrentStep) + 1;
                    $scope.CheckStep($scope.CurrentStep);
                }
            });
        }
        else if ($scope.CurrentStep == 8) {
            $http.get('/CMS_TRX_CaseUploading/CM_UploadValidateOccupation?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (Occupationdata) {
                if (Occupationdata.length > 0) {
                    $scope.GridRefresh("GridCM_UploadValidateOccupation");
                }
                else {
                    $scope.CurrentStep = parseFloat($scope.CurrentStep) + 1;
                    $scope.CheckStep($scope.CurrentStep);
                }
            });
        }
        else if ($scope.CurrentStep == 9) {
            $http.get('/CMS_TRX_CaseUploading/CM_UploadValidateOther01?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (Other01data) {
                if (Other01data.length > 0) {
                    $scope.GridRefresh("GridCM_UploadValidateOther01");
                }
                else {
                    $scope.CurrentStep = parseFloat($scope.CurrentStep) + 1;
                    $scope.CheckStep($scope.CurrentStep);
                }
            });
        }
        else if ($scope.CurrentStep == 10) {
            $http.get('/CMS_TRX_CaseUploading/CM_UploadValidatePassportNumber?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (PassportNumberdata) {
                if (PassportNumberdata.length > 0) {
                    $scope.GridRefresh("GridCM_UploadValidatePassportNumber");
                }
                else {
                    $scope.CurrentStep = parseFloat($scope.CurrentStep) + 1;
                    $scope.CheckStep($scope.CurrentStep);
                }
            });
        }
        else if ($scope.CurrentStep == 11) {
            $http.get('/CMS_TRX_CaseUploading/CM_UploadValidatePnone01?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (Pnone01data) {
                if (Pnone01data.length > 0) {
                    $scope.GridRefresh("GridCM_UploadValidatePnone01");
                }
                else {
                    $scope.CurrentStep = parseFloat($scope.CurrentStep) + 1;
                    $scope.CheckStep($scope.CurrentStep);
                }
            });
        }
        else if ($scope.CurrentStep == 12) {
            $http.get('/CMS_TRX_CaseUploading/CM_UploadValidatePnone02?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (Pnone01data) {
                if (Pnone01data.length > 0) {
                    $scope.GridRefresh("GridCM_UploadValidatePnone02");
                }
                else {
                    $scope.CurrentStep = parseFloat($scope.CurrentStep) + 1;
                    $scope.CheckStep($scope.CurrentStep);
                }
            });
        }
        else if ($scope.CurrentStep == 13) {
            $http.get('/CMS_TRX_CaseUploading/CM_UploadValidatePnone03?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (Pnone03data) {
                if (Pnone03data.length > 0) {
                    $scope.GridRefresh("GridCM_UploadValidatePnone03");
                }
                else {
                    $scope.CurrentStep = parseFloat($scope.CurrentStep) + 1;
                    $scope.CheckStep($scope.CurrentStep);
                }
            });
        }
        else if ($scope.CurrentStep == 14) {
            $http.get('/CMS_TRX_CaseUploading/CM_UploadValidatePnone04?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (Pnone04data) {
                if (Pnone04data.length > 0) {
                    $scope.GridRefresh("GridCM_UploadValidatePnone04");
                }
                else {
                    $scope.CurrentStep = parseFloat($scope.CurrentStep) + 1;
                    $scope.CheckStep($scope.CurrentStep);
                }
            });
        }
        else if ($scope.CurrentStep == 15) {
            $http.get('/CMS_TRX_CaseUploading/CM_UploadValidateResidenceNo?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (ResidenceNodata) {
                if (ResidenceNodata.length > 0) {
                    $scope.GridRefresh("GridCM_UploadValidateResidenceNo");
                }
                else {
                    $http.get('/CMS_TRX_CaseUploading/PostDebtorData?KaizenPublicKey='
                   + sessionStorage.PublicKey).success(function (resultData) {
                       if (resultData.Status == true) {
                           $.bigBox({
                               title: resultData.Massage,
                               content: resultData.Description,
                               color: "#739E73",
                               timeout: 4000,
                               icon: "fa fa-check shake animated"
                           });
                           $scope.CurrentStep = parseFloat($scope.CurrentStep) + 1;
                           $scope.CheckStep($scope.CurrentStep);
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
                   });
                }
            });
        }
        else {
            $.bigBox({
                title: "Successfully Updated",
                content: "Data is Up to date ..",
                color: "#739E73",
                timeout: 4000,
                icon: "fa fa-check shake animated"
            });
        }
    };
    $scope.DynamicCancel = function (row) {
        var PathURL = '';
        switch ($scope.CurrentStep) {
            case 0:
                PathURL = 'CancelUploadValidateDebtorName';
                break;
            case 1:
                PathURL = 'CancelUploadValidateEmployerName';
                break;
            case 2:
                PathURL = 'CancelUploadValidateBirthDate';
                break;
            case 3:
                PathURL = 'CancelValidateAddress1';
                break;
            case 4:
                PathURL = 'CancelValidateAddress2';
                break;
            case 5:
                PathURL = 'CancelUploadValidateEmail01';
                break;
            case 6:
                PathURL = 'CancelUploadValidateMobileNo1';
                break;
            case 7:
                PathURL = 'CancelUploadValidateNationalityID';
                break;
            case 8:
                PathURL = 'CancelUploadValidateOccupation';
                break;
            case 9:
                PathURL = 'CancelUploadValidateOther01';
                break;
            case 10:
                PathURL = 'CancelUploadValidatePassportNumber';
                break;
            case 11:
                PathURL = 'CancelUploadValidatePnone01';
                break;
            case 12:
                PathURL = 'CancelUploadValidatePnone02';
                break;
            case 13:
                PathURL = 'CancelUploadValidatePnone03';
                break;
            case 14:
                PathURL = 'CancelUploadValidatePnone04';
                break;
            case 15:
                PathURL = 'CancelUploadValidateResidenceNo';
                break;
        }
        $http.post('/CMS_TRX_CaseUploading/' + PathURL, {
            'UploadedOBJECT': row,
            'KaizenPublicKey': sessionStorage.PublicKey
        }).success(function (data) {
            if (data.Status == true) {
                $.smallBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#739E73",
                    iconSmall: "fa fa-times fa-2x fadeInRight animated",
                    timeout: 1000
                });
                $scope.CheckStep($scope.CurrentStep);
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
    $scope.DynamicOverride = function (row) {
        var PathURL = '';
        switch ($scope.CurrentStep) {
            case 0:
                PathURL = 'OverrideUploadValidateDebtorName';
                break;
            case 1:
                PathURL = 'OverrideUploadValidateEmployerName';
                break;
            case 2:
                PathURL = 'OverrideUploadValidateBirthDate';
                break;
            case 3:
                PathURL = 'OverrideUploadValidateAddress1';
                break;
            case 4:
                PathURL = 'OverrideUploadValidateAddress2';
                break;
            case 5:
                PathURL = 'OverrideUploadValidateEmail01';
                break;
            case 6:
                PathURL = 'OverrideUploadValidateMobileNo1';
                break;
            case 7:
                PathURL = 'OverrideUploadValidateNationalityID';
                break;
            case 8:
                PathURL = 'OverrideUploadValidateOccupation';
                break;
            case 9:
                PathURL = 'OverrideUploadValidateOther01';
                break;
            case 10:
                PathURL = 'OverrideUploadValidatePassportNumber';
                break;
            case 11:
                PathURL = 'OverrideUploadValidatePnone01';
                break;
            case 12:
                PathURL = 'OverrideUploadValidatePnone02';
                break;
            case 13:
                PathURL = 'OverrideUploadValidatePnone03';
                break;
            case 14:
                PathURL = 'OverrideUploadValidatePnone04';
                break;
            case 15:
                PathURL = 'OverrideUploadValidateResidenceNo';
                break;
        }
        $http.post('/CMS_TRX_CaseUploading/' + PathURL, {
            'UploadedOBJECT': row,
            'KaizenPublicKey': sessionStorage.PublicKey
        }).success(function (data) {
            if (data.Status == true) {
                $.smallBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#739E73",
                    iconSmall: "fa fa-times fa-2x fadeInRight animated",
                    timeout: 1000
                });
                $scope.CheckStep($scope.CurrentStep);
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
    //$scope.DynamicView = function (row) {
    //    $scope.CM00100 = {};
    //    $scope.CM00104 = {};
    //    $scope.LoadCM00100(row.DebtorID);
    //    $scope.DetailsWindow.center().toFront().open();
    //    $scope.DetailsWindow.refresh({
    //        url: "/Co_Debtor/DebtorDetails?KaizenPublicKey=" + sessionStorage.PublicKey
    //    });
    //};
    $scope.DynamicCancelAll = function () {
        var PathURL = '';
        var dataSourceData = [];
        switch ($scope.CurrentStep) {
            case 0:
                PathURL = 'CancelAllUploadValidateDebtorName';
                var grid = $("#GridCM_UploadValidate00").data("kendoGrid");
                angular.copy(grid.dataSource.data(), dataSourceData);
                break;
            case 1:
                PathURL = 'CancelAllUploadValidateEmployerName';
                var grid = $("#GridCM_UploadValidate01").data("kendoGrid");
                angular.copy(grid.dataSource.data(), dataSourceData);
                break;
            case 2:
                PathURL = 'CancelAllUploadValidateBirthDate';
                var grid = $("#GridCM_UploadValidate02").data("kendoGrid");
                angular.copy(grid.dataSource.data(), dataSourceData);
                break;
            case 3:
                PathURL = 'CancelAllValidateAddress1';
                var grid = $("#GridCM_UploadValidate03").data("kendoGrid");
                angular.copy(grid.dataSource.data(), dataSourceData);
                break;
            case 4:
                PathURL = 'CancelAllValidateAddress2';
                var grid = $("#CM_UploadValidate04").data("kendoGrid");
                angular.copy(grid.dataSource.data(), dataSourceData);
                break;
            case 5:
                PathURL = 'CancelAllUploadValidateEmail01';
                var grid = $("#GridCM_UploadValidateEmail01").data("kendoGrid");
                angular.copy(grid.dataSource.data(), dataSourceData);
                break;
            case 6:
                PathURL = 'CancelAllUploadValidateMobileNo1';
                var grid = $("#GridCM_UploadValidateMobileNo1").data("kendoGrid");
                angular.copy(grid.dataSource.data(), dataSourceData);
                break;
            case 7:
                PathURL = 'CancelUploadValidateNationalityID';
                var grid = $("#GridCM_UploadValidateNationalityID").data("kendoGrid");
                angular.copy(grid.dataSource.data(), dataSourceData);
                break;
            case 8:
                PathURL = 'CancelAllUploadValidateOccupation';
                var grid = $("#GridCM_UploadValidateOccupation").data("kendoGrid");
                angular.copy(grid.dataSource.data(), dataSourceData);
                break;
            case 9:
                PathURL = 'CancelAllUploadValidateOther01';
                var grid = $("#GridCM_UploadValidateOther01").data("kendoGrid");
                angular.copy(grid.dataSource.data(), dataSourceData);
                break;
            case 10:
                PathURL = 'CancelAllUploadValidatePassportNumber';
                var grid = $("#GridCM_UploadValidatePassportNumber").data("kendoGrid");
                angular.copy(grid.dataSource.data(), dataSourceData);
                break;
            case 11:
                PathURL = 'CancelAllUploadValidatePnone01';
                var grid = $("#GridCM_UploadValidatePnone01").data("kendoGrid");
                angular.copy(grid.dataSource.data(), dataSourceData);
                break;
            case 12:
                PathURL = 'CancelAllUploadValidatePnone02';
                var grid = $("#GridCM_UploadValidatePnone02").data("kendoGrid");
                angular.copy(grid.dataSource.data(), dataSourceData);
                break;
            case 13:
                PathURL = 'CancelAllUploadValidatePnone03';
                var grid = $("#GridCM_UploadValidatePnone03").data("kendoGrid");
                angular.copy(grid.dataSource.data(), dataSourceData);
                break;
            case 14:
                PathURL = 'CancelAllUploadValidatePnone04';
                var grid = $("#GridCM_UploadValidatePnone04").data("kendoGrid");
                angular.copy(grid.dataSource.data(), dataSourceData);
                break;
            case 15:
                PathURL = 'CancelAllUploadValidateResidenceNo';
                var grid = $("#GridCM_UploadValidateResidenceNo").data("kendoGrid");
                angular.copy(grid.dataSource.data(), dataSourceData);
                break;
        }
        $http.post('/CMS_TRX_CaseUploading/' + PathURL, {
            'UploadedOBJECT': dataSourceData,
            'KaizenPublicKey': sessionStorage.PublicKey
        }).success(function (data) {
            if (data.Status == true) {
                $.smallBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#739E73",
                    iconSmall: "fa fa-times fa-2x fadeInRight animated",
                    timeout: 1000
                });
                $scope.CheckStep($scope.CurrentStep);
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
    $scope.DynamicOverrideAll = function () {
        var PathURL = '';
        var dataSourceData = [];
        switch ($scope.CurrentStep) {
            case 0:
                PathURL = 'OverrideAllUploadValidateDebtorName';
                var grid = $("#GridCM_UploadValidate00").data("kendoGrid");
                angular.copy(grid.dataSource.data(), dataSourceData);
                break;
            case 1:
                PathURL = 'OverrideAllUploadValidateEmployerName';
                var grid = $("#GridCM_UploadValidate01").data("kendoGrid");
                angular.copy(grid.dataSource.data(), dataSourceData);
                break;
            case 2:
                PathURL = 'OverrideAllUploadValidateBirthDate';
                var grid = $("#GridCM_UploadValidate02").data("kendoGrid");
                angular.copy(grid.dataSource.data(), dataSourceData);
                break;
            case 3:
                PathURL = 'OverrideAllUploadValidateAddress1';
                var grid = $("#GridCM_UploadValidate03").data("kendoGrid");
                angular.copy(grid.dataSource.data(), dataSourceData);
                break;
            case 4:
                PathURL = 'OverrideAllUploadValidateAddress2';
                var grid = $("#CM_UploadValidate04").data("kendoGrid");
                angular.copy(grid.dataSource.data(), dataSourceData);
                break;
            case 5:
                PathURL = 'OverrideUploadValidateEmail01';
                var grid = $("#GridCM_UploadValidateEmail01").data("kendoGrid");
                angular.copy(grid.dataSource.data(), dataSourceData);
                break;
            case 6:
                PathURL = 'OverrideAllUploadValidateMobileNo1';
                var grid = $("#GridCM_UploadValidateMobileNo1").data("kendoGrid");
                angular.copy(grid.dataSource.data(), dataSourceData);
                break;
            case 7:
                PathURL = 'OverrideAllUploadValidateNationalityID';
                var grid = $("#GridCM_UploadValidateNationalityID").data("kendoGrid");
                angular.copy(grid.dataSource.data(), dataSourceData);
                break;
            case 8:
                PathURL = 'OverrideAllUploadValidateOccupation';
                var grid = $("#GridCM_UploadValidateOccupation").data("kendoGrid");
                angular.copy(grid.dataSource.data(), dataSourceData);
                break;
            case 9:
                PathURL = 'OverrideAllUploadValidateOther01';
                var grid = $("#GridCM_UploadValidateOther01").data("kendoGrid");
                angular.copy(grid.dataSource.data(), dataSourceData);
                break;
            case 10:
                PathURL = 'OverrideAllUploadValidatePassportNumber';
                var grid = $("#GridCM_UploadValidatePassportNumber").data("kendoGrid");
                angular.copy(grid.dataSource.data(), dataSourceData);
                break;
            case 11:
                PathURL = 'OverrideAllUploadValidatePnone01';
                var grid = $("#GridCM_UploadValidatePnone01").data("kendoGrid");
                angular.copy(grid.dataSource.data(), dataSourceData);
                break;
            case 12:
                PathURL = 'OverrideAllUploadValidatePnone02';
                var grid = $("#GridCM_UploadValidatePnone02").data("kendoGrid");
                angular.copy(grid.dataSource.data(), dataSourceData);
                break;
            case 13:
                PathURL = 'OverrideAllUploadValidatePnone03';
                var grid = $("#GridCM_UploadValidatePnone03").data("kendoGrid");
                angular.copy(grid.dataSource.data(), dataSourceData);
                break;
            case 14:
                PathURL = 'OverrideAllUploadValidatePnone04';
                var grid = $("#GridCM_UploadValidatePnone04").data("kendoGrid");
                angular.copy(grid.dataSource.data(), dataSourceData);
                break;
            case 15:
                PathURL = 'OverrideAllUploadValidateResidenceNo';
                var grid = $("#GridCM_UploadValidateResidenceNo").data("kendoGrid");
                angular.copy(grid.dataSource.data(), dataSourceData);
                break;
        }
        $http.post('/CMS_TRX_CaseUploading/' + PathURL, {
            'UploadedOBJECT': dataSourceData,
            'KaizenPublicKey': sessionStorage.PublicKey
        }).success(function (data) {
            if (data.Status == true) {
                $.smallBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#739E73",
                    iconSmall: "fa fa-times fa-2x fadeInRight animated",
                    timeout: 1000
                });
                $scope.CheckStep($scope.CurrentStep);
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
    $scope.ExportToPDF = function (row) {
        var GridID = '';
        switch ($scope.CurrentStep) {
            case 0:
                GridID = 'GridCM_UploadValidate00';
                break;
            case 1:
                GridID = 'GridCM_UploadValidate01';
                break;
            case 2:
                GridID = 'GridCM_UploadValidate02';
                break;
            case 3:
                GridID = 'GridCM_UploadValidate03';
                break;
            case 4:
                GridID = 'CM_UploadValidate04';
                break;
            case 5:
                GridID = 'GridCM_UploadValidateEmail01';
                break;
            case 6:
                GridID = 'GridCM_UploadValidateMobileNo1';
                break;
            case 7:
                GridID = 'GridCM_UploadValidateNationalityID';
                break;
            case 8:
                GridID = 'GridCM_UploadValidateOccupation';
                break;
            case 9:
                GridID = 'GridCM_UploadValidateOther01';
                break;
            case 10:
                GridID = 'GridCM_UploadValidatePassportNumber';
                break;
            case 11:
                GridID = 'GridCM_UploadValidatePnone01';
                break;
            case 12:
                GridID = 'GridCM_UploadValidatePnone02';
                break;
            case 13:
                GridID = 'GridCM_UploadValidatePnone03';
                break;
            case 14:
                GridID = 'GridCM_UploadValidatePnone04';
                break;
            case 15:
                GridID = 'GridCM_UploadValidateResidenceNo';
                break;
        }
        $scope.SaveAsPdf(GridID);
    };
    $scope.ExportToExcel = function (row) {
        var GridID = '';
        switch ($scope.CurrentStep) {
            case 0:
                GridID = 'GridCM_UploadValidate00';
                break;
            case 1:
                GridID = 'GridCM_UploadValidate01';
                break;
            case 2:
                GridID = 'GridCM_UploadValidate02';
                break;
            case 3:
                GridID = 'GridCM_UploadValidate03';
                break;
            case 4:
                GridID = 'CM_UploadValidate04';
                break;
            case 5:
                GridID = 'GridCM_UploadValidateEmail01';
                break;
            case 6:
                GridID = 'GridCM_UploadValidateMobileNo1';
                break;
            case 7:
                GridID = 'GridCM_UploadValidateNationalityID';
                break;
            case 8:
                GridID = 'GridCM_UploadValidateOccupation';
                break;
            case 9:
                GridID = 'GridCM_UploadValidateOther01';
                break;
            case 10:
                GridID = 'GridCM_UploadValidatePassportNumber';
                break;
            case 11:
                GridID = 'GridCM_UploadValidatePnone01';
                break;
            case 12:
                GridID = 'GridCM_UploadValidatePnone02';
                break;
            case 13:
                GridID = 'GridCM_UploadValidatePnone03';
                break;
            case 14:
                GridID = 'GridCM_UploadValidatePnone04';
                break;
            case 15:
                GridID = 'GridCM_UploadValidateResidenceNo';
                break;
        }
        $scope.SaveAsExcel(GridID);
    };

});