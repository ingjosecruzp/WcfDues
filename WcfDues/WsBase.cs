using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Web;
using System.Web;

namespace WcfDues
{
    public class WsBase
    {
        public WsBase()
        {
            OperationContext currentContext = OperationContext.Current;
            HttpRequestMessageProperty reqMsg = currentContext.IncomingMessageProperties["httpRequest"] as HttpRequestMessageProperty;
        }
        public void Error(Exception ex, String nombrevista)
        {
            OutgoingWebResponseContext response = WebOperationContext.Current.OutgoingResponse;
            string error = null;

            if (ex is DbEntityValidationException)

                error = ((System.Data.Entity.Validation.DbEntityValidationException)ex).EntityValidationErrors.ToList()[0].ValidationErrors.ToList()[0].ErrorMessage;

            else if (ex is DbUpdateException)
            {
                var sqlException = ex.InnerException.InnerException as MySql.Data.MySqlClient.MySqlException;

                if (sqlException.Number == 1451)  //NO SE PUEDE ELIMINAR POR QUE TIENE LLAVES FORANEAS
                {
                    error = string.Format("No es posible eliminar {0} porque esta en uso en el sistema", nombrevista);
                }
                else if (sqlException.Number == 1062)  //YA EXISTE EL ITEM EN EL SISTEMA
                    error = string.Format("{0} ya existe en el sistema", nombrevista);
                else
                    error = ex.InnerException.InnerException.Message;
            }
            else if (ex is EntityCommandExecutionException)
                error = ex.InnerException.Message;
            else
                error = ex.Message;

            response.StatusCode = HttpStatusCode.InternalServerError;
            response.Headers.Add("Access-Control-Expose-Headers", "Error");
            response.Headers.Add("Error", error);
        }

        public void Error(Exception ex)
        {
            OutgoingWebResponseContext response = WebOperationContext.Current.OutgoingResponse;
            string error = null;
            if (ex is DbEntityValidationException)
                error = ((System.Data.Entity.Validation.DbEntityValidationException)ex).EntityValidationErrors.ToList()[0].ValidationErrors.ToList()[0].ErrorMessage;
            else if (ex is DbUpdateException)
                error = ex.InnerException.InnerException.Message;
            else if (ex is EntityCommandExecutionException)
                error = ex.InnerException.Message;
            else

                error = ex.Message;
            response.Headers.Add("Error", error);

        }
        //Metodo para dar respuesta las peticiones OPTION CORS
        public void GetOptions()
        {
        }
    }
}