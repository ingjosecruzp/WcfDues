using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WcfDues
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        public List<clientes> getClientes()
        {
            try
            {
                duesEntities db = new duesEntities();
                List<clientes> LstClientes = db.clientes.Include("colonias").ToList();

                return LstClientes;
            }
            catch (Exception)
            {

                return null;
            }
        }
        public clientes updateCliente(clientes Cliente)
        {
            try
            {
                duesEntities db = new duesEntities();

                Cliente.colonias = null;
                db.clientes.Attach(Cliente);
                db.Entry(Cliente).State = EntityState.Modified;

                db.SaveChanges();

                return Cliente;
            }
            catch (Exception)
            {

                return null;
            }
        }
        //public clientes addCliente(clientes Cliente,int tipo)
        public clientes addCliente(clientes Cliente)
        {
            try
            {
                duesEntities db = new duesEntities();
                db.clientes.Add(Cliente);

                db.SaveChanges();

                return Cliente;
            }
            catch (Exception)
            {

                return null;
            }
        }
        public clientes getCliente(int Id)
        {
            try
            {
                duesEntities db = new duesEntities();
                //clientes cliente = db.clientes.Find(Id);
                clientes cliente = db.clientes.Include(c => c.colonias)
                            .Where(i => i.Id == Id).SingleOrDefault();

                //db.Database.ExecuteSqlCommand("insert");

                return cliente;
            }
            catch (Exception)
            {

                return null;
            }
        }
        public clientes deleteCliente(int Id)
        {
            try
            {
                duesEntities db = new duesEntities();
                clientes cliente = db.clientes.Find(Id);

                db.clientes.Remove(cliente);
                db.SaveChanges();

                return null;
            }
            catch (Exception)
            {

                return null;
            }
        }
        public string GetData(int value)
        {

            try
            {
                duesEntities db = new duesEntities();

                clientes cliente1 = new clientes();
                cliente1.Nombre = "Jose";
                cliente1.Telefono = "6691169189";
                cliente1.colonias = new colonias
                {
                    Nombre = "Sabalo"
                };


                db.clientes.Add(cliente1);
                db.SaveChanges();

                clientes cliente2 = db.clientes.Find(2);
                db.clientes.Where(c => c.Telefono == "6691169189");
            }
            catch (Exception)
            {

                throw;
            }

            return string.Format("You entered: {0}", value);

        }
    }
}
