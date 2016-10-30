angular.module('mainModule', [])
    .controller('ManageController',
        function ($scope, $http) {

            $scope.items = ['subjects', 'classes', 'teachers', 'students'];
            $scope.selection = $scope.items[1];
            var previousChoice;

            var searchById = function (arr, id) {
                for (var i = 0; i < arr.length; i++) {
                    if (arr[i].Id === id)
                        return i;
                }
                return -1;
            }

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

            var searchTeacher = function (id) {
                for (var i = 0; i < $scope.teachers.length; i++) {
                    if ($scope.teachers[i].Id === id)
                        return i;
                }
            }

            var getSubjects = function () {
                return $http.get("/api/manage/GetSubjects")
                    .then(onSubjects);
            };

            var getClasses = function () {
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

            var onClasses = function (response) {
                $scope.classes = response.data;
            }

            var onTeachers = function (response) {
                $scope.teachers = response.data;
            };

            var onStudents = function (response) {
                $scope.students = response.data;
            };

            var onSubject = function (response) {
                $scope.subject = response.data;
            }

            var onClass = function (response) {
                $scope.class = response.data;
            }

            $scope.changeActive = function (choice) {
                if (previousChoice)
                    previousChoice.removeClass('active');
                $scope.selection = $scope.items[choice];
                var myEl = angular.element(document.querySelector('#choice' + choice));
                myEl.addClass('active');
                $scope.subject = null;
                $scope.class = null;
                previousChoice = myEl;

            }

            $scope.deleteSubject = function (num) {


                if (confirm('Are you sure you want to delete this?')) {
                    var i = searchSubject(num);
                    $scope.subjects.splice(i, 1);
                    return $http.post("/api/manage/deletesubject", num);
                };

            };

            $scope.deleteClass = function (num) {


                if (confirm('Are you sure you want to delete this?')) {
                    var i = searchClass(num);
                    $scope.classes.splice(i, 1);
                    return $http.post("/api/manage/DeleteClass", num);
                };

            };

            $scope.deleteClassFromSubject = function (subjectId, classId) {
                if (confirm('Are you sure you want to delete this?')) {
                    var i = searchById($scope.subject.SchoolClasses, classId);
                    $scope.subject.SchoolClasses.splice(i, 1);
                    return $http.post("/api/manage/UpdateClassesSubject", JSON.stringify({ SubjectId: subjectId, ClassId: classId }));
                };
            }

            $scope.deleteTeacherFromSubject = function (subjectId, teacherId) {
                if (confirm('Are you sure you want to delete this?')) {
                    var i = searchById($scope.subject.Teachers, teacherId);
                    $scope.subject.Teachers.splice(i, 1);
                    return $http.post("/api/manage/UpdateTeacherSubject", JSON.stringify({ SubjectId: subjectId, TeacherId: teacherId }));
                };
            }

            $scope.deleteStudentFromClass = function (classId, studentId) {
                if (confirm('Are you sure you want to delete this?')) {
                    var i = searchById($scope.class.Students, studentId);
                    $scope.class.Students.splice(i, 1);
                    return $http.post("/api/manage/DeleteStudentFromClass", JSON.stringify({ ClassId: classId, StudentId: studentId }));
                };
            }

            $scope.deleteTeacherFromClass = function (classId) {
                if (confirm('Are you sure you want to delete this?')) {
                    $scope.class.Teacher = null;
                    return $http.post("/api/manage/DeleteTeacherFromClass", JSON.stringify({ ClassId: classId }));
                };
            }

            $scope.showSubjectDetail = function (subjectId) {
                return $http.get("/api/manage/SubjectDetail?subjectId=" + subjectId)
                  .then(onSubject);
            }

            $scope.showClassDetail = function (classId) {
                return $http.get("/api/manage/ClassDetail?classId=" + classId)
                  .then(onClass);
            }


            $scope.addSubject = function (name, url) {
                $http.post("/api/manage/addSubject", JSON.stringify({ Name: name, Url: url })).then(addSubjectResponse);
            };

            $scope.addClass = function (name) {
                $http.post("/api/manage/addClass?name=" + name).then(addClassResponse);
            };

            $scope.addClassToSubject = function (subjectId, classId) {
                var i = searchById($scope.classes, classId);
                if (searchById($scope.subject.SchoolClasses, $scope.classes[i].Id) == -1) {
                    $scope.subject.SchoolClasses.push($scope.classes[i]);
                }

                return $http.post("/api/manage/UpdateClassesSubject", JSON.stringify({ SubjectId: subjectId, ClassId: classId }));
            }

            $scope.addTeacherToSubject = function (subjectId, teacherId) {
                var i = searchTeacher(teacherId);
                if (searchById($scope.subject.Teachers, $scope.teachers[i].Id) == -1) {
                    $scope.subject.Teachers.push($scope.teachers[i]);
                }
                return $http.post("/api/manage/UpdateTeacherSubject", JSON.stringify({ SubjectId: subjectId, TeacherId: teacherId }));
            }

            $scope.addStudentToClass = function (classId, studentId) {
                var i = searchById($scope.students, studentId);
                if (searchById($scope.class.Students, $scope.students[i].Id) == -1) {
                    $scope.class.Students.push($scope.students[i]);
                }

                return $http.post("/api/manage/AddStudentToClass", JSON.stringify({ ClassId: classId, StudentId: studentId }));
            }


            $scope.addTeacherToClass = function (classId, teacherId) {
                var i = searchById($scope.teachers, teacherId);
                $scope.class.Teacher = $scope.teachers[i];

                return $http.post("/api/manage/AddTeacherToClass", JSON.stringify({ ClassId: classId, TeacherId: teacherId }));
            }


            var addClassResponse = function (response) {
                $scope.classes.push(response.data);
                angular.element('#myModal').modal('hide');
            }

            var addSubjectResponse = function (response) {
                $scope.subjects.push(response.data);
                angular.element('#myModal').modal('hide');
            }

            getSubjects();
            getClasses();
            getTeachers();
            getStudents();
        });