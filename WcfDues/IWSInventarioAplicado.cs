using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WcfDues
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IWSInventarioAplicado" in both code and config file together.
    [ServiceContract]
    public interface IWSInventarioAplicado
    {
        [OperationContract]
        [WebInvoke(UriTemplate = "",
        BodyStyle = WebMessageBodyStyle.Bare,
        ResponseFormat = WebMessageFormat.Json,
        RequestFormat = WebMessageFormat.Json,
        Method = "POST")]
        inventario_aplicado addItem(inventario_aplicado doc);

    }
}
