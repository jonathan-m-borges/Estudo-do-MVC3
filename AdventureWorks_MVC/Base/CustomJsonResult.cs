using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace AdventureWorks_MVC.Base
{
    public class CustomJsonResult : JsonResult
    {
        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }
            if (JsonRequestBehavior == JsonRequestBehavior.DenyGet &&
                String.Equals(context.HttpContext.Request.HttpMethod, "GET", StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException("Acesso negado");
            }

            HttpResponseBase response = context.HttpContext.Response;

            if (!String.IsNullOrEmpty(ContentType))
            {
                response.ContentType = ContentType;
            }
            else
            {
                response.ContentType = "application/json";
            }
            if (ContentEncoding != null)
            {
                response.ContentEncoding = ContentEncoding;
            }
            if (Data != null)
            {
                var serializer = new JavaScriptSerializer();
                serializer.RegisterConverters(new[] { new EntidadeJavaScriptConverter() });

                response.Write(serializer.Serialize(Data));
            }
        }
    }
}