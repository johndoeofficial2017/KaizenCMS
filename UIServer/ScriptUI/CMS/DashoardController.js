app.controller('DashoardController', function ($scope, $http) {
    $scope.selectedNumber = 10;
    $scope.maxValue = 100;
    $scope.minValue = 0;
    //alert($scope.MY.AgentID);
    //$http.get('/Dashoard/GetCurrentTarget?AgentID=' + $scope.MY.AgentID
    //    + '&KaizenPublicKey=' + sessionStorage.PublicKey).success(function (data) { 
    //    $scope.CM10110 = data;
    //    //alert(JSON.stringify(data, null, 2));
    //}).finally(function () { });

    //$scope.electricity = new kendo.data.DataSource({
    //    transport: {
    //        read: {
    //            url: "/CMS_Agent/GetAgentTargets?AgentID=999&ClientID=3&KaizenPublicKey=" + sessionStorage.PublicKey,
    //            dataType: "json"
    //        }
    //    }
    //});

    $scope.agentTargets = {};

    $http.get('/Inq_Target/GetAgentTargetClaimById?KaizenPublicKey=' + sessionStorage.PublicKey,
                     {
                         params: {
                             AgentID: 1
                         }
                     })
                     .success(function (data) {
                         $scope.agentTargets = data;
                     }).finally(function () { });

    $scope.kendoAxes = [
           {
               name: "rain",
               color: "#007eff",
               min: 0,
               max: 60
           }, {
               name: "wind",
               color: "#73c100",
               min: 0,
               max: 60
           }, {
               name: "temp",
               min: -30,
               max: 30
           }
    ];
    $scope.Cancel = function () {
        $scope.Clear();
        $scope.PagesSys00014 = [];
    };

    $scope.chartTitle = { text: 'Agent 1: Target vs Claim' }; 
});