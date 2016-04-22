(function () {
    var astroapp = angular.module("astroapp", ["ngRoute"])
        .config(["$routeProvider", function ($routeProvider) {
            $routeProvider
            .when("/", { templateUrl: "ng/views/sunrises.html", controller: "ctrollerSunrises" })
            .when("/sunrises", { templateUrl: "ng/views/sunrises.html", controller:"ctrollerSunrises" })
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
})()