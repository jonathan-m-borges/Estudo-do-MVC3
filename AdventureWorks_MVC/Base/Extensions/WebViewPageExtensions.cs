using System.Web.Mvc;

namespace AdventureWorks_MVC.Base.Extensions
{
    public static class WebViewPageExtensions
    {
        public static ModelMetadata GenericModelMetadata(this WebViewPage webViewPage)
        {
            var type = webViewPage.ViewData.ModelMetadata.ModelType.GetGenericArguments()[0];
            return ModelMetadataProviders.Current.GetMetadataForType(() => null, type);
        }
    }
}