using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FloAPI.Filters;

public class AuthenticationFilter : Attribute, IAuthorizationFilter
{
    private const string ApiKeyHeader = "Flo-Api-Key";

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        if(!IsApiKeyValid(context.HttpContext))
            context.Result = new UnauthorizedResult();
    }

    private static bool IsApiKeyValid(HttpContext context)
    {
        string? apiKey = context.Request.Headers[ApiKeyHeader];

        if (string.IsNullOrEmpty(apiKey))
            return false;
        
        string realApiKey = context.RequestServices
            .GetRequiredService<IConfiguration>()
            .GetValue<string>("ApiKey")!;
        
        return realApiKey == apiKey;
    }
}