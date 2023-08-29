using AutoMapper;
using SearchService.DTO.FilterDTO;
using SearchService.DTO.Response;
using SearchService.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchService.Services.Extensions
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            
            CreateMap<BlogPost, BlogPostResponse>();
            CreateMap<PagedViewResponse<BlogPost>, PagedViewResponse<BlogPostResponse>>();
            

        }
    }
}
