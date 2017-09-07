app.controller('Med_Co_DoctorController', function ($scope, $http) {
    $scope.Med00200 = {};
    $scope.SelectedView = {};
    $scope.PagesMed00200 = [];
    $scope.LoadLookUp = function () {
        $http.get('/HR_Set_Suffix/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.Suffixes = data;
        }).finally(function () { });
        $http.get('/HR_Set_Marital/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.Maritals = data;
        }).finally(function () { });
        $http.get('/Sys_Gender/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.Genders = data;
        }).finally(function () { });
        $http.get('/Med_Set_Position/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.Positions = data;
        }).finally(function () { });

    };
    $scope.LoadLookUp();

    $http.get('/Sys_View/GetViewsByScreen?ScreenID=1101&KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
        $scope.ViewList = data;
    }).finally(function () {
        if ($scope.ViewList.length > 0) {
            $scope.Med00200.ViewID = $scope.ViewList[0].ViewID;
            $scope.SelectedView = $scope.ViewList[0];
            $scope.ViewList.forEach(function (element, index) {
                if (element.IsDefault) {
                    $scope.Med00200.ViewID = element.ViewID;
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
                                $scope.AddMed00200();
                            });
                        },
                        menuButtons: [
                            { attributes: { "class": "btn-primary" }, text: "<span class=\"fa fa-plus-circle\"></span> New Doctor", click: function (e) { $scope.AddMed00200(); } },
                            { text: "<span class=\"fa fa-upload\"></span> Upload" }
                        ]
                    },
                    {
                        type: "splitButton",
                        text: "GO",
                        click: function (e) {
                            $scope.$apply(function () {
                                $scope.DoctorAddress();
                            })
                        },
                        menuButtons: [
                            {
                                type: "button", text: "<span class=\"fa fa-building-o\"></span> Address",
                                click: function (e) {
                                    $scope.$apply(function () {
                                        $scope.DoctorAddress();
                                    })
                                }
                            },
                            {
                                type: "button", text: "<span class=\"fa fa-user-o\"></span> Contact",
                                click: function (e) {
                                    $scope.$apply(function () {
                                        $scope.DoctorContact();
                                    })
                                }
                            },
                            {
                                type: "button", text: "<span class=\"\"></span> Doctor License", click: function (e) {
                                    $scope.$apply(function () {
                                        $scope.LicenseManagement();
                                    });
                                }
                            },
                            {
                                type: "button", text: "<span class=\"\"></span> Doctor Certification", click: function (e) {
                                    $scope.$apply(function () {
                                        $scope.CertificationManagement();
                                    });
                                }
                            },
                            {
                                type: "button", text: "<span class=\"\"></span> Doctor Education", click: function (e) {
                                    $scope.$apply(function () {
                                        $scope.EducationManagement();
                                    });
                                }
                            },
                            {
                                type: "button", text: "<span class=\"\"></span> Doctor Membership", click: function (e) {
                                    $scope.$apply(function () {
                                        $scope.MembershipManagement();
                                    });
                                }
                            },
                            {
                                type: "button", text: "<span class=\"\"></span> Doctor Specialty", click: function (e) {
                                    $scope.$apply(function () {
                                        $scope.SpecialtyManagement();
                                    });
                                }
                            },
                            {
                                type: "button", text: "<span class=\"\"></span> Doctor Language", click: function (e) {
                                    $scope.$apply(function () {
                                        $scope.LanguageManagement();
                                    });
                                }
                            },
                            {
                                type: "button", text: "<span class=\"\"></span> Doctor Insurance", click: function (e) {
                                    $scope.$apply(function () {
                                        $scope.InsuranceManagement();
                                    });
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
                    { type: "button", spriteCssClass: "k-tool-icon k-i-refresh" },
                    {
                        type: "button", text: " Filter", spriteCssClass: "k-tool-icon k-filter",
                        click: function (e) { $scope.OpenFilterWindow("GridMed_Co_Doctor", "Med_Co_Doctor"); }
                    },
                    { type: "separator" },
                    { template: "<label>View:</label>" },
                    {
                        template: "<select kendo-drop-down-list k-ng-model='SelectedView' k-on-change='ViewChanged()' k-options='DoctorViewOptions' id='GridViewoption_Med_Co_Doctor' style='width: 150px;'></select>",
                        overflow: "never"
                    }
            ]
            , resizable: true
        };
        $scope.DoctorViewOptions = {
            filter: "contains",
            model: "SelectedView",
            dataTextField: "ViewName",
            dataValueField: "ViewID",
            dataSource: $scope.ViewList,
            value: $scope.Med00200.ViewID
        };
    });

    $scope.LoadMed00200Page = function (ActionName) {
        removeEntityPage($scope.PagesMed00200);
        var URIPath = "/" + $scope.ToolBar.ActiveScreen.ID + "/" + ActionName + "?KaizenPublicKey=" + sessionStorage.PublicKey;
        var Page = { url: URIPath, ActionName: ActionName };
        $scope.PagesMed00200.push(Page);
    };
    $scope.ViewChanged = function () {
        $scope.Med00200.ViewID = $scope.SelectedView.ViewID;
        $scope.MainGridURL = "/Med_Co_Doctor/MainGrid?ViewID=" + $scope.SelectedView.ViewID + "&KaizenPublicKey=" + sessionStorage.PublicKey;
        //$scope.GridRefresh();
    };
    //------------------------Filter
    $scope.MYFilter.get = function (field) {
        var DS = {};
        if (field == 'SuffixID') {
            DS.key = "SuffixINam";
            DS.source = $scope.Suffixes;
        }
        if (field == 'MaritalID') {
            DS.key = "MaritalName";
            DS.source = $scope.Maritals;
        }
        if (field == 'GenderID') {
            DS.key = "GenderName";
            DS.source = $scope.Genders;
        }
        if (field == 'PositionID') {
            DS.key = "PositionName";
            DS.source = $scope.Positions;
        }
        return DS;
    };
    //---------------------------------------------------------------------------------------------
    $scope.LoadMed00200 = function (DoctorID) {
        if (angular.isUndefined($scope.Med00200.DoctorID)) {
            $http.get('/Med_Co_Doctor/GetSingle?KaizenPublicKey=' + sessionStorage.PublicKey + "&DoctorID=" + DoctorID).success(function (data) {
                $scope.Med00200 = data;
            }).finally(function () {
                $scope.Med00200.Status = 2;
                $scope.Med00200.BirthDate = kendoparsetoDate($scope.Med00200.BirthDate);
                $scope.Med00200.HireDate = kendoparsetoDate($scope.Med00200.HireDate);

                if ($scope.Med00200.PhotoExtension == null)
                    $scope.link = '/Photo/DoctorPhoto/DoctorID.jpg';
                else
                    $scope.link = '/Photo/DoctorPhoto/' + kaizenTrim($scope.Med00200.DoctorID) + "." + kaizenTrim($scope.Med00200.PhotoExtension) + "?c=" + Math.random();

                if ($scope.Med00200.SignatureExtension == null)
                    $scope.Signaturelink = '/Photo/DoctorSignature/DoctorSignature.png';
                else
                    $scope.Signaturelink = '/Photo/DoctorSignature/' + kaizenTrim($scope.Med00200.DoctorID) + "." + kaizenTrim($scope.Med00200.SignatureExtension) + "?c=" + Math.random();
                $scope.Med00201 = { DoctorID: $scope.Med00200.DoctorID, Status: 1 };
            });
        }
    };
    $scope.AddMed00200 = function () {
        $scope.LoadMed00200Page('Create');
        $scope.Clear();
        $scope.Med00200.Status = 1;
        $scope.Med00201.Status = 1;
        $http.get('/Med_Co_Doctor/DoctorAddressTypeFillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.DoctorAddressTypes = data;
        }).finally(function () { });
        $http.get('/Adm_Country/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.Countries = data;
        }).finally(function () { });
        $http.get('/Adm_City/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.Cities = data;
        }).finally(function () { });

    };
    $scope.EditMed00200 = function (DoctorID) {
        $scope.LoadMed00200Page('Create');
        $scope.LoadMed00200(DoctorID);
        $http.get('/Med_Co_Doctor/DoctorAddressTypeFillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey + '&ContactSourceID=1').success(function (data) {
            $scope.DoctorAddressTypes = data;
        }).finally(function () { });
        $http.get('/Adm_Country/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.Countries = data;
        }).finally(function () { });
        $http.get('/Adm_City/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.Cities = data;
        }).finally(function () { });
    };
    $scope.SaveData = function () {
        $scope.Med00200.BirthDate = kendoparsetoDate($scope.Med00200.BirthDate);
        $scope.Med00200.HireDate = kendoparsetoDate($scope.Med00200.HireDate);
        $http.post('/Med_Co_Doctor/SaveData', { 'Med00200': $scope.Med00200, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
            if (data.Status == true) {
                if ($scope.DoctorAddresses.length > 0) {
                    $http.post('/Med_Co_Doctor/SaveDoctorAddresses', { 'Med00201': $scope.DoctorAddresses, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
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
        $scope.Med00200.BirthDate = kendoparsetoDate($scope.Med00200.BirthDate);
        $scope.Med00200.HireDate = kendoparsetoDate($scope.Med00200.HireDate);
        $http.post('/Med_Co_Doctor/UpdateData', { 'Med00200': $scope.Med00200, 'KaizenPublicKey': sessionStorage.PublicKey, 'PhotoChanged': $scope.PhotoChanged }).success(function (data) {
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
        $scope.Med00200 = {};
        $scope.Med00203 = {};
        $scope.Med00204 = { SelectedEducationType: {} };
        $scope.Med00205 = { SelectedCertificationType: {} };
        $scope.Med00206 = {};

        $scope.Med00201 = { status: 0 };
        $scope.Med00200.Status = 1;
        $scope.Med00200.StatusID = '1'
        $scope.link = '/Photo/DoctorPhoto/DoctorID.jpg';
        $scope.Signaturelink = '/Photo/DoctorSignature/DoctorSignature.png';
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
                $http.post('/Med_Co_Doctor/DeleteData', { 'Med00200': $scope.Med00200, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
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
        $scope.PagesMed00200 = [];

        $scope.DoctorAddresses = [];
        $scope.DoctorLanguages = [];
        $scope.DoctorInsurances = [];
    };

    $scope.PhotoChanged = false; // 1= new ; 2 Edit
    $scope.SignatureChanged = false;

    $scope.DoctorBack = function (Doctor) {
        if (Doctor != null) {
            switch ($scope.CurrentControl) {
                case 'DoctorAddress':
                    $scope.Med00200 = Doctor;
                    $scope.Med00201 = { DoctorID: Doctor.DoctorID, Status: 1 };
                    $scope.GridRefresh('GridMed_DoctorAddressByDoctor');
                    break;
                case 'DoctorContact':
                    $scope.Med00200 = Doctor;
                    $scope.Med00203 = { DoctorID: Doctor.DoctorID, Status: 1 };
                    $scope.GridRefresh('GridMed_Co_DoctorContactByDoctor');
                    break;
                case 'DoctorLicense':
                    $scope.Med00200 = Doctor;
                    $scope.Med00206.DoctorID = Doctor.DoctorID;
                    $scope.GridRefresh('GridMed_Co_LicenseByDoctor');
                    break;
                case 'DoctorCertification':
                    $scope.Med00200 = Doctor;
                    $scope.Med00205.DoctorID = Doctor.DoctorID;
                    $scope.GridRefresh('GridMed_Co_CertificationByDoctor');
                    break;
                case 'DoctorEducation':
                    $scope.Med00200 = Doctor;
                    $scope.Med00204.DoctorID = Doctor.DoctorID;
                    $scope.GridRefresh('GridMed_Co_EducationByDoctor');
                    break;
                case 'DoctorMembership':
                    $scope.Med00200 = Doctor;
                    $scope.Med00207.DoctorID = Doctor.DoctorID;
                    $scope.GridRefresh('GridMed_Co_MembershipByDoctor');
                    break;
                case 'DoctorSpecialty':
                    $scope.Med00200 = Doctor;
                    $scope.Med00209.DoctorID = Doctor.DoctorID;
                    $scope.GridRefresh('GridMed_Co_SpecialtyByDoctor');
                    break;
                case 'DoctorLanguage':
                    $scope.Med00200 = Doctor;
                    $http.get('/Med_Co_Doctor/GetLanguagesByDoctor?KaizenPublicKey=' + sessionStorage.PublicKey, {
                        params: {
                            DoctorID: $scope.Med00200.DoctorID
                        }
                    }).success(function (data) {
                        $scope.DoctorLanguages = data;
                        for (var i = 0; i < $scope.Languages.length; i++) {
                            var item = $scope.Languages[i];
                            item.isSelected = false;
                            for (var j = 0; j < $scope.DoctorLanguages.length; j++) {
                                var obj = $scope.DoctorLanguages[j];
                                if (obj.LanguagID === item.LanguagID) {
                                    item.isSelected = true;
                                    break;
                                }
                            }
                        }
                    }).finally(function () { });
                    break;
                case 'DoctorInsurance':
                    $scope.Med00200 = Doctor;
                    $http.get('/Med_Co_Doctor/GetInsurancesByDoctor?KaizenPublicKey=' + sessionStorage.PublicKey, {
                        params: {
                            DoctorID: $scope.Med00200.DoctorID
                        }
                    }).success(function (data) {
                        $scope.DoctorInsurances = data;
                        for (var i = 0; i < $scope.Insurances.length; i++) {
                            var item = $scope.Insurances[i];
                            item.isSelected = false;
                            for (var j = 0; j < $scope.DoctorInsurances.length; j++) {
                                var obj = $scope.DoctorInsurances[j];
                                if (obj.InsuranceID === item.InsuranceID) {
                                    item.isSelected = true;
                                    break;
                                }
                            }
                        }
                    }).finally(function () { });
                    break;
            }
        }
    };
    $scope.NationalityBack = function (nationality) {
        if (nationality != null) {
            $scope.Med00200.NationalityID = nationality.NationalityID;
        }
    };
    $scope.DoctorContactBack = function (contact) {
        if (contact != null) {
            $scope.Med00200.ContactTypeID = contact.ContactTypeID;
        }
    };
    $scope.DoctorAddressBack = function (address) {
        if (address != null) {
            $scope.Med00200.AddressType = address.AddressCode;
        }
    };

    // Setting Photo path extension
    $scope.SetPhotoExtension = function (PhotoPath) {
        $scope.Med00200.PhotoExtension = PhotoPath;
        $scope.link = '/Photo/DoctorPhotoTemp/' + kaizenTrim(PhotoPath);
        $scope.PhotoChanged = true;
        // $scope.$apply();
    };
    $scope.RemovePhotoExtension = function (PhotoPath) {
        $scope.Med00200.PhotoExtension = "";
    };

    $scope.SetSignatureExtension = function (PhotoPath) {
        $scope.Med00200.SignatureExtension = PhotoPath;
        $scope.Signaturelink = '/Photo/DoctorSignatureTemp/' + kaizenTrim(PhotoPath);
        $scope.SignatureChanged = true;
    };
    $scope.RemoveSignatureExtension = function (PhotoPath) {
        $scope.Med00200.SignatureExtension = "";
    };
    //-------------------------- Address Information
    $scope.Med00201 = {};
    $scope.DoctorAddresses = [];
    $scope.DoctorAddress = function () {
        removeEntityPage($scope.PagesMed00200);
        $scope.LoadMed00200Page('DoctorAddress');
        $scope.Med00201.Status = 1;
        $http.get('/Med_Co_Doctor/DoctorAddressTypeFillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.DoctorAddressTypes = data;
        }).finally(function () { });
        $http.get('/Adm_Country/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.Countries = data;
        }).finally(function () { });
        $http.get('/Adm_City/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.Cities = data;
        }).finally(function () { });
    };
    $scope.SaveDoctorAddress = function () {
        var grid = $("#GridMed_DoctorAddressByDoctor").data("kendoGrid");
        var dataSourceData = grid.dataSource.data();

        if ($scope.functiontofindIndexByKeyValue(dataSourceData, "AddressCode", $scope.Med00201.AddressCode) != null) {
            $.smallBox({
                title: "No Changes have been made!!",
                content: "<i class='fa fa-clock-o'></i> <i>This Type is already has data in this Doctor !!</i>",
                color: "#3276B1",
                iconSmall: "fa fa-times fa-2x fadeInRight animated",
                timeout: 4000
            });
            return;
        }
        $scope.Med00201.DoctorID = $scope.Med00200.DoctorID;
        $http.post('/Med_Co_Doctor/SaveDoctorAddress', { 'Med00201': $scope.Med00201, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
            if (data.Status == true) {
                $.bigBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#739E73",
                    timeout: 4000,
                    icon: "fa fa-check shake animated"
                });
                $scope.GridRefresh('GridMed_DoctorAddressByDoctor');
                $scope.Med00201 = { Status: 1 };
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
    $scope.UpdateDoctorAddress = function () {
        $scope.Med00201.DoctorID = $scope.Med00200.DoctorID;
        $http.post('/Med_Co_Doctor/UpdateDoctorAddress', { 'Med00201': $scope.Med00201, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
            if (data.Status == true) {
                $.bigBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#739E73",
                    timeout: 4000,
                    icon: "fa fa-check shake animated"
                });
                $scope.GridRefresh('GridMed_DoctorAddressByDoctor');
                $scope.Med00201 = { Status: 1 };
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
    $scope.DeleteDoctorAddress = function () {
        $.SmartMessageBox({
            title: "Delete Record",
            content: "Are you sure you want to delete this?",
            buttons: '[No][Yes]'
        }, function (ButtonPressed) {
            if (ButtonPressed === "Yes") {
                $http.post('/Med_Co_Doctor/DeleteDoctorAddress', { 'Med00201': $scope.Med00201, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
                    if (data.Status == true) {
                        $.bigBox({
                            title: data.Massage,
                            content: data.Description,
                            color: "#739E73",
                            timeout: 4000,
                            icon: "fa fa-check shake animated"
                        });
                        $scope.GridRefresh('GridMed_DoctorAddressByDoctor');
                        $scope.Med00201 = { Status: 1 };
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
                $scope.Med00201 = { Status: 1 };
            }
        });
    };
    $scope.AddressTypeChanged = function () {
        if ($scope.Med00200.Status == 1) {
            var found = $scope.functiontofindObjectByKeyValue($scope.DoctorAddresses, "AddressCode", $scope.Med00201.AddressCode);
            if (found != null) {
                $scope.Med00201 = found;
                $scope.Med00201.Status = 2;
            }
            else {
                $scope.Med00201 = { AddressCode: $scope.Med00201.AddressCode, Status: 1 };
                var addressName = $scope.functiontofindObjectByKeyValue($scope.DoctorAddressTypes, "AddressCode", $scope.Med00201.AddressCode);
                $scope.Med00201.AddressName = addressName.AddressTypeName;
            }
        }
        else {
            var grid = $("#GridMed_DoctorAddressByDoctor").data("kendoGrid");
            var dataSourceData = [];
            angular.copy(grid.dataSource.data(), dataSourceData);
            var found = $scope.functiontofindObjectByKeyValue(dataSourceData, "AddressCode", $scope.Med00201.AddressCode);
            if (found != null) {
                $scope.Med00201 = found;
                $scope.Med00201.Status = 2;
                return;
            }
            else {
                $scope.Med00201 = { Status: 1, AddressCode: $scope.Med00201.AddressCode };
                return;
            }
        }
    };
    $scope.AddNewAddress = function () {
        $scope.Med00201.DoctorID = $scope.Med00200.DoctorID;
        $scope.DoctorAddresses.push($scope.Med00201);
        $scope.Med00201 = { Status: 1 };
    };
    $scope.UpdateAddress = function () {
        var index = $scope.GetSingleIndex($scope.DoctorAddresses, "AddressCode", $scope.Med00201.AddressCode);
        $scope.DoctorAddresses.splice(index, 1, $scope.Med00201);
        $scope.Med00201 = { Status: 1 };
    };
    $scope.RemoveAddress = function (address, index) {
        $scope.DoctorAddresses.splice(index, 1);
    };
    $scope.OnAddressTabSelected = function () {
        if ($scope.Med00200.Status == 2) {
            $scope.GridRefresh('GridMed_DoctorAddressByDoctor');
        }
    };

    //-------------------------- Contact Information
    $scope.Med00203 = {};
    $scope.DoctorContact = function () {
        removeEntityPage($scope.PagesMed00200);
        $scope.LoadMed00200Page('DoctorContact');
        $scope.Med00203.Status = 1;
        $http.get('/Med_Co_Doctor/DoctorContactTypeFillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.DoctorContactTypes = data;
        }).finally(function () { });

    };
    $scope.SaveDoctorContact = function () {
        var grid = $("#GridMed_Co_DoctorContactByDoctor").data("kendoGrid");
        var dataSourceData = grid.dataSource.data();

        if ($scope.functiontofindIndexByKeyValue(dataSourceData, "ContactTypeID", $scope.Med00203.ContactTypeID) != null) {
            $.smallBox({
                title: "No Changes have been made!!",
                content: "<i class='fa fa-clock-o'></i> <i>This Type is already has data in this Doctor !!</i>",
                color: "#3276B1",
                iconSmall: "fa fa-times fa-2x fadeInRight animated",
                timeout: 4000
            });
            return;
        }
        $scope.Med00203.DoctorID = $scope.Med00200.DoctorID;
        $http.post('/Med_Co_Doctor/SaveDoctorContact', { 'Med00203': $scope.Med00203, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
            if (data.Status == true) {
                $.bigBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#739E73",
                    timeout: 4000,
                    icon: "fa fa-check shake animated"
                });
                $scope.GridRefresh('GridMed_Co_DoctorContactByDoctor');
                $scope.Med00203 = { Status: 1 };
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
    $scope.UpdateDoctorContact = function () {
        $scope.Med00203.DoctorID = $scope.Med00200.DoctorID;
        $http.post('/Med_Co_Doctor/UpdateDoctorContact', { 'Med00203': $scope.Med00203, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
            if (data.Status == true) {
                $.bigBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#739E73",
                    timeout: 4000,
                    icon: "fa fa-check shake animated"
                });
                $scope.GridRefresh('GridMed_Co_DoctorContactByDoctor');
                $scope.Med00203 = { Status: 1 };
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
    $scope.DeleteDoctorContact = function () {
        $.SmartMessageBox({
            title: "Delete Record",
            content: "Are you sure you want to delete this?",
            buttons: '[No][Yes]'
        }, function (ButtonPressed) {
            if (ButtonPressed === "Yes") {
                $http.post('/Med_Co_Doctor/DeleteDoctorContact', { 'Med00203': $scope.Med00203, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
                    if (data.Status == true) {
                        $.bigBox({
                            title: data.Massage,
                            content: data.Description,
                            color: "#739E73",
                            timeout: 4000,
                            icon: "fa fa-check shake animated"
                        });
                        $scope.GridRefresh('GridMed_Co_DoctorContactByDoctor');
                        $scope.Med00203 = { Status: 1 };
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
                $scope.Med00203 = { Status: 1 };
            }
        });
    };
    $scope.ContactTypeChanged = function () {
        var grid = $("#GridMed_Co_DoctorContactByDoctor").data("kendoGrid");
        var dataSourceData = [];
        angular.copy(grid.dataSource.data(), dataSourceData);
        var found = $scope.functiontofindObjectByKeyValue(dataSourceData, "ContactTypeID", $scope.Med00203.ContactTypeID);
        if (found != null) {
            $scope.Med00203 = found;
            $scope.Med00203.Status = 2;
            return;
        }
        else {
            $scope.Med00203 = { Status: 1, ContactTypeID: $scope.Med00203.ContactTypeID };
            return;
        }
    };

    //-------------------------- Doctor License
    $scope.Med00206 = {};
    $scope.LicenseManagement = function () {
        removeEntityPage($scope.PagesMed00200);
        $scope.LoadMed00200Page('DoctorLicense');
        $scope.Med00206.Status = 1;
        $http.get('/Med_Set_LicenseType/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.LicenseTypes = data;
        }).finally(function () { });
        $http.get('/Adm_Country/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.countries = data;
        }).finally(function () {
        });
        $http.get('/Adm_City/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.cities = data;
        }).finally(function () {
        })
    };
    $scope.MedicalLicenseAgentBack = function (license_agent) {
        if (license_agent != null) {
            switch ($scope.CurrentControl) {
                case 'License':
                    $scope.Med00206.LicenseAgentID = license_agent.LicenseAgentID;
                    break;
                case 'Certification':
                    $scope.Med00205.LicenseAgentID = license_agent.LicenseAgentID;
                    $scope.Med00205.LicenseAgentName = license_agent.LicenseAgentName;
                    break;
            }
        }
    };

    $scope.SaveDoctorLicense = function () {
        var grid = $("#GridMed_Co_LicenseByDoctor").data("kendoGrid");
        var dataSourceData = grid.dataSource.data();

        if ($scope.functiontofindIndexByKeyValue(dataSourceData, "LicenseTypeID", $scope.Med00206.LicenseTypeID) != null) {
            $.smallBox({
                title: "Already Allocated",
                content: "<i class='fa fa-clock-o'></i> <i>This License is already allocated to this Doctor !!</i>",
                color: "#3276B1",
                iconSmall: "fa fa-times fa-2x fadeInRight animated",
                timeout: 4000
            });
            return;
        }
        $scope.Med00206.DoctorID = $scope.Med00200.DoctorID;
        if ($scope.Med00206.LicenseIssueDate != '' && $scope.Med00206.LicenseIssueDate != undefined) {
            $scope.Med00206.LicenseIssueDate = kendoparsetoDate($scope.Med00206.LicenseIssueDate);
        }
        if ($scope.Med00206.LicenseExpiryDate != '' && $scope.Med00206.LicenseExpiryDate != undefined) {
            $scope.Med00206.LicenseExpiryDate = kendoparsetoDate($scope.Med00206.LicenseExpiryDate);
        }
        if ($scope.Med00206.LicenseRenewerDate != '' && $scope.Med00206.LicenseRenewerDate != undefined) {
            $scope.Med00206.LicenseRenewerDate = kendoparsetoDate($scope.Med00206.LicenseRenewerDate);
        }
        $http.post('/Med_Co_Doctor/SaveDoctorLicense', { 'Med00206': $scope.Med00206, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
            if (data.Status == true) {
                $.bigBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#739E73",
                    timeout: 4000,
                    icon: "fa fa-check shake animated"
                });
                $scope.GridRefresh('GridMed_Co_LicenseByDoctor');
                $scope.Med00206 = { Status: 1 };
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
    $scope.UpdateDoctorLicense = function () {
        $scope.Med00206.DoctorID = $scope.Med00200.DoctorID;
        if ($scope.Med00206.LicenseIssueDate != '' && $scope.Med00206.LicenseIssueDate != undefined) {
            $scope.Med00206.LicenseIssueDate = kendoparsetoDate($scope.Med00206.LicenseIssueDate);
        }
        if ($scope.Med00206.LicenseExpiryDate != '' && $scope.Med00206.LicenseExpiryDate != undefined) {
            $scope.Med00206.LicenseExpiryDate = kendoparsetoDate($scope.Med00206.LicenseExpiryDate);
        }
        if ($scope.Med00206.LicenseRenewerDate != '' && $scope.Med00206.LicenseRenewerDate != undefined) {
            $scope.Med00206.LicenseRenewerDate = kendoparsetoDate($scope.Med00206.LicenseRenewerDate);
        }
        $http.post('/Med_Co_Doctor/UpdateDoctorLicense', { 'Med00206': $scope.Med00206, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
            if (data.Status == true) {
                $.bigBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#739E73",
                    timeout: 4000,
                    icon: "fa fa-check shake animated"
                });
                $scope.GridRefresh('GridMed_Co_LicenseByDoctor');
                $scope.Med00206 = { Status: 1 };
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
    $scope.DeleteDoctorLicense = function () {
        $.SmartMessageBox({
            title: "Delete Record",
            content: "Are you sure you want to delete this?",
            buttons: '[No][Yes]'
        }, function (ButtonPressed) {
            if (ButtonPressed === "Yes") {
                $http.post('/Med_Co_Doctor/DeleteDoctorLicense', { 'Med00206': $scope.Med00206, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
                    if (data.Status == true) {
                        $.bigBox({
                            title: data.Massage,
                            content: data.Description,
                            color: "#739E73",
                            timeout: 4000,
                            icon: "fa fa-check shake animated"
                        });
                        $scope.GridRefresh('GridMed_Co_LicenseByDoctor');
                        $scope.Med00206 = { Status: 1 };
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

    //-------------------------- Doctor Certification
    $scope.Med00205 = { SelectedCertificationType: {} };
    $scope.CertificationManagement = function () {
        removeEntityPage($scope.PagesMed00200);
        $scope.LoadMed00200Page('DoctorCertification');
        $scope.Med00205.Status = 1;
        $http.get('/Med_Set_CertificationType/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.CertificationTypes = data;
        }).finally(function () { });
        $http.get('/Adm_Country/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.countries = data;
        }).finally(function () {
        });
        $http.get('/Adm_City/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.cities = data;
        }).finally(function () {
        })
    };
    $scope.CertificationTypeChanged = function () {
        $scope.Med00205.CertificationTypeID = $scope.Med00205.SelectedCertificationType.CertificationTypeID;
        $scope.Med00205.CertificationTypeName = $scope.Med00205.SelectedCertificationType.CertificationTypeName;
    };

    $scope.SaveDoctorCertification = function () {
        $scope.Med00205.DoctorID = $scope.Med00200.DoctorID;
        if ($scope.Med00205.CertIssueDate != '' && $scope.Med00205.CertIssueDate != undefined) {
            $scope.Med00205.CertIssueDate = kendoparsetoDate($scope.Med00205.CertIssueDate);
        }
        if ($scope.Med00205.CertExpiryDate != '' && $scope.Med00205.CertExpiryDate != undefined) {
            $scope.Med00205.CertExpiryDate = kendoparsetoDate($scope.Med00205.CertExpiryDate);
        }
        if ($scope.Med00205.CertRenewerDate != '' && $scope.Med00205.CertRenewerDate != undefined) {
            $scope.Med00205.CertRenewerDate = kendoparsetoDate($scope.Med00205.CertRenewerDate);
        }
        $http.post('/Med_Co_Doctor/SaveDoctorCertification', { 'Med00205': $scope.Med00205, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
            if (data.Status == true) {
                $.bigBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#739E73",
                    timeout: 4000,
                    icon: "fa fa-check shake animated"
                });
                $scope.GridRefresh('GridMed_Co_CertificationByDoctor');
                $scope.Med00205 = { Status: 1 };
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
    $scope.UpdateDoctorCertification = function () {
        $scope.Med00205.DoctorID = $scope.Med00200.DoctorID;
        if ($scope.Med00205.CertIssueDate != '' && $scope.Med00205.CertIssueDate != undefined) {
            $scope.Med00205.CertIssueDate = kendoparsetoDate($scope.Med00205.CertIssueDate);
        }
        if ($scope.Med00205.CertExpiryDate != '' && $scope.Med00205.CertExpiryDate != undefined) {
            $scope.Med00205.CertExpiryDate = kendoparsetoDate($scope.Med00205.CertExpiryDate);
        }
        if ($scope.Med00205.CertRenewerDate != '' && $scope.Med00205.CertRenewerDate != undefined) {
            $scope.Med00205.CertRenewerDate = kendoparsetoDate($scope.Med00205.CertRenewerDate);
        }
        $http.post('/Med_Co_Doctor/UpdateDoctorCertification', { 'Med00205': $scope.Med00205, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
            if (data.Status == true) {
                $.bigBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#739E73",
                    timeout: 4000,
                    icon: "fa fa-check shake animated"
                });
                $scope.GridRefresh('GridMed_Co_CertificationByDoctor');
                $scope.Med00205 = { Status: 1 };
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
    $scope.DeleteDoctorCertification = function () {
        $.SmartMessageBox({
            title: "Delete Record",
            content: "Are you sure you want to delete this?",
            buttons: '[No][Yes]'
        }, function (ButtonPressed) {
            if (ButtonPressed === "Yes") {
                $http.post('/Med_Co_Doctor/DeleteDoctorCertification', { 'Med00205': $scope.Med00205, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
                    if (data.Status == true) {
                        $.bigBox({
                            title: data.Massage,
                            content: data.Description,
                            color: "#739E73",
                            timeout: 4000,
                            icon: "fa fa-check shake animated"
                        });
                        $scope.GridRefresh('GridMed_Co_CertificationByDoctor');
                        $scope.Med00205 = { Status: 1 };
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

    //-------------------------- Doctor Education
    $scope.Med00204 = { SelectedEducationType: {} };
    $scope.EducationManagement = function () {
        removeEntityPage($scope.PagesMed00200);
        $scope.LoadMed00200Page('DoctorEducation');
        $scope.Med00204.Status = 1;
        $http.get('/Med_Set_EducationType/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.EducationTypes = data;
        }).finally(function () { });
    };
    $scope.EducationTypeChanged = function () {
        $scope.Med00204.EducationTypeID = $scope.Med00204.SelectedEducationType.EducationTypeID;
        $scope.Med00204.EducationTypeName = $scope.Med00204.SelectedEducationType.EducationTypeName;
    };

    $scope.SaveDoctorEducation = function () {
        $scope.Med00204.DoctorID = $scope.Med00200.DoctorID;
        if ($scope.Med00204.EduDate != '' && $scope.Med00204.EduDate != undefined) {
            $scope.Med00204.EduDate = kendoparsetoDate($scope.Med00204.EduDate);
        }
        $http.post('/Med_Co_Doctor/SaveDoctorEducation', { 'Med00204': $scope.Med00204, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
            if (data.Status == true) {
                $.bigBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#739E73",
                    timeout: 4000,
                    icon: "fa fa-check shake animated"
                });
                $scope.GridRefresh('GridMed_Co_EducationByDoctor');
                $scope.Med00204 = { Status: 1 };
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
    $scope.UpdateDoctorEducation = function () {
        $scope.Med00204.DoctorID = $scope.Med00200.DoctorID;
        if ($scope.Med00204.EduDate != '' && $scope.Med00204.EduDate != undefined) {
            $scope.Med00204.EduDate = kendoparsetoDate($scope.Med00204.EduDate);
        }
        $http.post('/Med_Co_Doctor/UpdateDoctorEducation', { 'Med00204': $scope.Med00204, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
            if (data.Status == true) {
                $.bigBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#739E73",
                    timeout: 4000,
                    icon: "fa fa-check shake animated"
                });
                $scope.GridRefresh('GridMed_Co_EducationByDoctor');
                $scope.Med00204 = { Status: 1 };
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
    $scope.DeleteDoctorEducation = function () {
        $.SmartMessageBox({
            title: "Delete Record",
            content: "Are you sure you want to delete this?",
            buttons: '[No][Yes]'
        }, function (ButtonPressed) {
            if (ButtonPressed === "Yes") {
                $http.post('/Med_Co_Doctor/DeleteDoctorEducation', { 'Med00204': $scope.Med00204, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
                    if (data.Status == true) {
                        $.bigBox({
                            title: data.Massage,
                            content: data.Description,
                            color: "#739E73",
                            timeout: 4000,
                            icon: "fa fa-check shake animated"
                        });
                        $scope.GridRefresh('GridMed_Co_EducationByDoctor');
                        $scope.Med00204 = { Status: 1 };
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

    //-------------------------- Doctor Membership
    $scope.Med00207 = { SelectedMembershipType: {} };
    $scope.MembershipManagement = function () {
        removeEntityPage($scope.PagesMed00200);
        $scope.LoadMed00200Page('DoctorMembership');
        $scope.Med00207.Status = 1;
        $http.get('/Med_Set_MembershipType/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.MembershipTypes = data;
        }).finally(function () { });
    };
    $scope.MembershipTypeChanged = function () {
        $scope.Med00207.MembershipTypeID = $scope.Med00207.SelectedMembershipType.MembershipTypeID;
        $scope.Med00207.MembershipTypeName = $scope.Med00207.SelectedMembershipType.MembershipTypeName;
    };

    $scope.SaveDoctorMembership = function () {
        $scope.Med00207.DoctorID = $scope.Med00200.DoctorID;
        var grid = $("#GridMed_Co_MembershipByDoctor").data("kendoGrid");
        var dataSourceData = grid.dataSource.data();
        if ($scope.functiontofindIndexByKeyValue(dataSourceData, "MembershipTypeID", $scope.Med00207.MembershipTypeID) != null) {
            $.smallBox({
                title: "Already Allocated",
                content: "<i class='fa fa-clock-o'></i> <i>This License is already allocated to this Doctor !!</i>",
                color: "#3276B1",
                iconSmall: "fa fa-times fa-2x fadeInRight animated",
                timeout: 4000
            });
            return;
        }

        if ($scope.Med00207.IssueDate != '' && $scope.Med00207.IssueDate != undefined) {
            $scope.Med00207.IssueDate = kendoparsetoDate($scope.Med00207.IssueDate);
        }
        if ($scope.Med00207.ExpiryDate != '' && $scope.Med00207.ExpiryDate != undefined) {
            $scope.Med00207.ExpiryDate = kendoparsetoDate($scope.Med00207.ExpiryDate);
        }
        if ($scope.Med00207.RenewerDate != '' && $scope.Med00207.RenewerDate != undefined) {
            $scope.Med00207.RenewerDate = kendoparsetoDate($scope.Med00207.RenewerDate);
        }
        $http.post('/Med_Co_Doctor/SaveDoctorMembership', { 'Med00207': $scope.Med00207, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
            if (data.Status == true) {
                $.bigBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#739E73",
                    timeout: 4000,
                    icon: "fa fa-check shake animated"
                });
                $scope.GridRefresh('GridMed_Co_MembershipByDoctor');
                $scope.Med00207 = { Status: 1 };
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
    $scope.UpdateDoctorMembership = function () {
        $scope.Med00207.DoctorID = $scope.Med00200.DoctorID;
        if ($scope.Med00207.IssueDate != '' && $scope.Med00207.IssueDate != undefined) {
            $scope.Med00207.IssueDate = kendoparsetoDate($scope.Med00207.IssueDate);
        }
        if ($scope.Med00207.ExpiryDate != '' && $scope.Med00207.ExpiryDate != undefined) {
            $scope.Med00207.ExpiryDate = kendoparsetoDate($scope.Med00207.ExpiryDate);
        }
        if ($scope.Med00207.RenewerDate != '' && $scope.Med00207.RenewerDate != undefined) {
            $scope.Med00207.RenewerDate = kendoparsetoDate($scope.Med00207.RenewerDate);
        }
        $http.post('/Med_Co_Doctor/UpdateDoctorMembership', { 'Med00207': $scope.Med00207, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
            if (data.Status == true) {
                $.bigBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#739E73",
                    timeout: 4000,
                    icon: "fa fa-check shake animated"
                });
                $scope.GridRefresh('GridMed_Co_MembershipByDoctor');
                $scope.Med00207 = { Status: 1 };
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
    $scope.DeleteDoctorMembership = function () {
        $.SmartMessageBox({
            title: "Delete Record",
            content: "Are you sure you want to delete this?",
            buttons: '[No][Yes]'
        }, function (ButtonPressed) {
            if (ButtonPressed === "Yes") {
                $http.post('/Med_Co_Doctor/DeleteDoctorMembership', { 'Med00207': $scope.Med00207, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
                    if (data.Status == true) {
                        $.bigBox({
                            title: data.Massage,
                            content: data.Description,
                            color: "#739E73",
                            timeout: 4000,
                            icon: "fa fa-check shake animated"
                        });
                        $scope.GridRefresh('GridMed_Co_MembershipByDoctor');
                        $scope.Med00207 = { Status: 1 };
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

    //-------------------------- Doctor Specialty
    $scope.Med00209 = { SelectedSpecialtyType: {} };
    $scope.SpecialtyManagement = function () {
        removeEntityPage($scope.PagesMed00200);
        $scope.LoadMed00200Page('DoctorSpecialty');
        $scope.Med00209.Status = 1;
        $http.get('/Med_Set_SpecialtyType/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.SpecialtyTypes = data;
        }).finally(function () { });
    };
    $scope.SpecialtyTypeChanged = function () {
        $scope.Med00209.SpecialtyTypeID = $scope.Med00209.SelectedSpecialtyType.SpecialtyTypeID;
        $scope.Med00209.SpecialtyTypeName = $scope.Med00209.SelectedSpecialtyType.SpecialtyTypeName;
    };

    $scope.SaveDoctorSpecialty = function () {
        $scope.Med00209.DoctorID = $scope.Med00200.DoctorID;
        var grid = $("#GridMed_Co_SpecialtyByDoctor").data("kendoGrid");
        var dataSourceData = grid.dataSource.data();
        if ($scope.functiontofindIndexByKeyValue(dataSourceData, "SpecialtyTypeID", $scope.Med00209.SpecialtyTypeID) != null) {
            $.smallBox({
                title: "Already Allocated",
                content: "<i class='fa fa-clock-o'></i> <i>This License is already allocated to this Doctor !!</i>",
                color: "#3276B1",
                iconSmall: "fa fa-times fa-2x fadeInRight animated",
                timeout: 4000
            });
            return;
        }

        $http.post('/Med_Co_Doctor/SaveDoctorSpecialty', { 'Med00209': $scope.Med00209, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
            if (data.Status == true) {
                $.bigBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#739E73",
                    timeout: 4000,
                    icon: "fa fa-check shake animated"
                });
                $scope.GridRefresh('GridMed_Co_SpecialtyByDoctor');
                $scope.Med00209 = { Status: 1 };
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
    $scope.UpdateDoctorSpecialty = function () {
        $scope.Med00209.DoctorID = $scope.Med00200.DoctorID;
        $http.post('/Med_Co_Doctor/UpdateDoctorSpecialty', { 'Med00209': $scope.Med00209, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
            if (data.Status == true) {
                $.bigBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#739E73",
                    timeout: 4000,
                    icon: "fa fa-check shake animated"
                });
                $scope.GridRefresh('GridMed_Co_SpecialtyByDoctor');
                $scope.Med00209 = { Status: 1 };
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
    $scope.DeleteDoctorSpecialty = function () {
        $.SmartMessageBox({
            title: "Delete Record",
            content: "Are you sure you want to delete this?",
            buttons: '[No][Yes]'
        }, function (ButtonPressed) {
            if (ButtonPressed === "Yes") {
                $http.post('/Med_Co_Doctor/DeleteDoctorSpecialty', { 'Med00209': $scope.Med00209, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
                    if (data.Status == true) {
                        $.bigBox({
                            title: data.Massage,
                            content: data.Description,
                            color: "#739E73",
                            timeout: 4000,
                            icon: "fa fa-check shake animated"
                        });
                        $scope.GridRefresh('GridMed_Co_SpecialtyByDoctor');
                        $scope.Med00209 = { Status: 1 };
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

    //------------- Doctor Languages
    $scope.DoctorLanguages = [];
    $scope.Languages = [];
    $scope.LanguageManagement = function () {
        removeEntityPage($scope.PagesMed00200);
        $scope.LoadMed00200Page('DoctorLanguage');
        $http.get('/Med_Set_Language/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.Languages = data;
        }).finally(function () { });
    };

    $scope.CheckLanguage = function (lang) {
        if (lang.isSelected) {
            $http({
                url: '/Med_Co_Doctor/SaveDoctorLanguage?KaizenPublicKey=' + sessionStorage.PublicKey,
                method: "POST",
                data: $.param({
                    DoctorID: $scope.Med00200.DoctorID,
                    LanguagID: lang.LanguagID
                }),
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
        else {
            $http({
                url: '/Med_Co_Doctor/DeleteDoctorLanguage?KaizenPublicKey=' + sessionStorage.PublicKey,
                method: "POST",
                data: $.param({
                    DoctorID: $scope.Med00200.DoctorID,
                    LanguagID: lang.LanguagID
                }),
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
    };
    $scope.SaveAll = function () {
        $scope.Languages.forEach(function (lang, index) {
            if (!lang.isSelected) {
                $http({
                    url: '/Sys_role/SaveDoctorLanguage?KaizenPublicKey=' + sessionStorage.PublicKey,
                    method: "POST",
                    data: $.param({
                        DoctorID: $scope.Med00200.DoctorID,
                        LanguagID: lang.LanguagID
                    }),
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
        })
    };

    //------------- Doctor Insurances
    $scope.DoctorInsurances = [];
    $scope.Insurances = [];
    $scope.InsuranceManagement = function () {
        removeEntityPage($scope.PagesMed00200);
        $scope.LoadMed00200Page('DoctorInsurance');
        $http.get('/Med_Co_Insurance/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.Insurances = data;
        }).finally(function () { });
    };

    $scope.CheckInsurance = function (insurance) {
        if (insurance.isSelected) {
            $http({
                url: '/Med_Co_Doctor/SaveDoctorInsurance?KaizenPublicKey=' + sessionStorage.PublicKey,
                method: "POST",
                data: $.param({
                    DoctorID: $scope.Med00200.DoctorID,
                    InsuranceID: insurance.InsuranceID
                }),
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
        else {
            $http({
                url: '/Med_Co_Doctor/DeleteDoctorInsurance?KaizenPublicKey=' + sessionStorage.PublicKey,
                method: "POST",
                data: $.param({
                    DoctorID: $scope.Med00200.DoctorID,
                    InsuranceID: insurance.InsuranceID
                }),
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
    };
    $scope.SaveAllInsurance = function () {
        $scope.Insurances.forEach(function (insurance, index) {
            if (!insurance.isSelected) {
                $http({
                    url: '/Sys_role/SaveDoctorInsurance?KaizenPublicKey=' + sessionStorage.PublicKey,
                    method: "POST",
                    data: $.param({
                        DoctorID: $scope.Med00200.DoctorID,
                        InsuranceID: insurance.InsuranceID
                    }),
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
        })
    };

});
