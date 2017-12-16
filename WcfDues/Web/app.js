var app=angular.module("myApp",["ngRoute"]);

app.config(function($routeProvider){
	$routeProvider
	.when("/clientes",{
		templateUrl: "Views/clientes.html",
		controller : "ClientesControllers"
	})
	.when("/usuarios",{
		templateUrl : "Views/usuarios.html",
		controller  : "UsuariosControllers"
	})
});
