using System;
using System.Web.Mvc;

namespace AdventureWorks_MVC.Base.Extensions
{
    public static class TypeExtensions
    {
        public static ModelMetadata ObterModelMetadata(this Type type)
        {
            return ModelMetadataProviders.Current.GetMetadataForType(() => null, type);
        }
    }
}