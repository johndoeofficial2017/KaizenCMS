﻿app.controller('CMS_Set_ContactTypeDetorController', function ($scope, $http) {
    $scope.Sys00006 = {};
    $scope.PagesSys00006 = [];
    $scope.LoadSys00006 = function (ContactTypeID) {
        if (angular.isUndefined($scope.Sys00006.ContactTypeID)) {
            $http.get('/Admin_ContactType/GetSingle?KaizenPublicKey=' + sessionStorage.PublicKey + "&ContactTypeID=" + ContactTypeID).success(function (data) {
                $scope.Sys00006 = data;
            }).finally(function () { $scope.Sys00006.Status = 2; });
        }
    };
    $scope.LoadSys00006Page = function (ActionName) {
        removeEntityPage($scope.PagesSys00006);
        var URIPath = "/" + $scope.ToolBar.ActiveScreen.ID + "/" + ActionName + "?KaizenPublicKey=" + sessionStorage.PublicKey;
        var Page = {
            url: URIPath, ActionName: ActionName
        };
        $scope.PagesSys00006.push(Page);
    };
    $scope.toolbarOptions = {
        items: [
                 {
                     type: "button", text: "<span class=\"fa fa-plus-circle\"></span> New Contact Type",
                     attributes: { "class": "btn-primary" },
                     click: function (e) {
                         $scope.$apply(function () {
                             $scope.AddSys00006();
                         });
                     }
                 },
                 {
                     type: "splitButton",
                     text: "GO",
                     click: function (e) {
                     },
                     menuButtons: [
                         {
                             type: "button", text: "<span class=\"fa fa-cog\"></span> User PO Defauls Setup", click: function (e) {
                                 $scope.$apply(function () {
                                     $scope.SetupUserDefaultsPO();
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
                 }
        ]
, resizable: true
    };
    $scope.LoadLookUp = function () {};
    $scope.LoadLookUp();

    $scope.AddSys00006 = function () {
        $scope.LoadSys00006Page('Create');
        $scope.Clear();
        $scope.Sys00006.Status = 1;
    };
    $scope.EditSys00006 = function (ContactTypeID) {
        $scope.LoadSys00006Page('Create');
        $scope.Clear();
        $scope.LoadSys00006(ContactTypeID);
    };
    $scope.SaveData = function () {
        $http.post('/Admin_ContactType/SaveData', {
            'Sys00006': $scope.Sys00006, 'ContactSourceID': 2, 'KaizenPublicKey': sessionStorage.PublicKey
        }).success(function (data) {
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
        $http.post('/Admin_ContactType/UpdateData', { 'Sys00006': $scope.Sys00006, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
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
        $scope.Sys00006 = {};
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
                $http.post('/Admin_ContactType/DeleteData', { 'Sys00006': $scope.Sys00006, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
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
        $scope.PagesSys00006 = [];
    };
});