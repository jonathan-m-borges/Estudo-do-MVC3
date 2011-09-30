using System.Web.Mvc;
using AdventureWorks_MVC.Base.Model;

namespace AdventureWorks_MVC.Base.Extensions
{
    public static class ModelMetadataExtensions
    {
        public static bool PodeMostrar(this ModelMetadata metadata, ViewDataDictionary viewData = null)
        {
            return metadata.ShowForDisplay
                && (!metadata.IsComplexType || metadata.ModelType.IsSubclassOf(typeof(Entidade)))
                && (viewData == null || !viewData.TemplateInfo.Visited(metadata));
        }

        public static bool PodeEditar(ModelMetadata metadata, ViewDataDictionary viewData = null)
        {
            return metadata.ShowForEdit
                && (!metadata.IsComplexType || metadata.ModelType.IsSubclassOf(typeof(Entidade)))
                && (viewData == null || !viewData.TemplateInfo.Visited(metadata));
        }
    }
}