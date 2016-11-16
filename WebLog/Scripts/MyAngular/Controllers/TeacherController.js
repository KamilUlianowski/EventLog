var app = angular.module('mainModule');

    app.controller('TeacherController',
        function ($scope, $http, WebLog) {

            var getSchoolGrades = function (subjectId, schoolClassId) {
                WebLog.getSchoolGrades(subjectId, schoolClassId).then(function(response) {
                    $scope.grades = response.data();
                    });
            }

            $scope.init = function (subId, classId) {
                $scope.subjectId = subId;
                $scope.schoolClassId = classId;

                getSchoolGrades(subId, classId);
            };

        });