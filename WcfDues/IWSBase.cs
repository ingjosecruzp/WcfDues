using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WcfDues
{
    [ServiceContract]
    public interface IWSBase<TEntity>
    {
        [OperationContract]
        [WebInvoke(UriTemplate = "usuarios/{id}",
        BodyStyle = WebMessageBodyStyle.Bare,
        ResponseFormat = WebMessageFormat.Json,
        RequestFormat = WebMessageFormat.Json,
        Method = "GET")]
        TEntity get(string id);
    }
}