using back.Services;

namespace back.Middlewares
{
    public class TokenRevocation
    {
        private readonly RequestDelegate _next;
        private readonly Token _token;

        public TokenRevocation(RequestDelegate next, Token token)
        {
            _next = next;
            _token = token;
        }

        public async Task Invoke(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            if (!string.IsNullOrEmpty(token) && _token.IsTokenRevoked(token))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Token has been revoked");
                return;
            }

            await _next(context);
        }
    }


}