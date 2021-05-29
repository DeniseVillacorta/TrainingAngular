var app = angular.module('userApp', ['ngRoute']);

app.controller('userCtrl', function ($scope) {

});

app.config(function ($routeProvider, $locationProvider) {
    $routeProvider
        .when("/", {
            templateUrl: "signin/templates/signin.html",
            controller: "signinCtrl"

        })
        .otherwise({ redirectTo: '/' });
    $locationProvider.html5Mode(true);
});

app.service('services', function ($http, $window) {
    services = {};

    //Set the link once in upon open of project
    localStorage.setItem('link', 'http://localhost:49423/');

    var surl = localStorage.getItem('link');

    //Sign In
    services.signin = function (data) {
        var result = $http({
            method: 'POST',
            url: surl + 'api/auth/signin', data: data
        }).then(function (response) {
            return response;
        }, function (err) {
            return err;
        });
        return result;
    };


    return services;
});