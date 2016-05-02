(function () {
    var ctrollerEphemeris = angular.module("astroapp").controller("ctrollerEphemeris", function ($scope, svcWebapi, $location) {
        $scope.dt = null;
        $scope.location = null;
        $scope.meridian = null;
        $scope.ephemeris = [];
        $scope.locationCreating = false; //this just denotes if the location is being created or no
        var checkIfOkForDownload = function () {
            return $scope.dt !== null &&
                $scope.location.lat !== null &&
                $scope.location.lng !== null &&
                $scope.meridian != null;
        }
        var downloadEphemeris = function () {
            svcWebapi.getSolarEphemeris($scope.location.lat,
                $scope.location.lng,
                $scope.meridian.value,
                $scope.dt.year,
                $scope.dt.month).then(function (data) {
                    $scope.ephemeris = data;
            });
        }
        $scope.$watch("dt", function (after, before) {
            if (checkIfOkForDownload() == true) {
                downloadEphemeris();
            }
            else {
                $scope.ephemeris = [];
            }
        },true);
        $scope.$watch("location", function (after, before) {
            if (checkIfOkForDownload() == true) {
                downloadEphemeris();
            }
            else {
                $scope.ephemeris = [];
            }
        }, true);
        $scope.$watch("meridian", function (after, before) {
            if (checkIfOkForDownload() == true) {
                downloadEphemeris();
            }
            else {
                $scope.ephemeris = [];
            }
        });
        
    })
})();