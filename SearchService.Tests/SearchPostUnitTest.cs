using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using Search.Service.Controllers;
using SearchService.DTO.FilterDTO;
using SearchService.DTO.Response;
using SearchService.Services.CQRS.Queries;


namespace Search.Service.Tests
{
    public class SearchPostUnitTest
    {
      
        public SearchPostUnitTest()
        {
         

        }



        [Fact]
        public async Task GetBlogs_ReturnsOkResult()
        {
            // Arrange

            var mockMediator = new Mock<IMediator>();
            var controller = new PostsController(mockMediator.Object);

            var searchRequestModel = new SearchRequestModel
            {
                SearchText = string.Empty,
                PageSize = 10,
                Page = 1,
                SortColumn = string.Empty,
                SortOrder = string.Empty
            };

            var response = new BlogPostListResponse
            {
                BlogPostList = new PagedViewResponse<BlogPostResponse>()
                {
                    CurrentPage = 1,
                    NumberofPages = 1,
                    Pages = new List<int> { 1, 2 },
                    PageSize = 1,
                    RecordCount = 1,
                    Data = new List<BlogPostResponse>()
                    {
                       new BlogPostResponse()
                       {
                            BlogPostSid = "NewSID",
                            PostName = "Test",
                            PostDescription = "Test",
                            BlogImage = "image_string"
                       }
                    }
                }
            };

            mockMediator.Setup(m => m.Send(It.IsAny<GetPostQuery>(), CancellationToken.None))
                        .ReturnsAsync(response);

            // Act
            var result = await controller.GetBlogs(searchRequestModel);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            mockMediator.Verify(x => x.Send(It.IsAny<GetPostQuery>(), CancellationToken.None), Times.Once);
            Assert.Equal(200, okResult.StatusCode);
            Assert.NotNull(okResult.Value);
        }

  


    }
}


