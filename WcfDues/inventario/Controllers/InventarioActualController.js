app.controller("InventarioActualController", ['$scope','$window','$location','$uibModal','Inventario','inventario_aplicado','uiGridGroupingConstants','$q','i18nService', function($scope,$window,$location,$uibModal,Inventario,inventario_aplicado,uiGridGroupingConstants,$q,i18nService) { 
    $scope.inventario=[];
    $scope.inventariodetallesgrid=[];
    $scope.usuariosp={};

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
                {field: 'ItemCode', displayName:'Codigo SAP',enableCellEdit: false},
                {field: 'Codebars', displayName:'Codigo de Barras',enableCellEdit: false},
                {field: 'ItemName', displayName: 'Descripcion',enableCellEdit: false},
                {field: 'Cantidad', displayName: 'Cantidad',type: 'number',treeAggregationType: uiGridGroupingConstants.aggregation.SUM},
                {field: 'NombreLote', displayName: 'Lote',enableCellEdit: false},
                {field: 'usuarios.Usuario', displayName: 'Nombre del usuario',enableCellEdit: false}
        ],
        enableGridMenu: true,
        exporterMenuExcel: false,
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
                Inventario.query({method:'getInventarioActualDetalles',id:TomasInventario.toString()}, function(response) {
                    $scope.gridOptions.data=response.data;
                    $scope.inventariodetallesgrid=$scope.gridOptions.data;
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
        try{

                $scope.inventariodetallesgrid.forEach(function(record){
                      record.usuarios=null;
                });

                var newInventario = new inventario_aplicado({
                    idInventario:0,
                    UsuarioId   :0,
                    detalle_inventario_aplicado :$scope.gridOptions.data});
                    newInventario.$save();

                  //  $scope.gridOptions.data.length=0;
        }
    	catch(err){
            console.log(err);
        }
    }
}]);