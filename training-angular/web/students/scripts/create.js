var app = angular.module('studentsApp');

app.controller('createCtrl', function ($scope, $location, services) {
    //services.getUsertypes().then(function (result) {
    //    $scope.usertypes = result;
    //    console.log(result);
    //});

    $scope.create = function () {
        var data = {
            "firstName": $scope.student.Firstname,
            "lastName": $scope.student.Lastname,
            "middleName": $scope.student.Middlename,
            "gender": $scope.student.Gender,
            "age": $scope.student.Age,
            "address": $scope.student.Address,
            "course": $scope.student.Course,
        };
        services.createStudent(data).then(function (result) {
            if (result.status == 400) {
                swal(result.data.Message, "", "error");
            } else if (result.status == 500) {
                swal("Internal Server Error", "", "error");
            } else if (result.status == 200) {
                swal("Success", "", "success");
                $location.path("/list");
            }
        });
    }
});