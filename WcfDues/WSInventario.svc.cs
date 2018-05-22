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
        
       public inventario addItem(inventario doc)
        {
            try
            {
                inventario documento = new inventario();
                detalle_inventario detalle = new detalle_inventario();
                duesEntities db = new duesEntities();
                documento.detalle_inventario = new List<detalle_inventario>();
                List<detalle_inventario> products = doc.detalle_inventario.ToList();

                var categories = from p in doc.detalle_inventario.ToList() group p by p.ItemCode into g select new { itemcode = g.Key, cantidad = g.Sum(p => p.Cantidad), nombre = g.Select(o => o.ItemName).FirstOrDefault() };

                documento.FechaInicio = DateTime.Now;
             //   documento.UsuarioId = 1;

                foreach (var cat in categories)

                {

                    documento.detalle_inventario.Add(new detalle_inventario
                    {
                        ItemName = cat.nombre,
                        ItemCode = cat.itemcode,
                        Cantidad = cat.cantidad,
                       // UsuarioId = 1
                    });

                }
                  

            db.inventario.Add(doc);
            db.SaveChanges();

            return doc;
            }
            catch (Exception ex)
            {

                Error(ex, "El inventario");
                return null;
            }
        }

        public inventario get(string id)
        {
            throw new NotImplementedException();
        }
        public List<inventario> getInventarioActual()
        {
            try
            {
                duesEntities db = new duesEntities();

                List<inventario> LstInventarios = db.inventario.Where(e=>e.Estado=="ACTIVO").ToList();

                return LstInventarios;
            }
            catch (Exception ex)
            {

                Error(ex, "El inventario");
                return null;
            }
        }
        public List<detalle_inventario> getInventarioActualDetalles(string id)
        {

            int[] ids = id.Split(',').Select(x => int.Parse(x)).ToArray();
            try
            {
                duesEntities db = new duesEntities();

                List<detalle_inventario> LstInventariosDetalle = db.detalle_inventario.Include("usuarios").Where(i => ids.Contains(i.InventarioId)).ToList();
                

                return LstInventariosDetalle;
            }
            catch (Exception ex)
            {

                Error(ex, "El inventario");
                return null;
            }
        }
   /*     public detalle_inventario addInventariosDetalleAplicado(inventario doc)
        {


          var result=doc.detalle_inventario.GroupBy(x => x.ItemCode).Select(e=>e.)
          
            int[] ids = id.Split(',').Select(x => int.Parse(x)).ToArray();
            try
            {
                
                duesEntities db = new duesEntities();

              //  List<detalle_inventario> LstInventariosDetalle =doc.whe
                    foreach (detalle_inventario detalle in doc.detalle_inventario)
                {
                    doc.detalle_inventario
                }

                return LstInventariosDetalle;
            }
            catch (Exception ex)
            {

                Error(ex, "El inventario");
                return null;
            }
        }


   /     public List<detalle_inventario> getInventarioActualDetalles(string id)
        {

            int[] ids = id.Split(',').Select(x => int.Parse(x)).ToArray();
            try
            {
                duesEntities db = new duesEntities();

                List<detalle_inventario> LstInventariosDetalle = db.detalle_inventario.Include("usuarios").Where(i => ids.Contains(i.InventarioId)).ToList();


                return LstInventariosDetalle;
            }
            catch (Exception ex)
            {

                Error(ex, "El inventario");
                return null;
            }
        }*/

    }
}
