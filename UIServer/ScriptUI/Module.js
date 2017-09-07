var app = angular.module("ApplicationModule", ['kendo.directives', 'angular.filter', 'ngTagsInput', 'ngPatternRestrict','dndLists']);

var removeEntityPage = function (elements) {
    elements.forEach(function (element, index) {
        elements.splice(index, 1);
        removeEntityPage(elements);
    });
};

var getFieldType = function (dataSource, field) {
    if (angular.isDefined(field)) {
        return dataSource.options.schema.model.fields[field].type;
    }
    return;
};
function addDays(date, days) {
    var result = new Date(date);
    result.setDate(result.getDate() + days);
    return result;
};
function removeSpecialChars(strVal) {
    return strVal.replace(/[^a-zA-Z 0-9]+/g, '');
};
function getdecimalPlaces(num) {
    var match = ('' + num).match(/(?:\.(\d+))?(?:[eE]([+-]?\d+))?$/);
    if (!match) { return 0; }
    return Math.max(
         0, (match[1] ? match[1].length : 0) - (match[2] ? +match[2] : 0));
};
//-------------------------------------- Date Format ------------------------------------
function kendoparsetoDate(s) {
    return kendo.parseDate(s, "dd/MM/yyyy");
};
var GetUserPhoto = function (UserName, PhotoExtension) {
    if (typeof PhotoExtension === 'undefined' || angular.isUndefined(PhotoExtension)
               || PhotoExtension === null || PhotoExtension === '')
        PhotoExtension = '/Photo/UserLogin/EmpCard.jpg';
    else
        PhotoExtension = '/Photo/UserLogin/' + kaizenTrim(UserName) + '.' + kaizenTrim(PhotoExtension);
    return PhotoExtension;
};
app.controller("MainController", function ($window, $scope, $rootScope, $timeout, $http, $interval, $filter) {
    if (typeof sessionStorage.PublicKey === 'undefined' || angular.isUndefined(sessionStorage.PublicKey) || sessionStorage.PublicKey === null || sessionStorage.PublicKey === '') {
        window.location.replace("/SysUser\\Login");
        return;
    }
    $scope.MY = {};
    $http.get('/SysUser/GetMy?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
        $scope.MY.CompanyID = data.CompanyID;
        $scope.MY.CompanyName = data.CompanyName;
        $scope.MY.SegmentMark = data.SegmentMark;
        $scope.MY.GLFormat = data.GLFormat;
        $scope.MY.YearCode = data.YearCode;
        $scope.MY.CurrencyCode = data.CurrencyCode;
        $scope.MY.ExchangeTableID = data.ExchangeTableID;
        $scope.MY.DecimalDigit = data.DecimalDigit;
        $scope.MY.ExchangeRateID = data.ExchangeRateID;
        $scope.MY.IsMultiply = data.IsMultiply;
        $scope.MY.ExchangeRate = data.ExchangeRate;
        $scope.MY.CheckbookCode = data.CheckbookCode;
        $scope.MY.CashAccountID = data.AccountID;
        $scope.MY.CashACTNUMBR = data.ACTNUMBR;
        $scope.MY.CashAccountName = data.AccountName;
        $scope.MY.SOPTypeSetupID = data.SOPTypeSetupID;
        $scope.MY.SOPTYPE = data.SOPTYPE;
        $scope.MY.SiteID = data.SiteID;
        $scope.MY.FullName = data.FullName;
        $scope.MY.UserName = data.UserName;
        $scope.MY.AgentID = data.AgentID;
        $scope.MY.PublicKey = data.PublicKey;
        $scope.MY.DashboardCode = data.DashboardCode;
        $scope.MY.CurrentDate = kendoparsetoDate(data.CurrentDate);
        $scope.MY.PhotoExtension = GetUserPhoto(data.UserName, data.PhotoExtension);

    }).finally(function () { });

    $http.get('/MainDashoard/GetMyDashboard?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
        $scope.DashboardList = data;
        //alert(JSON.stringify($scope.DashboardList, null, 2));
        //alert($scope.TaskList.TotalItemCount);
        if ($scope.DashboardList.length > 0)
            $scope.AddNewMainTab('Dashoard', 'Dashoard/MainDashoard', 'Dashoard');
    }).finally(function () { });

    //$http.get('/SysTask/GetAllAgentTasks?KaizenPublicKey=' + sessionStorage.PublicKey + '&AgentID=' + $scope.MY.AgentID).success(function (data) {
    //    $scope.TaskList = data;
    //    //alert(JSON.stringify($scope.TaskList, null, 2));
    //    //alert($scope.TaskList.TotalItemCount);
    //}).finally(function () { });


    $scope.CurrentUser = {};
    $scope.CurrentYear = {};
    $scope.PopUpObject = {};
    $scope.OpenLoopDetails = function (COntroler, DebtorID) {

        if (COntroler === "CMS_Debtor")
            $scope.OpenDebtorDetails(DebtorID);

    };
    $scope.ViewImagePopUp = function (data, ControllerID, ActionID) {
        $scope.PopUpObject = data;
        //alert(JSON.stringify($scope.PopUpObject, null, 2));
        $scope.PopUpWindow.center().toFront().open();
        $scope.PopUpWindow.refresh({
            url: "/" + ControllerID + "/" + ActionID + "?KaizenPublicKey=" + sessionStorage.PublicKey
        });
    };

    //-----------------------------------------------Master
    $scope.functiontofindIndexByKeyValue = function (arraytosearch, key, valuetosearch) {
        for (var i = 0; i < arraytosearch.length; i++) {
            //alert(arraytosearch[i][key]);
            if (arraytosearch[i][key] === valuetosearch) {
                return i;
            }
        }
        return null;
    };
    $scope.functiontofindObjectByKeyValue = function (arraytosearch, key, valuetosearch) {
        for (var i = 0; i < arraytosearch.length; i++) {
            // alert(arraytosearch[i][key])
            if (arraytosearch[i][key] === valuetosearch) {
                return arraytosearch[i];
            }
        }
        return null;
    };
    $scope.functiontofindAllObjectsByKeyValue = function (arraytosearch, key, valuetosearch) {
        return $scope.FindAll(arraytosearch, key, valuetosearch);
    };
    $scope.FindAll = function (arraytosearch, key, valuetosearch) {
        var returnArray = [];
        for (var i = 0; i < arraytosearch.length; i++) {
            if (arraytosearch[i][key] === valuetosearch) {
                returnArray.push(arraytosearch[i]);
            }
        }
        if (returnArray.length > 0)
            return returnArray;
        else
            return null;
    };
    $scope.GetSingle = function (arraytosearch, key, valuetosearch) {
        for (var i = 0; i < arraytosearch.length; i++) {
            if (arraytosearch[i][key] === valuetosearch) {
                return arraytosearch[i];
            }
        }
        return null;
    };
    $scope.GetSingleIndex = function (arraytosearch, key, valuetosearch) {
        //alert(JSON.stringify(arraytosearch, null, 2));
        for (var i = 0; i < arraytosearch.length; i++) {
            //alert(arraytosearch[i][key]);
            if (arraytosearch[i][key] === valuetosearch) {
                return i;
            }
        }
        return null;
    };

    $scope.kaizenToTrim = function (x) {
        if (x === null)
            return "";
        else
            return x.replace(/^\s+|\s+$/gm, '');
    };
    $scope.Formmaximize = function () {
        var widget = $("#widget-" + $scope.ToolBar.ActiveScreen.ID)// el.parents(".widget").eq(0);
        var button = widget.find('.widget-buttons *[data-toggle="maximize"]').find("i").eq(0);
        var compress = "fa-compress";
        var expand = "fa-expand";
        if (widget.hasClass("maximized")) {
            if (button) {
                button.addClass(expand).removeClass(compress);
            }
            widget.removeClass("maximized");
            widget.find(".widget-body").css("height", "auto");
        } else {
            if (button) {
                button.addClass(compress).removeClass(expand);
            }
            widget.addClass("maximized");
            if (widget) {
                var windowHeight = $(window).height();
                var headerHeight = widget.find(".widget-header").height();
                widget.find(".widget-body").height(windowHeight - headerHeight);
            }
        }
    };
    $scope.FormClose = function (FormID) {
        angular.forEach($scope.templates, function (story, key) {
            story.cssClass = 'tab-red';
            if (story.ID === FormID) {
                var link = document.getElementById(FormID);
                if (link !== null)
                    link.style.display = 'block';
                $scope.templates.splice(key, 1);
                return;
            }
        });
        if ($scope.templates.length > 0) {
            $scope.templates[$scope.templates.length - 1].cssClass = 'active';
            $scope.ToolBar.ActiveScreen = $scope.templates[$scope.templates.length - 1];
        }
    };
    $scope.FormCloseAll = function () {
        if ($scope.templates.length === 0) return;
        $scope.FormClose($scope.ToolBar.ActiveScreen.ID);
        $scope.FormCloseAll();
    };
    $scope.RefreshCurrentPage = function () {
        var ID = $scope.ToolBar.ActiveScreen.ID;
        var TabName = $scope.ToolBar.ActiveScreen.TabName;
        var CurrentWindow = $scope.ToolBar.ActiveScreen.CurrentWindow;
        $scope.FormClose(ID);
        $scope.AddNewMainTab(ID, TabName, CurrentWindow);
    };
    $scope.Formcollapse = function () {
        angular.forEach($scope.templates, function (story, key) {
            story.cssClass = '';
            if (story.ID === $scope.ToolBar.ActiveScreen.ID) {
                story.IsTab = false;
                $scope.ToolBar.ActiveScreen.IsTab = false;
            }
        });
        var found = $filter('filter')($scope.templates, { IsTab: true }, true);
        //alert(found.length);
        if (found.length > 0) {
            //alert(found[0].ID)
            angular.forEach($scope.templates, function (story, key) {
                story.cssClass = '';
                if (story.ID === found[0].ID) {
                    story.IsTab = true;
                    story.cssClass = 'active';
                    $scope.ToolBar.ActiveScreen = story;
                    $scope.ToolBar.ActiveScreen.IsTab = true;
                    return;
                }
            });
            // $scope.templates[$scope.templates.length - 1].cssClass = 'active';
            //  $scope.ToolBar.ActiveScreen = $scope.templates[$scope.templates.length - 1];
            // $scope.ToolBar.ActiveScreen.IsTab = false;
        }
    };
    $scope.kaixnss = function () {
        //alert('ssssssss');
        //var ActiveScreenID = $("#ActiveScreenID").val();
        //alert(ActiveScreenID);
        //resizeGrid("Grid" + ActiveScreenID);
    };
    //----------------------------------------------------------------------------------------Form
    $scope.ToolBar = {};
    $scope.ToolBar.ActiveScreen = null;

    $scope.$on('$includeContentRequested', function (event) {
        //waitingDialog.show();
    });
    $scope.$on('$includeContentLoaded', function (event) {
        // waitingDialog.hide();
    });
    $scope.$on('$includeContentError', function (event) {
        console.log("in includeContentError");
    });

    $scope.templates = [];
    $scope.AddNewMainTab = function (ID, URIPath, tabName, currentWindow) {
        //alert(URIPath);
        if (currentWindow == '' || currentWindow == null || currentWindow == undefined || angular.isUndefined(currentWindow))
            currentWindow = 'MainGrid';
        //alert(currentWindow);
        //var link = document.getElementById(ID);
        //if (link !== null)
        //    link.style.display = 'none';
        //alert(link.style.display);
        //wael22
        $scope.FormClose(ID);
        if ($scope.templates.length > 0)
            $scope.templates[$scope.templates.length - 1].cssClass = '';

        // angular.forEach($scope.templatesDropDown, function (story, key) { story.cssClass = 'tab-red'; });
        //alert(ID);
        var URIPath = "/" + URIPath + "/Index?KaizenPublicKey=" + sessionStorage.PublicKey;

        var template = {
            url: URIPath, ID: ID, TabName: tabName, IsTab: true,
            CurrentWindow: currentWindow, cssClass: 'active',
            CurrentView: ''
        };
        $scope.ToolBar.ActiveScreen = template;
        $scope.templates.push(template);
        //alert($scope.ToolBar.ActiveScreen.ID)
        angular.forEach($scope.templates, function (story, key) {
            story.cssClass = '';
            if (story.ID === ID) {
                story.cssClass = 'active';
                $scope.ToolBar.ActiveScreen = story;
                return;
            }
        });
    };
    $scope.AddNewSubTab = function (ID, HandlerName, tabName) {
        var link = document.getElementById(ID);
        if (link !== null)
            link.style.display = 'none';
        if ($scope.templates.length > 0)
            $scope.templates[$scope.templates.length - 1].cssClass = '';
        var URIPath = "/" + ID + "/" + HandlerName + "?KaizenPublicKey=" + sessionStorage.PublicKey;
        var template = { url: URIPath, ID: ID, TabName: tabName, cssClass: 'active' };
        $scope.ToolBar.ActiveScreen = template;
        $scope.templates.push(template);
        angular.forEach($scope.templates, function (story, key) {
            story.cssClass = '';
            if (story.ID === ID) {
                story.cssClass = 'active';
                $scope.ToolBar.ActiveScreen = story;
                return;
            }
        });
    };
    $scope.RemoveTab = function (ID) {
        $scope.delete(ID);
    };
    $scope.delete = function (ID) {
        removeEntity($scope.templates, ID);
    };
    var removeEntity = function (elements, ID) {
        elements.forEach(function (element, index) {
            if (element.ID === ID) {
                elements.splice(index, 1);
                removeEntity(elements, ID);
            }
        })
    };
    $scope.ReloadMainTab = function () {
        var ID = $scope.ToolBar.ActiveScreen.ID;
        var dropdownlist = $("#GridViewoption_" + ID).data("kendoDropDownList");
        $scope.delete(ID);
        angular.forEach($scope.templatesDropDown, function (story, key) { story.cssClass = 'tab-red'; });
        var URIPath = "/" + ID + "/Index?KaizenPublicKey=" + sessionStorage.PublicKey + "&ViewID=" + dropdownlist.value();
        var template = { url: URIPath, ID: ID, TabName: $scope.ToolBar.ActiveScreen.TabName, cssClass: 'active' };
        $scope.ToolBar.ActiveScreen = template;
        $scope.templates.push(template);
        angular.forEach($scope.templates, function (story, key) {
            story.cssClass = 'tab-red';
            if (story.ID === ID) {
                story.cssClass = 'active';
                $scope.ToolBar.ActiveScreen = story;
                return;
            }
        });
    };
    //-------------------------------------------------------------
    $scope.templatesDropDown = [];
    $scope.AddNewDropdownTab = function (ID) {
        var temp = $scope.templates[$scope.templates.length - 2];
        angular.forEach($scope.templates, function (story, key) {
            if (story.ID === ID) {
                $scope.templatesDropDown.push(story);
                $scope.delete(ID);
                temp.cssClass = 'active';
                $scope.ToolBar.ActiveScreen = temp;
                //alert(temp);
                //alert(temp.ID);
                document.getElementById("tt" + temp.ID).click();
                //document.getElementById("tab" + temp.ID + "info").click();
                return;
            }
        });
    };

    //-------------------------------------------------------------
    $scope.filterFunction = function (element) {
        return element.IsTab;
    };
    $scope.dropdownMenueFilter = function (element) {
        //alert(element.IsTab);
        return !element.IsTab;
    };
    $scope.ActivateTab = function (ActiveScreen) {
        angular.forEach($scope.templates, function (story, key) {
            story.cssClass = '';
            if (story.ID === ActiveScreen.ID) {
                story.cssClass = 'active';
            }
        });
        ActiveScreen.cssClass = 'active';
        $scope.ToolBar.ActiveScreen = ActiveScreen;
    };
    $scope.ActivateDropDownTab = function (ActiveScreen) {
        //alert('ActivateDropDownTab');
        ActiveScreen.cssClass = 'active';
        ActiveScreen.IsTab = false;
        $scope.ToolBar.ActiveScreen = ActiveScreen;
        // angular.forEach($scope.templates, function (story, key) { story.cssClass = 'tab-red'; });
        angular.forEach($scope.templates, function (story, key) {
            story.cssClass = '';
            if (story.ID === ActiveScreen.ID) {
                story.cssClass = 'active';
                story.IsTab = true;

            }
        });
    };
    //-------------------------------------------------------------
    $scope.ShowMessageError = function (Titil, Msg) {
        dialogs.error(Titil, Msg);
    };
    $scope.ShowMessageNotify = function (Titil, Msg) {
        dialogs.error(Titil, Msg);
    };
    $scope.ShowMessageConfirm = function (Titil, Msg) {
        var dlg = dialogs.confirm();
        dlg.result.then(function (btn) {
            $scope.confirmed = 'You confirmed "Yes."';
        }, function (btn) {
            $scope.confirmed = 'You confirmed "No."';
        });
    };



    $scope.isLastpane = function (templatepane) {
        var cssClass = templatepane.cssClass === 'active' ? 'tab-pane in active' : 'tab-pane';
        return cssClass;
    };
    $scope.IsdropdownActive = function (templatepane) {
        var cssClass = templatepane.cssClass === 'active' ? 'tab-pane in active' : 'tab-pane';
        return cssClass;
    };



    $scope.ExportScreen = function (DIVCOntainer, PrintType, ref) {
        if (PrintType === "PDF") {
            kendo.drawing.drawDOM($("#" + DIVCOntainer))
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
                    fileName: ref + ".pdf",
                    proxyURL: "@Url.Action('Pdf_Export_Save')"
                });
            });
        }
        else if (PrintType === "Image") {
            // Convert the DOM element to a drawing using kendo.drawing.drawDOM
            kendo.drawing.drawDOM($("#" + DIVCOntainer))
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
                    fileName: ref + ".png",
                    proxyURL: "@Url.Action('Pdf_Export_Save')"
                });
            });
        }
        else if (PrintType === "SVG") {
            kendo.drawing.drawDOM($("#" + DIVCOntainer))
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
                    fileName: ref + ".svg",
                    proxyURL: "@Url.Action('Pdf_Export_Save')"
                });
            });
        }
    };
    $scope.SaveAsPdf = function (GridID) {
        if (typeof GridID === 'undefined')
            GridID = "Grid" + $scope.ToolBar.ActiveScreen.ID;
        var grid = $("#" + GridID).data("kendoGrid");

        var pageSize = grid.dataSource.pageSize();
        grid.dataSource.pageSize(1500);
        grid.saveAsPDF();
        grid.dataSource.pageSize(pageSize);
    };
    $scope.SaveAsExcel = function (GridID) {
        if (typeof GridID === 'undefined')
            GridID = "Grid" + $scope.ToolBar.ActiveScreen.ID;
        var grid = $("#" + GridID).data("kendoGrid");
        var pageSize = grid.dataSource.pageSize();
        grid.dataSource.pageSize(1500);
        grid.saveAsExcel();
        grid.dataSource.pageSize(pageSize);
    };
    $scope.GridRefresh = function (GridID) {
        if (typeof GridID === 'undefined')
            GridID = "Grid" + $scope.ToolBar.ActiveScreen.ID;

        var grid = $("#" + GridID).data("kendoGrid");
        grid.dataSource.read();
    };
    $scope.PivotGridRefresh = function (GridID) {
        if (typeof GridID === 'undefined')
            GridID = "Grid" + $scope.ToolBar.ActiveScreen.ID;

        var grid = $("#" + GridID).data("kendoPivotGrid");
        grid.dataSource.read();
    };
    $scope.GridRefreshWithCallback = function (GridID, _callback) {
        if (typeof GridID === 'undefined')
            GridID = "Grid" + $scope.ToolBar.ActiveScreen.ID;

        var grid = $("#" + GridID).data("kendoGrid");
        grid.dataSource.read();
        _callback();
    };

    $scope.RefreshGridGenericPopUp = function (gridstr) {
        var grid = $("#" + gridstr).data("kendoGrid");
        grid.dataSource.read();
    };

    $scope.SaveGrid = function (ViewID) {
        // alert(ViewID);
        var grid = $("#Grid" + $scope.ToolBar.ActiveScreen.ID).data("kendoGrid");
        var dataSource = grid.dataSource;
        var state = {
            columns: grid.columns,
            page: dataSource.page(),
            pageSize: dataSource.pageSize(),
            sort: dataSource.sort(),
            filter: dataSource.filter()
        };
        var url = "/Sys_View/SaveGrid";
        $.ajax({
            url: url, data: {
                ViewID: ViewID, state: JSON.stringify(state)
            }, success: DataRetrieved, type: 'POST', dataType: 'json'
        });
        //$.ajax({
        //    url: url, data: {
        //        ViewID: 1, GridOptions: kendo.stringify(grid.getOptions()), state: JSON.stringify(state)
        //    }, success: DataRetrieved, type: 'POST', dataType: 'json'
        //});
    };
    function DataRetrieved(data) {
        //alert('dddddd')
    };
    $scope.LoadGrid = function (ViewID) {
        var grid = $("#Grid" + $scope.ToolBar.ActiveScreen.ID).data("kendoGrid");
        var dataSource = grid.dataSource;
        $.ajax({
            url: "/Sys_View/LoadGrid", data: { ViewID: ViewID },
            success: function (state) {
                if (typeof state.Status !== "undefined") {
                    //alert(state.Massage);
                    grid.dataSource.read();
                    return;
                }
                if (typeof state.filter === "undefined") {
                    grid.dataSource.read();
                    return;
                }
                state = JSON.parse(state);
                // alert(JSON.stringify(state.filter, null, 2));
                //grid.dataSource.sort(state.sort);
                grid.dataSource.filter(state.filter);
                //grid.dataSource.page(state.page);
                //var options = grid.options;
                //options.columns = state.columns;
                //options.dataSource = dataSource;
                //options.dataSource.page(state.page);
                //options.dataSource.pageSize(state.pageSize);
                //options.dataSource.sort(state.sort);
                //options.dataSource.filter(state.filter);
                ////options.dataSource.group(state.group);
                //grid.destroy();
                //$("#Grid" + $scope.ToolBar.ActiveScreen.ID)
                //   .empty()
                //   .kendoGrid(options);
            }
        });
    };
    $scope.SaveGridAS = function () {

    };
    $scope.GetGridFilters = function (GridID) {
        var grid = $("#" + GridID).data("kendoGrid");
        var filterObj = grid.dataSource.filter();
        if (filterObj) {
            var logic = "";
            if (filterObj.logic)
                logic = filterObj.logic;
            var sqlQuery = "", str = "";
            var field = "", operatorValue = "", operator = "", value = "";
            //sqlQuery = "Select * from CM00025";
            if (filterObj.filters && filterObj.filters.length > 0 && logic) {
                var filters = filterObj.filters;
                //sqlQuery = sqlQuery + " Where";
                for (var i = 0; i < filters.length; i++) {
                    if (filters[i].filters && filters[i].logic) {
                        sqlQuery = sqlQuery + " (";
                        var filtersNested = filters[i].filters;
                        for (var j = 0; j < filtersNested.length; j++) {
                            sqlQuery = sqlQuery + $scope.GetCondition(filtersNested[j], filtersNested[j].logic);
                            if (j != filtersNested.length - 1)
                                sqlQuery = sqlQuery + " " + filters[i].logic;
                        }
                        sqlQuery = sqlQuery + ")";
                    } else {
                        str = $scope.GetCondition(filters[i], logic);
                        sqlQuery = sqlQuery + str;
                    }
                    if (i != filters.length - 1)
                        sqlQuery = sqlQuery + " " + logic;
                }
                //alert(sqlQuery);
                return sqlQuery;
            }
        }
    };
    $scope.GetCondition = function (filters, logic) {
        var sqlQuery = "";
        var field = filters.field;
        var operatorValue = filters.operator;
        var value = filters.value;
        if (operatorValue == "eq") {
            if (typeof value === 'number')
                sqlQuery = sqlQuery + " " + field + "=" + value;
            else if (typeof value === 'string')
                sqlQuery = sqlQuery + " " + field + "='" + value + "'";
            else
                sqlQuery = sqlQuery + " " + field + "='" + value + "'";
        } else if (operatorValue == "startswith") {
            sqlQuery = sqlQuery + " " + field + " like '" + value + "%'";
        }
        return sqlQuery;
    };


    //format Date and Time Now
    $scope.format_Fulldate = 'EEEE , MMMM , yyyy';
    $scope.format_date = 'dd MM yyyy';
    $scope.format_time = 'hh:mm:ss a';

    //--------------------------------------- chatting ---------------------------
    $scope.ChatRooms = [];
    $scope.AddChatRoom = function (user) {
        var IsNewTab = true;
        angular.forEach($scope.templates, function (story, key) {
            if (story.ID === 'Admin_Chat') {
                IsNewTab = false;
                return;
            }
        });
        if (IsNewTab)
            $scope.AddNewMainTab('Admin_Chat', 'Admin_Chat');
        $scope.ChatRooms.push(user);

        if (typeof user.ChattingMessage === 'undefined' || angular.isUndefined(user.ChattingMessage)
                  || user.ChattingMessage === null || user.ChattingMessage === '') {

            $http.get('/SysUser/GetLastChatttingMessage?UserNameTo=' + user.UserName + "&KaizenPublicKey=" + sessionStorage.PublicKey).success(function (data) {
                user.ChattingMessage = data;
            }).finally(function () {
                angular.forEach(user.ChattingMessage, function (story, key) {
                    if (story.IsReceived)
                        story.PhotoExtension = user.PhotoExtension
                    else
                        story.PhotoExtension = $scope.CurrentUser.PhotoExtension
                });
            });
        }

        angular.forEach($scope.UsersList, function (story, key) {
            if (kaizenTrim(story.UserName) === kaizenTrim(user.UserName)) {
                $scope.UsersList.splice(key, 1);
                return;
            }
        });
        //if ($scope.ChatRooms.length == 5)
        //    $scope.ChatRooms.splice(0, 1);
    };
    $scope.CLoseChatingRoom = function (UserName) {
        angular.forEach($scope.ChatRooms, function (story, key) {
            if (kaizenTrim(story.UserName) === kaizenTrim(UserName)) {
                $scope.UsersList.push(story);
                $scope.ChatRooms.splice(key, 1);
                return;
            }
        });
        if ($scope.ChatRooms.length === 0)
            $scope.FormClose();
    };
    $scope.SendChatMessage = function (user) {
        mainHubObject.server.sendChatMessage(sessionStorage.PublicKey, user.UserName, user.KaizenMessageLine);
    };

    $scope.GridOperators = [
    { type: "string", operator: "eq", text: "Is Equal To" },
    { type: "string", operator: "neq", text: "Is Not Equal To" },
    { type: "string", operator: "startswith", text: "Starts With" },
    { type: "string", operator: "endswith", text: "Ends With" },
    { type: "string", operator: "contains", text: "Contains" },
    { type: "string", operator: "doesnotcontain", text: "Does Not Contain" },
    { type: "number", operator: "eq", text: "Is Equal To" },
    { type: "number", operator: "neq", text: "Is Not Equal To" },
     { type: "number", operator: "gt", text: "Is Greater Than" },
    { type: "number", operator: "gte", text: "Is Greater Than Or Equal To" },
     { type: "number", operator: "lt", text: "Is Less Than" },
    { type: "number", operator: "lte", text: "Is Less Than Or Equal To" },
    { type: "date", operator: "eq", text: "Is Equal To" },
    { type: "date", operator: "neq", text: "Is Not Equal To" },
     { type: "date", operator: "gt", text: "Is After" },
    { type: "date", operator: "gte", text: "Is After Or Equal To" },
     { type: "date", operator: "lt", text: "Is Before" },
    { type: "date", operator: "lte", text: "Is Before Or Equal To" },
    { type: "dropdown", operator: "eq", text: "Is Equal To" },
    { type: "dropdown", operator: "neq", text: "Is Not Equal To" }
    ];
    $scope.OpenCustomPage = function (TaskID, MenuName, ScreenPath) {
        var window = $("#CustomDialog");
        window.kendoWindow({
            actions: ["Pin", "Refresh", "Maximize", "Minimize", "Close"],
            content: ScreenPath,
            title: MenuName,
            width: "1200",
            height: "800",
            draggable: true,
            modal: false,
            iframe: true
        });
        window.data("kendoWindow").open();
        window.data("kendoWindow").maximize();

        //var URIPath = "/Home/CustomPage?KaizenPublicKey=" + sessionStorage.PublicKey;
        //var template = { url: URIPath, ID: TaskID, TabName: MenuName, cssClass: 'active' };
        //$scope.ToolBar.ActiveScreen = template;
        //$scope.ToolBar.ActiveScreen.ScreenPath = ScreenPath;
        //$scope.templates.push(template);
        //angular.forEach($scope.templates, function (story, key) {
        //    story.cssClass = 'tab-red';
        //    if (story.ID == TaskID) {
        //        story.cssClass = 'active';
        //        $scope.ToolBar.ActiveScreen = story;
        //        return;
        //    }
        //});
    };
    $scope.TT = function (ControllerID, ActionID, Parm) {
        $scope.PopUpObject = Parm;
        var title = '';
        $("#MainDialog").kendoWindow({
            appendTo: "#DIVMaindialog",
            actions: ["Custom", "Pin", "Refresh", "Maximize", "Minimize", "Close"],
            content: "/" + ControllerID + "/" + ActionID + "?KaizenPublicKey=" + sessionStorage.PublicKey,
            title: title,
            width: 700,
            height: "400px",
            modal: true,
            iframe: false,
            visible: false,
            // close: $scope.FormCloseAll,
            close: function (e) {
                //alert(e);
            }
        });
        var win = $("#MainDialog").data("kendoWindow");
        win.center();
        win.toFront();
        win.open();
    };
    $scope.TransferObject = {};
    $scope.OpenkendoWindow = function (ControllerID, Parm, CurrentControl) {

        if (angular.isObject(Parm)) {
            $scope.ParmObject = {};
        }
        else {
            $scope.ParmObject = "";
        }
        $scope.ParmObject = Parm;
        $scope.CurrentControl = CurrentControl;
        var title = '';
        switch (ControllerID) {
            case "CMS_CaseLookUp":
                title = 'Case Transaction List';
                break;
            case "SOP_Customer":
                title = 'Customer LookUp';
                break;
            case "GL_Currency":
                title = 'Currency LookUp';
                break;
            case "HR_Co_Employee":
                title = 'Employee LookUp';
                break;
        }
        $("#MainDialog").kendoWindow({
            appendTo: "#DIVMaindialog",
            actions: ["Refresh", "Maximize", "Close"],
            content: "/LookUp/" + ControllerID + "/Index?KaizenPublicKey=" + sessionStorage.PublicKey,
            title: title,
            width: 900,
            height: "500px",
            modal: true,
            iframe: false,
            visible: false,
            close: function (e) {
            }
        });
        var win = $("#MainDialog").data("kendoWindow");
        win.center().toFront().open();
    };

    $scope.OpenkendoWindowForm = function (modulepath, ControllerID, ActionID, Parm, CurrentControl) {
        if (angular.isObject(Parm)) {
            $scope.ParmObject = {};
        }
        else {
            $scope.ParmObject = "";
        }
        $scope.ParmObject = Parm;
        //alert(Parm);
        $scope.CurrentControl = CurrentControl;
        var title = '';
        switch (ControllerID) {
            case "SOP_Customer":
                title = 'Customer LookUp';
                break;
            case "GL_Currency":
                title = 'Currency LookUp';
                break;
            case "HR_Co_Employee":
                title = 'Employee LookUp';
                break;
        }
        //alert('OpenkendoWindowForm');
        $("#MainDialog").kendoWindow({
            appendTo: "#DIVMaindialog",
            actions: ["Custom", "Pin", "Refresh", "Maximize", "Minimize", "Close"],
            content: "/" + modulepath + "/" + ControllerID + "/" + ActionID + "?KaizenPublicKey=" + sessionStorage.PublicKey,
            title: title,
            width: 900,
            height: "550px",
            modal: true,
            iframe: false,
            visible: false,
            // close: $scope.FormCloseAll,
            close: function (e) {
                //alert(e);
            }
        });
        var win = $("#MainDialog").data("kendoWindow");
        win.center().toFront().open();
    };
    $scope.UserLogout = function () {
        $.SmartMessageBox({
            title: "<i class='fa fa-sign-out txt-color-orangeDark'></i> Logout <span class='txt-color-orangeDark'><strong>" + $scope.MY.UserName + "</strong></span> ?",
            content: "Are you sure you want to Logout?",
            buttons: '[No][Yes]'
        }, function (ButtonPressed) {
            if (ButtonPressed === "Yes") {
                $http.get('/SysUser/Logout?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
                    window.location.replace("/SysUser\\Login");
                }).finally(function () { });
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



    //----------------------------- GO Online
    $scope.TaskList = [];
    $scope.UsersList = [];
    var mainHubObject = $.connection.mainHub;

    $.connection.hub.start().done(function () {
        //console.log("hub started");
        mainHubObject.server.newLoginUser(sessionStorage.PublicKey);
        $http.get('/SysUser/GetOnlineUsers?KaizenPublicKey=' + sessionStorage.PublicKey)
            .success(function (data) {
                //console.log("GetOnlineUsers Success", data);
                $scope.UsersList = data;
                //alert(JSON.stringify($scope.UsersList, null, 2));
            }).finally(function () {
                angular.forEach($scope.UsersList, function (tempUser, key) {
                    tempUser.PhotoExtension = GetUserPhoto(tempUser.UserName, tempUser.PhotoExtension);
                    var parsedstart = new Date(parseInt(tempUser.LoginDate.toString().substr(6)));
                    tempUser.LoginDate = new Date(parsedstart);
                });
            });
    });
    mainHubObject.client.OnUserOnline = function (NewUserLoged) {
        //console.log("mainHubObject.client.OnUserOnline =>");
        //console.log(NewUserLoged);
        $scope.$apply(function () {
            NewUserLoged.PhotoExtension = GetUserPhoto(NewUserLoged.UserName, NewUserLoged.PhotoExtension);
            $scope.UsersList.push(NewUserLoged);
        });
    };
    mainHubObject.client.OnUserOffline = function (NewUserLoged) {
        //console.log("mainHubObject.client.OnUserOffline =>");
        //console.log(NewUserLoged);
        $scope.$apply(function () {
            for (var i = 0; i < $scope.UsersList.length; i++) {
                var story = $scope.UsersList[i];
                if (kaizenTrim(story.UserName) === kaizenTrim(NewUserLoged.UserName)) {
                    $scope.UsersList.splice(i, 1);
                    return;
                }
            }
        });
    };

    mainHubObject.client.lockUserOut = function () {
        console.log("mainHubObject.client.lockUserOut =>", NewUserLoged);

        //var x = document.getElementsByTagName("BODY")[0];
        $scope.UsersList = [];
        $scope.MY = {};
        sessionStorage.PublicKey = null;
        $.connection.hub.stop();
        window.location.replace("/SysUser\\Login");
    };
    var tryingToReconnect = false;
    $.connection.hub.disconnected(function () {
        if (tryingToReconnect) {
            console.log("notifyUserOfDisconnect");
        } else {
            setTimeout(function () {
                $.connection.hub.start();
            }, 5000); // Restart connection after 5 seconds.
        }
        if ($.connection.hub.lastError) {
            console.log("$.connection.hub.lastError " + $.connection.hub.lastError);
        }

        window.location.replace("/SysUser\\Login");
    });

    $.connection.hub.reconnecting(function () {
        tryingToReconnect = true;
    });
    $.connection.hub.reconnected(function () {
        tryingToReconnect = false;
    });

    $scope.openchat = function (divId) {
        var c = $('#usr' + divId),
        d = c.attr("data-chat-id"),
        e = c.attr("data-chat-fname"),
        f = c.attr("data-chat-lname"),
        g = c.attr("data-chat-status") || "online",
        h = c.attr("data-chat-alertmsg"),
        i = c.attr("data-chat-alertshow") || !1;
        chatboxManager.addBox(d, {
            "title": "username" + d, "first_name": e, "last_name": f, "status": g, "alertmsg": h, "alertshow": i
        , messageSent: function (id, user, msg) {
            alert(user.first_name + ": " + msg);
            // this.boxManager.addMsg(user.first_name, msg);
        }
        })

    };
    $scope.SendMessage = function (toId, message) {
        mainHubObject.server.sendMessage(sessionStorage.PublicKey, message, toId);
    }
    mainHubObject.client.RecieveMessage = function (message, UserNameFrom) {

        $("#" + UserNameFrom).chatbox("option", "boxManager").addMsg(UserNameFrom, message);
        var newMsgSound = new Audio("/sound/voice_on.ogg");
        newMsgSound.loop = false;
        //newMsgSound.play();
        //$('#' + UserNameFrom).chatbox('option', "boxManager").highlightBox();
        //if (windowFocus == false) {
        //    showNotification(newMsgSound + ': ' + message, 'img/chat.png', UserNameFrom + ' sent New Message');
        //}
    };
    //---------------------------------------------------------------------------
    /// Filter  // Updatting BY wael
    $scope.Filter = { field: -1, operator: '', value: '' };
    $scope.MYFilter = {};
    $scope.Columns = [];
    $scope.Grid = {}; // { ID : ssss , Filter : []  , Get }
    $scope.Grid.Filter = [];
    $scope.Grids = [];
    $scope.OpenFilterWindow = function (grid, Screen) {
        $scope.Grid.ID = grid;
        $scope.Grid.Screen = Screen;
        $scope.Filter = { field: -1, operator: '', value: '' };
        $scope.GridFilterWindow.center().toFront().open();
        $scope.GridFilterWindow.refresh();
        var gridmain = $("#" + $scope.Grid.ID).data("kendoGrid"); // GridCMS_Case
        $scope.Columns = gridmain.columns;
        var dataSource = gridmain.dataSource;
        // Removing CHeck BOx Collumns 
        if ($scope.functiontofindIndexByKeyValue($scope.Columns, "title", "Select") !== null) {
            var removedcolumn = $scope.functiontofindIndexByKeyValue($scope.Columns, "title", "Select");
            $scope.Columns.splice(removedcolumn, 1);
        }
        $scope.Columns.forEach(function (element, index) {
            if (angular.isDefined(element.field)) {
                if (angular.isDefined(element.filterable)) {
                    element.type = "dropdown";
                } else {
                    element.type = getFieldType(dataSource, element.field);
                }
            }
        })

        var found = $filter('filter')($scope.Grids, { ID: $scope.Grid.ID }, true);
        if (found.length) {
            //alert(JSON.stringify(found[0]));
            $scope.Grid.Filter = found[0].Filter;
        }
        //alert(JSON.stringify($scope.GridFilters));
        //$scope.GridFilters = $scope.TT.GetFilter();
        //alert(JSON.stringify($scope.GridFilters));
    };

    $scope.AddFilter = function () {
        if ($scope.Filter.operator !== '' && angular.isDefined($scope.Filter.operator)
            && $scope.Filter.field !== '' && angular.isDefined($scope.Filter.field)) {
            if ($scope.Filter.SelectedColumn.type === 'string'
            && $scope.Filter.strvalue !== '' && angular.isDefined($scope.Filter.strvalue)) {
                $scope.Filter.value = $scope.Filter.strvalue;
                if ($scope.Filter.operator === "eq") {
                    $scope.Filter.FilterText = $scope.Filter.field + " Is Equal To '" + $scope.Filter.value.trim() + "'";
                }
                else if ($scope.Filter.operator === "neq") {
                    $scope.Filter.FilterText = $scope.Filter.field + " Is Not Equal To '" + $scope.Filter.value.trim() + "'";
                }
                else if ($scope.Filter.operator === "startswith") {
                    $scope.Filter.FilterText = $scope.Filter.field + " Starts With '" + $scope.Filter.value.trim() + "'";
                }
                else if ($scope.Filter.operator === "endswith") {
                    $scope.Filter.FilterText = $scope.Filter.field + " Ends With '" + $scope.Filter.value.trim() + "'";
                }
                else if ($scope.Filter.operator === "contains") {
                    $scope.Filter.FilterText = $scope.Filter.field + " Contains '" + $scope.Filter.value.trim() + "'";
                }
                else if ($scope.Filter.operator === "doesnotcontain") {
                    $scope.Filter.FilterText = $scope.Filter.field + " Does Not Contains '" + $scope.Filter.value.trim() + "'";
                }
            }
            else if ($scope.Filter.SelectedColumn.type === 'number'
                && $scope.Filter.numvalue !== '' && angular.isDefined($scope.Filter.numvalue)) {
                $scope.Filter.value = $scope.Filter.numvalue;
                if ($scope.Filter.operator === "lt") {
                    $scope.Filter.FilterText = $scope.Filter.field + " Is Less Than '" + $scope.Filter.value + "'";
                }
                else if ($scope.Filter.operator === "lte") {
                    $scope.Filter.FilterText = $scope.Filter.field + " Is Less Than Or Equal To '" + $scope.Filter.value + "'";
                }
                else if ($scope.Filter.operator === "gt") {
                    $scope.Filter.FilterText = $scope.Filter.field + " Is Greater Than '" + $scope.Filter.value + "'";
                }
                else if ($scope.Filter.operator === "gte") {
                    $scope.Filter.FilterText = $scope.Filter.field + " Is Greater Than Or Equal To '" + $scope.Filter.value + "'";
                }
                else if ($scope.Filter.operator === "eq") {
                    $scope.Filter.FilterText = $scope.Filter.field + " Is Equal To '" + $scope.Filter.value + "'";
                }
                else if ($scope.Filter.operator === "neq") {
                    $scope.Filter.FilterText = $scope.Filter.field + " Is Not Equal To '" + $scope.Filter.value + "'";
                }
            }
            else if ($scope.Filter.SelectedColumn.type === 'date'
                && $scope.Filter.dtvalue !== '' && angular.isDefined($scope.Filter.dtvalue)) {
                $scope.Filter.value = $scope.Filter.dtvalue;
                if ($scope.Filter.operator === "lt") {
                    $scope.Filter.FilterText = $scope.Filter.field + " Is Before '" + $scope.Filter.value + "'";
                }
                else if ($scope.Filter.operator === "lte") {
                    $scope.Filter.FilterText = $scope.Filter.field + " Is Before Or Equal To '" + $scope.Filter.value + "'";
                }
                else if ($scope.Filter.operator === "gt") {
                    $scope.Filter.FilterText = $scope.Filter.field + " Is After '" + $scope.Filter.value + "'";
                }
                else if ($scope.Filter.operator === "gte") {
                    $scope.Filter.FilterText = $scope.Filter.field + " Is After Or Equal To '" + $scope.Filter.value + "'";
                }
                else if ($scope.Filter.operator === "eq") {
                    $scope.Filter.FilterText = $scope.Filter.field + " Is Equal To '" + $scope.Filter.value + "'";
                }
                else if ($scope.Filter.operator === "neq") {
                    $scope.Filter.FilterText = $scope.Filter.field + " Is Not Equal To '" + $scope.Filter.value + "'";
                }
            }
            else if ($scope.Filter.SelectedColumn.type === 'dropdown'
                && $scope.Filter.ddlvalue !== '' && angular.isDefined($scope.Filter.ddlvalue)) {
                $scope.Filter.value = $scope.Filter.ddlvalue;
                if ($scope.Filter.operator === "eq") {
                    $scope.Filter.FilterText = $scope.Filter.field + " Is Equal To '" + $scope.Filter.value + "'";
                }
                else if ($scope.Filter.operator === "neq") {
                    $scope.Filter.FilterText = $scope.Filter.field + " Is Not Equal To '" + $scope.Filter.value + "'";
                }
            }
            else if ($scope.Filter.SelectedColumn.type === 'boolean'
            && $scope.Filter.bvalue !== '' && angular.isDefined($scope.Filter.bvalue)) {
                if ($scope.Filter.operator === "eq") {
                    $scope.Filter.FilterText = $scope.Filter.field + " Is Equal To '" + $scope.Filter.value + "'";
                }
                else if ($scope.Filter.operator === "neq") {
                    $scope.Filter.FilterText = $scope.Filter.field + " Is Not Equal To '" + $scope.Filter.value + "'";
                }
            }
            $scope.Grid.Filter.push($scope.Filter);
            $scope.Filter = {};
        }
    };
    $scope.RemoveFilter = function (index) {
        $scope.Grid.Filter.splice(index, 1);
    };
    $scope.ApplyFilters = function () {
        var grid = $("#" + $scope.Grid.ID).data("kendoGrid"); // GridCMS_Case
        $scope.GridFilterWindow.close();
        //total = grid.dataSource.total();
        //grid.dataSource.pageSize(total);
        grid.dataSource.filter($scope.Grid.Filter);

        var found = $filter('filter')($scope.Grids, { ID: $scope.Grid.ID }, true);
        if (found.length === 0) {
            $scope.Grids.push($scope.Grid);
        }
        $scope.Grid = {};
        $scope.Grid.Filter = [];
    };
    $scope.ColumnChanged = function () {
        if ($scope.Filter.SelectedColumn.type === 'dropdown') {
            var DS = $scope.MYFilter.get($scope.Filter.SelectedColumn.field);
            $scope.Filter.SelectedColumn.key = DS.key;
            $scope.Filter.SelectedColumn.field = $scope.Filter.SelectedColumn.field;
            $scope.Filter.SelectedColumn.source = DS.source;
            $scope.GridFilterWindow.refresh();

            //alert($scope.Filter.SelectedColumn.field);
            //alert(JSON.stringify($scope.Filter.SelectedColumn.source));
        }
        $scope.Filter.field = $scope.Filter.SelectedColumn.field;
    };
    //---------------------------------------------------------------------------
    $scope.DownloadFile = function (filename, destination) {
        var ifr = document.createElement('iframe');
        ifr.style.display = 'none';
        document.body.appendChild(ifr);
        ifr.src = "/api/FileApi/DownloadFile?filename='" + escape(filename)
        + "'&Destination=" + escape(destination);
        ifr.onload = function () {
            document.body.removeChild(ifr);
            ifr = null;
        };
    };
    $scope.OpenFile = function (filename, destination, filetype) {
        var ifr = document.createElement('iframe');
        ifr.style.display = 'none';
        document.body.appendChild(ifr);
        ifr.src = "/api/FileApi/OpenFile?filename='" + escape(filename)
        + "'&Destination=" + escape(destination) + "&filetype=" + escape(filetype);
        $.ajax({
            url: ifr.src,
            type: 'GET',
            complete: function (e, xhr, settings) {
                if (e.status === 200) {
                    document.getElementById("iframe").src = url;
                } else {
                    $.smallBox({
                        title: e.responseText,
                        content: "",
                        color: "#3276B1",
                        iconSmall: "fa fa-times fa-2x fadeInRight animated",
                        timeout: 4000
                    });
                }
            }
        });

        ifr.onload = function () {
            document.body.removeChild(ifr);
            ifr = null;
        };
    };
    $scope.GetUserPhoto = function (UserName, PhotoExtension) {
        if (typeof PhotoExtension === 'undefined' || angular.isUndefined(PhotoExtension)
                   || PhotoExtension === null || PhotoExtension === '')
            PhotoExtension = '/Photo/UserLogin/EmpCard.jpg';
        else
            PhotoExtension = '/Photo/UserLogin/' + kaizenTrim(UserName) + '.' + kaizenTrim(PhotoExtension);
        return PhotoExtension;
    };

    $scope.changeFormat = function (Pre, decimals) {
        //alert(decimals);
        var ActiveScreenID = $("#ActiveScreenID").val();
        var DecimalFormat = '';
        for (var i = 0; i < decimals; i++) {
            DecimalFormat += "0";
        }
        DecimalFormat = Pre.trim() + ' ##,#.' + DecimalFormat
        $("#widget-" + ActiveScreenID).find('[data-role="numerictextbox"]').each(function () {
            var $textbox = $(this);
            $textbox.data("kendoNumericTextBox").destroy();
            $textbox.kendoNumericTextBox({
                format: DecimalFormat,
                decimals: decimals
            });
        });
    }
    $scope.changeFormat = function (Pre, decimals, ClassName) {
        var ActiveScreenID = $("#ActiveScreenID").val();
        var DecimalFormat = '';
        for (var i = 0; i < decimals; i++) {
            DecimalFormat += "0";
        }
        DecimalFormat = Pre.trim() + ' ##,#.' + DecimalFormat
        $("#widget-" + ActiveScreenID).find('input.' + ClassName + '[data-role="numerictextbox"]').each(function () {
            var $textbox = $(this);
            $textbox.data("kendoNumericTextBox").destroy();
            $textbox.kendoNumericTextBox({
                format: DecimalFormat,
                decimals: decimals
            });
        });
    }
    $scope.changeFormat = function (Pre, decimals, ClassName, ShowSpinners) {
        var ActiveScreenID = $("#ActiveScreenID").val();
        var DecimalFormat = '';
        for (var i = 0; i < decimals; i++) {
            DecimalFormat += "0";
        }
        DecimalFormat = Pre.trim() + ' ##,#.' + DecimalFormat
        $("#widget-" + ActiveScreenID).find('input.' + ClassName + '[data-role="numerictextbox"]').each(function () {
            var $textbox = $(this);
            $textbox.data("kendoNumericTextBox").destroy();
            $textbox.kendoNumericTextBox({
                format: DecimalFormat,
                decimals: decimals,
                spinners: ShowSpinners
            });
        });
    }

    $scope.CurrencyFormatOptions = function (ShowSpinners) {
        var DecimalFormat = '';
        for (var i = 0; i < $scope.MY.DecimalDigit; i++) {
            DecimalFormat += "0";
        }
        return {
            format: $scope.MY.CurrencyCode.trim() + " #." + DecimalFormat,
            decimals: $scope.MY.DecimalDigit,
            spinners: ShowSpinners
        }
    };
    $scope.NumberFormatOptions = function (Pre, Decimal, ShowSpinners) {
        //alert(Pre)
        if (Pre !== undefined && Pre !== null) {
            var DecimalFormat = '';
            for (var i = 0; i < Decimal; i++) {
                DecimalFormat += "0";
            }
            return {
                format: Pre.trim() + " #,#." + DecimalFormat,
                decimals: Decimal,
                spinners: ShowSpinners
            }
        }
        else {
            if (Decimal == null) {//waelll
                if (ShowSpinners) {
                    var DecimalFormat = '';
                    for (var i = 0; i < $scope.MY.DecimalDigit; i++) {
                        DecimalFormat += "0";
                    }
                    return {
                        format: $scope.MY.CurrencyCode.trim() + " #,#." + DecimalFormat,
                        decimals: $scope.MY.DecimalDigit,
                        spinners: true
                    }
                } else {
                    return {
                        format: "0",
                        decimals: $scope.MY.DecimalDigit,
                        spinners: ShowSpinners
                    }
                }
            } else {
                return {
                    format: "0",
                    decimals: Decimal,
                    spinners: ShowSpinners
                }
            }
        }
    };
    //---------------------------------------------------------------------------

    //---------------------------------------------------------------------------
    $scope.Sys00030List = [];
    $scope.GL00002List = [];
    $scope.GetNationality = function () {
        if ($scope.Sys00030List.length > 0) return;
        $http.get('/Adm_Nationality/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.Sys00030List = data;
            $scope.NationalityOptions = {
                filter: "startswith",
                optionLabel: "-- Select Address --",
                dataTextField: "NationalityName",
                dataValueField: "NationalityID",
                dataSource: $scope.Sys00030List,
            };
        }).finally(function () { });
    }
    $scope.GetGL00002List = function () {
        if ($scope.GL00002List.length > 0) return;
        $http.get('/GL/GL_Set_FiscalYears/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.GL00002List = data;
            $scope.YearCodelistOptions = {
                filter: "startswith",
                optionLabel: "-- Select  --",
                dataTextField: "YearName",
                dataValueField: "YearCode",
                dataSource: $scope.GL00002List,
            };
        }).finally(function () { });
    }
    $scope.GetGL00002List();
    //---------------------------------------------------------------------------
    $scope.TRXTypes = [];
    $scope.CM00700List = [];
    $scope.CM00009List = [];
    $scope.CM00010List = [];
    $scope.CM00011List = [];
    $scope.CM00012List = [];
    $scope.CM00014List = [];
    $scope.CM00015List = [];
    $scope.Sys00006List = [];
    $scope.CM00003List = [];
    //$scope.CM00055List = [];
    $scope.CaseDocType = function () {
        if ($scope.TRXTypes.length > 0) return;
        $http.get('/CMS_CaseType/MYFillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.TRXTypes = data;
            $scope.CM00203TRXTPE = [];
            $scope.TRXTypes.forEach(function (element, index) {
                //alert(element.TableSource);
                //alert(element.TableSource == 'CM00203');
                if (kaizenTrim(element.TableSource) == 'CM00203')
                    $scope.CM00203TRXTPE.push(element);
            });
            $scope.CaseTypeOptions = {
                filter: "startswith",
                model: "SelectedType",
                optionLabel: "-- Select Type --",
                dataTextField: "TrxTypeName",
                dataValueField: "TRXTypeID",
                dataSource: $scope.CM00203TRXTPE,
            };
        }).finally(function () { });
    };
    $scope.GetStatusType = function () {
        if ($scope.CM00003List.length > 0) return;
        $http.get('/CMS_Set_StatusActionType/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.CM00003List = data;
        }).finally(function () { });
    };
    $scope.CaseDocType();
    $scope.GetStatusType();
    $scope.CaseStatusList = function () {
        if ($scope.CM00700List.length > 0) return;
        $http.get('/CMS_CaseStatus/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey)
               .success(function (data) {
                   $scope.CM00700List = data;
               }).finally(function () { });
    };

    $scope.GetBucketList = function () {
        if ($scope.CM00015List.length > 0) return;
        $http.get('/Set_Bucket/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.CM00015List = data;
        }).finally(function () { });
    };
    //--------------------Debtor
    $scope.DebtorAddressCodeType = function () {
        if ($scope.CM00009List.length > 0) return;
        $http.get('/CMS_Set_AddressCodeTypeDebtor/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.CM00009List = data;
            $scope.DebtorAddressCodeTypeOptions = {
                filter: "startswith",
                optionLabel: "-- Select Address --",
                dataTextField: "AddressName",
                dataValueField: "AddressCode",
                dataSource: $scope.CM00009List,
            };
        }).finally(function () { });
    }
    $scope.DebtorClass = function () {
        if ($scope.CM00010List.length > 0) return;
        $http.get('/CMS_DebtorClass/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.CM00010List = data;
        }).finally(function () { });
    }
    $scope.DebtorGroup = function () {
        if ($scope.CM00011List.length > 0) return;
        $http.get('/CMS_Group/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.CM00011List = data;
        }).finally(function () { });
    }
    $scope.DebtorStatus = function () {
        if ($scope.CM00014List.length > 0) return;
        $http.get('/CMS_DebtorStatus/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.CM00014List = data;
        }).finally(function () { });
    }
    $scope.DebtorPriority = function () {
        if ($scope.CM00012List.length > 0) return;
        $http.get('/Set_Priority/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.CM00012List = data;
            $scope.CM00012Options = {
                filter: "startswith",
                optionLabel: "-- Select Priority --",
                dataTextField: "PriorityName",
                dataValueField: "PriorityID",
                dataSource: $scope.CM00012List,
            };
        }).finally(function () { });
    }




    $scope.DebtorContactTypeID = function () {
        if ($scope.Sys00006List.length > 0) return;
        $http.get('/Admin_ContactType/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey + '&ContactSourceID=2').success(function (data) {
            $scope.Sys00006List = data;
        }).finally(function () { });

    }
    //--------------------



    //---------------------------------------------------------------------------
    $scope.actions = [
            { text: 'OK', primary: true }
    ];
});
app.controller('Sys_GridMasterController', function ($scope) {
    angular.element(document).ready(function () {
        $scope.LoadGrid($scope.SelectedView.ViewID);
    });
});
app.config(function ($httpProvider) {
    $httpProvider.defaults.headers.post = {};
    $httpProvider.defaults.headers.post["Content-Type"] = "application/json; charset=utf-8";
});
app.directive('selectOnClick', function () {
    return {
        restrict: 'A',
        link: function (scope, element) {
            var focusedElement;
            element.on('click', function () {
                if (focusedElement !== this) {
                    this.select();
                    focusedElement = this;
                }
            });
            element.on('focus', function () {
                if (focusedElement !== this) {
                    this.select();
                    focusedElement = this;
                }
            });
            element.on('blur', function () {
                focusedElement = null;
            });
        }
    };
});
app.directive('focusMe', function ($timeout, $parse) {
    return {
        link: function (scope, element, attrs) {
            var model = $parse(attrs.focusMe);
            scope.$watch(model, function (value) {
                if (value === true) {
                    $timeout(function () {
                        element[0].focus();
                    });
                }
            });
            element.bind('blur', function () {
                scope.$apply(model.assign(scope, false));
            })
        }
    };
});
app.directive('convertToNumber', function () {
    return {
        require: 'ngModel',
        link: function (scope, element, attrs, ngModel) {
            ngModel.$parsers.push(function (val) {
                return parseInt(val, 10);
            });
            ngModel.$formatters.push(function (val) {
                return '' + val;
            });
        }
    };
});
app.directive('checkboxAll', function () {
    return function (scope, iElement, iAttrs) {
        var parts = iAttrs.checkboxAll.split('.');
        iElement.attr('type', 'checkbox');
        iElement.bind('change', function (evt) {
            scope.$apply(function () {
                var setValue = iElement.prop('checked');
                angular.forEach(scope.$eval(parts[0]), function (v) {
                    v[parts[1]] = setValue;
                });
            });
        });
        scope.$watch(parts[0], function (newVal) {
            var hasTrue, hasFalse;
            angular.forEach(newVal, function (v) {
                if (v[parts[1]]) {
                    hasTrue = true;
                } else {
                    hasFalse = true;
                }
            });
            if (hasTrue && hasFalse) {
                iElement.attr('checked', false);
                iElement.addClass('greyed');
            } else {
                iElement.attr('checked', hasTrue);
                iElement.removeClass('greyed');
            }
        }, true);
    };
});
app.directive('ngDecimal', function ($filter) {
    var FLOAT_REGEXP_1 = /^\$?\d+(.\d{3})*(\,\d*)?$/; //Numbers like: 1.123,56
    var FLOAT_REGEXP_2 = /^\$?\d+(,\d{3})*(\.\d*)?$/; //Numbers like: 1,123.56
    var nDecimal = undefined;
    var decimalPlaces = function (num) {
        var match = ('' + num).match(/(?:\.(\d+))?(?:[eE]([+-]?\d+))?$/);
        if (!match) return 0;
        return Math.max(0, (match[1] ? match[1].length : 0) - (match[2] ? +match[2] : 0));
    }
    var formatNumber = function (_num, d) {
        if (d === undefined) {
            if (nDecimal === undefined) {
                d = decimalPlaces(_num);
            } else {
                d = nDecimal;
            }
        }
        return (isNaN(d) || d === undefined) ? $filter('number')(_num) : $filter('number')(_num, d);
    };
    var getNumber = function (_num, d) {
        if (d === undefined) {
            if (nDecimal === undefined) {
                d = decimalPlaces(_num);
            } else {
                d = nDecimal;
            }
        }
        if (!isNaN(_num)) _num = _num.toString();
        if (FLOAT_REGEXP_2.test(_num)) {
            var num = parseFloat(_num.replace(',', ''))
        } else {
            var num = 0;
        }
        if (!isNaN(d) || d !== undefined && d !== decimalPlaces(_num)) num = num.toFixed(d);
        return num;
    };
    return {
        require: 'ngModel',
        replace: true,
        scope: {
            decimal: '='
        },
        link: function (scope, elm, attrs, ctrl) {
            scope.$watch('decimal', function (newValue, oldValue) {
                nDecimal = newValue;
                var newNumber = getNumber(elm.val());
                ctrl.$setViewValue(formatNumber(newNumber));
                ctrl.$render();
            });
            elm.bind('blur', function () {
                ctrl.$viewValue = formatNumber(ctrl.$modelValue);
                ctrl.$render();
            });
            ctrl.$parsers.unshift(function (viewValue) {
                var testnum = getNumber(viewValue);
                ctrl.$setValidity('float', (testnum === 0 ? false : true));
                return (testnum === 0 ? undefined : testnum);
            });
            ctrl.$formatters.unshift(
                function (modelValue) {
                    return modelValue === undefined ? modelValue : formatNumber(modelValue);
                }
            );
        }
    };
});
app.directive('ngQuantity', function ($filter) {
    var FLOAT_REGEXP_1 = /^\$?\d+(.\d{3})*(\,\d*)?$/; //Numbers like: 1.123,56
    var FLOAT_REGEXP_2 = /^\$?\d+(,\d{3})*(\.\d*)?$/; //Numbers like: 1,123.56
    var nDecimal = undefined;
    var decimalPlaces = function (num) {
        var match = ('' + num).match(/(?:\.(\d+))?(?:[eE]([+-]?\d+))?$/);
        if (!match) return 0;
        return Math.max(0, (match[1] ? match[1].length : 0) - (match[2] ? +match[2] : 0));
    };
    var formatNumber = function (_num, d) {
        if (d === undefined) {
            if (nDecimal === undefined) {
                d = decimalPlaces(_num);
            } else {
                d = nDecimal;
            }
        }
        return (isNaN(d) || d === undefined) ? $filter('number')(_num) : $filter('number')(_num, d);
    };
    var getNumber = function (_num, d) {
        if (d === undefined) {
            if (nDecimal === undefined) {
                d = decimalPlaces(_num);
            } else {
                d = nDecimal;
            }
        }
        if (!isNaN(_num)) _num = _num.toString();
        if (FLOAT_REGEXP_2.test(_num)) {
            var num = parseFloat(_num.replace(',', ''))
        } else {
            var num = 0;
        }
        if (!isNaN(d) || d !== undefined && d !== decimalPlaces(_num)) num = num.toFixed(d);
        return num;
    };
    return {
        require: 'ngModel',
        replace: true,
        scope: {
            quantity: '='
        },
        link: function (scope, elm, attrs, ctrl) {
            scope.$watch('quantity', function (newValue, oldValue) {
                nDecimal = newValue;
                var newNumber = getNumber(elm.val());
                ctrl.$setViewValue(formatNumber(newNumber));
                ctrl.$render();
            });
            elm.bind('blur', function () {
                ctrl.$viewValue = formatNumber(ctrl.$modelValue);
                ctrl.$render();
            });
            ctrl.$parsers.unshift(function (viewValue) {
                var testnum = getNumber(viewValue);
                ctrl.$setValidity('float', (testnum === 0 ? false : true));
                return (testnum === 0 ? undefined : testnum);
            });
            ctrl.$formatters.unshift(
                function (modelValue) {
                    return modelValue === undefined ? modelValue : formatNumber(modelValue);
                }
            );
        }
    };
});
app.directive('onFinishRender', function ($timeout) {
    return {
        restrict: 'A',
        link: function (scope, element, attr) {
            if (scope.$last === true) {
                scope.$evalAsync(attr.onFinishRender);
            }
        }
    }
});
app.directive('keycodePressed', function ($timeout) {
    return {
        restrict: 'A',
        link: function ($scope, $element, $attrs) {
            $element.bind("keypress", function (event) {
                var keyCode = event.which || event.keyCode;

                if (keyCode === $attrs.code) {
                    $scope.$apply(function () {
                        $scope.$eval($attrs.keycodePressed, { $event: event });
                    });

                }
            });
        }
    }
});
app.directive('loading', ['$http', function ($http) {
    return {
        restrict: 'A',
        link: function (scope, elm, attrs) {
            scope.isLoading = function () {
                return $http.pendingRequests.length > 0;
            };
            scope.$watch(scope.isLoading, function (v) {
                if (v) {
                    elm.show();
                } else {
                    elm.hide();
                }
            });
        }
    };
}]);
app.directive('dynamic', function ($compile) {
    return {
        restrict: 'A',
        replace: true,
        link: function (scope, ele, attrs) {
            scope.$watch(attrs.dynamic, function (html) {
                ele.html(html);
                $compile(ele.contents())(scope);
            });
        }
    };
});