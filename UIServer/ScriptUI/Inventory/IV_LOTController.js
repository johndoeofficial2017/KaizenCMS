app.controller('IV_LOTController', function ($scope, $http) {
    $scope.IV00010 = {
    };
    $scope.PagesIV00010 = [];
    $scope.toolbarOptions = {
        items: [
                 {
                     type: "button", text: "<span class=\"fa fa-plus-circle\"></span> Set Lot ",
                     attributes: { "class": "btn-primary" },
                     click: function (e) {
                         $scope.$apply(function () {
                             $scope.AddIV00010();
                         });
                     }
                 },
                 {
                     type: "button", text: "<span class=\"fa fa-plus-circle\"></span> Setup Lot Columns",
                     attributes: { "class": "btn-primary" },
                     click: function (e) {
                         $scope.$apply(function () {
                             $scope.LotColumn();
                         });
                     }
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
                 }
        ]
  , resizable: true
    };
    $scope.LoadIV00010 = function (LotNumber) {
        if (angular.isUndefined($scope.IV00010.LotNumber)) {
            $http.get('/IV_LOT/GetSingle?KaizenPublicKey=' + sessionStorage.PublicKey + "&LotNumber=" + LotNumber).success(function (data) {
                $scope.IV00010 = data;
            }).finally(function () { $scope.IV00010.Status = 2; });
        }
    };
    $scope.LoadIV00010Page = function (ActionName) {
        removeEntityPage($scope.PagesIV00010);
        var URIPath = "/" + $scope.ToolBar.ActiveScreen.ID + "/" + ActionName + "?KaizenPublicKey=" + sessionStorage.PublicKey;
        var Page = {
            url: URIPath, ActionName: ActionName
        };
        $scope.PagesIV00010.push(Page);
    };

    $scope.AddIV00010 = function () {
        $scope.LoadIV00010Page('Create');
        $scope.Clear();
        $scope.IV00010.Status = 1;
    };
    $scope.EditIV00010 = function (LotNumber) {
        $scope.LoadIV00010Page('Create');
        $scope.LoadIV00010(LotNumber);
    };
    $scope.SaveData = function () {
        $http.post('/IV_LOT/SaveData', {
            'IV00010': $scope.IV00010, 'KaizenPublicKey': sessionStorage.PublicKey
        }).success(function (data) {

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
        $http.post('/IV_LOT/UpdateData', {
            'IV00010': $scope.IV00010, 'KaizenPublicKey': sessionStorage.PublicKey
        }).success(function (data) {
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
        $scope.IV00010 = {};
        $scope.IV00013 = {};
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
                $http.post('/IV_LOT/DeleteData', { 'IV00010': $scope.IV00010, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
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
        $scope.PagesIV00010 = [];

        $scope.InsertedLotColumn = [];
        $scope.UpdatedLotColumn = [];
        $scope.DeletedLotColumn = [];

        $scope.ColumnList = [];
        $scope.ListItems = [];
    };

    //-------------------------- Add Lot Column
    $scope.LotColumn = function (LotNumber) {
        removeEntityPage($scope.PagesIV00010);
        $scope.LoadIV00010Page('LotColumn');
        if (LotNumber == null) {
            $scope.LoadIV00010(LotNumber);
            $scope.LoadLotIV_LotColumn(LotNumber);
        }
    };
    $scope.IV00013 = {}; // -- 0 = original from database ; 1 Inserted New ; 2 Edited;3 = Deleted ; 4 File Chabged
    $scope.ColumnList = [];
    $scope.ListItems = [];

    $scope.launchLots = function () {
        var dlg = dialogs.create('/IV_LOT/PopUp?KaizenPublicKey=' + sessionStorage.PublicKey, 'IV_LOTPopUpController', {});
        dlg.result.then(function (lot) {
            $scope.IV00010 = lot;
            $scope.LoadLotIV_LotColumn(lot.LotNumber);
        });
    };
    $scope.LoadLotIV_LotColumn = function (LotNumber) {
        $http.get('/IV_LOTCollumn/GetByLotNumber?KaizenPublicKey=' + sessionStorage.PublicKey, { params: { LotNumber: LotNumber } }).success(function (data) {
            if (data.length > 0) {
                $scope.ColumnList = data;
            }
        }).finally(function () {
            $scope.IV00013.status = 0;
        });
    };

    $scope.InsertedLotColumn = [];
    $scope.UpdatedLotColumn = [];
    $scope.DeletedLotColumn = [];

    $scope.OnTypeChanged = function () {

    };
    $scope.IV00014 = {}
    $scope.AddListItems = function () {
        $scope.IV00014.CollCode = $scope.IV00013.CollCode
        var obj = $scope.IV00014;
        $scope.ListItems.push(obj);
        alert(JSON.stringify($scope.ListItems));
        $scope.IV00014 = {
        };
    };

    $scope.AddNewLotColumn = function () {
        $scope.IV00013.status = 1;
        $scope.IV00013.LotNumber = $scope.IV00010.LotNumber
        $scope.ColumnList.push($scope.IV00013);
        $scope.UpdateLotColumn();
    };
    $scope.UpdateLotColumn = function () {
        $scope.IV00013 = {};
        $scope.IV00013.status = 0;
    };
    $scope.SaveLotColumn = function () {
        for (var i = 0; i < $scope.ColumnList.length; i++) {
            var item = $scope.ColumnList[i];
            if (item.status == 1) {
                if (item.CollType == 3) {
                    var insert_tmp = {
                        LotNumber: $scope.IV00010.LotNumber,
                        CollCode: item.CollCode,
                        CollName: item.CollName,
                        CollType: item.CollType,
                        IV00014: $scope.ListItems
                    };
                    $scope.InsertedLotColumn.push(insert_tmp);
                }
                else {
                    var insert_tmp = {
                        LotNumber: $scope.IV00010.LotNumber,
                        CollCode: item.CollCode,
                        CollName: item.CollName,
                        CollType: item.CollType,
                    };
                    $scope.InsertedLotColumn.push(insert_tmp);
                }
            }
            else if (item.status == 2) {
                var update_tmp = {
                    LotNumber: $scope.IV00010.LotNumber,
                    CollCode: item.CollCode,
                    CollName: item.CollName,
                    CollType: item.CollType,
                };
                $scope.UpdatedLotColumn.push(update_tmp);
            }
            else if (item.status == 3) {
                if (item.CollType == 3) {
                    var delete_tmp = {
                        LotNumber: $scope.IV00010.LotNumber,
                        CollCode: item.CollCode,
                        CollName: item.CollName,
                        CollType: item.CollType,
                        IV00014: $scope.ListItems
                    };
                    $scope.DeletedLotColumn.push(delete_tmp);
                }
                else {
                    var delete_tmp = {
                        LotNumber: $scope.IV00010.LotNumber,
                        CollCode: item.CollCode,
                        CollName: item.CollName,
                        CollType: item.CollType,
                    };
                    $scope.DeletedLotColumn.push(delete_tmp);
                }
            }
        }
        if ($scope.InsertedLotColumn.length > 0) {
            $http({
                url: '/IV_LOTCollumn/SaveData?KaizenPublicKey=' + sessionStorage.PublicKey,
                method: "POST",
                data: $.param({
                    IV00013: $scope.InsertedLotColumn
                }),
                headers: {
                    'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8'
                }
            }).success(function (data) {
            }).error(function (data, status, headers, config) {
                alert();
            });
        }
        if ($scope.UpdatedLotColumn.length > 0) {
            $http({
                url: '/IV_LOTCollumn/UpdateData?KaizenPublicKey=' + sessionStorage.PublicKey,
                method: "POST",
                data: $.param({
                    IV00013: $scope.UpdatedLotColumn
                }),
                headers: {
                    'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8'
                }
            }).success(function (data) {

            }).error(function (data, status, headers, config) {
                alert();
            });
        }
        if ($scope.DeletedLotColumn.length > 0) {
            $http({
                url: '/IV_LOTCollumn/DeleteData?KaizenPublicKey=' + sessionStorage.PublicKey,
                method: "POST",
                data: $.param({
                    IV00013: $scope.DeletedLotColumn
                }),
                headers: {
                    'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8'
                }
            }).success(function (data) {

            }).error(function (data, status, headers, config) {
                alert();
            });
        }
        $scope.Cancel();
    };
    $scope.RemoveLotColumn = function (lotcolumn, index) {
        if ($scope.IV00013.status == 1)
            $scope.ColumnList.splice(index, 1);
        else
            lotcolumn.status = 3;
    };
});