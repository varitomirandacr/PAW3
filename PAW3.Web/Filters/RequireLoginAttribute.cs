using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace PAW3.Web.Filters;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
public class RequireLoginAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        // Skip login check for LoginController
        var controllerName = context.RouteData.Values["controller"]?.ToString();
        if (controllerName == "Login")
        {
            base.OnActionExecuting(context);
            return;
        }

        var isLoggedIn = context.HttpContext.Session.GetString("IsLoggedIn");
        
        if (isLoggedIn != "true")
        {
            context.Result = new RedirectToActionResult("Index", "Login", null);
            return;
        }

        base.OnActionExecuting(context);
    }
}

