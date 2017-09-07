app.controller('SysTaskController', function ($scope, $rootScope, $http) {
    $scope.Sys00100 = {};
    $scope.Sys00101 = {};
    $scope.Sys00102 = {};
    $scope.PagesSys00100 = [];
    $scope.Sys00101List = [];
    $scope.Sys00102List = [];
    $scope.SelectedLookUp = {};
    $scope.ShowScreenSys00102 = false;
    $scope.toolbarOptions = {
        items: [
                 {
                     type: "button", text: "<span class=\"fa fa-plus-circle\"></span> New Task",
                     attributes: { "class": "k-primary" },
                     click: function (e) { $scope.AddSys00100(); }
                 },
                 {
                     type: "button", text: "<span class=\"fa fa-tasks\"></span> Today Tasks",
                     attributes: { "class": "k-primary" },
                     click: function (e) {
                         $scope.TodayTasks();
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
    $scope.Priortires = [{ PriorityID: '1         ', PriorityName: 'High' },
        { PriorityID: '2         ', PriorityName: 'Meduim' },
        { PriorityID: '3         ', PriorityName: 'Low' }];
    $scope.TaskTypes = [{ TaskTypeID: '1         ', TaskTypeName: 'Other' },
      { TaskTypeID: '2         ', TaskTypeName: 'Important' }];

    $scope.MainGridURL = "/" + $scope.ToolBar.ActiveScreen.ID + "/MainGrid?KaizenPublicKey=" + sessionStorage.PublicKey;
    $scope.TaskActions = [];
    $scope.LoadLookUp = function () {
        //$http.get('/CMS_TaskType/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
        //    $scope.TaskTypes = data;
        //}).finally(function () {
        //    if ($scope.TaskTypes.length > 0) {
        //        $scope.Sys00100.TaskTypeID = $scope.TaskTypes[0].TaskTypeID
        //    }
        //});
        //$http.get('/CMS_TaskPriority/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
        //    $scope.TaskPriorities = data;
        //}).finally(function () {
        //    if ($scope.TaskPriorities.length > 0) {
        //        $scope.Sys00100.PriorityID = $scope.TaskPriorities[0].PriorityID
        //    }
        //});
    };
    $scope.LoadLookUp();

    $scope.LoadSys00100 = function (TaskID) {
        if (TaskID != undefined) {
            $http.get('/SysTask/GetSingle?KaizenPublicKey=' + sessionStorage.PublicKey + "&TaskID=" + TaskID).success(function (data) {
                $scope.Sys00100 = data;
                if ($scope.Sys00100.Sys00101) {
                    $scope.Sys00101List = $scope.Sys00100.Sys00101;
                }
                $scope.SelectedLookUp.SelectedTaskType = $scope.Sys00100.TaskTypeID;
                $scope.SelectedLookUp.SelectedPriority = $scope.Sys00100.PriorityID;
            }).finally(function () {
                $scope.Sys00100.Status = 2;
                $scope.Sys00100.TaskStartDate = parseMSDate($scope.Sys00100.TaskStartDate);
                $scope.Sys00100.TaskEndDate = parseMSDate($scope.Sys00100.TaskEndDate);
                $scope.Sys00100.minDate = $scope.Sys00100.TaskStartDate;
                $scope.Sys00100.maxDate = $scope.Sys00100.TaskEndDate;
                $scope.Sys00100.AssignDate = parseMSDate($scope.Sys00100.AssignDate);
                $http.get('/SysTask/GetAllTaskActions?KaizenPublicKey=' + sessionStorage.PublicKey, { params: { TaskID: TaskID } }).success(function (data) {
                    $scope.TaskActions = data;
                }).finally(function () {
                    $scope.TaskActions.forEach(function (element, index) {
                        element.TaskDate = parseMSDate(element.TaskDate);
                    })
                });
            });
        }
    };
    $scope.LoadSys00100Page = function (ActionName) {
        removeEntityPage($scope.PagesSys00100);
        var URIPath = "/Tools/" + $scope.ToolBar.ActiveScreen.ID + "/" + ActionName + "?KaizenPublicKey=" + sessionStorage.PublicKey;
        var Page = { url: URIPath, ActionName: ActionName };
        $scope.PagesSys00100.push(Page);
        //$scope.$apply();
    };

    $scope.AddAttachment = function () {
        $scope.ShowScreenSys00102 = true;
    }; 

    $scope.HideAttachmentScreen = function () {
        $scope.ShowScreenSys00102 = false;
    };

    $scope.UploadedData = function (response) {
        if (response) {
            $scope.Sys00102.DocumentID = response.DocumentID;
            $scope.Sys00102.PhotoExtension = response.PhotoExtension;
        }
    };

    $scope.AddDataSys00102 = function () {
        if ($scope.Sys00102) {
            var obj_extend = jQuery.extend({}, $scope.Sys00102);
            $scope.Sys00102List.push(obj_extend);
            $scope.Sys00102 = {};
        }
    };

    //------------------------Filter
    $scope.MYFilter.get = function (field) {
        var DS = {};
        if (field == 'PriorityID') {
            DS.key = "PriorityName";
            DS.source = $scope.TaskPriorities;
        }
        if (field == 'TaskTypeID') {
            DS.key = "TaskTypeName";
            DS.source = $scope.TaskTypes;
        }
        //alert(JSON.stringify($scope.Filter.source));
        return DS;
    };

    $scope.UserBack = function (user) {
        if (user == null) return;
        switch ($scope.CurrentControl) {
            case 'UserAssignTO':
                $scope.Sys00100.UserAsginTO = user.UserName;
                break;
            case 'UserAssignTOSys00101':
                $scope.Sys00101.UserAsginTO = user.UserName;
                break;
        }
    };

    $scope.LoadTaskTypes = function () {
        $http.get('/CMS_TaskType/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.TaskTypeList = data;
        }).finally(function () {

        });
    };

    $scope.LoadPriorities = function () {
        $http.get('/CMS_TaskPriority/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.PriorityList = data;
        }).finally(function () {

        });
    };

    $scope.TaskTypeChanged = function () {
        if (angular.isDefined($scope.SelectedLookUp.SelectedTaskType)) {
            $scope.Sys00100.TaskTypeID = $scope.SelectedLookUp.SelectedTaskType;
        }
    };

    $scope.PriorityChanged = function () {
        if (angular.isDefined($scope.SelectedLookUp.SelectedPriority)) {
            $scope.Sys00100.PriorityID = $scope.SelectedLookUp.SelectedPriority;
        }
    };


    $scope.AddSys00100 = function () {
        $scope.LoadSys00100Page('Create');
        $scope.Clear();
        $scope.Sys00100.Status = 1;
        $scope.Sys00100.TaskProgress = 0;
        $scope.LoadTaskTypes();
        $scope.LoadPriorities();
        //$(window).on('resize', function () {
        //    var sliders = $("[data-role='slider']");
        //    sliders.each(function (index, ele) {
        //        var slider = $(ele).getKendoSlider();
        //        slider.wrapper.css("width", "100%");
        //        slider.resize();
        //    });
        //});
    };
    $scope.EditSys00100 = function (TaskID) {
        $scope.AddSys00100();
        $scope.LoadSys00100(TaskID);
        $scope.Sys00100.Status = 2;
        $scope.Sys00101.TaskID = TaskID;
        $scope.ShowScreenSys00102 = false;
    };
    $scope.SaveData = function () {
        $scope.Sys00100.TaskStartDate = kendo.toString($scope.Sys00100.TaskStartDate, "MM-dd-yyyy h:mm:ss tt");
        $scope.Sys00100.TaskEndDate = kendo.toString($scope.Sys00100.TaskEndDate, "MM-dd-yyyy h:mm:ss tt");
        $http.post('/SysTask/SaveData', { 'Sys00100': $scope.Sys00100, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
            if (data.Status == true) {
                $.bigBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#739E73",
                    timeout: 8000,
                    icon: "fa fa-bolt swing animated"
                });
                $scope.Cancel();
                $scope.GridRefresh();
            }
            else {
                $.bigBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#739E73",
                    timeout: 8000,
                    icon: "fa fa-bolt swing animated"
                });
            }
        }).error(function (data, status, headers, config) { alert(); });
    };

    $scope.SaveDataSys00101 = function () {
        $scope.Sys00101.TaskID = $scope.Sys00100.TaskID;
        var taskDate = $scope.Sys00101.TaskDate;
        $scope.Sys00101.TaskDate = kendo.toString($scope.Sys00101.TaskDate, "MM-dd-yyyy h:mm:ss tt");
        $scope.Sys00101.Sys00102 = $scope.Sys00102List;
        $http.post('/SysTask/SaveTaskAction', { 'Sys00101': $scope.Sys00101, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
            if (data.Status == true) {
                $scope.Sys00101.TaskDate = taskDate;
                $scope.Sys00101List.push($scope.Sys00101);
                $scope.Sys00101 = {};
                $scope.Sys00102List = [];
                $scope.Sys00100.Status = 2;
                $scope.ShowScreenSys00102 = false;

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
                    color: "#739E73",
                    timeout: 8000,
                    icon: "fa fa-bolt swing animated"
                });
            }
        }).error(function (data, status, headers, config) { alert(); });
    };
    $scope.UpdateData = function () {
        $scope.Sys00100.TaskStartDate = new Date(parseMSDate($scope.Sys00100.TaskStartDate));
        $scope.Sys00100.TaskEndDate = new Date(parseMSDate($scope.Sys00100.TaskEndDate));
        $scope.Sys00100.AssignDate = new Date(parseMSDate($scope.Sys00100.AssignDate));
        $http.post('/SysTask/UpdateData', { 'Sys00100': $scope.Sys00100, 'PhotoChanged': $scope.PhotoChanged, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
            if (data.Status == true) {
                Notify(data.Massage, 'bottom-right', '5000', 'success', 'fa-check', true);
                $scope.Cancel();
                $scope.GridRefresh();
            }
            else {
                Notify(data.Massage, 'bottom-right', '5000', 'danger', 'fa-bolt', true);
            }
        }).error(function (data, status, headers, config) { alert(); });
    };
    $scope.Clear = function () {
        $scope.Sys00100 = {};
        $scope.Sys00100.Status = 1;
        $scope.Sys00100.TaskProgress = 0;
        //if ($scope.TaskTypes.length > 0) {
        //    $scope.Sys00100.TaskTypeID = $scope.TaskTypes[0].TaskTypeID
        //}
        //if ($scope.TaskPriorities.length > 0) {
        //    $scope.Sys00100.PriorityID = $scope.TaskPriorities[0].PriorityID
        //}
        var now = new Date();
        $scope.Sys00100.minDate = new Date(now.getFullYear() - 50, 1, 1, 0, 0, 0, 0);
        $scope.Sys00100.maxDate = new Date(now.getFullYear() + 50, 1, 1, 0, 0, 0, 0);

    };
    $scope.Print = function () {
        if ($scope.Sys00100.PrintType == "PDF") {
            // Convert the DOM element to a drawing using kendo.drawing.drawDOM
            kendo.drawing.drawDOM($(".widget-body"))
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
                    fileName: "Task ID " + $scope.Sys00100.TaskID.trim() + ".pdf",
                    proxyURL: "@Url.Action('Pdf_Export_Save')"
                });
            });
        }
        else if ($scope.Sys00100.PrintType == "Image") {
            // Convert the DOM element to a drawing using kendo.drawing.drawDOM
            kendo.drawing.drawDOM($(".widget-body"))
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
                    fileName: "Debtor ID " + $scope.Sys00100.DebtorID.trim() + ".png",
                    proxyURL: "@Url.Action('Pdf_Export_Save')"
                });
            });
        }
        else if ($scope.Sys00100.PrintType == "SVG") {
            // Convert the DOM element to a drawing using kendo.drawing.drawDOM
            kendo.drawing.drawDOM($(".widget-body"))
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
                    fileName: "Debtor ID " + $scope.Sys00100.DebtorID.trim() + ".svg",
                    proxyURL: "@Url.Action('Pdf_Export_Save')"
                });
            });
        }
    };
    $scope.Delete = function () {
        bootbox.confirm("Are you sure?", function (result) {
            if (result) {
                $http.post('/SysTask/DeleteData', { 'Sys00100': $scope.Sys00100, 'KaizenPublicKey': sessionStorage.PublicKey })
   .success(function (data) {
       if (data.Status == true) {
           Notify(data.Massage, 'bottom-right', '5000', 'success', 'fa-check', true);
           $scope.Cancel();
           $scope.GridRefresh();
       }
       else {
           Notify(data.Massage, 'bottom-right', '5000', 'danger', 'fa-bolt', true);
       }
   }).error(function (data, status, headers, config) { alert(); });
            }
        });
    };
    $scope.Cancel = function () {
        $scope.Clear();
        $scope.PagesSys00100 = [];
        $scope.TaskActions = [];
    };

    $scope.AgentBack = function (agent) {
        if (agent != null) {
            $scope.Sys00100.AgentID = agent.AgentID;
        }
    };
    $scope.openTaskActionWindow = function () {
        $scope.Sys00101 = {};
        $scope.TaskActionWindow.center().toFront().open();
        $scope.TaskActionWindow.refresh({
            url: "/SysTask/TaskAction?KaizenPublicKey=" + sessionStorage.PublicKey
        });
    };
    $scope.SaveTaskAction = function () {
        $scope.Sys00101.TaskID = $scope.Sys00100.TaskID;
        $http.post('/SysTask/SaveTaskAction', { 'Sys00101': $scope.Sys00101, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
            if (data.Status == true) {
                Notify(data.Massage, 'bottom-right', '5000', 'success', 'fa-check', true);
                $http.get('/SysTask/GetAllTaskActions?KaizenPublicKey=' + sessionStorage.PublicKey, { params: { TaskID: TaskID } }).success(function (data) {
                    $scope.TaskActions = data;
                }).finally(function () {
                    $scope.TaskActions.forEach(function (element, index) {
                        element.TaskDate = parseMSDate(element.TaskDate);
                    })
                    $scope.CloseTaskActionWindow();
                });
            }
            else {
                Notify(data.Massage, 'bottom-right', '5000', 'danger', 'fa-bolt', true);
            }
        }).error(function (data, status, headers, config) { alert(); });
    };
    $scope.CloseTaskActionWindow = function () {
        $scope.TaskActionWindow.close();
    };

    //----------------------     
    $scope.TodayTasks = function () {
        $scope.LoadSys00100Page('TodayTasks');
        $scope.Clear();
        $http.get('/SysTask/GetTodayTasks?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.TodayTaskList = data;
        }).finally(function () {
            $scope.TodayTaskList.forEach(function (element, index) {
                element.TaskStartDate = kendoparsetoDate(element.TaskStartDate);
                element.TaskEndDate = kendoparsetoDate(element.TaskEndDate);
                element.AssignDate = kendoparsetoDate(element.AssignDate);
                var obj = $scope.functiontofindObjectByKeyValue($scope.Priortires, 'PriorityID', element.PriorityID)
                element.PriorityName = obj.PriorityName;
                var type = $scope.functiontofindObjectByKeyValue($scope.TaskTypes, 'TaskTypeID', element.TaskTypeID)
                element.TaskTypeName = type.TaskTypeName;
            })
        });
    };
    $scope.TaskView = function (task) {
        removeEntityPage($scope.PagesSys00100);
        var URIPath = "/" + $scope.ToolBar.ActiveScreen.ID + "/" + "TaskView" + "?KaizenPublicKey=" + sessionStorage.PublicKey;
        var Page = { url: URIPath, ActionName: 'TaskView' };
        $scope.PagesSys00100.push(Page);
        $scope.Clear();
        $scope.Sys00100 = task;
        $http.get('/SysTask/GetAllTaskActions?KaizenPublicKey=' + sessionStorage.PublicKey, { params: { TaskID: task.TaskID } }).success(function (data) {
            $scope.TaskActionList = data;
        }).finally(function () {
            $scope.TaskActionList.forEach(function (element, index) {
                element.TaskDate = kendoparsetoDate(element.TaskDate);
            })
        });

    };

});
