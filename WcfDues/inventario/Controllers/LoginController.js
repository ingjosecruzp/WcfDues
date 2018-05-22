app.controller("LoginController", ['$scope','$window','Usuario','$location', function($scope,$window,Usuario,$location) { 
    $scope.data = {};
    $scope.Login={};
 
    $scope.Entrar = function() {
        try{			
	        var acceso = Usuario.query({method:'Login',usuario:$scope.Login.Usuario,password: $scope.Login.Password}, function() {
				console.log("accseso");
				console.log(acceso.data);
				$location.url('main');
			
			}, function(error) {
				console.log(error);
			});
    	}
    	catch(err){
    		console.log(err);
    	}
	}
}]);