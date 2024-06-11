using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SmartBoard.Models;

namespace SmartBoard.Filters

{
    public class AuthFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            context.Result = null;
            var user = context.HttpContext.Session.GetString("TipoPessoa");
            if (user == null)
            {
                context.Result = new RedirectToActionResult("Index", "Login", null);
            }
            //else
            //{
            //    if (Convert.ToChar(user) == 'C') context.Result = new RedirectToActionResult("HomeClient", "DeviceClient", context.HttpContext.Session);
            //    else if (Convert.ToChar(user) == 'T') context.Result = new RedirectToActionResult("HomeTecnico", "DeviceTecnico", context.HttpContext.Session);
            //    else if (Convert.ToChar(user) == 'A') context.Result = new RedirectToActionResult("HomeAdmin", "DeviceAdmin", context.HttpContext.Session);
            //}

            base.OnActionExecuting(context);
        }
    }
}
