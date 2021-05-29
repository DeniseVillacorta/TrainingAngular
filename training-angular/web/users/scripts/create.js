var app = angular.module('usersApp');

app.controller('createCtrl', function ($scope,$location,services) {
    //services.getUsertypes().then(function (result) {
    //    $scope.usertypes = result;
    //    console.log(result);
    //});

    $scope.create = function () {
        var data = {
            "username": $scope.user.Username,
            "password": $scope.user.Password,
            "name": $scope.user.Name,
            "email": $scope.user.Email,
        };
        services.createUser(data).then(function (result) {
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