app.controller("InventarioActualController", ['$scope','$window','$location','$uibModal', function($scope,$window,$location,$uibModal) { 
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
 
    $scope.TomaInventario = function() {
        try{			
            $scope.ModalTomaInventario = $uibModal.open({
                animation: true,
                ariaLabelledBy: 'modal-title',
                ariaDescribedBy: 'modal-body',
                templateUrl: 'Views/TomasInventarios.html',
                controller: 'TomasInventariosController',
                //controllerAs: '$ctrl'
              });    
    	}
    	catch(err){
    		console.log(err);
    	}
	}
}]);