app.controller('CMS_CaseStatuscontroller', function ($scope, $http) {
    $scope.CM00700 = {};
    $scope.PagesCM00700 = [];
    $scope.toolbarOptions = {
        items: [
                 {
                     type: "button", text: "<span class=\"fa fa-plus-circle\"></span> Case Status ",
                     attributes: { "class": "btn-primary" },
                     click: function (e) {
                         $scope.$apply(function () {
                             $scope.AddCM00700();
                         });
                     }
                 },
                 {
                     type: "splitButton",
                     text: "Configuration",
                     menuButtons: [
                         {
                             text: "Predefined Status Tasks", click: function (e) {
                                 $scope.$apply(function () {
                                     $scope.StatusTask();
                                 });
                             }
                         },
                         {
                             text: "Predefined Status Script", click: function (e) {
                                 $scope.$apply(function () {
                                     $scope.StatusScript();
                                 });
                             }
                         },
                         {
                             text: "Predefined Status Childs", click: function (e) {
                                 $scope.$apply(function () {
                                     $scope.StatusChild();
                                 });
                             }
                         },
                            {
                                text: "Case Status Role", click: function (e) {
                                    $scope.$apply(function () {
                                        $scope.StatusRole();
                                    });
                                }
                            },
                              {
                                  text: "Case Status User", click: function (e) {
                                      $scope.$apply(function () {
                                          $scope.StatusUser();
                                      });
                                  }
                              },
                              {
                                  text: "Case Status Fields", click: function (e) {
                                      $scope.$apply(function () {
                                          $scope.StatusFields();
                                      });
                                  }
                              },
                              {
                                  text: "Case Status Views", click: function (e) {
                                      $scope.$apply(function () {
                                          $scope.StatusViews();
                                      });
                                  }
                              },
                              {
                                  text: "Case Status Views Role", click: function (e) {
                                      $scope.$apply(function () {
                                          $scope.StausViewsRoles();
                                      });
                                  }
                              },
                              {
                                  text: "Case Status Views User", click: function (e) {
                                      $scope.$apply(function () {
                                          $scope.StatusViewsUsers();
                                      });
                                  }
                              },{
                                  text: "Case Status Views Field", click: function (e) {
                                      $scope.$apply(function () {
                                          $scope.StatusViewsFields();
                                      });
                                  }
                              }, {
                                  text: "Case Status Lookup", click: function (e) {
                                      $scope.$apply(function () {
                                          $scope.StatusLookup();
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
    $scope.LoadCM00700 = function (CaseStatusID) {
        if (angular.isUndefined($scope.CM00700.CaseStatusID)) {
            $http.get('/CMS_CaseStatus/GetSingle?KaizenPublicKey=' + sessionStorage.PublicKey + "&CaseStatusID=" + CaseStatusID).success(function (data) {
                $scope.CM00700 = data;
            }).finally(function () { $scope.CM00700.Status = 2; });
        }
    }
    $scope.LoadCM00700Page = function (ActionName) {
        removeEntityPage($scope.PagesCM00700);
        var URIPath = "/CMS/" + $scope.ToolBar.ActiveScreen.ID + "/" + ActionName + "?KaizenPublicKey=" + sessionStorage.PublicKey;
        var Page = { url: URIPath, ActionName: ActionName };
        $scope.PagesCM00700.push(Page);
    }
    $scope.LoadLookUp = function () {
        $http.get('/CMS_Set_StatusActionType/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.StatusActionTypes = data;
        }).finally(function () { });
        $http.get('/CMS_CaseStatus/StatusHasChildDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.StatusParents = data;
        }).finally(function () { });
    };
    $scope.LoadLookUp();

    $scope.AddCM00700 = function () {
        $scope.LoadCM00700Page('Create');
        $scope.Clear();
        $scope.CM00700.Status = 1;
    }
    $scope.EditCM00700 = function (CaseStatusID) {
        $scope.LoadCM00700Page('Create');
        $scope.LoadCM00700(CaseStatusID);
    };
    $scope.SaveData = function () {
        $http.post('/CMS_CaseStatus/SaveData', { 'CM00700': $scope.CM00700, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
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
        $http.post('/CMS_CaseStatus/UpdateData', { 'CM00700': $scope.CM00700, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
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
        $scope.CM00700 = {};
        $scope.CM00025 = {};
        $scope.CM00026 = {};
        $scope.CM00700.SelectedStatus = {};
        $scope.CM00025.TaskStartDate = new Date();
        $scope.CM00025.TaskEndDate = new Date();
        $scope.CM00025.AssignDate = new Date();

        $scope.Roles = [];
        $scope.CM00051 = {};
        $scope.CM00062 = {};
        $scope.CM00064 = {};
        $scope.StatusUsers = [];
        $scope.CaseStatusFieldsList = [];
        $scope.CaseStatusViewsList = [];
        $scope.CaseStatusViewsFieldsList = [];
    };
    $scope.Print = function () {
        alert('Printer not configured');
    };
    $scope.DeleteData = function () {
        $.SmartMessageBox({
            title: "Delete Record",
            content: "Are you sure you want to delete this?",
            buttons: '[No][Yes]'
        }, function (ButtonPressed) {
            if (ButtonPressed === "Yes") {

                $http.post('/CMS_CaseStatus/DeleteData', { 'CM00700': $scope.CM00700, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
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
        $scope.PagesCM00700 = [];
        $scope.InsertedTasks = [];
        $scope.UpdatedTasks = [];
        $scope.DeletedTasks = [];
        $scope.StatusTasks = [];

        $scope.InsertedScripts = [];
        $scope.UpdatedScripts = [];
        $scope.DeletedScripts = [];
        $scope.StatusScripts = [];

        $scope.UpdatedStatusChild = [];
    };

    $scope.PTPRequiredChanged = function () {
        if (angular.isDefined($scope.CM00700.IsPTPRequired) && $scope.CM00700.IsPTPRequired) {
            $scope.CM00700.IsPTP = true;
        }
    };

    //--------------------- Status Tasks -------------------------------
    $scope.StatusTask = function () {
        $scope.LoadCM00700Page('StatusTask')
        $scope.Clear();
        $http.get('/CMS_TaskType/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.TaskTypes = data;
            if ($scope.TaskTypes.length > 0) {
                $scope.CM00025.TaskTypeID = $scope.TaskTypes[0].TaskTypeID
            }
        }).finally(function () { });

        $http.get('/CMS_CaseStatus/StatusHasTasksDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.CaseStatusesHasTasks = data;
        }).finally(function () {
            $scope.CaseStatusHasTasksOptions = {
                filter: "startswith",
                model: "SelectedStatus",
                optionLabel: "-- Select Status --",
                dataTextField: "CaseStatusname",
                dataValueField: "CaseStatusID",
                dataSource: $scope.CaseStatusesHasTasks,
                value: $scope.CM00700.CaseStatusID
            };
        });
        $http.get('/CMS_TaskPriority/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.TaskPriorities = data;
        }).finally(function () { });
        $scope.CM00025.status = 0;
        $scope.CM00025.TaskStartDate = new Date();
        $scope.CM00025.TaskEndDate = new Date();
        $scope.CM00025.AssignDate = new Date();
    };
    $scope.LoadTaskCMS_CaseStatus = function (CaseStatusID) {
        $http.get('/CMS_CaseStatus/GetAllByCaseStatusID?KaizenPublicKey=' + sessionStorage.PublicKey, { params: { CaseStatusID: CaseStatusID } }).success(function (data) {
            if (data.length > 0) {
                $scope.StatusTasks = data;
            } else {
                $scope.StatusTasks = [];
            }
        }).finally(function () {
            $scope.CM00025.status = 0;
            $scope.StatusTasks.forEach(function (element, index) {
                element.TaskStartDate = new Date(element.TaskStartDate);
                element.TaskEndDate = new Date(element.TaskEndDate);
                element.AssignDate = new Date(element.AssignDate);
            })
        });
    };
    $scope.StatusChanged = function () {
        if (angular.isDefined($scope.CM00700.SelectedStatus)) {
            $scope.CM00700.CaseStatusID = $scope.CM00700.SelectedStatus.CaseStatusID;
            $scope.LoadTaskCMS_CaseStatus($scope.CM00700.CaseStatusID);
        }
    };
    $scope.AgentBack = function (agent) {
        if (agent != null) {
            $scope.CM00025.AgentID = agent.AgentID;
        }
    };

    $scope.InsertedTasks = [];
    $scope.UpdatedTasks = [];
    $scope.DeletedTasks = [];
    var indx;
    $scope.AddNewTask = function () {
        $scope.CM00025.status = 1;
        $scope.CM00025.CaseStatusID = $scope.CM00700.CaseStatusID;
        $scope.StatusTasks.push($scope.CM00025);
        $scope.CM00025 = {};
        $scope.CM00025.status = 0;
        $scope.CM00025.TaskStartDate = new Date();
        $scope.CM00025.TaskEndDate = new Date();
        $scope.CM00025.AssignDate = new Date();

    };
    $scope.UpdateTask = function () {
        if ($scope.CM00025.status == 0 || angular.isUndefined($scope.CM00025.status)) {
            $scope.CM00025.status = 2;
        }
        $scope.StatusTasks.splice(indx, 1, $scope.CM00025);
        $scope.CM00025 = {};
        $scope.CM00025.status = 0;
    };
    $scope.EditTask = function (task) {
        indx = $scope.StatusTasks.indexOf(task);
        $scope.CM00025 = task;
        var Obj_indx = $scope.functiontofindIndexByKeyValue($scope.StatusTasks, "CaseStatusID", $scope.CM00025.CaseStatusID);
        var obj = $scope.StatusTasks[Obj_indx];
        $scope.CM00700.SelectedStatus = obj;
        if ($scope.CM00025.status == 0 || angular.isUndefined($scope.CM00025.status)) {
            $scope.CM00025.status = 2;
        }
    };

    $scope.SaveStatusTask = function () {
        for (var i = 0; i < $scope.StatusTasks.length; i++) {
            var item = $scope.StatusTasks[i];
            if (item.status == 1) {
                var insert_tmp = {
                    CaseStatusID: item.CaseStatusID,
                    TaskTitle: item.TaskTitle,
                    TaskTypeID: item.TaskTypeID,
                    TaskDescription: item.TaskDescription,
                    PriorityID: item.PriorityID,
                    TaskStartDate: item.TaskStartDate,
                    TaskEndDate: item.TaskEndDate,
                    AgentID: item.AgentID,
                    AssignDate: item.AssignDate,
                    TaskProgress: item.TaskProgress
                };
                $scope.InsertedTasks.push(insert_tmp);
            }
            else if (item.status == 2) {
                var update_tmp = {
                    TaskID: item.TaskID,
                    CaseStatusID: item.CaseStatusID,
                    TaskTypeID: item.TaskTypeID,
                    TaskTitle: item.TaskTitle,
                    TaskDescription: item.TaskDescription,
                    PriorityID: item.PriorityID,
                    TaskStartDate: item.TaskStartDate,
                    TaskEndDate: item.TaskEndDate,
                    AgentID: item.AgentID,
                    AssignDate: item.AssignDate,
                    TaskProgress: item.TaskProgress
                };
                $scope.UpdatedTasks.push(update_tmp);
            }
            else if (item.status == 3) {
                var delete_tmp = {
                    TaskID: item.TaskID,
                    CaseStatusID: item.CaseStatusID,
                    TaskTypeID: item.TaskTypeID,
                    TaskTitle: item.TaskTitle,
                    TaskDescription: item.TaskDescription,
                    PriorityID: item.PriorityID,
                    TaskStartDate: item.TaskStartDate,
                    TaskEndDate: item.TaskEndDate,
                    AgentID: item.AgentID,
                    AssignDate: item.AssignDate,
                    TaskProgress: item.TaskProgress
                };
                $scope.DeletedTasks.push(delete_tmp);
            }
        }
        alert($scope.InsertedTasks.length);
        if ($scope.InsertedTasks.length > 0) {
            $http({
                url: '/CMS_CaseStatus/SaveTask?KaizenPublicKey=' + sessionStorage.PublicKey,
                method: "POST",
                data: JSON.stringify($scope.InsertedTasks),
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
        if ($scope.UpdatedTasks.length > 0) {
            $http({
                url: '/CMS_CaseStatus/UpdateTask?KaizenPublicKey=' + sessionStorage.PublicKey,
                method: "POST",
                data: JSON.stringify($scope.UpdatedTasks),
                dataType: "json",
                headers: {
                    'Content-Type': 'application/json; charset=utf-8'
                }
            }).success(function (data) {
                if (data.Status == true) {
                    //Notify(data.Massage, 'bottom-right', '5000', 'success', 'fa-check', true);
                }
                else {
                    //Notify(data.Massage, 'bottom-right', '5000', 'danger', 'fa-bolt', true);
                }
            }).error(function (data, status, headers, config) { alert(); });
        }
        if ($scope.DeletedTasks.length > 0) {
            $http({
                url: '/CMS_CaseStatus/DeleteTask?KaizenPublicKey=' + sessionStorage.PublicKey,
                method: "POST",
                data: JSON.stringify($scope.DeletedTasks),
                dataType: "json",
                headers: {
                    'Content-Type': 'application/json; charset=utf-8'
                }
            }).success(function (data) {
                if (data.Status == true) {
                    //Notify(data.Massage, 'bottom-right', '5000', 'success', 'fa-check', true);
                    $.bigBox({
                        title: data.Massage,
                        content: data.Description,
                        color: "#C46A69",
                        timeout: 8000,
                        icon: "fa fa-bolt swing animated"
                    });
                }
                else {
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

        $scope.Cancel();
    };
    $scope.RemoveStatusTask = function (task, index) {

        $.SmartMessageBox({
            title: "Delete Record",
            content: "Are you sure?",
            buttons: '[No][Yes]'
        }, function (ButtonPressed) {
            if (ButtonPressed === "Yes") {
                if (task.status == 1)
                    $scope.StatusTasks.splice(index, 1);
                else
                    task.status = 3;
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

    //--------------------- Status Scripts -------------------------------
    $scope.StatusScript = function () {
        $scope.LoadCM00700Page('StatusScript')
        $scope.Clear();
        $http.get('/CMS_CaseStatus/StatusHasScriptsDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.CaseStatusesHasScripts = data;
        }).finally(function () {
            $scope.CaseStatusHasScriptsOptions = {
                filter: "startswith",
                model: "SelectedStatus",
                optionLabel: "-- Select Status --",
                dataTextField: "CaseStatusname",
                dataValueField: "CaseStatusID",
                dataSource: $scope.CaseStatusesHasScripts,
                value: $scope.CM00700.CaseStatusID
            };
        });
        $scope.CM00026.status = 0;
    };
    $scope.LoadScriptCMS_CaseStatus = function (CaseStatusID) {
        $http.get('/CMS_CaseStatus/GetAllScriptsByCaseStatusID?KaizenPublicKey=' + sessionStorage.PublicKey, { params: { CaseStatusID: CaseStatusID } }).success(function (data) {
            if (data.length >= 0) {
                $scope.StatusScripts = data;
            } else {
                $scope.StatusScripts = [];
            }
        }).finally(function () {
            $scope.CM00026.status = 0;
        });
    };
    $scope.ScriptStatusChanged = function () {
        $scope.CM00700.CaseStatusID = $scope.CM00700.SelectedStatus.CaseStatusID;
        $scope.LoadScriptCMS_CaseStatus($scope.CM00700.CaseStatusID);
    };

    $scope.InsertedScripts = [];
    $scope.UpdatedScripts = [];

    $scope.StatusScripts = [];
    var indx;
    $scope.AddNewScript = function () {
        $scope.CM00026.status = 1;
        $scope.CM00026.CaseStatusID = $scope.CM00700.CaseStatusID;
        $scope.StatusScripts.push($scope.CM00026);
        $scope.CM00026 = {};
        $scope.CM00026.status = 0;
    };
    $scope.UpdateScript = function () {
        if ($scope.CM00026.status == 0 || angular.isUndefined($scope.CM00026.status)) {
            $scope.CM00026.status = 2;
        }
        $scope.StatusScripts.splice(indx, 1, $scope.CM00026);
        $scope.CM00026 = {};
        $scope.CM00026.status = 0;
    };
    $scope.EditScript = function (script) {
        indx = $scope.StatusScripts.indexOf(script);
        $scope.CM00026 = script;
        var Obj_indx = $scope.functiontofindIndexByKeyValue($scope.StatusScripts, "CaseStatusID", $scope.CM00026.CaseStatusID);
        var obj = $scope.StatusScripts[Obj_indx];
        $scope.CM00700.SelectedStatus = obj;
        if ($scope.CM00026.status == 0 || angular.isUndefined($scope.CM00026.status)) {
            $scope.CM00026.status = 2;
        }
    };

    $scope.SaveStatusScript = function () {
        //waitingDialog.show();
        for (var i = 0; i < $scope.StatusScripts.length; i++) {
            var item = $scope.StatusScripts[i];
            if (item.status == 1) {
                var insert_tmp = {
                    CaseStatusID: item.CaseStatusID,
                    StatusScriptName: item.StatusScriptName,
                    StatusScriptMain: item.StatusScriptMain
                };
                $scope.InsertedScripts.push(insert_tmp);
            }
            else if (item.status == 2) {
                var update_tmp = {
                    StatusScriptID: item.StatusScriptID,
                    CaseStatusID: item.CaseStatusID,
                    StatusScriptName: item.StatusScriptName,
                    StatusScriptMain: item.StatusScriptMain
                };
                $scope.UpdatedScripts.push(update_tmp);
            }
            else if (item.status == 3) {
                var delete_tmp = {
                    StatusScriptID: item.StatusScriptID,
                    CaseStatusID: item.CaseStatusID,
                    StatusScriptName: item.StatusScriptName,
                    StatusScriptMain: item.StatusScriptMain
                };
                $scope.DeletedScripts.push(delete_tmp);
            }
        }

        if ($scope.InsertedScripts.length > 0) {
            $http({
                url: '/CMS_CaseStatus/SaveScript?KaizenPublicKey=' + sessionStorage.PublicKey,
                method: "POST",
                data: JSON.stringify($scope.InsertedScripts),
                dataType: "json",
                headers: {
                    'Content-Type': 'application/json; charset=utf-8'
                }
            }).success(function (data) {
                if (data.Status == true) {
                    Notify(data.Massage, 'bottom-right', '5000', 'success', 'fa-check', true);
                }
                else {
                    Notify(data.Massage, 'bottom-right', '5000', 'danger', 'fa-bolt', true);
                }
            }).error(function (data, status, headers, config) { alert(); });
        }
        if ($scope.UpdatedScripts.length > 0) {
            $http({
                url: '/CMS_CaseStatus/UpdateScript?KaizenPublicKey=' + sessionStorage.PublicKey,
                method: "POST",
                data: JSON.stringify($scope.UpdatedScripts),
                dataType: "json",
                headers: {
                    'Content-Type': 'application/json; charset=utf-8'
                }
            }).success(function (data) {
                if (data.Status == true) {
                    Notify(data.Massage, 'bottom-right', '5000', 'success', 'fa-check', true);
                }
                else {
                    Notify(data.Massage, 'bottom-right', '5000', 'danger', 'fa-bolt', true);
                }
            }).error(function (data, status, headers, config) { alert(); });
        }
        if ($scope.DeletedScripts.length > 0) {
            $http({
                url: '/CMS_CaseStatus/DeleteScript?KaizenPublicKey=' + sessionStorage.PublicKey,
                method: "POST",
                data: JSON.stringify($scope.DeletedScripts),
                dataType: "json",
                headers: {
                    'Content-Type': 'application/json; charset=utf-8'
                }
            }).success(function (data) {
                if (data.Status == true) {
                    //Notify(data.Massage, 'bottom-right', '5000', 'success', 'fa-check', true);
                    $.bigBox({
                        title: data.Massage,
                        content: data.Description,
                        color: "#C46A69",
                        timeout: 8000,
                        icon: "fa fa-bolt swing animated"
                    });
                }
                else {
                    //Notify(data.Massage, 'bottom-right', '5000', 'danger', 'fa-bolt', true);
                }
            }).error(function (data, status, headers, config) { alert(); });
        }
        //waitingDialog.hide();
        $scope.Cancel();
    };
    $scope.RemoveStatusScript = function (script, index) {
        $.SmartMessageBox({
            title: "Delete Record",
            content: "Are you sure?",
            buttons: '[No][Yes]'
        }, function (ButtonPressed) {
            if (ButtonPressed === "Yes") {
                if (script.status == 1)
                    $scope.StatusScripts.splice(index, 1);
                else
                {
                    $http({
                        url: '/CMS_CaseStatus/DeleteStatusScript?KaizenPublicKey='
                            + sessionStorage.PublicKey,
                        method: "POST",
                        data: {
                            'oCM00026': script
                        },
                        dataType: "json",
                        headers: {
                            'Content-Type': 'application/json; charset=utf-8'
                        }
                    }).success(function (data) {
                        if (data.Status == true) {
                            $scope.ScriptStatusChanged();
                            //Notify(data.Massage, 'bottom-right', '5000', 'success', 'fa-check', true);
                            //$scope.Cancel();
                            //$scope.GridRefresh();
                        }
                        else {
                            //Notify(data.Massage, 'bottom-right', '5000', 'danger', 'fa-bolt', true);
                        }
                    }).error(function (data, status, headers, config) { alert(); });
                }
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

    //--------------------- Status Child -------------------------------
    $scope.UpdatedStatusChild = [];
    $scope.StatusChild = function () {
        $scope.LoadCM00700Page('StatusChild')
        $scope.Clear();
        $http.get('/CMS_CaseStatus/StatusHasChildDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.CaseStatusesHasChild = data;
        }).finally(function () {
            $scope.CaseStatusHasChildOptions = {
                filter: "startswith",
                model: "SelectedStatus",
                optionLabel: "-- Select Status --",
                dataTextField: "CaseStatusname",
                dataValueField: "CaseStatusID",
                dataSource: $scope.CaseStatusesHasChild,
                value: $scope.CM00700.CaseStatusID
            };
        });
    };
    $scope.ChildStatusChanged = function () {

        $scope.GridRefresh('GridCMS_CaseStatusChild');
        $http.get('/CMS_CaseStatus/GetCaseCHilds?KaizenPublicKey=' + sessionStorage.PublicKey
            + "&caseStatusID=" + $scope.CM00700.SelectedStatus.CaseStatusID
            ).success(function (data) {
                $scope.CaseShild = data;
            }).finally(function () {
                var grid = $('#GridCMS_CaseStatusChild').data().kendoGrid;
                var DataSourceData = grid.dataSource.data();
                DataSourceData.forEach(function (element, index) {

                    $scope.CaseShild.forEach(function (cHildelement, index) {
                        if (element.CaseStatusID == cHildelement.CaseStatusID) {
                            element.IsSelected = true;
                        }
                    });
                });
            });

    };
    $scope.UpdateStatusChild = function () {
        $scope.UpdatedStatusChild = [];
        $scope.DeletedCHildStatus = [];
        var grid = $('#GridCMS_CaseStatusChild').data().kendoGrid;
        var DataSourceData = grid.dataSource.data();
        DataSourceData.forEach(function (element, index) {
            var isIncluded = false;
            $scope.CaseShild.forEach(function (cHildelement, index) {
                if (element.CaseStatusID == cHildelement.CaseStatusID) {
                    if (!element.IsSelected) {
                        $scope.DeletedCHildStatus.push(element);
                    } else {
                        isIncluded = true;
                    }
                }
            });

            if (!isIncluded) {
                if (element.IsSelected) {
                    $scope.UpdatedStatusChild.push(element);
                }
            }
        });
        if ($scope.UpdatedStatusChild.length > 0) {
            $http({
                url: '/CMS_CaseStatus/UpdateChild?KaizenPublicKey='
                    + sessionStorage.PublicKey,
                method: "POST",
                data: {
                    'CM00700': $scope.UpdatedStatusChild,
                    'CaseStatusID': $scope.CM00700.SelectedStatus.CaseStatusID
                },
                dataType: "json",
                headers: {
                    'Content-Type': 'application/json; charset=utf-8'
                }
            }).success(function (data) {
                if (data.Status == true) {
                    //Notify(data.Massage, 'bottom-right', '5000', 'success', 'fa-check', true);
                    $scope.Cancel();
                    $scope.GridRefresh();
                }
                else {
                    //Notify(data.Massage, 'bottom-right', '5000', 'danger', 'fa-bolt', true);
                }
            }).error(function (data, status, headers, config) { alert(); });
        }

        if ($scope.DeletedCHildStatus.length > 0) {
            $http({
                url: '/CMS_CaseStatus/DeleteChild?KaizenPublicKey='
                    + sessionStorage.PublicKey,
                method: "POST",
                data: {
                    'CM00700': $scope.DeletedCHildStatus,
                    'CaseStatusID': $scope.CM00700.SelectedStatus.CaseStatusID
                },
                dataType: "json",
                headers: {
                    'Content-Type': 'application/json; charset=utf-8'
                }
            }).success(function (data) {
                if (data.Status == true) {
                    //Notify(data.Massage, 'bottom-right', '5000', 'success', 'fa-check', true);
                    $scope.Cancel();
                    $scope.GridRefresh();
                }
                else {
                    //Notify(data.Massage, 'bottom-right', '5000', 'danger', 'fa-bolt', true);
                }
            }).error(function (data, status, headers, config) { alert(); });
        }
    };

    //--------------------- Status Role -------------------------------
    $scope.StatusRole = function () {
        $scope.LoadCM00700Page('StatusRole');
        $scope.Clear();

        $http.get('/CMS_CaseStatus/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.CaseStatusesHasTasks = data;
        }).finally(function () {
            $scope.CaseStatusHasTasksOptions = {
                filter: "startswith",
                model: "SelectedStatus",
                optionLabel: "-- Select Status --",
                dataTextField: "CaseStatusname",
                dataValueField: "CaseStatusID",
                dataSource: $scope.CaseStatusesHasTasks,
                value: $scope.CM00700.CaseStatusID
            };
        });

        if ($scope.Roles == null || $scope.Roles.length == 0) {
            $http.get('/Sys_role/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
                $scope.Roles = data;
            }).finally(function () { });
        }
    };

    $scope.StatusChangedForRoles = function () {
        if (angular.isDefined($scope.CM00700.SelectedStatus)) {
            $scope.CM00700.CaseStatusID = $scope.CM00700.SelectedStatus.CaseStatusID;
            $scope.LoadRoles_CaseStatus($scope.CM00700.CaseStatusID);
        }
    };

    $scope.LoadRoles_CaseStatus = function (CaseStatusID) {
        $http.get('/CMS_CaseStatus/GetAllLookup02?KaizenPublicKey=' + sessionStorage.PublicKey,
            { params: { CaseStatusID: CaseStatusID } }).success(function (data) {
                if (data.length >= 0) {
                    if (data.length > 0)
                        $scope.StatusRoles = data;
                    else
                        $scope.StatusRoles = [];

                    $scope.Roles.forEach(function (eleRole, index) {
                        eleRole.isSelected = false;
                        $scope.StatusRoles.forEach(function (eleStatusRole, index) {
                            if (eleRole.RoleID === eleStatusRole.RoleID) {
                                eleRole.isSelected = true;
                            }
                        });
                    });
                }
            });
    };

    $scope.UpdateStatusRole = function (role) {

        if (role) {
            $scope.CM00052 = {};
            $scope.CM00052.RoleID = role.RoleID;
            $scope.CM00052.CaseStatusID = $scope.CM00700.CaseStatusID;

            var urlToUpdate = "";

            if (role.isSelected) {
                urlToUpdate = "/CMS_CaseStatus/SaveRole?KaizenPublicKey=" + sessionStorage.PublicKey;
            } else {
                urlToUpdate = "/CMS_CaseStatus/DeleteRole?KaizenPublicKey=" + sessionStorage.PublicKey;
            }

            $http({
                url: urlToUpdate,
                method: "POST",
                data: JSON.stringify($scope.CM00052),
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
                    content: "Unable to save role for the selected status",
                    color: "#C46A69",
                    timeout: 8000,
                    icon: "fa fa-bolt swing animated"
                });
            });
        }
    }

    //--------------------- Status User -------------------------------
    $scope.CM00051 = {};
    $scope.StatusUsers = [];
    $scope.StatusUser = function () {
        $scope.LoadCM00700Page('StatusUser');
        $scope.Clear();

        $http.get('/CMS_CaseStatus/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.CaseStatusList = data;
        }).finally(function () {
            //$scope.CaseStatusHasTasksOptions = {
            //    filter: "startswith",
            //    model: "SelectedStatus",
            //    optionLabel: "-- Select Status --",
            //    dataTextField: "CaseStatusname",
            //    dataValueField: "CaseStatusID",
            //    dataSource: $scope.CaseStatusesHasTasks,
            //    value: $scope.CM00700.CaseStatusID
            //};
        });
    };

    $scope.UserBack = function (user) {
        if (user != null) {
            switch ($scope.CurrentControl) {
                case 'StatusUsers':
                    $scope.StatusUsers = [];
                    $scope.CM00051.UserName = user.UserName;
                    $http.get('/CMS_CaseStatus/GetStatusByUser?KaizenPublicKey=' + sessionStorage.PublicKey,
                        { params: { userName: user.UserName } }).success(function (data) {
                        if (data.length >= 0) {
                            $scope.StatusUsers = data;

                            $scope.CaseStatusList.forEach(function (ele, index) {
                                ele.isSelected = false;
                                $scope.StatusUsers.forEach(function (eleselected, index) {
                                    if (ele.CaseStatusID === eleselected.CaseStatusID) {
                                        ele.isSelected = true;
                                    }
                                });
                            });
                        }
                    });
                    break;

                case 'CaseStatusViewUsers':
                    $scope.CaseStatusViewUsers = [];
                    $scope.CM00064.UserName = user.UserName;
                    $http.get('/CMS_CaseStatus/GetCaseStatusViewsByUser?KaizenPublicKey=' + sessionStorage.PublicKey,
                        { params: { userName: user.UserName } }).success(function (data) {
                            if (data.length >= 0) {
                                $scope.CaseStatusViewUsers = data;

                                $scope.CaseStatusViewsList.forEach(function (ele, index) {
                                    ele.isSelected = false;
                                    $scope.CaseStatusViewUsers.forEach(function (eleselected, index) {
                                        if (ele.ViewID === eleselected.ViewID) {
                                            ele.isSelected = true;
                                        }
                                    });
                                });
                            }
                        });
                    break;
            }
        }
    };

    $scope.StatusChangedForUsers = function () {
        if (angular.isDefined($scope.CM00700.SelectedStatus)) {
            $scope.CM00700.CaseStatusID = $scope.CM00700.SelectedStatus.CaseStatusID;
        }
    };

    $scope.UpdateStatusUser = function () {
        if (!$scope.CM00051.UserName) {
            $.bigBox({
                title: "Error",
                content: "Username not found",
                color: "#C46A69",
                timeout: 3000,
                icon: "fa fa-bolt swing animated"
            });
            return;
        }
        if (!$scope.CM00700.CaseStatusID) {
            $.bigBox({
                title: "Error",
                content: "Status not found",
                color: "#C46A69",
                timeout: 3000,
                icon: "fa fa-bolt swing animated"
            });
            return;
        }

        var statusUser = {};
        statusUser.UserName = $scope.CM00051.UserName;
        statusUser.CaseStatusID = $scope.CM00700.CaseStatusID;

        $scope.StatusUsers.push(statusUser);
        $scope.AddStatusByUser(statusUser);
    };

    $scope.AddStatusByUser = function (statusUser) {
        $http({
            url: '/CMS_CaseStatus/AddStatusByUser?KaizenPublicKey=' + sessionStorage.PublicKey,
            method: "POST",
            data: JSON.stringify(statusUser),
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

    $scope.RemoveStatusUser = function (statusUser, index) {

        $.SmartMessageBox({
            title: "Delete Record",
            content: "Are you sure?",
            buttons: '[No][Yes]'
        }, function (ButtonPressed) {
            if (ButtonPressed === "Yes") {
                if (statusUser.status == 1)
                    $scope.StatusUsers.splice(index, 1);
                else
                    statusUser.status = 3;
                
                $scope.DeleteStatusUser(statusUser);
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

    $scope.DeleteStatusUser = function (statusUser) {
        $http({
            url: '/CMS_CaseStatus/DeleteStatusByUser?KaizenPublicKey=' + sessionStorage.PublicKey,
            method: "POST",
            data: JSON.stringify(statusUser),
            dataType: "json",
            headers: {
                'Content-Type': 'application/json; charset=utf-8'
            }
        }).success(function (data) {
            if (data.Status == true) {
                $.bigBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#C46A69",
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
            }
        }).error(function (data, status, headers, config) { alert(); });
    };

    $scope.UpdateCaseStatusUser = function (caseStatus) {
        if (!$scope.CM00051 || !$scope.CM00051.UserName) {
            $.bigBox({
                title: "Error",
                content: "Please select user name",
                color: "#C46A69",
                timeout: 8000,
                icon: "fa fa-bolt swing animated"
            });
            caseStatus.isSelected = !caseStatus.isSelected;
            return;
        }
        if (caseStatus) {
            var caseStatusUser = {};
            caseStatusUser.UserName = $scope.CM00051.UserName;
            caseStatusUser.CaseStatusID = caseStatus.CaseStatusID;

            if (caseStatus.isSelected) {
                $scope.AddStatusByUser(caseStatusUser);
            } else {
                $scope.DeleteStatusUser(caseStatusUser);
            }
        }
    };

    //------------------- Load Case Status Common -------------------------
    $scope.LoadCaseStatus = function() {
        $http.get('/CMS_CaseStatus/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.CaseStatuses = data;
        }).finally(function () {
            $scope.CaseStatusHasOptions = {
                filter: "startswith",
                model: "CM00700.SelectedStatus",
                optionLabel: "-- Select Status --",
                dataTextField: "CaseStatusname",
                dataValueField: "CaseStatusID",
                dataSource: $scope.CaseStatuses,
                value: $scope.CM00700.CaseStatusID
            };
        });
    };

    $scope.GetFieldCodesByCaseStatus = function (CaseStatusID) {
        $http.get('/CMS_CaseStatus/GetFieldCodesByCaseStatus?KaizenPublicKey=' + sessionStorage.PublicKey,
            { params: { CaseStatusID: CaseStatusID } }).success(function (data) {
            if (data.length <= 0)
                $scope.CaseStatusFieldList = [];
            else
                $scope.CaseStatusFieldList = data;
        }).finally(function () {
            $scope.FieldCodeHasOptions = {
                filter: "startswith",
                model: "CM00060.SelectedField",
                optionLabel: "-- Select Field --",
                dataTextField: "FieldCode",
                dataValueField: "FieldTypeID",
                dataSource: $scope.CaseStatusFieldList,
                value: $scope.CM00060.CaseStatusID
            };
        });
    };
    
    //-------------------- Status Field -------------------------------
    $scope.CM00060 = {};
    $scope.CM00028 = {};
    $scope.StatusFields = function() {
        $scope.LoadCM00700Page('StatusFields');
        $scope.Clear();
        $scope.CM00060.status = 0;
        $scope.LoadCaseStatus();
        $scope.LoadFieldTypes();
    };

    $scope.StatusChangedForFields = function () {
        if (angular.isDefined($scope.CM00700.SelectedStatus)) {
            $scope.CM00700.CaseStatusID = $scope.CM00700.SelectedStatus.CaseStatusID;
            $scope.CM00060.CaseStatusID = $scope.CM00700.SelectedStatus.CaseStatusID;
            $scope.LoadFields_CaseStatus($scope.CM00700.CaseStatusID);
        }
    };

    $scope.FieldTypeChangedForCaseStatus = function () {
        if (angular.isDefined($scope.CM00060.SelectedField)) {
            $scope.CM00061.FieldCode = $scope.CM00060.SelectedField.FieldCode;
            $scope.CM00060.FieldCode = $scope.CM00060.SelectedField.FieldCode;
            $scope.LoadLookups($scope.CM00024.CaseStatusID,$scope.CM00060.FieldCode);
        }
    };

    $scope.LoadLookups = function (CaseStatusID,Fieldcode) {
        $http.get('/CMS_CaseStatus/GetLookups?KaizenPublicKey=' + sessionStorage.PublicKey,
            { params: { CaseStatusID: CaseStatusID, fieldCode: Fieldcode } }).success(function (data) {
                if (data.length >= 0) {
                    if (data.length > 0)
                        $scope.LookupList = data;
                    else
                        $scope.LookupList = [];
                }
            });
    };

    $scope.LoadFieldTypes = function () {
        $http.get('/CMS_CaseType/FillFieldTypeList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.FieldTypeList = data;
        }).finally(function () {
            $scope.FieldTypeHasOptions = {
                filter: "startswith",
                model: "CM00028.SelectedFieldType",
                optionLabel: "-- Select Field Type --",
                dataTextField: "FieldTypeName",
                dataValueField: "FieldTypeID",
                dataSource: $scope.FieldTypeList,
                value: $scope.CM00028.FieldTypeID
            };
        });
    }

    $scope.LoadFields_CaseStatus = function (CaseStatusID) {
        $http.get('/CMS_CaseStatus/GetFieldsByCaseStatus?KaizenPublicKey=' + sessionStorage.PublicKey,
            { params: { CaseStatusID: CaseStatusID } }).success(function (data) {
                if (data.length >= 0) {
                    if (data.length > 0)
                        $scope.CaseStatusFieldsList = data;
                    else
                        $scope.CaseStatusFieldsList = [];
                }
            });
    };

    $scope.AddNewField = function () {
        $scope.CM00060.status = 1;
        
        if ($scope.CM00060) {
            $http({
                url: '/CMS_CaseStatus/SaveCaseStatusField?KaizenPublicKey=' + sessionStorage.PublicKey,
                method: "POST",
                data: JSON.stringify($scope.CM00060),
                dataType: "json",
                headers: {
                    'Content-Type': 'application/json; charset=utf-8'
                }
            }).success(function (data) {
                if (data.Status == true) {
                    $scope.CaseStatusFieldsList.push($scope.CM00060);
                    $scope.CM00060 = {};
                    $scope.CM00060.status = 0;

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
                    content: "Unable to save case status field data",
                    color: "#C46A69",
                    timeout: 8000,
                    icon: "fa fa-bolt swing animated"
                });
            });
        }
    };

    $scope.EditField = function (caseStatusField) {
        indx = $scope.CaseStatusFieldsList.indexOf(caseStatusField);
        $scope.CM00060 = caseStatusField;
        $scope.CM00028.SelectedFieldType = $scope.CM00060;
        var Obj_indx = $scope.functiontofindIndexByKeyValue($scope.CaseStatusFieldsList, "CaseStatusID", $scope.CM00060.CaseStatusID);
        var obj = $scope.CaseStatusFieldsList[Obj_indx];
        //if ($scope.CM00070.status == 0 || angular.isUndefined($scope.CM00070.status)) {
        $scope.CM00060.status = 2;
        //}
    };

    $scope.UpdateField = function () {
        if ($scope.CM00060.status == 0 || angular.isUndefined($scope.CM00060.status)) {
            $scope.CM00060.status = 2;
        }
        $scope.CaseStatusFieldsList.splice(indx, 1, $scope.CM00060);

        if ($scope.CM00060) {
            //if ($scope.CM00028.SelectedFieldType && $scope.CM00028.SelectedFieldType.FieldTypeID)
            //    $scope.CM00060.FieldTypeID = $scope.CM00028.SelectedFieldType.FieldTypeID;

            $http({
                url: '/CMS_CaseStatus/UpdateField?KaizenPublicKey=' + sessionStorage.PublicKey,
                method: "POST",
                data: $scope.CM00060,
                dataType: "json",
                headers: {
                    'Content-Type': 'application/json; charset=utf-8'
                }
            }).success(function (data) {
                if (data.Status == true) {
                    $scope.CM00060 = {};
                    $scope.CM00060.status = 0;

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
                    content: "Unable to update case status field data",
                    color: "#C46A69",
                    timeout: 8000,
                    icon: "fa fa-bolt swing animated"
                });
            });
        }
    };

    $scope.RemoveCaseStatusField = function (caseStatusField, index) {

        $.SmartMessageBox({
            title: "Delete Record",
            content: "Are you sure?",
            buttons: '[No][Yes]'
        }, function (ButtonPressed) {
            if (ButtonPressed === "Yes") {

                $http({
                    url: '/CMS_CaseStatus/DeleteField?KaizenPublicKey=' + sessionStorage.PublicKey,
                    method: "POST",
                    data: JSON.stringify(caseStatusField),
                    dataType: "json",
                    headers: {
                        'Content-Type': 'application/json; charset=utf-8'
                    }
                }).success(function (data) {
                    if (data.Status == true) {
                        if (caseStatusField.status == 1)
                            $scope.CaseStatusFieldsList.splice(index, 1);
                        else
                            caseStatusField.status = 3;
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
                }).error(function (data, status, headers, config) {
                    $.bigBox({
                        title: "Error",
                        content: "Unable to delete case status field",
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

    //--------------------- Status Views -----------------------------
    $scope.CM00062 = {};
    $scope.StatusViews = function () {
        $scope.LoadCM00700Page('StatusViews');
        $scope.Clear();
        $scope.CM00062 = {};
        $scope.CM00062.status = 0;
        $scope.LoadCaseStatus();
    };
    $scope.StatusChangedForViews = function () {
        if (angular.isDefined($scope.CM00700.SelectedStatus)) {
            $scope.CM00700.CaseStatusID = $scope.CM00700.SelectedStatus.CaseStatusID;
            $scope.CM00062.CaseStatusID = $scope.CM00700.SelectedStatus.CaseStatusID;
            $scope.LoadViews_CaseStatus($scope.CM00062.CaseStatusID);
        }
    };
    $scope.LoadViews_CaseStatus = function (CaseStatusID) {
        $http.get('/CMS_CaseStatus/GetViewsByCaseStatus?KaizenPublicKey=' + sessionStorage.PublicKey,
            { params: { CaseStatusID: CaseStatusID } }).success(function (data) {
                if (data.length >= 0) {
                    if (data.length > 0)
                        $scope.CaseStatusViewsList = data;
                    else
                        $scope.CaseStatusViewsList = [];
                }
            });
    };

    $scope.EditView = function (caseStatusView) {
        indx = $scope.CaseStatusViewsList.indexOf(caseStatusView);
        $scope.CM00062 = caseStatusView;
        //var Obj_indx = $scope.functiontofindIndexByKeyValue($scope.CaseStatusViewsList, "ViewID", $scope.CM00062.ViewID);
        //var obj = $scope.CaseTypeViewsList[Obj_indx];
        //$scope.CM00029.SelectedCaseType = obj;
        //if ($scope.CM00070.status == 0 || angular.isUndefined($scope.CM00070.status)) {
        $scope.CM00062.status = 2;
        //}
    };

    $scope.UpdateView = function () {
        if ($scope.CM00062.status == 0 || angular.isUndefined($scope.CM00062.status)) {
            $scope.CM00062.status = 2;
        }
        $scope.CaseStatusViewsList.splice(indx, 1, $scope.CM00062);

        if ($scope.CM00062) {
            $http({
                url: '/CMS_CaseStatus/UpdateView?KaizenPublicKey=' + sessionStorage.PublicKey,
                method: "POST",
                data: JSON.stringify($scope.CM00062),
                dataType: "json",
                headers: {
                    'Content-Type': 'application/json; charset=utf-8'
                }
            }).success(function (data) {
                if (data.Status == true) {
                    $scope.CM00062 = {};
                    $scope.CM00062.status = 0;

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
            }).error(function (data, status, headers, config) { alert(); });
        }
    };

    $scope.RemoveCaseStatusView = function (caseStatusView, index) {

        $.SmartMessageBox({
            title: "Delete Record",
            content: "Are you sure?",
            buttons: '[No][Yes]'
        }, function (ButtonPressed) {
            if (ButtonPressed === "Yes") {

                $http({
                    url: '/CMS_CaseStatus/DeleteView?KaizenPublicKey=' + sessionStorage.PublicKey,
                    method: "POST",
                    data: JSON.stringify(caseStatusView),
                    dataType: "json",
                    headers: {
                        'Content-Type': 'application/json; charset=utf-8'
                    }
                }).success(function (data) {
                    if (data.Status == true) {
                        if (caseStatusView.status == 1)
                            $scope.CaseStatusViewsList.splice(index, 1);
                        else
                            caseStatusView.status = 3;
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

    $scope.AddNewView = function () {
        $scope.CM00062.status = 1;
        $scope.CM00062.CaseStatusID = $scope.CM00700.CaseStatusID;

        if ($scope.CM00062) {
            $http({
                url: '/CMS_CaseStatus/SaveCaseStatusView?KaizenPublicKey=' + sessionStorage.PublicKey,
                method: "POST",
                data: JSON.stringify($scope.CM00062),
                dataType: "json",
                headers: {
                    'Content-Type': 'application/json; charset=utf-8'
                }
            }).success(function (data) {
                if (data.Status == true) {
                    $scope.CaseStatusViewsList.push($scope.CM00062);
                    $scope.CM00062 = {};
                    $scope.CM00062.status = 0;

                    $.bigBox({
                        title: data.Massage,
                        content: data.Description,
                        color: "#739E73",
                        timeout: 8000,
                        icon: "fa fa-bolt swing animated"
                    });
                    $scope.LoadViews_CaseStatus($scope.CM00700.CaseStatusID);
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
                    content: "Unable to save case status view data",
                    color: "#C46A69",
                    timeout: 8000,
                    icon: "fa fa-bolt swing animated"
                });
            });
        }
    };

    //-------------------------- Status View Roles ----------------------
    $scope.CM00065 = {};
    $scope.StausViewsRoles = function() {
        $scope.LoadCM00700Page('StatusViewsRoles');
        $scope.Clear();
        $scope.LoadCaseStatusViews();
        $scope.CM00065.status = 0;

        if ($scope.Roles == null || $scope.Roles.length == 0) {
            $http.get('/Sys_role/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function(data) {
                $scope.Roles = data;
            }).finally(function() {});
        }
    };

    $scope.LoadCaseStatusViews = function() {
        $scope.CaseStatusViewsList = [];
        $http.get('/CMS_CaseStatus/GetViews?KaizenPublicKey=' + sessionStorage.PublicKey).success(function(data) {
            $scope.CaseStatusViewsList = data;
        }).finally(function() {
            $scope.ViewHasOptions = {
                filter: "startswith",
                model: "CM00062.SelectedView",
                optionLabel: "-- Select View --",
                dataTextField: "ViewName",
                dataValueField: "ViewID",
                dataSource: $scope.CaseStatusViewsList,
                value: $scope.CM00062.ViewID
            };
        });
    };
    $scope.ViewChangedForRoles = function () {
        if (angular.isDefined($scope.CM00062.SelectedView)) {
            $scope.CM00062.ViewID = $scope.CM00062.SelectedView.ViewID;
            $scope.LoadRoles_ByViews($scope.CM00062.ViewID);
        }
    };
    $scope.LoadRoles_ByViews = function (ViewID) {
        $http.get('/CMS_CaseStatus/GetRolesByView?KaizenPublicKey=' + sessionStorage.PublicKey,
            { params: { ViewID: ViewID } }).success(function (data) {
                if (data.length >= 0) {
                    if (data.length > 0)
                        $scope.CaseStatusViewRoles = data;
                    else
                        $scope.CaseStatusViewRoles = [];

                    $scope.Roles.forEach(function (eleRole, index) {
                        eleRole.isSelected = false;
                        $scope.CaseStatusViewRoles.forEach(function (eleViewRole, index) {
                            if (eleRole.RoleID === eleViewRole.RoleID) {
                                eleRole.isSelected = true;
                            }
                        });
                    });
                }
            });
    };
    $scope.UpdateViewRole = function(role) {
        if (!$scope.CM00062 || !$scope.CM00062.ViewID) {
            $.bigBox({
                title: "Error",
                content: "Please select view",
                color: "#C46A69",
                timeout: 8000,
                icon: "fa fa-bolt swing animated"
            });
            role.isSelected = !role.isSelected;
            return;
        }
        if (role) {
            $scope.CM00065 = {};
            $scope.CM00065.RoleID = role.RoleID;
            $scope.CM00065.ViewID = $scope.CM00062.ViewID;

            var urlToUpdate = "";

            if (role.isSelected) {
                urlToUpdate = "/CMS_CaseStatus/SaveViewRole?KaizenPublicKey=" + sessionStorage.PublicKey;
            } else {
                urlToUpdate = "/CMS_CaseStatus/DeleteViewRole?KaizenPublicKey=" + sessionStorage.PublicKey;
            }

            $http({
                url: urlToUpdate,
                method: "POST",
                data: JSON.stringify($scope.CM00065),
                dataType: "json",
                headers: {
                    'Content-Type': 'application/json; charset=utf-8'
                }
            }).success(function(data) {
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
            }).error(function(data, status, headers, config) {
                $.bigBox({
                    title: "Error",
                    content: "Unable to save role for the selected view",
                    color: "#C46A69",
                    timeout: 8000,
                    icon: "fa fa-bolt swing animated"
                });
            });
        }
    };

    //------------------------ Status View Users -----------------------
    $scope.CM00064 = {};
    $scope.StatusViewsUsers = function () {
        $scope.LoadCM00700Page('StatusViewsUsers');
        $scope.Clear();
        $scope.LoadCaseStatusViews();
        $scope.CM00064.status = 0;
    }

    $scope.UpdateCaseStatusViewUser = function (caseStatusView) {
        if (!$scope.CM00064 || !$scope.CM00064.UserName) {
            $.bigBox({
                title: "Error",
                content: "Please select user name",
                color: "#C46A69",
                timeout: 8000,
                icon: "fa fa-bolt swing animated"
            });
            caseStatusView.isSelected = !caseStatusView.isSelected;
            return;
        }
        if (caseStatusView) {
            var caseStatusViewUser = {};
            caseStatusViewUser.UserName = $scope.CM00064.UserName;
            caseStatusViewUser.ViewID = caseStatusView.ViewID;
            
            if (caseStatusView.isSelected) {
                $scope.AddCaseStatusViewByUser(caseStatusViewUser);
            } else {
                $scope.DeleteCaseStatusViewUser(caseStatusViewUser);
            }
        }
    };

    $scope.AddCaseStatusViewByUser = function (caseStatusViewUser) {
        $http({
            url: '/CMS_CaseStatus/AddCaseStatusViewByUser?KaizenPublicKey=' + sessionStorage.PublicKey,
            method: "POST",
            data: JSON.stringify(caseStatusViewUser),
            dataType: "json",
            headers: {
                'Content-Type': 'application/json; charset=utf-8'
            }
        }).success(function (data) {
            if (data.Status == true) {
                //$scope.CM00064 = {};
                $scope.CM00064.status = 0;
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
                content: "Unable to save case status view user",
                color: "#C46A69",
                timeout: 8000,
                icon: "fa fa-bolt swing animated"
            });
        });
    }
    
    $scope.DeleteCaseStatusViewUser = function (caseStatusViewUser) {
        $http({
            url: '/CMS_CaseStatus/DeleteCaseStatusViewUser?KaizenPublicKey=' + sessionStorage.PublicKey,
            method: "POST",
            data: JSON.stringify(caseStatusViewUser),
            dataType: "json",
            headers: {
                'Content-Type': 'application/json; charset=utf-8'
            }
        }).success(function (data) {
            if (data.Status == true) {
                $.bigBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#C46A69",
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
            }
        }).error(function (data, status, headers, config) {
            $.bigBox({
                title: "Error",
                content: "Unable to delete case status view user",
                color: "#C46A69",
                timeout: 8000,
                icon: "fa fa-bolt swing animated"
            });
        });
    };

    //---------------------- Sattus Views Fields
    $scope.CM00063 = {};
    $scope.StatusViewsFields = function () {
        $scope.LoadCM00700Page('StatusViewsFields');
        $scope.Clear();
        $scope.LoadCaseStatus();
        $scope.LoadCaseStatusViews();
        $scope.CM00063.status = 0;
    }
    $scope.ViewChangedForFields = function () {
        if (angular.isDefined($scope.CM00062.SelectedView)) {
            $scope.CM00063.ViewID = $scope.CM00062.SelectedView.ViewID;
            $scope.LoadAllFields_ByView($scope.CM00063.ViewID);
        }
    };
    $scope.LoadAllFields_ByView = function (ViewID) {
        $http.get('/CMS_CaseStatus/GetAllFieldsByView?KaizenPublicKey=' + sessionStorage.PublicKey,
            { params: { ViewID: ViewID } }).success(function (data) {
                if (data.length >= 0) {
                    if (data.length > 0) {
                        $scope.FieldsListByView = data;

                    } else {
                        $scope.FieldsListByView = [];
                    }
                    $scope.CaseStatusViewsFieldsList.forEach(function (ele, index) {
                        ele.isSelected = false;
                        $scope.FieldsListByView.forEach(function (eleViewField, index) {
                            ele.ViewID = eleViewField.ViewID;
                            ele.FieldOrder = 0;

                            if (ele.TRXTypeID === eleViewField.TRXTypeID &&
                                ele.FieldCode === eleViewField.FieldCode) {
                                ele.FieldOrder = eleViewField.FieldOrder;
                                ele.isSelected = true;
                            } 
                        });
                    });
                }
            });
    };

    $scope.StatusChangedForViewsFields = function () {
        if (angular.isDefined($scope.CM00700.SelectedStatus)) {
            $scope.CM00700.CaseStatusID = $scope.CM00700.SelectedStatus.CaseStatusID;
            $scope.CM00063.CaseStatusID = $scope.CM00700.SelectedStatus.CaseStatusID;
            $scope.LoadAllFields_CaseStatus($scope.CM00700.CaseStatusID);
        }
    };

    $scope.LoadAllFields_CaseStatus = function (CaseStatusID) {
        $http.get('/CMS_CaseStatus/GetAllFieldsByCaseStatus?KaizenPublicKey=' + sessionStorage.PublicKey,
            { params: { CaseStatusID: CaseStatusID } }).success(function (data) {
                if (data.length >= 0) {
                    if (data.length > 0)
                        $scope.CaseStatusViewsFieldsList = data;
                    else
                        $scope.CaseStatusViewsFieldsList = [];
                }
            });
    };

    $scope.UpdateCaseStatusViewsFields = function (viewsFieldsObj) {

        if (viewsFieldsObj) {
            if (!$scope.CM00063 || !$scope.CM00063.ViewID) {
                $.bigBox({
                    title: "Error",
                    content: "Please select view",
                    color: "#C46A69",
                    timeout: 8000,
                    icon: "fa fa-bolt swing animated"
                });
                viewsFieldsObj.isSelected = !viewsFieldsObj.isSelected;
                return;
            }
            //$scope.CM00056 = {};
            //$scope.CM00056.RoleID = role.RoleID;
            viewsFieldsObj.ViewID = $scope.CM00063.ViewID;
            viewsFieldsObj.CaseStatusID = $scope.CM00063.CaseStatusID;

            $scope.CM00063 = viewsFieldsObj;

            var urlToUpdate = "";

            if (viewsFieldsObj.isSelected) {
                urlToUpdate = "/CMS_CaseStatus/SaveViewsFields?KaizenPublicKey=" + sessionStorage.PublicKey;
            } else {
                urlToUpdate = "/CMS_CaseStatus/DeleteViewsFields?KaizenPublicKey=" + sessionStorage.PublicKey;
            }

            $http({
                url: urlToUpdate,
                method: "POST",
                data: JSON.stringify($scope.CM00063),
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
                    content: "Unable to save views fields for the selected case status",
                    color: "#C46A69",
                    timeout: 8000,
                    icon: "fa fa-bolt swing animated"
                });
            });
        }
    }
    $scope.UpdateViewsFields = function () {
        //var selView = $scope.CM00062.SelectedView;
        if ($scope.CM00062 && $scope.CM00062.SelectedView) {
            var selectedViewsFields = [];
            $scope.CaseStatusViewsFieldsList.forEach(function (ele, index) {
                if (ele.isSelected === true) {
                    ele.ViewID = $scope.CM00062.SelectedView.ViewID;
                    selectedViewsFields.push(ele);
                }
            });
            if (selectedViewsFields.length > 0) {
                $http({
                    url: '/CMS_CaseStatus/UpdateViewsFields?KaizenPublicKey=' + sessionStorage.PublicKey,
                    method: "POST",
                    data: selectedViewsFields,
                    dataType: "json",
                    headers: {
                        'Content-Type': 'application/json; charset=utf-8'
                    }
                }).success(function (data) {
                    if (data.Status == true) {
                        //$scope.CM00073 = {};
                        
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
                        content: "Unable to update case status views fields",
                        color: "#C46A69",
                        timeout: 8000,
                        icon: "fa fa-bolt swing animated"
                    });
                });
            }
        } else {
            $.bigBox({
                title: "Info",
                content: "Please select view",
                color: "#C46A69",
                timeout: 8000,
                icon: "fa fa-bolt swing animated"
            });
        }
    };

    //---------------------- Status Lookup
    $scope.CM00061 = {};
    $scope.StatusLookup = function() {
        $scope.LoadCM00700Page('StatusLookup');
        $scope.Clear();
        $scope.LoadCaseStatus();
        //$scope.LoadCaseStatusViews();
        $scope.CM00061.status = 0;
    };
    $scope.StatusChangedForLookups = function () {
        if (angular.isDefined($scope.CM00700.SelectedStatus)) {
            $scope.CM00700.CaseStatusID = $scope.CM00700.SelectedStatus.CaseStatusID;
            $scope.CM00061.CaseStatusID = $scope.CM00700.SelectedStatus.CaseStatusID;
            $scope.GetFieldCodesByCaseStatus($scope.CM00700.CaseStatusID);
        }
    };
    $scope.FieldChangedForLookups = function () {
        if (angular.isDefined($scope.CM00700.SelectedStatus)) {
            $scope.CM00700.CaseStatusID = $scope.CM00700.SelectedStatus.CaseStatusID;
            $scope.CM00061.CaseStatusID = $scope.CM00700.SelectedStatus.CaseStatusID;
        }
    };

    $scope.AddNewLookup = function () {
        $scope.CM00061.status = 1;

        if ($scope.CM00061) {
            $http({
                url: '/CMS_CaseStatus/AddCM00061?KaizenPublicKey=' + sessionStorage.PublicKey,
                method: "POST",
                data: JSON.stringify($scope.CM00061),
                dataType: "json",
                headers: {
                    'Content-Type': 'application/json; charset=utf-8'
                }
            }).success(function (data) {
                if (data.Status == true) {
                    $scope.LookupList.push($scope.CM00061);
                    $scope.CM00061 = {};
                    $scope.CM00061.status = 0;

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
                    content: "Unable to save lookup data",
                    color: "#C46A69",
                    timeout: 8000,
                    icon: "fa fa-bolt swing animated"
                });
            });
        }
    };

    $scope.EditLookup = function (lookup) {
        indx = $scope.LookupList.indexOf(lookup);
        $scope.CM00061 = lookup;
        var Obj_indx = $scope.functiontofindIndexByKeyValue($scope.LookupList, "FieldCode", $scope.CM00061.FieldCode);
        var obj = $scope.LookupList[Obj_indx];
        //if ($scope.CM00070.status == 0 || angular.isUndefined($scope.CM00070.status)) {
        $scope.CM00061.status = 2;
        //}
    };

    $scope.UpdateLookup = function () {
        if ($scope.CM00061.status == 0 || angular.isUndefined($scope.CM00061.status)) {
            $scope.CM00061.status = 2;
        }
        $scope.LookupList.splice(indx, 1, $scope.CM00061);

        if ($scope.CM00061) {
            //if ($scope.CM00028.SelectedFieldType && $scope.CM00028.SelectedFieldType.FieldTypeID)
            //    $scope.CM00061.FieldTypeID = $scope.CM00028.SelectedFieldType.FieldTypeID;

            $http({
                url: '/CMS_CaseStatus/UpdateCM00061?KaizenPublicKey=' + sessionStorage.PublicKey,
                method: "POST",
                data: $scope.CM00061,
                dataType: "json",
                headers: {
                    'Content-Type': 'application/json; charset=utf-8'
                }
            }).success(function (data) {
                if (data.Status == true) {
                    $scope.CM00061 = {};
                    $scope.CM00061.status = 0;

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
                    content: "Unable to update lookup data",
                    color: "#C46A69",
                    timeout: 8000,
                    icon: "fa fa-bolt swing animated"
                });
            });
        }
    };

    $scope.RemoveLookup = function (lookup, index) {

        $.SmartMessageBox({
            title: "Delete Record",
            content: "Are you sure?",
            buttons: '[No][Yes]'
        }, function (ButtonPressed) {
            if (ButtonPressed === "Yes") {

                $http({
                    url: '/CMS_CaseStatus/DeleteCM00061?KaizenPublicKey=' + sessionStorage.PublicKey,
                    method: "POST",
                    data: JSON.stringify(lookup),
                    dataType: "json",
                    headers: {
                        'Content-Type': 'application/json; charset=utf-8'
                    }
                }).success(function (data) {
                    if (data.Status == true) {
                        if (lookup.status == 1)
                            $scope.LookupList.splice(index, 1);
                        else
                            lookup.status = 3;
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
                }).error(function (data, status, headers, config) {
                    $.bigBox({
                        title: "Error",
                        content: "Unable to delete lookup record",
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
});