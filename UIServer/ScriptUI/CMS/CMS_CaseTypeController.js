app.controller('CMS_CaseTypeController', function ($scope, $http) {
    $scope.CM00029 = {};
    $scope.PagesCM00029 = [];
    $scope.SelectedLookUp = {};

    $scope.toolbarOptions = {
        items: [
                 {
                     type: "button", text: "<span class=\"fa fa-plus-circle\"></span> New Case Type ",
                     attributes: { "class": "btn-primary" },
                     click: function (e) {
                         $scope.$apply(function () {
                             $scope.AddCM00029();
                         });
                     }
                 },
                  {
                      type: "splitButton",
                      text: "Security",
                      menuButtons: [
                             {
                                 text: "Roles", click: function (e) {
                                     $scope.$apply(function () {
                                         $scope.CaseTypeRole();
                                     });
                                 }
                             },
                               {
                                   text: "Users", click: function (e) {
                                       $scope.$apply(function () {
                                           $scope.CaseTypeUser();
                                       });
                                   }
                               },
                               {
                                   text: "Role Access Views", click: function (e) {
                                       $scope.$apply(function () {
                                           $scope.CaseTypeViewsRole();
                                       });
                                   }
                               },
                               {
                                   text: "User Access Views", click: function (e) {
                                       $scope.$apply(function () {
                                           $scope.CaseTypeViewsUser();
                                       });
                                   }
                               }
                      ]
                  },
                 {
                     type: "splitButton",
                     text: "Configuration",
                     menuButtons: [
                              {
                                  text: "Case Type Products", click: function (e) {
                                      $scope.$apply(function () {
                                          $scope.CaseTypeProduct();
                                      });
                                  }
                              },
                              {
                                  text: "Case Type Fields", click: function (e) {
                                      $scope.$apply(function () {
                                          $scope.CaseTypeFields();
                                      });
                                  }
                              },
                               {
                                   text: "Case Standard Fields", click: function (e) {
                                       $scope.$apply(function () {
                                           $scope.CaseTypeStandardFields();
                                       });
                                   }
                               },
                              {
                                  text: "Case Type Views", click: function (e) {
                                      $scope.$apply(function () {
                                          $scope.CaseTypeViews();
                                      });
                                  }
                              },
                              {
                                  text: "Case Type Views Fields", click: function (e) {
                                      $scope.$apply(function () {
                                          $scope.CaseTypeViewsFields();
                                      });
                                  }
                              },
                               {
                                   text: "View Condition Builder", click: function (e) {
                                       $scope.$apply(function () {
                                           $scope.OpenViewsConditionBuilder();
                                       });
                                   }
                               },
                               {
                                   text: "Case Type Field Order", click: function (e) {
                                       $scope.$apply(function () {
                                           $scope.CaseTypeFieldOrder();
                                       });
                                   }
                               }
                     ]
                 },
                 {
                     type: "splitButton",
                     text: "Graph",
                     menuButtons: [
                            {
                                text: "Graph Setup", click: function (e) {
                                    $scope.$apply(function () {
                                        $scope.CaseTypeGraphSetup();
                                    });
                                }
                            },
                              {
                                  text: "Graph View", click: function (e) {
                                      $scope.$apply(function () {
                                          $scope.CaseTypeGraphView();
                                      });
                                  }
                              },
                              {
                                  text: "Show Graph", click: function (e) {
                                      $scope.$apply(function () {
                                          $scope.CaseTypeShowGraph();
                                      });
                                  }
                              },
                              {
                                  text: "View Field Summary", click: function (e) {
                                      $scope.$apply(function () {
                                          $scope.ViewFieldSummary();
                                      });
                                  }
                              },
                              {
                                  text: "View Pivot Table", click: function (e) {
                                      $scope.$apply(function () {
                                          $scope.ViewPivotTable();
                                      });
                                  }
                              },
                              {
                                  text: "Graph Category", click: function (e) {
                                      $scope.$apply(function () {
                                          $scope.GraphCategory();
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
                     click: function (e) { $scope.openFilterWindow(); }
                 },
                 {
                     type: "button", text: " Email", spriteCssClass: "k-tool-icon k-i-email",
                     click: function (e) { $scope.CaseTypeEmail(); }
                 },
                 {
                     type: "button", text: " Equation Fields", spriteCssClass: "k-tool-icon k-i-email",
                     click: function (e) { $scope.CaseTypeEquationFields(); }
                 },
                 {
                     type: "button", text: " Equation Fields By View", spriteCssClass: "k-tool-icon k-i-email",
                     click: function (e) { $scope.CaseTypeEquationFieldsByView(); }
                 },
                 {
                     type: "button", text: " Lookup", spriteCssClass: "k-tool-icon k-i-email",
                     click: function (e) { $scope.CaseTypeLookupField(); }
                 },
                 {
                     type: "button", text: " Tables", spriteCssClass: "k-tool-icon k-i-email",
                     click: function (e) { $scope.CaseTypeTables(); }
                 }
        ]
, resizable: true
    };

    $scope.CheckbookBack = function (checkbook) {
        if (checkbook != null) {
            if ($scope.CurrentControl == "CaseTypeCurrencyCode") {
                $scope.CM00029.CurrencyCode = checkbook.CurrencyCode;
                $scope.CM00029.DecimalDigit = checkbook.DecimalDigit;
                $scope.CM00029.IsMultiply = checkbook.IsMultiply;
                $scope.CM00029.ExchangeRate = checkbook.ExchangeRate;
            }
        }
    };

    $scope.LoadTableSource = function () {
        $http.get('/CMS_CaseType/GetAllCM00037?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.TableSourceList = data;
        }).finally(function () {
            
        });
    };

    $scope.TableSourceChanged = function () {
        if (angular.isDefined($scope.SelectedLookUp.SelectedTableSource)) {
            $scope.CM00029.TableSource = $scope.SelectedLookUp.SelectedTableSource;
        }
    };
    //--------------------- Load Case Types Common ---------------------------
    $scope.LoadCaseTypes = function () {
        $http.get('/CMS_CaseType/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.CaseTypes = data;
        }).finally(function () {
            $scope.CaseTypeHasOptions = {
                filter: "startswith",
                model: "SelectedCaseType",
                optionLabel: "-- Select Case Type --",
                dataTextField: "TrxTypeName",
                dataValueField: "TRXTypeID",
                dataSource: $scope.CaseTypes,
                value: $scope.SelectedLookUp.TRXTypeID
            };
        });
    };
    $scope.LoadCaseTypeViews = function (SelectedCaseType) {
        $scope.CaseTypeViewsList = [];
       // alert($scope.SelectedLookUp.SelectedType);
        $http.get('/CMS_CaseType/GetViewsByCaseType?TRXTypeID=' + SelectedCaseType + '&KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.CaseTypeViewsList = data;
        }).finally(function () { });

        //$http.get('/CMS_CaseType/GetViews?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
        //    $scope.CaseTypeViewsList = data;
        //}).finally(function () {
        //    $scope.ViewHasOptions = {
        //        filter: "startswith",
        //        model: "CM00071.SelectedView",
        //        optionLabel: "-- Select View --",
        //        dataTextField: "ViewName",
        //        dataValueField: "ViewID",
        //        dataSource: $scope.CaseTypeViewsList,
        //        value: $scope.CM00071.ViewID
        //    };
        //});
    }
    $scope.LoadAllFields_CaseType = function (TRXTypeID) {
        $http.get('/CMS_CaseType/GetAllFieldsByCaseType?KaizenPublicKey=' + sessionStorage.PublicKey,
            { params: { TRXTypeID: TRXTypeID } }).success(function (data) {
                if (data.length >= 0) {
                    if (data.length > 0)
                        $scope.CaseTypeViewsFieldsList = data;
                    else
                        $scope.CaseTypeViewsFieldsList = [];
                }
            });
    };
    $scope.LoadAllFields_ByView = function (ViewID) {
        $http.get('/CMS_CaseType/GetAllFieldsByView?KaizenPublicKey=' + sessionStorage.PublicKey,
            { params: { ViewID: ViewID } }).success(function (data) {
                if (data.length > 0) {
                    $scope.FieldsListByView = data;
                } else {
                    $scope.FieldsListByView = [];
                }
                //alert(JSON.stringify($scope.FieldsListByView, null, 2));
                //alert(JSON.stringify($scope.CaseTypeViewsFieldsList, null, 2));
                $scope.CaseTypeViewsFieldsList.forEach(function (ele, index) {
                    ele.isSelected = false;
                    $scope.FieldsListByView.forEach(function (eleViewField, index) {
                        if (ele.FieldName.trim() === eleViewField.FieldName.trim()) {
                            ele.isSelected = true;
                        }
                    });
                });
            });
    };


    $scope.LoadCM00029 = function (TRXTypeID) {
        if (angular.isUndefined($scope.CM00029.TRXTypeID)) {
            $http.get('/CMS_CaseType/GetSingle?KaizenPublicKey=' + sessionStorage.PublicKey + "&TRXTypeID=" + TRXTypeID).success(function (data) {
                $scope.CM00029 = data;
                if ($scope.CM00029.TableSource) {
                    $scope.SelectedLookUp.SelectedTableSource = $scope.CM00029.TableSource;
                }
            }).finally(function () { $scope.CM00029.Status = 2; });
        }
    }
    $scope.LoadCM00029Page = function (ActionName) {
        removeEntityPage($scope.PagesCM00029);
        var URIPath = "/CMS/" + $scope.ToolBar.ActiveScreen.ID + "/" + ActionName + "?KaizenPublicKey=" + sessionStorage.PublicKey;
        var Page = { url: URIPath, ActionName: ActionName };
        $scope.PagesCM00029.push(Page);
    };

    $scope.AddCM00029 = function () {
        $scope.LoadCM00029Page('Create');
        $scope.Clear();
        $scope.CM00029.Status = 1;
        $scope.LoadTableSource();
    }
    $scope.EditCM00029 = function (TRXTypeID) {
        $scope.LoadCM00029Page('Create');
        $scope.LoadCM00029(TRXTypeID);
        $scope.LoadTableSource();
    };
    $scope.SaveData = function () {
        $http.post('/CMS_CaseType/SaveData', { 'CM00029': $scope.CM00029, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
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
    $scope.UpdateData = function () {
        $http.post('/CMS_CaseType/UpdateData', { 'CM00029': $scope.CM00029, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
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
        $scope.CM00029 = {};
        $scope.CM00055 = {};

        $scope.Roles = [];
        $scope.CM00057 = {};
        $scope.CaseTypeUsers = [];

        $scope.CM00070 = {};
        $scope.CM00071 = {};
        $scope.CM00073 = {};
        $scope.CaseTypeViewUsers = [];
        $scope.CaseTypeViewsList = [];
        $scope.CaseTypeViewsFieldsList = [];

        $scope.CaseTypeGraphList = [];
        $scope.CM00040.SelectedGraphType = {};

        //---- Case Type Graph View
        $scope.FieldsListByView = [];
        $scope.FieldsListByView_GraphSummary = [];
        $scope.FieldSummaryListByView = [];
        $scope.FieldSummaryListByView_GraphSummary = [];

        $scope.FieldListCM00084 = [];
        $scope.FieldListCM00085 = [];
        $scope.FieldListCM00086 = [];

        $scope.GraphCategoryList = [];

        $scope.CM00076 = {};

        $scope.CM00080 = {};
        $scope.CM00081 = {};
        $scope.SelectedLookUp = {};
        $scope.CaseTypeEquationFieldsList = [];
        $scope.CaseTypeStandardFieldsList = [];

        //---- Case Type Lookup Field
        $scope.CM00050 = {};
        $scope.CaseTypeLookupFieldsList = [];

        $scope.CaseTypeTableList = [];
        $scope.CM00037 = {};
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
                $http.post('/CMS_CaseType/DeleteData', { 'CM00029': $scope.CM00029, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
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

    $scope.InsertedProducts = [];
    $scope.UpdatedProducts = [];
    $scope.DeletedProducts = [];

    $scope.Cancel = function () {
        $scope.Clear();
        $scope.PagesCM00029 = [];

        $scope.InsertedProducts = [];
        $scope.UpdatedProducts = [];
        $scope.DeletedProducts = [];
        $scope.CaseTypeProducts = [];
    };

    $scope.CaseTypeHasOptions = {
        filter: "startswith",
        optionLabel: "-- Select Case Type --"
    };

    //--------------------- Case Type Role -------------------------------
    $scope.CaseTypeRole = function () {
        $scope.Roles = [];
        $scope.LoadCM00029Page('CaseTypeRole');
        $scope.Clear();

        $http.get('/CMS_CaseType/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.CaseTypes = data;
        }).finally(function () {
            $scope.CaseTypeHasOptions = {
                filter: "startswith",
                model: "SelectedCaseType",
                optionLabel: "-- Select Case Type --",
                dataTextField: "TrxTypeName",
                dataValueField: "TRXTypeID",
                dataSource: $scope.CaseTypes,
                value: $scope.CM00029.TRXTypeID
            };
        });

        if ($scope.Roles == null || $scope.Roles.length == 0) {
            $http.get('/Sys_role/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
                $scope.Roles = data;
            }).finally(function () { });
        }
    };

    $scope.CaseTypeChangedForRoles = function () {
        //alert($scope.CM00029.SelectedCaseType);
        if (angular.isDefined($scope.CM00029.SelectedCaseType)) {
            $scope.CM00029.TRXTypeID = $scope.CM00029.SelectedCaseType;
            $scope.LoadRoles_StatusGroup($scope.CM00029.TRXTypeID);
        }
    };

    $scope.LoadRoles_StatusGroup = function (TRXTypeID) {
        $http.get('/CMS_CaseType/GetRolesByCaseType?KaizenPublicKey=' + sessionStorage.PublicKey,
            { params: { TRXTypeID: TRXTypeID } }).success(function (data) {
                if (data.length >= 0) {
                    if (data.length > 0)
                        $scope.CaseTypeRoles = data;
                    else
                        $scope.CaseTypeRoles = [];

                    $scope.Roles.forEach(function (eleRole, index) {
                        eleRole.isSelected = false;
                        $scope.CaseTypeRoles.forEach(function (eleStatusRole, index) {
                            if (eleRole.RoleID === eleStatusRole.RoleID) {
                                eleRole.isSelected = true;
                            }
                        });
                    });
                }
            });
    };

    $scope.UpdateCaseTypeRole = function (role) {
        if (!$scope.CM00029 || !$scope.CM00029.TRXTypeID) {
            $.bigBox({
                title: "Error",
                content: "Please select case type",
                color: "#C46A69",
                timeout: 8000,
                icon: "fa fa-bolt swing animated"
            });
            role.isSelected = !role.isSelected;
            return;
        }
        if (role) {
            $scope.CM00056 = {};
            $scope.CM00056.RoleID = role.RoleID;
            $scope.CM00056.TRXTypeID = $scope.CM00029.TRXTypeID;

            var urlToUpdate = "";

            if (role.isSelected) {
                urlToUpdate = "/CMS_CaseType/SaveRole?KaizenPublicKey=" + sessionStorage.PublicKey;
            } else {
                urlToUpdate = "/CMS_CaseType/DeleteRole?KaizenPublicKey=" + sessionStorage.PublicKey;
            }

            $http({
                url: urlToUpdate,
                method: "POST",
                data: JSON.stringify($scope.CM00056),
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
                        timeout: 8000,
                        icon: "fa fa-bolt swing animated"
                    });
                }
                else {
                    $.bigBox({
                        title: data.Massage,
                        content: data.Description,
                        color: "#C46A69",
                        timeout: 8000,
                        icon: "fa fa-bolt swing animated"
                    });
                    return;
                }
            }).error(function (data, status, headers, config) {
                $.bigBox({
                    title: "Error",
                    content: "Unable to save role for the selected status",
                    color: "#C46A69",
                    timeout: 8000,
                    icon: "fa fa-bolt swing animated"
                });
            });
        }
    }

    //--------------------- Case Type User -------------------------------
    $scope.CM00057 = {};
    $scope.CaseTypeUsers = [];
    $scope.CaseTypeUser = function () {
        $scope.LoadCM00029Page('CaseTypeUser');
        $scope.Clear();

        $http.get('/CMS_CaseType/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.CaseTypes = data;
        });
    };



    $scope.CaseTypeChangedForUsers = function () {
        if (angular.isDefined($scope.CM00029.SelectedCaseType)) {
            $scope.CM00029.TRXTypeID = $scope.CM00029.SelectedCaseType.TRXTypeID;
        }
    };

    $scope.UpdateCaseTypeUser = function (caseType) {
        if (!$scope.CM00057 || !$scope.CM00057.UserName) {
            $.bigBox({
                title: "Error",
                content: "Please select user name",
                color: "#C46A69",
                timeout: 8000,
                icon: "fa fa-bolt swing animated"
            });
            caseType.isSelected = !caseType.isSelected;
            return;
        }
        if (caseType) {
            var caseTypeUser = {};
            caseTypeUser.UserName = $scope.CM00057.UserName;
            caseTypeUser.TRXTypeID = caseType.TRXTypeID;

            if (caseType.isSelected) {
                $scope.AddCaseTypeByUser(caseTypeUser);
            } else {
                $scope.CaseTypeUser_Delete(caseTypeUser);
            }
        }
    };
    $scope.CaseTypeUser_Delete = function (caseTypeUser) {
        $http({
            url: '/CMS_CaseType/DeleteCaseTypeByUser?KaizenPublicKey=' + sessionStorage.PublicKey,
            method: "POST",
            data: JSON.stringify(caseTypeUser),
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
                    timeout: 8000,
                    icon: "fa fa-bolt swing animated"
                });
            }
            else {
                $.bigBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#C46A69",
                    timeout: 8000,
                    icon: "fa fa-bolt swing animated"
                });
                return;
            }
        }).error(function (data, status, headers, config) { alert(); });
    }
    $scope.AddCaseTypeByUser = function (caseTypeUser) {
        $http({
            url: '/CMS_CaseType/AddCaseTypeByUser?KaizenPublicKey=' + sessionStorage.PublicKey,
            method: "POST",
            data: JSON.stringify(caseTypeUser),
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
                    timeout: 8000,
                    icon: "fa fa-bolt swing animated"
                });
            }
            else {
                $.bigBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#C46A69",
                    timeout: 8000,
                    icon: "fa fa-bolt swing animated"
                });
                return;
            }
        }).error(function (data, status, headers, config) { alert(); });
    }

    $scope.RemoveCaseTypeUser = function (caseTypeUser, index) {

        $.SmartMessageBox({
            title: "Delete Record",
            content: "Are you sure?",
            buttons: '[No][Yes]'
        }, function (ButtonPressed) {
            if (ButtonPressed === "Yes") {
                if (caseTypeUser.status == 1)
                    $scope.CaseTypeUsers.splice(index, 1);
                else
                    caseTypeUser.status = 3;

                $scope.DeleteStatusUser(caseTypeUser);
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

    $scope.DeleteStatusGroupUser = function (caseTypeUser) {
        $http({
            url: '/CMS_CaseType/DeleteCaseTypeByUser?KaizenPublicKey=' + sessionStorage.PublicKey,
            method: "POST",
            data: JSON.stringify(caseTypeUser),
            dataType: "json",
            headers: {
                'Content-Type': 'application/json; charset=utf-8'
            }
        }).success(function (data) {
            if (data.Status == true) {
                $.bigBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#C46A69",
                    timeout: 8000,
                    icon: "fa fa-bolt swing animated"
                });
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
        }).error(function (data, status, headers, config) { alert(); });
    };
    //--------------------- Case Type Fields -------------------------------
    $scope.CM00070 = {};
    $scope.FieldTypeList = [];

    $scope.CaseTypeFields = function () {
        $scope.LoadCM00029Page('CaseTypeFields');
        $scope.Clear();
        $scope.LoadCaseTypes();
        $scope.LoadFieldTypes();
        $scope.CM00070.status = 0;
    }
    $scope.LoadFieldTypes = function () {
        $http.get('/CMS_CaseType/FillFieldTypeList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.FieldTypeList = data;
        }).finally(function () {
            $scope.FieldTypeHasOptions = {
                filter: "startswith",
                model: "SelectedFieldType",
                optionLabel: "-- Select Field Type --",
                dataTextField: "FieldTypeName",
                dataValueField: "FieldTypeID",
                dataSource: $scope.FieldTypeList,
                value: $scope.CM00070.FieldTypeID
            };
        });
    }
    $scope.FieldTypeChangedForCaseType = function () {
        if (angular.isDefined($scope.CM00070.SelectedFieldType)) {
            $scope.CM00070.FieldTypeID = $scope.CM00070.SelectedFieldType.FieldTypeID;
        }
    };

    $scope.CaseTypeChangedForFields = function () {
        //alert($scope.SelectedLookUp.SelectedType);
        $scope.CM00070.TRXTypeID = $scope.SelectedLookUp.SelectedType;
        $scope.LoadFields_CaseType($scope.SelectedLookUp.SelectedType);
    };
    $scope.LoadFields_CaseType = function (TRXTypeID) {
        $http.get('/CMS_CaseType/GetFieldsByCaseType?KaizenPublicKey=' + sessionStorage.PublicKey,
            { params: { TRXTypeID: TRXTypeID } }).success(function (data) {
                if (data.length >= 0) {
                    if (data.length > 0)
                        $scope.CaseTypeFieldsList = data;
                    else
                        $scope.CaseTypeFieldsList = [];
                }
            });
    };

    $scope.AddNewField = function () {
        $scope.CM00070.status = 1;
        if ($scope.CM00070) {
            $http({
                url: '/CMS_CaseType/SaveCaseTypeField?KaizenPublicKey=' + sessionStorage.PublicKey,
                method: "POST",
                data: JSON.stringify($scope.CM00070),
                dataType: "json",
                headers: {
                    'Content-Type': 'application/json; charset=utf-8'
                }
            }).success(function (data) {
                if (data.Status == true) {
                    $scope.CaseTypeFieldsList.push($scope.CM00070);
                    $scope.CM00070 = {};
                    $scope.CM00070.TRXTypeID = $scope.SelectedLookUp.SelectedType;
                    $scope.CM00070.status = 0;

                    $.bigBox({
                        title: data.Massage,
                        content: data.Description,
                        color: "#739E73",
                        timeout: 8000,
                        icon: "fa fa-bolt swing animated"
                    });
                } else {
                    $.bigBox({
                        title: data.Massage,
                        content: data.Description,
                        color: "#C46A69",
                        timeout: 8000,
                        icon: "fa fa-bolt swing animated"
                    });
                    return;
                }
            }).error(function (data, status, headers, config) {
                $.bigBox({
                    title: "Error",
                    content: "Unable to save case type field data",
                    color: "#C46A69",
                    timeout: 8000,
                    icon: "fa fa-bolt swing animated"
                });
            });
        }
    };

    $scope.EditField = function (caseTypeField) {
        indx = $scope.CaseTypeFieldsList.indexOf(caseTypeField);
        $scope.CM00070 = caseTypeField;
        $scope.SelectedFieldType = $scope.CM00070;
        var Obj_indx = $scope.functiontofindIndexByKeyValue($scope.CaseTypeFieldsList, "TRXTypeID", $scope.CM00070.TRXTypeID);
        var obj = $scope.CaseTypeFieldsList[Obj_indx];
        $scope.CM00029.SelectedCaseType = obj;
        //if ($scope.CM00070.status == 0 || angular.isUndefined($scope.CM00070.status)) {
        $scope.CM00070.status = 2;
        //}
    };

    $scope.UpdateField = function () {
        if ($scope.CM00070.status == 0 || angular.isUndefined($scope.CM00070.status)) {
            $scope.CM00070.status = 2;
        }
        $scope.CaseTypeFieldsList.splice(indx, 1, $scope.CM00070);

        if ($scope.CM00070) {
            if ($scope.SelectedFieldType && $scope.SelectedFieldType.FieldTypeID)
                $scope.CM00070.FieldTypeID = $scope.SelectedFieldType.FieldTypeID;

            $http({
                url: '/CMS_CaseType/UpdateField?KaizenPublicKey=' + sessionStorage.PublicKey,
                method: "POST",
                data: $scope.CM00070,
                dataType: "json",
                headers: {
                    'Content-Type': 'application/json; charset=utf-8'
                }
            }).success(function (data) {
                if (data.Status == true) {
                    $scope.CM00070 = {};
                    $scope.CM00070.status = 0;

                    $.bigBox({
                        title: data.Massage,
                        content: data.Description,
                        color: "#739E73",
                        timeout: 8000,
                        icon: "fa fa-bolt swing animated"
                    });
                } else {
                    $.bigBox({
                        title: data.Massage,
                        content: data.Description,
                        color: "#C46A69",
                        timeout: 8000,
                        icon: "fa fa-bolt swing animated"
                    });
                    return;
                }
            }).error(function (data, status, headers, config) { alert(); });
        }
    };


    $scope.RemoveCaseTypeField = function (caseTypeField, index) {

        $.SmartMessageBox({
            title: "Delete Record",
            content: "Are you sure?",
            buttons: '[No][Yes]'
        }, function (ButtonPressed) {
            if (ButtonPressed === "Yes") {

                $http({
                    url: '/CMS_CaseType/DeleteField?KaizenPublicKey=' + sessionStorage.PublicKey,
                    method: "POST",
                    data: JSON.stringify(caseTypeField),
                    dataType: "json",
                    headers: {
                        'Content-Type': 'application/json; charset=utf-8'
                    }
                }).success(function (data) {
                    if (data.Status == true) {
                        if (caseTypeField.status == 1)
                            $scope.CaseTypeFieldsList.splice(index, 1);
                        else
                            caseTypeField.status = 3;
                        //Notify(data.Massage, 'bottom-right', '5000', 'success', 'fa-check', true);
                        $.bigBox({
                            title: data.Massage,
                            content: data.Description,
                            color: "#C46A69",
                            timeout: 8000,
                            icon: "fa fa-bolt swing animated"
                        });
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
    };

    //--------------------- Case Type Views -------------------------------
    $scope.CM00071 = {};
    $scope.CaseTypeViews = function () {
        $scope.LoadCM00029Page('CaseTypeViews');
        $scope.Clear();
        $scope.LoadCaseTypes();
        $scope.LoadTableSource();
        $scope.CM00071.status = 0;
    }
    $scope.TableSourceChangedForViews = function () {
        if (angular.isDefined($scope.SelectedLookUp.SelectedTableSource)) {
            $scope.CM00071.TableSource = $scope.SelectedLookUp.SelectedTableSource;
        }
    };
    //$scope.ViewRolesDataSource_Change = function () {
    //    $scope.CaseTypeViewsList = [];
    //    $http.get('/CMS_CaseType/GetViewsByCaseType?TRXTypeID=' + $scope.SelectedLookUp.SelectedType + '&KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
    //        $scope.CaseTypeViewsList = data;
    //    }).finally(function () { });
    //}
 


    $scope.CaseTypeChangedForViews = function () {
        if (angular.isDefined($scope.SelectedLookUp.SelectedType)) {
            $scope.CM00029.TRXTypeID = $scope.SelectedLookUp.SelectedType;
            $scope.CM00071.TRXTypeID = $scope.SelectedLookUp.SelectedType;
            $scope.LoadViews_CaseType($scope.CM00029.TRXTypeID);
        }
    };
    $scope.AddNewView = function () {
        $scope.CM00071.status = 1;

        if ($scope.CM00071.TableSource == undefined || $scope.CM00071.TableSource == "") {
            $.bigBox({
                title: "Error",
                content: "Table Source is missing",
                color: "#C46A69",
                timeout: 8000,
                icon: "fa fa-bolt swing animated"
            });
            return;
        }

        if ($scope.CM00071) {
            $http({
                url: '/CMS_CaseType/SaveCaseTypeView?KaizenPublicKey=' + sessionStorage.PublicKey,
                method: "POST",
                data: JSON.stringify($scope.CM00071),
                dataType: "json",
                headers: {
                    'Content-Type': 'application/json; charset=utf-8'
                }
            }).success(function (data) {
                if (data.Status == true) {
                    $scope.CaseTypeViewsList.push($scope.CM00071);
                    $scope.CM00071 = {};
                    //$scope.SelectedLookUp = {};
                    $scope.CM00071.status = 0;

                    $.bigBox({
                        title: data.Massage,
                        content: data.Description,
                        color: "#739E73",
                        timeout: 8000,
                        icon: "fa fa-bolt swing animated"
                    });
                    $scope.LoadViews_CaseType($scope.CM00029.TRXTypeID);
                } else {
                    $.bigBox({
                        title: data.Massage,
                        content: data.Description,
                        color: "#C46A69",
                        timeout: 8000,
                        icon: "fa fa-bolt swing animated"
                    });
                    return;
                }
            }).error(function (data, status, headers, config) {
                $.bigBox({
                    title: "Error",
                    content: "Unable to save case type view data",
                    color: "#C46A69",
                    timeout: 8000,
                    icon: "fa fa-bolt swing animated"
                });
            });
        }
    };

    $scope.EditView = function (caseTypeView) {
        indx = $scope.CaseTypeViewsList.indexOf(caseTypeView);
        $scope.CM00071 = caseTypeView;
        $scope.SelectedLookUp.SelectedType = caseTypeView.TRXTypeID;
        $scope.SelectedLookUp.SelectedTableSource = caseTypeView.TableSource;
        $scope.CM00071.status = 2;
        //}
    };

    $scope.UpdateView = function () {
        if ($scope.CM00071.status == 0 || angular.isUndefined($scope.CM00071.status)) {
            $scope.CM00071.status = 2;
        }
        $scope.CaseTypeViewsList.splice(indx, 1, $scope.CM00071);

        if ($scope.CM00071) {
            $http({
                url: '/CMS_CaseType/UpdateView?KaizenPublicKey=' + sessionStorage.PublicKey,
                method: "POST",
                data: JSON.stringify($scope.CM00071),
                dataType: "json",
                headers: {
                    'Content-Type': 'application/json; charset=utf-8'
                }
            }).success(function (data) {
                if (data.Status == true) {
                    $scope.CM00071 = {};
                    $scope.CM00071.status = 0;
                    $scope.SelectedLookUp = {};

                    $.bigBox({
                        title: data.Massage,
                        content: data.Description,
                        color: "#739E73",
                        timeout: 8000,
                        icon: "fa fa-bolt swing animated"
                    });
                } else {
                    $.bigBox({
                        title: data.Massage,
                        content: data.Description,
                        color: "#C46A69",
                        timeout: 8000,
                        icon: "fa fa-bolt swing animated"
                    });
                    return;
                }
            }).error(function (data, status, headers, config) { alert(); });
        }
    };

    $scope.LoadViews_CaseType = function (TRXTypeID) {
        $http.get('/CMS_CaseType/GetViewsByCaseType?KaizenPublicKey=' + sessionStorage.PublicKey,
            { params: { TRXTypeID: TRXTypeID } }).success(function (data) {
                if (data.length >= 0) {
                    if (data.length > 0)
                        $scope.CaseTypeViewsList = data;
                    else
                        $scope.CaseTypeViewsList = [];
                }
            });
    };

    $scope.RemoveCaseTypeView = function (caseTypeView, index) {

        $.SmartMessageBox({
            title: "Delete Record",
            content: "Are you sure?",
            buttons: '[No][Yes]'
        }, function (ButtonPressed) {
            if (ButtonPressed === "Yes") {

                $http({
                    url: '/CMS_CaseType/DeleteView?KaizenPublicKey=' + sessionStorage.PublicKey,
                    method: "POST",
                    data: JSON.stringify(caseTypeView),
                    dataType: "json",
                    headers: {
                        'Content-Type': 'application/json; charset=utf-8'
                    }
                }).success(function (data) {
                    if (data.Status == true) {
                            $scope.CaseTypeViewsList.splice(index, 1);

                            $.bigBox({
                            title: data.Massage,
                            content: data.Description,
                            color: "#C46A69",
                            timeout: 8000,
                            icon: "fa fa-bolt swing animated"
                        });
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
    };

    //--------------------- Case Type Views Fields 75 -------------------------------
    $scope.CM00075 = {};
    $scope.CaseTypeViewsFields = function () {
        $scope.CaseTypeViewsList = [];
        $scope.LoadCM00029Page('CaseTypeViewsFields');
        $scope.Clear();
        $scope.LoadCaseTypes();
        $scope.CaseViewformatOptions = {
            filter: "contains",
            optionLabel: "-- ALL View --",
            dataTextField: "ViewName",
            dataValueField: "ViewID",
            dataSource: $scope.CaseTypeViewsList,
        };
        //$scope.LoadCaseTypeViews();
        $scope.CM00075.status = 0;
    }
    $scope.CaseTypeViewFields_Changed = function () {

        $scope.CM00075.TRXTypeID = $scope.SelectedLookUp.SelectedType;
        $scope.LoadAllFields_CaseType($scope.SelectedLookUp.SelectedType);
        $scope.LoadCaseTypeViews($scope.SelectedLookUp.SelectedType);
    };
    $scope.ViewField_Changed = function () {
        $scope.CM00075.ViewID = $scope.SelectedLookUp.SelectedView;
        $scope.LoadAllFields_ByView($scope.SelectedLookUp.SelectedView);
    };

    //----------------------------------------------------
    $scope.CaseTypeChangedForViewsFields = function () {
        $scope.CM00029.TRXTypeID = $scope.SelectedLookUp.SelectedCaseType;
        $scope.CM00075.TRXTypeID = $scope.SelectedLookUp.SelectedCaseType;
        $scope.LoadAllFields_CaseType($scope.SelectedLookUp.SelectedType);
        $scope.LoadCaseTypeViews($scope.SelectedLookUp.SelectedType);
        $scope.CM00071 = {};
    };
    $scope.ViewChangedForFields = function () {
        if (angular.isDefined($scope.CM00071.SelectedView)) {
            $scope.CM00075.ViewID = $scope.CM00071.SelectedView.ViewID;
            $scope.LoadAllFields_ByView($scope.CM00075.ViewID);
        }
    };
    $scope.OpenViewsConditionBuilder = function () {
        $scope.LoadCM00029Page('ViewsConditionBuilder');

        $scope.LoadCaseTypes();
    };

    $scope.CaseTypeChangedForConditionBuilder = function () {
        if (angular.isDefined($scope.CM00029.SelectedCaseType)) {
            $scope.CM00029.TRXTypeID = $scope.CM00029.SelectedCaseType.TRXTypeID;
            $scope.CM00071.TRXTypeID = $scope.CM00029.SelectedCaseType.TRXTypeID;

            $scope.LoadViews_CaseTypeConditionBuilder($scope.CM00029.TRXTypeID);
            $scope.FieldsListByView = [];
        }
    };

    $scope.LoadViews_CaseTypeConditionBuilder = function (TRXTypeID) {
        $http.get('/CMS_CaseType/GetViewsByCaseType?KaizenPublicKey=' + sessionStorage.PublicKey,
            { params: { TRXTypeID: TRXTypeID } }).success(function (data) {
                if (data.length > 0)
                    $scope.CaseTypeViewsList = data;
                else
                    $scope.CaseTypeViewsList = [];
            }).finally(function () {
                //$scope.ViewHasOptions = {
                //    filter: "startswith",
                //    model: "CM00071.SelectedView",
                //    optionLabel: "-- Select View --",
                //    dataTextField: "ViewName",
                //    dataValueField: "ViewID",
                //    dataSource: $scope.CaseTypeViewsList,
                //    value: $scope.CM00071.ViewID
                //};
            });
    };

    $scope.ViewChangedForConditionBuilder = function () {
        if (angular.isDefined($scope.CM00071.SelectedView)) {
            $scope.CM00071.ViewID = $scope.CM00071.SelectedView.ViewID;

            $scope.SeachSQL = $scope.CM00071.SelectedView.WhereCondition;
            $scope.SeachText = $scope.SeachSQL;
            $scope.LoadAllFields_ByViewCondBuilder($scope.CM00071.ViewID);
            //alert($scope.SeachSQL)
        }
    };

    $scope.LoadAllFields_ByViewCondBuilder = function (ViewID) {

        $http.get('/CMS_CaseType/GetAllFieldsByView?KaizenPublicKey=' + sessionStorage.PublicKey,
            { params: { ViewID: ViewID } }).success(function (data) {
                if (data.length > 0) {
                    $scope.FieldsListByView = data;
                    //alert(JSON.stringify($scope.FieldsListByView, null, 2));
                } else {
                    $scope.FieldsListByView = [];
                }
            });
    };

    $scope.UpdateConditionBuilder = function () {
        if ($scope.CM00071.status == 0 || angular.isUndefined($scope.CM00071.status)) {
            $scope.CM00071.status = 2;
        }
        if ($scope.CM00071.SelectedView) {
            if ($scope.SeachSQL) {
                $scope.CM00071.SelectedView.WhereCondition = $scope.SeachSQL;
            }

            $http({
                url: '/CMS_CaseType/UpdateView?KaizenPublicKey=' + sessionStorage.PublicKey,
                method: "POST",
                data: JSON.stringify($scope.CM00071.SelectedView),
                dataType: "json",
                headers: {
                    'Content-Type': 'application/json; charset=utf-8'
                }
            }).success(function (data) {
                if (data.Status == true) {
                    $scope.CM00071.status = 0;
                    $scope.SeachSQL = '';
                    $scope.SeachText = '';

                    $.bigBox({
                        title: data.Massage,
                        content: data.Description,
                        color: "#739E73",
                        timeout: 8000,
                        icon: "fa fa-bolt swing animated"
                    });
                } else {
                    $.bigBox({
                        title: data.Massage,
                        content: data.Description,
                        color: "#C46A69",
                        timeout: 8000,
                        icon: "fa fa-bolt swing animated"
                    });
                    return;
                }
            }).error(function (data, status, headers, config) { alert(); });
        }
    };

    $scope.SeachText = '';
    $scope.SeachSQL = '';
    $scope.IsOrOperation = 0;
    $scope.AddSeach = function (IsOr) {
        $scope.Temp = '';
        $scope.TempText = '';
        $scope.FieldsListByView.forEach(function (element, index) {
            if (element.IsChecked) {
                var str = '';
                if (element.FieldTypeID == 2) {
                    var dd = new Date(element.FixedValue);
                    str = dd.getFullYear() + "-" + (dd.getMonth() + 1) + "-" + dd.getDate();
                }
                else //if (element.FieldTypeID == 3 || element.FieldTypeID == 11 || element.FieldTypeID == 24 || element.FieldTypeID == 24)
                {
                    str = "'" + element.FixedValue + "'";
                }
                if ($scope.Temp != '') {
                    if ($scope.IsOrOperation == 1) {
                        $scope.TempText += " OR ";
                        $scope.Temp += " OR ";
                    }
                    else {
                        $scope.TempText += " and ";
                        $scope.Temp += " and ";
                    }
                }
                $scope.Temp += element.FieldName + "=" + str.toString();
                $scope.TempText += element.FieldDisplay + "=" + str.toString();
            }
            element.IsChecked = false;
        });
        if ($scope.SeachSQL != '') {
            if (IsOr == 1) {
                $scope.SeachText += " OR ";
                $scope.SeachSQL += " OR ";
            } else {
                $scope.SeachText += " and ";
                $scope.SeachSQL += " and ";
            }
        }
        $scope.SeachText += "(" + $scope.TempText + ")";
        $scope.SeachSQL += "(" + $scope.Temp + ")";
    };
    $scope.OperationSelectation = function () {
        $scope.IsOrOperation = 1;
    }
    $scope.Filter_Selected = function (column) {
        if (!column.IsChecked) {
            column.FixedValue = null;
            column.html = '';
            return;
        }
        //alert(column.FieldTypeID);
        switch (column.FieldTypeID) {
            case 2:
                column.FixedValue = new Date();
                column.html = "<input kendo-date-picker k-parse-formats=\"['yyyy-MM-ddTHH:mm:ss']\" k-format='dd/MM/yyyy' k-ng-model='column.FixedValue' style='width: 100%;' />";
                break;
            case 3:
                column.html = "<input type='text' class='form-control' ng-model='column.FixedValue' />";
                break;
            case 6:
                column.html = "<input kendo-numeric-text-box class=\"currency\" k-options=\"NumberFormatOptions('',3,false)\" k-ng-model=\"column.FixedValue\" style=\"width:100%\" />";
                break;
            case 11:
                column.html = "<div class='input-group'><input type='text' class='form-control' readonly " +
                      "placeholder='Nationality ID' ng-model='column.FixedValue' />" +
                      "<span class='input-group-btn input-group-sm'>" +
                      "<button class='btn btn-default' ng-click=\"OpenkendoWindow('Nationality',column.FieldName)\">" +
                      "<span class='glyphicon glyphicon-search'></span></button></span></div>";
                break;
            case 15:
                column.html = "<div class='input-group'><input type='text' class='form-control' readonly " +
                    "placeholder='CLient' ng-model='column.FixedValue' />" +
                    "<span class='input-group-btn input-group-sm'>" +
                    "<button class='btn btn-default' ng-click=\"OpenkendoWindowForm('CMS', 'CMS_CO_Client','PopUp',null,'Main')\">" +
                    "<span class='glyphicon glyphicon-search'></span></button></span></div>";
                break;
            case 17:
                column.html = "<div class='input-group'><input type='text' class='form-control' readonly " +
                    "placeholder='CLient' ng-model='column.FixedValue' />" +
                    "<span class='input-group-btn input-group-sm'>" +
                    "<button class='btn btn-default' ng-click=\"OpenkendoWindow('CMS_ContractLookup',column.FieldName)\">" +
                    "<span class='glyphicon glyphicon-search'></span></button></span></div>";
                break;
            case 20:
                column.html = "<div class='input-group'><input type='text' class='form-control' readonly " +
                    "placeholder='Legal' ng-model='column.FixedValue' />" +
                    "<span class='input-group-btn input-group-sm'>" +
                    "<button class='btn btn-default' ng-click=\"OpenkendoWindow('CMS_LegalLookup',column.FieldName)\">" +
                    "<span class='glyphicon glyphicon-search'></span></button></span></div>";
                break;
            case 23:
                $scope.CaseStatusList();
                //alert(JSON.stringify($scope.CM00700List, null, 2));
                column.html = "<select kendo-drop-down-list k-filter=\"'startswith'\" k-option-label=\"'--Select Status-'\" k-ng-model=\"column.FixedValue\" k-data-text-field=\"'CaseStatusname'\" k-value-primitive=\"true\" k-data-value-field=\"'CaseStatusID'\" k-data-source=\"CM00700List\" k-on-change=\"StatusChanged()\" style=\"width: 100%\"></select>";
                break;
            case 24:
                column.html = "<div class='input-group'><input type='text' class='form-control' readonly " +
                    "placeholder='AgentID' ng-model='column.FixedValue' />" +
                    "<span class='input-group-btn input-group-sm'>" +
                    "<button class='btn btn-default' ng-click=\"OpenkendoWindow('KaizenUser',null,'ViewsConditionBuilder')\">" +
                    "<span class='glyphicon glyphicon-search'></span></button></span></div>";
                break;
        }
        return;
        if (column.FieldName == 'CUSTCLAS') {
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

    };
    $scope.StatusChanged = function () {

    }

    $scope.LegalBack = function (Legal) {
        if (Legal != null) {
            var index = $scope.GetSingleIndex($scope.FieldsListByView, "FieldName", $scope.ParmObject);
            $scope.FieldsListByView[index].FixedValue = Legal.LegalID;
        }
    };
    $scope.NationalityBack = function (nationality) {
        if (nationality != null) {
            var index = $scope.GetSingleIndex($scope.FieldsListByView, "FieldName", $scope.ParmObject);
            $scope.FieldsListByView[index].FixedValue = nationality.NationalityID;
        }
    };
    $scope.UserBack = function (user) {
        if (user != null) {
            switch ($scope.CurrentControl) {
                case 'CaseTypeUsers':
                    $scope.CaseTypeUsers = [];
                    $scope.CM00057.UserName = user.UserName;
                    $http.get('/CMS_CaseType/GetCaseTypeByUser?KaizenPublicKey=' + sessionStorage.PublicKey,
                        { params: { userName: user.UserName } }).success(function (data) {
                            $scope.CaseTypeUsers = data;
                            $scope.CaseTypes.forEach(function (ele, index) {
                                ele.isSelected = false;
                                $scope.CaseTypeUsers.forEach(function (eleselected, index) {
                                    if (ele.TRXTypeID === eleselected.TRXTypeID) {
                                        ele.isSelected = true;
                                    }
                                });
                            });
                        });
                    break;
                case 'CaseTypeViewUsers':
                    $scope.CaseTypeViewUsers = [];
                    $scope.CM00073.UserName = user.UserName;
                    $http.get('/CMS_CaseType/GetCaseTypeViewsByUser?KaizenPublicKey=' + sessionStorage.PublicKey,
                        { params: { TRXTypeID: $scope.SelectedLookUp.SelectedType, userName: user.UserName } }).success(function (data) {
                            if (data.length >= 0) {
                                $scope.CaseTypeViewUsers = data;

                                $scope.CaseTypeViewsList.forEach(function (ele, index) {
                                    ele.isSelected = false;
                                    ele.IsDefault = false;
                                    $scope.CaseTypeViewUsers.forEach(function (eleselected, index) {
                                        if (ele.ViewID === eleselected.ViewID) {
                                            ele.isSelected = true;
                                            ele.IsDefault = eleselected.IsDefault;
                                        }
                                    });
                                });
                            }
                        });
                    break;
                case 'ViewsConditionBuilder':
                    if (user != null) {
                        alert(user.UserName)
                        var index = $scope.GetSingleIndex($scope.FieldsListByView, "FieldName", 'AgentID');
                        $scope.FieldsListByView[index].FixedValue = user.UserName;
                    }
                    break;
            }
        }
    };
    $scope.ClientBack = function (client) {
        if (client != null) {
            switch ($scope.CurrentControl) {
                case 'Main':
                    //alert(client.ClientID);
                    var index = $scope.GetSingleIndex($scope.FieldsListByView, "FieldName", 'ClientID');
                    //alert(index);
                    $scope.FieldsListByView[index].FixedValue = client.ClientID;
                    break;
            }
        }
    };
    $scope.ContractBack = function (contract) {
        if (contract != null) {
            var index = $scope.GetSingleIndex($scope.FieldsListByView, "FieldName", $scope.ParmObject);
            $scope.FieldsListByView[index].FixedValue = contract.ContractID;
        }
    };




    $scope.UpdateCaseTypeViewsFields = function (viewsFieldsObj) {

        if (viewsFieldsObj) {
            if (!$scope.CM00075 || !$scope.CM00075.ViewID) {
                $.bigBox({
                    title: "Error",
                    content: "Please select view",
                    color: "#C46A69",
                    timeout: 8000,
                    icon: "fa fa-bolt swing animated"
                });
                viewsFieldsObj.isSelected = !viewsFieldsObj.isSelected;
                return;
            }
            //$scope.CM00056 = {};
            //$scope.CM00056.RoleID = role.RoleID;
            viewsFieldsObj.ViewID = $scope.CM00075.ViewID;
            viewsFieldsObj.TRXTypeID = $scope.CM00075.TRXTypeID;

            $scope.CM00075 = viewsFieldsObj;

            var urlToUpdate = "";

            if (viewsFieldsObj.isSelected) {
                urlToUpdate = "/CMS_CaseType/SaveViewsFields?KaizenPublicKey=" + sessionStorage.PublicKey;
            } else {
                urlToUpdate = "/CMS_CaseType/DeleteViewsFields?KaizenPublicKey=" + sessionStorage.PublicKey;
            }

            $http({
                url: urlToUpdate,
                method: "POST",
                data: JSON.stringify($scope.CM00075),
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
                        timeout: 8000,
                        icon: "fa fa-bolt swing animated"
                    });
                }
                else {
                    $.bigBox({
                        title: data.Massage,
                        content: data.Description,
                        color: "#C46A69",
                        timeout: 8000,
                        icon: "fa fa-bolt swing animated"
                    });
                    return;
                }
            }).error(function (data, status, headers, config) {
                $.bigBox({
                    title: "Error",
                    content: "Unable to save views fields for the selected case type",
                    color: "#C46A69",
                    timeout: 8000,
                    icon: "fa fa-bolt swing animated"
                });
            });
        }
    }

    $scope.UpdateViewsFields = function () {
        //var selView = $scope.CM00071.SelectedView;
        if ($scope.CM00071 && $scope.CM00071.SelectedView) {
            var selectedViewsFields = [];
            $scope.CaseTypeViewsFieldsList.forEach(function (ele, index) {
                if (ele.isSelected === true) {
                    ele.ViewID = $scope.CM00071.SelectedView.ViewID;
                    selectedViewsFields.push(ele);
                }
            });
            if (selectedViewsFields.length > 0) {
                $http({
                    url: '/CMS_CaseType/UpdateViewsFields?KaizenPublicKey=' + sessionStorage.PublicKey,
                    method: "POST",
                    data: selectedViewsFields,
                    dataType: "json",
                    headers: {
                        'Content-Type': 'application/json; charset=utf-8'
                    }
                }).success(function (data) {
                    if (data.Status == true) {
                        //$scope.CM00073 = {};
                        $scope.CM00073.status = 0;
                        $.bigBox({
                            title: data.Massage,
                            content: data.Description,
                            color: "#739E73",
                            timeout: 8000,
                            icon: "fa fa-bolt swing animated"
                        });
                    } else {
                        $.bigBox({
                            title: data.Massage,
                            content: data.Description,
                            color: "#C46A69",
                            timeout: 8000,
                            icon: "fa fa-bolt swing animated"
                        });
                        return;
                    }
                }).error(function (data, status, headers, config) {
                    $.bigBox({
                        title: "Error",
                        content: "Unable to update case type views fields",
                        color: "#C46A69",
                        timeout: 8000,
                        icon: "fa fa-bolt swing animated"
                    });
                });
            }
        } else {
            $.bigBox({
                title: "Info",
                content: "Please select view",
                color: "#C46A69",
                timeout: 8000,
                icon: "fa fa-bolt swing animated"
            });
        }
    };




    //--------------------- Case View User -------------------------------
    $scope.CM00073 = {};
    $scope.CaseTypeViewsUser = function () {
        $scope.LoadCM00029Page('CaseTypeViewsUser');
        $scope.Clear();
        $scope.LoadCaseTypes();
        $scope.CM00073.status = 0;
    }
    $scope.CaseTypeViewUser_Changed = function () {
        $scope.LoadCaseTypeViews($scope.SelectedLookUp.SelectedType);
    };
    $scope.ViewChangedForUsers = function () {
        if (angular.isDefined($scope.CM00071.SelectedView)) {
            $scope.CM00071.ViewID = $scope.CM00071.SelectedView.ViewID;
        }
    };

    $scope.UpdateCaseTypeViewUser = function (caseTypeView) {
        if (!$scope.CM00073 || !$scope.CM00073.UserName) {
            $.bigBox({
                title: "Error",
                content: "Please select user name",
                color: "#C46A69",
                timeout: 8000,
                icon: "fa fa-bolt swing animated"
            });
            caseTypeView.isSelected = !caseTypeView.isSelected;
            return;
        }
        if (caseTypeView) {
            var caseTypeViewUser = {};
            caseTypeViewUser.UserName = $scope.CM00073.UserName;
            caseTypeViewUser.ViewID = caseTypeView.ViewID;
            caseTypeViewUser.IsDefault = caseTypeView.IsDefault;

            if (caseTypeView.isSelected) {
                $scope.AddCaseTypeViewByUser(caseTypeViewUser);
            } else {
                $scope.DeleteCaseTypeViewUser(caseTypeViewUser);
            }
        }
    };

    $scope.UpdateIsDefaultViewUser = function (caseTypeView) {
        if (!$scope.CM00073 || !$scope.CM00073.UserName) {
            $.bigBox({
                title: "Error",
                content: "Please select user name",
                color: "#C46A69",
                timeout: 8000,
                icon: "fa fa-bolt swing animated"
            });
            caseTypeView.isSelected = !caseTypeView.isSelected;
            return;
        }
        if (caseTypeView) {
            var caseTypeViewUser = {};
            caseTypeViewUser.UserName = $scope.CM00073.UserName;
            caseTypeViewUser.ViewID = caseTypeView.ViewID;
            caseTypeViewUser.IsDefault = caseTypeView.IsDefault;

            if (caseTypeView.isSelected) {
                $scope.UpdateCaseTypeViewByUser(caseTypeViewUser);
            }
        }
    };

    $scope.UpdateCaseTypeViewByUser = function (caseTypeViewUser) {
        $http({
            url: '/CMS_CaseType/UpdateCaseTypeViewByUser?KaizenPublicKey=' + sessionStorage.PublicKey,
            method: "POST",
            data: JSON.stringify(caseTypeViewUser),
            dataType: "json",
            headers: {
                'Content-Type': 'application/json; charset=utf-8'
            }
        }).success(function (data) {
            if (data.Status == true) {
                //$scope.CM00073 = {};
                $scope.CM00073.status = 0;
                $.bigBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#739E73",
                    timeout: 8000,
                    icon: "fa fa-bolt swing animated"
                });
            } else {
                $.bigBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#C46A69",
                    timeout: 8000,
                    icon: "fa fa-bolt swing animated"
                });
                return;
            }
        }).error(function (data, status, headers, config) {
            $.bigBox({
                title: "Error",
                content: "Unable to save case type view user",
                color: "#C46A69",
                timeout: 8000,
                icon: "fa fa-bolt swing animated"
            });
        });
    };

    $scope.AddCaseTypeViewByUser = function (caseTypeViewUser) {
        $http({
            url: '/CMS_CaseType/AddCaseTypeViewByUser?KaizenPublicKey=' + sessionStorage.PublicKey,
            method: "POST",
            data: JSON.stringify(caseTypeViewUser),
            dataType: "json",
            headers: {
                'Content-Type': 'application/json; charset=utf-8'
            }
        }).success(function (data) {
            if (data.Status == true) {
                //$scope.CM00073 = {};
                $scope.CM00073.status = 0;
                $.bigBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#739E73",
                    timeout: 8000,
                    icon: "fa fa-bolt swing animated"
                });
            }
            else {
                $.bigBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#C46A69",
                    timeout: 8000,
                    icon: "fa fa-bolt swing animated"
                });
                return;
            }
        }).error(function (data, status, headers, config) {
            $.bigBox({
                title: "Error",
                content: "Unable to save case type view user",
                color: "#C46A69",
                timeout: 8000,
                icon: "fa fa-bolt swing animated"
            });
        });
    }

    $scope.RemoveCaseTypeViewUser = function (caseTypeViewUser, index) {

        $.SmartMessageBox({
            title: "Delete Record",
            content: "Are you sure?",
            buttons: '[No][Yes]'
        }, function (ButtonPressed) {
            if (ButtonPressed === "Yes") {
                if (caseTypeViewUser.status == 1)
                    $scope.CaseTypeViewUsers.splice(index, 1);
                else
                    caseTypeViewUser.status = 3;

                $scope.DeleteCaseTypeViewUser(caseTypeViewUser);
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

    $scope.DeleteCaseTypeViewUser = function (caseTypeViewUser) {
        $http({
            url: '/CMS_CaseType/DeleteCaseTypeViewUser?KaizenPublicKey=' + sessionStorage.PublicKey,
            method: "POST",
            data: JSON.stringify(caseTypeViewUser),
            dataType: "json",
            headers: {
                'Content-Type': 'application/json; charset=utf-8'
            }
        }).success(function (data) {
            if (data.Status == true) {
                $.bigBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#C46A69",
                    timeout: 8000,
                    icon: "fa fa-bolt swing animated"
                });
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
                title: "Error",
                content: "Unable to delete case type view user",
                color: "#C46A69",
                timeout: 8000,
                icon: "fa fa-bolt swing animated"
            });
        });
    };

    //--------------------- Case Type Views Role -------------------------------
    $scope.CM00072 = {};
    $scope.CaseTypeViewsRole = function () {
        $scope.Clear();
        $http.get('/CMS_CaseType/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.CaseTypes = data;
        }).finally(function () {});
        $scope.LoadCM00029Page('CaseTypeViewsRole');
        $scope.CM00072.status = 0;
        if ($scope.Roles == null || $scope.Roles.length == 0) {
            $http.get('/Sys_role/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
                $scope.Roles = data;
            }).finally(function () { });
        }
    }
    $scope.ViewRolesDataSource_Change = function () {
        $scope.CaseTypeViewsList = [];
        $http.get('/CMS_CaseType/GetViewsByCaseType?TRXTypeID=' + $scope.SelectedLookUp.SelectedCaseType + '&KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.CaseTypeViewsList = data;
        }).finally(function () { });
    }
    $scope.ViewRolesView_Changed = function () {
        if (angular.isDefined($scope.CM00071.SelectedView)) {
            $scope.CM00071.ViewID = $scope.CM00071.SelectedView.ViewID;
            $http.get('/CMS_CaseType/GetRolesByView?KaizenPublicKey=' + sessionStorage.PublicKey,
             { params: { ViewID: $scope.CM00071.SelectedView } }).success(function (data) {
                 if (data.length >= 0) {
                     if (data.length > 0)
                         $scope.CaseTypeViewRoles = data;
                     else
                         $scope.CaseTypeViewRoles = [];

                     $scope.Roles.forEach(function (eleRole, index) {
                         eleRole.isSelected = false;
                         $scope.CaseTypeViewRoles.forEach(function (eleViewRole, index) {
                             if (eleRole.RoleID === eleViewRole.RoleID) {
                                 eleRole.isSelected = true;
                             }
                         });
                     });
                 }
             });
        }
    };
    $scope.ViewRolesView_SaveData = function (role) {
        if (!$scope.CM00071 || !$scope.CM00071.SelectedView) {
            $.bigBox({
                title: "Error",
                content: "Please select view",
                color: "#C46A69",
                timeout: 8000,
                icon: "fa fa-bolt swing animated"
            });
            role.isSelected = !role.isSelected;
            return;
        }
        if (role) {
            $scope.CM00072 = {};
            $scope.CM00072.RoleID = role.RoleID;
            $scope.CM00072.ViewID = $scope.CM00071.SelectedView;
            var urlToUpdate = "";
            if (role.isSelected) {
                urlToUpdate = "/CMS_CaseType/SaveViewRole?KaizenPublicKey=" + sessionStorage.PublicKey;
            } else {
                urlToUpdate = "/CMS_CaseType/DeleteViewRole?KaizenPublicKey=" + sessionStorage.PublicKey;
            }
            $http({
                url: urlToUpdate,
                method: "POST",
                data: JSON.stringify($scope.CM00072),
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
                        timeout: 8000,
                        icon: "fa fa-bolt swing animated"
                    });
                }
                else {
                    $.bigBox({
                        title: data.Massage,
                        content: data.Description,
                        color: "#C46A69",
                        timeout: 8000,
                        icon: "fa fa-bolt swing animated"
                    });
                    return;
                }
            }).error(function (data, status, headers, config) { });
        }
    }

    $scope.ViewChangedForRoles = function () {
        if (angular.isDefined($scope.CM00071.SelectedView)) {
            $scope.CM00071.ViewID = $scope.CM00071.SelectedView.ViewID;
            $scope.LoadRoles_ByViews($scope.CM00071.ViewID);
        }
    };

    $scope.LoadRoles_ByViews = function (ViewID) {
        $http.get('/CMS_CaseType/GetRolesByView?KaizenPublicKey=' + sessionStorage.PublicKey,
            { params: { ViewID: ViewID } }).success(function (data) {
                if (data.length >= 0) {
                    if (data.length > 0)
                        $scope.CaseTypeViewRoles = data;
                    else
                        $scope.CaseTypeViewRoles = [];

                    $scope.Roles.forEach(function (eleRole, index) {
                        eleRole.isSelected = false;
                        $scope.CaseTypeViewRoles.forEach(function (eleViewRole, index) {
                            if (eleRole.RoleID === eleViewRole.RoleID) {
                                eleRole.isSelected = true;
                            }
                        });
                    });
                }
            });
    };
    


    $scope.ViewHasOptions = {
        filter: "startswith",
        optionLabel: "-- Select View --"
    };

    $scope.LoadViews_CaseTypeWithOptions = function (TRXTypeID) {
        $http.get('/CMS_CaseType/GetViewsByCaseType?KaizenPublicKey=' + sessionStorage.PublicKey,
            { params: { TRXTypeID: TRXTypeID } }).success(function (data) {
                if (data.length >= 0) {
                    if (data.length > 0) {
                        $scope.CaseTypeViewsList = data;
                    } else
                        $scope.CaseTypeViewsList = [];

                    $scope.ViewHasOptions = {
                        filter: "startswith",
                        model: "CM00071.SelectedView",
                        optionLabel: "-- Select View --",
                        dataTextField: "ViewName",
                        dataValueField: "ViewID",
                        dataSource: $scope.CaseTypeViewsList,
                        value: $scope.CM00071.ViewID
                    };
                }
            });
    };


    //--------------------- Case Type Product -------------------------------
    $scope.CaseTypeProduct = function () {
        $scope.LoadCM00029Page('CaseTypeProduct');
        $scope.Clear();

        $http.get('/CMS_CaseType/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.CaseTypes = data;
        }).finally(function () {
            $scope.CaseTypeHasOptions = {
                filter: "startswith",
                model: "SelectedCaseType",
                optionLabel: "-- Select Case Type --",
                dataTextField: "TrxTypeName",
                dataValueField: "TRXTypeID",
                dataSource: $scope.CaseTypes,
                value: $scope.CM00029.TRXTypeID
            };
        });

        $scope.CM00055.status = 0;
    };
    var indx;
    $scope.AddNewProduct = function () {
        $scope.CM00055.status = 1;
        $scope.CM00055.TRXTypeID = $scope.CM00029.TRXTypeID;

        $http({
            url: '/CMS_CaseType/SaveProduct?KaizenPublicKey=' + sessionStorage.PublicKey,
            method: "POST",
            data: JSON.stringify($scope.CM00055),
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
                    timeout: 8000,
                    icon: "fa fa-bolt swing animated"
                });

                $scope.LoadProducts_CaseType($scope.CM00029.TRXTypeID);

                $scope.CaseTypeProducts.push($scope.CM00055);
                $scope.CM00055 = {};
                $scope.CM00055.status = 0;
            } else {
                $.bigBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#C46A69",
                    timeout: 8000,
                    icon: "fa fa-bolt swing animated"
                });
                return;
            }
        }).error(function (data, status, headers, config) { alert(); });
    };
    $scope.UpdateProduct = function () {
        //if ($scope.CM00055.status == 0 || angular.isUndefined($scope.CM00055.status)) {
        //    $scope.CM00055.status = 2;
        //}
        //$scope.CaseTypeProducts.splice(indx, 1, $scope.CM00055);

        $http({
            url: '/CMS_CaseType/UpdateProduct?KaizenPublicKey=' + sessionStorage.PublicKey,
            method: "POST",
            data: JSON.stringify($scope.CM00055),
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
                    timeout: 8000,
                    icon: "fa fa-bolt swing animated"
                });

                $scope.CM00055 = {};
                $scope.CM00055.status = 0;
            } else {
                $.bigBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#C46A69",
                    timeout: 8000,
                    icon: "fa fa-bolt swing animated"
                });
                return;
            }
        }).error(function (data, status, headers, config) { alert(); });
    };
    $scope.EditProduct = function (product) {
        indx = $scope.CaseTypeProducts.indexOf(product);
        $scope.CM00055 = product;
        var Obj_indx = $scope.functiontofindIndexByKeyValue($scope.CaseTypeProducts, "TRXTypeID", $scope.CM00055.TRXTypeID);
        var obj = $scope.CaseTypeProducts[Obj_indx];
        $scope.CM00029.SelectedCaseType = obj;
        //if ($scope.CM00055.status == 0 || angular.isUndefined($scope.CM00055.status)) {
        $scope.CM00055.status = 2;
        //}
    };

    $scope.SaveCaseTypeProduct = function () {
        if ($scope.CaseTypeProducts) {
            for (var i = 0; i < $scope.CaseTypeProducts.length; i++) {
                var item = $scope.CaseTypeProducts[i];
                if (item.status == 1) {
                    var insert_tmp = {
                        TRXTypeID: item.TRXTypeID,
                        //ProductID: item.ProductID,
                        ProductName: item.ProductName
                    };
                    $scope.InsertedProducts.push(insert_tmp);
                } else if (item.status == 2) {
                    var update_tmp = {
                        ProductID: item.ProductID,
                        TRXTypeID: item.TRXTypeID,
                        ProductName: item.ProductName
                    };
                    $scope.UpdatedProducts.push(update_tmp);
                } else if (item.status == 3) {
                    var delete_tmp = {
                        ProductID: item.ProductID,
                        TRXTypeID: item.TRXTypeID,
                        ProductName: item.ProductName
                    };
                    $scope.DeletedProducts.push(delete_tmp);
                }
            }

            if ($scope.InsertedProducts.length > 0) {
                $http({
                    url: '/CMS_CaseType/SaveProduct?KaizenPublicKey=' + sessionStorage.PublicKey,
                    method: "POST",
                    data: JSON.stringify($scope.InsertedProducts),
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
                            timeout: 8000,
                            icon: "fa fa-bolt swing animated"
                        });
                    } else {
                        $.bigBox({
                            title: data.Massage,
                            content: data.Description,
                            color: "#C46A69",
                            timeout: 8000,
                            icon: "fa fa-bolt swing animated"
                        });
                        return;
                    }
                }).error(function (data, status, headers, config) { alert(); });
            }
            if ($scope.UpdatedProducts.length > 0) {
                $http({
                    url: '/CMS_CaseType/UpdateProduct?KaizenPublicKey=' + sessionStorage.PublicKey,
                    method: "POST",
                    data: JSON.stringify($scope.UpdatedProducts),
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
                            timeout: 8000,
                            icon: "fa fa-bolt swing animated"
                        });
                    } else {
                        $.bigBox({
                            title: data.Massage,
                            content: data.Description,
                            color: "#C46A69",
                            timeout: 8000,
                            icon: "fa fa-bolt swing animated"
                        });
                        return;
                    }
                }).error(function (data, status, headers, config) { alert(); });
            }
            if ($scope.DeletedProducts.length > 0) {
                $http({
                    url: '/CMS_CaseType/DeleteProduct?KaizenPublicKey=' + sessionStorage.PublicKey,
                    method: "POST",
                    data: JSON.stringify($scope.DeletedProducts),
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
                            color: "#C46A69",
                            timeout: 8000,
                            icon: "fa fa-bolt swing animated"
                        });
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
        }
        $scope.Cancel();
    };
    $scope.RemoveCaseTypeProduct = function (caseTypeProduct, index) {

        $.SmartMessageBox({
            title: "Delete Record",
            content: "Are you sure?",
            buttons: '[No][Yes]'
        }, function (ButtonPressed) {
            if (ButtonPressed === "Yes") {
                if (caseTypeProduct.status == 1)
                    $scope.CaseTypeProducts.splice(index, 1);
                else
                    caseTypeProduct.status = 3;

                $http({
                    url: '/CMS_CaseType/DeleteProduct?KaizenPublicKey=' + sessionStorage.PublicKey,
                    method: "POST",
                    data: JSON.stringify(caseTypeProduct),
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
                            color: "#C46A69",
                            timeout: 8000,
                            icon: "fa fa-bolt swing animated"
                        });
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
    };

    $scope.CaseTypeChangedForProducts = function () {
        if (angular.isDefined($scope.CM00029.SelectedCaseType)) {
            $scope.CM00029.TRXTypeID = $scope.CM00029.SelectedCaseType.TRXTypeID;
            $scope.LoadProducts_CaseType($scope.CM00029.TRXTypeID);
        }
    };

    $scope.LoadProducts_CaseType = function (TRXTypeID) {
        $http.get('/CMS_CaseType/GetProductsByCaseType?KaizenPublicKey=' + sessionStorage.PublicKey,
            { params: { TRXTypeID: TRXTypeID } }).success(function (data) {
                if (data.length >= 0) {
                    if (data.length > 0)
                        $scope.CaseTypeProducts = data;
                    else
                        $scope.CaseTypeProducts = [];
                }
            });
    };

    //-------------------------- Case Type Standard Fields ----------------
    $scope.CM00074 = {};
    $scope.IsShowCM00074FieldOrder = false;
    $scope.CaseTypeStandardFields = function () {
        $scope.Clear();
        $http.get('/CMS_CaseType/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.CaseTypes = data;
        }).finally(function () {
            $scope.LoadCM00029Page('CaseTypeStandardFields');
        });
    };
    $scope.CaseTypeChangedForStandardFields = function () {
        //alert($scope.CM00029.SelectedCaseType)
        $scope.CM00029.TRXTypeID = $scope.CM00029.SelectedCaseType;
            $scope.CM00074.TRXTypeID = $scope.CM00029.SelectedCaseType;
            $scope.LoadStandardFields_CaseType($scope.CM00029.TRXTypeID);
    };
    $scope.LoadStandardFieldOrder = function () {
        $scope.IsShowCM00074FieldOrder = true;
        $scope.CM00029.SelectedCaseType = 0;
        $scope.CaseTypeStandardFieldsList = [];
    };
    $scope.BackFieldOrderCM00074 = function () {
        $scope.IsShowCM00074FieldOrder = false;
        $scope.CM00029.SelectedCaseType = 0;
        $scope.CaseTypeStandardFieldsList=[];
        $scope.CaseTypeStandardFields();
    };
    $scope.LoadStandardFields_CaseType = function (TRXTypeID) {
        $http.get('/CMS_CaseType/GetStandardFieldsByCaseType?KaizenPublicKey=' + sessionStorage.PublicKey,
            { params: { TRXTypeID: TRXTypeID } }).success(function (data) {
                if (data.length >= 0) {
                    if (data.length > 0)
                        $scope.CaseTypeStandardFieldsList = data;
                    else
                        $scope.CaseTypeStandardFieldsList = [];
                }
            });
    };
    $scope.CaseTypeStandardFieldsList = [];
    $scope.UpdateStandardFields = function () {
        //var selView = $scope.CM00071.SelectedView;
        $scope.tt = [];
        $scope.CaseTypeStandardFieldsList.forEach(function (eleStatusRole, index) {
            var tttemp = {
                FieldDisplay: eleStatusRole.FieldDisplay,
                FieldName: eleStatusRole.FieldName,
                FieldOrder: eleStatusRole.FieldOrder,
                IsActive: eleStatusRole.IsActive,
                FieldTypeID: eleStatusRole.FieldTypeID,
                FieldWidth: eleStatusRole.FieldWidth,
                //IsEmailable: eleStatusRole.IsEmailable,
                //IsFilterable: eleStatusRole.IsFilterable,
                //Islockable: eleStatusRole.Islockable,
                //Islocked: eleStatusRole.Islocked,
                //IsPrintable: eleStatusRole.IsPrintable,
                //IsRequired: eleStatusRole.IsRequired,
                //IsSortable: eleStatusRole.IsSortable
                TRXTypeID: eleStatusRole.TRXTypeID
            }
            $scope.tt.push(tttemp);
        });
        $http.post('/CMS_CaseType/UpdateStandardFields', {
            'CM00074List': $scope.tt, 'callNumber': 1, 'KaizenPublicKey': sessionStorage.PublicKey
        }).success(function (data) {
            if (data.Status == true) {
                $.smallBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#739E73",
                    iconSmall: "fa fa-times fa-2x fadeInRight animated",
                    timeout: 4000
                });

                $scope.tt = [];
                $scope.CaseTypeStandardFieldsList.forEach(function (eleStatusRole, index) {
                    var tttemp = {
                        //FieldDisplay: eleStatusRole.FieldDisplay,
                        FieldName: eleStatusRole.FieldName,
                        //FieldOrder: eleStatusRole.FieldOrder,
                        //IsActive: eleStatusRole.IsActive,
                        IsEmailable: eleStatusRole.IsEmailable,
                        IsFilterable: eleStatusRole.IsFilterable,
                        Islockable: eleStatusRole.Islockable,
                        Islocked: eleStatusRole.Islocked,
                        IsPrintable: eleStatusRole.IsPrintable,
                        IsRequired: eleStatusRole.IsRequired,
                        IsSortable: eleStatusRole.IsSortable,
                        TRXTypeID: eleStatusRole.TRXTypeID
                    }
                    $scope.tt.push(tttemp);
                });
                $http.post('/CMS_CaseType/UpdateStandardFields', {
                    'CM00074List': $scope.tt, 'callNumber': 2, 'KaizenPublicKey': sessionStorage.PublicKey
                }).success(function (data) {
                    if (data.Status == true) {
                        $.smallBox({
                            title: data.Massage,
                            content: data.Description,
                            color: "#739E73",
                            iconSmall: "fa fa-times fa-2x fadeInRight animated",
                            timeout: 4000
                        });

                    }
                    else {
                        //alert('ssssss')
                    }
                }).error(function (data, status, headers, config) {
                    alert(data);
                });
            }
            else {
                //alert('ssssss')
            }
        }).error(function (data, status, headers, config) {
            alert(data);
        });
    };

    $scope.DeleteCM00074 = function (obj, index) {

        $.SmartMessageBox({
            title: "Delete Record",
            content: "Are you sure?",
            buttons: '[No][Yes]'
        }, function (ButtonPressed) {
            if (ButtonPressed === "Yes") {

                $http({
                    url: '/CMS_CaseType/DeleteCM00074?KaizenPublicKey=' + sessionStorage.PublicKey,
                    method: "POST",
                    data: JSON.stringify(obj),
                    dataType: "json",
                    headers: {
                        'Content-Type': 'application/json; charset=utf-8'
                    }
                }).success(function (data) {
                    if (data.Status == true) {
                        $scope.CaseTypeStandardFieldsList.splice(index, 1);
                        //Notify(data.Massage, 'bottom-right', '5000', 'success', 'fa-check', true);
                        $.bigBox({
                            title: data.Massage,
                            content: data.Description,
                            color: "#C46A69",
                            timeout: 8000,
                            icon: "fa fa-bolt swing animated"
                        });
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
                }).error(function (data, status, headers, config) {
                    $.bigBox({
                        title: "Error",
                        content: "Unable to delete data",
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

    //-------------------------- Case Type Graph Setup ----------------
    $scope.SelectedTabName = "CM00077";
    $scope.CM00076 = {};
    $scope.CaseTypeGraphSetup = function () {
        $scope.LoadCM00029Page('CaseTypeGraphSetup');
        $scope.Clear();
        $scope.LoadCaseTypes();
        $scope.LoadGraphTypes();
        $scope.CM00076.status = 0;
    };

    $scope.LoadGraphTypes = function () {
        $http.get('/CMS_CaseType/GetGraphTypes?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.GraphTypes = data;
        }).finally(function () {
        });
    }
    $scope.CaseTypeChangedForGraphSetup = function () {
        if (angular.isDefined($scope.CM00029.SelectedCaseType)) {
            $scope.CaseTypeGraphList = [];
            $scope.CM00029.TRXTypeID = $scope.CM00029.SelectedCaseType;
            //$scope.CM00076.TRXTypeID = $scope.CM00029.SelectedCaseType.TRXTypeID;
            $scope.LoadViews_CaseTypeGraph($scope.CM00029.TRXTypeID);
        }
    };
    $scope.ViewGraphHasOptions = {
        optionLabel: "-- View --"
    };
    $scope.LoadViews_CaseTypeGraph = function (TRXTypeID) {
        $http.get('/CMS_CaseType/GetViewsByCaseType?KaizenPublicKey=' + sessionStorage.PublicKey,
            { params: { TRXTypeID: TRXTypeID } }).success(function (data) {
                if (data.length >= 0) {
                    if (data.length > 0) {
                        $scope.CaseTypeViewsList = data;
                    } else
                        $scope.CaseTypeViewsList = [];

                    $scope.ViewGraphHasOptions = {
                        filter: "startswith",
                        model: "CM00071.SelectedView",
                        optionLabel: "-- Select View --",
                        dataTextField: "ViewName",
                        dataValueField: "ViewID",
                        dataSource: $scope.CaseTypeViewsList,
                        value: $scope.CM00071.ViewID
                    };
                }
            });
    };

    $scope.ViewChangedForGraphSetup = function () {
        if (angular.isDefined($scope.CM00071.SelectedView)) {
            $scope.CM00071.ViewID = $scope.CM00071.SelectedView.ViewID;
            $scope.CM00076.ViewID = $scope.CM00071.SelectedView.ViewID;
            $scope.LoadGraphData_ByView($scope.CM00071.ViewID);
        }
    };

    $scope.LoadGraphData_ByView = function (ViewID) {
        $http.get('/CMS_CaseType/GetGraphData?KaizenPublicKey=' + sessionStorage.PublicKey,
            { params: { ViewID: ViewID } }).success(function (data) {
                $scope.CaseTypeGraphList = data;
            }).finally(function () {

            });
    }
    $scope.CM00040 = {};
    $scope.GraphTypeChanged = function () {
        if (angular.isDefined($scope.CM00040.SelectedGraphType)) {
            $scope.CM00076.GraphTypeID = $scope.CM00040.SelectedGraphType;
        }
    };

    $scope.AddNewGraphSetup = function () {
        $scope.CM00076.status = 1;
        if ($scope.CM00076) {
            $http({
                url: '/CMS_CaseType/AddCM00076?KaizenPublicKey=' + sessionStorage.PublicKey,
                method: "POST",
                data: JSON.stringify($scope.CM00076),
                dataType: "json",
                headers: {
                    'Content-Type': 'application/json; charset=utf-8'
                }
            }).success(function (data) {
                if (data.Status == true) {
                    $scope.CaseTypeGraphList.push($scope.CM00076);
                    $scope.CM00076 = {};
                    $scope.CM00076.status = 0;

                    $.bigBox({
                        title: data.Massage,
                        content: data.Description,
                        color: "#739E73",
                        timeout: 8000,
                        icon: "fa fa-bolt swing animated"
                    });
                } else {
                    $.bigBox({
                        title: data.Massage,
                        content: data.Description,
                        color: "#C46A69",
                        timeout: 8000,
                        icon: "fa fa-bolt swing animated"
                    });
                    return;
                }
            }).error(function (data, status, headers, config) {
                $.bigBox({
                    title: "Error",
                    content: "Unable to save case type graph data",
                    color: "#C46A69",
                    timeout: 8000,
                    icon: "fa fa-bolt swing animated"
                });
            });
        }
    };

    $scope.EditGraphSetup = function (caseTypeGraph) {
        indx = $scope.CaseTypeGraphList.indexOf(caseTypeGraph);
        $scope.CM00076 = caseTypeGraph;
        $scope.CM00071.SelectedView = caseTypeGraph;
        $scope.CM00040.SelectedGraphType = caseTypeGraph.GraphTypeID;
        //var Obj_indx = $scope.functiontofindIndexByKeyValue($scope.CaseTypeGraphList, "TRXTypeID", $scope.CM00076.TRXTypeID);
        //var obj = $scope.CaseTypeFieldsList[Obj_indx];
        //$scope.CM00029.SelectedCaseType = obj;
        //if ($scope.CM00076.status == 0 || angular.isUndefined($scope.CM00076.status)) {
        $scope.CM00076.status = 2;
        //}
    };

    $scope.UpdateGraphSetup = function () {
        if ($scope.CM00076.status == 0 || angular.isUndefined($scope.CM00076.status)) {
            $scope.CM00076.status = 2;
        }
        $scope.CaseTypeGraphList.splice(indx, 1, $scope.CM00076);

        if ($scope.CM00076) {

            $http({
                url: '/CMS_CaseType/UpdateCM00076?KaizenPublicKey=' + sessionStorage.PublicKey,
                method: "POST",
                data: $scope.CM00076,
                dataType: "json",
                headers: {
                    'Content-Type': 'application/json; charset=utf-8'
                }
            }).success(function (data) {
                if (data.Status == true) {
                    $scope.CM00076 = {};
                    $scope.CM00076.status = 0;

                    $.bigBox({
                        title: data.Massage,
                        content: data.Description,
                        color: "#739E73",
                        timeout: 8000,
                        icon: "fa fa-bolt swing animated"
                    });
                } else {
                    $.bigBox({
                        title: data.Massage,
                        content: data.Description,
                        color: "#C46A69",
                        timeout: 8000,
                        icon: "fa fa-bolt swing animated"
                    });
                    return;
                }
            }).error(function (data, status, headers, config) {
                $.bigBox({
                    title: "Error",
                    content: "Unable to update selected graph data",
                    color: "#C46A69",
                    timeout: 8000,
                    icon: "fa fa-bolt swing animated"
                });
            });
        }
    };

    $scope.RemoveCaseTypeGraphSetup = function (caseTypeGraphSetup, index) {

        $.SmartMessageBox({
            title: "Delete Record",
            content: "Are you sure?",
            buttons: '[No][Yes]'
        }, function (ButtonPressed) {
            if (ButtonPressed === "Yes") {

                $http({
                    url: '/CMS_CaseType/DeleteCM00076?KaizenPublicKey=' + sessionStorage.PublicKey,
                    method: "POST",
                    data: JSON.stringify(caseTypeGraphSetup),
                    dataType: "json",
                    headers: {
                        'Content-Type': 'application/json; charset=utf-8'
                    }
                }).success(function (data) {
                    if (data.Status == true) {
                        if (caseTypeGraphSetup.status == 1)
                            $scope.CaseTypeGraphList.splice(index, 1);
                        else
                            caseTypeGraphSetup.status = 3;
                        //Notify(data.Massage, 'bottom-right', '5000', 'success', 'fa-check', true);
                        $.bigBox({
                            title: data.Massage,
                            content: data.Description,
                            color: "#C46A69",
                            timeout: 8000,
                            icon: "fa fa-bolt swing animated"
                        });
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
                }).error(function (data, status, headers, config) {
                    $.bigBox({
                        title: "Error",
                        content: "Unable to delete selected graph data",
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

    //-------------------------- Case Type Graph View ----------------
    $scope.CM00077 = {};
    $scope.CM00078 = {};
    $scope.CaseTypeGraphView = function () {
        $scope.LoadCM00029Page('CaseTypeGraphView');
        $scope.Clear();
        $scope.LoadCaseTypes();
        $scope.LoadGraphTypes();
        $scope.CM00076.status = 0;
    };
    $scope.CaseTypeChangedForGraphView = function () {
        if (angular.isDefined($scope.SelectedLookUp.SelectedType)) {
            $scope.CaseTypeGraphList = [];
            $scope.CM00029.TRXTypeID = $scope.SelectedLookUp.SelectedType;
            //$scope.CM00076.TRXTypeID = $scope.SelectedLookUp.SelectedType;
            $scope.LoadViews_CaseTypeGraph($scope.CM00029.TRXTypeID);
            $scope.FieldsListByView = [];
            $scope.FieldsListByView_GraphSummary = [];
        }
    };

    $scope.ViewChangedForGraphView = function () {
        if (angular.isDefined($scope.CM00071.SelectedView)) {
            $scope.CM00071.ViewID = $scope.CM00071.SelectedView.ViewID;
            $scope.CM00077.ViewID = $scope.CM00071.SelectedView.ViewID;
            $scope.CM00078.ViewID = $scope.CM00071.SelectedView.ViewID;
            $scope.LoadGraphViewData_ByView($scope.CM00071.ViewID);
            $scope.LoadAllFields_ByView_ForGraph($scope.CM00071.ViewID);
            $scope.LoadAllFields_ByView_ForGraph_Summary($scope.CM00071.ViewID);
            $scope.LoadSummaryTypes();
        }
    };

    $scope.GraphViewChanged = function () {
        if (angular.isDefined($scope.CM00076.SelectedGraph)) {
            $scope.CM00076.GraphID = $scope.CM00076.SelectedGraph.GraphID;
            $scope.CM00077.GraphID = $scope.CM00076.SelectedGraph.GraphID;
            $scope.CM00078.GraphID = $scope.CM00076.SelectedGraph.GraphID;
            $scope.LoadGraphViewsFields($scope.CM00076.GraphID);
        }
    };
    $scope.FieldsListByView = [];
    $scope.LoadGraphViewsFields = function (GraphID) {
        $http.get('/CMS_CaseType/GetGraphViewsFields?KaizenPublicKey=' + sessionStorage.PublicKey,
            { params: { GraphID: GraphID } }).success(function (data) {
                $scope.CaseTypeGraphViewsFields = data;

                $scope.FieldsListByView.forEach(function (ele, index) {
                    ele.isSelected = false;
                    $scope.CaseTypeGraphViewsFields.forEach(function (eleSelected, index) {
                        if (ele.FieldName.trim() === eleSelected.FieldName.trim()) {
                            ele.isSelected = true;
                        }
                    });
                });

                $http.get('/CMS_CaseType/GetGraphViewsFields_Summary?KaizenPublicKey=' + sessionStorage.PublicKey,
                { params: { GraphID: GraphID } }).success(function (data) {
                    $scope.CaseTypeGraphViewsFields_Summary = data;

                    $scope.FieldsListByView_GraphSummary.forEach(function (ele, index) {
                        ele.isSelected = false;
                        $scope.CaseTypeGraphViewsFields_Summary.forEach(function (eleSelected, index) {
                            if (ele.FieldName.trim() === eleSelected.FieldName.trim()) {
                                ele.isSelected = true;
                                ele.SummaryType = {};
                                ele.SummaryType = eleSelected.SummeryTypeID;
                            }
                        });
                    });
                });
            });
    };

    $scope.LoadSummaryTypes = function () {
        $http.get('/CMS_CaseType/GetSummaryTypes?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.SummaryTypeList = data;
        }).finally(function () {
            //$scope.SummaryTypeHasOptions = {
            //    filter: "startswith",
            //    //model: "CM00047.SelectedSummaryType",
            //    optionLabel: "-- Select Summary Type --",
            //    dataTextField: "SummeryTypeName",
            //    dataValueField: "SummeryTypeID",
            //    dataSource: $scope.SummaryTypeList,
            //    //value: "SummeryTypeID"
            //};
        });
    };
    $scope.CM00047 = {};

    $scope.SummaryTypeChanged = function (field) {
        if (angular.isDefined(field) && field.SummaryType != undefined) {
            //$scope.CM00047.SummeryTypeID = field.SummaryType;
            $scope.CM00078.SummeryTypeID = field.SummaryType;
        }
    };
    $scope.GraphViewDataHasOptions = {
        optionLabel: "-- Graph --"
    };
    $scope.LoadGraphViewData_ByView = function (ViewID) {
        $http.get('/CMS_CaseType/GetGraphData?KaizenPublicKey=' + sessionStorage.PublicKey,
           { params: { ViewID: ViewID } }).success(function (data) {
               $scope.CaseTypeGraphList = data;
           }).finally(function () {
               $scope.GraphViewDataHasOptions = {
                   filter: "startswith",
                   model: "CM00076.SelectedGraph",
                   optionLabel: "-- Select Graph --",
                   dataTextField: "GraphName",
                   dataValueField: "GraphID",
                   dataSource: $scope.CaseTypeGraphList,
                   value: $scope.CM00076.GraphID
               };
           });
    }

    $scope.LoadAllFields_ByView_ForGraph = function (ViewID) {
        $http.get('/CMS_CaseType/GetAllFieldsByView?KaizenPublicKey=' + sessionStorage.PublicKey,
            { params: { ViewID: ViewID } }).success(function (data) {
                if (data.length >= 0) {
                    if (data.length > 0) {
                        $scope.FieldsListByView = data;
                    } else {
                        $scope.FieldsListByView = [];
                    }
                }
            });
    };

    $scope.LoadAllFields_ByView_ForGraph_Summary = function (ViewID) {
        $http.get('/CMS_CaseType/GetAllFieldsByView?KaizenPublicKey=' + sessionStorage.PublicKey,
            { params: { ViewID: ViewID } }).success(function (data) {
                if (data.length >= 0) {
                    if (data.length > 0) {
                        $scope.FieldsListByView_GraphSummary = data;

                    } else {
                        $scope.FieldsListByView_GraphSummary = [];
                    }
                }
            });
    };

    $scope.AddOrDeleteGraphViewsFields = function (viewsFieldsObj) {

        if (viewsFieldsObj) {
            if ($scope.CM00077 == undefined || $scope.CM00077.ViewID == undefined) {
                $.bigBox({
                    title: "Error",
                    content: "Please select view",
                    color: "#C46A69",
                    timeout: 8000,
                    icon: "fa fa-bolt swing animated"
                });
                viewsFieldsObj.isSelected = !viewsFieldsObj.isSelected;
                return;
            }
            if ($scope.CM00077 == undefined || $scope.CM00077.GraphID == undefined) {
                $.bigBox({
                    title: "Error",
                    content: "Please select graph",
                    color: "#C46A69",
                    timeout: 8000,
                    icon: "fa fa-bolt swing animated"
                });
                viewsFieldsObj.isSelected = !viewsFieldsObj.isSelected;
                return;
            }
            viewsFieldsObj.GraphID = $scope.CM00077.GraphID;
            $scope.CM00077 = viewsFieldsObj;

            var urlToUpdate = "";

            if (viewsFieldsObj.isSelected) {
                urlToUpdate = "/CMS_CaseType/AddCM00077?KaizenPublicKey=" + sessionStorage.PublicKey;
            } else {
                urlToUpdate = "/CMS_CaseType/DeleteCM00077?KaizenPublicKey=" + sessionStorage.PublicKey;
            }

            $http({
                url: urlToUpdate,
                method: "POST",
                data: JSON.stringify($scope.CM00077),
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
                        timeout: 8000,
                        icon: "fa fa-bolt swing animated"
                    });
                } else {
                    $.bigBox({
                        title: data.Massage,
                        content: data.Description,
                        color: "#C46A69",
                        timeout: 8000,
                        icon: "fa fa-bolt swing animated"
                    });
                    return;
                }
            }).error(function (data, status, headers, config) {
                $.bigBox({
                    title: "Error",
                    content: "Unable to save views fields for the selected graph",
                    color: "#C46A69",
                    timeout: 8000,
                    icon: "fa fa-bolt swing animated"
                });
            });
        }
    };

    $scope.UpdateGraphViewsFields = function (tabName) {
        if ($scope.SelectedTabName === tabName) {
            $scope.UpdateCM00077();
        }
        else if ($scope.SelectedTabName === tabName) {
            $scope.UpdateCM00078();
        }
    };

    $scope.UpdateCM00077 = function () {
        //var selView = $scope.CM00071.SelectedView;
        if ($scope.CM00071 && $scope.CM00071.SelectedView) {
            if (!$scope.CM00076 && !$scope.CM00076.SelectedGraph.GraphID) {
                $.bigBox({
                    title: "Info",
                    content: "Please select graph",
                    color: "#C46A69",
                    timeout: 8000,
                    icon: "fa fa-bolt swing animated"
                });
                return;
            }
            var selectedViewsFields = [];
            $scope.FieldsListByView.forEach(function (ele, index) {
                if (ele.isSelected === true) {
                    ele.ViewID = $scope.CM00071.SelectedView.ViewID;
                    ele.GraphID = $scope.CM00076.SelectedGraph.GraphID;
                    selectedViewsFields.push(ele);
                }
            });
            if (selectedViewsFields.length > 0) {
                $http({
                    url: '/CMS_CaseType/UpdateCM00077?KaizenPublicKey=' + sessionStorage.PublicKey,
                    method: "POST",
                    data: selectedViewsFields,
                    dataType: "json",
                    headers: {
                        'Content-Type': 'application/json; charset=utf-8'
                    }
                }).success(function (data) {
                    if (data.Status == true) {
                        //$scope.CM00073 = {};
                        $scope.CM00073.status = 0;
                        $.bigBox({
                            title: data.Massage,
                            content: data.Description,
                            color: "#739E73",
                            timeout: 8000,
                            icon: "fa fa-bolt swing animated"
                        });
                    } else {
                        $.bigBox({
                            title: data.Massage,
                            content: data.Description,
                            color: "#C46A69",
                            timeout: 8000,
                            icon: "fa fa-bolt swing animated"
                        });
                        return;
                    }
                }).error(function (data, status, headers, config) {
                    $.bigBox({
                        title: "Error",
                        content: "Unable to update graph views fields",
                        color: "#C46A69",
                        timeout: 8000,
                        icon: "fa fa-bolt swing animated"
                    });
                });
            }
        } else {
            $.bigBox({
                title: "Info",
                content: "Please select view",
                color: "#C46A69",
                timeout: 8000,
                icon: "fa fa-bolt swing animated"
            });
        }
    };

    $scope.UpdateCM00078 = function () {
        //var selView = $scope.CM00071.SelectedView;
        if ($scope.CM00071 && $scope.CM00071.SelectedView) {
            if (!$scope.CM00076 && !$scope.CM00076.SelectedGraph.GraphID) {
                $.bigBox({
                    title: "Info",
                    content: "Please select graph",
                    color: "#C46A69",
                    timeout: 8000,
                    icon: "fa fa-bolt swing animated"
                });
                return;
            }
            var selectedViewsFields = [];
            $scope.FieldsListByView_GraphSummary.forEach(function (ele, index) {
                if (ele.isSelected === true) {
                    ele.ViewID = $scope.CM00071.SelectedView.ViewID;
                    ele.GraphID = $scope.CM00076.SelectedGraph.GraphID;
                    if (ele.SummaryType == undefined || ele.SummaryType == undefined
                        || ele.SummaryType === "") {
                        $.bigBox({
                            title: "Info",
                            content: "Summery Type is missing",
                            color: "#C46A69",
                            timeout: 8000,
                            icon: "fa fa-bolt swing animated"
                        });
                        return;
                    }
                    ele.SummeryTypeID = ele.SummaryType;
                    selectedViewsFields.push(ele);
                }
            });
            if (selectedViewsFields.length > 0) {
                $http({
                    url: '/CMS_CaseType/UpdateCM00078?KaizenPublicKey=' + sessionStorage.PublicKey,
                    method: "POST",
                    data: selectedViewsFields,
                    dataType: "json",
                    headers: {
                        'Content-Type': 'application/json; charset=utf-8'
                    }
                }).success(function (data) {
                    if (data.Status == true) {
                        //$scope.CM00073 = {};
                        $scope.CM00073.status = 0;
                        $.bigBox({
                            title: data.Massage,
                            content: data.Description,
                            color: "#739E73",
                            timeout: 8000,
                            icon: "fa fa-bolt swing animated"
                        });
                    } else {
                        $.bigBox({
                            title: data.Massage,
                            content: data.Description,
                            color: "#C46A69",
                            timeout: 8000,
                            icon: "fa fa-bolt swing animated"
                        });
                        return;
                    }
                }).error(function (data, status, headers, config) {
                    $.bigBox({
                        title: "Error",
                        content: "Unable to update graph views fields",
                        color: "#C46A69",
                        timeout: 8000,
                        icon: "fa fa-bolt swing animated"
                    });
                });
            }
        } else {
            $.bigBox({
                title: "Info",
                content: "Please select view",
                color: "#C46A69",
                timeout: 8000,
                icon: "fa fa-bolt swing animated"
            });
        }
    };
    $scope.AddOrDeleteGraphSummary = function (viewsFieldsObj) {

        if (viewsFieldsObj) {
            if ($scope.CM00078 == undefined || $scope.CM00078.ViewID == undefined) {
                $.bigBox({
                    title: "Error",
                    content: "Please select view",
                    color: "#C46A69",
                    timeout: 8000,
                    icon: "fa fa-bolt swing animated"
                });
                viewsFieldsObj.isSelected = !viewsFieldsObj.isSelected;
                return;
            }
            if ($scope.CM00078 == undefined || $scope.CM00078.GraphID == undefined) {
                $.bigBox({
                    title: "Error",
                    content: "Please select graph",
                    color: "#C46A69",
                    timeout: 8000,
                    icon: "fa fa-bolt swing animated"
                });
                viewsFieldsObj.isSelected = !viewsFieldsObj.isSelected;
                return;
            }
            if (viewsFieldsObj.SummaryType == undefined || viewsFieldsObj.SummaryType == undefined) {
                $.bigBox({
                    title: "Error",
                    content: "Please select summary type",
                    color: "#C46A69",
                    timeout: 8000,
                    icon: "fa fa-bolt swing animated"
                });
                viewsFieldsObj.isSelected = !viewsFieldsObj.isSelected;
                return;
            }
            viewsFieldsObj.GraphID = $scope.CM00078.GraphID;
            viewsFieldsObj.SummeryTypeID = viewsFieldsObj.SummaryType;
            $scope.CM00078 = viewsFieldsObj;

            var urlToUpdate = "";

            if (viewsFieldsObj.isSelected) {
                urlToUpdate = "/CMS_CaseType/AddCM00078?KaizenPublicKey=" + sessionStorage.PublicKey;
            } else {
                urlToUpdate = "/CMS_CaseType/DeleteCM00078?KaizenPublicKey=" + sessionStorage.PublicKey;
            }

            $http({
                url: urlToUpdate,
                method: "POST",
                data: JSON.stringify($scope.CM00078),
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
                        timeout: 8000,
                        icon: "fa fa-bolt swing animated"
                    });
                } else {
                    $.bigBox({
                        title: data.Massage,
                        content: data.Description,
                        color: "#C46A69",
                        timeout: 8000,
                        icon: "fa fa-bolt swing animated"
                    });
                    return;
                }
            }).error(function (data, status, headers, config) {
                $.bigBox({
                    title: "Error",
                    content: "Unable to save views fields for the selected graph",
                    color: "#C46A69",
                    timeout: 8000,
                    icon: "fa fa-bolt swing animated"
                });
            });
        }
    };

    //---------------- Case Type Email --------------------------------
    $scope.EmailHasOptions = {
        filter: "startswith",
        optionLabel: "-- Select Email --"
    };
    $scope.emailObj = {};

    $scope.Kaizen00020 = {};
    $scope.emailToList = [];
    $scope.CaseTypeEmailList = [];
    $scope.CaseTypeEmail = function () {
        $scope.LoadCM00029Page('CaseTypeEmail');
        $scope.Clear();
        $scope.LoadCaseTypes();
        $scope.LoadEmailSettings();
    };

    $scope.CaseTypeChangedForEmail = function () {
        if (angular.isDefined($scope.CM00029.SelectedCaseType)) {
            $scope.CM00029.TRXTypeID = $scope.CM00029.SelectedCaseType.TRXTypeID;
            //$scope.CM00076.TRXTypeID = $scope.CM00029.SelectedCaseType.TRXTypeID;
            $scope.LoadViews_CaseTypeWithOptions($scope.CM00029.TRXTypeID);
        }
    };

    $scope.ViewChangedForEmail = function () {
        if (angular.isDefined($scope.CM00071.SelectedView)) {
            $scope.CM00071.ViewID = $scope.CM00071.SelectedView.ViewID;
        }
    };

    $scope.LoadEmailSettings = function () {
        $http.get('/CMS_CaseType/GetEmailSettings?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.CaseTypeEmailList = data;
        }).finally(function () {
            $scope.EmailHasOptions = {
                filter: "startswith",
                model: "Kaizen00020.SelectedEmail",
                optionLabel: "-- Select Email --",
                dataTextField: "EmailID",
                dataValueField: "EmailID",
                dataSource: $scope.CaseTypeEmailList,
                value: $scope.Kaizen00020.EmailID
            };
        });
    };

    $scope.EmailChangedForUser = function () {
        if (angular.isDefined($scope.Kaizen00020.SelectedEmail)) {
            $scope.Kaizen00020.EmailID = $scope.Kaizen00020.SelectedEmail.EmailID;
        }
    };

    $scope.SendEmailData = function () {
        var errorMsg = "";
        if (!$scope.CM00029 || $scope.CM00029.TRXTypeID == undefined || $scope.CM00029.TRXTypeID == "")
            errorMsg = "Select case type";
        else if (!$scope.CM00071 || $scope.CM00071.ViewID == undefined || $scope.CM00071.ViewID == "")
            errorMsg = "Select view";
        else if (!$scope.Kaizen00020 || $scope.Kaizen00020.EmailID == undefined || $scope.Kaizen00020.EmailID == "")
            errorMsg = "Select email from";

        if (errorMsg != "") {
            $.bigBox({
                title: "Info",
                content: errorMsg,
                color: "#C46A69",
                timeout: 8000,
                icon: "fa fa-bolt swing animated"
            });
            return;
        }
        if ($scope.emailToList && $scope.emailToList.length <= 0) {
            $.bigBox({
                title: "Info",
                content: "Please enter one or more email Ids to whom email needs to be send",
                color: "#C46A69",
                timeout: 8000,
                icon: "fa fa-bolt swing animated"
            });
            return;
        }

        var toList = $scope.emailToList.map(function (a) { return a.text; });

        $http({
            url: '/CMS_Case/SendEmailData?KaizenPublicKey=' + sessionStorage.PublicKey,
            method: "POST",
            data: {
                TRXTypeID: $scope.CM00029.TRXTypeID, ViewID: $scope.CM00071.ViewID,
                userEmailSetting: $scope.Kaizen00020.SelectedEmail, emailToList: toList,
                subject: $scope.emailObj.subject, message: $scope.emailObj.message
            },
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
                    timeout: 8000,
                    icon: "fa fa-bolt swing animated"
                });
            } else {
                $.bigBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#C46A69",
                    timeout: 8000,
                    icon: "fa fa-bolt swing animated"
                });
                return;
            }
        }).error(function (data, status, headers, config) {
            $.bigBox({
                title: "Error",
                content: "Unable to send email",
                color: "#C46A69",
                timeout: 8000,
                icon: "fa fa-bolt swing animated"
            });
        });
    };

    //------------------ Case Type Equation Fields -------------------
    $scope.CM00080 = {};
    $scope.CaseTypeEquationFields = function () {
        $scope.LoadCM00029Page('CaseTypeEquationFields');
        $scope.Clear();
        $scope.LoadCaseTypes_EquationFields();
        $scope.LoadFieldTypes_EquationField();
        $scope.CM00080.status = 0;
    };

    $scope.LoadCaseTypes_EquationFields = function () {
        $http.get('/CMS_CaseType/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.CaseTypes = data;
        }).finally(function () {

        });
    };

    $scope.LoadStandardFields_EquationField = function (TRXTypeID) {
        $http.get('/CMS_CaseType/GetStandardFieldsByCaseType?KaizenPublicKey=' + sessionStorage.PublicKey,
            { params: { TRXTypeID: TRXTypeID } }).success(function (data) {
                if (data.length >= 0) {
                    if (data.length > 0)
                        $scope.CaseTypeStandardFieldsList = data;
                    else
                        $scope.CaseTypeStandardFieldsList = [];
                }
            });
    };

    $scope.LoadFieldTypes_EquationField = function () {
        $http.get('/CMS_CaseType/FillFieldTypeList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.FieldTypeList = data;
        }).finally(function () {

        });
    };

    $scope.FieldTypeChangedFor_EquationFields = function () {
        if (angular.isDefined($scope.CM00080.FieldTypeID)) {
            $scope.CM00080.FieldEquation = "";
            $scope.LoadFieldCodelist_ByFieldTypeID($scope.CM00080.FieldTypeID);
        }
    };

    $scope.LoadFieldCodelist_ByFieldTypeID = function (FieldTypeID) {
        $http.get('/CMS_CaseType/FillFieldCodeList?KaizenPublicKey=' + sessionStorage.PublicKey,
            { params: { FieldTypeID: FieldTypeID } }).success(function (data) {
                $scope.FunctionCodeList = data;
            }).finally(function () {

            });
    };

    $scope.CaseTypeChangedForEquationFields = function () {
        //alert($scope.SelectedLookUp.SelectedType);
        $scope.CM00080.TRXTypeID = $scope.SelectedLookUp.SelectedType;
        $scope.LoadStandardFields_EquationField($scope.CM00080.TRXTypeID);
        $scope.LoadEquationFields_CaseType($scope.CM00080.TRXTypeID);
    };

    $scope.LoadEquationFields_CaseType = function (TRXTypeID) {
        $http.get('/CMS_CaseType/GetEquationFieldsByCaseType?KaizenPublicKey=' + sessionStorage.PublicKey,
            { params: { TRXTypeID: TRXTypeID } }).success(function (data) {
                if (data.length >= 0) {
                    if (data.length > 0)
                        $scope.CaseTypeEquationFieldsList = data;
                    else
                        $scope.CaseTypeEquationFieldsList = [];
                }
            });
    };

    $scope.CaseTypeStandardFields_Changed = function () {
        if ($scope.CM00080.FieldEquation == undefined)
            $scope.CM00080.FieldEquation = "";

        var id = "fieldEquation";
        var txtToAdd = $scope.SelectedLookUp.SelectedField.FieldName.trim();

        var result = $scope.AddFieldAtPosition(id, txtToAdd);
        $scope.CM00080.FieldEquation = result;
    };

    $scope.FunctionCodeList_Changed = function () {
        if ($scope.CM00080.FieldEquation == undefined)
            $scope.CM00080.FieldEquation = "";

        var id = "fieldEquation";
        var txtToAdd = $scope.SelectedLookUp.SelectedFunction.FunctionDisplay.trim();

        var result = $scope.AddFieldAtPosition(id, txtToAdd);

        $scope.CM00080.FieldEquation = result;
    };

    $scope.AddNewEquationField = function () {
        $scope.CM00080.status = 1;
        if ($scope.CM00080) {
            $http({
                url: '/CMS_CaseType/SaveEquationField?KaizenPublicKey=' + sessionStorage.PublicKey,
                method: "POST",
                data: JSON.stringify($scope.CM00080),
                dataType: "json",
                headers: {
                    'Content-Type': 'application/json; charset=utf-8'
                }
            }).success(function (data) {
                if (data.Status == true) {
                    //$scope.CaseTypeEquationFieldsList.push($scope.CM00080);
                    $scope.CM00080 = {};
                    $scope.CM00080.TRXTypeID = $scope.SelectedLookUp.SelectedType;
                    $scope.CM00080.status = 0;

                    $.bigBox({
                        title: data.Massage,
                        content: data.Description,
                        color: "#739E73",
                        timeout: 8000,
                        icon: "fa fa-bolt swing animated"
                    });

                    $scope.LoadEquationFields_CaseType($scope.CM00080.TRXTypeID);

                } else {
                    $.bigBox({
                        title: data.Massage,
                        content: data.Description,
                        color: "#C46A69",
                        timeout: 8000,
                        icon: "fa fa-bolt swing animated"
                    });
                    return;
                }
            }).error(function (data, status, headers, config) {
                $.bigBox({
                    title: "Error",
                    content: "Unable to save case type field data",
                    color: "#C46A69",
                    timeout: 8000,
                    icon: "fa fa-bolt swing animated"
                });
            });
        }
    };

    $scope.EditEquationField = function (caseTypeField) {
        indx = $scope.CaseTypeEquationFieldsList.indexOf(caseTypeField);
        var obj_extend = jQuery.extend({}, $scope.CaseTypeEquationFieldsList[indx]);
        $scope.CM00080 = obj_extend;
        $scope.SelectedLookUp.SelectedType = $scope.CM00080;
        //var Obj_indx = $scope.functiontofindIndexByKeyValue($scope.CaseTypeEquationFieldsList, "TRXTypeID", $scope.CM00080.TRXTypeID);
        //var obj = $scope.CaseTypeEquationFieldsList[Obj_indx];
        $scope.SelectedLookUp.SelectedType = caseTypeField.TRXTypeID;

        $scope.LoadStandardFields_EquationField(caseTypeField.TRXTypeID);
        $scope.LoadFieldCodelist_ByFieldTypeID(caseTypeField.FieldTypeID);

        //if ($scope.CM00080.status == 0 || angular.isUndefined($scope.CM00080.status)) {
        $scope.CM00080.status = 2;
        //}
    };

    $scope.UpdateEquationField = function () {
        if ($scope.CM00080.status == 0 || angular.isUndefined($scope.CM00080.status)) {
            $scope.CM00080.status = 2;
        }
        $scope.CaseTypeEquationFieldsList.splice(indx, 1, $scope.CM00080);

        if ($scope.CM00080) {
            //if ($scope.SelectedFieldType && $scope.SelectedFieldType.FieldTypeID)
            //    $scope.CM00080.FieldTypeID = $scope.SelectedFieldType.FieldTypeID;

            $http({
                url: '/CMS_CaseType/UpdateEquationField?KaizenPublicKey=' + sessionStorage.PublicKey,
                method: "POST",
                data: $scope.CM00080,
                dataType: "json",
                headers: {
                    'Content-Type': 'application/json; charset=utf-8'
                }
            }).success(function (data) {
                if (data.Status == true) {
                    $scope.CM00080 = {};
                    $scope.CM00080.status = 0;

                    $.bigBox({
                        title: data.Massage,
                        content: data.Description,
                        color: "#739E73",
                        timeout: 8000,
                        icon: "fa fa-bolt swing animated"
                    });
                } else {
                    $.bigBox({
                        title: data.Massage,
                        content: data.Description,
                        color: "#C46A69",
                        timeout: 8000,
                        icon: "fa fa-bolt swing animated"
                    });
                    return;
                }
            }).error(function (data, status, headers, config) { });
        }
    };

    $scope.RemoveCaseTypeEquationField = function (caseTypeField, index) {

        $.SmartMessageBox({
            title: "Delete Record",
            content: "Are you sure?",
            buttons: '[No][Yes]'
        }, function (ButtonPressed) {
            if (ButtonPressed === "Yes") {

                $http({
                    url: '/CMS_CaseType/DeleteEquationField?KaizenPublicKey=' + sessionStorage.PublicKey,
                    method: "POST",
                    data: JSON.stringify(caseTypeField),
                    dataType: "json",
                    headers: {
                        'Content-Type': 'application/json; charset=utf-8'
                    }
                }).success(function (data) {
                    if (data.Status == true) {
                        if (caseTypeField.status == 1)
                            $scope.CaseTypeEquationFieldsList.splice(index, 1);
                        else
                            caseTypeField.status = 3;
                        //Notify(data.Massage, 'bottom-right', '5000', 'success', 'fa-check', true);
                        $.bigBox({
                            title: data.Massage,
                            content: data.Description,
                            color: "#C46A69",
                            timeout: 8000,
                            icon: "fa fa-bolt swing animated"
                        });
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
    };

    $scope.CancelUpdateEquationField = function () {
        $scope.CM00080.status = 1;
        $scope.CM00080.FieldName = "";
        $scope.CM00080.FieldDisplay = "";
        $scope.CM00080.FieldEquation = "";
    };

    //------------------ Case Type Equation Fields By View -------------------
    $scope.CM00081 = {};
    $scope.CaseTypeEquationFieldsByView = function () {
        $scope.LoadCM00029Page('CaseTypeEquationFieldsByView');
        $scope.Clear();
        $scope.LoadCaseTypes_EquationFieldsByView();
        $scope.LoadFieldTypes_EquationFieldByView();
        $scope.CM00081.status = 0;
    };

    $scope.LoadCaseTypes_EquationFieldsByView = function () {
        $http.get('/CMS_CaseType/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.CaseTypes = data;
        }).finally(function () {

        });
    };

    $scope.LoadStandardFields_EquationFieldByView = function (TRXTypeID, ViewID) {
        $http.get('/CMS_CaseType/GetStandardFieldsByCaseTypeAndView?KaizenPublicKey=' + sessionStorage.PublicKey,
            { params: { TRXTypeID: TRXTypeID, ViewID: ViewID } }).success(function (data) {
                if (data.length >= 0) {
                    if (data.length > 0)
                        $scope.CaseTypeStandardFieldsList = data;
                    else
                        $scope.CaseTypeStandardFieldsList = [];
                }
            });
    };

    $scope.LoadFieldTypes_EquationFieldByView = function () {
        $http.get('/CMS_CaseType/FillFieldTypeList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.FieldTypeList = data;
        }).finally(function () {

        });
    };

    $scope.FieldTypeChangedFor_EquationFieldsByView = function () {
        if (angular.isDefined($scope.CM00081.FieldTypeID)) {
            $scope.LoadFieldCodelist_ByFieldTypeID($scope.CM00081.FieldTypeID);
        }
    };

    $scope.LoadFieldCodelist_ByFieldTypeIDByView = function (FieldTypeID) {
        $http.get('/CMS_CaseType/FillFieldCodeList?KaizenPublicKey=' + sessionStorage.PublicKey,
            { params: { FieldTypeID: FieldTypeID } }).success(function (data) {
                $scope.FunctionCodeList = data;
            }).finally(function () {

            });
    };

    $scope.CaseTypeChangedForEquationFieldsByView = function () {
        $scope.CM00081.TRXTypeID = $scope.SelectedLookUp.SelectedType;
        $scope.GetViews_EquationFieldsByCaseType($scope.CM00081.TRXTypeID);
    };

    $scope.GetViews_EquationFieldsByCaseType = function (TRXTypeID) {
        $http.get('/CMS_CaseType/GetViews_EquationFieldsByCaseType?KaizenPublicKey=' + sessionStorage.PublicKey,
            { params: { TRXTypeID: TRXTypeID } }).success(function (data) {
                $scope.CaseTypeViews = data;
            }).finally(function () {
            });
    };

    $scope.ViewChangedForEquationFields = function () {
        $scope.CM00081.ViewID = $scope.SelectedLookUp.SelectedView;
        $scope.LoadStandardFields_EquationFieldByView($scope.CM00081.TRXTypeID, $scope.CM00081.ViewID);
        $scope.LoadEquationFields_CaseTypeByView($scope.CM00081.TRXTypeID, $scope.CM00081.ViewID);
    };

    $scope.LoadEquationFields_CaseTypeByView = function (TRXTypeID, ViewID) {
        $http.get('/CMS_CaseType/GetEquationFieldsByCaseTypeAndView?KaizenPublicKey=' + sessionStorage.PublicKey,
            { params: { TRXTypeID: TRXTypeID, ViewID: ViewID } }).success(function (data) {
                if (data.length >= 0) {
                    if (data.length > 0)
                        $scope.CaseTypeEquationFieldsList = data;
                    else
                        $scope.CaseTypeEquationFieldsList = [];
                }
            });
    };

    $scope.CaseTypeStandardFields_ChangedByView = function () {
        if ($scope.CM00081.FieldEquation == undefined)
            $scope.CM00081.FieldEquation = "";

        var id = "fieldEquation";
        var txtToAdd = $scope.SelectedLookUp.SelectedField.FieldName.trim();

        var result = $scope.AddFieldAtPosition(id, txtToAdd);
        $scope.CM00081.FieldEquation = result;
    };

    $scope.FunctionCodeList_ChangedByView = function () {
        if ($scope.CM00081.FieldEquation == undefined)
            $scope.CM00081.FieldEquation = "";

        var id = "fieldEquation";
        var txtToAdd = $scope.SelectedLookUp.SelectedFunction.FunctionDisplay.trim();

        var result = $scope.AddFieldAtPosition(id, txtToAdd);
        $scope.CM00081.FieldEquation = result;
    };

    $scope.AddFieldAtPosition = function (id, txtToAdd) {
        var $txt = $("#" + id);
        var caretPos = $txt[0].selectionStart;
        var textAreaTxt = $txt.val();
        var result = textAreaTxt.substring(0, caretPos) + " " + txtToAdd + " " + textAreaTxt.substring(caretPos);
        //$scope.CM00081.FieldEquation = textAreaTxt.substring(0, caretPos) + " " + txtToAdd + " " + textAreaTxt.substring(caretPos);
        return result;
    };

    $scope.AddNewEquationFieldByView = function () {
        $scope.CM00081.status = 1;
        if ($scope.CM00081) {
            $http({
                url: '/CMS_CaseType/SaveEquationFieldByView?KaizenPublicKey=' + sessionStorage.PublicKey,
                method: "POST",
                data: JSON.stringify($scope.CM00081),
                dataType: "json",
                headers: {
                    'Content-Type': 'application/json; charset=utf-8'
                }
            }).success(function (data) {
                if (data.Status == true) {
                    //$scope.CaseTypeEquationFieldsList.push($scope.CM00081);
                    $scope.CM00081 = {};
                    $scope.CM00081.TRXTypeID = $scope.SelectedLookUp.SelectedType;
                    $scope.CM00081.ViewID = $scope.SelectedLookUp.SelectedView;
                    $scope.CM00081.status = 0;

                    $.bigBox({
                        title: data.Massage,
                        content: data.Description,
                        color: "#739E73",
                        timeout: 8000,
                        icon: "fa fa-bolt swing animated"
                    });

                    $scope.LoadEquationFields_CaseTypeByView($scope.CM00081.TRXTypeID, $scope.CM00081.ViewID);

                } else {
                    $.bigBox({
                        title: data.Massage,
                        content: data.Description,
                        color: "#C46A69",
                        timeout: 8000,
                        icon: "fa fa-bolt swing animated"
                    });
                    return;
                }
            }).error(function (data, status, headers, config) {
                $.bigBox({
                    title: "Error",
                    content: "Unable to save case type field data",
                    color: "#C46A69",
                    timeout: 8000,
                    icon: "fa fa-bolt swing animated"
                });
            });
        }
    };

    $scope.EditEquationFieldByView = function (caseTypeField) {

        indx = $scope.CaseTypeEquationFieldsList.indexOf(caseTypeField);
        var obj_extend = jQuery.extend({}, $scope.CaseTypeEquationFieldsList[indx]);
        $scope.CM00081 = obj_extend;
        var Obj_indx = $scope.functiontofindIndexByKeyValue($scope.CaseTypeEquationFieldsList, "TRXTypeID", $scope.CM00081.TRXTypeID);
        var obj = $scope.CaseTypeEquationFieldsList[Obj_indx];
        $scope.SelectedLookUp.SelectedType = caseTypeField.TRXTypeID;
        $scope.SelectedLookUp.SelectedView = caseTypeField.ViewID;
        //if ($scope.CM00080.status == 0 || angular.isUndefined($scope.CM00080.status)) {
        $scope.LoadStandardFields_EquationFieldByView(caseTypeField.TRXTypeID, caseTypeField.ViewID);
        $scope.LoadFieldCodelist_ByFieldTypeID(caseTypeField.FieldTypeID);

        $scope.CM00081.status = 2;
    };

    $scope.UpdateEquationFieldByView = function () {
        if ($scope.CM00081.status == 0 || angular.isUndefined($scope.CM00081.status)) {
            $scope.CM00081.status = 2;
        }
        $scope.CaseTypeEquationFieldsList.splice(indx, 1, $scope.CM00081);

        if ($scope.CM00081) {
            //if ($scope.SelectedFieldType && $scope.SelectedFieldType.FieldTypeID)
            //    $scope.CM00081.FieldTypeID = $scope.SelectedFieldType.FieldTypeID;

            $http({
                url: '/CMS_CaseType/UpdateEquationFieldByView?KaizenPublicKey=' + sessionStorage.PublicKey,
                method: "POST",
                data: $scope.CM00081,
                dataType: "json",
                headers: {
                    'Content-Type': 'application/json; charset=utf-8'
                }
            }).success(function (data) {
                if (data.Status == true) {
                    $scope.CM00081 = {};
                    $scope.CM00081.status = 0;

                    $.bigBox({
                        title: data.Massage,
                        content: data.Description,
                        color: "#739E73",
                        timeout: 8000,
                        icon: "fa fa-bolt swing animated"
                    });
                } else {
                    $.bigBox({
                        title: data.Massage,
                        content: data.Description,
                        color: "#C46A69",
                        timeout: 8000,
                        icon: "fa fa-bolt swing animated"
                    });
                    return;
                }
            }).error(function (data, status, headers, config) { });
        }
    };

    $scope.RemoveCaseTypeEquationFieldByView = function (caseTypeField, index) {

        $.SmartMessageBox({
            title: "Delete Record",
            content: "Are you sure?",
            buttons: '[No][Yes]'
        }, function (ButtonPressed) {
            if (ButtonPressed === "Yes") {

                $http({
                    url: '/CMS_CaseType/DeleteEquationFieldByView?KaizenPublicKey=' + sessionStorage.PublicKey,
                    method: "POST",
                    data: JSON.stringify(caseTypeField),
                    dataType: "json",
                    headers: {
                        'Content-Type': 'application/json; charset=utf-8'
                    }
                }).success(function (data) {
                    if (data.Status == true) {
                        if (caseTypeField.status == 1)
                            $scope.CaseTypeEquationFieldsList.splice(index, 1);
                        else
                            caseTypeField.status = 3;
                        //Notify(data.Massage, 'bottom-right', '5000', 'success', 'fa-check', true);
                        $.bigBox({
                            title: data.Massage,
                            content: data.Description,
                            color: "#C46A69",
                            timeout: 8000,
                            icon: "fa fa-bolt swing animated"
                        });
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
    };

    $scope.CancelUpdateEquationFieldByView = function () {
        $scope.CM00081.status = 1;
        $scope.CM00081.FieldName = "";
        $scope.CM00081.FieldDisplay = "";
        $scope.CM00081.FieldEquation = "";
    };

    //------------------ Case Type Show Graph -------------------
    $scope.CaseTypeShowGraph = function () {
        $scope.LoadCM00029Page('CaseTypeShowGraph');
        $scope.Clear();
        //$scope.LoadGraphs();
        $scope.LoadCaseTypes_ShowGraphs();
        //$scope.LoadGraphTypes();
        //$scope.LoadCaseTypes_EquationFieldsByView();
        //$scope.LoadFieldTypes_EquationFieldByView();
        //$scope.CM00081.status = 0;
    };
    $scope.Graphs = [];
    $scope.GraphTypes = [];

    $scope.LoadGraphs_ByCaseTypeView = function (ViewID) {
        $http.get('/CMS_CaseType/GetGraphs?KaizenPublicKey=' + sessionStorage.PublicKey,
                { params: { ViewID: ViewID } }).success(function (data) {
                    if (data.length > 0) {
                        $scope.Graphs = data;
                    }
                }).error(function (data, status, headers, config) {
                    $.bigBox({
                        title: "Error",
                        content: "Unable to get graph data",
                        color: "#C46A69",
                        timeout: 8000,
                        icon: "fa fa-bolt swing animated"
                    });
                });
    };

    $scope.LoadGraphTypes = function () {
        $http.get('/CMS_CaseType/GetGraphTypes?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            if (data.length > 0) {
                $scope.GraphTypes = data;
            }
        }).error(function (data, status, headers, config) {
            $.bigBox({
                title: "Error",
                content: "Unable to get graph data",
                color: "#C46A69",
                timeout: 8000,
                icon: "fa fa-bolt swing animated"
            });
        });
    };

    $scope.SetGraphData = function (GraphID, GraphTypeName, Chart) {
        var graphData = [];
        $http.get('/CMS_CaseType/GraphData?KaizenPublicKey=' + sessionStorage.PublicKey,
            { params: { GraphID: GraphID, GraphTypeName: GraphTypeName } }).success(function (data) {
                if (data) {
                    graphData = data;
                    Chart.categories = data.Categories;
                    Chart.title = data.Title;
                    Chart.LoadSeries(data.Series);
                    if (Chart.type.toLowerCase() === "pie" || Chart.type.toLowerCase() === "donut" ||
                        Chart.type.toLowerCase() === "funnel") {
                        Chart.categoryAxis = {};
                    }

                    Chart.Load();
                }
            }).error(function (data, status, headers, config) {
                $.bigBox({
                    title: "Error",
                    content: "Unable to get graph data",
                    color: "#C46A69",
                    timeout: 8000,
                    icon: "fa fa-bolt swing animated"
                });
            });

        return graphData;
    };
    $scope.GraphTypeName = "";
    $scope.GraphIdChanged = function () {
        var graph;
        if ($scope.SelectedLookUp.SelectedGraph) {
            graph = $scope.SelectedLookUp.SelectedGraph;
            var Chart = new KendoGraph();
            Chart.divId = "caseTypeChart";
            Chart.serviceURL = "/CMS_CaseType/GraphSettings?KaizenPublicKey=" + sessionStorage.PublicKey;
            Chart.type = graph.CM00040.GraphTypeName;
            //barChart.dataURL = "/CMS_CaseType/GraphData?KaizenPublicKey=" + sessionStorage.PublicKey + "";
            var data = $scope.SetGraphData(graph.GraphID, graph.CM00040.GraphTypeName, Chart);
            $scope.GraphTypeName = graph.CM00040.GraphTypeName;
        }
    };

    $scope.LoadCaseTypes_ShowGraphs = function () {
        $http.get('/CMS_CaseType/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.CaseTypes = data;
        }).finally(function () {

        });
    };

    $scope.CaseTypeChangedToShowGraph = function () {
        $scope.LoadCaseTypeViews_ShowGraphs($scope.SelectedLookUp.SelectedType);
    };

    $scope.LoadCaseTypeViews_ShowGraphs = function (TRXTypeID) {
        $http.get('/CMS_CaseType/GetViewsByCaseType?KaizenPublicKey=' + sessionStorage.PublicKey,
            { params: { TRXTypeID: TRXTypeID } }).success(function (data) {
                $scope.CaseTypeViews = data;
            }).finally(function () {
            });
    };

    $scope.ViewChangedToShowGraph = function () {
        $scope.LoadGraphs_ByCaseTypeView($scope.SelectedLookUp.SelectedView);
    };

    //------------------ Case Type Lookup Field -------------------
    $scope.CM00050 = {};
    $scope.CaseTypeLookupField = function () {
        $scope.LoadCM00029Page('CaseTypeLookupField');
        $scope.Clear();
        $scope.LoadCaseTypes_LookupField();
        $scope.CM00050.status = 0;
    };

    $scope.LoadCaseTypes_LookupField = function () {
        $http.get('/CMS_CaseType/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.CaseTypes = data;
        }).finally(function () {
        });
    };

    $scope.CaseTypeChangedForLookupField = function () {
        if (angular.isDefined($scope.SelectedLookUp.SelectedType)) {
            $scope.CM00050.TRXTypeID = $scope.SelectedLookUp.SelectedType;
            $scope.GetFieldNames_ByCaseType($scope.CM00050.TRXTypeID);
        }
    };

    $scope.GetFieldNames_ByCaseType = function (TRXTypeID) {
        $http.get('/CMS_CaseType/GetFieldNames?KaizenPublicKey=' + sessionStorage.PublicKey,
        { params: { TRXTypeID: TRXTypeID } }).success(function (data) {
            $scope.FieldNameList = data;
            $scope.LoadLookupFields_ByCaseTypeField($scope.CM00050.TRXTypeID);
        }).finally(function () {
        });
    };

    $scope.FieldNameChangedForLookup = function () {
        if (angular.isDefined($scope.SelectedLookUp.SelectedField)) {
            $scope.CM00050.FieldName = $scope.SelectedLookUp.SelectedField;
        }
    };

    $scope.LoadLookupFields_ByCaseTypeField = function (TRXTypeID) {
        $http.get('/CMS_CaseType/GetCaseTypeLookupFields?KaizenPublicKey=' + sessionStorage.PublicKey,
            { params: { TRXTypeID: TRXTypeID} }).success(function (data) {
                if (data.length >= 0) {
                    if (data.length > 0)
                        $scope.CaseTypeLookupFieldsList = data;
                    else
                        $scope.CaseTypeLookupFieldsList = [];
                }
            });
    };
    $scope.AddLookupField = function () {
        $scope.CM00050.status = 1;
        if ($scope.CM00050) {
            $http({
                url: '/CMS_CaseType/SaveLookupField?KaizenPublicKey=' + sessionStorage.PublicKey,
                method: "POST",
                data: JSON.stringify($scope.CM00050),
                dataType: "json",
                headers: {
                    'Content-Type': 'application/json; charset=utf-8'
                }
            }).success(function (data) {
                if (data.Status == true) {
                    //$scope.CaseTypeLookupFieldsList.push($scope.CM00050);
                    $scope.CM00050 = {};
                    $scope.CM00050.TRXTypeID = $scope.SelectedLookUp.SelectedType;
                    $scope.CM00050.FieldName = $scope.SelectedLookUp.SelectedField;
                    $scope.CM00050.status = 0;

                    $.bigBox({
                        title: data.Massage,
                        content: data.Description,
                        color: "#739E73",
                        timeout: 8000,
                        icon: "fa fa-bolt swing animated"
                    });

                    $scope.LoadLookupFields_ByCaseTypeField($scope.CM00050.TRXTypeID, $scope.CM00050.FieldName);

                } else {
                    $.bigBox({
                        title: data.Massage,
                        content: data.Description,
                        color: "#C46A69",
                        timeout: 8000,
                        icon: "fa fa-bolt swing animated"
                    });
                    return;
                }
            }).error(function (data, status, headers, config) {
                $.bigBox({
                    title: "Error",
                    content: "Unable to save case type field data",
                    color: "#C46A69",
                    timeout: 8000,
                    icon: "fa fa-bolt swing animated"
                });
            });
        }
    };

    $scope.EditLookupField = function (lookupField) {

        indx = $scope.CaseTypeLookupFieldsList.indexOf(lookupField);
        var obj_extend = jQuery.extend({}, $scope.CaseTypeLookupFieldsList[indx]);
        $scope.CM00050 = obj_extend;
        $scope.SelectedLookUp.SelectedType = lookupField.TRXTypeID;
        $scope.SelectedLookUp.SelectedField = lookupField.FieldName;

        $scope.CM00050.status = 2;
    };

    $scope.UpdateLookupField = function () {
        if ($scope.CM00050.status == 0 || angular.isUndefined($scope.CM00050.status)) {
            $scope.CM00050.status = 2;
        }

        if ($scope.CM00050) {
            //if ($scope.SelectedFieldType && $scope.SelectedFieldType.FieldTypeID)
            //    $scope.CM00050.FieldTypeID = $scope.SelectedFieldType.FieldTypeID;

            $http({
                url: '/CMS_CaseType/UpdateLookupField?KaizenPublicKey=' + sessionStorage.PublicKey,
                method: "POST",
                data: $scope.CM00050,
                dataType: "json",
                headers: {
                    'Content-Type': 'application/json; charset=utf-8'
                }
            }).success(function (data) {
                var objCM00050 = jQuery.extend({}, $scope.CM00050);
                $scope.CaseTypeLookupFieldsList.splice(indx, 1, objCM00050);

                if (data.Status == true) {
                    $scope.CM00050.LookupFieldValue = "";
                    $scope.CM00050.status = 0;

                    $.bigBox({
                        title: data.Massage,
                        content: data.Description,
                        color: "#739E73",
                        timeout: 8000,
                        icon: "fa fa-bolt swing animated"
                    });
                } else {
                    $.bigBox({
                        title: data.Massage,
                        content: data.Description,
                        color: "#C46A69",
                        timeout: 8000,
                        icon: "fa fa-bolt swing animated"
                    });
                    return;
                }
            }).error(function (data, status, headers, config) { });
        }
    };

    $scope.RemoveLookupField = function (lookupField, index) {

        $.SmartMessageBox({
            title: "Delete Record",
            content: "Are you sure?",
            buttons: '[No][Yes]'
        }, function (ButtonPressed) {
            if (ButtonPressed === "Yes") {

                $http({
                    url: '/CMS_CaseType/DeleteLookupField?KaizenPublicKey=' + sessionStorage.PublicKey,
                    method: "POST",
                    data: JSON.stringify(lookupField),
                    dataType: "json",
                    headers: {
                        'Content-Type': 'application/json; charset=utf-8'
                    }
                }).success(function (data) {
                    if (data.Status == true) {
                        //if (lookupField.status == 1)
                        $scope.CaseTypeLookupFieldsList.splice(index, 1);
                        //else
                        //    lookupField.status = 3;
                        //Notify(data.Massage, 'bottom-right', '5000', 'success', 'fa-check', true);
                        $.bigBox({
                            title: data.Massage,
                            content: data.Description,
                            color: "#C46A69",
                            timeout: 8000,
                            icon: "fa fa-bolt swing animated"
                        });
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
    };

    $scope.CancelLookupField = function () {
        $scope.CM00050.status = 1;
        $scope.CM00050.LookupFieldValue = "";
    };

    //------------------ Case Type Field Order -------------------
    $scope.CaseTypeFieldOrder = function () {
        $scope.LoadCM00029Page('CaseTypeFieldOrder');
        $scope.Clear();
        $scope.LoadCaseTypes_FieldOrder();
        $scope.CM00074.status = 0;
    };

    $scope.LoadCaseTypes_FieldOrder = function () {
        $http.get('/CMS_CaseType/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.CaseTypes = data;
        }).finally(function () {
        });
    };

    $scope.CaseTypeChangedForFieldOrder = function () {
        if (angular.isDefined($scope.SelectedLookUp.SelectedType)) {
            $scope.CM00074.TRXTypeID = $scope.SelectedLookUp.SelectedType;
            $scope.LoadStandardFields_FieldOrder($scope.CM00074.TRXTypeID);
        }
    };

    $scope.LoadStandardFields_FieldOrder = function (TRXTypeID) {
        $http.get('/CMS_CaseType/GetStandardFieldsByCaseType?KaizenPublicKey=' + sessionStorage.PublicKey,
            { params: { TRXTypeID: TRXTypeID } }).success(function (data) {
                if (data.length >= 0) {
                    if (data.length > 0)
                        $scope.CaseTypeStandardFieldsList = data;
                    else
                        $scope.CaseTypeStandardFieldsList = [];
                }
            });
    };

    $scope.UpdateStandardFieldOrder = function () {
        //var selView = $scope.CM00071.SelectedView;
        $scope.tt = [];
        $scope.CaseTypeStandardFieldsList.forEach(function (eleStatusRole, index) {
            var tttemp = {
                FieldDisplay: eleStatusRole.FieldDisplay,
                FieldName: eleStatusRole.FieldName,
                //FieldEquation: eleStatusRole.FieldEquation,
                FieldOrder: eleStatusRole.FieldOrder,
                IsActive: eleStatusRole.IsActive,
                FieldTypeID: eleStatusRole.FieldTypeID,
                FieldWidth: eleStatusRole.FieldWidth,
                //IsEmailable: eleStatusRole.IsEmailable,
                //IsFilterable: eleStatusRole.IsFilterable,
                //Islockable: eleStatusRole.Islockable,
                //Islocked: eleStatusRole.Islocked,
                //IsPrintable: eleStatusRole.IsPrintable,
                //IsRequired: eleStatusRole.IsRequired,
                //IsSortable: eleStatusRole.IsSortable
                TRXTypeID: eleStatusRole.TRXTypeID
            }
            $scope.tt.push(tttemp);
        });
        $http.post('/CMS_CaseType/UpdateStandardFieldOrder', {
            'CM00074List': $scope.tt, 'callNumber': 1, 'KaizenPublicKey': sessionStorage.PublicKey
        }).success(function (data) {
            if (data.Status == true) {

                $scope.tt = [];
                $scope.CaseTypeStandardFieldsList.forEach(function (eleStatusRole, index) {
                    var tttemp = {
                        //FieldDisplay: eleStatusRole.FieldDisplay,
                        FieldName: eleStatusRole.FieldName,
                        FieldEquation: eleStatusRole.FieldEquation,
                        //FieldOrder: eleStatusRole.FieldOrder,
                        //IsActive: eleStatusRole.IsActive,
                        IsEmailable: eleStatusRole.IsEmailable,
                        IsFilterable: eleStatusRole.IsFilterable,
                        Islockable: eleStatusRole.Islockable,
                        Islocked: eleStatusRole.Islocked,
                        IsPrintable: eleStatusRole.IsPrintable,
                        IsRequired: eleStatusRole.IsRequired,
                        IsSortable: eleStatusRole.IsSortable,
                        TRXTypeID: eleStatusRole.TRXTypeID
                    }
                    $scope.tt.push(tttemp);
                });
                $http.post('/CMS_CaseType/UpdateStandardFieldOrder', {
                    'CM00074List': $scope.tt, 'callNumber': 2, 'KaizenPublicKey': sessionStorage.PublicKey
                }).success(function (data) {
                    if (data.Status == true) {
                        $.bigBox({
                            title: data.Massage,
                            content: data.Description,
                            color: "#739E73",
                            timeout: 8000,
                            icon: "fa fa-bolt swing animated"
                        });

                    }
                    else {
                        //alert('ssssss')
                    }
                }).error(function (data, status, headers, config) {
                    alert(data);
                });
            }
            else {
                //alert('ssssss')
            }
        }).error(function (data, status, headers, config) {
            alert(data);
        });
    };

    //------------------ View Field Summary -------------------
    $scope.CM00082 = {};
    $scope.CM00083 = {};
    $scope.ViewFieldSummary = function () {
        $scope.LoadCM00029Page('ViewFieldSummary');
        $scope.Clear();
        $scope.LoadCaseTypes_ViewFieldSummary();
        $scope.LoadGraphTypes();
        $scope.CM00074.status = 0;
    };

    $scope.LoadCaseTypes_ViewFieldSummary = function () {
        $http.get('/CMS_CaseType/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.CaseTypes = data;
        }).finally(function () {

        });
    };

    $scope.CaseTypeChangedForViewFieldSummary = function () {
        if (angular.isDefined($scope.SelectedLookUp.SelectedType)) {
            $scope.CaseTypeGraphList = [];
            $scope.LoadViews_CaseTypeGraph($scope.SelectedLookUp.SelectedType);
            $scope.FieldSummaryListByView = [];
            $scope.FieldSummaryListByView_GraphSummary = [];
            $scope.LoadAllFields_ByViewField($scope.SelectedLookUp.SelectedType);
            $scope.LoadAllFields_ByViewFieldSummary($scope.SelectedLookUp.SelectedType);
        }
    };

    $scope.ViewChangedForFieldSummary = function () {
        if (angular.isDefined($scope.SelectedLookUp.SelectedView)) {
            $scope.CM00082.ViewID = $scope.SelectedLookUp.SelectedView;
            $scope.CM00083.ViewID = $scope.SelectedLookUp.SelectedView;
            $scope.LoadGraphViewData_FieldSummary($scope.SelectedLookUp.SelectedView);
            $scope.LoadViewFieldSummaryTypes();
            $scope.LoadGraphViewsFieldSummary($scope.SelectedLookUp.SelectedView);
        }
    };

    $scope.GraphViewChangedForFieldSummary = function () {
        if (angular.isDefined($scope.SelectedLookUp.SelectedGraph)) {
            $scope.CM00082.GraphID = $scope.SelectedLookUp.SelectedGraph;
            $scope.CM00083.GraphID = $scope.SelectedLookUp.SelectedGraph;
        }
    };
    $scope.FieldSummaryListByView = [];
    $scope.LoadGraphViewsFieldSummary = function (ViewID) {
        $http.get('/CMS_CaseType/GetCM00082?KaizenPublicKey=' + sessionStorage.PublicKey,
            { params: { ViewID: ViewID } }).success(function (data) {
                $scope.CaseTypeGraphViewsFields = data;

                $scope.FieldSummaryListByView.forEach(function (ele, index) {
                    ele.isSelected = false;
                    $scope.CaseTypeGraphViewsFields.forEach(function (eleSelected, index) {
                        if (ele.FieldName.trim() === eleSelected.FieldName.trim()) {
                            ele.isSelected = true;
                        }
                    });
                });

                $http.get('/CMS_CaseType/GetCM00083?KaizenPublicKey=' + sessionStorage.PublicKey,
                { params: { ViewID: ViewID } }).success(function (data) {
                    $scope.CaseTypeGraphViewsFields_Summary = data;

                    $scope.FieldSummaryListByView_GraphSummary.forEach(function (ele, index) {
                        ele.isSelected = false;
                        $scope.CaseTypeGraphViewsFields_Summary.forEach(function (eleSelected, index) {
                            if (ele.FieldName.trim() === eleSelected.FieldName.trim()) {
                                ele.isSelected = true;
                                ele.SummaryType = {};
                                ele.SummaryType = eleSelected.SummeryTypeID;
                            }
                        });
                    });
                });
            });
    };

    $scope.LoadViewFieldSummaryTypes = function () {
        $http.get('/CMS_CaseType/GetSummaryTypes?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.SummaryTypeList = data;
        }).finally(function () {
        });
    };
    $scope.CM00047 = {};

    $scope.ViewFieldSummaryTypeChanged = function (field) {
        if (angular.isDefined(field) && field.SummaryType != undefined) {
            //$scope.CM00047.SummeryTypeID = field.SummaryType.SummeryTypeID;
            field.SummeryTypeID = field.SummaryType;
        }
    };
    $scope.GraphViewDataHasOptions = {
        optionLabel: "-- Graph --"
    };
    $scope.LoadGraphViewData_FieldSummary = function (ViewID) {
        $http.get('/CMS_CaseType/GetGraphData?KaizenPublicKey=' + sessionStorage.PublicKey,
           { params: { ViewID: ViewID } }).success(function (data) {
               $scope.CaseTypeGraphList = data;
           }).finally(function () {

           });
    }

    $scope.LoadAllFields_ByViewField = function (TRXTypeID) {
        $http.get('/CMS_CaseType/GetAllFieldsByCaseType?KaizenPublicKey=' + sessionStorage.PublicKey,
            { params: { TRXTypeID: TRXTypeID } }).success(function (data) {
                if (data.length >= 0) {
                    if (data.length > 0) {
                        $scope.FieldSummaryListByView = data;
                    } else {
                        $scope.FieldSummaryListByView = [];
                    }
                }
            });
    };

    $scope.LoadAllFields_ByViewFieldSummary = function (TRXTypeID) {
        $http.get('/CMS_CaseType/GetAllFieldsByCaseType?KaizenPublicKey=' + sessionStorage.PublicKey,
            { params: { TRXTypeID: TRXTypeID } }).success(function (data) {
                if (data.length >= 0) {
                    if (data.length > 0) {
                        $scope.FieldSummaryListByView_GraphSummary = data;

                    } else {
                        $scope.FieldSummaryListByView_GraphSummary = [];
                    }
                }
            });
    };

    $scope.AddOrDeleteCM00082 = function (viewsFieldsObj) {

        if (viewsFieldsObj) {
            if ($scope.SelectedLookUp == undefined || $scope.SelectedLookUp.SelectedView == undefined) {
                $.bigBox({
                    title: "Error",
                    content: "Please select view",
                    color: "#C46A69",
                    timeout: 8000,
                    icon: "fa fa-bolt swing animated"
                });
                viewsFieldsObj.isSelected = !viewsFieldsObj.isSelected;
                return;
            }

            $scope.CM00082 = viewsFieldsObj;
            $scope.CM00082.ViewID = $scope.SelectedLookUp.SelectedView;

            var urlToUpdate = "";

            if (viewsFieldsObj.isSelected) {
                urlToUpdate = "/CMS_CaseType/AddCM00082?KaizenPublicKey=" + sessionStorage.PublicKey;
            } else {
                urlToUpdate = "/CMS_CaseType/DeleteCM00082?KaizenPublicKey=" + sessionStorage.PublicKey;
            }

            $http({
                url: urlToUpdate,
                method: "POST",
                data: JSON.stringify($scope.CM00082),
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
                        timeout: 8000,
                        icon: "fa fa-bolt swing animated"
                    });
                } else {
                    $.bigBox({
                        title: data.Massage,
                        content: data.Description,
                        color: "#C46A69",
                        timeout: 8000,
                        icon: "fa fa-bolt swing animated"
                    });
                    return;
                }
            }).error(function (data, status, headers, config) {
                $.bigBox({
                    title: "Error",
                    content: "Unable to save views fields for the selected graph",
                    color: "#C46A69",
                    timeout: 8000,
                    icon: "fa fa-bolt swing animated"
                });
            });
        }
    };

    $scope.UpdateGraphViewsFieldSummary = function (tabName) {
        if ($scope.SelectedTabName === tabName) {
            $scope.UpdateCM00082();
        }
        else if ($scope.SelectedTabName === tabName) {
            $scope.UpdateCM00083();
        }
    };

    $scope.UpdateCM00082 = function () {
        //var selView = $scope.CM00071.SelectedView;
        if ($scope.SelectedLookUp && $scope.SelectedLookUp.SelectedView) {
            var selectedViewsFields = [];
            $scope.FieldSummaryListByView.forEach(function (ele, index) {
                if (ele.isSelected === true) {
                    ele.ViewID = $scope.SelectedLookUp.SelectedView;
                    selectedViewsFields.push(ele);
                }
            });
            if (selectedViewsFields.length > 0) {
                $http({
                    url: '/CMS_CaseType/UpdateCM00082?KaizenPublicKey=' + sessionStorage.PublicKey,
                    method: "POST",
                    data: selectedViewsFields,
                    dataType: "json",
                    headers: {
                        'Content-Type': 'application/json; charset=utf-8'
                    }
                }).success(function (data) {
                    if (data.Status == true) {
                        //$scope.CM00073 = {};
                        $scope.CM00073.status = 0;
                        $.bigBox({
                            title: data.Massage,
                            content: data.Description,
                            color: "#739E73",
                            timeout: 8000,
                            icon: "fa fa-bolt swing animated"
                        });
                    } else {
                        $.bigBox({
                            title: data.Massage,
                            content: data.Description,
                            color: "#C46A69",
                            timeout: 8000,
                            icon: "fa fa-bolt swing animated"
                        });
                        return;
                    }
                }).error(function (data, status, headers, config) {
                    $.bigBox({
                        title: "Error",
                        content: "Unable to update graph views fields",
                        color: "#C46A69",
                        timeout: 8000,
                        icon: "fa fa-bolt swing animated"
                    });
                });
            }
        } else {
            $.bigBox({
                title: "Info",
                content: "Please select view",
                color: "#C46A69",
                timeout: 8000,
                icon: "fa fa-bolt swing animated"
            });
        }
    };

    $scope.UpdateCM00083 = function () {
        //var selView = $scope.CM00071.SelectedView;
        if ($scope.SelectedLookUp && $scope.SelectedLookUp.SelectedView) {
            var selectedViewsFields = [];
            $scope.FieldSummaryListByView_GraphSummary.forEach(function (ele, index) {
                if (ele.isSelected === true) {
                    ele.ViewID = $scope.SelectedLookUp.SelectedView;
                    if (ele.SummaryType == undefined || ele.SummaryType === "") {
                        $.bigBox({
                            title: "Info",
                            content: "Summery Type is missing",
                            color: "#C46A69",
                            timeout: 8000,
                            icon: "fa fa-bolt swing animated"
                        });
                        return;
                    }
                    ele.SummeryTypeID = ele.SummaryType;
                    selectedViewsFields.push(ele);
                }
            });
            if (selectedViewsFields.length > 0) {
                $http({
                    url: '/CMS_CaseType/UpdateCM00083?KaizenPublicKey=' + sessionStorage.PublicKey,
                    method: "POST",
                    data: selectedViewsFields,
                    dataType: "json",
                    headers: {
                        'Content-Type': 'application/json; charset=utf-8'
                    }
                }).success(function (data) {
                    if (data.Status == true) {
                        //$scope.CM00073 = {};
                        $scope.CM00073.status = 0;
                        $.bigBox({
                            title: data.Massage,
                            content: data.Description,
                            color: "#739E73",
                            timeout: 8000,
                            icon: "fa fa-bolt swing animated"
                        });
                    } else {
                        $.bigBox({
                            title: data.Massage,
                            content: data.Description,
                            color: "#C46A69",
                            timeout: 8000,
                            icon: "fa fa-bolt swing animated"
                        });
                        return;
                    }
                }).error(function (data, status, headers, config) {
                    $.bigBox({
                        title: "Error",
                        content: "Unable to update graph views fields",
                        color: "#C46A69",
                        timeout: 8000,
                        icon: "fa fa-bolt swing animated"
                    });
                });
            }
        } else {
            $.bigBox({
                title: "Info",
                content: "Please select view",
                color: "#C46A69",
                timeout: 8000,
                icon: "fa fa-bolt swing animated"
            });
        }
    };
    $scope.AddOrDeleteCM00083 = function (viewsFieldsObj) {

        if (viewsFieldsObj) {
            if ($scope.SelectedLookUp.SelectedView == undefined || $scope.SelectedLookUp.SelectedView == undefined) {
                $.bigBox({
                    title: "Error",
                    content: "Please select view",
                    color: "#C46A69",
                    timeout: 8000,
                    icon: "fa fa-bolt swing animated"
                });
                viewsFieldsObj.isSelected = !viewsFieldsObj.isSelected;
                return;
            }
            if (viewsFieldsObj.SummaryType == undefined) {
                $.bigBox({
                    title: "Error",
                    content: "Please select summary type",
                    color: "#C46A69",
                    timeout: 8000,
                    icon: "fa fa-bolt swing animated"
                });
                viewsFieldsObj.isSelected = !viewsFieldsObj.isSelected;
                return;
            }
            viewsFieldsObj.SummeryTypeID = viewsFieldsObj.SummaryType;
            viewsFieldsObj.ViewID = $scope.SelectedLookUp.SelectedView;

            $scope.CM00083 = viewsFieldsObj;

            var urlToUpdate = "";

            if (viewsFieldsObj.isSelected) {
                urlToUpdate = "/CMS_CaseType/AddCM00083?KaizenPublicKey=" + sessionStorage.PublicKey;
            } else {
                urlToUpdate = "/CMS_CaseType/DeleteCM00083?KaizenPublicKey=" + sessionStorage.PublicKey;
            }

            $http({
                url: urlToUpdate,
                method: "POST",
                data: JSON.stringify($scope.CM00083),
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
                        timeout: 8000,
                        icon: "fa fa-bolt swing animated"
                    });
                } else {
                    $.bigBox({
                        title: data.Massage,
                        content: data.Description,
                        color: "#C46A69",
                        timeout: 8000,
                        icon: "fa fa-bolt swing animated"
                    });
                    return;
                }
            }).error(function (data, status, headers, config) {
                $.bigBox({
                    title: "Error",
                    content: "Unable to save views fields for the selected graph",
                    color: "#C46A69",
                    timeout: 8000,
                    icon: "fa fa-bolt swing animated"
                });
            });
        }
    };

    //------------------ View Pivot Table -------------------
    $scope.CM00084 = {};
    $scope.CM00085 = {};
    $scope.ViewPivotTable = function () {
        $scope.LoadCM00029Page('ViewPivotTable');
        $scope.Clear();
        $scope.LoadCaseTypes_ViewPivotTable();
        $scope.LoadGraphTypes();
        $scope.CM00074.status = 0;
    };

    $scope.LoadCaseTypes_ViewPivotTable = function () {
        $http.get('/CMS_CaseType/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.CaseTypes = data;
        }).finally(function () {

        });
    };

    $scope.CaseTypeChangedForViewPivotTable = function () {
        if (angular.isDefined($scope.SelectedLookUp.SelectedType)) {
            $scope.CaseTypeGraphList = [];
            $scope.LoadViews_CaseTypeGraph($scope.SelectedLookUp.SelectedType);
            $scope.FieldListCM00084 = [];
            $scope.FieldListCM00085 = [];
            $scope.FieldListCM00086 = [];
            $scope.LoadAllFields_CM00084($scope.SelectedLookUp.SelectedType);
            $scope.LoadAllFields_CM00085($scope.SelectedLookUp.SelectedType);
            $scope.LoadAllFields_CM00086($scope.SelectedLookUp.SelectedType);
        }
    };

    $scope.ViewChangedForFieldSummaryPivot = function () {
        if (angular.isDefined($scope.SelectedLookUp.SelectedView)) {
            $scope.CM00084.ViewID = $scope.SelectedLookUp.SelectedView;
            $scope.CM00085.ViewID = $scope.SelectedLookUp.SelectedView;
            $scope.LoadGraphViewsFieldSummaryPivot($scope.SelectedLookUp.SelectedView);
            $scope.LoadSummaryTypes();
        }
    };

    //$scope.GraphViewChangedForFieldSummary = function () {
    //    if (angular.isDefined($scope.SelectedLookUp.SelectedGraph)) {
    //        $scope.CM00084.GraphID = $scope.SelectedLookUp.SelectedGraph;
    //        $scope.CM00085.GraphID = $scope.SelectedLookUp.SelectedGraph;
    //    }
    //};
    $scope.FieldListCM00084 = [];
    $scope.LoadGraphViewsFieldSummaryPivot = function (ViewID) {
        $http.get('/CMS_CaseType/GetCM00084?KaizenPublicKey=' + sessionStorage.PublicKey,
            { params: { ViewID: ViewID } }).success(function (data) {
                $scope.CaseTypeGraphViewsFields = data;

                $scope.FieldListCM00084.forEach(function (ele, index) {
                    ele.isSelected = false;
                    $scope.CaseTypeGraphViewsFields.forEach(function (eleSelected, index) {
                        if (ele.FieldName.trim() === eleSelected.FieldName.trim()) {
                            ele.isSelected = true;
                        }
                    });
                });

                $http.get('/CMS_CaseType/GetCM00085?KaizenPublicKey=' + sessionStorage.PublicKey,
                { params: { ViewID: ViewID } }).success(function (data) {
                    $scope.CaseTypeGraphViewsFields_Summary = data;

                    $scope.FieldListCM00085.forEach(function (ele, index) {
                        ele.isSelected = false;
                        $scope.CaseTypeGraphViewsFields_Summary.forEach(function (eleSelected, index) {
                            if (ele.FieldName.trim() === eleSelected.FieldName.trim()) {
                                ele.isSelected = true;
                            }
                        });
                    });

                        $http.get('/CMS_CaseType/GetCM00086?KaizenPublicKey=' + sessionStorage.PublicKey,
                        { params: { ViewID: ViewID } }).success(function (data) {
                            $scope.CM00086List = data;

                            $scope.FieldListCM00086.forEach(function (ele, index) {
                                ele.isSelected = false;
                                $scope.CM00086List.forEach(function (eleSelected, index) {
                                    if (ele.FieldName.trim() === eleSelected.FieldName.trim()) {
                                        ele.isSelected = true;
                                    }
                                });
                            });
                        });
                });
            });
    };

    $scope.LoadAllFields_CM00084 = function (TRXTypeID) {
        $http.get('/CMS_CaseType/GetAllFieldsByCaseType?KaizenPublicKey=' + sessionStorage.PublicKey,
            { params: { TRXTypeID: TRXTypeID } }).success(function (data) {
                if (data.length >= 0) {
                    if (data.length > 0) {
                        $scope.FieldListCM00084 = data;
                    } else {
                        $scope.FieldListCM00084 = [];
                    }
                }
            });
    };

    $scope.LoadAllFields_CM00085 = function (TRXTypeID) {
        $http.get('/CMS_CaseType/GetAllFieldsByCaseType?KaizenPublicKey=' + sessionStorage.PublicKey,
            { params: { TRXTypeID: TRXTypeID } }).success(function (data) {
                if (data.length >= 0) {
                    if (data.length > 0) {
                        $scope.FieldListCM00085 = data;

                    } else {
                        $scope.FieldListCM00085 = [];
                    }
                }
            });
    };

    $scope.LoadAllFields_CM00086 = function (TRXTypeID) {
        $http.get('/CMS_CaseType/GetAllFieldsByCaseType?KaizenPublicKey=' + sessionStorage.PublicKey,
            { params: { TRXTypeID: TRXTypeID } }).success(function (data) {
                if (data.length >= 0) {
                    if (data.length > 0) {
                        $scope.FieldListCM00086 = data;

                    } else {
                        $scope.FieldListCM00086 = [];
                    }
                }
            });
    };

    $scope.AddOrDeleteCM00084 = function (viewsFieldsObj) {

        if (viewsFieldsObj) {
            if ($scope.SelectedLookUp == undefined || $scope.SelectedLookUp.SelectedView == undefined) {
                $.bigBox({
                    title: "Error",
                    content: "Please select view",
                    color: "#C46A69",
                    timeout: 8000,
                    icon: "fa fa-bolt swing animated"
                });
                viewsFieldsObj.isSelected = !viewsFieldsObj.isSelected;
                return;
            }

            $scope.CM00084 = viewsFieldsObj;
            $scope.CM00084.ViewID = $scope.SelectedLookUp.SelectedView;

            var urlToUpdate = "";

            if (viewsFieldsObj.isSelected) {
                urlToUpdate = "/CMS_CaseType/AddCM00084?KaizenPublicKey=" + sessionStorage.PublicKey;
            } else {
                urlToUpdate = "/CMS_CaseType/DeleteCM00084?KaizenPublicKey=" + sessionStorage.PublicKey;
            }

            $http({
                url: urlToUpdate,
                method: "POST",
                data: JSON.stringify($scope.CM00084),
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
                        timeout: 8000,
                        icon: "fa fa-bolt swing animated"
                    });
                } else {
                    $.bigBox({
                        title: data.Massage,
                        content: data.Description,
                        color: "#C46A69",
                        timeout: 8000,
                        icon: "fa fa-bolt swing animated"
                    });
                    return;
                }
            }).error(function (data, status, headers, config) {
                $.bigBox({
                    title: "Error",
                    content: "Unable to save views fields",
                    color: "#C46A69",
                    timeout: 8000,
                    icon: "fa fa-bolt swing animated"
                });
            });
        }
    };

    $scope.UpdateCM00084 = function () {
        //var selView = $scope.CM00071.SelectedView;
        if ($scope.SelectedLookUp && $scope.SelectedLookUp.SelectedView) {
            var selectedViewsFields = [];
            $scope.FieldListCM00084.forEach(function (ele, index) {
                if (ele.isSelected === true) {
                    ele.ViewID = $scope.SelectedLookUp.SelectedView;
                    selectedViewsFields.push(ele);
                }
            });
            if (selectedViewsFields.length > 0) {
                $http({
                    url: '/CMS_CaseType/UpdateCM00084?KaizenPublicKey=' + sessionStorage.PublicKey,
                    method: "POST",
                    data: selectedViewsFields,
                    dataType: "json",
                    headers: {
                        'Content-Type': 'application/json; charset=utf-8'
                    }
                }).success(function (data) {
                    if (data.Status == true) {
                        //$scope.CM00073 = {};
                        $scope.CM00073.status = 0;
                        $.bigBox({
                            title: data.Massage,
                            content: data.Description,
                            color: "#739E73",
                            timeout: 8000,
                            icon: "fa fa-bolt swing animated"
                        });
                    } else {
                        $.bigBox({
                            title: data.Massage,
                            content: data.Description,
                            color: "#C46A69",
                            timeout: 8000,
                            icon: "fa fa-bolt swing animated"
                        });
                        return;
                    }
                }).error(function (data, status, headers, config) {
                    $.bigBox({
                        title: "Error",
                        content: "Unable to update graph views fields",
                        color: "#C46A69",
                        timeout: 8000,
                        icon: "fa fa-bolt swing animated"
                    });
                });
            }
        } else {
            $.bigBox({
                title: "Info",
                content: "Please select view",
                color: "#C46A69",
                timeout: 8000,
                icon: "fa fa-bolt swing animated"
            });
        }
    };

    $scope.UpdateCM00085 = function () {
        //var selView = $scope.CM00071.SelectedView;
        if ($scope.SelectedLookUp && $scope.SelectedLookUp.SelectedView) {
            var selectedViewsFields = [];
            $scope.FieldListCM00085.forEach(function (ele, index) {
                if (ele.isSelected === true) {
                    ele.ViewID = $scope.SelectedLookUp.SelectedView;
                    selectedViewsFields.push(ele);
                }
            });
            if (selectedViewsFields.length > 0) {
                $http({
                    url: '/CMS_CaseType/UpdateCM00085?KaizenPublicKey=' + sessionStorage.PublicKey,
                    method: "POST",
                    data: selectedViewsFields,
                    dataType: "json",
                    headers: {
                        'Content-Type': 'application/json; charset=utf-8'
                    }
                }).success(function (data) {
                    if (data.Status == true) {
                        //$scope.CM00073 = {};
                        $.bigBox({
                            title: data.Massage,
                            content: data.Description,
                            color: "#739E73",
                            timeout: 8000,
                            icon: "fa fa-bolt swing animated"
                        });
                    } else {
                        $.bigBox({
                            title: data.Massage,
                            content: data.Description,
                            color: "#C46A69",
                            timeout: 8000,
                            icon: "fa fa-bolt swing animated"
                        });
                        return;
                    }
                }).error(function (data, status, headers, config) {
                    $.bigBox({
                        title: "Error",
                        content: "Unable to update graph views fields",
                        color: "#C46A69",
                        timeout: 8000,
                        icon: "fa fa-bolt swing animated"
                    });
                });
            }
        } else {
            $.bigBox({
                title: "Info",
                content: "Please select view",
                color: "#C46A69",
                timeout: 8000,
                icon: "fa fa-bolt swing animated"
            });
        }
    };
    $scope.AddOrDeleteCM00085 = function (viewsFieldsObj) {

        if (viewsFieldsObj) {
            if ($scope.SelectedLookUp.SelectedView == undefined || $scope.SelectedLookUp.SelectedView == undefined) {
                $.bigBox({
                    title: "Error",
                    content: "Please select view",
                    color: "#C46A69",
                    timeout: 8000,
                    icon: "fa fa-bolt swing animated"
                });
                viewsFieldsObj.isSelected = !viewsFieldsObj.isSelected;
                return;
            }
            viewsFieldsObj.SummeryTypeID = viewsFieldsObj.SummaryType;
            viewsFieldsObj.ViewID = $scope.SelectedLookUp.SelectedView;

            $scope.CM00085 = viewsFieldsObj;

            var urlToUpdate = "";

            if (viewsFieldsObj.isSelected) {
                urlToUpdate = "/CMS_CaseType/AddCM00085?KaizenPublicKey=" + sessionStorage.PublicKey;
            } else {
                urlToUpdate = "/CMS_CaseType/DeleteCM00085?KaizenPublicKey=" + sessionStorage.PublicKey;
            }

            $http({
                url: urlToUpdate,
                method: "POST",
                data: JSON.stringify($scope.CM00085),
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
                        timeout: 8000,
                        icon: "fa fa-bolt swing animated"
                    });
                } else {
                    $.bigBox({
                        title: data.Massage,
                        content: data.Description,
                        color: "#C46A69",
                        timeout: 8000,
                        icon: "fa fa-bolt swing animated"
                    });
                    return;
                }
            }).error(function (data, status, headers, config) {
                $.bigBox({
                    title: "Error",
                    content: "Unable to save views fields for the selected graph",
                    color: "#C46A69",
                    timeout: 8000,
                    icon: "fa fa-bolt swing animated"
                });
            });
        }
    };

    $scope.UpdateCM00086 = function () {
        //var selView = $scope.CM00071.SelectedView;
        if ($scope.SelectedLookUp && $scope.SelectedLookUp.SelectedView) {
            var selectedViewsFields = [];
            $scope.FieldListCM00086.forEach(function (ele, index) {
                if (ele.isSelected === true) {
                    ele.ViewID = $scope.SelectedLookUp.SelectedView;
                    selectedViewsFields.push(ele);
                }
            });
            if (selectedViewsFields.length > 0) {
                $http({
                    url: '/CMS_CaseType/UpdateCM00086?KaizenPublicKey=' + sessionStorage.PublicKey,
                    method: "POST",
                    data: selectedViewsFields,
                    dataType: "json",
                    headers: {
                        'Content-Type': 'application/json; charset=utf-8'
                    }
                }).success(function (data) {
                    if (data.Status == true) {
                        //$scope.CM00073 = {};
                        $.bigBox({
                            title: data.Massage,
                            content: data.Description,
                            color: "#739E73",
                            timeout: 8000,
                            icon: "fa fa-bolt swing animated"
                        });
                    } else {
                        $.bigBox({
                            title: data.Massage,
                            content: data.Description,
                            color: "#C46A69",
                            timeout: 8000,
                            icon: "fa fa-bolt swing animated"
                        });
                        return;
                    }
                }).error(function (data, status, headers, config) {
                    $.bigBox({
                        title: "Error",
                        content: "Unable to update data",
                        color: "#C46A69",
                        timeout: 8000,
                        icon: "fa fa-bolt swing animated"
                    });
                });
            }
        } else {
            $.bigBox({
                title: "Info",
                content: "Please select view",
                color: "#C46A69",
                timeout: 8000,
                icon: "fa fa-bolt swing animated"
            });
        }
    };
    $scope.AddOrDeleteCM00086 = function (viewsFieldsObj) {

        if (viewsFieldsObj) {
            if ($scope.SelectedLookUp == undefined || $scope.SelectedLookUp.SelectedView == undefined) {
                $.bigBox({
                    title: "Error",
                    content: "Please select view",
                    color: "#C46A69",
                    timeout: 8000,
                    icon: "fa fa-bolt swing animated"
                });
                viewsFieldsObj.isSelected = !viewsFieldsObj.isSelected;
                return;
            }
            if (viewsFieldsObj.SummaryType == undefined) {
                $.bigBox({
                    title: "Error",
                    content: "Please select summary type",
                    color: "#C46A69",
                    timeout: 8000,
                    icon: "fa fa-bolt swing animated"
                });
                viewsFieldsObj.isSelected = !viewsFieldsObj.isSelected;
                return;
            }
            viewsFieldsObj.ViewID = $scope.SelectedLookUp.SelectedView;
            viewsFieldsObj.SummeryTypeID = viewsFieldsObj.SummaryType;

            $scope.CM00086 = viewsFieldsObj;

            var urlToUpdate = "";

            if (viewsFieldsObj.isSelected) {
                urlToUpdate = "/CMS_CaseType/AddCM00086?KaizenPublicKey=" + sessionStorage.PublicKey;
            } else {
                urlToUpdate = "/CMS_CaseType/DeleteCM00086?KaizenPublicKey=" + sessionStorage.PublicKey;
            }

            $http({
                url: urlToUpdate,
                method: "POST",
                data: JSON.stringify($scope.CM00086),
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
                        timeout: 8000,
                        icon: "fa fa-bolt swing animated"
                    });
                } else {
                    $.bigBox({
                        title: data.Massage,
                        content: data.Description,
                        color: "#C46A69",
                        timeout: 8000,
                        icon: "fa fa-bolt swing animated"
                    });
                    return;
                }
            }).error(function (data, status, headers, config) {
                $.bigBox({
                    title: "Error",
                    content: "Unable to save views fields for the selected graph",
                    color: "#C46A69",
                    timeout: 8000,
                    icon: "fa fa-bolt swing animated"
                });
            });
        }
    };

    $scope.SummaryTypeChangedCM00086 = function (field) {
        if (angular.isDefined(field) && field.SummaryType != undefined) {
            //$scope.CM00047.SummeryTypeID = field.SummaryType.SummeryTypeID;
            field.SummeryTypeID = field.SummaryType;
        }
    };

    //------------------ Graph Category -------------------
    $scope.CM00079 = {};
    $scope.CM00079SelectedFieldName = [];
    $scope.CM00079SelectedFieldValue = [];
    $scope.GraphCategoryList = [];

    $scope.GraphCategory = function () {
        $scope.LoadCM00029Page('GraphCategory');
        $scope.Clear();
        $scope.LoadCaseTypes_GraphCategory();
        $scope.LoadGraphTypes();
        $scope.CM00074.status = 0;
    };

    $scope.LoadCaseTypes_GraphCategory = function () {
        $http.get('/CMS_CaseType/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.CaseTypes = data;
        }).finally(function () {

        });
    };

    $scope.CaseTypeChangedForGraphCategory = function () {
        if (angular.isDefined($scope.SelectedLookUp.SelectedType)) {
            $scope.CaseTypeGraphList = [];
            $scope.LoadViews_CaseTypeGraph($scope.SelectedLookUp.SelectedType);
            $scope.CM00075ListForFieldName = [];
            $scope.CM00075ListForFieldValue = [];
            $scope.GraphCategoryList = [];
        }
    };

    $scope.ViewChangedForGraphCategory = function () {
        if (angular.isDefined($scope.SelectedLookUp.SelectedView)) {
            $scope.LoadCM00075_ForFieldName($scope.SelectedLookUp.SelectedView);
            $scope.LoadCM00075_ForFieldValue($scope.SelectedLookUp.SelectedView);

            $scope.LoadGraphViewData_GroupCategory($scope.SelectedLookUp.SelectedView);
            $scope.LoadSummaryTypes();
            $scope.GraphCategoryList = [];
        }
    };

    $scope.GraphViewChangedForGraphCategory = function () {
        if (angular.isDefined($scope.SelectedLookUp.SelectedGraph)) {
            $scope.CM00079.GraphID = $scope.SelectedLookUp.SelectedGraph;
            $scope.LoadGraphCategoryData($scope.SelectedLookUp.SelectedGraph);
        }
    };
    $scope.CM00075ListForFieldName = [];
    $scope.LoadGraphCategoryData = function (GraphID) {
        $http.get('/CMS_CaseType/GetCM00079?KaizenPublicKey=' + sessionStorage.PublicKey,
            { params: { GraphID: GraphID } }).success(function (data) {
                if (data && data.length > 0)
                    $scope.GraphCategoryList = data;
                else
                    $scope.GraphCategoryList = [];

                //$scope.CM00075ListForFieldName.forEach(function (ele, index) {
                //    ele.isSelected = false;
                //    $scope.GraphCategoryList.forEach(function (eleSelected, index) {
                //        if (ele.FieldName.trim() === eleSelected.FieldName.trim()) {
                //            ele.isSelected = true;
                //        }
                //    });
                //});

                //$scope.CM00075ListForFieldValue.forEach(function (ele, index) {
                //    ele.isSelected = false;
                //    $scope.GraphCategoryList.forEach(function (eleSelected, index) {
                //        if (ele.FieldValue.trim() === eleSelected.FieldValue.trim()) {
                //            ele.isSelected = true;
                //            ele.SummaryType = eleSelected.SummeryTypeID;
                //            ele.FieldColor = eleSelected.FieldColor;
                //        }
                //    });
                //});
            });
    };

    $scope.LoadSummaryTypes = function () {
        $http.get('/CMS_CaseType/GetSummaryTypes?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.SummaryTypeList = data;
        }).finally(function () {
        });
    };
    $scope.CM00047 = {};

    $scope.GraphCategoryTypeChanged = function (field) {
        if (angular.isDefined(field) && field.SummaryType != undefined) {
            //$scope.CM00047.SummeryTypeID = field.SummaryType.SummeryTypeID;
            $scope.CM00079.SummeryTypeID = field.SummaryType;
        }
    };
    $scope.GraphViewDataHasOptions = {
        optionLabel: "-- Graph --"
    };
    $scope.LoadGraphViewData_GroupCategory = function (ViewID) {
        $http.get('/CMS_CaseType/GetGraphData?KaizenPublicKey=' + sessionStorage.PublicKey,
           { params: { ViewID: ViewID } }).success(function (data) {
               $scope.CaseTypeGraphList = data;
           }).finally(function () {

           });
    }

    $scope.LoadCM00075_ForFieldName = function (ViewID) {
        $http.get('/CMS_CaseType/GetAllFieldsByView?KaizenPublicKey=' + sessionStorage.PublicKey,
            { params: { ViewID: ViewID } }).success(function (data) {
                if (data.length >= 0) {
                    if (data.length > 0) {
                        $scope.CM00075ListForFieldName = data;
                    } else {
                        $scope.CM00075ListForFieldName = [];
                    }
                }
            });
    };

    $scope.LoadCM00075_ForFieldValue = function (ViewID) {
        $http.get('/CMS_CaseType/GetAllFieldsByView?KaizenPublicKey=' + sessionStorage.PublicKey,
            { params: { ViewID: ViewID } }).success(function (data) {
                if (data.length >= 0) {
                    if (data.length > 0) {
                        $scope.CM00075ListForFieldValue = data;

                    } else {
                        $scope.CM00075ListForFieldValue = [];
                    }
                }
            });
    };

    $scope.AddCM00079 = function (viewsFieldsObj) {

        if ($scope.SelectedLookUp == undefined || $scope.SelectedLookUp.SelectedView == undefined) {
            $.bigBox({
                title: "Error",
                content: "Please select view",
                color: "#C46A69",
                timeout: 8000,
                icon: "fa fa-bolt swing animated"
            });
            viewsFieldsObj.isSelected = !viewsFieldsObj.isSelected;
            return;
        }
        if ($scope.SelectedLookUp == undefined || $scope.SelectedLookUp.SelectedGraph == undefined) {
            $.bigBox({
                title: "Error",
                content: "Please select graph",
                color: "#C46A69",
                timeout: 8000,
                icon: "fa fa-bolt swing animated"
            });
            viewsFieldsObj.isSelected = !viewsFieldsObj.isSelected;
            return;
        }

        if ($scope.CM00079SelectedFieldName.length > 0) {
            $scope.CM00079.FieldName = $scope.CM00079SelectedFieldName[0].FieldName.trim();
            $scope.CM00079.FieldDisplay = $scope.CM00079SelectedFieldName[0].FieldDisplay.trim();
        }
        if ($scope.CM00079SelectedFieldValue.length > 0) {
            $scope.CM00079.FieldValue = $scope.CM00079SelectedFieldValue[0].FieldName.trim();
            $scope.CM00079.SummeryTypeID = $scope.CM00079SelectedFieldValue[0].SummeryTypeID;
            $scope.CM00079.FieldColor = $scope.CM00079SelectedFieldValue[0].FieldColor.trim();
        }

        var urlToUpdate = "";

        urlToUpdate = "/CMS_CaseType/AddCM00079?KaizenPublicKey=" + sessionStorage.PublicKey;
        $http({
            url: urlToUpdate,
            method: "POST",
            data: JSON.stringify($scope.CM00079),
            dataType: "json",
            headers: {
                'Content-Type': 'application/json; charset=utf-8'
            }
        }).success(function (data) {
            if (data.Status == true) {
                $scope.GraphCategoryList.push($scope.CM00079);

                $scope.CM00075ListForFieldName.forEach(function (ele, index) {
                    ele.isSelected = false;
                });

                $scope.CM00075ListForFieldValue.forEach(function (ele, index) {
                    ele.isSelected = false;
                    ele.FieldColor = "";
                });

                $.bigBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#739E73",
                    timeout: 8000,
                    icon: "fa fa-bolt swing animated"
                });
            } else {
                $.bigBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#C46A69",
                    timeout: 8000,
                    icon: "fa fa-bolt swing animated"
                });
                return;
            }
        }).error(function (data, status, headers, config) {
            $.bigBox({
                title: "Error",
                content: "Unable to save data",
                color: "#C46A69",
                timeout: 8000,
                icon: "fa fa-bolt swing animated"
            });
        });
    };

    $scope.CM00079FieldName_Selected = function (viewsFieldsObj) {

        if ($scope.CM00079SelectedFieldName && $scope.CM00079SelectedFieldName.length > 0) {
            if ($scope.CM00079SelectedFieldName[0].FieldName.trim() === viewsFieldsObj.FieldName.trim())
                $scope.CM00079SelectedFieldName.splice(0);
            else {
                viewsFieldsObj.isSelected = !viewsFieldsObj.isSelected;
            }
        }
        else {
            $scope.CM00079SelectedFieldName.push(viewsFieldsObj);
        }
    };

    $scope.CM00079FieldValue_Selected = function (viewsFieldsObj) {
        if (viewsFieldsObj.SummaryType == undefined) {
            $.bigBox({
                title: "Error",
                content: "Please select summary type",
                color: "#C46A69",
                timeout: 8000,
                icon: "fa fa-bolt swing animated"
            });
            viewsFieldsObj.isSelected = !viewsFieldsObj.isSelected;
            return;
        }

        if ($scope.CM00079SelectedFieldValue && $scope.CM00079SelectedFieldValue.length > 0) {
            if ($scope.CM00079SelectedFieldValue[0].FieldName.trim() === viewsFieldsObj.FieldName.trim())
                $scope.CM00079SelectedFieldValue.splice(0);
            else {
                viewsFieldsObj.isSelected = !viewsFieldsObj.isSelected;
            }
        }
        else {
            $scope.CM00079SelectedFieldValue.push(viewsFieldsObj);
        }
    };

    $scope.UpdateCM00079 = function () {
        //var selView = $scope.CM00071.SelectedView;
        if ($scope.SelectedLookUp && $scope.SelectedLookUp.SelectedView) {
            var selectedViewsFields = [];
            $scope.CM00075ListForFieldValue.forEach(function (ele, index) {
                if (ele.isSelected === true) {
                    ele.ViewID = $scope.SelectedLookUp.SelectedView;
                    if (ele.SummaryType == undefined || ele.SummaryType === "") {
                        $.bigBox({
                            title: "Info",
                            content: "Summery Type is missing",
                            color: "#C46A69",
                            timeout: 8000,
                            icon: "fa fa-bolt swing animated"
                        });
                        return;
                    }
                    ele.SummeryTypeID = ele.SummaryType;
                    selectedViewsFields.push(ele);
                }
            });
            if (selectedViewsFields.length > 0) {
                $http({
                    url: '/CMS_CaseType/UpdateCM00079?KaizenPublicKey=' + sessionStorage.PublicKey,
                    method: "POST",
                    data: selectedViewsFields,
                    dataType: "json",
                    headers: {
                        'Content-Type': 'application/json; charset=utf-8'
                    }
                }).success(function (data) {
                    if (data.Status == true) {
                        //$scope.CM00073 = {};
                        $scope.CM00073.status = 0;
                        $.bigBox({
                            title: data.Massage,
                            content: data.Description,
                            color: "#739E73",
                            timeout: 8000,
                            icon: "fa fa-bolt swing animated"
                        });
                    } else {
                        $.bigBox({
                            title: data.Massage,
                            content: data.Description,
                            color: "#C46A69",
                            timeout: 8000,
                            icon: "fa fa-bolt swing animated"
                        });
                        return;
                    }
                }).error(function (data, status, headers, config) {
                    $.bigBox({
                        title: "Error",
                        content: "Unable to update graph views fields",
                        color: "#C46A69",
                        timeout: 8000,
                        icon: "fa fa-bolt swing animated"
                    });
                });
            }
        } else {
            $.bigBox({
                title: "Info",
                content: "Please select view",
                color: "#C46A69",
                timeout: 8000,
                icon: "fa fa-bolt swing animated"
            });
        }
    };

    $scope.GroupCategorySummaryTypeChanged = function (field) {
        if (angular.isDefined(field) && field.SummaryType != undefined) {
            field.SummeryTypeID = field.SummaryType;
        }
    };

    $scope.DeleteCM00079 = function (obj, index) {

        $.SmartMessageBox({
            title: "Delete Record",
            content: "Are you sure?",
            buttons: '[No][Yes]'
        }, function (ButtonPressed) {
            if (ButtonPressed === "Yes") {

                $http({
                    url: '/CMS_CaseType/DeleteCM00079?KaizenPublicKey=' + sessionStorage.PublicKey,
                    method: "POST",
                    data: JSON.stringify(obj),
                    dataType: "json",
                    headers: {
                        'Content-Type': 'application/json; charset=utf-8'
                    }
                }).success(function (data) {
                    if (data.Status == true) {
                        $scope.GraphCategoryList.splice(index, 1);
                        //Notify(data.Massage, 'bottom-right', '5000', 'success', 'fa-check', true);
                        $.bigBox({
                            title: data.Massage,
                            content: data.Description,
                            color: "#C46A69",
                            timeout: 8000,
                            icon: "fa fa-bolt swing animated"
                        });
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
                }).error(function (data, status, headers, config) {
                    $.bigBox({
                        title: "Error",
                        content: "Unable to delete data",
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

    //------------------ Case Type Tables -------------------
    $scope.CM00037 = {};
    $scope.CaseTypeTableList = [];

    $scope.CaseTypeTables = function () {
        $scope.LoadCM00029Page('CaseTypeTables');
        $scope.Clear();
        $scope.CM00037.status = 0;
        $scope.LoadAllCM00037();
    };

    $scope.LoadAllCM00037 = function () {
        $http.get('/CMS_CaseType/GetAllCM00037?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.CaseTypeTableList = data;
        }).finally(function () {
            
        });
    };

    $scope.AddCM00037 = function () {

        if ($scope.CM00037 == undefined || $scope.CM00037.TableSource == undefined || $scope.CM00037.TableSource == "") {
            $.bigBox({
                title: "Error",
                content: "Table Source is missing",
                color: "#C46A69",
                timeout: 8000,
                icon: "fa fa-bolt swing animated"
            });
            return;
        }

        if ($scope.CM00037.IsCustomTable == undefined || $scope.CM00037.IsCustomTable == "") {
            $scope.CM00037.IsCustomTable = false;
        }

        $http({
            url: "/CMS_CaseType/AddCM00037?KaizenPublicKey=" + sessionStorage.PublicKey,
            method: "POST",
            data: JSON.stringify($scope.CM00037),
            dataType: "json",
            headers: {
                'Content-Type': 'application/json; charset=utf-8'
            }
        }).success(function (data) {
            if (data.Status == true) {
                $scope.CaseTypeTableList.push($scope.CM00037);
                $scope.CM00037 = {};
                $scope.CM00037.status = 1;

                $.bigBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#739E73",
                    timeout: 8000,
                    icon: "fa fa-bolt swing animated"
                });
            } else {
                $.bigBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#C46A69",
                    timeout: 8000,
                    icon: "fa fa-bolt swing animated"
                });
                return;
            }
        }).error(function (data, status, headers, config) {
            $.bigBox({
                title: "Error",
                content: "Unable to save data",
                color: "#C46A69",
                timeout: 8000,
                icon: "fa fa-bolt swing animated"
            });
        });
    };

    $scope.EditCaseTypeTable = function (obj) {

        indx = $scope.CaseTypeTableList.indexOf(obj);
        var obj_extend = jQuery.extend({}, $scope.CaseTypeTableList[indx]);
        $scope.CM00037 = obj_extend;
        $scope.CM00037.status = 2;
    };

    $scope.DeleteCM00037 = function (obj, index) {

        $.SmartMessageBox({
            title: "Delete Record",
            content: "Are you sure?",
            buttons: '[No][Yes]'
        }, function (ButtonPressed) {
            if (ButtonPressed === "Yes") {

                $http({
                    url: '/CMS_CaseType/DeleteCM00037?KaizenPublicKey=' + sessionStorage.PublicKey,
                    method: "POST",
                    data: JSON.stringify(obj),
                    dataType: "json",
                    headers: {
                        'Content-Type': 'application/json; charset=utf-8'
                    }
                }).success(function (data) {
                    if (data.Status == true) {
                        $scope.GraphCategoryList.splice(index, 1);
                        //Notify(data.Massage, 'bottom-right', '5000', 'success', 'fa-check', true);
                        $.bigBox({
                            title: data.Massage,
                            content: data.Description,
                            color: "#C46A69",
                            timeout: 8000,
                            icon: "fa fa-bolt swing animated"
                        });
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
                }).error(function (data, status, headers, config) {
                    $.bigBox({
                        title: "Error",
                        content: "Unable to delete data",
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

    $scope.CancelUpdateCaseTypeTable = function () {
        $scope.CM00037 = {};
        $scope.CM00037.status = 1;
    };

    $scope.UpdateCM00037 = function () {

        $http({
            url: "/CMS_CaseType/UpdateCM00037?KaizenPublicKey=" + sessionStorage.PublicKey,
            method: "POST",
            data: JSON.stringify($scope.CM00037),
            dataType: "json",
            headers: {
                'Content-Type': 'application/json; charset=utf-8'
            }
        }).success(function (data) {
            if (data.Status == true) {
                $scope.CaseTypeTableList.splice(indx, 1, $scope.CM00037);
                $scope.CM00037 = {};
                $scope.CM00037.status = 1;

                $.bigBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#739E73",
                    timeout: 8000,
                    icon: "fa fa-bolt swing animated"
                });
            } else {
                $.bigBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#C46A69",
                    timeout: 8000,
                    icon: "fa fa-bolt swing animated"
                });
                return;
            }
        }).error(function (data, status, headers, config) {
            $.bigBox({
                title: "Error",
                content: "Unable to save data",
                color: "#C46A69",
                timeout: 8000,
                icon: "fa fa-bolt swing animated"
            });
        });
    };
});