using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Serialization;
using AdventureWorks_MVC.Base.Extensions;
using AdventureWorks_MVC.Base.Model;

namespace AdventureWorks_MVC.Base
{
    public class EntidadeJavaScriptConverter : JavaScriptConverter
    {
        public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
        {
            var dictionary = new Dictionary<string, object>();

            var modelMetadata = obj.GetType().ObterModelMetadata();

            foreach (var metadata in modelMetadata.Properties)
            {
                if (metadata.PodeMostrar())
                {
                    var valor = obj.ObterValor(metadata.PropertyName);
                    dictionary.Add(metadata.PropertyName,
                                   valor == null
                                       ? ""
                                       : string.IsNullOrEmpty(metadata.DisplayFormatString)
                                             ? valor.ToString()
                                             : string.Format(metadata.DisplayFormatString, valor));
                }
            }

            return dictionary;
        }

        public override IEnumerable<Type> SupportedTypes
        {
            get { return new Type[] { typeof(Entidade) }; }
        }
    }
}