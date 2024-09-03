﻿using System.Net;
using System.Text.Json;
using VigneCommerce.Api.Response.Base;

namespace VigneCommerce.Api.Middlewares
{
    /// <summary>
    /// Middleware de exception durante a execução da API
    /// </summary>
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate next;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="next"></param>
        public ExceptionMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        /// <inheritdoc />
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            ResponseBase response;

            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
                response = new ResponseBase(false, $"Exception: Mensagem: {ex.Message}; InnerException: {ex?.InnerException?.Message}; StackTrace: {ex?.StackTrace}");
            else
                response = new ResponseBase(false, "Ocorreu um erro no processamento da sua requisição. Por favor tente novamente mais tarde.");

            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var result = JsonSerializer.Serialize(response);
            context.Response.ContentType = "application/json";
            return context.Response.WriteAsync(result);
        }
    }
}
