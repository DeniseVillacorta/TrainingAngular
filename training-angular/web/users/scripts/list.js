var app = angular.module('usersApp');

app.controller('listCtrl', function ($scope,$location, $rootScope) {
    $scope.page = 1;
    $scope.selectvalue = '15';

    services.getUsers().then(function (result) {
        $scope.users = result;
        console.log(result);
    });

    $scope.create = function () {
        $location.path('create');
    }

    $scope.edit = function (id) {
        $rootScope.userId = id;
        $location.path('update');
    }

    $scope.changeStatus = function (userid, status) {

        services.changeStatus(userid, status).then(function (result) {
            if (result.status == 400) {
                swal(result.data.Message, "", "error");
            } else if (result.status == 500) {
                swal("Internal Server Error", "", "error");
            } else if (result.status == 200) {
                if (status == true)
                    swal("Success", "Enabled User", "success");
                if (status == false)
                    swal("Success", "Disabled User", "success");
            }
        });
    }
});