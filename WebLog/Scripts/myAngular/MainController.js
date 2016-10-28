angular.module('mainModule', [])
    .controller('MainController',
        function ($scope, $http) {
            $scope.test = "hej ho angular";

            var getMessages = function() {
                return $http.get("/api/message/GetMessages")
                    .then(onMessages);
            };

            var onMessages = function(response) {
                $scope.messages = response.data;
            }

            getMessages();
        });