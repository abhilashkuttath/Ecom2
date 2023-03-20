using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Errors
{
    public class ApiException : ApiResponse
    {
        public ApiException(int statusCode, string message = null,string deatils = null) : base(statusCode, message)
        {
            Details =deatils;

        }
        public string Details { get; set; }
    }
}