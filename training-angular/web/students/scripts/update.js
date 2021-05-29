var app = angular.module('studentsApp');

app.controller('updateCtrl', function ($scope, $rootScope, $location) {
    //services.getUsertypes().then(function (result) {
    //    $scope.usertypes = result;
    //    console.log(result);
    //});

    services.getStudent($rootScope.studentId).then(function (result) {
        $scope.student = result;
        console.log(result);
    });

    $scope.update = function () {
        var data = {
            "firstName": $scope.student.Firstname,
            "lastName": $scope.student.Lastname,
            "middleName": $scope.student.Middlename,
            "gender": $scope.student.Gender,
            "age": $scope.student.Age,
            "address": $scope.student.Address,
            "course": $scope.student.Course,
            "is_active": $scope.student.IsActive,
        };
        services.updateStudent($rootScope.studentId, data).then(function (result) {
            if (result.status == 400) {
                swal(result.data.Message, "", "error");
            } else if (result.status == 500) {
                swal("Internal Server Error", "", "error");
            } else if (result.status == 200) {
                swal("Success", "", "success");
                $location.path("list");
            }
        });
    }
});