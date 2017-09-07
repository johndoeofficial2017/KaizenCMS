app.controller('CO_EmailController', function ($scope, $http) {
    $scope.CM00030 = {};
    $scope.PagesCM00030 = [];
    $scope.SelectedLookUp = {};
    $scope.FieldItems = [],
    $scope.toolbarOptions = {
        items: [
                 {
                     type: "button", text: "<span class=\"fa fa-plus-circle\"></span> Add Eamil Message",
                     attributes: { "class": "btn-primary" },
                     click: function (e) {
                         $scope.$apply(function () {
                             $scope.AddCM00030();
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
    $scope.FullEditorTools = [
            "bold",
            "italic",
            "underline",
            "strikethrough",
            "justifyLeft",
            "justifyCenter",
            "justifyRight",
            "justifyFull",
            "insertUnorderedList",
            "insertOrderedList",
            "indent",
            "outdent",
            "createLink",
            "unlink",
            "insertImage",
            "insertFile",
            "subscript",
            "superscript",
            "createTable",
            "addRowAbove",
            "addRowBelow",
            "addColumnLeft",
            "addColumnRight",
            "deleteRow",
            "deleteColumn",
            "viewHtml",
            "cleanFormatting",
            "fontName",
            "fontSize",
            "foreColor", 
            "backColor",
            "print",
            {
                name: "insertHtml",
                items: $scope.FieldItems
            },
            "formatting",
    ];
    $scope.messages = { insertHtml: "Insert Marker" };
    $scope.stylesheets = [
                "../Content/editorStyles.css"
    ];

    $scope.LoadCM00030 = function (TemplateID) {
        $http.get('/CO_Email/GetSingle?KaizenPublicKey=' + sessionStorage.PublicKey + "&TemplateID=" + TemplateID).success(function (data) {
            $scope.CM00030 = data;
        }).finally(function () { $scope.CM00030.Status = 2; });
    };
    $scope.LoadCM00030Page = function (ActionName) {
        removeEntityPage($scope.PagesCM00030);
        var URIPath = "/CMS/CO_Email/" + ActionName + "?KaizenPublicKey=" + sessionStorage.PublicKey;
        var Page = { url: URIPath, ActionName: ActionName };
        $scope.PagesCM00030.push(Page);
    };
    $scope.AddCM00030 = function () {
        $scope.LoadCM00030Page('Create');
        $scope.Clear();
        $scope.CM00030.Status = 1;
        $scope.LoadCaseTypes();
    };
    $scope.EditCM00030 = function (TemplateID) {
        $scope.LoadCM00030Page('Create');
        $scope.Clear();
        $scope.LoadCM00030(TemplateID);
    };
    $scope.SaveData = function () {
        $http.post('/CO_Email/SaveData', { 'CM00030': $scope.CM00030, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
            if (data.Status == true) {
                $.bigBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#739E73",
                    timeout: 8000,
                    icon: "fa fa-check shake animated"
                });
                $scope.Cancel();
                $scope.GridRefresh('GridCMS_CM00030');
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
        $http.post('/CO_Email/UpdateData', { 'CM00030': $scope.CM00030, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
            if (data.Status == true) {
                $.bigBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#739E73",
                    timeout: 8000,
                    icon: "fa fa-check shake animated"
                });
                $scope.Cancel();
                $scope.GridRefresh('GridCMS_CM00030');
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
        $scope.CM00030 = {};
        $scope.SelectedLookUp = {};
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
                $http.post('/CO_Email/DeleteData', { 'CM00030': $scope.CM00030, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
                    if (data.Status == true) {
                        $.bigBox({
                            title: data.Massage,
                            content: data.Description,
                            color: "#739E73",
                            timeout: 8000,
                            icon: "fa fa-check shake animated"
                        });
                        $scope.Cancel();
                        $scope.GridRefresh('GridCMS_CM00030');
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
        $scope.PagesCM00030 = [];
    };

    $scope.LoadCaseTypes = function () {
        $http.get('/CMS_CaseType/FillDropDownList?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
            $scope.CaseTypes = data;
        }).finally(function () {

        });
    };
    $scope.CaseTypeChanged_Email = function () {
        if (angular.isDefined($scope.SelectedLookUp.SelectedType)) {
            $scope.CM00030.TRXTypeID = $scope.SelectedLookUp.SelectedType;
            $scope.GetCM00074_ByCaseType($scope.CM00030.TRXTypeID);
        }
    };
    $scope.GetCM00074_ByCaseType = function (TRXTypeID) {
        $http.get('/CMS_CaseType/GetAllFieldsByCaseType?KaizenPublicKey=' + sessionStorage.PublicKey,
        { params: { TRXTypeID: TRXTypeID } }).success(function (data) {
            $scope.FieldNameList = data;
            var arr = [];

            $scope.FieldNameList.forEach(function (ele, index) {
                arr.push({
                    text: ele.FieldDisplay.trim(), value: "<span class='marker'>[" + ele.FieldName.trim() + "]</span>&nbsp"
                });
            });

            $scope.FieldItems = arr;

            var ddl = $("select.k-insertHtml").getKendoSelectBox();
            ddl.dataSource.data($scope.FieldItems);

        }).finally(function () {
        });
    };
});