﻿namespace MohaQuiz.Backend.MiddleWareConfigs;

public static class CorsRules
{
    public static IServiceCollection AddCorsRules(this IServiceCollection services)
    {
        services.AddCors(options => options
                .AddDefaultPolicy(policyConfig => policyConfig
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials()
                .WithOrigins("http://localhost:4200", "http://peterthegreat-001-site1.gtempurl.com")));

        return services;
    }
}
