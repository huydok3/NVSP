using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NVSP.Services;

namespace NVSP.Attributes
{
    public class AuthorizeRoleAttribute : Attribute, IAsyncActionFilter
    {
        private readonly string[] _allowedRoles;

        public AuthorizeRoleAttribute(params string[] allowedRoles)
        {
            _allowedRoles = allowedRoles;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var authorizationService = context.HttpContext.RequestServices.GetService<IAuthorizationService>();
            var maCaNhan = context.HttpContext.User.FindFirst("MaCaNhan")?.Value;

            if (string.IsNullOrEmpty(maCaNhan))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            var user = await authorizationService.GetCurrentUserAsync(maCaNhan);
            if (user == null || !_allowedRoles.Contains(user.LoaiTk))
            {
                context.Result = new ForbidResult();
                return;
            }

            await next();
        }
    }
}