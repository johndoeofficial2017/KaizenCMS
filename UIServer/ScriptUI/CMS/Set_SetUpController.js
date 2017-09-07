app.controller('Set_SetUpController', function ($scope, $http) {
    $scope.CM00000 = { Status: 1 };
    $scope.PagesCM00000 = [];
    $scope.LoadLookUp = function () {
        $http.get('/Set_SetUp/GetSingle?KaizenPublicKey=' + sessionStorage.PublicKey + "&SetupID=" + 1).success(function (data) {
            if (data.SetupID == 1) {
                $scope.CM00000 = data;
                $scope.CM00000.Status = 2;
            }
            else {
                $scope.CM00000 = { Status: 1 };
            }
        }).finally(function () { });
    };
    $scope.LoadLookUp();

    $scope.SaveData = function () {
        $http.post('/CMS_Set_CM00000/SaveData', { 'CM00000': $scope.CM00000, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
            if (data.Status == true) {
                $.bigBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#739E73",
                    timeout: 4000,
                    icon: "fa fa-check shake animated"
                });
                $scope.LoadLookUp();
            }
            else {
                $.bigBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#C46A69",
                    timeout: 4000,
                    icon: "fa fa-bolt swing animated"
                });
            }

        }).error(function (data, status, headers, config) {
            $.bigBox({
                title: data.Massage,
                content: data.Description,
                color: "#C46A69",
                timeout: 4000,
                icon: "fa fa-bolt swing animated"
            });
        });
    };
    $scope.UpdateData = function () {
        $http.post('/CMS_Set_CM00000/UpdateData', { 'CM00000': $scope.CM00000, 'KaizenPublicKey': sessionStorage.PublicKey }).success(function (data) {
            if (data.Status == true) {
                $.bigBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#739E73",
                    timeout: 4000,
                    icon: "fa fa-check shake animated"
                });
                $scope.LoadLookUp();
            }
            else {
                $.bigBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#C46A69",
                    timeout: 4000,
                    icon: "fa fa-bolt swing animated"
                });
            }

        }).error(function (data, status, headers, config) {
            $.bigBox({
                title: data.Massage,
                content: data.Description,
                color: "#C46A69",
                timeout: 4000,
                icon: "fa fa-bolt swing animated"
            });
        });
    };

    $scope.Cancel = function () {
        $scope.Clear();
        $scope.PagesCM00000 = [];
    };

    $scope.Clear = function () {
        $scope.StandardFieldsList = [];
    };


    $scope.StandardFields = function() {
        $scope.LoadCM00000Page('StandardFields');
        //$scope.Clear();
        $scope.LoadStandardFields();
    };
    $scope.LoadCM00000Page = function (ActionName) {
        removeEntityPage($scope.PagesCM00000);
        var URIPath = "/CMS/" + $scope.ToolBar.ActiveScreen.ID + "/" + ActionName + "?KaizenPublicKey=" + sessionStorage.PublicKey;
        var Page = { url: URIPath, ActionName: ActionName };
        $scope.PagesCM00000.push(Page);
    }
    $scope.StandardFieldsList = [];
    $scope.LoadStandardFields = function () {
        $http.get('/Set_SetUp/GetStandardFields?KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) {
                if (data.length >= 0) {
                    if (data.length > 0)
                        $scope.StandardFieldsList = data;
                    else
                        $scope.StandardFieldsList = [];
                }
            });
    };
    $scope.UpdateStandardFields = function () {
        //var selView = $scope.CM00071.SelectedView;
        $scope.fieldList1 = [];
        $scope.StandardFieldsList.forEach(function (ele, index) {
            var temp = {
                FieldDisplay: ele.FieldDisplay,
                FieldName: ele.FieldName,
                FieldOrder: ele.FieldOrder,
                FieldWidth: ele.FieldWidth,
                //IsActive: ele.IsActive,
                //IsEmailable: ele.IsEmailable,
                //IsFilterable: ele.IsFilterable,
                //Islockable: ele.Islockable,
                //Islocked: ele.Islocked,
                //IsPrintable: ele.IsPrintable,
                //IsRequired: ele.IsRequired,
                //IsSortable: ele.IsSortable
                FieldTypeID: ele.FieldTypeID
            }
            $scope.fieldList1.push(temp);
        });
        $http.post('/Set_SetUp/UpdateStandardFields1', {
            'CM00038List': $scope.fieldList1,'callNumber':1,
            'KaizenPublicKey': sessionStorage.PublicKey
        }).success(function (data) {
            if (data.Status == true) {
                $.bigBox({
                    title: data.Massage,
                    content: data.Description,
                    color: "#739E73",
                    timeout: 8000,
                    icon: "fa fa-check shake animated"
                });

                $scope.fieldList2 = [];
                $scope.StandardFieldsList.forEach(function (ele, index) {
                    var temp = {
                        //FieldDisplay: ele.FieldDisplay,
                        FieldName: ele.FieldName,
                        //FieldOrder: ele.FieldOrder,
                        //FieldWidth: ele.FieldWidth,
                        IsActive: ele.IsActive,
                        IsEmailable: ele.IsEmailable,
                        IsFilterable: ele.IsFilterable,
                        Islockable: ele.Islockable,
                        Islocked: ele.Islocked,
                        IsPrintable: ele.IsPrintable,
                        IsRequired: ele.IsRequired,
                        IsSortable: ele.IsSortable,
                        //FieldTypeID: ele.FieldTypeID
                    }
                    $scope.fieldList2.push(temp);
                });
                $http.post('/Set_SetUp/UpdateStandardFields2', {
                    'CM00038List': $scope.fieldList2, 'callNumber': 2,
                    'KaizenPublicKey': sessionStorage.PublicKey
                }).success(function (data) {
                    if (data.Status == true) {
                        $.bigBox({
                            title: data.Massage,
                            content: data.Description,
                            color: "#739E73",
                            timeout: 8000,
                            icon: "fa fa-check shake animated"
                        });
                    }
                }).error(function (data, status, headers, config) {
                    alert(data);
                });
            }
            else {
                //alert('ssssss')
            }
        }).error(function (data, status, headers, config) {
            alert(data);
        });
    };
});