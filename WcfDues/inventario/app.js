var app = angular.module("InventarioDues", ['ngRoute','ngAnimate', 'ngSanitize','ngTouch','ngResource','ui.bootstrap','ui.grid']);

app.config(function($routeProvider,$httpProvider) {
    $routeProvider
    .when("/", {
        templateUrl : "Views/Login.html",
        controller  : "LoginController"
    })
    .when("/main", {    
        templateUrl : "Views/Main.html"
    })
    .when("/inventarioactual", {
        templateUrl : "Views/InventarioActual.html",
        controller  : "InventarioActualController"
    });
});

//Variables Globales
app.value('Variables',{
    //IpServidor: '192.168.1.47:80/WcfDues'
    IpServidor: 'localhost:8080'
    //IpServidor: 'duestextil.fortiddns.com:8020/WcfDues'
});

//Modifica la referencia cirulares de las peticiones entrantes
app.factory('resourceInterceptor', function(Servicios) {
    return {
      response: function(response) {
        response.data=Servicios.parseAndResolve(JSON.stringify(response.data));
        return response;
      }
    }
  });
  app.service('Servicios', function() {
    this.parseAndResolve=function(json) {
          var refMap = {};
              return JSON.parse(json, function (key, value) {
                  if (key === '$id') { 
                      refMap[value] = this;
                      // return undefined so that the property is deleted
                      return void(0);
                  }
  
                  if (value && value.$ref) { return refMap[value.$ref]; }
  
                  return value; 
              });
      };
  });