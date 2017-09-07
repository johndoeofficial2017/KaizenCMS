app.controller('Event_CalendarController', function ($scope, $http) {
    $scope.Met00200 = [];
    $scope.CalenderNames = [];
    $scope.MeetingRepeatTypes = [];
    $scope.MeetingRooms = [];
    $scope.CaseTypes = [];
    $scope.CaseRefList = [];

    $scope.Met00202Lst = ["abc", "def"];

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
        $http.get('/Event_Calendar/GetAllCalenderNames?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.CalenderNames = data;
            $http.get('/CMS_CaseType/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
                $scope.CaseTypes = data;
                $http.get('/Event_Calendar/GetAllRooms?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
                    $scope.MeetingRooms = data;
                    $http.get('/Event_Calendar/GetAllMeetingRepeatTypes?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
                        $scope.MeetingRepeatTypes = data;
                        $http.get('/Event_Calendar/GetAllMeetings?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
                            $scope.Met00200 = data;
                            //$scope.Met00200[0].recurrenceRule= "FREQ=DAILY;COUNT=5;INTERVAL=1;BYDAY=MO,TU,WE,TH,FR,SA,SU";
                            $scope.Met00200.MeetingEndGroup = "MeetingEndGroup";

                            $.each($scope.Met00200, function (i, v) {
                                if (v.Met00201 && v.Met00201.length > 0) {
                                    attendeesList = jQuery.map(v.Met00201, function (n, i) {
                                        return n.CalendarID;
                                    });
                                    v.attendees = attendeesList;
                                }
                            });
                            $scope.LoadScheduler();
                        }).finally(function () { });
                    }).finally(function () { });
                }).finally(function () { });
            }).finally(function () { });
        }).finally(function () { });
    };

    $scope.LoadScheduler = function () {
        var resources = [{
            field: "attendees",
            name: "CalendarName",
            dataSource: [],
            title: "Attendee",
            multiple: true
        }];
        $.each($scope.CalenderNames, function (i, v) {
            resources[0].dataSource.push({ text: v.CalendarName, value: v.CalendarID, color: v.CalendarColor });
        });

        $("#eventCalendar").kendoScheduler({
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
            dataBound: $scope.scheduler_dataBound,
            add: $scope.SchedulerAdd,
            save: $scope.SchedulerSave,
            edit: $scope.SchedulerEdit,
            remove: $scope.SchedulerRemove,
            dataSource: {
                batch: true,
                data: $scope.Met00200,
                schema: {
                    model: {
                        id: "MeetingID",
                        fields: {
                            MeetingID: { from: "MeetingID" },
                            Description: { from: "MeetingDescription" },
                            title: { from: "MeetingName" },
                            MeetingRoomID: { from: "MeetingRoomID" },
                            TRXTypeID: { from: "TRXTypeID" },
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
                            recurrenceRule: { from: "RecurrenceRule" },
                            attendees: { from: "attendees" }
                        }
                    }
                }
            },
            resources: resources
        });
        //$scope.LoadTeamUpByAttendee();
    };

    $scope.scheduler_dataBound = function (e) {
    }

    $scope.SchedulerAdd = function (e) {
        setTimeout(function () {

        }, 100);
    };
    $scope.SchedulerEdit = function (e) {
        setTimeout(function () {
            $scope.BindKendoControlsInTemplate();
            $scope.LoadMeetingRepeatTypes();
            $scope.LoadRooms();
            $scope.LoadCaseTypes();
            $scope.LoadCalendarNames();
            $scope.LoadCaseRefList();
            $scope.LoadAttendees(e);

            if (e.event.MeetingRepeatTypeID == undefined) {
                e.event.MeetingRepeatTypeID = 1;
                $("#meetingRepeatTypeList").data("kendoDropDownList").select(0);
            }

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


            element = e.container.find("#btnAddAttendee");
            element.bind("click", { MeetingID: e.event.MeetingID }, $scope.AddAttendee);
        }, 100);
    };

    $scope.AddAttendee = function (e) {
        if ($("#attendeeName").val()) {
            var obj = {};
            obj.AttendeeName = $("#attendeeName").val();
            obj.AttendeeID = 0;

            if (e.data.MeetingID)
                obj.MeetingID = e.data.MeetingID;

            var attendeeControl = $("#attendeesList").data("kendoMultiSelect");
            var attendeeList = attendeeControl.dataSource.data();
            attendeeList.push(obj);
            debugger;
            //var dataSource = new kendo.data.SchedulerDataSource({
            //    data: attendeeList.dataSource,
            //    schema: {
            //        model: {
            //            id: "AttendeeID",
            //            fields: {
            //                AttendeeID: { from: "AttendeeID" },
            //                AttendeeName: { from: "AttendeeName" },
            //            }
            //        }
            //    }
            //});

            //attendeeControl.setDataSource(dataSource);
            var values = $.map(attendeeList, function (dataItem) {
                return dataItem.AttendeeID;
            });

            attendeeControl.value(values);
        }
    }

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
        obj.TRXTypeID = e.event.TRXTypeID;
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
        obj.Met00202 = e.event.Met00202;

        if (obj.RepeatTypeID === 3) {
            obj.YearRepeatMonth = e.event.YearRepeatMonth;
        }
        else if (obj.RepeatTypeID === 4) {
            obj.YearRepeatMonth = e.event.YearRepeatMonth2;
        }


        //Region Met00201
        var lst = [];

        if (e.event.Met00201 && e.event.Met00201.length > 0) {
            for (i = 0; i < e.event.Met00201.length; i++) {
                var objMet00201 = {};
                objMet00201.CalendarID = e.event.Met00201[i].CalendarID;
                objMet00201.MeetingID = obj.MeetingID;

                lst.push(objMet00201);
            }
        }

        obj.Met00201 = lst;
        //End Region Met00201

        //Region Met00203
        var lst2 = [];

        //if (e.event.Met00203 && e.event.Met00203.length > 0) {
        //    for (i = 0; i < e.event.Met00203.length; i++) {
        //        var objMet00203 = {};
        //        objMet00203.CaseRef = e.event.Met00203[i].CaseRef;
        //        objMet00203.MeetingID = obj.MeetingID;

        //        lst2.push(objMet00203);
        //    }
        //}

        //obj.Met00203 = lst2;
        //End Region Met00203

        var objMet00203 = {};
        objMet00203.CaseRef = $("#caseRefList").data("kendoAutoComplete").value();
        objMet00203.MeetingID = obj.MeetingID;
        lst2.push(objMet00203);

        obj.Met00203 = lst2;

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

            if (obj.Met00202 && obj.Met00202.length > 0) {
                for (var i = 0; i < attendee_array.length; i++) {
                    found = false;
                    for (var j = 0; j < obj.Met00202.length; j++) {
                        if (attendee_array[i].trim() === obj.Met00202[j].AttendeeName.trim()) {
                            found = true;
                        }
                    }
                    if (!found) {
                        var objToPush = {};
                        objToPush.AttendeeName = attendee_array[i];
                        objToPush.MeetingID = obj.MeetingID;

                        obj.Met00202.push(objToPush);
                    }
                }
            }
            else {
                obj.Met00202 = [];
                for (var i = 0; i < attendee_array.length; i++) {
                    var objToPush = {};
                    objToPush.AttendeeName = attendee_array[i];
                    objToPush.MeetingID = obj.MeetingID;

                    obj.Met00202.push(objToPush);
                }
            }
        }

        $http.post('/Event_Calendar/SaveData', { 'Met00200': obj, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
            if (data && data.length > 0) {
                $scope.Met00200 = data;
                $.each($scope.Met00200, function (i, v) {
                    if (v.Met00201 && v.Met00201.length > 0) {
                        attendeesList = jQuery.map(v.Met00201, function (n, i) {
                            return n.CalendarID;
                        });
                        v.attendees = attendeesList;
                    }
                });
                var scheduler = $("#eventCalendar").data("kendoScheduler");
                var dataSource = new kendo.data.SchedulerDataSource({
                    data: $scope.Met00200,
                    schema: {
                        model: {
                            id: "MeetingID",
                            fields: {
                                MeetingID: { from: "MeetingID" },
                                Description: { from: "MeetingDescription" },
                                title: { from: "MeetingName" },
                                MeetingRoomID: { from: "MeetingRoomID" },
                                TRXTypeID: { from: "TRXTypeID" },
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
                                recurrenceRule: { from: "RecurrenceRule" },
                                attendees: { from: "attendees" }
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

        $http.post('/Event_Calendar/DeleteData', { 'Met00200': obj, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {

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
                    $scope.Met00200.MeetingRepeatTypeID = e.dataItem.MeetingRepeatTypeID;

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

    $scope.LoadCaseTypes = function () {
        $("#caseTypeList").kendoDropDownList({
            dataTextField: "TrxTypeName",
            dataValueField: "TRXTypeID",
            dataSource: {
                data: $scope.CaseTypes
            },
            select: function (e) {
                //$scope.SetCaseRef(e.dataItem.TRXTypeID);
            }
        });
    };

    $scope.SetCaseRef = function (TRXTypeID, searchText) {
        //$http.get('/Event_Calendar/GetAllCM00203?KaizenPublicKey=' + sessionStorage.PublicKey,
        $http.get('/Event_Calendar/SearchCaseRefCM00203?KaizenPublicKey=' + sessionStorage.PublicKey,
            { params: { TRXTypeID: TRXTypeID, searchText: searchText } }).success(function (data) {
                if (data && data.length > 0) {
                    var caseRefList = $("#caseRefList").data('kendoAutoComplete');
                    caseRefList.setDataSource(new kendo.data.DataSource({ data: [] }));
                    caseRefList.setDataSource(new kendo.data.DataSource({ data: data }));
                    caseRefList.refresh();
                }
            }).finally(function () { });
    };
    $scope.IsCaseRefSetected = false;
    $scope.LoadCaseRefList = function () {
        $("#caseRefList").kendoAutoComplete({
            dataTextField: "CaseRef",
            filter: "contains",
            minLength: 3,
            dataSource: {
                type: "json",
                serverFiltering: true,
                transport: {
                    read: {
                        url: '/Event_Calendar/SearchCaseRefCM00203?KaizenPublicKey=' + sessionStorage.PublicKey,
                    },
                    parameterMap : function (data, type) {
                        if (type === "read") {
                            return {
                                TRXTypeID: $("#caseTypeList").data("kendoDropDownList").value(),
                                searchText: $("#caseRefList").data("kendoAutoComplete").value(),
                            }
                        }
                    }
                }
            }
        });
    };

    $scope.LoadRooms = function () {
        $("#roomList").kendoDropDownList({
            dataTextField: "MeetingRoomName",
            dataValueField: "MeetingRoomID",
            dataSource: {
                data: $scope.MeetingRooms
            },
            select: function (e) {
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
                    read: '/Event_Calendar/GetAllCalenderNames?KaizenPublicKey=' + sessionStorage.PublicKey
                }
            }
        });
    };
    $scope.attendeesList = "";
    $scope.LoadAttendees = function (e) {
        $scope.attendeesList = "";
        var data = e.event.Met00202;
        if (data != undefined && data.length > 0) {
            for (var i = 0; i < data.length; i++) {
                $scope.attendeesList += data[i].AttendeeName + ",";
            }
        }
        $("#attendeesList").val($scope.attendeesList);
    };

    $scope.Load();

    $("#eventCalendar").delegate(".chkTeamUp", "change", function () {
        $scope.LoadTeamUpByAttendee();
    });

    $scope.LoadTeamUpByAttendee = function () {
        var checked = [];
        var filter = {};

        checked = $.map($("#teamUpSchedule :checked"), function (checkbox) {
            return $(checkbox).val();
        });

        var scheduler = $("#eventCalendar").data("kendoScheduler");

        //if (checked.length > 0) {
        filter = {
            field: "Met00201",
            operator: function (items, filterValue) {

                var intersect = MyFunctions.getIntersect(items, checked);
                if (items != undefined && items.length == 0)
                    return true;

                if (intersect != undefined && intersect.length > 0) return true;
                return false;
            },
            value: checked
        };
        //}

        scheduler.dataSource.filter(filter);
    };

    MyFunctions = {
        getIntersect: function (arr1, arr2) {
            var intersect = [];
            var found = false;

            if (arr1 != undefined && arr2 != undefined) {
                for (i = 0; i < arr1.length; i++) {
                    for (j = 0; j < arr2.length; j++) {
                        if (arr1[i].CalendarID.toString() == arr2[j].toString()) {
                            intersect.push(arr2[j]);
                        }
                    }
                }
            }

            return intersect;
        }
    }


});