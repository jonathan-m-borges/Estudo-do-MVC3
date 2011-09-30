using System.Collections.Generic;
using System.Dynamic;
using System.Runtime.Serialization;

namespace AdventureWorks_MVC.Base
{
    public class ObjetoDinamico : DynamicObject, ISerializable
    {
        private readonly Dictionary<string, object> propriedades = new Dictionary<string, object>();

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            if (propriedades.ContainsKey(binder.Name))
            {
                result = propriedades[binder.Name];
                return true;
            }

            result = null;
            return false;
        }

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            var propertyName = binder.Name;
            return TrySetMember(propertyName, value);
        }

        public bool TrySetMember(string propertyName, object value)
        {
            if (propriedades.ContainsKey(propertyName))
                propriedades[propertyName] = value;
            else
                propriedades.Add(propertyName, value);

            return true;
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            throw new System.NotImplementedException();
        }
    }
}