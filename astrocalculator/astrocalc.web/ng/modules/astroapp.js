(function () {
    var astroapp = angular.module("astroapp", ["ngRoute"])
        .config(["$routeProvider", function ($routeProvider) {
            $routeProvider
            .when("/", { templateUrl: "ng/views/solarephemeris.html", controller: "ctrollerSunrises" })
            .when("/sunrises", { templateUrl: "ng/views/solarephemeris.html", controller: "ctrollerSunrises" })
            .otherwise({ redirectTo: "/" })
        }])
    .provider("webserver", function () {
        var ws = {
            baseUrl:"http://localhost:1248/"
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