using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace CarbonPlatformExercise.Api.Models
{
    public class ResponseModel<TData>
    {
        public ResponseModel()
        {
            Success = true;
            StatusCode = HttpStatusCode.OK;
        }
        public bool Success { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public string Message { get; set; }
        public TData Data { get; set; }
    }
}
