contactApp.config(["$routeProvider", function ($routeProvider) {
    
    $routeProvider.when("/", {
        templateUrl: "app/contactList.html",
        controller: "contactController"
    }),

    $routeProvider.when("/newcontact", {
        templateUrl: "app/contactForm.html",
    controller: "contactAddController"
    }),
    
    $routeProvider.when("/:id", {
        templateUrl: "app/contactForm.html",
    controller: "contactEditController"
    }),

    $routeProvider.otherwise({
        redirectTo: "/"
    });

}]);
