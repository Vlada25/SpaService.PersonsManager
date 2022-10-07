﻿using Microsoft.AspNetCore.Diagnostics;
using PersonsManager.Domain.ErrorModels;
using System.Net;

namespace PersonsManager.API.Extensions
{
    public static class ExeptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();

                    if (contextFeature != null)
                    {
                        await context.Response.WriteAsync(new ErrorDetails()
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = "Internal Server Error."

                        }.ToString());
                    }
                });
            });
        }
    }
}
