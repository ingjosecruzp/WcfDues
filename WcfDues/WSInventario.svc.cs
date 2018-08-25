using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Web.Script.Serialization;

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
                duesEntities db = new duesEntities();
                /*inventario documento = new inventario();
                detalle_inventario detalle = new detalle_inventario();
                documento.detalle_inventario = new List<detalle_inventario>();
                List<detalle_inventario> products = doc.detalle_inventario.ToList();

                var categories = from p in doc.detalle_inventario.ToList() group p by p.ItemCode into g select new { itemcode = g.Key, cantidad = g.Sum(p => p.Cantidad), nombre = g.Select(o => o.ItemName).FirstOrDefault() };

                documento.FechaInicio = DateTime.Now;
                documento.Estado = "ACTIVO";
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

                }*/

                doc.FechaInicio = DateTime.Now;
                doc.Estado = "ACTIVO";
                foreach (detalle_inventario detalle in doc.detalle_inventario)
                {
                    detalle.inventario = null;
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
        public List<nInv> getInventarioActual(string id)
        {

            int[] ids = id.Split(',').Select(x => int.Parse(x)).ToArray();

            try
            {
                duesEntities db = new duesEntities();

                List<inventario> LstInventarios = db.inventario.Where(e=>e.Estado=="ACTIVO" && !ids.Contains(e.idInventario)).ToList();

                List<nInv> LstInventarios2 = new List<nInv>();
                nInv tmp=new nInv();

                foreach (inventario inv in LstInventarios)
                {
                    tmp = new nInv();
                    usuarios[] nombreUsuario = db.usuarios.Where(e => e.Id == inv.UsuarioId).ToArray();

                    //Formateo de la fecha con 0 si es menor de 10 el día o el mes
                    String day = "";
                    String month = "";

                    if (inv.FechaInicio.Day < 10) day = "0" + inv.FechaInicio.Day;
                    else day = inv.FechaInicio.Day.ToString();

                    if (inv.FechaInicio.Month < 10) month = "0" + inv.FechaInicio.Month;
                    else month = inv.FechaInicio.Month.ToString();
                    //Guardado de los datos en un objeto
                    tmp.idInventario = inv.idInventario;
                    tmp.FechaInicio = day+"/"+month+"/"+inv.FechaInicio.Year;
                    tmp.Usuario = nombreUsuario[0].Usuario;
                    tmp.UUID = inv.UUID;
                    tmp.Modelo = inv.Modelo;

                    LstInventarios2.Add(tmp);
                }

                return LstInventarios2;
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

        public string updateDetalleInventario(string id, string cantidad)
        {
            int[] ids = id.Split(',').Select(x => int.Parse(x)).ToArray();
            int[] cantidades = cantidad.Split(',').Select(x => int.Parse(x)).ToArray();

            try
            {
                duesEntities db = new duesEntities();

                var detalle = db.detalle_inventario.First();
                int idActual;

                for (int x=0; x<ids.Length; x++)
                {
                    idActual = ids[x];
                    detalle = db.detalle_inventario.Where(s => s.idDetalle_Inventario == idActual).First();
                    detalle.Cantidad = cantidades[x];
                    db.SaveChanges();
                }

                return null;
            }
            catch (Exception ex)
            {

                Error(ex, "El inventario");
                return ex.Message;
            }
        }

        public string saveDiferencias(string inventario, string diferencia)
        {
            int[] inventarios = inventario.Split(',').Select(x => int.Parse(x)).ToArray();

            /*var data = JsonConvert.DeserializeObject<IEnumerable<IDictionary<string, object>>>(json);
            var array = data.Select(d => d.Values.ToArray()).ToArray();
            
            JavaScriptSerializer js = new JavaScriptSerializer();
            Person [] persons =  js.Deserialize<Person[]>(json);*/

            JavaScriptSerializer js = new JavaScriptSerializer();
            detalle_diferencias[] difs = js.Deserialize<detalle_diferencias[]>(diferencia);

            /*List<detalle_diferencias> difs = new List<detalle_diferencias>();

            foreach (Object element in array){
                difs.Add((detalle_diferencias)element);
            }*/

            try{
                duesEntities db = new duesEntities();
                SapEntities db2 = new SapEntities();

                diferencias doc = new diferencias();
                doc.Fecha = DateTime.Now;
                doc.Estado = "ACTIVO";
                doc.detalle_diferencias = difs;

                foreach (detalle_diferencias detalle in doc.detalle_diferencias){
                    detalle.diferencias = null;

                    List<OITW> oitwList = db2.OITW.Where(p => p.ItemCode == detalle.ItemCode).ToList();
                    OITW FirstOITW = oitwList.ElementAt(0);

                    detalle.Cantidad = (long)FirstOITW.OnHand;
                    detalle.Diferencia = detalle.Cantidad - detalle.Contado;
                }

                db.diferencias.Add(doc);

                foreach (int actual in inventarios){
                    if (actual!=0){
                        inventario desactivar = db.inventario.Where(s => s.idInventario == actual).First();

                        desactivar.Estado = "APLICADO";
                        db.SaveChanges();
                    }
                }

                db.SaveChanges();
            }
            catch (Exception ex){

                Error(ex, "El inventario");
                return ex.Message;
            }

            return "";
            /*try
            {
                duesEntities db = new duesEntities();

                var detalle = db.detalle_inventario.First();
                int idActual;

                for (int x = 0; x < ids.Length; x++)
                {
                    idActual = ids[x];
                    detalle = db.detalle_inventario.Where(s => s.idDetalle_Inventario == idActual).First();
                    detalle.Cantidad = cantidades[x];
                    db.SaveChanges();
                }

                return null;
            }
            catch (Exception ex)
            {

                Error(ex, "El inventario");
                return ex.Message;
            }*/
        }

        //Clase para los inventarios activos con nombre de usuario
        public class nInv {
            public int idInventario;
            public String FechaInicio;
            public String Usuario;
            public String UUID;
            public String Modelo;
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
