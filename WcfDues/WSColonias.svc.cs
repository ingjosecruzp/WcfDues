using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfDues
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "WSColonias" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select WSColonias.svc or WSColonias.svc.cs at the Solution Explorer and start debugging.
    public class WSColonias : IWSColonias
    {
        public List<colonias> getColonias(string id)
        {
            try
            {
                duesEntities db = new duesEntities();
                List<colonias> LstColonias = db.colonias.ToList();

                return LstColonias;
            }
            catch (Exception)
            {

                return null;
            }
        }
    }
}
