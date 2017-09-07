app.controller('CMS_TRX_CaseUploadingController', function ($scope, $http) {
    $scope.UploadObject = {};
    $scope.UploadObject.BookingDate = new Date();
    $scope.UploadObject.DecimalDigit = $scope.MY.DecimalDigit;
    $scope.UploadObject.CurrencyCode = $scope.MY.CurrencyCode;
    $scope.UploadObject.ExchangeTableID = $scope.MY.ExchangeTableID;
    $scope.UploadObject.IsMultiply = $scope.MY.IsMultiply;
    $scope.UploadObject.ExchangeRateID = $scope.MY.ExchangeRateID;
    $scope.UploadObject.ExchangeRate = $scope.MY.ExchangeRate;
    $scope.LoadLookUp = function () {
        //$scope.ShowGridCM_UploadValidate03 = false;
        //$scope.ShowCM_UploadValidate04 = false;
        $scope.CaseStatusList();
        $scope.CaseDocType();
        $scope.DebtorAddressCodeType();
        $http.get('/CMS_CaseStatus/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey)
              .success(function (data) {
                  $scope.CM00700Listss = data;
                  $scope.CaseStatusestemp = [];
                  for (var T = 0; T < $scope.CM00700Listss.length; T++) {
                      if ($scope.CM00700Listss[T].StatusActionTypeID == 1
                          && $scope.CM00700Listss[T].CaseStatusParent == null
                          ) {
                          $scope.CaseStatusestemp.push($scope.CM00700Listss[T]);
                      }
                  }
              }).finally(function () { });
        $http.get('/CMS_Trx_BatchRecovery/GetNextBatchID?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.UploadObject.BatchID = data;

        }).finally(function () { });
    };
    $scope.LoadLookUp();
    $scope.CM00055List = [];
    $scope.CaseProductList = function (TRXTypeID) {
        if ($scope.CM00055List.length > 0) return;
        $http.get('/CMS_CaseType/GetProductsByCaseType?TRXTypeID=' + TRXTypeID + '&KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.CM00055List = data;
            //$scope.CM00055ListOptions = {
            //    filter: "startswith",
            //    optionLabel: "-- Select Type --",
            //    dataTextField: "ProductName",
            //    dataValueField: "ProductID",
            //    dataSource: $scope.CM00055List
            //};
        }).finally(function () { });
    };
    $scope.ExportExcel = function () {
        if ($scope.CurrentStep >= 0 && $scope.CurrentStep <= 6)
            $scope.SaveAsExcel('GridCaseMissingDebtorData');
        else if ($scope.CurrentStep == 7)
            $scope.SaveAsExcel('GridValidateCaseDuplicateForUploading');
        else if ($scope.CurrentStep >= 17 && $scope.CurrentStep <= 19)
            $scope.SaveAsExcel('CM_CaseUploadMissingData');
        else if ($scope.CurrentStep == 21)
            $scope.SaveAsExcel('GridCM_CaseOldForUploading');
        else if ($scope.CurrentStep == 22)
            $scope.SaveAsExcel('GridCM_CaseDuplicateForUploading');
        else if ($scope.CurrentStep == 23)
            $scope.SaveAsExcel('GridCM_CaseNewForUploading');
    };
    $scope.TransactionTypeChanged = function () {
        $scope.CM00055List = [];
        $scope.TRXTypes.forEach(function (element, index) {
            //alert($scope.UploadObject.TRXTypeID);
            if (element.TRXTypeID === $scope.UploadObject.TRXTypeID) {
                $scope.UploadObject.TrxTypeName = element.TrxTypeName;
                if (element.CurrencyCode == '' || element.CurrencyCode == null || angular.isUndefined(element.CurrencyCode)) {
                    $scope.UploadObject.DecimalDigit = $scope.MY.DecimalDigit;
                    $scope.UploadObject.CurrencyCode = $scope.MY.CurrencyCode;
                    $scope.UploadObject.ExchangeTableID = $scope.MY.ExchangeTableID;
                    $scope.UploadObject.IsMultiply = $scope.MY.IsMultiply;
                    $scope.UploadObject.ExchangeRateID = $scope.MY.ExchangeRateID;
                    $scope.UploadObject.ExchangeRate = $scope.MY.ExchangeRate;
                } else {
                    $scope.UploadObject.DecimalDigit = element.DecimalDigit;
                    $scope.UploadObject.CurrencyCode = element.CurrencyCode;
                    $scope.UploadObject.IsMultiply = element.IsMultiply;
                    $scope.UploadObject.ExchangeRate = element.ExchangeRate;
                }
                return;
            }
        });
    };

    // -2
    $scope.CurrentStep = -3;
    var TableName;
    $scope.ExcelColumns = [];

    $scope.OpenMapFieldsWindow = function () {
        $scope.MapFieldstWindow.center().toFront().open();
    };

    $scope.NationalityBack = function (nationality) {
        if (nationality != null) {
            var index = $scope.GetSingleIndex($scope.KaizenColumn, "FieldName", "NationalityID");
            $scope.KaizenColumn[index].FixedValue = nationality.NationalityID;
        }
    };
    $scope.ClientBack = function (client) {
        if (client != null) {
            switch ($scope.CurrentControl) {
                case 'Main':
                    $scope.CM00110 = client;
                    $scope.link = '/Photo/ClientTemp/' + kaizenTrim($scope.CM00110.PhotoExtension);
                    $scope.UploadObject.ClientID = client.ClientID;
                    $scope.UploadObject.ClientName = client.ClientName;
                    break;
                case 'ClientPivotGrid':
                    $scope.ClientdataSource.add(client);
                    $scope.selectedClients.push(client.ClientID);
                    break;
                case 'UploadView':
                    $scope.UploadObject.ClientID = client.ClientID;
                    $scope.UploadObject.ClientName = client.ClientName;
                    break;
            }
        }
    };
    $scope.ContractBack = function (contract) {
        if (contract != null) {
            switch ($scope.CurrentControl) {
                case 'Main':
                    $scope.UploadObject.ContractID = contract.ContractID;
                    $scope.UploadObject.ContractName = contract.ContractName;
                    $scope.UploadObject.CurrencyCode = contract.CurrencyCode;
                    if (angular.isDefined($scope.UploadObject.CurrencyCode) && $scope.UploadObject.CurrencyCode != "") {
                        $http.get('/GL_Currency/GetSingle?KaizenPublicKey=' + sessionStorage.PublicKey, { params: { CurrencyCode: $scope.UploadObject.CurrencyCode } }).success(function (data) {
                            $scope.GL00102 = data;
                            $scope.UploadObject.DecimalDigit = data.DecimalDigit;
                        }).finally(function () { });
                    }
                    break;
                case 'UploadView':
                    $scope.UploadObject.ContractID = contract.ContractID;
                    $scope.UploadObject.ContractName = contract.ContractName;
                    break;
            }
        }
    };
    $scope.SaveExchangeRate = function () {
        $scope.GL00012.ExchangeTableID = $scope.CM00207.ExchangeTableID;
        $scope.GL00012.CurrencyCode = $scope.CM00207.CurrencyCode;

        $http.post('/GL_ExchangeRate/SaveData', { 'GL00012': $scope.GL00012, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
            if (data.Status == true) {
                Notify(data.Massage, 'bottom-right', '5000', 'success', 'fa-check', true);
                $scope.ClearExchangeRate();
                $scope.GridRefresh('GridGL_ExchangeRatePopUp');
            }
            else {
                Notify(data.Massage, 'bottom-right', '5000', 'danger', 'fa-bolt', true);
            }
        }).error(function (data, status, headers, config) { alert(); });
    };
    $scope.ClearExchangeRate = function () {
        $scope.GL00012 = {};
    };
    $scope.OpenExchangeRateWindow = function (str) {
        $scope.GL00012 = {};
        if (str == "Main") {
            $scope.ParmObject = $scope.CM00207;
        }
        else if (str == "UploadView") {
            $scope.ParmObject = $scope.UploadObject;
        }
        $scope.ExchangeRateWindow.center().toFront().open();
        $scope.ExchangeRateWindow.refresh({
            url: "/GL_ExchangeRate/PopUpRates?KaizenPublicKey=" + sessionStorage.PublicKey
        });
    };

    $scope.CurrencyBack = function (currency) {
        if (currency != null) {
            $scope.UploadObject.DecimalDigit = currency.DecimalDigit;
            $scope.UploadObject.CurrencyCode = currency.CurrencyCode;
            $scope.UploadObject.ExchangeTableID = currency.ExchangeTableID;
            $scope.UploadObject.IsMultiply = currency.IsMultiply;
            $scope.UploadObject.ExchangeRateID = currency.ExchangeRateID;
            $scope.UploadObject.ExchangeRate = currency.ExchangeRate;
            var kendoWindow = $("#MainDialog").data("kendoWindow");
            kendoWindow.close();
        }
    };
    $scope.ExchangeRateBack = function (rate) {
        if (rate != null) {
            switch ($scope.CurrentControl) {
                case 'Main':
                    $scope.CM00207.ExchangeRateID = rate.ExchangeRateID;
                    $scope.CM00207.ExchangeRate = rate.ExchangeRate;
                    break;
                case 'UploadView':
                    $scope.UploadObject.ExchangeRateID = rate.ExchangeRateID;
                    $scope.UploadObject.ExchangeRate = rate.ExchangeRate;
                    break;
            }
        }
    };
    $scope.ExchangeTableBack = function (table) {
        if (table != null) {
            switch ($scope.CurrentControl) {
                case 'Main':
                    $scope.CM00207.ExchangeTableID = table.ExchangeTableID;
                    $scope.CM00207.IsMultiply = table.IsMultiply;
                    var kendoWindow = $("#MainDialog").data("kendoWindow");
                    kendoWindow.close();
                    break;
                case 'UploadView':
                    $scope.UploadObject.ExchangeTableID = table.ExchangeTableID;
                    $scope.UploadObject.IsMultiply = table.IsMultiply;
                    var kendoWindow = $("#MainDialog").data("kendoWindow");
                    kendoWindow.close();
                    break;
            }
        }
    };
    //$scope.AgentBack = function (agent) {

    //};
    $scope.HasChild = false;
    $scope.UploadCaseStatusChanged = function () {
        //alert($scope.UploadObject.SelectedStatusID);
        //alert($scope.UploadObject.SelectedStatus);

        $scope.UploadObject.SelectedStatus = $scope.functiontofindObjectByKeyValue($scope.CM00700List, "CaseStatusID", $scope.UploadObject.SelectedStatusID);
        $scope.UploadObject.CaseStatusID = $scope.UploadObject.SelectedStatus.CaseStatusID;
        $scope.UploadObject.CaseStatusname = $scope.UploadObject.SelectedStatus.CaseStatusname;
        if ($scope.UploadObject.SelectedStatus.IsHasChild) {
            $http.get('/CMS_CaseStatus/GetStatusChilds?KaizenPublicKey=' + sessionStorage.PublicKey, { params: { CaseStatusParent: $scope.UploadObject.SelectedStatus.CaseStatusID } }).success(function (data) {
                if (data.length > 0) {
                    $scope.StatusChilds = data;
                    $scope.HasChild = true;
                }
                else {
                    $scope.StatusChilds = [];
                    $scope.HasChild = false;
                }
            }).finally(function () {
                $scope.CaseUploadStatusChildOptions = {
                    filter: "startswith",
                    model: "SelectedChild",
                    optionLabel: "-- Select Status --",
                    dataTextField: "CaseStatusname",
                    dataValueField: "CaseStatusID",
                    dataSource: $scope.StatusChilds,
                    value: $scope.UploadObject.CaseStatusID
                };
            });
        } else {
            $scope.HasChild = false;
        }
        if ($scope.UploadObject.SelectedStatus.IsScripting) {
            $http.get('/CMS_CaseStatus/GetAllScriptsByCaseStatusID?KaizenPublicKey=' + sessionStorage.PublicKey, { params: { CaseStatusID: $scope.UploadObject.SelectedStatus.CaseStatusID } }).success(function (data) {
                if (data.length > 0) {
                    $scope.StatusScripts = data;
                }
                else {
                    $scope.StatusScripts = [];
                }
            }).finally(function () {
                $scope.CaseUploadStatusScriptOptions = {
                    filter: "startswith",
                    model: "SelectedScript",
                    optionLabel: "-- Select Script --",
                    dataTextField: "StatusScriptName",
                    dataValueField: "StatusScriptID",
                    dataSource: $scope.StatusScripts,
                    value: $scope.UploadObject.StatusScriptID
                };
            });
        }

    };
    $scope.UploadChildStatusChanged = function () {
        if (angular.isDefined($scope.UploadObject.SelectedChild)) {
            $scope.UploadObject.CaseStatusChild = $scope.UploadObject.SelectedChild.CaseStatusID;
        }
    };
    $scope.ScriptUploadStatusChanged = function () {
        if (angular.isDefined($scope.UploadObject.SelectedScript)) {
            $scope.UploadObject.CaseStatusComment = $scope.UploadObject.SelectedScript.StatusScriptMain;
        }
    };
   
    //--------------------
    $scope.UploadedData = function (Columns) {
        TableName = Columns.TableName;
        $scope.ExcelColumns = Columns.ExcelColumns;
        $scope.ExcelColumns.push({ Index: "NULL", ColumnName: "N/A" });
        $scope.ExcelColumns.push({ Index: "Fixed", ColumnName: "Fixed" });
        $scope.KaizenColumn = [
          { FieldName: "DebtorID", FieldDisplay: "CPR ID", IsRequired: true, FieldValue: "NULL" },
          { FieldName: "PassportNumber", FieldDisplay: "Passport Number", FieldValue: "NULL" },
          //{ FieldName: "CPRCRNo", FieldDisplay: "CPR", FieldValue: "NULL" },
          { FieldName: "EnglishFullName", IsRequired: true, FieldDisplay: "Full Name", FieldValue: "NULL" },
          { FieldName: "FirstNameEnglish", FieldDisplay: "FirstNameEnglish", FieldValue: "NULL" },
          { FieldName: "FirstNameArabic", FieldDisplay: "FirstNameArabic", FieldValue: "NULL" },
          { FieldName: "NationalityID", FieldDisplay: "Nationality", FieldValue: "NULL", FieldTypeID: 11 },
          { FieldName: "BirthDate", FieldDisplay: "Birth Date", FieldValue: "NULL", FieldTypeID: 2 },
          { FieldName: "PLACE_OF_BIRTH", FieldDisplay: "PLACE OF BIRTH", FieldValue: "NULL" },
          { FieldName: "OccupationEnglish", FieldDisplay: "Occupation", FieldValue: "NULL" },
          { FieldName: "OccupationDescription1", FieldDisplay: "OccupationDescription1", FieldValue: "NULL" },
          { FieldName: "GenderID", FieldDisplay: "Gender", FieldValue: "NULL", FieldTypeID: 16 },
          { FieldName: "EmployerName1", FieldDisplay: "Employer Name", FieldValue: "NULL", FieldTypeID: 16 },

          { FieldName: "CountryID", FieldDisplay: "Country", FieldValue: "NULL" },
          { FieldName: "CityID", FieldDisplay: "City", FieldValue: "NULL" },
          { FieldName: "FlatNo", FieldDisplay: "Flat No", FieldValue: "NULL" },
          { FieldName: "BuildingNo", FieldDisplay: "Building No", FieldValue: "NULL" },
          { FieldName: "RoadName", FieldDisplay: "Road Name", FieldValue: "NULL" },
          { FieldName: "RoadNo", FieldDisplay: "Road No", FieldValue: "NULL" },
          { FieldName: "BlockNo", FieldDisplay: "Block No", FieldValue: "NULL" },
          { FieldName: "BlockName", FieldDisplay: "Block Name", FieldValue: "NULL" },
          { FieldName: "AddressEnglish", FieldDisplay: "Address English", FieldValue: "NULL" },
          {FieldName: "AddressArabic", FieldDisplay: "AddressArabic", FieldValue: "NULL"},
          { FieldName: "Address1", FieldDisplay: "Address1", FieldValue: "NULL" },
          { FieldName: "Address2", FieldDisplay: "Address2", FieldValue: "NULL" },
          { FieldName: "Address3", FieldDisplay: "Address3", FieldValue: "NULL" },
          { FieldName: "Address4", FieldDisplay: "Address4", FieldValue: "NULL" },
          { FieldName: "Phone01", FieldDisplay: "Phone01", FieldValue: "NULL" },
          { FieldName: "Phone02", FieldDisplay: "Phone02", FieldValue: "NULL" },
          { FieldName: "Phone03", FieldDisplay: "Office Phone", FieldValue: "NULL" },
          { FieldName: "Phone04", FieldDisplay: "Fax", FieldValue: "NULL" },
          { FieldName: "MobileNo1", FieldDisplay: "MobileNo1", FieldValue: "NULL" },
          { FieldName: "MobileNo2", FieldDisplay: "MobileNo2", FieldValue: "NULL" },
          { FieldName: "MobileNo3", FieldDisplay: "MobileNo3", FieldValue: "NULL" },
          { FieldName: "MobileNo4", FieldDisplay: "MobileNo4", FieldValue: "NULL" },
          { FieldName: "Email01", FieldDisplay: "Email01", FieldValue: "NULL" },
          { FieldName: "Email02", FieldDisplay: "Email02", FieldValue: "NULL" },
          { FieldName: "Email03", FieldDisplay: "Email03", FieldValue: "NULL" },
          { FieldName: "Email04", FieldDisplay: "Email04", FieldValue: "NULL" },
          { FieldName: "WebSite", FieldDisplay: "WebSite", FieldValue: "NULL" },
          { FieldName: "POBox", FieldDisplay: "POBox", FieldValue: "NULL" },
          { FieldName: "Other01", FieldDisplay: "Other01", FieldValue: "NULL" },
          { FieldName: "Other02", FieldDisplay: "Other02", FieldValue: "NULL" },
          { FieldName: "Other03", FieldDisplay: "Other03", FieldValue: "NULL" },
          { FieldName: "Other04", FieldDisplay: "Other04", FieldValue: "NULL" },

          { FieldName: "CUSTCLAS", FieldDisplay: "Class", FieldValue: "NULL" },
          { FieldName: "GroupID", FieldDisplay: "Group", FieldValue: "NULL" },
          { FieldName: "PriorityID", FieldDisplay: "Priority", FieldValue: "NULL" },
          { FieldName: "DebtorStatusID", FieldDisplay: "Status", FieldValue: "NULL" },
          { FieldName: "MaritalID", FieldDisplay: "MaritalID", FieldValue: "NULL" },
          { FieldName: "ResidenceNo", FieldDisplay: "Residence No", FieldValue: "NULL" },
          { FieldName: "MonthlySalary", FieldDisplay: "Monthly Salary", FieldValue: "NULL" },
          { FieldName: "CPRIssueDate", FieldDisplay: "CPR Issue Date", FieldValue: "NULL", FieldTypeID: 2 },
          { FieldName: "CPRExpiryDate", FieldDisplay: "CPR Expiry Date", FieldValue: "NULL", FieldTypeID: 2 },
          { FieldName: "PassportIssueDate", FieldDisplay: "Passport Issue Date", FieldValue: "NULL", FieldTypeID:2 },
          { FieldName: "PassportExpiryDate", FieldDisplay: "Passport Expiry Date", FieldValue: "NULL", FieldTypeID:2 },
          { FieldName: "VisaNo", FieldDisplay: "Visa No", FieldValue: "NULL" },
          { FieldName: "VisaExpiryDate", FieldDisplay: "Visa Expiry Date", FieldValue: "NULL", FieldTypeID: 2 }

        ];
    };
    $scope.CheckStep = function () {
        //alert($scope.CurrentStep);
        if ($scope.CurrentStep == -3) {
            $scope.UploadObject.AddressCode = $scope.SelectedLookUp.AddressTypes.AddressCode;
            $http.post('/CMS_TRX_CaseUploading/UploadBach', {
                'UploadedDataModel': $scope.UploadObject, 'FileName': TableName,
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
                    $scope.CurrentStep = -2
                    return;
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
            }).error(function (data, status, headers, config) { });
            return;
        }
        if ($scope.CurrentStep == -2) {
            $http.post('/CMS_TRX_CaseUploading/UploadDebtorData', {
                'TableName': TableName,
                'KaizenColumn': $scope.KaizenColumn,
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
                    $scope.CurrentStep = -1;
                    $scope.GONext();
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
        if ($scope.CurrentStep >= 0 && $scope.CurrentStep <= 8) {
            $http.get('/CMS_TRX_CaseUploading/ValidateMissingDebtorIDData?KaizenPublicKey='
            + sessionStorage.PublicKey + "&CurrentStep=" + $scope.CurrentStep).success(function (dataNewCase) {
                $scope.ConditionVariable = dataNewCase;
                if (dataNewCase != "OK") {
                    $scope.GridRefresh("GridCaseMissingDebtorData");
                }
                else {
                    $scope.GONext();
                }
            });
        }
        if ($scope.CurrentStep == 9) {
            $scope.PostDebtorData();
        }
        if ($scope.CurrentStep == 10) {
            $http.get('/CMS_TRX_CaseUploading/GetCaseFields?TRXTypeID=' + $scope.UploadObject.TRXTypeID + '&KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
                $scope.CaseKaizenColumn = data;
                $scope.CurrentStep = 11;
            }).finally(function () {  });
        }
        if ($scope.CurrentStep == 11) {
            $http.post('/CMS_TRX_CaseUploading/UploadCaseData', {
                'KaizenColumn': $scope.CaseKaizenColumn,
                'TableName': TableName,
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
                    $scope.GONext();
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
        if ($scope.CurrentStep >= 12 && $scope.CurrentStep <= 16) {
            $http.get('/CMS_TRX_CaseUploading/ValidateMissingDate?KaizenPublicKey='
             + sessionStorage.PublicKey + "&CurrentStep=" + $scope.CurrentStep).success(function (dataNewCase) {
                 $scope.ConditionVariable = dataNewCase;
                 if (dataNewCase != "OK") {
                     $scope.GridRefresh("CM_CaseUploadMissingData");
                 }
                 else {
                     $scope.GONext();
                 }
             });
        }
        if ($scope.CurrentStep === 17) {
            $http.get('/CMS_TRX_CaseUploading/ValidateCaseDuplicateForUploading?KaizenPublicKey='
                 + sessionStorage.PublicKey).success(function (dataNewCase) {
                     $scope.ConditionVariable = dataNewCase;
                     if (dataNewCase != "OK") {
                         $scope.GridRefresh("GridValidateCaseDuplicateForUploading");
                     }
                     else {
                         $scope.GONext();
                     }
                 });
        }
        if ($scope.CurrentStep === 18) {
            $http.get('/CMS_TRX_CaseUploading/ValidateCaseOld?KaizenPublicKey='
           + sessionStorage.PublicKey).success(function (dataNewCase) {
               $scope.ConditionVariable = dataNewCase;
               if (dataNewCase != "OK") {
                   $scope.GridRefresh("GridCM_CaseOldForUploading");
               }
               else {
                   $scope.GONext();
               }
           });
        }
        if ($scope.CurrentStep === 19) {
            $http.get('/CMS_TRX_CaseUploading/ValidateRepeatedCases?KaizenPublicKey='
            + sessionStorage.PublicKey).success(function (dataNewCase) {
                $scope.ConditionVariable = dataNewCase;
                if (dataNewCase != "OK") {
                    $scope.GridRefresh("GridCM_CaseDuplicateForUploading");
                }
                else {
                    $scope.GONext();
                }
            });
        }
        if ($scope.CurrentStep === 20) {
            $http.get('/CMS_TRX_CaseUploading/ValidateCaseNew?KaizenPublicKey='
             + sessionStorage.PublicKey).success(function (dataNewCase) {
                 $scope.ConditionVariable = dataNewCase;
                 if (dataNewCase != "OK") {
                     $scope.GridRefresh("GridCM_CaseNewForUploading");
                 }
                 else {
                     alert('there Is no New cases');
                 }
             });
        }
        if ($scope.CurrentStep === 21) {
            $http.get('/CMS_TRX_CaseUploading/PostUploadCases?KaizenPublicKey='
              + sessionStorage.PublicKey).success(function (data) {
                  if (data.Status == true) {
                      $.smallBox({
                          title: data.Massage,
                          content: data.Description,
                          color: "#739E73",
                          iconSmall: "fa fa-times fa-2x fadeInRight animated",
                          timeout: 1000
                      });
                      $scope.GONext();
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
        if ($scope.CurrentStep === 22) {
            $http.get('/CMS_Case/CaseReconcile?ClientID=' + $scope.UploadObject.ClientID + '&KaizenPublicKey='
           + sessionStorage.PublicKey).success(function (data) {
               //alert(reultData.Status);
               if (data.Status == true) {
                   $.smallBox({
                       title: data.Massage,
                       content: data.Description,
                       color: "#739E73",
                       iconSmall: "fa fa-times fa-2x fadeInRight animated",
                       timeout: 1000
                   });
                   $scope.GONext();
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
        if ($scope.CurrentStep === 23) {
            $.bigBox({
                title: "Successfully Updated",
                content: "Data is Up to date ..",
                color: "#739E73",
                timeout: 4000,
                icon: "fa fa-check shake animated"
            });

        }
    };
    $scope.PostDebtorData = function () {
        $http.get('/CMS_TRX_CaseUploading/PostDebtorData?KaizenPublicKey='
                + sessionStorage.PublicKey).success(function (resultData) {
                    if (resultData.Status == true) {
                        $.smallBox({
                            title: resultData.Massage,
                            content: resultData.Description,
                            color: "#739E73",
                            iconSmall: "fa fa-times fa-2x fadeInRight animated",
                            timeout: 1000
                        });
                        //$scope.StatusActionOptions = {
                        //    filter: "startswith",
                        //    model: "SelectedStatus",
                        //    optionLabel: "-- Select Status --",
                        //    dataTextField: "CaseStatusname",
                        //    dataValueField: "CaseStatusID",
                        //    dataSource: $scope.CaseStatuses,
                        //    value: $scope.StatusAction.CaseStatusID,
                        //    popup: {
                        //        appendTo: $("#ChangeStatusModal")
                        //    }
                        //};
                        $scope.GONext();
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
    };
    $scope.GONext = function () {
        if ($scope.CurrentStep == 18)
            $scope.AdvancedActionPage = null;
        $scope.CurrentStep = parseFloat($scope.CurrentStep) + 1;
        //alert('GONext' + $scope.CurrentStep);
      
        //if ($scope.CurrentStep == 25)
        //    $scope.CurrentStep = 26;
        //if ($scope.CurrentStep >= 27 && $scope.CurrentStep <= 30)
        //    $scope.CurrentStep = 31;
        $scope.CheckStep();
    };
    $scope.GOBack = function () {
        if (parseFloat($scope.CurrentStep) == -2)
            $scope.CurrentStep = -3;
        else if (parseFloat($scope.CurrentStep) <= 11)
            $scope.CurrentStep = -2;
        else if (parseFloat($scope.CurrentStep) >= 10 && parseFloat($scope.CurrentStep) <= 20) {
            $scope.CurrentStep = 11;
        } else if ($scope.CurrentStep == 18)
            $scope.AdvancedActionPage = null;
        else {
            $scope.CurrentStep = parseFloat($scope.CurrentStep) - 1;
            $scope.CheckStep();
        }
    };
    $scope.PostUploadCases = function () {
      
    };
    $scope.UserBack = function (agent) {
        if (agent != null) {
            var index = $scope.GetSingleIndex($scope.CaseKaizenColumn, "FieldName", "AgentID");
            $scope.CaseKaizenColumn[index].FixedValue = agent.UserName;
        }
    };
    $scope.SourceChanged = function (column) {
        //alert(column.FieldTypeID);
        //alert(column.FieldValue);
        if (column.FieldValue != 'Fixed') return;
        if (column.FieldName == 'NationalityID') {
            column.html = "<div class='input-group'><input type='text' class='form-control' readonly " +
                "placeholder='Nationality ID' ng-model='column.FixedValue' />" +
                "<span class='input-group-btn input-group-sm'>" +
                "<button class='btn btn-default' ng-click=\"OpenkendoWindow('Nationality')\">" +
                "<span class='glyphicon glyphicon-search'></span></button></span></div>";
        }
        else if (column.FieldName == 'AgentID') {
            column.html = "<div class='input-group'><input type='text' class='form-control' readonly " +
                "placeholder='AgentID' ng-model='column.FixedValue' />" +
                "<span class='input-group-btn input-group-sm'>" +
                "<button class='btn btn-default' ng-click=\"OpenkendoWindow('KaizenUser')\">" +
                "<span class='glyphicon glyphicon-search'></span></button></span></div>";
        }
        else if (column.FieldName == 'CUSTCLAS') {
            column.html = "<select class='form-control input-sm no-padding-right no-padding-left' " +
                "ng-model='column.FixedValue'><option ng-repeat='class in debtorClasses' value='{{class.CUSTCLAS}}'>{{class.CUSTCLASNAME}}</option></select>";
        }
        else if (column.FieldName == 'GroupID') {
            column.html = "<select class='form-control input-sm no-padding-right no-padding-left' " +
                "ng-model='column.FixedValue'><option ng-repeat='group in debtorGroups' value='{{group.GroupID}}'>{{group.GroupName}}</option></select>";
        }
        else if (column.FieldName == 'PriorityID') {
            column.html = "<select class='form-control input-sm no-padding-right no-padding-left' " +
                "ng-model='column.FixedValue'><option ng-repeat='priority in debtorPriorities' value='{{priority.PriorityID}}'>{{priority.PriorityName}}</option></select>";
        }
        else if (column.FieldName == 'DebtorStatusID') {
            column.html = "<select class='form-control input-sm no-padding-right no-padding-left' " +
                "ng-model='column.FixedValue'><option ng-repeat='status in debtorStatuses' value='{{status.DebtorStatusID}}'>{{status.DebtorStatusName}}</option></select>";
        }
        else if (column.FieldName == 'AssignDate' || column.FieldName == 'FirstDisburementDate'
            || column.FieldName == 'MATURITY_DATE' || column.FieldName == 'TransactionDate'
            || column.FieldName == 'OverDueDate' || column.FieldName == 'LastPaymentDateUplod'
            || column.FieldName == 'FirstLifeOverDueDate' || column.FieldName == 'LastPaymentDate'
            || column.FieldName == 'Date01' || column.FieldName == 'Date02'
            || column.FieldName == 'Date03' || column.FieldName == 'Date04') {
            column.FixedValue = new Date();
            column.html = "<input kendo-date-picker k-parse-formats=\"['yyyy-MM-ddTHH:mm:ss']\" k-format='dd/MM/yyyy' k-ng-model='column.FixedValue' style='width: 100%;' />";
        }
        else if (column.FieldName == 'PrincipleAmount' || column.FieldName == 'MaturityAmount'
            || column.FieldName == 'OutstandingAMT' || column.FieldName == 'OutStandingToday'
            || column.FieldName == 'InstallmentAmount' || column.FieldName == 'ClaimAmount'
            || column.FieldName == 'FinanceCharge' || column.FieldName == 'WriteOffAMT'
            || column.FieldName == 'CreditLimit' || column.FieldName == 'LastPaymentAMTUpload'
            || column.FieldName == 'TotalLifeCollactedAMT' || column.FieldName == 'AMT01'
            || column.FieldName == 'AMT02' || column.FieldName == 'AMT03'
            || column.FieldName == 'AMT04' || column.FieldName == 'AMT05'
            || column.FieldName == 'AMT06' || column.FieldName == 'AMT07'
            || column.FieldName == 'AMT08' || column.FieldName == 'AMT09' || column.FieldName == 'AMT10') {
            column.html = "<input kendo-numeric-text-box class=\"currency\" k-options=\"NumberFormatOptions(UploadObject.CurrencyCode,UploadObject.DecimalDigit,false)\" k-ng-model=\"column.FixedValue\" style=\"width:100%\" />";
        }
        else if (column.FieldName == 'ProductID') {
            $scope.CaseProductList($scope.UploadObject.TRXTypeID);
            column.html = "<select class='form-control input-sm no-padding-right no-padding-left' " +
              "ng-model='column.FixedValue'><option ng-repeat='Addressty in CM00055List' value='{{Addressty.ProductID}}'>{{Addressty.ProductName}}</option></select>";
        }
        else if (column.FieldName == 'CountryID') {
            $scope.GetCountries();
            column.html = "<select class='form-control input-sm no-padding-right no-padding-left' " +
              "ng-model='column.FixedValue'><option ng-repeat='Addressty in Sys00013List' value='{{Addressty.CountryID}}'>{{Addressty.CountryName}}</option></select>";
        }
        else if (column.FieldName == 'CityID') {
            $scope.GetCity();
            column.html = "<select class='form-control input-sm no-padding-right no-padding-left' " +
              "ng-model='column.FixedValue'><option ng-repeat='Addressty in Sys00014List' value='{{Addressty.CityID}}'>{{Addressty.CityName}}</option></select>";
        }
        else if (column.FieldTypeID == 2) {
            column.FixedValue = new Date();
            column.html = "<input kendo-date-picker k-parse-formats=\"['yyyy-MM-ddTHH:mm:ss']\" k-format='dd/MM/yyyy' k-ng-model='column.FixedValue' style='width: 100%;' />";
        }
        else if (column.FieldTypeID == 16) {
            column.html = "<select class='form-control input-sm no-padding-right no-padding-left' " +
                "ng-model='column.FixedValue'><option value='1'>Male</option><option value='2'>Female</option><option value='3'>N/A</option></select>";
        } else {
            column.FieldValue = "NULL";
            kendo.alert("Incorrect Selection", "warning");
        }

    };


    $scope.OverrideAll = function () {
        $http.post('/CMS_TRX_CaseUploading/OverrideAll', {
            'CurrentStep': $scope.CurrentStep,
            'KaizenPublicKey': sessionStorage.PublicKey
        }).success(function (data) {
            alert(data);
            if (data.Status == true) {
                $.smallBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#739E73",
                    iconSmall: "fa fa-times fa-2x fadeInRight animated",
                    timeout: 1000
                });
                $scope.GONext();
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
        $http.post('/CMS_TRX_CaseUploading/DynamicOverride', {
            'UploadedOBJECT': row, 'CurrentStep': $scope.CurrentStep,
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
                if ($scope.CurrentStep >= 0 && $scope.CurrentStep <= 12) {
                    $scope.GridRefresh("GridCM_UploadValidate00");
                }
                $scope.CheckStep();
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
    $scope.DynamicCancel = function (row) {
        $http.post('/CMS_TRX_CaseUploading/DynamicCancel', {
            'UploadedOBJECT': row, 'CurrentStep': $scope.CurrentStep,
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
                $scope.CheckStep();
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

    $scope.OverrideAllDuplicatedCases = function () {
        $http.post('/CMS_TRX_CaseUploading/OverrideAllDuplicatedCases', {
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
                $scope.CheckStep();
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


    $scope.LoadCM00100 = function (DebtorID) {
        if (angular.isUndefined($scope.CM00100.DebtorID)) {
            $http.get('/CMS_Debtor/GetSingle?KaizenPublicKey=' + sessionStorage.PublicKey + "&DebtorID=" + DebtorID).success(function (data) {
                $scope.CM00100 = data;
            }).finally(function () {
                $scope.CM00100.Status = 2;
                if ($scope.CM00100.PhotoExtension == null)
                    $scope.link = '/Photo/DebtorPhoto/DebtorID.jpg';
                else
                    $scope.link = '/Photo/DebtorPhoto/' + kaizenTrim($scope.CM00100.DebtorID) + "." + kaizenTrim($scope.CM00100.PhotoExtension) + "?c=" + Math.random();
                $scope.CM00104 = { DebtorID: $scope.CM00100.DebtorID, Status: 1 };
                $scope.GridRefresh('GridCMS_DebtorAddressByDebtor');
            });
        }
    };
    $scope.DynamicView = function (row) {
        $scope.CM00100 = {};
        $scope.LoadLookUp(row.DebtorID, $scope.LoadCM00100);
        $scope.DetailsWindow.center().toFront().open();
        $scope.DetailsWindow.refresh({
            url: "/CMS_TRX_CaseUploading/DebtorDetails?KaizenPublicKey=" + sessionStorage.PublicKey
        });
    };
    $scope.LoadLookUp = function (DebtorID, callback) {
        $http.get('/CMS_DebtorClass/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.debtorClasses = data;
        }).finally(function () { });
        $http.get('/CMS_DebtorStatus/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.debtorStatuses = data;
        }).finally(function () { });
        $http.get('/CMS_Group/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.debtorGroups = data;
        }).finally(function () { });
        $http.get('/Set_Priority/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.debtorPriorities = data;
        }).finally(function () { });
        $http.get('/Adm_Country/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.Countries = data;
        }).finally(function () { });
        $http.get('/Adm_City/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.Cities = data;
        }).finally(function () { });
        $http.get('/CMS_Debtor/DebtorAddressTypeFillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey + '&ContactSourceID=1').success(function (data) {
            $scope.DebtorAddressTypes = data;
        }).finally(function () { });
        callback(DebtorID);
    };
    $scope.AddressTypeChanged = function () {
        var grid = $("#GridCMS_DebtorAddressByDebtor").data("kendoGrid");
        var dataSourceData = [];
        angular.copy(grid.dataSource.data(), dataSourceData);
        var found = $scope.functiontofindObjectByKeyValue(dataSourceData, "AddressCode", $scope.CM00104.AddressCode);
        if (found != null) {
            $scope.CM00104 = found;
            $scope.CM00104.Status = 2;
            return;
        }
        else {
            $scope.CM00104 = { Status: 1, AddressCode: $scope.CM00104.AddressCode };
            return;
        }
    };

    $scope.StatusAction = {};
    $scope.StatusActionChanged = function () {
        if (angular.isDefined($scope.UploadObject.SelectedStatusID)) {
            $scope.StatusAction.CaseStatusID = $scope.UploadObject.SelectedStatusID;
            $scope.StatusAction.CaseStatusname = $scope.StatusAction.SelectedStatus.CaseStatusname;
            if ($scope.StatusAction.SelectedStatus.IsHasChild) {
                $http.get('/CMS_CaseStatus/GetStatusChilds?KaizenPublicKey=' + sessionStorage.PublicKey, { params: { CaseStatusParent: $scope.StatusAction.SelectedStatus.CaseStatusID } }).success(function (data) {
                    if (data.length > 0) {
                        $scope.StatusChilds = data;
                    }
                    else {
                        $scope.StatusChilds = [];
                    }
                }).finally(function () {
                    $scope.StatusActionChildOptions = {
                        filter: "startswith",
                        model: "SelectedChild",
                        optionLabel: "-- Select Status --",
                        dataTextField: "CaseStatusname",
                        dataValueField: "CaseStatusID",
                        dataSource: $scope.StatusChilds,
                        value: $scope.StatusAction.CaseStatusID,
                        popup: {
                            appendTo: $("#ChangeStatusModal")
                        }
                    };
                });
            }
            if ($scope.StatusAction.SelectedStatus.IsScripting) {
                $http.get('/CMS_CaseStatus/GetAllScriptsByCaseStatusID?KaizenPublicKey=' + sessionStorage.PublicKey, { params: { CaseStatusID: $scope.UploadObject.SelectedStatus.CaseStatusID } }).success(function (data) {
                    if (data.length > 0) {
                        $scope.StatusScripts = data;
                    }
                    else {
                        $scope.StatusScripts = [];
                    }
                }).finally(function () {
                    $scope.StatusActionScriptOptions = {
                        filter: "startswith",
                        model: "SelectedScript",
                        optionLabel: "-- Select Script --",
                        dataTextField: "StatusScriptName",
                        dataValueField: "StatusScriptID",
                        dataSource: $scope.StatusScripts,
                        value: $scope.StatusAction.StatusScriptID,
                        popup: {
                            appendTo: $("#ChangeStatusModal")
                        }
                    };
                });
            }
        }
    };
    $scope.ChildStatusActionChanged = function () {
        if (angular.isDefined($scope.StatusAction.SelectedChild)) {
            $scope.StatusAction.CaseStatusChild = $scope.StatusAction.SelectedChild.CaseStatusID;
            $scope.StatusAction.CaseStatusChildName = $scope.StatusAction.SelectedChild.CaseStatusname;
        }
    };
    $scope.ScriptStatusActionChanged = function () {
        if (angular.isDefined($scope.StatusAction.SelectedScript)) {
            $scope.StatusAction.CaseStatusComment = $scope.StatusAction.SelectedScript.StatusScriptMain;
        }
    };


    $scope.UpdateOption = {};
    $scope.UpdateOptionsAction = function () {
        $http.post('/CMS_TRX_CaseUploading/UpdateCaseOptions', {
            'CurrentStep': $scope.CurrentStep, 'UpdateOptionsDataModel': $scope.UpdateOption,
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
                $('#UpdateOptionsModal').modal('hide');
                $scope.GONext();
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
    $scope.AdvancedActionPage = null;
    $scope.OpenDoAction_Click = function () {
        $scope.DoActionWindow.center().toFront().open();
        $scope.DoActionWindow.refresh({
            url: "/CMS/CMS_TRX_CaseUploading/DoAction?KaizenPublicKey=" + sessionStorage.PublicKey
        });
    };
    $scope.CaseChecked = function (dataItem) {
        $http.post('/CMS_TRX_CaseUploading/CaseCheckedhistry', {
            'CaseRef': dataItem.CaseRef,
            'IsJoint': dataItem.IsJoint,
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
    };
    $scope.CaseCheckedAll = function (checked) {
        $http.post('/CMS_TRX_CaseUploading/CaseCheckedhistryAll', {
            'IsJoint': checked,
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
    };
    $scope.CaseArchive_Click = function () {
        $http.post('/CMS_TRX_CaseUploading/CaseArchive_Click', {
            'KaizenPublicKey': sessionStorage.PublicKey
        }).success(function (data) {
            if (data.Status == true) {
                //$('#ChangeStatusModal').modal('hide');
                $scope.DoActionWindow.close();
                $scope.GridRefresh('GridCMS_AdvancedAction');
                $.smallBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#739E73",
                    iconSmall: "fa fa-times fa-2x fadeInRight animated",
                    timeout: 1000
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
    };
    $scope.CaseDelete_Click = function () {
        $http.post('/CMS_TRX_CaseUploading/CaseDelete_Click', {
            'CM00203': $scope.SelectedCaseList,
            'ChangeStatusDataModel': $scope.StatusAction,
            'KaizenPublicKey': sessionStorage.PublicKey
        }).success(function (data) {
            if (data.Status == true) {
                //$('#ChangeStatusModal').modal('hide');
                $scope.DoActionWindow.close();
                $scope.GridRefresh('GridCMS_AdvancedAction');
                $.smallBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#739E73",
                    iconSmall: "fa fa-times fa-2x fadeInRight animated",
                    timeout: 1000
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
    };
    $scope.ChangeOldCasesStatus = function () {
        if ($scope.AdvancedActionPage != null) {
            $scope.DoAdvancedAction_click();
            //$scope.FilterBy();
            return;
        }
        alert('ChangeOldCasesStatus');
        $http.post('/CMS_TRX_CaseUploading/ChangeOldCasesStatus', {
            'ChangeStatusDataModel': $scope.StatusAction,
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
                $('#ChangeStatusModal').modal('hide');
                $scope.GONext();
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
    $scope.AdvancedAction_click = function () {
        $scope.AdvancedActionPage = "/CMS/CMS_TRX_CaseUploading/AdvancedAction?KaizenPublicKey=" + sessionStorage.PublicKey;
    };
    //$scope.Close_click = function () {
    //    $scope.FormClose($scope.ToolBar.ActiveScreen.ID);
    //};
    $scope.AdvancedAction = {};
    $scope.AdvancedActionDO = {};
    $scope.CM10701 = {};
    $scope.FilterBy = function () {
        $scope.GridRefresh('GridCMS_AdvancedAction');
    };
    //$scope.AdvancedActionGONext = function () {
    //    $scope.AdvancedActionPage = null;
    //    $scope.GridRefresh('GridCM_CaseOldForUploading');
    //};
    $scope.SelectedCaseList = [];
    $scope.DoAdvancedAction_click = function () {
        //alert('sssssssssssssssssssssss');
        $scope.SelectedCaseList = [];
        var items = $("#GridCMS_AdvancedAction").data("kendoGrid").dataSource.data();
        for (i = 0; i < items.length; i++) {
            var item = items[i];
            if (item.IsJoint) {
                $scope.SelectedCaseList.push(item.CaseRef);
            }
        }
        if ($scope.SelectedCaseList.length == 0) {
            alert('Case Selection is required');
            return;
        }
        //wael
        //alert(JSON.stringify($scope.SelectedCaseList, null, 2));
        $scope.CM10701.DecimalDigit = $scope.UploadObject.DecimalDigit;
        $scope.CM10701.CurrencyCode = $scope.UploadObject.CurrencyCode;
        $scope.CM10701.ExchangeTableID = $scope.UploadObject.ExchangeTableID;
        $scope.CM10701.IsMultiply = $scope.UploadObject.IsMultiply;
        $scope.CM10701.ExchangeRateID = $scope.UploadObject.ExchangeRateID;
        $scope.CM10701.ExchangeRate = $scope.UploadObject.ExchangeRate;
        $http.post('/CMS_TRX_CaseUploading/DoAdvancedAction_click', {
            'oCM10701': $scope.CM10701,
            'KaizenPublicKey': sessionStorage.PublicKey
        }).success(function (data) {
            if (data.Status == true) {
                //$('#ChangeStatusModal').modal('hide');
                $scope.DoActionWindow.close();
                $scope.GridRefresh('GridCMS_AdvancedAction');
                $.smallBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#739E73",
                    iconSmall: "fa fa-times fa-2x fadeInRight animated",
                    timeout: 1000
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
    };
    $scope.AdvancedActionStatusChanged = function () {
        $scope.StatusDynamicChilds = [];
        $scope.CM00060List = [];
        $scope.LookupList = [];
        $scope.StatusScripts = [];
        //$scope.CM10701.CaseStatusID = $scope.AdvancedActionDO.SelectedStatusID;
        $scope.AdvancedActionDO.SelectedStatus = $scope.functiontofindObjectByKeyValue($scope.CM00700List, "CaseStatusID", $scope.AdvancedActionDO.SelectedStatusID);
        //alert(JSON.stringify($scope.SelectedLookUp.SelectedStatus, null, 2))
        $scope.CM10701.TRXTypeID = $scope.UploadObject.TRXTypeID;
        //alert($scope.AdvancedActionDO.SelectedStatus.StatusActionTypeID);
        $scope.CM10701.StatusActionTypeID = $scope.AdvancedActionDO.SelectedStatus.StatusActionTypeID;
        $scope.CM10701.CaseStatusID = $scope.AdvancedActionDO.SelectedStatus.CaseStatusID;
        $scope.CM10701.CaseStatusname = $scope.AdvancedActionDO.SelectedStatus.CaseStatusname;
        //alert($scope.CM10701.CaseStatusID);
        $scope.GetDynamicSubStatus($scope.AdvancedActionDO.SelectedStatus.CaseStatusID);
        $scope.GetScripts($scope.AdvancedActionDO.SelectedStatus.CaseStatusID);
    };
    $scope.GetDynamicSubStatus = function (CaseStatusID) {
        for (var T = 0; T < $scope.StatusDynamicChilds.length; T++) {
            if (angular.isUndefined($scope.StatusDynamicChilds[T].CaseStatusChildID)) continue;
            if ($scope.StatusDynamicChilds[T].CaseStatusChildID == CaseStatusID) {
                T += 1;
                for (var i = T; i < $scope.StatusDynamicChilds.length; i++) {
                    $scope.StatusDynamicChilds.splice(T, 1);
                }
            }
        }
        for (var T = 0; T < $scope.CaseStatusestemp.length; T++) {
            if ($scope.CaseStatusestemp[T].CaseStatusID == CaseStatusID) {
                if ($scope.CaseStatusestemp[T].IsHasChild) {
                    $scope.temp = { CaseStatusID: CaseStatusID };
                    $scope.temp.StatusChilds = [];
                    for (var I = 0; I < $scope.CM00700List.length; I++) {
                        if ($scope.CM00700List[I].CaseStatusParent == CaseStatusID) {
                            var temm = $scope.CM00700List[I];
                            var tempStstus = {
                                CaseStatusID: temm.CaseStatusID
                                , IsHasChild: temm.IsHasChild
                                , CaseStatusname: temm.CaseStatusname
                            };
                            $scope.temp.StatusChilds.push(tempStstus);
                        }
                    }
                    $scope.StatusDynamicChilds.push($scope.temp);
                }
                return;
            }
        }
    };
    $scope.GetScripts = function (CaseStatusID) {
        for (var T = 0; T < $scope.CM00700List.length; T++) {
            var tt = $scope.CM00700List[T];
            if (tt.IsScripting) {
                if (tt.CaseStatusID == CaseStatusID) {
                    $http.get('/CMS_CaseStatus/GetAllScriptsByCaseStatusID?KaizenPublicKey=' + sessionStorage.PublicKey, {
                        params: {
                            CaseStatusID: CaseStatusID
                        }
                    }).success(function (data) {
                        if (data.length > 0) {
                            for (var I = 0; I < data.length; I++) {
                                var tempIsExited = false;
                                for (var T = 0; T < $scope.StatusScripts.length; T++) {
                                    if ($scope.StatusScripts[T].StatusScriptID == data[I].StatusScriptID) {
                                        tempIsExited = true;
                                    }
                                }
                                if (!tempIsExited) {
                                    $scope.temp = {
                                        StatusScriptID: data[I].StatusScriptID
                                        , StatusScriptName: data[I].StatusScriptName
                                        , StatusScriptMain: data[I].StatusScriptMain
                                    };
                                    $scope.StatusScripts.push($scope.temp);
                                }
                            }
                            return;
                        }
                    }).finally(function () { });
                }
            }
        }
    };
    $scope.CaseStatusChildChanged = function (CaseStatusID) {
        //$scope.GetDynamicSubStatus(CaseStatusID);
        $scope.GetScripts($scope.CM10701.CaseStatusChildID);
        //alert($scope.CM10701.CaseStatusChildID);
        //alert(CaseStatusID);
        for (var i = 0; i < $scope.CM00700List.length; i++) {
            // alert(arraytosearch[i][key])
            if ($scope.CM00700List[i].CaseStatusID == $scope.CM10701.CaseStatusChildID) {
                $scope.CM10701.CaseStatusChild = $scope.CM00700List[i].CaseStatusID
                $scope.CM10701.CaseStatusChildName = $scope.CM00700List[i].CaseStatusname
                break;
            }
        }
        //alert(JSON.stringify($scope.AdvancedActionDO.CaseStatusChildObj, null, 2));
        //$scope.CM10701.CaseStatusChild = CaseStatusID;
        //$scope.CM10701.CaseStatusChildName = $scope.AdvancedActionDO.CaseStatusChildObj.CaseStatusname;
        //alert($scope.CM10701.CaseStatusChildName);
    };
    $scope.ScriptStatusChanged = function () {
        if (angular.isDefined($scope.CM10701.StatusScriptID)) {
            for (var I = 0; I < $scope.StatusScripts.length; I++) {
                if ($scope.StatusScripts[I].StatusScriptID == $scope.CM10701.StatusScriptID) {
                    $scope.CM10701.CaseStatusComment = $scope.StatusScripts[I].StatusScriptMain;
                    return;
                }
            }
        }
    };
});
