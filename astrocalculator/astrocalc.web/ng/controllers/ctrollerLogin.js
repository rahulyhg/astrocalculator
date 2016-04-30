(function () {
    var ctrollerLogin = angular.module("astroapp").controller("ctrollerLogin", function ($scope, svcWebapi, svcCache, $location) {
        $scope.m = {
            username: {ok: true, value:""},
            pin: {ok: true, value:""}
        }
        $scope.warning = { show: false, msg: "" };
        svcCache.flushUser();
        $scope.validate = function () {
            $scope.m.username.ok = $scope.m.username.value != "" && /@/.test($scope.m.username.value) == false ? true : false;
            $scope.m.pin.ok = $scope.m.pin.value != "" && $scope.m.pin.value.length <= 12 ? true:false;
            return $scope.m.username.ok && $scope.m.pin.ok;
        }
        $scope.login = function () {
            if ($scope.validate() == true) {
                svcWebapi.userLogin().then(function (data) {
                    console.info("User has logged in successfully");
                    $scope.warning.show = false;
                    $scope.warning.msg = "";
                    svcCache.storeUser(data).then(function (d) {
                        $location.url("/sunrises");
                    });
                }, function (response) {
                    //this is when the user is not authorized 
                    switch (response.status) {
                        case 400:
                            $scope.warning.message = "Username is invalid , please try again";
                            break;
                        case 401:
                            $scope.warning.message = "Username and password combination is incorrect, please try again";
                            break;
                        case 500:
                            $scope.warning.message = "Something went wrong on the server, please contact admin";
                            break;
                        default:
                    }
                    $scope.warning.show = true;
                })
            }
        }
    })
})()