﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Diagnostics;
using Newtonsoft.Json;

namespace Bliss.Recruitment.Api.Exceptions
{
    public static class GlobalExceptionHandler
    {
		public static void UseGlobalExceptionHandler(this IApplicationBuilder app, ILoggerFactory loggerFactory)
		{
			app.UseExceptionHandler(builder =>
			{
				builder.Run(async context =>
				{
					var exceptionHandlerFeature = context.Features.Get<IExceptionHandlerFeature>();

					if (exceptionHandlerFeature != null)
					{
						var exception = exceptionHandlerFeature.Error;

						var problemDetails = new ProblemDetails
						{
							Instance = context.Request.HttpContext.Request.Path
						};

						if (exception is BadHttpRequestException badHttpRequestException)
						{
							problemDetails.Title = "The request is invalid";
							problemDetails.Status = StatusCodes.Status400BadRequest;
							problemDetails.Detail = badHttpRequestException.Message;
						}
						else
						{
							var logger = loggerFactory.CreateLogger("GlobalExceptionHandler");
							logger.LogError($"Unexpected error: {exceptionHandlerFeature.Error}");

							problemDetails.Title = exception.Message;
							problemDetails.Status = StatusCodes.Status500InternalServerError;
							problemDetails.Detail = exception?.StackTrace;
						}

						context.Response.StatusCode = problemDetails.Status.Value;
						context.Response.ContentType = "application/problem+json";

						var json = System.Text.Json.JsonSerializer.Serialize(problemDetails);
						await context.Response.WriteAsync(json);
					}
				});
			});
		}

	}
}
