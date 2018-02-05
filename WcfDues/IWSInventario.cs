﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WcfDues
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IWSInventario" in both code and config file together.
    [ServiceContract]
    public interface IWSInventario : IWSBase<inventario>
    {
        [OperationContract]
        [WebInvoke(UriTemplate = "",
           BodyStyle = WebMessageBodyStyle.Bare,
           ResponseFormat = WebMessageFormat.Json,
           RequestFormat = WebMessageFormat.Json,
           Method = "POST")]
        detalle_inventario addItem(inventario doc);
    }
}
