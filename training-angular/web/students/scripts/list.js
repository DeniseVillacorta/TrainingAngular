var app = angular.module('studentsApp');

app.controller('listCtrl', function ($scope, $location, $rootScope) {
    $scope.page = 1;
    $scope.selectvalue = '15';

    services.getStudents().then(function (result) {
        $scope.students = result;
        console.log(result);
    });

    $scope.create = function () {
        $location.path('create');
    }

    $scope.edit = function (id) {
        $rootScope.studentId = id;
        $location.path('update');
    }

    $scope.changeStatus = function (studentid, status) {

        services.changeStatus(studentid, status).then(function (result) {
            if (result.status == 400) {
                swal(result.data.Message, "", "error");
            } else if (result.status == 500) {
                swal("Internal Server Error", "", "error");
            } else if (result.status == 200) {
                if (status == true)
                    swal("Success", "Enabled Student", "success");
                if (status == false)
                    swal("Success", "Disabled Student", "success");
            }
        });
    }
});