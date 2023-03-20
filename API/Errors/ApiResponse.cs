using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Errors
{
    public class ApiResponse
    {
        public ApiResponse(int statusCode, string message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessageForSatusCode(statusCode); 
           //null coliesing operator ,if message is null the execute that method
        }

        public int StatusCode { get; set; }
        public string Message { get; set; }

         private string GetDefaultMessageForSatusCode(int statusCode)
        {
            return statusCode switch
            {
                400 => "Bad request ,you have made",
                401 => "Authorized ,yu are not",
                404 => "Resource found, it was not",
                500 => "server Error",
                _ => null
            };
        }

    }
}