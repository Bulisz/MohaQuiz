namespace MohaQuiz.Backend.MiddleWareConfigs;

public static class CorsRules
{
    public static IServiceCollection AddCorsRules(this IServiceCollection services)
    {
        services.AddCors(options => options
                .AddDefaultPolicy(policyConfig => policyConfig
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed((host) => true)
                .AllowCredentials()));

        //https://stackoverflow.com/questions/54823650/cors-policy-dont-want-to-work-with-signalr-and-asp-net-core

        return services;
    }
}
