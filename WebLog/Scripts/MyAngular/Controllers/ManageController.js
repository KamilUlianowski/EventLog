var app = angular.module('mainModule');

app.controller('ManageController',
    function ($scope, $http, WebLog) {

        $scope.items = ['subjects', 'classes', 'teachers', 'students', 'advertisements'];
        $scope.selection = $scope.items[1];
        $scope.selectedClass;
        $scope.selectedTeacher;
        $scope.selectedStudent;
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

        var updateSubjectDetails = function (subject) {
            var classesWithout = $scope.classes.slice(0);
            var teacherWithout = $scope.teachers.slice(0);

            for (var i = 0; i < subject.SchoolClasses.length; i++) {
                position = searchById(classesWithout, subject.SchoolClasses[i].Id);
                if (position != -1) {
                    classesWithout.splice(position, 1);
                }
            }

            for (var i = 0; i < subject.Teachers.length; i++) {
                position2 = searchById(teacherWithout, subject.Teachers[i].Id);
                if (position2 != -2) {
                    teacherWithout.splice(position2, 1);
                }
            }

            $scope.otherClasses = classesWithout;
            $scope.otherTeachers = teacherWithout;

        }

        var updateClassDetails = function (schoolClass) {
            var restOfStudents = $scope.students.slice(0);
            var restOfTeachers = $scope.teachers.slice(0);


            for (var i = 0; i < schoolClass.Students.length; i++) {
                position = searchById(restOfStudents, schoolClass.Students[i].Id);
                if (position != -1) {
                    restOfStudents.splice(position, 1);
                }
            }

            if (schoolClass.Teacher != undefined)  {
                position2 = searchById(restOfTeachers, schoolClass.Teacher.Id);
                if (position2 != -2) {
                    restOfTeachers.splice(position2, 1);
                }
            }

            $scope.restOfStudents = restOfStudents;
            $scope.restOfTeachers = restOfTeachers;
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
            WebLog.getClasses().then(function (response) {
                $scope.classes = response.data;
            });
        }

        var getTeachers = function () {
            WebLog.getTeachers().then(function (response) {
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
                WebLog.deleteClassFromSubject(subjectId, classId).then(function (response) {
                    $scope.subject = response.data;
                    updateSubjectDetails($scope.subject);
                });
            };
        }

        $scope.deleteTeacherFromSubject =
            function (subjectId, teacherId) {
                if (confirm('Are you sure you want to delete this?')) {
                    WebLog.deleteTeacherFromSubject(subjectId, teacherId).then(function (response) {
                        $scope.subject = response.data;
                        updateSubjectDetails($scope.subject);
                    });;
                };
            }

        $scope.deleteStudentFromClass = function (classId, studentId) {
            if (confirm('Are you sure you want to delete this?')) {
                WebLog.deleteStudentFromClass(classId, studentId).then(function (response) {
                    $scope.class = response.data;
                    updateClassDetails($scope.class);
                });;
            };
        }

        $scope.deleteTeacherFromClass = function (classId) {
            if (confirm('Are you sure you want to delete this?')) {
                WebLog.deleteTeacherFromClass(classId).then(function (response) {
                    $scope.class = response.data;
                    updateClassDetails($scope.class);
                });;
            };
        }

        $scope.showSubjectDetail = function (subjectId) {
            WebLog.showSubjectDetail(subjectId).then(function (response) {
                $scope.subject = response.data;
                updateSubjectDetails($scope.subject);
            });
        }

        $scope.showClassDetail = function (classId) {
            WebLog.showClassDetail(classId).then(function (response) {
                $scope.class = response.data;
                updateClassDetails($scope.class);
            });
        }


        $scope.addSubject = function (name, url) {
            WebLog.addSubject(name, url).then(function (response) {
                $scope.subjects.push(response.data);
                angular.element('#myModal').modal('hide');
            });
        };

        $scope.addClass = function (name) {
            WebLog.addClass(name).then(function (response) {
                $scope.classes.push(response.data);
                angular.element('#myModal').modal('hide');
            });
        };

        $scope.addAdvertisement = function (titleAdv, textAdv) {
            WebLog.addAdvertisement(titleAdv, textAdv).then(function (response) {
                $scope.advertisements.push(response.data);
                angular.element('#myModal').modal('hide');
            });
        }

        $scope.addClassToSubject = function (subjectId, classId) {
            var i = searchById($scope.classes, classId);
            if (searchById($scope.subject.SchoolClasses, $scope.classes[i].Id) == -1) {
                WebLog.addClassToSubject(subjectId, classId).then(function (response) {
                    $scope.subject = response.data;
                    updateSubjectDetails($scope.subject);
                });;
            }

        }

        $scope.addTeacherToSubject = function (subjectId, teacherId) {
            var i = searchTeacher(teacherId);
            if (searchById($scope.subject.Teachers, $scope.teachers[i].Id) == -1) {
                WebLog.addTeacherToSubject(subjectId, teacherId).then(function (response) {
                    $scope.subject = response.data;
                    updateSubjectDetails($scope.subject);
                });
            }
        }

        $scope.addStudentToClass = function (classId, studentId) {
            var i = searchById($scope.students, studentId);
            if (searchById($scope.class.Students, $scope.students[i].Id) == -1) {
                WebLog.addStudentToClass(classId, studentId).then(function (response) {
                    $scope.class = response.data;
                    updateClassDetails($scope.class);
                });
            }
        }


        $scope.addTeacherToClass = function (classId, teacherId) {
            WebLog.addTeacherToClass(classId, teacherId).then(function (response) {
                $scope.class = response.data;
                updateClassDetails($scope.class);
            });
        }

        getSubjects();
        getClasses();
        getTeachers();
        getStudents();
        getMainAdvertisements();
    });