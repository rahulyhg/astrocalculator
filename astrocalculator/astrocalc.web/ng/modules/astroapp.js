(function () {
    var astroapp = angular.module("astroapp", ["ngRoute"])
        .config(["$routeProvider", function ($routeProvider) {
            $routeProvider
            .when("/", { templateUrl: "ng/views/home.html" })
            .when("/sunrises", { templateUrl: "ng/views/sunrises.html", controller:"ctrollerSunrises" })
            .otherwise({ redirectTo: "/" })
        }])
})()