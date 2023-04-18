using Application;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using SRP.Models;
using System.Net;
using System.Threading.Tasks;
using System;

namespace SRP.Middleware
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await ConvertException(context, ex);
            }
        }

        private Task ConvertException(HttpContext context, Exception exception)
        {
            HttpStatusCode httpStatusCode = HttpStatusCode.InternalServerError;

            context.Response.ContentType = "application/json";

            var result = string.Empty;

            switch (exception)
            {
                case NotFoundException notFoundException:
                    httpStatusCode = HttpStatusCode.NotFound;
                    result = JsonConvert.SerializeObject(new ErrorResponseModel(notFoundException));
                    break;
                case InvalidSearchParameterException invalidSearchParameterException:
                    httpStatusCode = HttpStatusCode.UnprocessableEntity;
                    result = JsonConvert.SerializeObject(new ErrorResponseModel(invalidSearchParameterException));
                    break;
                case Exception ex:
                    httpStatusCode = HttpStatusCode.BadRequest;
                    break;
            }

            context.Response.StatusCode = (int)httpStatusCode;

            if (result == string.Empty)
            {
                result = JsonConvert.SerializeObject(new ErrorResponseModel(exception));
            }
            return context.Response.WriteAsync(result);
        }
    }
}
