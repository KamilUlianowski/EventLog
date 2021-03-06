﻿//$.fn.datepicker.defaults.format = 'mm/dd/yyyy';

function resetForm() {
    $("#adv-form")[0].reset();
    $("#returnMessage").text("@Global.MessageSent");

}
function GetSubjectDetail(id) {
    $.ajax({
        url: "/Student/StudentSubjectDetail",
        data: { "subjectId": id },
        type: 'GET',

        success: function (data) {
            $('#student-account-content').html(data);
        }
    });
}

function GetTestResult(id) {
    $.ajax({
        url: "/Test/SolveTest",
        data: { "testId": id },
        type: 'GET',

        success: function (data) {
            $('#student-account-content').html(data);
        }
    });
}

function onLoadEnd() {
    $('#myModal').modal('hide');
    $('body').removeClass('modal-open');
    $('.modal-backdrop').remove();
}

function addGrade(id) {
    var selectedGrade = $("#student" + id).val();
    $("#grades" + id).append(selectedGrade + "  ");
};

var closeModal = function () {
    $('#myModal').modal('hide');
}

$(function () {

    function addGrade(id) {
        var selectedGrade = $("#student" + id).val();
        $("#grades" + id).append(selectedGrade + "  ");
    }

    var form = $(".login-form");

    form.css({
        opacity: 1,
        "-webkit-transform": "scale(1)",
        "transform": "scale(1)",
        "-webkit-transition": ".5s",
        "transition": ".5s"
    });

    var signUpForm = $(".register-form");

    signUpForm.css({
        opacity: 1,
        "-webkit-transform": "scale(1)",
        "transform": "scale(1)",
        "-webkit-transition": ".5s",
        "transition": ".5s"
    });
});

