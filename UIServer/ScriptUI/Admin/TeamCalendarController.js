app.controller('TeamCalendarController', function ($scope, $http) {
    $scope.Met00001 = {};
    $scope.TeamUpCalendarList = [];
    $scope.CalendarList = [];
    $scope.PagesMet00001 = [];

    $scope.toolbarOptions = {
        items: [
                 {
                     type: "button", text: " Users", spriteCssClass: "k-tool-icon k-filter",
                     click: function (e) { $scope.CalendarUser(); }
                 },
                 {
                     type: "button", text: " Role Security", spriteCssClass: "k-tool-icon k-filter",
                     click: function (e) { $scope.CalendarRole(); }
                 },
        ]
 , resizable: true
    };

    $scope.LoadMet00001Page = function (ActionName) {
        removeEntityPage($scope.PagesMet00001);
        var URIPath = "/Tools/" + $scope.ToolBar.ActiveScreen.ID + "/" + ActionName + "?KaizenPublicKey=" + sessionStorage.PublicKey;
        var Page = { url: URIPath, ActionName: ActionName };
        $scope.PagesMet00001.push(Page);
    };

    $scope.Load = function () {
        $scope.Met00001.status = 0;
        $scope.LoadTeamCalendarData();
    };

    $scope.LoadTeamCalendarData = function () {
        $http.get('/TeamCalendar/GetAllCalenderNames?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            if (data.length >= 0) {
                if (data.length > 0)
                    $scope.TeamUpCalendarList = data;
                else
                    $scope.TeamUpCalendarList = [];
            }
        });
    };

    $scope.UserBack = function (user) {
        if (user != null) {
            switch ($scope.CurrentControl) {
                case 'TeampUpCalendarUsers':
                    $scope.Met00001.UserName = user.UserName;
                    break;
                case 'CalendarUsers':
                    $scope.Met00002.UserName = user.UserName;
                    break;
            }
        }
    };

    $scope.AddTeamCalendar = function () {
        $scope.Met00001.status = 1;
        if ($scope.Met00001) {
            $http({
                url: '/TeamCalendar/SaveTeamCalendar?KaizenPublicKey=' + sessionStorage.PublicKey,
                method: "POST",
                data: JSON.stringify($scope.Met00001),
                dataType: "json",
                headers: {
                    'Content-Type': 'application/json; charset=utf-8'
                }
            }).success(function (data) {
                if (data.Status == true) {
                    $scope.Met00001 = {};
                    $scope.Met00001.status = 0;

                    $scope.LoadTeamCalendarData();

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
                    content: "Unable to save team calendar data",
                    color: "#C46A69",
                    timeout: 8000,
                    icon: "fa fa-bolt swing animated"
                });
            });
        }
    };
    var indx;
    $scope.EditTeamCalendar = function (teamObj) {
        indx = $scope.TeamUpCalendarList.indexOf(teamObj);
        var obj_room = jQuery.extend({}, $scope.TeamUpCalendarList[indx]);
        $scope.Met00001 = obj_room;
        //$scope.SelectedFieldType = $scope.CM00070;
        $scope.Met00001.status = 2;
    };

    $scope.UpdateTeamCalendar = function () {
        if ($scope.Met00001.status == 0 || angular.isUndefined($scope.Met00001.status)) {
            $scope.Met00001.status = 2;
        }
        $scope.TeamUpCalendarList.splice(indx, 1, $scope.Met00001);

        if ($scope.Met00001) {
            $http({
                url: '/TeamCalendar/UpdateTeamCalendar?KaizenPublicKey=' + sessionStorage.PublicKey,
                method: "POST",
                data: $scope.Met00001,
                dataType: "json",
                headers: {
                    'Content-Type': 'application/json; charset=utf-8'
                }
            }).success(function (data) {
                if (data.Status == true) {
                    $scope.Met00001 = {};
                    $scope.Met00001.status = 0;

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

    $scope.RemoveTeamCalendar = function (teamObj, index) {

        $.SmartMessageBox({
            title: "Delete Record",
            content: "Are you sure?",
            buttons: '[No][Yes]'
        }, function (ButtonPressed) {
            if (ButtonPressed === "Yes") {

                $http({
                    url: '/TeamCalendar/DeleteTeamCalendar?KaizenPublicKey=' + sessionStorage.PublicKey,
                    method: "POST",
                    data: JSON.stringify(teamObj),
                    dataType: "json",
                    headers: {
                        'Content-Type': 'application/json; charset=utf-8'
                    }
                }).success(function (data) {
                    if (data.Status == true) {
                        if (teamObj.status == 1)
                            $scope.TeamUpCalendarList.splice(index, 1);
                        else
                            teamObj.status = 3;
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

    $scope.Load();

    $scope.Cancel = function () {
        $scope.Clear();
        $scope.PagesMet00001 = [];
    };

    $scope.Clear = function () {
        $scope.Met00001 = {};
        $scope.Met00002 = {};
        $scope.Met00003 = {};

        $scope.CalendarUsers = [];
        $scope.Roles = [];
    };
    //------------------------------ Source Role Security ---------------------------
    $scope.Met00003 = {};
    $scope.CalendarRole = function () {
        $scope.LoadMet00001Page('CalendarRole');
        $scope.Clear();
        $scope.Met00003.Status = 0;
        $scope.LoadTeamCalendarData();
        $scope.LoadRoles();
    };

    $scope.CalendarIDChangedForRoles = function () {
        if (angular.isDefined($scope.Met00003.CalendarID)) {
            $http.get('/TeamCalendar/GetCalendarRoles?KaizenPublicKey=' + sessionStorage.PublicKey,
            { params: { CalendarID: $scope.Met00003.CalendarID } }).success(function (data) {

                if (data.length >= 0) {
                    if (data.length > 0)
                        $scope.CalendarRoleSecurityList = data;
                    else
                        $scope.CalendarRoleSecurityList = [];

                    $scope.Roles.forEach(function (eleRole, index) {
                        eleRole.isSelected = false;
                        $scope.CalendarRoleSecurityList.forEach(function (eleCalendarRole, index) {
                            if (eleRole.RoleID === eleCalendarRole.RoleID) {
                                eleRole.isSelected = true;
                            }
                        });
                    });
                }
            }).finally(function () { });
        }
    };

    $scope.LoadRoles = function () {
        if ($scope.Roles == null || $scope.Roles.length == 0) {
            $http.get('/Sys_role/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
                $scope.Roles = data;
            }).finally(function () { });
        }
    };

    $scope.UpdateCalendarRole = function (role) {
        if (!$scope.Met00003 || !$scope.Met00003.CalendarID) {
            $.bigBox({
                title: "Error",
                content: "Please select calendar id",
                color: "#C46A69",
                timeout: 8000,
                icon: "fa fa-bolt swing animated"
            });
            role.isSelected = !role.isSelected;
            return;
        }
        if (role) {
            $scope.Met00003.RoleID = role.RoleID;

            var urlToUpdate = "";

            if (role.isSelected) {
                urlToUpdate = "/TeamCalendar/SaveCalendarRole?KaizenPublicKey=" + sessionStorage.PublicKey;
            } else {
                urlToUpdate = "/TeamCalendar/DeleteCalendarRole?KaizenPublicKey=" + sessionStorage.PublicKey;
            }

            $http({
                url: urlToUpdate,
                method: "POST",
                data: JSON.stringify($scope.Met00003),
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
                    content: "Unable to save role for the selected source",
                    color: "#C46A69",
                    timeout: 8000,
                    icon: "fa fa-bolt swing animated"
                });
            });
        }
    };

    //------------------------------ Calendar User ---------------------------
    $scope.Met00002 = {};
    $scope.CalendarUsers = [];
    $scope.CalendarUser = function () {
        $scope.LoadMet00001Page('CalendarUser');
        $scope.Clear();
        $scope.Met00002.Status = 0;
        $scope.LoadTeamCalendarData();
    };

    $scope.RemoveCalendarUser = function (calendarUser, index) {

        $.SmartMessageBox({
            title: "Delete Record",
            content: "Are you sure?",
            buttons: '[No][Yes]'
        }, function (ButtonPressed) {
            if (ButtonPressed === "Yes") {

                $http({
                    url: '/TeamCalendar/DeleteCalendarUser?KaizenPublicKey=' + sessionStorage.PublicKey,
                    method: "POST",
                    data: JSON.stringify(calendarUser),
                    dataType: "json",
                    headers: {
                        'Content-Type': 'application/json; charset=utf-8'
                    }
                }).success(function (data) {
                    if (data.Status == true) {
                        $scope.CalendarUsers.splice(index, 1);
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
                        content: "Unable to delete selected data",
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

    $scope.AddCalendarUser = function () {
        $http({
            url: '/TeamCalendar/SaveCalendarUser?KaizenPublicKey=' + sessionStorage.PublicKey,
            method: "POST",
            data: JSON.stringify($scope.Met00002),
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

                var newObject = jQuery.extend(true, {}, $scope.Met00002);
                $scope.CalendarUsers.push(newObject);
                $scope.Met00002.UserName = "";
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
                content: "Unable to add user",
                color: "#C46A69",
                timeout: 8000,
                icon: "fa fa-bolt swing animated"
            });
        });
    };

    $scope.UpdateCalendarUser = function (calendarUser) {
        $http({
            url: '/TeamCalendar/UpdateCalendarUser?KaizenPublicKey=' + sessionStorage.PublicKey,
            method: "POST",
            data: JSON.stringify(calendarUser),
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
                content: "Unable to add user",
                color: "#C46A69",
                timeout: 8000,
                icon: "fa fa-bolt swing animated"
            });
        });
    };

    $scope.CalendarIDChangedForUsers = function () {
        if (angular.isDefined($scope.Met00002.CalendarID)) {
            $http.get('/TeamCalendar/GetCalendarUsers?KaizenPublicKey=' + sessionStorage.PublicKey,
            { params: { CalendarID: $scope.Met00002.CalendarID } }).success(function (data) {
                $scope.CalendarUsers = data;
            }).finally(function () { });
        }
    };

});