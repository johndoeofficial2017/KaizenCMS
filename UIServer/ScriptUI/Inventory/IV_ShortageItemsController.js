app.controller('IV_ShortageItemsController', function ($scope, $http) {
    $scope.Pageurl = "/IV_ShortageItems/MainReportView?&KaizenPublicKey=" + sessionStorage.PublicKey;
    $scope.Pages = [];
    $scope.Clear = function () {
    };
    $scope.Print = function () {
        alert('Printer not configured');
    };
    $scope.Cancel = function () {
        $scope.Pages = [];
        $scope.Clear();
    };
});
