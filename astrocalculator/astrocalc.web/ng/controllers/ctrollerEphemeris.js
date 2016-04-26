(function () {
    var ctrollerEphemeris = angular.module("astroapp").controller("ctrollerEphemeris", function ($scope, svcWebapi) {
        $scope.cityLike = "";
        $scope.cities = [];
        var citySuggestions = function (like) {
            svcWebapi.citySuggestions(like).then(function (data) {
               
                data.forEach(function (el, idx) {
                    el.picked = false;
                    $scope.cities.push(el)
                })
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
            $scope.cities.filter(function (el, index) {
                return el.id != s.id; //we are trying to get all the other cities not being selected
            }).forEach(function (el, index) {
                el.picked = false;
            });
            $scope.cities.filter(function (el, index) {
                return el.id == s.id;
            })[0].picked = true;
        }
    })
})();