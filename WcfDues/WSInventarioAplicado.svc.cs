using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfDues
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "WSInventarioAplicado" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select WSInventarioAplicado.svc or WSInventarioAplicado.svc.cs at the Solution Explorer and start debugging.
    public class WSInventarioAplicado : WsBase, IWSInventarioAplicado
    {
        public inventario_aplicado addItem(inventario_aplicado doc)
        {
            try
            {
                inventario_aplicado documento = new inventario_aplicado();
                detalle_inventario_aplicado detalle = new detalle_inventario_aplicado();
                duesEntities db = new duesEntities();
                documento.detalle_inventario_aplicado = new List<detalle_inventario_aplicado>();
                List<detalle_inventario_aplicado> products = doc.detalle_inventario_aplicado.ToList();

                var categories = from p in doc.detalle_inventario_aplicado.ToList() group p by p.ItemCode into g select new { itemcode = g.Key, cantidad = g.Sum(p => p.Cantidad), nombre = g.Select(o => o.ItemName).FirstOrDefault() };

                documento.FechaInicio = DateTime.Now;
                documento.UsuarioId = 1;

                foreach (var cat in categories)

                {

                    documento.detalle_inventario_aplicado.Add(new detalle_inventario_aplicado
                    {
                        ItemName = cat.nombre,
                        ItemCode = cat.itemcode,
                        Cantidad = cat.cantidad,
                        UsuarioId = 1
                    });

                }


                db.inventario_aplicado.Add(documento);
                db.SaveChanges();

                return doc;
            }
            catch (Exception ex)
            {

                Error(ex, "El inventario");
                return null;
            }
        }
    }
}
