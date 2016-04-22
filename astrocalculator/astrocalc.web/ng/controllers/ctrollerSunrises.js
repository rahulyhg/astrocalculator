(function () {
    var ctrollerSunrises = angular.module("astroapp").controller("ctrollerSunrises", function ($scope, svcWebApi) {
        $scope.model = {};
        
        console.info("we are inside the sunrises controller");
        var init = function () {
            //this is where you can go ahead to get the data from the server
            //data  - solar configuration and the options
            console.info("now getting the colar configuration");
            svcWebApi.getSolarConfig().then(function (data) {
                $scope.model = angular.copy(data, $scope.model);
                $scope.model.city = $scope.model.citychoices[0].title;
            }, function (status) {
                //this is when the model was not being populated .. 
                $scope.model = {};
            })
        }; init();
      
    })
})()