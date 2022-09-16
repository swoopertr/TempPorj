using DataLayer.Db;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebCore.Extentions;

namespace TempProj.UI.Filters
{
    public class LoginFilter : Attribute, IActionFilter
    {
        private readonly string[] _roles;
        public LoginFilter(string[] roles)
        {
            _roles = roles;
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            //[LoginFilter(new[] {"Admin", "User", "Tester"})]
            var loginType = context.HttpContext.Session.GetObjectFromJson<User>("login_type");
            if (_roles.Any(q => q.Equals(loginType)))
            {
                context.Result = new RedirectToActionResult("Login", "admin", null);
            }
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
           
        }
    }
}
