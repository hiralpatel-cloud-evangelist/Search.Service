using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SearchService.DTO.FilterDTO;
using SearchService.DTO.Response;
using SearchService.Services.CQRS.Queries;
using SearchService.Helper;
using SearchService.DTO.Request;

namespace Search.Service.Controllers
{
    /// <summary>
    /// API controller for managing blog posts.
    /// </summary>
    [ApiController]
    [ValidateModel]
    [Route("v1/posts")]
    [Authorize()]
    public class PostsController : ControllerBase
    {

        private IMediator _mediator;


        public PostsController(IMediator mediator)
        {

            _mediator = mediator;
        }

        /// <summary>
        /// Retrieves a list of blog posts based on search criteria.
        /// </summary>
        /// <param name="searchRequestModel">The search criteria.</param>
        /// <returns>An <see cref="BlogPostListResponse"/> containing the list of blog posts.</returns> 
        /// <summary>
        /// Retrieves a list of blog posts based on search criteria.
        /// </summary>
        /// <response code="200">OK: The request was successful and the response body contains the representation requested.</response>
        /// <response code="401">UNAUTHORIZED: The supplied credentials, if any, are not sufficient to access the resource.</response>
        /// <response code="500">SERVER ERROR: We couldn't return the representation due to an internal server error.</response>
        /// <returns code="429">TOO MANY REQUESTS: Your application is sending too many simultaneous requests.</returns>
        [ProducesResponseType(typeof(BlogPostListResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(void), StatusCodes.Status429TooManyRequests)]
        [HttpGet]
        [Authorize(Policy = "Read")]
        public async Task<IActionResult> GetBlogs([FromQuery] SearchRequestModel searchRequestModel)
        {

            var response = await _mediator.Send(new GetPostQuery(searchRequestModel));
            return Ok(response);

        }





    }
}
