angular.module('mainModule').controller('MessageController',
        function ($scope, $http, WebLog) {

            var currentPerson = "";

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
                $scope.myMessages.push({ m_Item1: $scope.myMessages[$scope.myMessages.length - 1].m_Item1 + 1000, m_Item2: message });
                WebLog.sendMessage(message, currentPerson.m_Item2);
            }

            $scope.sendNewMessage = function (message) {
                WebLog.sendNewMessage(message, $scope.selected).then(function(response) {
                    $scope.messages = response.data;
                });
            }

            getMessages();
            getTeachers();
        });