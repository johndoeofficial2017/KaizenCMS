app.controller('Saved_PrinterController', function ($scope, $http) {
    $scope.Prn00100 = {};
    $scope.PagesPrn00100 = [];

    $scope.toolbarOptions = {
        items: [
                 
        ]
 , resizable: true
    };

    $scope.LoadPrn00100Page = function (ActionName) {
        removeEntityPage($scope.PagesPrn00100);
        var URIPath = "/Tools/" + $scope.ToolBar.ActiveScreen.ID + "/" + ActionName + "?KaizenPublicKey=" + sessionStorage.PublicKey;
        var Page = { url: URIPath, ActionName: ActionName };
        $scope.PagesPrn00100.push(Page);
    };

    $scope.UserBack = function (user) {
        if (user != null) {
            switch ($scope.CurrentControl) {
                
            }
        }
    };

    var indx;

    $scope.Cancel = function () {
        $scope.Clear();
        $scope.PagesPrn00100 = [];
    };

    $scope.Clear = function () {
        $scope.Prn00100 = {};
    };

});