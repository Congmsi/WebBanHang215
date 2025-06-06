using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
namespace WebBanHang215.Models.Athentication
{
    public class Authentication : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.Session.GetString("TenDangNhap") == null)
            {
                context.Result = new RedirectToRouteResult(
                   new RouteValueDictionary
                   {
                       {"Controller", "DangNhap" },
                       {"Action", "Login" }
                   });
                    }
        }
    }
}
        
    

