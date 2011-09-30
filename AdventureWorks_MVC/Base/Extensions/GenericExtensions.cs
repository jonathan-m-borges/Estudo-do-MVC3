using System;

namespace AdventureWorks_MVC.Base.Extensions
{
    public static class GenericExtensions
    {
        public static object ObterValor<T>(this T obj, string property)
        {
            object value = null;

            if (!string.IsNullOrEmpty(property))
            {
                foreach (var prop in property.Split('.'))
                {
                    try
                    {
                        var propertyInfo = obj.GetType().GetProperty(prop);
                        value = propertyInfo.GetValue(obj, new object[] { });
                    }
                    catch
                    {
                    }
                }
            }

            return value;
        }
    }
}