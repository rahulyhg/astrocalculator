(function () {
    var astroapp = angular.module("astroapp", ["ngRoute"])
        .config(["$routeProvider", function ($routeProvider) {
            $routeProvider
            .when("/", { templateUrl: "ng/views/home.html" })
            .when("/home", { templateUrl: "ng/views/home.html" })
            .otherwise({ redirectTo: "/" })
        }])
})()