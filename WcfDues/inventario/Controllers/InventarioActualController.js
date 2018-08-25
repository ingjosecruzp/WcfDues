app.controller("InventarioActualController", ['$scope','$window','$location','$uibModal','Inventario','inventario_aplicado','uiGridGroupingConstants','$q','i18nService','$rootScope', function($scope,$window,$location,$uibModal,Inventario,inventario_aplicado,uiGridGroupingConstants,$q,i18nService,$rootScope) { 
    $scope.inventario=[];
    $scope.inventariodetallesgrid=[];
    $scope.usuariosp={};
    $rootScope.agregados=[0];
    $rootScope.original=[];

    //Definición del grid
    i18nService.setCurrentLang('es');
    $scope.gridOptions = {
        enableFiltering: true,
        enableRowSelection: true,
        enableSelectAll: true,
        selectionRowHeaderWidth: 35,
        treeRowHeaderAlwaysVisible: false,
        rowEditWaitInterval: -1,
        rowHeight: 35,
        showGridFooter:true,
        columnDefs: [
                {field: 'ItemCode', displayName:'Codigo SAP',enableCellEdit: false,grouping:{groupPriority:0}},
                {field: 'Codebars', displayName:'Codigo de Barras',enableCellEdit: false,grouping:{groupPriority:1}},
                {field: 'ItemName', displayName: 'Descripcion',enableCellEdit: false,grouping:{groupPriority:2}},
                {field: 'Cantidad', displayName: 'Cantidad',type: 'number',treeAggregationType: uiGridGroupingConstants.aggregation.SUM},
                {field: 'NombreLote', displayName: 'Lote',enableCellEdit: false},
                {field: 'usuarios.Usuario', displayName: 'Nombre del usuario',enableCellEdit: false}
        ],
        enableGridMenu: true,
        exporterMenuExcel: false,
        onRegisterApi: function(gridApi){$scope.gridApi=gridApi;}
    };

    $scope.saveRow = function( rowEntity ) {
      console.log( 'SaveRow called for: ' + rowEntity.id);
      // create a fake promise - normally you'd use the promise returned by $http or $resource
      var promise = $q.defer();
      $scope.gridApi.rowEdit.setSavePromise( $scope.gridApi.grid, rowEntity, promise.promise );
      
      // fake a delay of 3 seconds whilst the save occurs, return error if gender is "male"
      $interval( function() {
        if (rowEntity.gender === 'male' ){
          promise.reject();
        } else {
          promise.resolve();
        }
      }, 3000, 1);
    };

    $scope.TomaInventario = function() {
        try{			
            var ModalTomaInventario = $uibModal.open({
                animation: true,
                ariaLabelledBy: 'modal-title',
                ariaDescribedBy: 'modal-body',
                templateUrl: 'Views/TomasInventarios.html',
                controller: 'TomasInventariosController',
                //controllerAs: '$ctrl'
              });    

              ModalTomaInventario.result.then(function (TomasInventario) {
               
                console.log(TomasInventario);

                TomasInventario.forEach(function(inventario){
                  $rootScope.agregados.push(inventario);
                });

                Inventario.query({method:'getInventarioActualDetalles',id:TomasInventario.toString()}, function(response) {
                    //$scope.gridOptions.data=response.data;

                    response.data.forEach(function(row){
                        console.log(row);
                        $scope.gridOptions.data.push(row);
                        $rootScope.original.push(angular.copy(row));
                    });
                    
                    console.log($scope.inventariodetallesgrid);
                    $scope.inventariodetallesgrid=$scope.gridOptions.data;

                    $scope.gridApi.grouping.clearGrouping();
                    $scope.gridApi.grouping.groupColumn('ItemCode');
                    $scope.gridApi.grouping.aggregateColumn('Cantidad',uiGridGroupingConstants.aggregation.SUM);
                }, function(error) {
                    console.log(error);
                });
              }, function () {
                console.log('Modal dismissed at: ' + new Date());
              });
    	}
    	catch(err){
    		console.log(err);
        }
    }

    $scope.AplicarInventario = function() {
      $scope.diferencias=[];
      $scope.gridApi.treeBase.collapseAllRows();

      console.log($scope.agregados);
      console.log("++++++ INVENTARIO ++++++"); 

      var promise = $q(function(resolve, reject){
        $scope.gridApi.grid.getVisibleRows().forEach(function(row){
          if(row.groupHeader){
            $scope.difActual={};
            $scope.difActual.ItemCode=row.entity['$$uiGrid-0005'].groupVal;
            $scope.difActual.Contado=row.entity['$$uiGrid-0008'].value;
            //console.log(row.entity['$$uiGrid-0005'].groupVal);
            $scope.diferencias.push($scope.difActual);
            console.log($scope.difActual);
          }
        });
        console.log($scope.diferencias);

        resolve();
      });

      promise.then(function(){
        $scope.diferencias.forEach(function(dif){
            $scope.inventariodetallesgrid.forEach(function(detalle){
              if(detalle.ItemCode==dif.ItemCode){
                  dif.ItemName=detalle.ItemName;
                  dif.Codebars=detalle.Codebars;
                  dif.Cantidad=0;
                  dif.Diferencia=0;
              }
            });

            //Inicio
            Inventario.query({method:'saveDiferencias',inventario:$scope.agregados.toString(),diferencia:JSON.stringify($scope.diferencias)}, function(response) {
              console.log("Listo");
          }, function(error) {
              console.log(error);
          });
            //Fin
        });

        console.log($scope.diferencias);
      });
    }

    $scope.guardarCambios=function(){
      $scope.idCambiados=[];
      $scope.cantCambiados=[];

      var promise = $q(function(resolve, reject){
        for(x=0; x<$scope.original.length; x++){
          if($scope.original[x].Cantidad!=$scope.gridOptions.data[x].Cantidad){
            console.log("cambiando");
            $scope.idCambiados.push($scope.gridOptions.data[x].idDetalle_Inventario);
            $scope.cantCambiados.push($scope.gridOptions.data[x].Cantidad);
          }
          if(x==$scope.original.length-1) resolve();
        }
      });

      promise.then(function(){
        Inventario.query({method:'updateDetalleInventario',id:$scope.idCambiados.toString(),cantidad:$scope.cantCambiados.toString()}, function(response){
          alert("Cambios guardados");
        }, function(){
          alert("No pudieron guardarse los cambios");
        });
      });
    }
}]);