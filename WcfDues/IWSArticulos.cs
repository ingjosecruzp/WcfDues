using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Web.Script.Services;

namespace WcfDues
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IWSArticulos" in both code and config file together.
    [ServiceContract]
    public interface IWSArticulos : IWSBase<OITM>
    {
        [OperationContract]
        [WebInvoke(UriTemplate = "?method=getXCodigo&codigo={codigo}",
            BodyStyle = WebMessageBodyStyle.Bare,
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            Method = "GET")]
        List<OITM> getXCodigo(string codigo);

        [OperationContract]
        [WebInvoke(UriTemplate = "?method=getImagen&Imagen={nombreimagen}",
            BodyStyle = WebMessageBodyStyle.Bare,
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            Method = "GET")]
        List<string> getImagen(string nombreimagen);

        [OperationContract]
        [WebInvoke(UriTemplate = "?method=getArticulos&Nombre={nombrearticulo}&Index={index}",
            BodyStyle = WebMessageBodyStyle.Bare,
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            Method = "GET")]
        List<OITM> getArticulos(string nombrearticulo,int index);
    }
}
