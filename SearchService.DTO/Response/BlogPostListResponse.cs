using Newtonsoft.Json;
using SearchService.DTO.FilterDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchService.DTO.Response
{
    public class BlogPostListResponse
    {
        [JsonProperty("blog_post_list")]
        public PagedViewResponse<BlogPostResponse> BlogPostList { get; set; }
    }

    public class BlogPostResponse
    {
        [JsonProperty("post_sid")]
        public string? BlogPostSid { get; set; }

        [JsonProperty("post_name")]
        public string? PostName { get; set; }

        [JsonProperty("post_description")]
        public string? PostDescription { get; set; }

        [JsonProperty("blog_image")]
        public string? BlogImage { get; set; }
    }

}
