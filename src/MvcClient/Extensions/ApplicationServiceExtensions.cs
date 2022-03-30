using System.IdentityModel.Tokens.Jwt;

namespace MvcClient.Extensions;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        JwtSecurityTokenHandler.DefaultMapInboundClaims = false;

        services.AddAuthentication(options =>
            {
                options.DefaultScheme = "Cookies";
                options.DefaultChallengeScheme = "oidc";
            })
            .AddCookie("Cookies")
            .AddOpenIdConnect("oidc", options =>
            {
                options.Authority = "https://localhost:5001";

                options.ClientId = "mvc";
                options.ClientSecret = "secret";
                options.ResponseType = "code";
                
                options.Scope.Add("profile");
                options.GetClaimsFromUserInfoEndpoint = true;

                options.SaveTokens = true;
                
                options.Scope.Add("api1");
                options.Scope.Add("offline_access");
            });

        return services;
    }
}