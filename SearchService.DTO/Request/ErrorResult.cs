using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchService.DTO.Request
{
    public class ErrorResult
    {
        [JsonProperty(PropertyName = "code")]
        public int Code { get; set; }

        //[JsonProperty(PropertyName = "success")]
        //public string Success { get; set; }

        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; }

        [JsonProperty(PropertyName = "more_info")]
        public string MoreInfo { get; set; }

        //[JsonProperty(PropertyName = "data")]
        //public T Data { get; set; }

        [JsonProperty(PropertyName = "status")]
        public int Status { get; set; }
    }

    public class SucessResult
    {
        [JsonProperty(PropertyName = "code")]
        public int Code { get; set; }

        //[JsonProperty(PropertyName = "success")]
        //public string Success { get; set; }

        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; }

        [JsonProperty(PropertyName = "more_info")]
        public string MoreInfo { get; set; }

        //[JsonProperty(PropertyName = "data")]
        //public T Data { get; set; }

        [JsonProperty(PropertyName = "status")]
        public int Status { get; set; }
    }

    public class FluentErrorResponse
    {
        public int Code { get; set; }
        public int Status { get; set; }
        public JObject Message { get; set; }
        public bool isFluentError { get; set; }
    }
}
