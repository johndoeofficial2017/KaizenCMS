app.controller('Sys_PopUpController', function ($scope, $http) {

    $http.get('/Sys_PopUp/GetScreenPopUpFields?KaizenPublicKey=' + sessionStorage.PublicKey, {
        params: {
            ScreenID: $scope.TransferObject.ScreenID
        }
    }).success(function (data) {
        $scope.GridColumns = data;
        $scope.GridSchema = [];
        $scope.GridColumns.forEach(function (element, index) {
            var arry = {};
            if (element.FieldTypeID == 1)
                arry[element.field] = { type: "number" };
            else if (element.FieldTypeID == 2)
                arry[element.field] = { type: "string" };
            else if (element.FieldTypeID == 3)
                arry[element.field] = { type: "date" };
            else
                arry[element.field] = { type: "string" };
            $scope.GridSchema.push(arry);
        })
        alert(JSON.stringify($scope.GridColumns,null,2));
        $scope.GenericGridOptions = {
            dataSource: {
                transport: {
                    read: {
                        url: "/Sys_View/LookUpDataGrid",
                        data: {
                            KaizenPublicKey: sessionStorage.PublicKey,
                            kaizenTableName: $scope.CurrentControl
                        },
                        dataType: "json",
                        type: "GET"
                    }
                },
                pageSize: 20,
                serverPaging: true,
                serverSorting: true
            },
            sortable: true, refresh: true,
            scrollable: false,
            pageable: {
                pageSize: 20,
                refresh: true,
            },
            columns: $scope.GridColumns
        };

    });


    $scope.LoadPopUp = function () {
        alert('enter');

    };

});
