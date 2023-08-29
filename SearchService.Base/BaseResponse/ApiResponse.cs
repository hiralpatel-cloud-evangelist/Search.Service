using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace SearchService.Base.BaseResponse
{
    public class HttpStatusCodeException : Exception
    {
        public int StatusCode { get; set; }
        public string ContentType { get; set; } = @"text/plain";
        public string Code { get; set; }

        public HttpStatusCodeException(object status404NotFound, int statusCode)
        {
            this.StatusCode = statusCode;
        }


        public HttpStatusCodeException(int statusCode, string message, string Code = "0") : base(message)
        {
            this.ContentType = @"application/json";
            this.StatusCode = statusCode;
            this.Code = Code;
        }

        public HttpStatusCodeException(int statusCode, Exception inner, string Code = "0") : this(statusCode, inner.ToString(), Code) { }

        public HttpStatusCodeException(int statusCode, JObject errorObject, string Code = "0") : this(statusCode, errorObject.ToString(), Code)
        {
            this.ContentType = @"application/json";
        }
    }

    public class CommonApiResponse
    {

        public object Content { get; set; }
        public int StatusCode { get; set; }
        public int ErrorCode { get; set; }
        public string Errormessage { get; set; }
    }

    public class NotFoundAPIResponse
    {
        /// <summary>
        /// (Conditional) An error code to find help for the exception.
        /// </summary>
        [JsonProperty("code")]

        public int code { get; set; }

        /// <summary>
        /// A more descriptive message regarding the exception.
        /// </summary>
        [JsonProperty("message")]
        public string message { get; set; }

        /// <summary>
        /// The HTTP status code for the exception.
        /// </summary>
        [JsonProperty("status")]
        public int status { get; set; }

    }
}
