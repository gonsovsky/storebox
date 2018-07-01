//Контроллер списка контактов
contactApp.controller("contactController",
    ["$scope", "contactDataService", "$location",
    function categoryController($scope, contactDataService) {
        $scope.contacts = [];

        $scope.sortType = "Fio"; 
        $scope.sortReverse = false;

        if (getCookie("sortType") != null) {
            $scope.sortType = getCookie("sortType");
        }

        loadContactData();

        function loadContactData() {
            contactDataService.getContacts()
            .then(function () {
                $scope.contacts = contactDataService.contacts;
            },
                function () {
                    //Ошибка
                })
                .then(function () {
                    $scope.isBusy = false;
                });
        };

    }]);

//Контроллер добавления контакта
contactApp.controller("contactAddController",
    ["$scope", "contactDataService", "$window",
    function categoryController($scope, contactDataService,$window) {
        $scope.contact = {};
        $scope.isEdit = false;

        $scope.contactCancel = function () {
            $window.location = "#/";
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

//Контроллер редактирования контакта
contactApp.controller("contactEditController",
    ["$scope", "contactDataService", "$window","$routeParams","$modal",
    function categoryController($scope, contactDataService, $window,$routeParams,$modal) {
        $scope.contact = {};
        $scope.isEdit = true;

        var lFirstChange = true;

        if ($routeParams.id) {
            $scope.contact = contactDataService.findContactById($routeParams.id);
            $scope.$watchCollection("contact", function () {
                if (!lFirstChange) {
                    $("#deleteButton").hide(400);
                }
                lFirstChange = false;
            });
        }

        $scope.contactCancel = function () {
            $window.location = "#/";
        };


        $scope.contactDelete = function (size, contact) {

            var modalInstance = $modal.open({
                templateUrl: "app/contactDelete.html",
                controller: function ($scope, $modalInstance,contact) {
                    $scope.contact = contact;
                    $scope.cancel = function () {
                        $modalInstance.dismiss("cancel");
                    };

                    $scope.ok = function (contact) {
                        contactDataService.deleteContact(contact.Id)
                        .then(function () {
                            $window.location = "#/";
                            $modalInstance.close(contact);
                        },
                        function () {
                            //Ошибка        
                        });
                    };
                },
                size: size,
                resolve: {
                    contact: function () {
                        return contact;
                    }
                }
            });
        };

        $scope.contactSave = function () {
            if ($scope.contactForm.$invalid) return;
            contactDataService.updateContact($scope.contact)
            .then(function () {
                
                $window.location = "#/";
            },
            function () {
                //Error        
            });

        };
        }]);


function setCookie(name, value) {
    var expires = "";
    var date = new Date();
    date.setTime(date.getTime() + (1 * 24 * 60 * 60 * 1000));
    expires = "; expires=" + date.toUTCString();
    document.cookie = name + "=" + (value || "") + expires + "; path=/";
}

function getCookie(name) {
    var nameEq = name + "=";
    var ca = document.cookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) === " ") c = c.substring(1, c.length);
        if (c.indexOf(nameEq) === 0) return c.substring(nameEq.length, c.length);
    }
    return null;
}
