var app = angular.module('mainModule');

app.controller('ManageController',
    function ($scope, $http, WebLog) {

        $scope.items = ['subjects', 'classes', 'teachers', 'students', 'advertisements'];
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
            WebLog.getSubjects().then(function (response) {
                $scope.subjects = response.data;
            });
        };

        var getClasses = function () {
            WebLog.getClasses().then(function(response) {
                $scope.classes = response.data;
            });
        }

        var getTeachers = function () {
            WebLog.getTeachers().then(function(response) {
                $scope.teachers = response.data;
            });
        }

        var getStudents = function () {
            WebLog.getStudents().then(function (response) {
                $scope.students = response.data;
            });;
        }


        var getMainAdvertisements = function () {
            WebLog.getMainAdvertisements().then(function (response) {
                $scope.advertisements = response.data;
            });;
        }

        var onSubject = function (response) {
            $scope.subject = response.data;
        }

        $scope.sendRatingSummary = function () {
            if (confirm('Czy napewno chcesz wysłać zestawienie ocen?')) {
                WebLog.sendRatingSummary();
            }
        }

        $scope.changeActive = function (choice) { // previousChoice do usuwanie active wcześniej
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
                WebLog.deleteSubject(num);
            };

        };

        $scope.deleteClass = function (num) {


            if (confirm('Are you sure you want to delete this?')) {
                var i = searchClass(num);
                $scope.classes.splice(i, 1);
                WebLog.deleteClass(num);
            };

        };

        $scope.deleteClassFromSubject = function (subjectId, classId) {
            if (confirm('Are you sure you want to delete this?')) {
                var i = searchById($scope.subject.SchoolClasses, classId);
                $scope.subject.SchoolClasses.splice(i, 1);
                WebLog.deleteClassFromSubject(subjectId, classId);
            };
        }

        $scope.deleteTeacherFromSubject = function (subjectId, teacherId) {
            if (confirm('Are you sure you want to delete this?')) {
                var i = searchById($scope.subject.Teachers, teacherId);
                $scope.subject.Teachers.splice(i, 1);
                WebLog.deleteTeacherFromSubject(subjectId, teacherId);
            };
        }

        $scope.deleteStudentFromClass = function (classId, studentId) {
            if (confirm('Are you sure you want to delete this?')) {
                var i = searchById($scope.class.Students, studentId);
                $scope.class.Students.splice(i, 1);
                WebLog.deleteStudentFromClass(classId, studentId);
            };
        }

        $scope.deleteTeacherFromClass = function (classId) {
            if (confirm('Are you sure you want to delete this?')) {
                $scope.class.Teacher = null;
                WebLog.deleteTeacherFromClass(classId);
            };
        }

        $scope.showSubjectDetail = function (subjectId) {
            WebLog.showSubjectDetail(subjectId).then(function(response) {
                $scope.subject = response.data;
            });
        }

        $scope.showClassDetail = function (classId) {
            WebLog.showClassDetail(classId).then(function(response) {
                $scope.class = response.data;
            });
        }


        $scope.addSubject = function (name, url) {
            WebLog.addSubject(name, url).then(function(response) {
                $scope.subjects.push(response.data);
                angular.element('#myModal').modal('hide');
            });
        };

        $scope.addClass = function (name) {
            WebLog.addClass(name).then(function(response) {
                $scope.classes.push(response.data);
                angular.element('#myModal').modal('hide');
            });
        };

        $scope.addAdvertisement = function (textAdv) {
            WebLog.addAdvertisement(textAdv).then(function(response) {
                $scope.advertisements.push(response.data);
                angular.element('#myModal').modal('hide');
            });
        }

        $scope.addClassToSubject = function (subjectId, classId) {
            var i = searchById($scope.classes, classId);
            if (searchById($scope.subject.SchoolClasses, $scope.classes[i].Id) == -1) {
                $scope.subject.SchoolClasses.push($scope.classes[i]);
            }
            WebLog.addClassToSubject(subjectId, classId);
        }

        $scope.addTeacherToSubject = function (subjectId, teacherId) {
            var i = searchTeacher(teacherId);
            if (searchById($scope.subject.Teachers, $scope.teachers[i].Id) == -1) {
                $scope.subject.Teachers.push($scope.teachers[i]);
            }
            WebLog.addTeacherToSubject(subjectId, teacherId);
        }

        $scope.addStudentToClass = function (classId, studentId) {
            var i = searchById($scope.students, studentId);
            if (searchById($scope.class.Students, $scope.students[i].Id) == -1) {
                $scope.class.Students.push($scope.students[i]);
            }
            WebLog.addStudentToClass(classId, studentId);
        }


        $scope.addTeacherToClass = function (classId, teacherId) {
            var i = searchById($scope.teachers, teacherId);
            $scope.class.Teacher = $scope.teachers[i];
            WebLog.addTeacherToClass(classId, teacherId);
        }

        getSubjects();
        getClasses();
        getTeachers();
        getStudents();
        getMainAdvertisements();
    });