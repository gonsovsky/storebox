
//Контроллер редактирования контакта
contactApp.controller("contactEditController",
    ["$scope", "contactDataService", "$window", "$routeParams", "$modal", "$rootScope",
        function categoryController($scope, contactDataService, $window, $routeParams, $modal, $rootScope) {
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

            $scope.photoUpload = function () {
                var id = document.getElementById("contactIdDiv").innerText;
                var fileUpload = $("#FileUpload1").get(0);
                var files = fileUpload.files;
                var test = new FormData();
                test.append(files[0].name, files[0]);
                $.ajax({
                    url: "contact/UploadPhoto/" + id,
                    type: "POST",
                    contentType: false,
                    processData: false,
                    data: test,
                    // dataType: "json",
                    success: function (result) {
                        refreshImageById("imagePhoto");
                        $rootScope.$emit('contact-updated', { Id: id });
                        alert(result);
                    },
                    error: function (err) {
                        alert(err.statusText);
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
                            //Ошибка
                        });

            };

            $scope.contactDelPromt = function (id) {
                $rootScope.$emit('contact-delete', { Id: id });
            };

        }]);
