app.controller('Inq_TargetController', function ($scope, $http) {
    $scope.PagePath = [];
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
    $scope.GoTo = function (PagePath) {
        if (PagePath == null || PagePath == undefined || angular.isUndefined(PagePath))
            PagePath = $scope.ToolBar.ActiveScreen.CurrentWindow;
        $scope.PagePath.push(PagePath);
        $scope.ToolBar.ActiveScreen.CurrentWindow = PagePath;
        switch (PagePath) {
            case "MainGrid":
                $scope.LoadMainView();
                break;
        }
    }
    $scope.LoadPage = function (pageName) {
        $scope.MainURL = '';
        $scope.MainURL = "/CMS/CMS_Case/" + pageName + "?KaizenPublicKey=" + sessionStorage.PublicKey;
    };
});

