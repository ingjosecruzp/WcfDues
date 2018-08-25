app.controller("DiferenciasController", ['$scope','$window','$location','$uibModal','Inventario','inventario_aplicado','uiGridGroupingConstants','$q','i18nService','$rootScope', function($scope,$window,$location,$uibModal,Inventario,inventario_aplicado,uiGridGroupingConstants,$q,i18nService,$rootScope) { 
    $scope.inventario=[];
    $scope.inventariodetallesgrid=[];

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
                {field: 'ItemCode', displayName:'Codigo SAP',enableCellEdit: false, grouping:{groupPriority:0}},
                {field: 'Codebars', displayName:'Codigo de Barras',enableCellEdit: false},
                {field: 'ItemName', displayName: 'Descripcion',enableCellEdit: false},
                {field: 'Contado', displayName: 'Contado',type: 'number',treeAggregationType: uiGridGroupingConstants.aggregation.SUM},
                {field: 'Cantidad', displayName: 'Cantidad',enableCellEdit: false},
                {field: 'Diferencias', displayName: 'Diferencias', enableCellEdit: false}
        ],
        enableGridMenu: true,
        exporterMenuExcel: false,
        onRegisterApi: function(gridApi){$scope.gridApi=gridApi;}
    };
}]);