app.controller('Sys_EncryptionController', function ($scope, $http) {
    $scope.Encryption = {};
    $scope.EncryptPassword = function () {
        $http.get('/SysUser/PasswordEncryption?KaizenPublicKey=' + sessionStorage.PublicKey, {
            params: {
                Password: $scope.Encryption.EncryptPassword
            }
        }).success(function (data) {
            $scope.Encryption.DecryptPassword = data;
        }).finally(function () { });
    };
    $scope.DecryptPassword = function () {
        $http.get('/SysUser/PasswordDecryption?KaizenPublicKey=' + sessionStorage.PublicKey, {
            params: {
                Password: $scope.Encryption.DecryptPassword
            }
        }).success(function (data) {
            $scope.Encryption.EncryptPassword = data;
        }).finally(function () { });
    };
    $scope.Cancel = function () {
        $scope.Encryption = {};
    };
});