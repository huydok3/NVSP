using NVSP.Services;

namespace NVSP.Middleware
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IJwtService jwtService)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (!string.IsNullOrEmpty(token))
            {
                var maCaNhan = jwtService.ValidateToken(token);
                if (!string.IsNullOrEmpty(maCaNhan))
                {
                    var claims = new[]
                    {
                        new System.Security.Claims.Claim("MaCaNhan", maCaNhan)
                    };

                    var identity = new System.Security.Claims.ClaimsIdentity(claims, "JWT");
                    context.User = new System.Security.Claims.ClaimsPrincipal(identity);
                }
            }

            await _next(context);
        }
    }
}