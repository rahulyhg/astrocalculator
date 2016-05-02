(function () {
    var locationAppend = angular.module("astroapp").directive("locationAppend", function (svcWebapi) {
        return {
            restrict: "EA",
            scope: {
                location: "=",
                created:"&"
            },
            templateUrl: "ng/templates/location-append.html",
            controller: function ($scope) {
                //this is the default location model that is set when the user starts editing the location
                $scope.location = {
                    lat: null,
                    lng: null,
                    title: "",
                    state: ""
                };
                $scope.states = [];
                var getStates = function () {
                    $scope.states = [];
                    svcWebapi.getStates().then(function (data) {
                        $scope.states = data;
                    }, function (data) {
                        console.error("there was a problem getting the states options from the server");
                        $scope.states = [];
                    })
                }; getStates();
                $scope.onCreate = function () {
                    svcWebapi.postLocation({
                        city: $scope.location.title,
                        longitude: $scope.location.lng,
                        latitude: $scope.location.lat,
                        state:$scope.location.state
                    }).then(function (data) {
                        $scope.created();
                    }, function (data) {
                        console.error("Cannot create new location");
                    })
                }
            }
        }
    })
})();