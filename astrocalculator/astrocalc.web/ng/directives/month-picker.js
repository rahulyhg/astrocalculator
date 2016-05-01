(function () {
    var monthPicker = angular.module("astroapp").directive("monthPicker", function () {
        return {
            restrict: "EA",
            scope: {
                dt:"="
            },
            templateUrl: "ng/templates/month-picker.html",
            controller: function ($scope) {
                //this is the date that is selected and operted on
                $scope.dt = {
                    year: new Date().getFullYear(),
                    month: new Date().getMonth() + 1,
                    monthValue: function () {
                        var merged = [].concat.apply([], $scope.monthsLayout);
                        return merged.filter(function (el) {
                            return el.pos == $scope.dt.month;
                        })[0].value;
                    }
                };
                $scope.dropMenu = false; //denotes if the selection menu is dropped
                //this is the month layout for the selection menu
                $scope.monthsLayout = [
                   [{ value: "Jan", pos: 1 }, { value: "Feb", pos: 2 }, { value: "Mar", pos: 3 }, {value:"Apr", pos:4}],
                   [{ value: "May", pos: 5 }, { value: "Jun", pos: 6 }, { value: "Jul", pos: 7 }, { value: "Aug", pos: 8 }],
                   [{ value: "Sep", pos: 9 }, { value: "Oct", pos: 10 }, { value: "Nov", pos: 11 }, { value: "Dec", pos: 12 }],
                ];
                //this is to change the year of the date
                $scope.moveYear = function (delta) {
                    $scope.dt.year = $scope.dt.year + delta;
                }
                //this is the movement in the month
                $scope.moveMonth = function (pos) {
                    $scope.dt.month = pos;
                    $scope.dropMenu = false;
                }
            }
        }
    })
})();