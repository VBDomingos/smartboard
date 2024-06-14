using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Data;
using SmartBoard.Models;
using System.Diagnostics;
using System.Linq.Expressions;

namespace SmartBoard.Filters

{
    public class AuthFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var user = context.HttpContext.Session.GetString("TipoPessoa");
            if (user == null)
            {
                context.Result = new RedirectToActionResult("Index", "Login", null);
            }
            else
            {
                if (Convert.ToChar(user) == 'C' && context.RouteData.Values.Values.First().ToString() != "DeviceClient") context.Result = new RedirectToActionResult("HomeClient", "DeviceClient", null);
                else if (Convert.ToChar(user) == 'T' && context.RouteData.Values.Values.First().ToString() != "DeviceTecnico") context.Result = new RedirectToActionResult("HomeTecnico", "DeviceTecnico", null);
                else if (Convert.ToChar(user) == 'A' && context.RouteData.Values.Values.First().ToString() != "DeviceAdmin") context.Result = new RedirectToActionResult("HomeAdmin", "DeviceAdmin", null);
            }

            base.OnActionExecuting(context);
        }
    }
}
