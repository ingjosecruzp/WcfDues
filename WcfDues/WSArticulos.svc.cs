using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WcfDues
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "WSArticulos" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select WSArticulos.svc or WSArticulos.svc.cs at the Solution Explorer and start debugging.
    public class WSArticulos : WsBase, IWSArticulos
    {
        public OITM get(string id)
        {
            throw new NotImplementedException();
        }

        public List<OITM> getXCodigo(string codigo)
        {
            try
            {
                SapEntities db = new SapEntities();
                List<OITM> LstArticulos = db.OITM.Where(p => p.CodeBars == codigo).ToList();

                if (LstArticulos.Count == 0)
                    throw new Exception("Articulo no encontrado");

                return LstArticulos;
            }
            catch (Exception ex)
            {

                Error(ex, "El articulo");
                return null;
            }
        }
        public List<string> getImagen(string nombreimagen)
        {
            try
            {
                
                //string test=WebOperationContext.Current.IncomingRequest.UriTemplateMatch.QueryParameters["ssn"];

                //return base64code(nombreimagen);

                List<string> LstImagenes = new List<string>();
                LstImagenes.Add(base64code(nombreimagen));

                return LstImagenes;
            }
            catch (Exception ex)
            {

                Error(ex, "El articulo");
                return null;

            }
        }
        public String base64code(string ImagenPath)
        {
            try
            {
                byte[] imageArray = File.ReadAllBytes(@"\\192.168.1.11\sapbo\IMAGENES\" + ImagenPath);
                return Convert.ToBase64String(imageArray);
                /*if(System.Diagnostics.Debugger.IsAttached == true)
                {
                    byte[] imageArray = File.ReadAllBytes(@"\\192.168.1.11\sapbo\IMAGENES\" + ImagenPath);
                    return Convert.ToBase64String(imageArray);
                }
                else
                {
                    byte[] imageArray = File.ReadAllBytes(@"I:\" + ImagenPath);
                    return Convert.ToBase64String(imageArray);
                }*/
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<OITM> getArticulos(string nombrearticulo,int index)
        {
            try
            {
                if(nombrearticulo.Length < 3)
                    throw new Exception("Introducir mas caracteres para realizar la busqueda");

                SapEntities db = new SapEntities();

                //List<OITM> LstArticulos = db.OITM.Where(p => p.ItemName.Contains(nombrearticulo)).ToList();

                List<OITM> LstArticulos = db.OITM.Where(p => p.ItemName.Contains(nombrearticulo)).OrderBy(o => o.ItemName).Skip(index).Take(15) .ToList();


                return LstArticulos;
            }
            catch (Exception ex)
            {


                Error(ex, "El articulo");
                return null;
            }
        }

        public List<ITM1> getPrecios(String itemCode)
        {

            try
            {
                SapEntities db = new SapEntities();
                List<ITM1> LstPrecios = db.ITM1.Where(p => p.ItemCode == itemCode && (p.PriceList == 4 || p.PriceList == 1 || p.PriceList == 2 || p.PriceList == 3 || p.PriceList == 6 || p.PriceList == 7 || p.PriceList == 5 || p.PriceList == 14)).ToList();

                return LstPrecios;
            }
            catch (Exception ex)
            {
                Error(ex, "El articulo");
                return null;
            }
        }

        public List<OITM> getDatosInventario(String itemCode)
        {

            try
            {
                SapEntities db = new SapEntities();
                List<OITM> datosInventario = db.OITM.Where(p => p.ItemCode == itemCode).ToList();

                return datosInventario;
            }
            catch (Exception ex)
            {
                Error(ex, "El articulo");
                return null;
            }
        }

        public List<OWHS> getListaAlmacenes()
        {

            try
            {
                SapEntities db = new SapEntities();
                List<OWHS> listaAlmacenes = db.OWHS.ToList();

                return listaAlmacenes;
            }
            catch (Exception ex)
            {
                Error(ex, "El articulo");
                return null;
            }
        }

        public List<OITW> getDetalleAlmacen(String itemCode, String whsCode)
        {

            try
            {
                SapEntities db = new SapEntities();
                List<OITW> detalleAlmacen = db.OITW.Where(p => p.ItemCode == itemCode && p.WhsCode == whsCode).ToList();

                return detalleAlmacen;
            }
            catch (Exception ex)
            {
                Error(ex, "El articulo");
                return null;
            }
        }
    }
}
