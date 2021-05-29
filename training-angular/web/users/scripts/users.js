var app = angular.module('usersApp', ['ngRoute', 'angularUtils.directives.dirPagination', 'ui.bootstrap']);

app.controller('usersCtrl', function ($scope, services) {
    services.profile().then(function (result) {
        $scope.profile = result;
        console.log($scope.profile);
    });
});

app.config(function ($routeProvider, $locationProvider) {
    $routeProvider
        .when("/list", {
            templateUrl: "templates/list.html",
            controller: "listCtrl"

        })
        .when("/create", {
            templateUrl: "templates/create.html",
            controller: "createCtrl"
        })
        .when("/update", {
            templateUrl: "templates/update.html",
            controller: "updateCtrl"
        })
        .otherwise({ redirectTo: '/list' });
    $locationProvider.html5Mode(true);
});


app.service('services', function ($http, $window) {
    services = {};

    var token = localStorage.getItem('token');
    var surl = localStorage.getItem('link');

    //Get Profile of User
    services.profile = function () {
        var result =
            $http({
                method: 'GET',
                url: surl + 'api/users/profile',
                headers: {
                    'Authorization': 'Bearer ' + token
                }
            }).then(function (response) {
                return response.data;
            }, function (err) {
                return err;
            });

        return result;
    };

    //Get All Users
    services.getUsers = function () {
        var result =
            $http({
                method: 'GET',
                url: surl + 'api/users',
                headers: {
                    'Authorization': 'Bearer ' + token
                }
            }).then(function (response) {
                return response.data;
            }, function (err) {
                return err;
            });

        return result;
    };

    //Get User by Id
    services.getUser = function (id) {
        var result =
            $http({
                method: 'GET',
                url: surl + 'api/users/' + id,
                headers: {
                    'Authorization': 'Bearer ' + token
                }
            }).then(function (response) {
                return response.data;
            }, function (err) {
                return err;
            });

        return result;
    };

    //Create User
    services.createUser = function (data) {
        var result =
            $http({
                method: 'POST',
                url: surl + 'api/users',
                headers: {
                    'Authorization': 'Bearer ' + token
                }, data: data
            }).then(function (response) {
                return response;
            }, function (err) {
                return err;
            });
        return result;
    };

    //Update User
    services.updateUser = function (id, data) {
        var result =
            $http({
                method: 'PUT',
                url: surl + 'api/users/' + id,
                headers: {
                    'Authorization': 'Bearer ' + token
                }, data: data
            }).then(function (response) {
                return response;
            }, function (err) {
                return err;
            });
        return result;
    };

    ////Get User type List
    //services.getUsertypes = function () {
    //    var result =
    //        $http({
    //            method: 'GET',
    //            url: surl + 'api/users',
    //            headers: {
    //                'Authorization': 'Bearer ' + token
    //            }
    //        }).then(function (response) {
    //            return response.data;
    //        }, function (err) {
    //            if (err.status == 401) {
    //                $window.location.href = "login/index.html";
    //            }
    //            return err;
    //        });

    //    return result;
    //};

    //Update Status of User
    services.changeStatus = function (id, status) {
        var result =
            $http({
                method: 'PUT',
                url: surl + 'api/users/change/' + id + "?status=" + status,
                headers: {
                    'Authorization': 'Bearer ' + token
                }, data: status
            }).then(function (response) {
                return response;
            }, function (err) {
                return err;
            });
        return result;
    };


    return services;
});

app.directive('validationError', function () {
    return {
        restrict: 'A',
        require: 'ngModel',
        link: function (scope, elem, attrs, ctrl) {
            scope.$watch(attrs['validationError'], function (errMsg) {
                if (elem[0] && elem[0].setCustomValidity) { // HTML5 validation
                    elem[0].setCustomValidity(errMsg);
                }
                if (ctrl) { // AngularJS validation
                    ctrl.$setValidity('validationError', errMsg ? false : true);
                }
            });
        }
    }
});