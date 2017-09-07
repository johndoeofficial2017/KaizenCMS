var profile = angular.module("ProfileModule", []);

profile.controller('SysProfileController', function ($scope, $http) {
	$scope.Profile = {};
	$scope.LoadProfile = function (UserName) {
		if (angular.isUndefined($scope.Profile.UserName)) {
			$http.get('/Adm_City/GetSingle?KaizenPublicKey=' + sessionStorage.PublicKey + "&UserName=" + UserName).success(function (data) {
				$scope.Profile = data;
			}).finally(function () {
				$scope.Profile.Status = 2;
			});
		}
	};
	$scope.UpdateProfile = function () {
	    //alert('UpdateProfile');
	    $http.post('/SysUser/UserProfile', { 'model': $scope.Profile }).success(function (data) {
	        //alert(data.Status);
	        if (data.Status == true) {
	            $.bigBox({
	                title: data.Massage,
	                content: data.Description,
	                color: "#739E73",
	                timeout: 4000,
	                icon: "fa fa-check shake animated"
	            });
	        }
	        else {
	            alert(data.Status);
	            $.bigBox({
	                title: data.Massage,
	                content: data.Description,
	                color: "#C46A69",
	                timeout: 4000,
	                icon: "fa fa-bolt swing animated"
	            });
	        }
	    }).error(function (data, status, headers, config) {
	        alert(data.Status);
	        $.bigBox({
	            title: data.Massage,
	            content: data.Description,
	            color: "#C46A69",
	            timeout: 4000,
	            icon: "fa fa-bolt swing animated"
	        });
	    });
	};
});