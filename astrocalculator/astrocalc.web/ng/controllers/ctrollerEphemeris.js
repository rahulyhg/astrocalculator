(function () {
    var ctrollerEphemeris = angular.module("astroapp").controller("ctrollerEphemeris", function ($scope, svcWebapi) {
        $scope.cityLike = "";
        $scope.cities = [];
        //this is the model that is used to send to api to get the ephemeris values
        $scope.httpBody = {
            lat: null,
            lng: null,
            zen: null,
            yr: null,
            mn:null
        }
        var citySuggestions = function (like) {
            svcWebapi.citySuggestions(like).then(function (data) {
                $scope.cities = data;//whatever the server sends in is an acceptable model
            })
        }
        $scope.$watch("cityLike", function (after, before) {
            if (after !== undefined && after !== "") {
                //we have a change in the suggestion
                $scope.cities = [];
                citySuggestions(after);
            }
        });

        $scope.selectCity = function (s) {
            //trying to get the city from the collection
            var city  = $scope.cities.filter(function (el, index) {
                return el.id == s.id;
            })[0];
            //getting the request payload populated.
            if (city != null && city !== undefined) {
                $scope.httpBody.lat = city.latitude;
                $scope.httpBody.lng = city.longitude;
            }
            //clearing off the suggestions since the city has been selected
            $scope.cities = [];
        }
        $scope.deselectCity = function () {
            $scope.httpBody.lat = null;
            $scope.httpBody.lng = null;
        }

        svcWebapi.yearSuggestions().then(function (data) {
            $scope.years = data;
        })
    })
})();