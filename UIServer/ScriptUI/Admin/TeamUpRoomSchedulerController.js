app.controller('TeamUpRoomSchedulerController', function ($scope, $http) {
    $scope.Met00100 = [];
    $scope.CalenderNames = [];
    $scope.MeetingRepeatTypes = [];
    $scope.MeetingRooms = [];

    $scope.RepeatEvery = false;
    $scope.RepeatEndType = "";
    $scope.RepeatTypeID = 1;

    $scope.MonthOccurences = [{
        text: "first", value: 1
    }, {
        text: "second", value: 2
    }, {
        text: "third", value: 3
    }, {
        text: "forth", value: 4
    }, {
        text: "last", value: -1
    }];

    $scope.WeekOccurences = [{
        text: "Day", value: -2
    }, {
        text: "Weekday", value: -1
    }, {
        text: "Weekend day", value: 0
    }, {
        text: "Sunday", value: 1
    }, {
        text: "Monday", value: 2
    }, {
        text: "Tuesday", value: 3
    }, {
        text: "Wednesday", value: 4
    }, {
        text: "Thursday", value: 5
    }, {
        text: "Friday", value: 6
    }, {
        text: "Saturday", value: 7
    }];

    $scope.YearOccurences = [{
        text: "January", value: 1
    }, {
        text: "February", value: 2
    }, {
        text: "March", value: 3
    }, {
        text: "April", value: 4
    }, {
        text: "May", value: 5
    }, {
        text: "June", value: 6
    }, {
        text: "July", value: 7
    }, {
        text: "August", value: 8
    }, {
        text: "September", value: 9
    }, {
        text: "October", value: 10
    }, {
        text: "November", value: 11
    }, {
        text: "December", value: 12
    }];

    $scope.Load = function () {
        $http.get('/TeamUpScheduler/GetAllCalenderNames?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.CalenderNames = data;
            $http.get('/TeamUpScheduler/GetAllRooms?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
                $scope.MeetingRooms = data;
                $http.get('/TeamUpScheduler/GetAllMeetingRepeatTypes?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
                    $scope.MeetingRepeatTypes = data;
                    $http.get('/TeamUpScheduler/GetAllMeetings?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
                        $scope.Met00100 = data;
                        //$scope.Met00100[0].recurrenceRule= "FREQ=DAILY;COUNT=5;INTERVAL=1;BYDAY=MO,TU,WE,TH,FR,SA,SU";
                        $scope.Met00100.MeetingEndGroup = "MeetingEndGroup";
                        $scope.LoadScheduler();
                    }).finally(function () { });
                }).finally(function () { });
            }).finally(function () { });
        }).finally(function () { });
    };

    $scope.LoadScheduler = function () {
        var resources = [{
            field: "CalendarID",
            name: "CalendarName",
            dataSource: [],
            title: "Attendee"
        }];
        $.each($scope.CalenderNames, function (i, v) {
            resources[0].dataSource.push({ text: v.CalendarName, value: v.CalendarID, color: v.CalendarColor });
        });

        $("#teamUpRoomScheduler").kendoScheduler({
            date: new Date(),
            //startTime: stDate,
            height: 600,
            views: [
                { type: "day", majorTick: 15 },
                { type: "month", selected: true },
                "week",
                "agenda",
                "timeline"
            ],
            editable: {
                template: $("#meetingEditor").html()
            },
            add: $scope.SchedulerAdd,
            save: $scope.SchedulerSave,
            edit: $scope.SchedulerEdit,
            remove: $scope.SchedulerRemove,
            dataSource: {
                batch: true,
                data: $scope.Met00100,
                schema: {
                    model: {
                        id: "MeetingID",
                        fields: {
                            MeetingID: { from: "MeetingID" },
                            Description: { from: "MeetingDescription" },
                            title: { from: "MeetingName" },
                            MeetingRoomID: { from: "MeetingRoomID" },
                            IsFullDay: { from: "IsFullDay" },
                            start: { type: "date", from: "StartDateTime", format: "{0:dd-MMM-yyyy}" },
                            end: { type: "date", from: "EndDateTime", format: "{0:dd-MMM-yyyy}" },
                            RepeatSun: { from: "RepeatSun" },
                            RepeatMon: { from: "RepeatMon" },
                            RepeatTus: { from: "RepeatTus" },
                            RepeatWed: { from: "RepeatWed" },
                            RepeatThur: { from: "RepeatThur" },
                            RepeatFrid: { from: "RepeatFrid" },
                            RepeatSat: { from: "RepeatSat" },
                            RepeatEndOn: { type: "date", from: "RepeatEndOn", format: "{0:dd-MMM-yyyy}" },
                            RepeatEndAfter: { from: "RepeatEndAfter" },
                            RepeatEndNever: { from: "RepeatEndNever" },
                            RepeatTypeID: { from: "RepeatTypeID" },
                            RepeatEvery: { from: "RepeatEvery" },
                            MonthRepeatWeekDay: { from: "MonthRepeatWeekDay" },
                            MonthRepeatOn: { from: "MonthRepeatOn" },
                            YearRepeatMonth: { from: "YearRepeatMonth" },
                            YearRepeatMonth2: { from: "YearRepeatMonth" },
                            YearRepeatOn: { from: "YearRepeatOn" },
                            YearRepeatMonthDay: { from: "YearRepeatMonthDay" },
                            StartDateTime: { from: "StartDateTime" },
                            EndDateTime: { from: "EndDateTime" },
                            SelectedValue: { from: "SelectedValue" },
                            MeetingEndGroup: { from: "MeetingEndGroup" },
                            MonthDayOccurence: { from: "MonthDayOccurence" },
                            YearMonthDayOccurence: { from: "YearMonthDayOccurence" },
                            recurrenceRule: { from: "RecurrenceRule" }
                        }
                    }
                }
            },
            resources: resources
        });

        $scope.LoadTeamUpByMeetingRoom();
    };

    $scope.SchedulerAdd = function (e) {
        setTimeout(function () {

        }, 100);
    };
    $scope.SchedulerEdit = function (e) {
        setTimeout(function () {
            $scope.BindKendoControlsInTemplate();
            $scope.LoadMeetingRepeatTypes();
            $scope.LoadRooms();
            $scope.LoadCalendarNames();
            $scope.LoadAttendees(e);

            if (e.event.MeetingRepeatTypeID && e.event.MeetingRepeatTypeID != 1) {

                $scope.ShowHideRepeatPanels(e.event.MeetingRepeatTypeID);
                $scope.SetRepeatTypeText(e.event.MeetingRepeatTypeID);
            }
            else {
                $scope.ShowHideRepeatPanels();
            }

            if (e.event.RepeatEndType)
                $scope.RepeatEndType = e.event.RepeatEndType;
            else
                $scope.RepeatEndType = "Never";

            if (e.event.RepeatEndType === "On") {
                $("#RepeatEndOn").prop('checked', true);
                $("#inputRepeatEndAfter").data("kendoNumericTextBox").enable(false);
                $("#inputRepeatEndOn").data("kendoDatePicker").enable(true);
            }
            else if (e.event.RepeatEndType === "After") {
                $("#RepeatEndAfter").prop('checked', true);
                $("#inputRepeatEndAfter").data("kendoNumericTextBox").enable(true);
                $("#inputRepeatEndOn").data("kendoDatePicker").enable(false);
            }
            else {
                $("#RepeatEndNever").prop('checked', true);
                $("#inputRepeatEndAfter").data("kendoNumericTextBox").enable(false);
                $("#inputRepeatEndOn").data("kendoDatePicker").enable(false);
            }

            if (e.event.IsFullDay) {
                $("#inputStartDate").data("kendoDateTimePicker").enable(false);
                $("#inputEndDate").data("kendoDateTimePicker").enable(false);
            }

            if (e.event.RepeatTypeID === 1)
                $("#MonthRepeatOn").prop('checked', true);
            else if (e.event.RepeatTypeID === 2)
                $("#MonthRepeatOnDay").prop('checked', true);
            else if (e.event.RepeatTypeID === 3)
                $("#YearRepeatOn").prop('checked', true);
            else if (e.event.RepeatTypeID === 4)
                $("#YearRepeatOnDay").prop('checked', true);
            else {
                $("#MonthRepeatOn").prop('checked', true);
                $("#YearRepeatOn").prop('checked', true);
            }

            if (e.event.RepeatTypeID)
                $scope.RepeatTypeID = e.event.RepeatTypeID;

        }, 100);
    };

    $scope.BindKendoControlsInTemplate = function () {

        $("#repeatEvery").kendoNumericTextBox({
            min: 1,
            decimals: 0,
            format: '#',
            value: 1
        });
        $("#inputRepeatEndAfter").kendoNumericTextBox({
            min: 1,
            decimals: 0,
            format: '#',
            value: 1
        });
        $("#inputRepeatOnMonth").kendoNumericTextBox({
            min: 1,
            decimals: 0,
            format: '#',
            value: 1
        });
        $("#monthRepeatDayList").kendoDropDownList({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: {
                data: $scope.MonthOccurences
            },
            select: function (e) {

            }
        });
        $("#monthRepeatWeekList").kendoDropDownList({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: {
                data: $scope.WeekOccurences
            },
            select: function (e) {

            }
        });
        $("#monthRepeatYearListByDate").kendoNumericTextBox({
            min: 1,
            decimals: 0,
            format: '#',
            value: 1
        });

        $("#yearRepeatDayList").kendoDropDownList({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: {
                data: $scope.MonthOccurences
            },
            select: function (e) {

            }
        });

        $("#yearRepeatWeekList").kendoDropDownList({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: {
                data: $scope.WeekOccurences
            },
            select: function (e) {

            }
        });
        $(".inputRepeatOnMonthForYear").kendoDropDownList({
            dataTextField: "text",
            dataValueField: "value",
            dataSource: {
                data: $scope.YearOccurences
            },
            select: function (e) {

            }
        });
    };

    $scope.RepeatEndCheckChanged = function (e) {
        if (e.id === "RepeatEndNever") {
            $scope.RepeatEndType = "Never";
            $("#inputRepeatEndAfter").data("kendoNumericTextBox").enable(false);
            $("#inputRepeatEndOn").data("kendoDatePicker").enable(false);
        }
        else if (e.id === "RepeatEndAfter") {
            $scope.RepeatEndType = "After";
            $("#inputRepeatEndAfter").data("kendoNumericTextBox").enable(true);
            $("#inputRepeatEndOn").data("kendoDatePicker").enable(false);
        }
        else if (e.id === "RepeatEndOn") {
            $scope.RepeatEndType = "On";
            $("#inputRepeatEndAfter").data("kendoNumericTextBox").enable(false);
            $("#inputRepeatEndOn").data("kendoDatePicker").enable(true);
        }
    }
    $scope.RepeatOnMonthCheckChanged = function (e) {
        if (e.id === "MonthRepeatOn") {
            $scope.RepeatTypeID = 1;
        }
        else if (e.id === "MonthRepeatOnDay") {
            $scope.RepeatTypeID = 2;
        }
    };

    $scope.RepeatOnYearCheckChanged = function (e) {
        if (e.id === "YearRepeatOn") {
            $scope.RepeatTypeID = 3;
        }
        else if (e.id === "YearRepeatOnDay") {
            $scope.RepeatTypeID = 4;
        }
    };

    $scope.AllDayCheckChanged = function (e) {
        $("#inputStartDate").data("kendoDateTimePicker").enable(!e.checked);
        $("#inputEndDate").data("kendoDateTimePicker").enable(!e.checked);

        if (e.checked) {
            var sDate = new Date($("#inputStartDate").val());
            var StartDate = new Date(sDate.getFullYear(), sDate.getMonth(), sDate.getDate(), 0, 0, 0);
            $("#inputStartDate").data("kendoDateTimePicker").value(StartDate);

            var eDate = new Date($("#inputEndDate").val());
            var EndDate = new Date(eDate.getFullYear(), eDate.getMonth(), eDate.getDate(), 23, 59, 59);
            $("#inputEndDate").data("kendoDateTimePicker").value(EndDate);
        }
    };

    $scope.SchedulerSave = function (e) {
        var obj = {};
        var sDate = $("#inputStartDate").data("kendoDateTimePicker").value();
        var eDate = $("#inputEndDate").data("kendoDateTimePicker").value();

        obj.StartDateTime = sDate;
        obj.EndDateTime = eDate;
        obj.MeetingDescription = e.event.Description;
        obj.MeetingName = e.event.title;
        obj.MeetingRoomID = e.event.MeetingRoomID;
        obj.IsFullDay = e.event.IsFullDay;
        obj.RepeatSun = e.event.RepeatSun;
        obj.RepeatMon = e.event.RepeatMon;
        obj.RepeatTus = e.event.RepeatTus;
        obj.RepeatWed = e.event.RepeatWed;
        obj.RepeatThur = e.event.RepeatThur;
        obj.RepeatFrid = e.event.RepeatFrid;
        obj.RepeatSat = e.event.RepeatSat;
        obj.RepeatTypeID = $scope.RepeatTypeID;
        obj.RepeatEndType = $scope.RepeatEndType;
        obj.RepeatEvery = e.event.RepeatEvery;
        obj.MonthRepeatWeekDay = e.event.MonthRepeatWeekDay;
        obj.MonthRepeatOn = e.event.MonthRepeatOn;
        obj.YearRepeatOn = e.event.YearRepeatOn;
        obj.YearRepeatMonthDay = e.event.YearRepeatMonthDay;
        obj.RepeatEndAfter = e.event.RepeatEndAfter;
        obj.RepeatEndOn = e.event.RepeatEndOn;
        obj.MeetingID = e.event.MeetingID;
        obj.MonthDayOccurence = e.event.MonthDayOccurence;
        obj.YearMonthDayOccurence = e.event.YearMonthDayOccurence;
        obj.Met00102 = e.event.Met00102;

        if (obj.RepeatTypeID === 3) {
            obj.YearRepeatMonth = e.event.YearRepeatMonth;
        }
        else if (obj.RepeatTypeID === 4) {
            obj.YearRepeatMonth = e.event.YearRepeatMonth2;
        }

        var lst = [];

        if (e.event.Met00101 && e.event.Met00101.length > 0) {
            for (i = 0; i < e.event.Met00101.length; i++) {
                var objMet00101 = {};
                objMet00101.CalendarID = e.event.Met00101[i].CalendarID;
                objMet00101.MeetingID = obj.MeetingID;

                lst.push(objMet00101);
            }
        }

        obj.Met00101 = lst;

        if (obj.RepeatEndType === "Never")
            obj.RepeatEndNever = true;
        else
            obj.RepeatEndNever = false;

        if ($("#meetingRepeatTypeList").data("kendoDropDownList").value())
            obj.MeetingRepeatTypeID = $("#meetingRepeatTypeList").data("kendoDropDownList").value();

        //e.event.ID = "50";

        $scope.attendeesList = $('#attendeesList').val();

        if ($scope.attendeesList) {
            var attendee_array = [], found = false;
            attendee_array = $scope.attendeesList.split(',');

            if (obj.Met00102 && obj.Met00102.length > 0) {
                for (var i = 0; i < attendee_array.length; i++) {
                    found = false;
                    for (var j = 0; j < obj.Met00102.length; j++) {
                        if (attendee_array[i].trim() === obj.Met00102[j].AttendeeName.trim()) {
                            found = true;
                        }
                    }
                    if (!found) {
                        var objToPush = {};
                        objToPush.AttendeeName = attendee_array[i];
                        objToPush.MeetingID = obj.MeetingID;

                        obj.Met00102.push(objToPush);
                    }
                }
            }
            else {
                obj.Met00102 = [];
                for (var i = 0; i < attendee_array.length; i++) {
                    var objToPush = {};
                    objToPush.AttendeeName = attendee_array[i];
                    objToPush.MeetingID = obj.MeetingID;

                    obj.Met00102.push(objToPush);
                }
            }
        }

        $http.post('/TeamUpScheduler/SaveData', { 'Met00100': obj, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
            if (data && data.length > 0) {
                $scope.Met00100 = data;
                var scheduler = $("#teamUpRoomScheduler").data("kendoScheduler");
                var dataSource = new kendo.data.SchedulerDataSource({
                    data: $scope.Met00100,
                    schema: {
                        model: {
                            id: "MeetingID",
                            fields: {
                                MeetingID: { from: "MeetingID" },
                                Description: { from: "MeetingDescription" },
                                title: { from: "MeetingName" },
                                MeetingRoomID: { from: "MeetingRoomID" },
                                IsFullDay: { from: "IsFullDay" },
                                start: { type: "date", from: "StartDateTime", format: "{0:dd-MMM-yyyy}" },
                                end: { type: "date", from: "EndDateTime", format: "{0:dd-MMM-yyyy}" },
                                RepeatSun: { from: "RepeatSun" },
                                RepeatMon: { from: "RepeatMon" },
                                RepeatTus: { from: "RepeatTus" },
                                RepeatWed: { from: "RepeatWed" },
                                RepeatThur: { from: "RepeatThur" },
                                RepeatFrid: { from: "RepeatFrid" },
                                RepeatSat: { from: "RepeatSat" },
                                RepeatEndOn: { type: "date", from: "RepeatEndOn", format: "{0:dd-MMM-yyyy}" },
                                RepeatEndAfter: { from: "RepeatEndAfter" },
                                RepeatEndNever: { from: "RepeatEndNever" },
                                RepeatTypeID: { from: "RepeatTypeID" },
                                RepeatEvery: { from: "RepeatEvery" },
                                MonthRepeatWeekDay: { from: "MonthRepeatWeekDay" },
                                MonthRepeatOn: { from: "MonthRepeatOn" },
                                YearRepeatMonth: { from: "YearRepeatMonth" },
                                YearRepeatMonth2: { from: "YearRepeatMonth" },
                                YearRepeatOn: { from: "YearRepeatOn" },
                                YearRepeatMonthDay: { from: "YearRepeatMonthDay" },
                                StartDateTime: { from: "StartDateTime" },
                                EndDateTime: { from: "EndDateTime" },
                                SelectedValue: { from: "SelectedValue" },
                                MeetingEndGroup: { from: "MeetingEndGroup" },
                                MonthDayOccurence: { from: "MonthDayOccurence" },
                                YearMonthDayOccurence: { from: "YearMonthDayOccurence" },
                                recurrenceRule: { from: "RecurrenceRule" }
                            }
                        }
                    }
                });

                scheduler.setDataSource(dataSource);
            }
        }).finally(function () {
            $scope.RepeatEndType = "";
            $scope.RepeatTypeID = 1;
        });
    };
    $scope.SchedulerRemove = function (e) {
        var obj = {};
        obj.MeetingID = e.event.MeetingID;

        $http.post('/TeamUpScheduler/DeleteData', { 'Met00100': obj, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {

        }).finally(function () { });
    };
    $scope.LoadMeetingRepeatTypes = function () {
        $("#meetingRepeatTypeList").kendoDropDownList({
            dataTextField: "MeetingRepeatTypeName",
            dataValueField: "MeetingRepeatTypeID",
            dataSource: {
                data: $scope.MeetingRepeatTypes
            },
            select: function (e) {
                if (e.dataItem && e.dataItem.MeetingRepeatTypeID) {
                    $scope.Met00100.MeetingRepeatTypeID = e.dataItem.MeetingRepeatTypeID;

                    $scope.ShowHideRepeatPanels(e.dataItem.MeetingRepeatTypeID);
                    $scope.SetRepeatTypeText(e.dataItem.MeetingRepeatTypeID);

                    if (e.dataItem.MeetingRepeatTypeID == 4)
                        $scope.RepeatTypeID = 1;
                    else if (e.dataItem.MeetingRepeatTypeID == 5)
                        $scope.RepeatTypeID = 3;
                }
            }
        });
    };
    $scope.SetRepeatTypeText = function (MeetingRepeatTypeID) {
        if (MeetingRepeatTypeID === 2)
            $("#repeatTypeText").html("Day(s)");
        else if (MeetingRepeatTypeID === 3)
            $("#repeatTypeText").html("Week(s)");
        else if (MeetingRepeatTypeID === 4)
            $("#repeatTypeText").html("Month(s)");
        else if (MeetingRepeatTypeID === 5)
            $("#repeatTypeText").html("Year(s)");
    };

    $scope.ShowHideRepeatPanels = function (MeetingRepeatTypeID) {
        if (MeetingRepeatTypeID === 1) {
            $("#divRepeatEvery").css("display", "none");
            $("#divMeetingEnd").css("display", "none");
            $("#divRepeatOnWeek").css("display", "none");
            $("#divRepeatOnMonth").css("display", "none");
            $("#divRepeatOnYear").css("display", "none");
        }
        else if (MeetingRepeatTypeID === 2) {
            $("#divRepeatEvery").css("display", "block");
            $("#divMeetingEnd").css("display", "block");
            $("#divRepeatOnWeek").css("display", "none");
            $("#divRepeatOnMonth").css("display", "none");
            $("#divRepeatOnYear").css("display", "none");
        }
        else if (MeetingRepeatTypeID === 3) {
            $("#divRepeatEvery").css("display", "block");
            $("#divMeetingEnd").css("display", "block");
            $("#divRepeatOnWeek").css("display", "block");
            $("#divRepeatOnMonth").css("display", "none");
            $("#divRepeatOnYear").css("display", "none");
        }
        else if (MeetingRepeatTypeID === 4) {
            $("#divRepeatEvery").css("display", "block");
            $("#divMeetingEnd").css("display", "block");
            $("#divRepeatOnWeek").css("display", "none");
            $("#divRepeatOnMonth").css("display", "block");
            $("#divRepeatOnYear").css("display", "none");
        }
        else if (MeetingRepeatTypeID === 5) {
            $("#divRepeatEvery").css("display", "block");
            $("#divMeetingEnd").css("display", "block");
            $("#divRepeatOnWeek").css("display", "none");
            $("#divRepeatOnMonth").css("display", "none");
            $("#divRepeatOnYear").css("display", "block");
        }
        else {
            $("#divRepeatEvery").css("display", "none");
            $("#divMeetingEnd").css("display", "none");
            $("#divRepeatOnWeek").css("display", "none");
            $("#divRepeatOnMonth").css("display", "none");
            $("#divRepeatOnYear").css("display", "none");
        }
    };

    $scope.LoadRooms = function () {
        $("#roomList").kendoDropDownList({
            dataTextField: "MeetingRoomName",
            dataValueField: "MeetingRoomID",
            dataSource: {
                data: $scope.MeetingRooms
            },
            select: function (e) {
                if (e.item) {

                }
            }
        });
    };
    $scope.LoadCalendarNames = function () {
        $("#calendarList").kendoMultiSelect({
            dataTextField: "CalendarName",
            dataValueField: "CalendarID",
            filter: "contains",
            minLength: 1,

            //select: onSelectCustomer,
            dataSource: {
                type: "json",
                serverFiltering: true,
                transport: {
                    read: '/TeamUpScheduler/GetAllCalenderNames?KaizenPublicKey=' + sessionStorage.PublicKey
                }
            }
        });
    };
    $scope.attendeesList = "";
    $scope.LoadAttendees = function (e) {
        $scope.attendeesList = "";
        var data = e.event.Met00102;
        if (data != undefined && data.length > 0) {
            for (var i = 0; i < data.length; i++) {
                $scope.attendeesList += data[i].AttendeeName + ",";
            }
        }
        $("#attendeesList").val($scope.attendeesList);
    };


    $scope.Load();

    $("#teamUpRoomSchedule").delegate(".chkTeamUpRoom", "change", function () {
        $scope.LoadTeamUpByMeetingRoom();
    });

    $scope.LoadTeamUpByMeetingRoom = function () {
        var checked = [];
        var filter = {};

        checked = $.map($("#teamUpRoomSchedule :checked"), function (checkbox) {
            return $(checkbox).val();
        });

        var scheduler = $("#teamUpRoomScheduler").data("kendoScheduler");

        if (checked.length <= 0) {
            filter = {
                logic: "or",
                filters: [{
                    operator: "eq",
                    field: "MeetingRoomID",
                    value: ""
                }]
            };
        }
        else {
            filter = {
                logic: "or",
                filters: $.map(checked, function (value) {
                    return {
                        operator: "eq",
                        field: "MeetingRoomID",
                        value: value
                    };
                })
            };
        }

        scheduler.dataSource.filter(filter);
    };
});