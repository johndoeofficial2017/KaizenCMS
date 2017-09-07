app.controller('INV_SiteController', function ($scope, $http) {
    $scope.IV00011 = {};
    $scope.PagesIV00011 = [];
    $scope.toolbarOptions = {
        items: [
                 {
                     type: "button", text: "<span class=\"fa fa-plus-circle\"></span> New Site",
                     attributes: { "class": "btn-primary" },
                     click: function (e) {
                         $scope.$apply(function () {
                             $scope.AddIV00011();
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
                              type: "button", text: "<span class=\"fa fa-cog\"></span> Bin Setup", click: function (e) {
                                  $scope.$apply(function () {
                                      $scope.SetupBin();
                                  });
                              }
                          },
                           {
                               type: "button", text: "<span class=\"fa fa-cog\"></span> Sub Bin Setup", click: function (e) {
                                   $scope.$apply(function () {
                                       $scope.SetupSubBin();
                                   });
                               }
                           }
                      ]
                  },
                   {
                       type: "splitButton",
                       text: "Configuration",
                       click: function (e) {
                       },
                       menuButtons: [
                           {
                               type: "button", text: "<span class=\"fa fa-cog\"></span> Site Items", click: function (e) {
                                   $scope.$apply(function () {
                                       $scope.SiteItems();
                                   });
                               }
                           },
                            {
                                type: "button", text: "<span class=\"fa fa-cog\"></span> Site Item Bins", click: function (e) {
                                    $scope.$apply(function () {
                                        $scope.SiteItemBins();
                                    });
                                }
                            },
                             {
                                 type: "button", text: "<span class=\"fa fa-cog\"></span> Site Item SubBins", click: function (e) {
                                     $scope.$apply(function () {
                                         $scope.SiteItemSubBins();
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

    $scope.LoadIV00011 = function (SiteID) {
        if (angular.isUndefined($scope.IV00011.SiteID)) {
            $http.get('/INV_Site/GetSingle?KaizenPublicKey=' + sessionStorage.PublicKey + "&SiteID=" + SiteID).success(function (data) {
                $scope.IV00011 = data;
            }).finally(function () { $scope.IV00011.Status = 2; });
        }
    }
    $scope.LoadIV00011Page = function (ActionName) {
        removeEntityPage($scope.PagesIV00011);
        var URIPath = "/IVCommon/" + $scope.ToolBar.ActiveScreen.ID + "/" + ActionName + "?KaizenPublicKey=" + sessionStorage.PublicKey;
        var Page = { url: URIPath, ActionName: ActionName };
        $scope.PagesIV00011.push(Page);
    }

    $scope.AddIV00011 = function () {
        $scope.LoadIV00011Page('Create')
        $scope.Clear();
        $scope.IV00011.Status = 1;
    }
    $scope.EditIV00011 = function (SiteID) {
        $scope.LoadIV00011Page('Create')
        $scope.Clear();
        $scope.LoadIV00011(SiteID);
    };
    $scope.SaveData = function () {
        $http.post('/INV_Site/SaveData', { 'IV00011': $scope.IV00011, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
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
        $http.post('/INV_Site/UpdateData', { 'IV00011': $scope.IV00011, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
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
        $scope.IV00011 = {};
        $scope.IV00012 = {};
        $scope.IV00021 = {};
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
                $http.post('/INV_Site/DeleteData', { 'IV00011': $scope.IV00011, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
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
        $scope.PagesIV00011 = [];
    };

    $scope.ItemBack = function (item) {
        if (item != null) {
            switch ($scope.CurrentControl) {
                case 'ItemSiteBin':
                    $scope.IV00130.ItemID = item.ItemID;
                    if ($scope.IV00130.SiteID != '' && $scope.IV00130.SiteID != undefined
                        && $scope.IV00130.ItemID != '' && $scope.IV00130.ItemID != undefined) {
                        $scope.GridRefresh('GridINV_SiteItemBins');
                    }
                    break;
                case 'ItemSiteSubBin':
                    $scope.IV00131.ItemID = item.ItemID;
                    if ($scope.IV00131.SiteID != '' && $scope.IV00131.SiteID != undefined
                         && $scope.IV00131.ItemID != '' && $scope.IV00131.ItemID != undefined
                          && $scope.IV00131.BinID != '' && $scope.IV00131.BinID != undefined) {
                        $scope.GridRefresh('GridINV_SiteItemSubBins');
                    }
                    break;
            }
        }
    };

    //-------------------------- Bin
    $scope.IV00012 = {};
    $scope.SetupBin = function () {
        removeEntityPage($scope.PagesIV00011);
        $scope.LoadIV00011Page('SetupBin');
        $scope.IV00012.Status = 1;
    };
    $scope.SiteBack = function (site) {
        if (site != null) {
            switch ($scope.CurrentControl) {
                case 'Bin':
                    $scope.IV00011 = site;
                    $scope.IV00012.SiteID = site.SiteID;
                    $scope.GridRefresh('GridINV_Set_Bin');
                    break;
                case 'SubBin':
                    $scope.IV00011 = site;
                    $scope.SiteBins = [];
                    $http.get('/INV_Site/GetBinGroupBySiteID?KaizenPublicKey=' + sessionStorage.PublicKey + "&SiteID=" + site.SiteID).success(function (data) {
                        $scope.SiteBins = data;
                    }).finally(function () {
                        $("#Bins").data("kendoDropDownList").dataSource.read();
                    });
                    break;
                case 'ItemSite':
                    $scope.IV00011 = site;
                    $scope.IV00102.SiteID = site.SiteID;
                    $scope.GridRefresh('GridINV_SiteItems');
                    break;
                case 'ItemSiteBin':
                    $scope.IV00011 = site;
                    $scope.IV00130.SiteID = site.SiteID;
                    if ($scope.IV00130.SiteID != '' && $scope.IV00130.SiteID != undefined
                        && $scope.IV00130.ItemID != '' && $scope.IV00130.ItemID != undefined) {
                        $scope.GridRefresh('GridINV_SiteItemBins');
                    }
                    break;
                case 'ItemSiteSubBin':
                    $scope.IV00011 = site;
                    $scope.IV00131.SiteID = site.SiteID;
                    $http.get('/INV_Site/GetBinGroupBySiteID?KaizenPublicKey=' + sessionStorage.PublicKey + "&SiteID=" + site.SiteID).success(function (data) {
                        $scope.SiteBins = data;
                    }).finally(function () {
                        $("#Bins").data("kendoDropDownList").dataSource.read();
                    });
                    if ($scope.IV00131.SiteID != '' && $scope.IV00131.SiteID != undefined
                       && $scope.IV00131.ItemID != '' && $scope.IV00131.ItemID != undefined
                        && $scope.IV00131.BinID != '' && $scope.IV00131.BinID != undefined) {
                        $scope.GridRefresh('GridINV_SiteItemSubBins');
                    }
                    break
            }
        }
    };

    $scope.SaveBin = function () {
        $scope.IV00012.SiteID = $scope.IV00011.SiteID;
        $http.post('/INV_Site/SaveBinData', { 'IV00012': $scope.IV00012, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
            if (data.Status == true) {
                $.bigBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#739E73",
                    timeout: 4000,
                    icon: "fa fa-check shake animated"
                });
                $scope.GridRefresh('GridINV_Set_Bin');
                $scope.IV00012 = { Status: 1 };
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
    $scope.UpdateBin = function () {
        $scope.IV00012.SiteID = $scope.IV00011.SiteID;
        $http.post('/INV_Site/UpdateBinData', { 'IV00012': $scope.IV00012, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
            if (data.Status == true) {
                $.bigBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#739E73",
                    timeout: 4000,
                    icon: "fa fa-check shake animated"
                });
                $scope.GridRefresh('GridINV_Set_Bin');
                $scope.IV00012 = { Status: 1 };
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
    $scope.DeleteBin = function () {
        $.SmartMessageBox({
            title: "Delete Record",
            content: "Are you sure you want to delete this?",
            buttons: '[No][Yes]'
        }, function (ButtonPressed) {
            if (ButtonPressed === "Yes") {
                $http.post('/INV_Site/DeleteBinData', { 'IV00012': $scope.IV00012, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
                    if (data.Status == true) {
                        $.bigBox({
                            title: data.Massage,
                            content: data.Description,
                            color: "#739E73",
                            timeout: 4000,
                            icon: "fa fa-check shake animated"
                        });
                        $scope.GridRefresh('GridINV_Set_Bin');
                        $scope.IV00012 = { Status: 1 };
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

    //-------------------------- Sub Bin
    $scope.IV00021 = {};
    $scope.SetupSubBin = function () {
        removeEntityPage($scope.PagesIV00011);
        $scope.LoadIV00011Page('SetupSubBin');
        $scope.IV00021.Status = 1;
    };
    $scope.BinChanged = function () {
        $scope.GridRefresh('GridINV_Set_SubBin');
    };

    $scope.SaveSubBin = function () {
        $http.post('/INV_Site/SaveSubBinData', { 'IV00021': $scope.IV00021, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
            if (data.Status == true) {
                $.bigBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#739E73",
                    timeout: 4000,
                    icon: "fa fa-check shake animated"
                });
                $scope.GridRefresh('GridINV_Set_SubBin');
                $scope.IV00021 = { Status: 1, SiteID: $scope.IV00011.SiteID, BinID: $scope.IV00021.BinID };
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
    $scope.UpdateSubBin = function () {
        $http.post('/INV_Site/UpdateSubBinData', { 'IV00021': $scope.IV00021, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
            if (data.Status == true) {
                $.bigBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#739E73",
                    timeout: 4000,
                    icon: "fa fa-check shake animated"
                });
                $scope.GridRefresh('GridINV_Set_SubBin');
                $scope.IV00021 = { Status: 1, SiteID: $scope.IV00011.SiteID, BinID: $scope.IV00021.BinID };
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
    $scope.DeleteSubBin = function () {
        $.SmartMessageBox({
            title: "Delete Record",
            content: "Are you sure you want to delete this?",
            buttons: '[No][Yes]'
        }, function (ButtonPressed) {
            if (ButtonPressed === "Yes") {
                $http.post('/INV_Site/DeleteSubBinData', { 'IV00021': $scope.IV00021, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
                    if (data.Status == true) {
                        $.bigBox({
                            title: data.Massage,
                            content: data.Description,
                            color: "#739E73",
                            timeout: 4000,
                            icon: "fa fa-check shake animated"
                        });
                        $scope.GridRefresh('GridINV_Set_SubBin');
                        $scope.IV00021 = { Status: 1, SiteID: $scope.IV00011.SiteID, BinID: $scope.IV00021.BinID };
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

    //-------------------------- Site Items
    $scope.IV00102 = {};
    $scope.SiteItems = function () {
        removeEntityPage($scope.PagesIV00011);
        $scope.LoadIV00011Page('SiteItems');
    };

    //-------------------------- Site Item Bins
    $scope.IV00130 = {};
    $scope.SiteItemBins = function () {
        removeEntityPage($scope.PagesIV00011);
        $scope.LoadIV00011Page('SiteItemBins');
    };

    //-------------------------- Site Item SubBins
    $scope.IV00131 = {};
    $scope.SiteItemSubBins = function () {
        removeEntityPage($scope.PagesIV00011);
        $scope.LoadIV00011Page('SiteItemSubBins');
    };
    $scope.SelectedBinChanged = function () {
        if ($scope.IV00131.SiteID != '' && $scope.IV00131.SiteID != undefined
                      && $scope.IV00131.ItemID != '' && $scope.IV00131.ItemID != undefined
                       && $scope.IV00131.BinID != '' && $scope.IV00131.BinID != undefined) {
            $scope.GridRefresh('GridINV_SiteItemSubBins');
        }
    };

});
