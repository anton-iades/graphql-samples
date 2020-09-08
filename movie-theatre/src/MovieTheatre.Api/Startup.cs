using System.IO;
using GraphQL.Server;
using GraphQL.Server.Ui.Playground;
using GraphQL.Server.Ui.Voyager;
using GraphQL.Types;
using GraphQL.Utilities.Federation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MovieTheatre.Api.Resolvers;

namespace MovieTheatre.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add Mediatr
            services.AddMediatR(typeof(Startup));

            // Add GraphQL services and configure options
            services
                .AddSingleton<TopLevelResolver>()
                .AddSingleton<AnyScalarGraphType>()
                .AddSingleton<ServiceGraphType>()
                .AddSingleton(provider =>
                {
                    return FederatedSchema.For(File.ReadAllText("schema.gql"), schemaBuilder =>
                    {
                        schemaBuilder.ServiceProvider = provider;
                        schemaBuilder.Types.Include<TopLevelResolver>();
                    });
                })
                .AddGraphQL((options, provider) =>
                {
                    var env = provider.GetRequiredService<IWebHostEnvironment>();
                    options.EnableMetrics = env.IsDevelopment();
                    var logger = provider.GetRequiredService<ILogger<Startup>>();
                    options.UnhandledExceptionDelegate = ctx => logger.LogError("{Error} occured", ctx.OriginalException.Message);
                })
                .AddSystemTextJson(deserializerSettings => { }, serializerSettings => { })
                .AddErrorInfoProvider((opt, provider) =>
                {
                    var env = provider.GetRequiredService<IWebHostEnvironment>();
                    opt.ExposeExceptionStackTrace = env.IsDevelopment();
                })
                .AddDataLoader();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app
                .UseGraphQL<ISchema>("/gql")
                .UseGraphQLPlayground(new GraphQLPlaygroundOptions { GraphQLEndPoint = "/gql" })
                .UseGraphQLVoyager(new GraphQLVoyagerOptions { GraphQLEndPoint = "/gql" });
        }
    }
}
