var app = angular.module('studentsApp', ['ngRoute', 'angularUtils.directives.dirPagination', 'ui.bootstrap']);

app.controller('studentsCtrl', function ($scope, services) {
    //services.profile().then(function (result) {
    //    $scope.profile = result;
    //    console.log($scope.profile);
    //});
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

    //Get All Students
    services.getStudents = function () {
        var result =
            $http({
                method: 'GET',
                url: surl + 'api/students',
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

    //Get Student by Id
    services.getStudent = function (id) {
        var result =
            $http({
                method: 'GET',
                url: surl + 'api/students/' + id,
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

    //Create Student
    services.createStudent = function (data) {
        var result =
            $http({
                method: 'POST',
                url: surl + 'api/students',
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

    //Update Student
    services.updateStudent = function (id, data) {
        var result =
            $http({
                method: 'PUT',
                url: surl + 'api/students/' + id,
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

    //Update Status Student
    services.changeStatus = function (id, status) {
        var result =
            $http({
                method: 'PUT',
                url: surl + 'api/students/change/' + id + "?status=" + status,
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

app.filter('isUnderAge', function () {
    return function (val,addedAge) {
       
        if (val + addedAge <= 17)
            return true;

        return false;
    };
});

app.directive('alphaNumeric', function () {
    return {
        require: 'ngModel',
        link: function (scope, element, attr, ngModelCtrl) {
            function fromUser(text) {
                if (text) {
                    var transformedInput = text.replace(/[^A-Za-z0-9]/g, '');

                    if (transformedInput !== text) {
                        ngModelCtrl.$setViewValue(transformedInput);
                        ngModelCtrl.$render();
                    }
                    return transformedInput;
                }
                return undefined;
            }
            ngModelCtrl.$parsers.push(fromUser);
        }
    };
});
app.directive('numbersOnly', function () {
    return {
        require: 'ngModel',
        link: function (scope, element, attr, ngModelCtrl) {
            function fromUser(text) {
                if (text) {
                    var transformedInput = text.replace(/[^0-9]/g, '');

                    if (transformedInput !== text) {
                        ngModelCtrl.$setViewValue(transformedInput);
                        ngModelCtrl.$render();
                    }
                    return transformedInput;
                }
                return undefined;
            }
            ngModelCtrl.$parsers.push(fromUser);
        }
    };
});
app.directive('alphaOnly', function () {
    return {
        require: 'ngModel',
        link: function (scope, element, attr, ngModelCtrl) {
            function fromUser(text) {
                if (text) {
                    var transformedInput = text.replace(/[^A-Za-z]/g, '');

                    if (transformedInput !== text) {
                        ngModelCtrl.$setViewValue(transformedInput);
                        ngModelCtrl.$render();
                    }
                    return transformedInput;
                }
                return undefined;
            }
            ngModelCtrl.$parsers.push(fromUser);
        }
    };
});