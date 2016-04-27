(function () {
    var locationController = angular.module("astroapp").controller("locationController", function ($scope, svcWebapi, $location) {
        $scope.httpmodel = {
            state: "",
            longitude: 00,
            latitude: 00,
            city:""
        };
        $scope.states = [];
        //download the states from the server
        svcWebapi.getStates().then(function (data) {
            $scope.states = data
        });
        $scope.createLocation = function () {
            svcWebapi.postLocation($scope.httpmodel).then(function (data) {
                $location.url("/")
            })
        }
    })
})();