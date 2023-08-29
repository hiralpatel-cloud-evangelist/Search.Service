//using Auth_Service.CustomFilters;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SearchService.Services.CQRS.Queries;
using SearchService.DTO.Response;
using SearchService.Models.Tables;
using SearchService.Services.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authorization;

using static SearchService.Services.CQRS.Queries.GetPostQuery;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Identity.Web;
using Search.Service.Helper;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.




builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));


builder.Services.AddAuthorization(config => {

    config.AddPolicy("Read", policyBuilder =>
    {
        policyBuilder.RequireAuthenticatedUser();
        policyBuilder.Requirements.Add(new ScopeAuthorizationRequirement("Post.Read"));
    });

});



builder.Services.AddScoped<IAuthorizationHandler, ScopeAuthorizationHandler>();



#region For Database Connection
try
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    builder.Services.AddDbContext<PostBlogsContext>(options => options.UseSqlServer(connectionString), ServiceLifetime.Scoped);
}
catch (Exception ex)
{
    //logger.Information("serilog added" + ex.Message);
    throw;
}

#endregion
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

builder.Services.AddScoped<IRequestHandler<GetPostQuery, BlogPostListResponse>, GetPostQueryHandler>();


builder.Services.AddBusinessContexts();
builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1",
        new OpenApiInfo
        {
            Title = "Search Service Microservices",
            Version = "v1"
        }
     );
    c.EnableAnnotations(enableAnnotationsForInheritance: true, enableAnnotationsForPolymorphism: true);

    var filePath = Path.Combine(System.AppContext.BaseDirectory, "MyApi.xml");

    c.IncludeXmlComments(filePath);

});
builder.Services.AddSwaggerGenNewtonsoftSupport();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);


var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Search.Service API V1");
    });



}//

app.UseHttpsRedirection();


app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.UseHttpStatusCodeExceptionMiddleware();
app.Run();
