using GraphQL.Server;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EcoManager.AccountsApi.GraphQl;
using Microsoft.EcoManager.Infrastructure.GraphQl;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Microsoft.EcoManager.AccountsApi
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;


        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }


        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddGraphQL(options =>
                {
                    options.EnableMetrics = false;
                })
                .AddSystemTextJson()
                .AddErrorInfoProvider(opt => opt.ExposeExceptionStackTrace = _env.IsDevelopment())
                .AddDataLoader()
                .AddGraphTypes(typeof(AccountSchema))
                .AddFederation(typeof(AccountSchema).Assembly);
            services.AddSingleton<AccountSchema>();
            services.AddSingleton<AccountRepository>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseGraphQLPlayground("/graphql/playground");
            app.UseGraphQL<AccountSchema>();
        }
    }
}
