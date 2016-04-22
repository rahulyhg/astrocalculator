(function () {
    var ctrollerSunrises = angular.module("astroapp").controller("ctrollerSunrises", function ($scope, svcWebApi) {
        $scope.model = {};
        $scope.selection = {
            city :"",year:0, month:""
        };
        $scope.solardetails = [];
        var init = function () {
            //this is where you can go ahead to get the data from the server
            //data  - solar configuration and the options
            svcWebApi.getSolarConfig().then(function (data) {
                $scope.model = angular.copy(data, $scope.model);
                $scope.selection.city = $scope.model.city.title;
                $scope.selection.year = $scope.model.year.toString();
                $scope.selection.month = $scope.model.choices.month[$scope.model.month - 1];
                //with the current month , day year and the location we can make the call to get the sunrises
                var postmodel = {};
                postmodel = angular.copy($scope.model, postmodel);
                postmodel.choices = null;
                svcWebApi.getSunrises(postmodel).then(function (data) {
                    console.log("we have received the monthly solar details .. ");
                    $scope.solardetails = data;
                    console.debug($scope.solardetails);
                }, function (status) {
                    console.error("failed to receive the monthly solar details");
                })
            }, function (status) {
                //this is when the model was not being populated .. 
                $scope.model = {};
            })
        }; init();
        $scope.$watch("selection.city", function (after, before) {
            if (after !== undefined && after != null && after != "" && $scope.model.choices!==undefined) {
                $scope.model.city = $scope.model.choices.city.filter(function (el) {
                    return el.title == after;
                })[0];
            }
        })
        $scope.$watch("selection.year", function (after, before) {
            if (after !== undefined && after != null && $scope.model.choices!==undefined) {
                $scope.model.year = $scope.model.choices.year.filter(function (el) {
                    return el == after;
                })[0];
            }
        });
        $scope.$watch("selection.month", function (after, before) {
            if (after !== undefined && after != null && after != "" && $scope.model.choices!==undefined) {
                $scope.model.month = $scope.model.choices.month.filter(function (el) {
                    return el == after;
                })[0];
            }
        })
    })
})()