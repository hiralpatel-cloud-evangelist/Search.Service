using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SearchService.Base.HelperClasses;
using SearchService.DTO.Constants;
using SearchService.DTO.FilterDTO;
using SearchService.DTO.Response;
using SearchService.Models.Tables;
using SearchService.Services.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace SearchService.Services.CQRS.Queries
{
    public class GetPostQuery : IRequest<BlogPostListResponse>
    {
        public SearchRequestModel Filters { get; set; }
        public GetPostQuery(SearchRequestModel filters)
        {
           
            Filters = filters;
        }

        public class GetPostQueryHandler : IRequestHandler<GetPostQuery, BlogPostListResponse>
        {
            private readonly IMapper _mapper;
            private readonly PostBlogsContext _dbContext;

            public GetPostQueryHandler(IMapper mapper, PostBlogsContext dbContext)
            {
                _mapper = mapper;
                _dbContext = dbContext;
            }

            public async Task<BlogPostListResponse> Handle(GetPostQuery query, CancellationToken cancellationToken)
            {
                // Initialize ordering expression
                Expression<Func<BlogPost, dynamic>> TOrderBy = null;

                // Determine ordering expression based on provided sorting column
                TOrderBy = !string.IsNullOrEmpty(query.Filters.SortColumn)
                    ? FilterExtensions.GetDynamicQueryForBlogPosts(query.Filters.SortColumn)
                    : d => d.LastModifiedByUserId;

                // Initialize status filter criteria
                Expression<Func<BlogPost, bool>> criteria = p => p.Status != (int)Status.Delete;

                Expression<Func<BlogPost, bool>> allSearchCriteria = null;

                // Combine search criteria for title and technique using OR operation
                if (!string.IsNullOrEmpty(query.Filters.SearchText))
                {
                    var title = FilterExtensions.GetDynamicQueryForBlogPosts(CommonConstants.PostName, query.Filters.SearchText);
                    var description = FilterExtensions.GetDynamicQueryForBlogPosts(CommonConstants.PostDescription, query.Filters.SearchText);
                    allSearchCriteria = ExpressionHelper.CombineOR(allSearchCriteria, title);
                    allSearchCriteria = ExpressionHelper.CombineOR(allSearchCriteria, description);

                    criteria = ExpressionHelper.CombineAnd(criteria, allSearchCriteria);
                }

                IQueryable<BlogPost> queryableRecords = null;
                if (!string.IsNullOrEmpty(query.Filters.SearchText))
                {
                    //queryableRecords = _getPostQueryRepository.GetManyAsyncReturnQuerable(criteria);
                    queryableRecords = _dbContext.BlogPosts.Where(criteria).AsNoTracking();
                }
                else
                {
                    queryableRecords = _dbContext.BlogPosts.Where(a => a.Status != 3).AsNoTracking();
                }

                // Apply sorting based on sort order
                if (query.Filters.SortOrder == CommonConstants.Desc)
                    queryableRecords = queryableRecords.OrderByDescending(TOrderBy);
                else
                    queryableRecords = queryableRecords.OrderBy(TOrderBy);

                // Initialize a PagedViewResponse to hold paginated data
                var postList = new PagedViewResponse<BlogPost>();

                // Populate the PagedViewResponse using pagination utility
                await Pagination<BlogPost>.Data(query.Filters.PageSize, query.Filters.Page, postList, queryableRecords);

                var blogpostList = _mapper.Map<PagedViewResponse<BlogPostResponse>>(postList);

                // Create and return ApiResponse containing the paginated blog post list
                return new BlogPostListResponse
                {
                    BlogPostList = blogpostList,
                };


            }
        }

    }
}
