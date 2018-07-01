//Контроллер списка контактов
contactApp.controller("contactController",
    ["$scope", "contactDataService", "$location", "$modal", "$window","$rootScope",
        function categoryController($scope, contactDataService, $location, $modal, $window, $rootScope) {
        $scope.contacts = [];

        $scope.sortType = "Fio"; 
        $scope.sortReverse = false;

        if (getCookie("sortType") != null)
            $scope.sortType = getCookie("sortType");
        if (getCookie("sortReverse") != null)
            $scope.sortReverse = getCookie("sortReverse");

        loadContactData(false);

        function loadContactData(aRefresh) {
            contactDataService.getContacts()
                .then(function ()
                    {
                        $scope.contacts = contactDataService.contacts;
                    },
                function () {
                    //Ошибка
                })
                .then(function () {
                    $scope.isBusy = false;
                });
        };

        $scope.SetSort = function (aSortType, aSortReverse) {
            $scope.sortType = aSortType;
            $scope.sortReverse = aSortReverse;
            setCookie("sortType", $scope.sortType);
            setCookie("sortReverse", $scope.sortReverse);
        };

        $scope.contactDel = function (id) {
            var contact = contactDataService.findContactById(id);
            var size = 'md';
            var modalInstance = $modal.open({
             templateUrl: 'app/contactDelete.html',
             controller: function($scope, $modalInstance, contact) {
                 $scope.contact = contact;
                 $scope.cancel = function() {
                     $modalInstance.dismiss('cancel');
                 };

                 $scope.ok = function(contact) {
                     contactDataService.deleteContact(contact.Id)
                         .then(function() {
                             $window.location = "#/";
                                 loadContactData();
                                 $modalInstance.close(contact);
                             },
                             function() {
                                 //Error        
                             });
                 };
             },
             size: size,
             resolve: {
                 contact: function() {
                     return contact;
                 }
             }
         });
        };

        //Получено сообщение от UpdateController
        if (!$rootScope.$$listeners['contact-updated'])
            $rootScope.$on('contact-updated', onUpdate);
        function onUpdate(event, msg) {
            loadContactData(true);
        };

        //Получено сообщение от EditController (на удаление)
        if (!$rootScope.$$listeners['contact-delete'])
            $rootScope.$on('contact-delete', onDelete);
        function onDelete(event, msg) {
            $scope.contactDel(msg.Id);
         };

        $scope.$on('$destroy', function () {
            $rootScope.$on('contact-delete', function () { });
            $rootScope.$on('contact-updated', function () { });
        });
}]);

