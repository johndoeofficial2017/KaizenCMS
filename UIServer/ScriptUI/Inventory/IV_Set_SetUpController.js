app.controller('IV_Set_SetUpController', function ($scope, $http) {
    $scope.IV000014 = { Status: 1 };
    $scope.PagesIV000014 = [];
    $scope.LoadLookUp = function () {
        $http.get('/IV_Set_SetUp/GetSingle?KaizenPublicKey=' + sessionStorage.PublicKey + "&SetupID=" + 1).success(function (data) {
            if (data.SetupID == 1) {
                $scope.IV000014 = data;
                $scope.IV000014.Status = 2;
            }
            else {
                $scope.IV000014 = { Status: 1 };
            }
        }).finally(function () { });
    };
    $scope.LoadLookUp();

    $scope.SaveData = function () {
        $http.post('/IV_Set_SetUp/SaveData', { 'IV000014': $scope.IV000014, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
            if (data.Status == true) {
                $.bigBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#739E73",
                    timeout: 8000,
                    icon: "fa fa-check shake animated"
                });
                $scope.LoadLookUp();
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
        $http.post('/IV_Set_SetUp/UpdateData', { 'IV000014': $scope.IV000014, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
            if (data.Status == true) {
                $.bigBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#739E73",
                    timeout: 8000,
                    icon: "fa fa-check shake animated"
                });
                $scope.LoadLookUp();
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

    $scope.AccountBack = function (account) {
        if (account != null) {
            switch ($scope.CurrentControl) {
                case 'ItemMainPurchase':
                    $scope.IV000014.PurchaseID = account.AccountID;
                    $scope.IV000014.PurchaseName = account.AccountName;
                    $scope.IV000014.PurchaseNumber = account.ACTNUMBR;
                    break;
                case 'ItemMainPurchaseTradeDiscount':
                    $scope.IV000014.PurchaseTradDiscountID = account.AccountID;
                    $scope.IV000014.PurchaseTradDiscountName = account.AccountName;
                    $scope.IV000014.PurchaseTradDiscountNumber = account.ACTNUMBR;
                    break;
                case 'ItemMainPurchaseFreight':
                    $scope.IV000014.PurchaseFreightID = account.AccountID;
                    $scope.IV000014.PurchaseFreightName = account.AccountName;
                    $scope.IV000014.PurchaseFreightNumber = account.ACTNUMBR;
                    break;
                case 'ItemMainPurchaseTax':
                    $scope.IV000014.PurchaseTaxID = account.AccountID;
                    $scope.IV000014.PurchaseTaxName = account.AccountName;
                    $scope.IV000014.PurchaseTaxNumber = account.ACTNUMBR;
                    break;
                case 'ItemMainPurchaseMis':
                    $scope.IV000014.PurchaseMisID = account.AccountID;
                    $scope.IV000014.PurchaseMisName = account.AccountName;
                    $scope.IV000014.PurchaseMisNumber = account.ACTNUMBR;
                    break;
            }
        }
    };

});