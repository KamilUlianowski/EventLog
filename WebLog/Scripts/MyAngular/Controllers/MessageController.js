angular.module('mainModule').controller('MessageController',
        function ($scope, $http, WebLog) {

            var currentPerson = "";
            $scope.formInputs = {}

            var getPerson = function (user) {
                for (var i = 0; i < $scope.messages.length; i++) {
                    if ($scope.messages[i].m_Item1 === user) {
                        return $scope.messages[i];
                    }
                }
            }

            var getMessages = function () {
                WebLog.getMessages().then(function (response) {
                    $scope.messages = response.data;
                    if (currentPerson != "") {
                        $scope.showMessages(currentPerson.m_Item1);
                    }
                });
            };


            var getTeachers = function () {
                WebLog.getTeachers().then(function (response) {
                    $scope.teachers = response.data;
                    $scope.selected = $scope.teachers[0].Email;
                });
            }

            $scope.changeTeacher = function (teacherId) {
                $scope.selected = teacherId;
                $scope.myMessages = null;
            }

            $scope.showMessages = function (person) {
                currentPerson = getPerson(person);
                $scope.myMessages = currentPerson.m_Item3;
                $scope.selected = 0;
            }

            $scope.sendMessage = function (message) {
                WebLog.sendMessage(message, currentPerson.m_Item2).then(function (response) {
                    getMessages();
                });
            }

            $scope.sendNewMessage = function (message) {
                WebLog.sendNewMessage(message, $scope.selected).then(function(response) {
                    getMessages();

                });
            }

            getMessages();
            getTeachers();
        });