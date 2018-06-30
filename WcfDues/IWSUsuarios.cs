using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WcfDues
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IWSUsuarios" in both code and config file together.
    [ServiceContract]
    public interface IWSUsuarios : IWSBase<usuarios>
    {
        [OperationContract]
        [WebInvoke(UriTemplate = "?method=Login&usuario={usuario}&password={password}",
        BodyStyle = WebMessageBodyStyle.WrappedRequest,
        ResponseFormat = WebMessageFormat.Json,
        RequestFormat = WebMessageFormat.Json,
        Method = "GET")]
        List<String> Login(string usuario, string password);

        [OperationContract]
        [WebInvoke(UriTemplate = "?method=getId&usuario={usuario}&password={password}",
        BodyStyle = WebMessageBodyStyle.WrappedRequest,
        ResponseFormat = WebMessageFormat.Json,
        RequestFormat = WebMessageFormat.Json,
        Method = "GET")]
        String getId(string usuario, string password);

        [OperationContract]
       [WebInvoke(UriTemplate = "?method=socket",
        BodyStyle = WebMessageBodyStyle.WrappedRequest,
        ResponseFormat = WebMessageFormat.Json,
        RequestFormat = WebMessageFormat.Json,
        Method = "GET")]
        string socket();

        [OperationContract]
        [WebInvoke(UriTemplate = "?method=video",
        BodyStyle = WebMessageBodyStyle.WrappedRequest,
        ResponseFormat = WebMessageFormat.Json,
        RequestFormat = WebMessageFormat.Json,
        Method = "GET")]
        string video();
    }
}
