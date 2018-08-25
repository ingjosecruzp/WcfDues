app.controller("TomasInventariosController", ['$scope','$window','Inventario','GridInventario','$uibModalInstance','$rootScope', function($scope,$window,Inventario,GridInventario,$uibModalInstance,$rootScope) { 
    var data=[];
    var data2=[];
    var inventarioseleccionados=[];
    $scope.gridOptions = {
        enableRowSelection: true,
        enableSelectAll: true,
        selectionRowHeaderWidth: 35,
        rowHeight: 35,
        showGridFooter:true,
        columnDefs: [
                {field: 'idInventario', displayName:'Id'},
                {field: 'FechaInicio', displayName: 'Fecha Inicio'},
                {field: 'Usuario', displayName: 'Usuario'},
                {field: 'UUID', displayName: 'UUID'}
        ],
        onRegisterApi: function (gridApi) {
            $scope.gridOptions.gridApi = gridApi;
            gridApi.selection.on.rowSelectionChanged($scope, function (row) { //evento que cacha la fila seleccionada
                if(row.isSelected){
                    console.log("Entro");
                   
                    inventarioseleccionados.push(row.entity.idInventario);
                   }
            });
        }
          
      };

    
    Inventario.query({method:'getInventarioActual',id:$rootScope.agregados.toString()}, function(response) { 
        
        console.log(response.data)
        $scope.gridOptions.data=response.data;
    }, function(error) {
        console.log(error);
    });

    $scope.TomaInventario = function(){
       $uibModalInstance.close(inventarioseleccionados);
    };
    $scope.Cancel=function(){
        $uibModalInstance.dismiss('cancel');
    }
}]);