(function () {
    var locationPicker = angular.module("astroapp").directive("locationPicker", function (svcWebapi) {
        return {
            restrict: "EA",
            scope: {
                location:"="
            },
            templateUrl: "ng/templates/location-picker.html",
            controller: function ($scope) {
                $scope.location = {
                    suggestions: [],
                    phrase: "",
                    lat: null,
                    lng: null,
                    title: ""
                }
                $scope.clearLocation = function () {
                    $scope.location.lat = null;
                    $scope.location.lng = null;
                    $scope.location.suggestions = [];
                    $scope.location.title = "";
                    $scope.location.phrase = "";
                }
                $scope.selectCity = function (city) {
                    $scope.location.suggestions = [];
                    $scope.location.phrase = "";
                    $scope.location.lat = city.latitude;
                    $scope.location.lng = city.longitude;
                    $scope.location.title = city.city;
                }
                $scope.$watch("location.phrase", function (after, before) {
                    if (after !== "" && after !== null) {
                        //go get the suggestions
                        svcWebapi.citySuggestions(after).then(function (data) {
                            $scope.location.suggestions = data;
                        }, function (data) {
                            console.error("failed to load the city suggestions");
                        })
                    }
                });
            }
        }
    })
})();