using Application;
using System;

namespace SRP.Models
{
    public class ErrorResponseModel
    {
        public string Type { get; set; }
        public string Message { get; set; }
        public string Name { get; set; }
        public string StackTrace { get; set; }

        public ErrorResponseModel(NotFoundException ex)
        {
            Type = ex.GetType().Name;
            Message = ex.Message;
            StackTrace = ex.ToString();
            Name = ex.LocalizableObjectName;
        }
        public ErrorResponseModel(InvalidSearchParameterException ex)
        {
            Type = ex.GetType().Name;
            Message = ex.Message;
            StackTrace = ex.ToString();
            Name = ex.Parameter;
        }

        public ErrorResponseModel(Exception ex)
        {
            Type = ex.GetType().Name;
            Message = ex.Message;
            StackTrace = ex.ToString();
        }
    }
}
