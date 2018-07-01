//Контроллер добавления контакта
contactApp.controller("contactAddController",
    ["$scope", "contactDataService", "$window",
        function categoryController($scope, contactDataService, $window) {
            $scope.contact = { Id: 0 };
            $scope.isEdit = false;

            $scope.contactCancel = function () {
                $window.location = "#/";
            };

            $scope.photoUpload = function () {
                window.alert("Создайте контакт (Сохранить), потом загружайте фотографию.");
            };

            $scope.contactSave = function () {

                if ($scope.contactForm.$invalid) return;
                contactDataService.addContact($scope.contact)
                    .then(function () {
                            $window.location = "#/";
                        },
                        function () {
                            //Ошибка        
                        });
            };
        }]);