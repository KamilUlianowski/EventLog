var app = angular.module('mainModule');

    app.controller('TeacherController',
        function ($scope, $http) {


            var saveGrades = function (response) {
                $scope.grades = response.data();
            }

            var getSchoolGrades = function (subjectId, schoolClassId) {
                return $http.get("/api/teacher/GetSchoolGrades", { SubjectId: subjectId, SchoolClassId: schoolClassId })
                    .then(saveGrades);
            }

            $scope.init = function (subId, classId) {
                $scope.subjectId = subId;
                $scope.schoolClassId = classId;

                getSchoolGrades(subId, classId);
            };


        });