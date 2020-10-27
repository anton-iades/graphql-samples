using System;
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
using ReviewService.Api.Models;
using ReviewService.Api.Queries;
using ReviewService.Api.Resolvers;

namespace ReviewService.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMediatR(typeof(Startup));

            // Add GraphQL services and configure options
            services
                .AddSingleton<TopLevelResolver>()
                .AddSingleton<MovieResolver>()
                .AddSingleton<ReviewResolver>()
                .AddSingleton<AnyScalarGraphType>()
                .AddSingleton<ServiceGraphType>()
                .AddSingleton(provider =>
                {
                    return FederatedSchema.For(File.ReadAllText("schema.gql"), schemaBuilder =>
                    {
                        schemaBuilder.ServiceProvider = provider;
                        schemaBuilder.Types.Include<TopLevelResolver>();
                        schemaBuilder.Types.Include<MovieResolver>();
                        schemaBuilder.Types.Include<ReviewResolver>();
                        // reference resolvers
                        schemaBuilder.Types.For("Review").ResolveReferenceAsync(ctx =>
                        {
                            var mediator = provider.GetRequiredService<IMediator>();
                            var reviewId = (string)ctx.Arguments["id"];
                            return mediator.Send(new GetReviewById.Request(reviewId));
                        });
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

            services.AddSingleton<GetReviewsContext>(serviceProvider => () => new[]
            {
                new Review {Id = "1111", MovieId = "111", Stars = 4.7, Message = "Outstanding movie with a haunting performance and best character development ever seen.", ContainsSpoilers = false, Created = new DateTime(2019,09,10)},
                new Review {Id = "1112", MovieId = "111", Stars = 4.4, Message = "A psychological study, rather than a superhero flick.", ContainsSpoilers = true, Created = new DateTime(2019,10,03)},
                new Review {Id = "1113", MovieId = "111", Stars = 3.9, Message = "Joaquin 'OSCAR', Joker = best Dark suspense thriller ... Darker than dark Knight.", ContainsSpoilers = false, Created = new DateTime(2019,09,10)},
                new Review {Id = "2221", MovieId = "222", Stars = 4.7, Message = "For the first time in forever a true Disney classic is realized.", ContainsSpoilers = true, Created = new DateTime(2019,10,07)},
                new Review {Id = "2222", MovieId = "222", Stars = 4.2, Message = "A funny and unpredictable Disney Princess musical.", ContainsSpoilers = false, Created = new DateTime(2019,09,01)},
                new Review {Id = "3331", MovieId = "333", Stars = 4.8, Message = "Downright not awful!", ContainsSpoilers = false, Created = new DateTime(2019,10,04)}
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            app
                .UseGraphQL<ISchema>("/gql")
                .UseGraphQLPlayground(new GraphQLPlaygroundOptions { GraphQLEndPoint = "/gql" })
                .UseGraphQLVoyager(new GraphQLVoyagerOptions { GraphQLEndPoint = "/gql" });
        }
    }
}