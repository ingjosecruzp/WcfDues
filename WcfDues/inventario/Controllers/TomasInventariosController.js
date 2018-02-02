app.controller("TomasInventariosController", ['$scope','$window', function($scope,$window) { 
    $scope.myData = [
        {
            firstName: "Cox",
            lastName: "Carney",
            company: "Enormo",
            employed: true
        },
        {
            firstName: "Lorraine",
            lastName: "Wise",
            company: "Comveyer",
            employed: false
        },
        {
            firstName: "Nancy",
            lastName: "Waters",
            company: "Fuelton",
            employed: false
        }
    ];

    $scope.TomaInventario = function(){
    	alert("Test");
    };

}]);