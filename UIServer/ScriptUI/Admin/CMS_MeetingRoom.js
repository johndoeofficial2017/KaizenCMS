app.controller('CMS_MeetingRoomController', function ($scope, $http) {
    $scope.Met00204 = {};
    $scope.Met00206 = {};
    $scope.MeetingRoomList = [];
    $scope.PagesMet00204 = [];
    $scope.Roles = [];
    $scope.SelectedLookUp = {};

    $scope.Load = function () {
        $scope.Met00204.status = 0;
        $scope.LoadMeetingRoomData();
    };

    $scope.LoadMet00204Page = function (ActionName) {
        removeEntityPage($scope.PagesMet00204);
        var URIPath = "/Tools/" + $scope.ToolBar.ActiveScreen.ID + "/" + ActionName + "?KaizenPublicKey=" + sessionStorage.PublicKey;
        var Page = { url: URIPath, ActionName: ActionName };
        $scope.PagesMet00204.push(Page);
    };

    $scope.toolbarOptions = {
        items: [
                 {
                     type: "button", text: "<span class=\"fa fa-plus-circle\"></span> Users ",
                     attributes: { "class": "btn-primary" },
                     click: function (e) {
                         $scope.$apply(function () {
                             $scope.MeetingRoomUser();
                         });
                     }
                 },
                 {
                     type: "button", text: "<span class=\"fa fa-plus-circle\"></span> Roles ",
                     attributes: { "class": "btn-primary" },
                     click: function (e) {
                         $scope.$apply(function () {
                             $scope.MeetingRoomRole();
                         });
                     }
                 }
        ]
 , resizable: true
    };

    $scope.LoadMeetingRoomData = function () {
        $http.get('/CMS_MeetingRoom/GetAllRooms?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            if (data.length >= 0) {
                if (data.length > 0)
                    $scope.MeetingRoomList = data;
                else
                    $scope.MeetingRoomList = [];
            }
        });
    };

    $scope.AddMeetingRoom = function () {
        $scope.Met00204.status = 1;
        if ($scope.Met00204) {
            $http({
                url: '/CMS_MeetingRoom/SaveMeetingRoom?KaizenPublicKey=' + sessionStorage.PublicKey,
                method: "POST",
                data: JSON.stringify($scope.Met00204),
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

                    $scope.Met00204 = {};
                    $scope.Met00204.status = 0;

                    $scope.LoadMeetingRoomData();

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
                    content: "Unable to save meeting room data",
                    color: "#C46A69",
                    timeout: 8000,
                    icon: "fa fa-bolt swing animated"
                });
            });
        }
    };

    var indx;
    $scope.EditMeetingRoom = function (teamObj) {
        indx = $scope.MeetingRoomList.indexOf(teamObj);
        var obj_room = jQuery.extend({}, $scope.MeetingRoomList[indx]);
        $scope.Met00204 = obj_room;
        //$scope.SelectedFieldType = $scope.CM00070;
        var Obj_indx = $scope.functiontofindIndexByKeyValue($scope.MeetingRoomList, "MeetingRoomID", $scope.Met00204.MeetingRoomID);
        var obj = $scope.MeetingRoomList[Obj_indx];
        //$scope.SelectedLookUp.SelectedMeetingRoom = obj;
        //if ($scope.CM00070.status == 0 || angular.isUndefined($scope.CM00070.status)) {
        $scope.Met00204.status = 2;
    };

    $scope.UpdateMeetingRoom = function () {
        if ($scope.Met00204.status == 0 || angular.isUndefined($scope.Met00204.status)) {
            $scope.Met00204.status = 2;
        }
        $scope.MeetingRoomList.splice(indx, 1, $scope.Met00204);

        if ($scope.Met00204) {
            $http({
                url: '/CMS_MeetingRoom/UpdateMeetingRoom?KaizenPublicKey=' + sessionStorage.PublicKey,
                method: "POST",
                data: $scope.Met00204,
                dataType: "json",
                headers: {
                    'Content-Type': 'application/json; charset=utf-8'
                }
            }).success(function (data) {
                if (data.Status == true) {
                    $scope.Met00204 = {};
                    $scope.Met00204.status = 0;

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

    $scope.RemoveMeetingRoom = function (teamObj, index) {

        $.SmartMessageBox({
            title: "Delete Record",
            content: "Are you sure?",
            buttons: '[No][Yes]'
        }, function (ButtonPressed) {
            if (ButtonPressed === "Yes") {

                $http({
                    url: '/CMS_MeetingRoom/DeleteMeetingRoom?KaizenPublicKey=' + sessionStorage.PublicKey,
                    method: "POST",
                    data: JSON.stringify(teamObj),
                    dataType: "json",
                    headers: {
                        'Content-Type': 'application/json; charset=utf-8'
                    }
                }).success(function (data) {
                    if (data.Status == true) {
                        if (teamObj.status == 1)
                            $scope.MeetingRoomList.splice(index, 1);
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
        $scope.PagesMet00204 = [];
    };
    $scope.Clear = function () {
        $scope.Met00205 = {};
    };
    //--------------------- Meeting Room Role -------------------------------
    $scope.MeetingRoomRole = function () {
        $scope.Roles = [];
        $scope.LoadMet00204Page('MeetingRoomRole');
        $scope.Clear();

        $http.get('/CMS_MeetingRoom/GetAllRooms?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.MeetingRoomList = data;
        }).finally(function () {
        });

        if ($scope.Roles == null || $scope.Roles.length == 0) {
            $http.get('/Sys_role/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
                $scope.Roles = data;
            }).finally(function () { });
        }
    };

    $scope.MeetingRoomChangedForRoles = function () {
        //alert($scope.SelectedLookUp.SelectedMeetingRoom);
        if (angular.isDefined($scope.SelectedLookUp.SelectedMeetingRoom)) {
            $scope.Met00206.MeetingRoomID = $scope.SelectedLookUp.SelectedMeetingRoom;
            $scope.LoadRoles_MeetingRoom($scope.Met00206.MeetingRoomID);
        }
    };

    $scope.LoadRoles_MeetingRoom = function (MeetingRoomID) {
        $http.get('/CMS_MeetingRoom/GetRolesByMeetingRoom?KaizenPublicKey=' + sessionStorage.PublicKey,
            { params: { MeetingRoomID: MeetingRoomID } }).success(function (data) {
                if (data.length >= 0) {
                    if (data.length > 0)
                        $scope.MeetingRoomRoles = data;
                    else
                        $scope.MeetingRoomRoles = [];

                    $scope.Roles.forEach(function (eleRole, index) {
                        eleRole.isSelected = false;
                        $scope.MeetingRoomRoles.forEach(function (eleStatusRole, index) {
                            if (eleRole.RoleID === eleStatusRole.RoleID) {
                                eleRole.isSelected = true;
                            }
                        });
                    });
                }
            });
    };

    $scope.UpdateMet00206 = function (role) {
        if (!$scope.Met00206 || !$scope.Met00206.MeetingRoomID) {
            $.bigBox({
                title: "Error",
                content: "Please select meeting room",
                color: "#C46A69",
                timeout: 8000,
                icon: "fa fa-bolt swing animated"
            });
            role.isSelected = !role.isSelected;
            return;
        }
        if (role) {
            $scope.Met00206.RoleID = role.RoleID;

            var urlToUpdate = "";

            if (role.isSelected) {
                urlToUpdate = "/CMS_MeetingRoom/SaveRole?KaizenPublicKey=" + sessionStorage.PublicKey;
            } else {
                urlToUpdate = "/CMS_MeetingRoom/DeleteRole?KaizenPublicKey=" + sessionStorage.PublicKey;
            }

            $http({
                url: urlToUpdate,
                method: "POST",
                data: JSON.stringify($scope.Met00206),
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
                    content: "Unable to save role for the selected meeting room",
                    color: "#C46A69",
                    timeout: 8000,
                    icon: "fa fa-bolt swing animated"
                });
            });
        }
    };

    $scope.UserBack = function (user) {
        if (user != null) {
            switch ($scope.CurrentControl) {
                case 'MeetingRoomUsers':
                    $scope.Met00205.UserName = user.UserName;
                    $scope.LoadAllMet00205($scope.Met00205.UserName);
                    break;
            }
        }
    };

    //--------------------- Meeting Room User -------------------------------
    $scope.Met00205 = {};
    $scope.MeetingRoomUsers = [];
    $scope.MeetingRoomUser = function () {
        $scope.LoadMet00204Page('MeetingRoomUser');
        $scope.Clear();

        $http.get('/CMS_MeetingRoom/GetAllRooms?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.MeetingRoomList = data;
        });
    };

    $scope.LoadAllMet00205 = function (UserName) {
        $http.get('/CMS_MeetingRoom/GetMeetingRoomsByUser?KaizenPublicKey=' + sessionStorage.PublicKey,
                                { params: { userName: UserName } }).success(function (data) {
                                    $scope.MeetingRoomUsers = data;
                                    $scope.MeetingRoomList.forEach(function (ele, index) {
                                        ele.isSelected = false;
                                        $scope.MeetingRoomUsers.forEach(function (eleselected, index) {
                                            if (ele.MeetingRoomID === eleselected.MeetingRoomID) {
                                                ele.isSelected = true;
                                            }
                                        });
                                    });
                                });
    };

    $scope.UpdateMet00205 = function (obj) {
        if (!$scope.Met00205 || !$scope.Met00205.UserName) {
            $.bigBox({
                title: "Error",
                content: "Please select user name",
                color: "#C46A69",
                timeout: 8000,
                icon: "fa fa-bolt swing animated"
            });
            obj.isSelected = !obj.isSelected;
            return;
        }
        if (obj) {
            var objUser = {};
            objUser.UserName = $scope.Met00205.UserName;
            objUser.MeetingRoomID = obj.MeetingRoomID;

            if (obj.isSelected) {
                $scope.AddMeetingRoomByUser(objUser);
            } else {
                $scope.MeetingRoomUser_Delete(objUser);
            }
        }
    };
    $scope.MeetingRoomUser_Delete = function (objUser) {
        $http({
            url: '/CMS_MeetingRoom/DeleteMet00205?KaizenPublicKey=' + sessionStorage.PublicKey,
            method: "POST",
            data: JSON.stringify(objUser),
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
    $scope.AddMeetingRoomByUser = function (objUser) {
        $http({
            url: '/CMS_MeetingRoom/AddMet00205?KaizenPublicKey=' + sessionStorage.PublicKey,
            method: "POST",
            data: JSON.stringify(objUser),
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
});