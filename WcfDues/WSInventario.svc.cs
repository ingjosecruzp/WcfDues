using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WcfDues
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "WSInventario" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select WSInventario.svc or WSInventario.svc.cs at the Solution Explorer and start debugging.
    public class WSInventario : WsBase, IWSInventario
    {
        public detalle_inventario addItem(inventario doc)
        {
            throw new NotImplementedException();
        }

        public inventario get(string id)
        {
            throw new NotImplementedException();
        }
    }
}
