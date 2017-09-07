app.controller('Inq_CaseArchiveController', function ($scope, $http) {
    $scope.CM20203 = {};
    $scope.MainFilter = {};
    $scope.PagesCM20203 = [];
    $scope.CaseDocType();
    $scope.GetGL00002List();
    $scope.CM00021List = [];
    $scope.ClientClassLoading = function () {
        if ($scope.CM00021List.length > 0) return;
        $http.get('/CMS_ClientClass/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.CM00021List = data;
        }).finally(function () { });
    };

    $scope.LoadLookUp = function () {

        $http.get('/Sys_View/GetViewsByScreen?ScreenID=11&KaizenPublicKey=' + sessionStorage.PublicKey)
          .success(function (data) {
              $scope.ViewList = data;
          }).finally(function () {
              if ($scope.ViewList.length > 0) {
                  $scope.MainFilter.ViewID = $scope.ViewList[0].ViewID;
                  $scope.ViewList.forEach(function (element, index) {
                      if (element.IsDefault) {
                          $scope.MainFilter.ViewID = element.ViewID;
                          return;
                      }
                  });
                  $scope.toolbarOptions = {
                      items: [
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
                          { type: "separator" },
                          { template: "<label>Filter:</label>" },
                          {
                              template: "<select kendo-drop-down-list k-ng-model='MainFilter.TRXTypeID' k-on-change='ViewChanged()' k-options='CaseTypeOptions' style='width: 200px;' k-value-primitive='true'></select>",
                              overflow: "never"
                          },
                          {
                              template: "<select kendo-drop-down-list k-ng-model='MainFilter.YearCode' k-on-change='FillPeriod()' k-options='YearCodelistOptions' style='width: 120px;' k-value-primitive='true'></select>",
                              overflow: "never"
                          },
                          {
                              template: "<select kendo-drop-down-list k-ng-model='MainFilter.PERIODID' k-on-change='ViewChanged()' k-options='PERIODIDDropDownlistOptions' id='CaseArchivPeriodDownlistOptions' style='width: 120px;' k-value-primitive='true'></select>",
                              overflow: "never"
                          },
                          {
                              template: "<select kendo-drop-down-list k-ng-model='MainFilter.ViewID' k-on-change='ViewChanged()' k-options='ViewIDDropDownlistOptions' style='width: 120px;' k-value-primitive='true'></select>",
                              overflow: "never"
                          },
                          {
                              type: "splitButton",
                              text: "Case List",
                              click: function (e) {
                                  $scope.$apply(function () {
                                      $scope.Cancel();
                                  });
                              },
                              menuButtons: [
                                  {
                                      text: "Portfolio Grading",
                                      click: function (e) {
                                          $scope.$apply(function () {
                                              $scope.LoadCM20203Page("PortfolioGrading");
                                              $scope.ClientClassLoading();

                                          });
                                      }
                                  },
                                   {
                                       text: "Portfolio Cycle",
                                       click: function (e) {
                                           $scope.$apply(function () {
                                               $scope.LoadCM20203Page("PortfolioCycleProduct");
                                           });
                                       }
                                   },
                                    {
                                        text: "Delinquency Cycle",
                                        click: function (e) {
                                            $scope.$apply(function () {
                                                $scope.LoadCM20203Page("DelinquencyCycle");
                                            });
                                        }
                                    },
                                   {
                                       text: "Case Collection",
                                       click: function (e) {
                                           $scope.$apply(function () {
                                               $scope.LoadCM20203Page("PortfolioCollection");
                                           });
                                       }
                                   },
                                  {
                                      text: "Agent Target",
                                      click: function (e) {
                                          $scope.$apply(function () {
                                              $scope.LoadCM20203Page("AgentTargetProduct");
                                          });
                                      }
                                  },
                                  {
                                      text: "Portfolio Movement",
                                      click: function (e) {
                                          $scope.$apply(function () {
                                              $scope.LoadCM20203Page("PortfolioMovment");
                                          });
                                      }
                                  },
                                  {
                                      text: "Pivot View",
                                      click: function (e) {
                                          $scope.$apply(function () {
                                              $scope.GoTo("PivotGrid");
                                          });
                                      }
                                  }
                              ]
                          },
                          {
                              template: "<span class='k-textbox k-space-right' style='width: 100%;'><input type='text' ng-model='MainFilter.Searchcritaria'/>"
                                      + "<a href='#' class='k-icon k-i-search'>&nbsp;</a></span>",
                              overflow: "never"
                          },
                          {
                              type: "button", text: "Search"
                                 , click: function (e) {
                                     $scope.$apply(function () {
                                         $scope.LoadCM20203Page("AdvancedSearch");
                                         $scope.AdvancedSearch();
                                     });
                                 }
                          }
                      ] 
                , resizable: true
                  };
                  $scope.PERIODIDDropDownlistOptions = {
                      filter: "contains",
                      valuePrimitive: true,
                      optionLabel: "-- ALL Period --",
                      dataTextField: "PeriodName",
                      dataValueField: "PERIODID",
                      dataSource: $scope.GL00003,
                      value: $scope.MainFilter.PERIODID
                  };
                  $scope.ViewIDDropDownlistOptions = {
                      filter: "contains",
                      dataTextField: "ViewName",
                      dataValueField: "ViewID",
                      dataSource: $scope.ViewList,
                      value: $scope.MainFilter.ViewID
                  };
                  $scope.MainFilter.TRXTypeID = $scope.TRXTypes[0].TRXTypeID;
                  if ($scope.GL00002List.length == 0) return;
                  $scope.MainFilter.YearCode = $scope.GL00002List[0].YearCode;
                  $scope.ViewChanged();
              }
              else {
                  alert('Erorr Setup');
                  // Close the window by wael
                  return;
              }
          });
    };
    $scope.AdvancedSearch = function () {
        kendo.alert("This is a Kendo UI Alert message.");
        $http.get('/Sys_View/GetColumnsByView?ViewID=18666&KaizenPublicKey=' + sessionStorage.PublicKey)
     .success(function (data) {
         $scope.CaseKaizenColumn = data;
         $scope.CaseKaizenColumn.forEach(function (element, index) {
             element.IsChecked = false;
         });
     }).finally(function () {});
    };
    $scope.SeachText = '';
    $scope.SeachSQL = '';
    $scope.IsOrOperation = 0;
    $scope.AddSeach = function (IsOr) {
        $scope.Temp = '';
        $scope.TempText = '';
        $scope.CaseKaizenColumn.forEach(function (element, index) {
            if (element.IsChecked)
            {
                if (element.FieldTypeID == 3)
                {
                    var dd =new Date(element.FixedValue);
                    var str = dd.getFullYear() + "-" + (dd.getMonth() + 1) + "-" + dd.getDate();
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
                    $scope.Temp += element.field + "=" + str.toString();
                    $scope.TempText += element.title + "=" + str.toString();
                }
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
        // alert(column.FieldTypeID == 3)
        if (column.field == 'NationalityID') {
            column.html = "<div class='input-group'><input type='text' class='form-control' readonly " +
                "placeholder='Nationality ID' ng-model='column.FixedValue' />" +
                "<span class='input-group-btn input-group-sm'>" +
                "<button class='btn btn-default' ng-click=\"OpenkendoWindow('Nationality')\">" +
                "<span class='glyphicon glyphicon-search'></span></button></span></div>";
        }
        else if (column.field == 'AgentID') {
            column.html = "<div class='input-group'><input type='text' class='form-control' readonly " +
                "placeholder='AgentID' ng-model='column.FixedValue' />" +
                "<span class='input-group-btn input-group-sm'>" +
                "<button class='btn btn-default' ng-click=\"OpenkendoWindowForm('CMS','CMS_Agent','PopUp')\">" +
                "<span class='glyphicon glyphicon-search'></span></button></span></div>";
        }
        else if (column.FieldTypeID == 3) {
            column.FixedValue = new Date();
            column.html = "<input kendo-date-picker k-parse-formats=\"['yyyy-MM-ddTHH:mm:ss']\" k-format='dd/MM/yyyy' k-ng-model='column.FixedValue' style='width: 100%;' />";
        }

    };

    $scope.ViewChanged = function () {
        // var temp = $scope.functiontofindObjectByKeyValue($scope.ViewList, "ViewID", $scope.MainFilter.ViewID);
        //alert('sssssssssssssssssssssss');//wael
        //alert($scope.MainFilter.YearCode);
        //alert($scope.MainFilter.TRXTypeID);
        //$scope.GridRefresh('GridCMS_Inq_CaseArchive');
        $scope.MainGridURL = "/CMS/Inq_CaseArchive/MainGrid?ViewID=" + $scope.MainFilter.ViewID + "&KaizenPublicKey=" + sessionStorage.PublicKey;
    };
    $scope.FillPeriod = function () {
        $scope.GL00003 = [];
        $http.get('/GL_Set_FiscalYears/GetPeriodsByYearCode?YearCode=' + $scope.MainFilter.YearCode
            + "&KaizenPublicKey=" + sessionStorage.PublicKey)
            .success(function (data) {
                $scope.GL00003 = data;
                $("#CaseArchivPeriodDownlistOptions").data("kendoDropDownList").dataSource.data($scope.GL00003);
            })
            .finally(function () {
                $scope.ViewChanged();
            });
    };
    $scope.LoadLookUp();
    $scope.LoadCM20203 = function (TrxID) {
        if (angular.isUndefined($scope.CM20203.TrxID)) {
            $http.get('/CMS_Inq_CaseArchive/GetSingle?KaizenPublicKey=' + sessionStorage.PublicKey + "&TrxID=" + TrxID).success(function (data) {
                $scope.CM20203 = data;
            }).finally(function () { $scope.CM20203.Status = 2; });
        }
    };
    $scope.LoadCM20203Page = function (ActionName) {
        removeEntityPage($scope.PagesCM20203);
        var URIPath = "/CMS/" + $scope.ToolBar.ActiveScreen.ID + "/" + ActionName + "?KaizenPublicKey=" + sessionStorage.PublicKey;
        var Page = { url: URIPath, ActionName: ActionName };
        $scope.PagesCM20203.push(Page);
    };
    $scope.EditCM20203 = function (TrxID) {
        $scope.LoadCM20203Page('Create');
        $scope.LoadCM20203(TrxID);
    };
    $scope.Clear = function () {
        $scope.CM20203 = {};
    };
    $scope.Print = function () {
        alert('Printer not configured');
    };
    $scope.Cancel = function () {
        $scope.Clear();
        $scope.PagesCM20203 = [];
    };
    $scope.ClientBack = function (client) {
        if (client != null) {
            switch ($scope.CurrentControl) {
                case 'ClientCases':
                    $scope.MainFilter.ClientID = client.ClientID;
                    $scope.MainFilter.ClientName = client.ClientName;
                    break;
            }
        }
    };
    $scope.ContractBack = function (contract) {
        switch ($scope.CurrentControl) {
            case 'Main':
                $scope.MainFilter.ContractID = contract.ContractID;
                $scope.MainFilter.ContractName = contract.ContractName;
                break;
        }
    };
});

