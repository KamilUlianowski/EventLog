angular.module('mainModule', [])
    .controller('ManageController',
        function ($scope, $http) {

            $scope.items = ['subjects', 'classes', 'teachers', 'students'];
            $scope.selection = $scope.items[1];
            var previousChoice;

            var searchSubject = function (id) {
                for (var i = 0; i < $scope.subjects.length; i++) {
                    if ($scope.subjects[i].Id === id)
                        return i;
                }
            }

            var searchClass = function (id) {
                for (var i = 0; i < $scope.classes.length; i++) {
                    if ($scope.classes[i].Id === id)
                        return i;
                }
            }

            var getSubjects = function () {
                return $http.get("/api/manage/GetSubjects")
                    .then(onSubjects);
            };

            var getClasses = function() {
                return $http.get("/api/manage/GetClasses")
                  .then(onClasses);
            }

            var getTeachers = function () {
                return $http.get("/api/manage/GetTeachers")
                  .then(onTeachers);
            }

            var getStudents = function () {
                return $http.get("/api/manage/GetStudents")
                  .then(onStudents);
            }


            var onSubjects = function (response) {
                $scope.subjects = response.data;
            };

            var onClasses = function(response) {
                $scope.classes = response.data;
            }

            var onTeachers = function (response) {
                $scope.teachers = response.data;
            };

            var onStudents = function (response) {
                $scope.students = response.data;
            };

            $scope.changeActive = function (choice) {
                if (previousChoice)
                    previousChoice.removeClass('active');
                $scope.selection = $scope.items[choice];
                var myEl = angular.element(document.querySelector('#choice' + choice));
                myEl.addClass('active');
                previousChoice = myEl;

            }

            $scope.deleteSubject = function (num) {


                if (confirm('Are you sure you want to delete this?')) {
                    var i = searchSubject(num);
                    if (i) {
                        $scope.subjects.splice(i, 1);
                    }
                    return $http.post("/api/manage/deletesubject", num);
                };

            };

            $scope.deleteClass = function (num) {


                if (confirm('Are you sure you want to delete this?')) {
                    var i = searchClass(num);
                    if (i) {
                        $scope.classes.splice(i, 1);
                    }
                    return $http.post("/api/manage/DeleteClass", num);
                };

            };


            $scope.addSubject = function (name, url) {
                $scope.subjects.push({ Name: name, ImagePath: url });
                $http.post("/api/manage/addSubject", JSON.stringify({ Name: name, Url: url })).then(closeModal);
            };

            $scope.addClass = function (name) {
                $scope.classes.push({ Name: name });
                $http.post("/api/manage/addClass?name=" + name).then(closeModal);
            };


            var closeModal = function () {
                angular.element('#myModal').modal('hide');
            }

            getSubjects();
            getClasses();
            getTeachers();
            getStudents();
        });