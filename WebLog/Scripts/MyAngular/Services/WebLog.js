angular.module('mainModule').factory('WebLog', function ($http) {

    var getSubjects = function () {
        return $http.get("/api/manage/GetSubjects");
    };

    var getClasses = function () {
        return $http.get("/api/manage/GetClasses");
    }

    var getTeachers = function () {
        return $http.get("/api/manage/GetTeachers");
    }

    var getStudents = function () {
        return $http.get("/api/manage/GetStudents");
    }

    var getMainAdvertisements = function () {
        return $http.get("/api/manage/GetMainAdvertisements");
    }

    var sendRatingSummary = function () {
        return $http.post("/api/manage/SendRatingSummary");
    }

    var deleteSubject = function (id) {
        return $http.post("/api/manage/deletesubject", id);
    }

    var deleteClassFromSubject = function (subjectId, classId) {
        return $http.post("/api/manage/UpdateClassesSubject", JSON.stringify({ SubjectId: subjectId, ClassId: classId }));
    };


    var deleteTeacherFromSubject = function (subjectId, teacherId) {
        return $http.post("/api/manage/UpdateTeacherSubject",
            JSON.stringify({ SubjectId: subjectId, TeacherId: teacherId }));
    };

    var deleteStudentFromClass = function (classId, studentId) {
        return $http.post("/api/manage/DeleteStudentFromClass", JSON.stringify({ ClassId: classId, StudentId: studentId }));
    };

    var deleteTeacherFromClass = function (classId) {
        return $http.post("/api/manage/DeleteTeacherFromClass", JSON.stringify({ ClassId: classId }));
    };

    var showSubjectDetail = function (subjectId) {
        return $http.get("/api/manage/SubjectDetail?subjectId=" + subjectId);
    }

    var showClassDetail = function (classId) {
        return $http.get("/api/manage/ClassDetail?classId=" + classId);
    }


    var addSubject = function (name, url) {
        return $http.post("/api/manage/addSubject", JSON.stringify({ Name: name, Url: url }));
    };

    var addClass = function (name) {
        return $http.post("/api/manage/addClass?name=" + name);
    };

    var addAdvertisement = function (textAdv) {
        return $http.post("/api/manage/AddMainAdvertisement", JSON.stringify({ Text: textAdv }));
    }

    var addClassToSubject = function (subjectId, classId) {
        return $http.post("/api/manage/UpdateClassesSubject", JSON.stringify({ SubjectId: subjectId, ClassId: classId }));
    }

    var addTeacherToSubject = function (subjectId, teacherId) {
        return $http.post("/api/manage/UpdateTeacherSubject", JSON.stringify({ SubjectId: subjectId, TeacherId: teacherId }));
    }

    var addStudentToClass = function (classId, studentId) {
        return $http.post("/api/manage/AddStudentToClass", JSON.stringify({ ClassId: classId, StudentId: studentId }));
    }


    var addTeacherToClass = function (classId, teacherId) {
        return $http.post("/api/manage/AddTeacherToClass", JSON.stringify({ ClassId: classId, TeacherId: teacherId }));
    }




    return {
        getSubjects: getSubjects,
        getClasses: getClasses,
        getTeachers: getTeachers,
        getStudents: getStudents,
        getMainAdvertisements: getMainAdvertisements,
        sendRatingSummary: sendRatingSummary,
        deleteSubject: deleteSubject,
        deleteClassFromSubject: deleteClassFromSubject,
        deleteTeacherFromSubject: deleteTeacherFromSubject,
        deleteStudentFromClass: deleteStudentFromClass,
        deleteTeacherFromClass: deleteTeacherFromClass,
        showSubjectDetail: showSubjectDetail,
        showClassDetail: showClassDetail,
        addSubject: addSubject,
        addClass: addClass,
        addAdvertisement: addAdvertisement,
        addClassToSubject: addClassToSubject,
        addTeacherToSubject: addTeacherToSubject,
        addStudentToClass: addStudentToClass,
        addTeacherToClass: addTeacherToClass


    };
})