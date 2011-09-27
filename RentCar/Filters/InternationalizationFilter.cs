using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Web.Mvc;
using System.Linq;

namespace RentCar.Filters
{
    public class InternationalizationFilter : ActionFilterAttribute
    {
        private readonly string[] cultures = { "pt-br", "en-us" };

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var cultureName = GetCultureName(filterContext);

            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(cultureName);
            Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture(cultureName);

            var selectList = GetCultureSelectList(cultureName);

            filterContext.Controller.ViewBag._cultures = selectList;
            filterContext.Controller.ViewBag._culture = cultureName;
        }

        private SelectList GetCultureSelectList(string cultureName)
        {
            var culturesInfo = cultures.ToDictionary(culture => culture, culture => new CultureInfo(culture));
            var selectList = new SelectList(culturesInfo, "Key", "Value.NativeName", cultureName);
            return selectList;
        }

        private string GetCultureName(ActionExecutingContext filterContext)
        {
            var request = filterContext.HttpContext.Request;

            var cultureCookie = request.Cookies["_culture"];
            var cultureName = cultureCookie != null ? cultureCookie.Value : request.UserLanguages[0];

            if (cultureName == null)
                return cultures[0];

            cultureName = cultureName.ToLower();

            if (!cultures.Contains(cultureName))
                foreach (var culture in cultures.Where(culture => culture.StartsWith(cultureName.Substring(0, 2))))
                    return culture;

            return cultureName;
        }
    }
}