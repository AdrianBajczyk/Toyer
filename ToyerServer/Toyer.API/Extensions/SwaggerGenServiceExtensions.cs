﻿using Microsoft.OpenApi.Models;
using System.Reflection;

namespace Toyer.API.Extensions;

public static class SwaggerGenServiceExtensions
{
    public static IServiceCollection AddCustomSwaggerGen(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "Toyer API",
                Description = "This is a training project summarizing a year of learning in the CodeCool company course. " +
                              "Here you will find a ready-made API that supports remote control of toys powered by a microcontroller with a built-in Wi-Fi module. " +
                              "Each user who has access to the aforementioned device with a unique serial code can create an account and register the device to be able to control it.",
                TermsOfService = new Uri("https://mit-license.org/"),
                Contact = new OpenApiContact
                {
                    Name = "Admin",
                    Email = "adrian.bajczyk@gmail.com",
                },
                License = new OpenApiLicense
                {
                    Name = "MIT",
                    Url = new Uri("https://mit-license.org/")
                }
            });
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please enter a valid token",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "Bearer"
            });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type=ReferenceType.SecurityScheme,
                            Id="Bearer"
                        }
                    },
                    new string[]{}
                }
            });

            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
        });

        return services;
    }
}