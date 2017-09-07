app.controller('CMS_CalendarController', function ($scope, $http) {
    $scope.CM00005 = {};
    $scope.PagesCM00005 = [];
    $scope.toolbarOptions = {
        items: [
                 {
                     type: "button", text: "<span class=\"fa fa-plus-circle\"></span> Set CMS Calendar ",
                     attributes: { "class": "btn-primary" },
                     click: function (e) {
                         $scope.$apply(function () {
                             $scope.AddCM00005();
                         });
                     }
                 },
                 {
                     type: "button", text: "<span class=\"fa fa-calendar\"></span> Set Calendar Periods",
                     attributes: { "class": "btn-primary" },
                     click: function (e) {
                         $scope.$apply(function () {
                             $scope.CalendarPeriods();
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
    $scope.LoadCM00005 = function (CalendarID) {
        if (angular.isUndefined($scope.CM00005.CalendarID)) {
            $http.get('/CMS/CMS_Calendar/GetSingle?KaizenPublicKey=' + sessionStorage.PublicKey + "&CalendarID=" + CalendarID).success(function (data) {
                $scope.CM00005 = data;
            }).finally(function () { $scope.CM00005.Status = 2; });
        }
    }
    $scope.LoadCM00005Page = function (ActionName) {
        removeEntityPage($scope.PagesCM00005);
        var URIPath = "/CMS/" + $scope.ToolBar.ActiveScreen.ID + "/" + ActionName + "?KaizenPublicKey=" + sessionStorage.PublicKey;
        var Page = { url: URIPath, ActionName: ActionName };
        $scope.PagesCM00005.push(Page);
    }
     
    $scope.AddCM00005 = function () {
        $scope.LoadCM00005Page('Create');
        $scope.Clear();
        $scope.CM00005.Status = 1;
    }
    $scope.EditCM00005 = function (CalendarID) {
        $scope.LoadCM00005Page('Create');
        $scope.Clear();
        $scope.LoadCM00005(CalendarID);
    };
    $scope.SaveData = function () {
        $http.post('/CMS/CMS_Calendar/SaveData', { 'CM00005': $scope.CM00005, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
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
        $http.post('/CMS/CMS_Calendar/UpdateData', { 'CM00005': $scope.CM00005, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
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
        $scope.CM00005 = {};
        $scope.CM00007 = {};
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
                $http.post('/CMS/CMS_Calendar/DeleteData', { 'CM00005': $scope.CM00005, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
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
        $scope.PagesCM00005 = [];
        $scope.InsertedPeriods = [];
        $scope.UpdatedPeriods = [];
        $scope.DeletedPeriods = [];
        $scope.CalendarPeriodList = [];
    };
    ////////////////////////////////////////////////// Calendar Periods
    $scope.CM00007 = {}; // -- 0 = original from database ; 1 Inserted New ; 2 Edited;3 = Deleted ; 4 File Chabged
    $scope.CM00007.SelectedYear = {};
    $scope.CalendarPeriods = function () {
        $scope.LoadCM00005Page('CalendarPeriods');
        $scope.Clear();
        $scope.CM00007.status = 0;
        $http.get('/CMS/CMS_Calendar/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.Calendars = data;
        }).finally(function () { });
        $http.get('/GL_Set_FiscalYears/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.Years = data;
        }).finally(function () { });
    };
    $scope.YearChanged = function () {
        $scope.CM00007.YearCode = $scope.CM00007.SelectedYear.YearCode;
        //$scope.CM00007.SelectedYear.PeriodFrom =$scope.CM00007.SelectedYear.PeriodFrom;
        //$scope.CM00007.SelectedYear.PeriodTo = $scope.CM00007.SelectedYear.PeriodTo;
        if ($scope.CM00007.CalendarID != undefined && $scope.CM00007.CalendarID != '') {
            $http.get('/CMS/CMS_Calendar/GetCalendarPeriods?KaizenPublicKey=' + sessionStorage.PublicKey, { params: { CalendarID: $scope.CM00007.CalendarID, YearCode: $scope.CM00007.YearCode } }).success(function (data) {
                $scope.CalendarPeriodList = data;
            }).finally(function () {
               
                $scope.CalendarPeriodList.forEach(function (element, index) {
                    element.status = 0;
                    element.StartDate = new Date(element.StartDate);
                    element.EndDate = new Date(element.EndDate);
                    if (index == 0) {
                        if ($scope.CalendarPeriodList.length > index + 1) {
                            element.minDate = $scope.CM00007.SelectedYear.PeriodFrom;
                            element.minDate2 = element.StartDate;
                            element.maxDate = element.EndDate;
                            element.maxDate2 = $scope.CalendarPeriodList[1].StartDate;

                            $scope.CalendarPeriodList[1].minDate = new Date(addDays(element.EndDate, 1));
                            $scope.CalendarPeriodList[1].minDate2 = $scope.CalendarPeriodList[1].StartDate;
                            $scope.CalendarPeriodList[1].maxDate = $scope.CalendarPeriodList[1].EndDate;
                            $scope.CalendarPeriodList[1].maxDate2 = $scope.CalendarPeriodList[1].EndDate;
                        }
                        else {
                            element.minDate = element.StartDate;
                            element.minDate2 = element.StartDate;
                            element.maxDate = element.EndDate;
                            element.maxDate2 = $scope.CM00007.SelectedYear.PeriodTo;
                        }
                    }
                    else {
                        element.minDate = $scope.CalendarPeriodList[index - 1].EndDate;
                        if ($scope.CalendarPeriodList.length > index + 1) {
                            element.minDate = new Date($scope.CalendarPeriodList[index - 1].EndDate);
                            element.minDate2 = element.StartDate;
                            element.maxDate = element.EndDate;
                            element.maxDate2 = $scope.CalendarPeriodList[index + 1].StartDate;
                        } else {
                            element.minDate = new Date($scope.CalendarPeriodList[index - 1].EndDate);
                            element.minDate2 = element.StartDate;
                            element.maxDate = element.EndDate;
                            element.maxDate2 = $scope.CM00007.SelectedYear.PeriodTo;
                        }
                    }
                })
            });
        }
    };
    $scope.CalendarChanged = function () {
        $scope.CM00007.CalendarID = $scope.CM00007.SelectedCalendar.CalendarID;
        if ($scope.CM00007.YearCode != undefined && $scope.CM00007.YearCode != '') {

            $http.get('/CMS/CMS_Calendar/GetCalendarPeriods?KaizenPublicKey=' + sessionStorage.PublicKey, { params: { CalendarID: $scope.CM00007.CalendarID, YearCode: $scope.CM00007.YearCode } }).success(function (data) {
                $scope.CalendarPeriodList = data;
                if (data.length > 0) {
                    $scope.CalendarPeriodList.forEach(function (element, index) {
                        element.status = 0;
                        element.StartDate = parseMSDate(element.StartDate);
                        element.EndDate = parseMSDate(element.EndDate);
                        if (index == 0) {
                            if ($scope.CalendarPeriodList.length > index + 1) {
                                element.minDate = $scope.CM00007.SelectedYear.PeriodFrom;
                                element.minDate2 = element.StartDate;
                                element.maxDate = element.EndDate;
                                element.maxDate2 = $scope.CalendarPeriodList[1].StartDate;

                                $scope.CalendarPeriodList[1].minDate = new Date(addDays(element.EndDate, 1));
                                $scope.CalendarPeriodList[1].minDate2 = $scope.CalendarPeriodList[1].StartDate;
                                $scope.CalendarPeriodList[1].maxDate = $scope.CalendarPeriodList[1].EndDate;
                                $scope.CalendarPeriodList[1].maxDate2 = $scope.CalendarPeriodList[1].EndDate;
                            }
                            else {
                                element.minDate = element.StartDate;
                                element.minDate2 = element.StartDate;
                                element.maxDate = element.EndDate;
                                element.maxDate2 = $scope.CM00007.SelectedYear.PeriodTo;
                            }
                        }
                        else {
                            element.minDate = $scope.CalendarPeriodList[index - 1].EndDate;
                            if ($scope.CalendarPeriodList.length > index + 1) {
                                element.minDate = new Date($scope.CalendarPeriodList[index - 1].EndDate);
                                element.minDate2 = element.StartDate;
                                element.maxDate = element.EndDate;
                                element.maxDate2 = $scope.CalendarPeriodList[index + 1].StartDate;
                            } else {
                                element.minDate = new Date($scope.CalendarPeriodList[index - 1].EndDate);
                                element.minDate2 = element.StartDate;
                                element.maxDate = element.EndDate;
                                element.maxDate2 = $scope.CM00007.SelectedYear.PeriodTo;
                            }
                        }
                    })
                }
                else {
                    var num = $scope.CM00007.SelectedCalendar.PeriodCount;
                    for (var i = 0; i < num; i++) {
                        var Period = {
                            CalendarID: $scope.CM00007.CalendarID,
                            IsClosed: false,
                            status: 1
                        };
                        $scope.CalendarPeriodList.push(Period);
                    }
                    $scope.CalendarPeriodList[0].StartDate = $scope.CM00007.SelectedYear.PeriodFrom;
                    $scope.CalendarPeriodList[$scope.CalendarPeriodList.length - 1].EndDate = $scope.CM00007.SelectedYear.PeriodTo;
                }
            }).finally(function () {

            });
        }
    };

    $scope.InsertedPeriods = [];
    $scope.UpdatedPeriods = [];
    $scope.DeletedPeriods = [];
    $scope.CalendarPeriodList = [];

    $scope.fromDateChanged = function (period, index) {
        if ($scope.CalendarPeriodList.length > 0) {
            if (index == 0) {
                if ($scope.CalendarPeriodList.length > index + 1) {
                    period.minDate = $scope.CM00007.SelectedYear.PeriodFrom;
                    period.minDate2 = period.StartDate;
                    period.maxDate = period.EndDate;
                    period.maxDate2 = $scope.CalendarPeriodList[1].StartDate;

                    $scope.CalendarPeriodList[1].minDate = addDays(period.EndDate, 1);
                    $scope.CalendarPeriodList[1].minDate2 = $scope.CalendarPeriodList[1].StartDate;
                    $scope.CalendarPeriodList[1].maxDate = $scope.CalendarPeriodList[1].EndDate;
                    $scope.CalendarPeriodList[1].maxDate2 = $scope.CalendarPeriodList[1].EndDate;
                }
                else {
                    period.minDate = $scope.CM00007.SelectedYear.PeriodFrom;
                    period.minDate2 = period.StartDate;
                    period.maxDate = period.EndDate;
                    period.maxDate2 = $scope.CM00007.SelectedYear.PeriodTo;
                }
            }
            else {
                if ($scope.CalendarPeriodList.length > index + 1) {
                    $scope.CalendarPeriodList[index + 1].minDate = addDays(period.EndDate, 1);
                    $scope.CalendarPeriodList[index + 1].minDate2 = $scope.CalendarPeriodList[index + 1].StartDate;
                    $scope.CalendarPeriodList[index + 1].maxDate = $scope.CalendarPeriodList[index + 1].EndDate;
                    $scope.CalendarPeriodList[index + 1].maxDate2 = $scope.CalendarPeriodList[index + 1].EndDate;
                }
                else {
                    period.minDate = $scope.CalendarPeriodList[index - 1].EndDate;
                    period.minDate2 = period.StartDate;
                    period.maxDate = period.EndDate;
                    period.maxDate2 = $scope.CM00007.SelectedYear.PeriodTo;
                }
            }
        }
        else if ($scope.CalendarPeriodList.length == 0) {
            period.minDate = $scope.CM00007.SelectedYear.PeriodFrom;
            period.minDate2 = period.StartDate;
            period.maxDate = period.EndDate;
            period.maxDate2 = $scope.CM00007.SelectedYear.PeriodTo;
        }
    };
    $scope.toDateChanged = function (period, index) {
        if ($scope.CalendarPeriodList.length > 0) {
            if (index == 0) {
                if ($scope.CalendarPeriodList.length > index + 1) {
                    period.minDate = $scope.CM00007.SelectedYear.PeriodFrom;
                    period.minDate2 = addDays(period.StartDate, 1);
                    period.maxDate = addDays(period.EndDate, -1);
                    period.maxDate2 = period.EndDate;

                    $scope.CalendarPeriodList[1].minDate = addDays(period.EndDate, 1);
                    $scope.CalendarPeriodList[1].minDate2 = $scope.CalendarPeriodList[1].StartDate;
                    $scope.CalendarPeriodList[1].maxDate = $scope.CalendarPeriodList[1].EndDate;
                    $scope.CalendarPeriodList[1].maxDate2 = $scope.CalendarPeriodList[1].EndDate;
                }
                else {
                    period.minDate = $scope.CM00007.SelectedYear.PeriodFrom;
                    period.minDate2 = period.StartDate;
                    period.maxDate = period.EndDate;
                    period.maxDate2 = $scope.CM00007.SelectedYear.PeriodTo;
                }
            }
            else {
                if ($scope.CalendarPeriodList.length > index + 1) {
                    $scope.CalendarPeriodList[index + 1].minDate = addDays(period.EndDate, 1);
                    $scope.CalendarPeriodList[index + 1].minDate2 = $scope.CalendarPeriodList[index + 1].StartDate;
                    $scope.CalendarPeriodList[index + 1].maxDate = $scope.CalendarPeriodList[index + 1].EndDate;
                    $scope.CalendarPeriodList[index + 1].maxDate2 = $scope.CalendarPeriodList[index + 1].EndDate;
                }
                else {
                    period.minDate = $scope.CalendarPeriodList[index - 1].EndDate;
                    period.minDate2 = period.StartDate;
                    period.maxDate = period.EndDate;
                    period.maxDate2 = $scope.CM00007.SelectedYear.PeriodTo;
                }

                $scope.CalendarPeriodList[index - 1].minDate = $scope.CalendarPeriodList[index - 1].StartDate;
                $scope.CalendarPeriodList[index - 1].minDate2 = $scope.CalendarPeriodList[index - 1].StartDate;
                $scope.CalendarPeriodList[index - 1].maxDate = period.StartDate;
                $scope.CalendarPeriodList[index - 1].maxDate2 = period.StartDate;
            }
        }
        else if ($scope.CalendarPeriodList.length == 0) {
            period.minDate = $scope.CM00007.SelectedYear.PeriodFrom;
            period.minDate2 = period.StartDate;
            period.maxDate = period.EndDate;
            period.maxDate2 = $scope.CM00007.SelectedYear.PeriodTo;
        }
    };
    $scope.AddNewPeriod = function () {
        if ($scope.CalendarPeriodList.length > 0) {
            var Obj =
                {
                    CalendarID: $scope.CM00007.CalendarID,
                    YearCode: $scope.CM00007.YearCode,
                    StartDate: addDays($scope.CalendarPeriodList[$scope.CalendarPeriodList.length - 1].EndDate, 1),
                    EndDate: $scope.CM00007.SelectedYear.PeriodTo,
                    minDate: addDays($scope.CalendarPeriodList[$scope.CalendarPeriodList.length - 1].EndDate, 1),
                    minDate2: addDays($scope.CalendarPeriodList[$scope.CalendarPeriodList.length - 1].EndDate, 1),
                    maxDate: $scope.CM00007.SelectedYear.PeriodTo,
                    maxDate2: $scope.CM00007.SelectedYear.PeriodTo,
                    status: 1
                };
            $scope.CalendarPeriodList.push(Obj);
        }
        else {
            var Obj =
            {
                CalendarID: $scope.CM00007.CalendarID,
                YearCode: $scope.CM00007.YearCode,
                minDate: $scope.CM00007.SelectedYear.PeriodFrom,
                minDate2: $scope.CM00007.SelectedYear.PeriodFrom,
                StartDate: $scope.CM00007.SelectedYear.PeriodFrom,
                EndDate: $scope.CM00007.SelectedYear.PeriodTo,
                maxDate: $scope.CM00007.SelectedYear.PeriodTo,
                maxDate2: $scope.CM00007.SelectedYear.PeriodTo,
                status: 1
            };
            $scope.CalendarPeriodList.push(Obj);
        }
    };
    $scope.UpdatePeriod = function (period) {
        if (period.status == 1 || period.status == 3)
            return;
        else
            period.status = 2;
    };

    $scope.SaveCalendarPeriods = function () {
        //alert($scope.CM00007.YearCode);
        //return;
        for (var i = 0; i < $scope.CalendarPeriodList.length; i++) {
            var item = $scope.CalendarPeriodList[i];
            if (item.status == 1) {
                var insert_tmp = {
                    CalendarID: $scope.CM00007.CalendarID,
                    YearCode: $scope.CM00007.YearCode,
                    PeriodName: item.PeriodName,
                    StartDate: item.StartDate,
                    EndDate: item.EndDate,
                    IsClosed: item.IsClosed
                };
                $scope.InsertedPeriods.push(insert_tmp);
            }
            else if (item.status == 2) {
                var update_tmp = {
                    PERIODID: item.PERIODID,
                    CalendarID: $scope.CM00007.CalendarID,
                    YearCode: $scope.CM00007.YearCode,
                    PeriodName: item.PeriodName,
                    StartDate: item.StartDate,
                    EndDate: item.EndDate,
                    IsClosed: item.IsClosed
                };
                $scope.UpdatedPeriods.push(update_tmp);
            }
            else if (item.status == 3) {
                var delete_tmp = {
                    PERIODID: item.PERIODID,
                    CalendarID: $scope.CM00007.CalendarID,
                    YearCode: $scope.CM00007.YearCode,
                    PeriodName: item.PeriodName,
                    StartDate: item.StartDate,
                    EndDate: item.EndDate,
                    IsClosed: item.IsClosed
                };
                $scope.DeletedPeriods.push(delete_tmp);
            }
        }
        if ($scope.InsertedPeriods.length > 0) {
            $http({
                url: '/CMS/CMS_Calendar/SavePeriods?KaizenPublicKey=' + sessionStorage.PublicKey,
                method: "POST",
                data: JSON.stringify($scope.InsertedPeriods),
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
                        icon: "fa fa-check shake animated"
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
                    title: data.Massage,
                    content: data.Description,
                    color: "#C46A69",
                    timeout: 8000,
                    icon: "fa fa-bolt swing animated"
                });
            });
        }
        if ($scope.UpdatedPeriods.length > 0) {
            $http({
                url: '/CMS/CMS_Calendar/UpdatePeriods?KaizenPublicKey=' + sessionStorage.PublicKey,
                method: "POST",
                data: JSON.stringify($scope.UpdatedPeriods),
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
                        icon: "fa fa-check shake animated"
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
                    title: data.Massage,
                    content: data.Description,
                    color: "#C46A69",
                    timeout: 8000,
                    icon: "fa fa-bolt swing animated"
                });
            });
        }
        if ($scope.DeletedPeriods.length > 0) {
            $http({
                url: '/CMS/CMS_Calendar/DeletePeriods?KaizenPublicKey=' + sessionStorage.PublicKey,
                method: "POST",
                data: JSON.stringify($scope.DeletedPeriods),
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
                        icon: "fa fa-check shake animated"
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
                    title: data.Massage,
                    content: data.Description,
                    color: "#C46A69",
                    timeout: 8000,
                    icon: "fa fa-bolt swing animated"
                });
            });
        }
        //$scope.Cancel();
    };
    $scope.RemovePeriod = function (period, index) {
        if (period.status == 1)
            $scope.CalendarPeriodList.splice(index, 1);
        else
            period.status = 3;
    };

});