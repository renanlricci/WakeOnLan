using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Text;
using WakeOnLan.CrossCutting.Configuration;
using WakeOnLan.Domain.Commands.Auth;
using WakeOnLan.Domain.Commands.WakeUp.MainDevice;
using WakeOnLan.Domain.Interfaces.Services;
using WakeOnLan.Services.Jwt;
using WakeOnLan.Services.WakeOnLan;

namespace WakeOnLan.Api.Infrastruture.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSwagger(this IServiceCollection serviceCollection, AppSettings appSettings)
        {
            serviceCollection.AddSwaggerGen(x =>
            {
                x.SwaggerDoc(
                    "v1",
                    new OpenApiInfo()
                    {
                        Title = appSettings.AppName,
                        Version = "v1"
                    });
            });
            return serviceCollection;
        }

        public static IServiceCollection AddSwaggerBearerSecurity(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSwaggerGen(x =>
            {
                x.AddSecurityDefinition(
                    "Bearer",
                    new OpenApiSecurityScheme
                    {
                        Description = "JWT Authorization header using the Bearer scheme. Example \"Authorization: Bearer {token}",
                        Name = "Authorization",
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.ApiKey
                    });
                x.AddSecurityRequirement(
                    new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme , Id = "Bearer" },
                                Scheme = "oauth2",
                                Name = "Bearer",
                                In = ParameterLocation.Header,
                            },
                            new List<string>()
                        }
                    });
            });
            return serviceCollection;
        }

        public static IServiceCollection AddBearerAuthentication(this IServiceCollection serviceCollection, AppSettings appSettings)
        {
            serviceCollection
                .AddAuthentication(authOptions =>
                {
                    authOptions.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(bearerOptions =>
                {
                    var parameters = bearerOptions.TokenValidationParameters;
                    parameters.IssuerSigningKey =
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appSettings.JwtSettings.Secret));
                    parameters.ValidAudience = appSettings.JwtSettings.Audience;
                    parameters.ValidIssuer = appSettings.JwtSettings.Issuer;

                    parameters.ValidateIssuerSigningKey = true;
                    parameters.ValidateLifetime = true;
                    parameters.ClockSkew = TimeSpan.Zero;
                });

            serviceCollection.AddAuthorization(auth =>
            {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser()
                    .Build());
            });
            return serviceCollection;
        }

        public static IServiceCollection AddIoC(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddMediatR(typeof(AuthenticateCommand), typeof(WakeUpMainCommand));
            serviceCollection.AddSingleton<ITokenGeneratorService, TokenGeneratorService>();
            serviceCollection.AddSingleton<IWakeOnLanService, WakeOnLanService>();

            return serviceCollection;
        }
    }
}
