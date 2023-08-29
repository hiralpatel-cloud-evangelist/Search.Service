using SearchService.Base.HelperClasses;
using SearchService.Base.HelperClasses.IHelperClasses;
using Microsoft.Extensions.DependencyInjection;


namespace SearchService.Base.Extensions
{
    public static class ApiConfigurationExtensions
    {
        public static void AddApiBaseContexts(this IServiceCollection services)
        {
            services.AddSingleton<ILoggerManager, LoggerManager>();
        }
    }
}
