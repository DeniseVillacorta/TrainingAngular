var app = angular.module('userApp');

app.controller('signinCtrl', function ($scope, services,$window) {

    $scope.signin = function () {
        var data = {
            "username": $scope.username,
            "password": $scope.password
        };

        services.signin(data).then(function (result) {

            console.log(result.data.Token);
            if (result.status == 200) {
                // set the token once successful sign in
                localStorage.setItem('token', result.data);
                swal({
                    title: "Success",
                    icon: "success",
                    closeOnClickOutside: false
                }).then(function () {
                    $window.location.href = "../users/index.html";
                });
            }
            else if (result.status == 400) {
                /*   swal(result.data.Message, "", "error");*/
            }
        });
    }
});