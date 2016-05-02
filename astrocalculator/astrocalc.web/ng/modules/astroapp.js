(function () {
    var astroapp = angular.module("astroapp", ["ngRoute"])
        .config(["$routeProvider", function ($routeProvider) {
            $routeProvider
            .when("/", { templateUrl: "ng/views/login.html", controller: "ctrollerLogin" })
            .when("/login", { templateUrl: "ng/views/login.html", controller: "ctrollerLogin" })
            .when("/sunrises", { templateUrl: "ng/views/solarephemeris.html", controller: "" })
            .otherwise({ redirectTo: "/" })
        }])
    .provider("webserver", function () {
        var ws = {
            baseUrl:"http://localhost:8081/api/"
        }
        this.$get = function () {
            return ws;
        }
    })
    .filter("angle", function () {
        return function (value, format) {
            if (/deg min/.test(format) ==true) {
                //we would have to convert the value to degrees and minutes
                var numberVal = parseFloat(value);
                var mins = Math.floor((numberVal - Math.floor(numberVal)) * 60)
                return Math.floor(numberVal).toString() + "\xB0" + mins.toString() + "'"
            }
        }
    })
})()