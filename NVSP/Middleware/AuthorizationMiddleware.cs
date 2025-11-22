using System.Security.Claims;

namespace NVSP.Middleware
{
    public class AuthorizationMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthorizationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Lấy thông tin user từ token (tạm thời dùng header)
            var maCaNhan = context.Request.Headers["X-User-Id"].FirstOrDefault();

            if (!string.IsNullOrEmpty(maCaNhan))
            {
                var claims = new List<Claim>
                {
                    new Claim("MaCaNhan", maCaNhan)
                };

                var identity = new ClaimsIdentity(claims, "Custom");
                context.User = new ClaimsPrincipal(identity);
            }

            await _next(context);
        }
    }
}