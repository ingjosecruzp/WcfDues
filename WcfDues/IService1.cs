using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WcfDues
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1 
    {

       [OperationContract]
       [WebInvoke(UriTemplate = "getClientes",
       BodyStyle = WebMessageBodyStyle.WrappedRequest,
       ResponseFormat = WebMessageFormat.Json,
       RequestFormat = WebMessageFormat.Json,
       Method = "POST")]
        List<clientes> getClientes();

        [OperationContract]
        [return: MessageParameter(Name = "MyResult")]
        [WebInvoke(UriTemplate = "addCliente",
        BodyStyle = WebMessageBodyStyle.WrappedRequest,
        ResponseFormat = WebMessageFormat.Json,
        RequestFormat = WebMessageFormat.Json,
        Method = "POST")]
        //clientes addCliente(clientes Cliente,int tipo);
        clientes addCliente(clientes Cliente);

        [OperationContract]
        //[return: MessageParameter(Name = "MyResult")]
        [WebInvoke(UriTemplate = "updateCliente",
        BodyStyle = WebMessageBodyStyle.WrappedRequest,
        ResponseFormat = WebMessageFormat.Json,
        RequestFormat = WebMessageFormat.Json,
        Method = "POST")]
        clientes updateCliente(clientes Cliente);

        [OperationContract]
        //[return: MessageParameter(Name = "MyResult")]
        [WebInvoke(UriTemplate = "getCliente",
        BodyStyle = WebMessageBodyStyle.Bare,
        ResponseFormat = WebMessageFormat.Json,
        RequestFormat = WebMessageFormat.Json,
        Method = "POST")]
        clientes getCliente(int Id);

        [OperationContract]
        //[return: MessageParameter(Name = "MyResult")]
        [WebInvoke(UriTemplate = "deleteCliente",
        BodyStyle = WebMessageBodyStyle.WrappedRequest,
        ResponseFormat = WebMessageFormat.Json,
        RequestFormat = WebMessageFormat.Json,
        Method = "POST")]
        clientes deleteCliente(int Id);
    }

}
