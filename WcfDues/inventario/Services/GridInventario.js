app.service('GridInventario', function($q,Inventario) {
    var variable=1;
    this.CodigoPaquete=function() {
       myData1 = [
            {
                firstName: "Eduardo",
                lastName: "Guerra",
                company: "Bobadilla",
                employed: true
            },
            {
                firstName: "Eduardo1",
                lastName: "Guerra1",
                company: "Bobadilla1",
                employed: true
            }
     
        ];
        console.log("entro al servicio");
        return myData1;
    }

  });