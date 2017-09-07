app.controller('Event_SetupController', function ($scope, $http) {
    $scope.Met00011 = {};
    $scope.EventSetupList = [];
    $scope.CalendarList = [];
    $scope.PagesMet00011 = [];

    $scope.toolbarOptions = {
        items: [
                 {
                     type: "button", text: " Users", spriteCssClass: "k-tool-icon k-filter",
                     click: function (e) { $scope.EventSetupUser(); }
                 },
                 {
                     type: "button", text: " Role Security", spriteCssClass: "k-tool-icon k-filter",
                     click: function (e) { $scope.EventSetupRole(); }
                 },
        ]
 , resizable: true
    };

    $scope.LoadMet00011Page = function (ActionName) {
        removeEntityPage($scope.PagesMet00011);
        var URIPath = "/Tools/" + $scope.ToolBar.ActiveScreen.ID + "/" + ActionName + "?KaizenPublicKey=" + sessionStorage.PublicKey;
        var Page = { url: URIPath, ActionName: ActionName };
        $scope.PagesMet00011.push(Page);
    };

    $scope.Load = function () {
        $scope.Met00011.status = 0;
        $scope.LoadEventSetupData();
    };

    $scope.LoadEventSetupData = function () {
        $http.get('/Event_Setup/GetAllCalenderNames?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            if (data.length >= 0) {
                if (data.length > 0)
                    $scope.EventSetupList = data;
                else
                    $scope.EventSetupList = [];
            }
        });
    };

    $scope.UserBack = function (user) {
        if (user != null) {
            switch ($scope.CurrentControl) {
                case 'EventSetupUsers':
                    $scope.Met00012.UserName = user.UserName;
                    break;
            }
        }
    };

    $scope.AddEventSetup = function () {
        $scope.Met00011.status = 1;
        if ($scope.Met00011) {
            $http({
                url: '/Event_Setup/SaveEventSetup?KaizenPublicKey=' + sessionStorage.PublicKey,
                method: "POST",
                data: JSON.stringify($scope.Met00011),
                dataType: "json",
                headers: {
                    'Content-Type': 'application/json; charset=utf-8'
                }
            }).success(function (data) {
                if (data.Status == true) {
                    $scope.Met00011 = {};
                    $scope.Met00011.status = 0;

                    $scope.LoadEventSetupData();

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
                    content: "Unable to save event setup data",
                    color: "#C46A69",
                    timeout: 8000,
                    icon: "fa fa-bolt swing animated"
                });
            });
        }
    };
    var indx;
    $scope.EditEventSetup = function (teamObj) {
        indx = $scope.EventSetupList.indexOf(teamObj);
        var obj_room = jQuery.extend({}, $scope.EventSetupList[indx]);
        $scope.Met00011 = obj_room;
        //$scope.SelectedFieldType = $scope.CM00070;
        $scope.Met00011.status = 2;
    };

    $scope.UpdateEventSetup = function () {
        if ($scope.Met00011.status == 0 || angular.isUndefined($scope.Met00011.status)) {
            $scope.Met00011.status = 2;
        }
        $scope.EventSetupList.splice(indx, 1, $scope.Met00011);

        if ($scope.Met00011) {
            $http({
                url: '/Event_Setup/UpdateEventSetup?KaizenPublicKey=' + sessionStorage.PublicKey,
                method: "POST",
                data: $scope.Met00011,
                dataType: "json",
                headers: {
                    'Content-Type': 'application/json; charset=utf-8'
                }
            }).success(function (data) {
                if (data.Status == true) {
                    $scope.Met00011 = {};
                    $scope.Met00011.status = 0;

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

    $scope.RemoveEventSetup = function (teamObj, index) {

        $.SmartMessageBox({
            title: "Delete Record",
            content: "Are you sure?",
            buttons: '[No][Yes]'
        }, function (ButtonPressed) {
            if (ButtonPressed === "Yes") {

                $http({
                    url: '/Event_Setup/DeleteEventSetup?KaizenPublicKey=' + sessionStorage.PublicKey,
                    method: "POST",
                    data: JSON.stringify(teamObj),
                    dataType: "json",
                    headers: {
                        'Content-Type': 'application/json; charset=utf-8'
                    }
                }).success(function (data) {
                    if (data.Status == true) {
                        if (teamObj.status == 1)
                            $scope.EventSetupList.splice(index, 1);
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
        $scope.PagesMet00011 = [];
    };

    $scope.Clear = function () {
        $scope.Met00011 = {};
        $scope.Met00012 = {};
        $scope.Met00013 = {};

        $scope.EventSetupUsers = [];
        $scope.Roles = [];
    };
    //------------------------------ Source Role Security ---------------------------
    $scope.Met00013 = {};
    $scope.EventSetupRole = function () {
        $scope.LoadMet00011Page('EventSetupRole');
        $scope.Clear();
        $scope.Met00013.Status = 0;
        $scope.LoadEventSetupData();
        $scope.LoadRoles();
    };

    $scope.CalendarIDChangedForRoles = function () {
        if (angular.isDefined($scope.Met00013.CalendarID)) {
            $http.get('/Event_Setup/GetEventSetupRoles?KaizenPublicKey=' + sessionStorage.PublicKey,
            { params: { CalendarID: $scope.Met00013.CalendarID } }).success(function (data) {

                if (data.length >= 0) {
                    if (data.length > 0)
                        $scope.EventSetupRoleSecurityList = data;
                    else
                        $scope.EventSetupRoleSecurityList = [];

                    $scope.Roles.forEach(function (eleRole, index) {
                        eleRole.isSelected = false;
                        $scope.EventSetupRoleSecurityList.forEach(function (eleEventSetupRole, index) {
                            if (eleRole.RoleID === eleEventSetupRole.RoleID) {
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

    $scope.UpdateEventSetupRole = function (role) {
        if (!$scope.Met00013 || !$scope.Met00013.CalendarID) {
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
            $scope.Met00013.RoleID = role.RoleID;

            var urlToUpdate = "";

            if (role.isSelected) {
                urlToUpdate = "/Event_Setup/SaveEventSetupRole?KaizenPublicKey=" + sessionStorage.PublicKey;
            } else {
                urlToUpdate = "/Event_Setup/DeleteEventSetupRole?KaizenPublicKey=" + sessionStorage.PublicKey;
            }

            $http({
                url: urlToUpdate,
                method: "POST",
                data: JSON.stringify($scope.Met00013),
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
    $scope.Met00012 = {};
    $scope.EventSetupUsers = [];
    $scope.EventSetupUser = function () {
        $scope.LoadMet00011Page('EventSetupUser');
        $scope.Clear();
        $scope.Met00012.Status = 0;
        $scope.LoadEventSetupData();
    };

    $scope.RemoveEventSetupUser = function (eventSetupUser, index) {

        $.SmartMessageBox({
            title: "Delete Record",
            content: "Are you sure?",
            buttons: '[No][Yes]'
        }, function (ButtonPressed) {
            if (ButtonPressed === "Yes") {

                $http({
                    url: '/Event_Setup/DeleteEventSetupUser?KaizenPublicKey=' + sessionStorage.PublicKey,
                    method: "POST",
                    data: JSON.stringify(eventSetupUser),
                    dataType: "json",
                    headers: {
                        'Content-Type': 'application/json; charset=utf-8'
                    }
                }).success(function (data) {
                    if (data.Status == true) {
                        $scope.EventSetupUsers.splice(index, 1);
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

    $scope.AddEventSetupUser = function () {
        $http({
            url: '/Event_Setup/SaveEventSetupUser?KaizenPublicKey=' + sessionStorage.PublicKey,
            method: "POST",
            data: JSON.stringify($scope.Met00012),
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

                var newObject = jQuery.extend(true, {}, $scope.Met00012);
                $scope.EventSetupUsers.push(newObject);
                $scope.Met00012.UserName = "";
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

    $scope.UpdateEventSetupUser = function (eventSetupUser) {
        $http({
            url: '/Event_Setup/UpdateEventSetupUser?KaizenPublicKey=' + sessionStorage.PublicKey,
            method: "POST",
            data: JSON.stringify(eventSetupUser),
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
                content: "Unable to update user",
                color: "#C46A69",
                timeout: 8000,
                icon: "fa fa-bolt swing animated"
            });
        });
    };

    $scope.CalendarIDChangedForUsers = function () {
        if (angular.isDefined($scope.Met00012.CalendarID)) {
            $http.get('/Event_Setup/GetEventSetupUsers?KaizenPublicKey=' + sessionStorage.PublicKey,
            { params: { CalendarID: $scope.Met00012.CalendarID } }).success(function (data) {
                $scope.EventSetupUsers = data;
            }).finally(function () { });
        }
    };

});