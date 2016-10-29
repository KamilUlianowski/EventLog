angular.module('mainModule', [])
    .controller('ManageController',
        function ($scope, $http) {

            var getSubjects = function () {
                return $http.get("/api/manage/GetSubjects")
                    .then(onSubjects);
            };

            var onSubjects = function (response) {
                $scope.subjects = response.data;
            }

            getSubjects();
        });