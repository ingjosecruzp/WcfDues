var app = angular.module("InventarioDues", ['ngRoute','ngAnimate', 'ngSanitize','ngTouch','ngResource','ui.bootstrap','ui.grid', 'ui.grid.selection', 'ui.grid.cellNav','ui.grid.grouping', 'ui.grid.edit', 'ui.grid.rowEdit','ui.grid.exporter']);

app.config(function($routeProvider,$httpProvider) {
    //Intercepta las petciones realizadas por $resource
    $httpProvider.interceptors.push(function($q) {
        return {
          'responseError': function(response) {
            if (response.status == 400) {
             
            }
            if (response.status == 500) {
              //console.log(response.headers("Error")); 
            }
            // Always reject (or resolve) the deferred you're given
            return $q.reject(response);
          },
          // optional method
          'request': function(config) {
            // do something on success
            //console.log(config);
            return config;
          }
        };
      });

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
    //IpServidor: '200.52.220.238:8082'
});

//Modifica la referencia cirulares de las peticiones entrantes
app.factory('resourceInterceptor', function(Servicios) {
    return {
      response: function(response) {
        console.log("referencia");
        console.log(response);
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
      this.convertToJSONDate=function(strDate){
        var dt = new Date(strDate);
        var newDate = new Date(Date.UTC(dt.getFullYear(), dt.getMonth(), dt.getDate(), dt.getHours(), dt.getMinutes(), dt.getSeconds(), dt.getMilliseconds()));
        return '/Date(' + newDate.getTime() + ')/';
      };
  });