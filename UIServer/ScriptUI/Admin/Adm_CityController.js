app.controller('Adm_CityController', function ($scope, $http) {
	$scope.Sys00014 = {};
	$scope.PagesSys00014 = [];
	$scope.toolbarOptions = {
		items: [
				 {
					 type: "button", text: "<span class=\"fa fa-plus-circle\"></span> New City",
					 attributes: { "class": "btn-primary" },
					 click:
						 function (e) {
							 $scope.$apply(function () {
								 $scope.AddSys00014();
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
	$scope.LoadSys00014 = function (CityID) {
		if (angular.isUndefined($scope.Sys00014.CityID)) {
			$http.get('/Adm_City/GetSingle?KaizenPublicKey=' + sessionStorage.PublicKey + "&CityID=" + CityID).success(function (data) {
				$scope.Sys00014 = data;
			}).finally(function () {
				$scope.Sys00014.Status = 2;
			});
		}
	};
	$scope.LoadSys00014Page = function (ActionName) {
		removeEntityPage($scope.PagesSys00014);
		var URIPath = "/" + $scope.ToolBar.ActiveScreen.ID + "/" + ActionName + "?KaizenPublicKey=" + sessionStorage.PublicKey;
		var Page = {
			url: URIPath, ActionName: ActionName
		};
		$scope.PagesSys00014.push(Page);
	};

	$scope.AddSys00014 = function () {
		$scope.LoadSys00014Page('Create');
		$scope.Clear();
		$scope.Sys00014.Status = 1;
	};
	$scope.EditSys00014 = function (CityID) {
		$scope.LoadSys00014Page('Create');
		$scope.LoadSys00014(CityID);
	};
	$scope.SaveData = function () {
		$http.post('/Adm_City/SaveData', {
			'Sys00014': $scope.Sys00014, 'KaizenPublicKey': sessionStorage.PublicKey
		}).success(function (data) {
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
		$http.post('/Adm_City/UpdateData', {
			'Sys00014': $scope.Sys00014, 'KaizenPublicKey': sessionStorage.PublicKey
		}).success(function (data) {
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
		$scope.Sys00014 = {
		};
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
				$http.post('/Adm_City/DeleteData', { 'Sys00014': $scope.Sys00014, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
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
		$scope.PagesSys00014 = [];
	};
});