angular.module('mainModule', [])
    .controller('MainController',
        function ($scope, $http) {
            $scope.test = "hej ho angular";
            var currentPerson = "";

            var getPerson = function (user) {
                for (var i = 0; i < $scope.messages.length; i++) {
                    if ($scope.messages[i].m_Item1 === user) {
                        return $scope.messages[i];
                    }
                }
            }

            var getMessages = function () {
                return $http.get("/api/message/GetMessages")
                    .then(onMessages);
            };

            var onMessages = function (response) {
                $scope.messages = response.data;
            }

            $scope.showMessages = function (person) {
                currentPerson = getPerson(person);
                $scope.myMessages = currentPerson.m_Item3;
            }

            $scope.sendMessage = function (message) {
                $scope.myMessages.push(message);
                $http.post("/api/message/sendMessage", JSON.stringify({ Email: currentPerson.m_Item2, Text: message }));
            }

            getMessages();
        });