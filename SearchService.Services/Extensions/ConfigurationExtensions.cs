using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SearchService.DTO.Response;
using SearchService.Services.CQRS.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SearchService.Services.CQRS.Queries.GetPostQuery;

namespace SearchService.Services.Extensions
{
    public static class ConfigurationExtensions
    {
        public static void AddBusinessContexts(this IServiceCollection services)
        {
            services.AddScoped<IRequestHandler<GetPostQuery, BlogPostListResponse>, GetPostQueryHandler>();
        }
    }
}
