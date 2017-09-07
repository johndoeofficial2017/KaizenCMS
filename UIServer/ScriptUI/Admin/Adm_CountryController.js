app.controller('Adm_CountryController', function ($scope, $http) {
	$scope.Sys00013 = {};
	$scope.PagesSys00013 = [];
	$scope.toolbarOptions = {
		items: [
				 {
					 type: "button", text: "<span class=\"fa fa-plus-circle\"></span> New Country",
					 attributes: { "class": "btn-primary" },
					 click:
						 function (e) {
							 $scope.$apply(function () {
								 $scope.AddSys00013();
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
	$scope.LoadSys00013 = function (CountryID) {
		if (angular.isUndefined($scope.Sys00013.CountryID)) {
			$http.get('/Adm_Country/GetSingle?KaizenPublicKey=' + sessionStorage.PublicKey + "&CountryID=" + CountryID).success(function (data) {
				$scope.Sys00013 = data;
			}).finally(function () {
				$scope.Sys00013.Status = 2;
			});
		}
	};
	$scope.LoadSys00013Page = function (ActionName) {
		removeEntityPage($scope.PagesSys00013);
		var URIPath = "/" + $scope.ToolBar.ActiveScreen.ID + "/" + ActionName + "?KaizenPublicKey=" + sessionStorage.PublicKey;
		var Page = {
			url: URIPath, ActionName: ActionName
		};
		$scope.PagesSys00013.push(Page);
	};

	$scope.AddSys00013 = function () {
		$scope.LoadSys00013Page('Create');
		$scope.Clear();
		$scope.Sys00013.Status = 1;
	};
	$scope.EditSys00013 = function (CountryID) {
		$scope.LoadSys00013Page('Create');
		$scope.LoadSys00013(CountryID);
	};
	$scope.SaveData = function () {
		$http.post('/Adm_Country/SaveData', {
			'Sys00013': $scope.Sys00013, 'KaizenPublicKey': sessionStorage.PublicKey
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
		$http.post('/Adm_Country/UpdateData', {
			'Sys00013': $scope.Sys00013, 'KaizenPublicKey': sessionStorage.PublicKey
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
		$scope.Sys00013 = {
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
				$http.post('/Adm_Country/DeleteData', { 'Sys00013': $scope.Sys00013, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
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
		$scope.PagesSys00013 = [];
	};
});