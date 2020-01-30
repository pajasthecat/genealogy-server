using Geneology.Infrastructure.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MediatR;
using Geneology.Api.Middleware;
using Neo4jClient;
using System;
using FluentValidation;
using Geneology.Infrastructure.Repositories.Cache;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Genealogy.Application.Repositories;
using Genealogy.Application.PipelineBehaviors;
using Genealogy.Application.Handlers.CommandHandlers;
using Genealogy.Application.Handlers.QueryHandlers;
using System.Reflection;

namespace Geneology.Api
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
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.Authority = Configuration["Auth0:Domain"];
                options.Audience = Configuration["Auth0:Audience"];
            });
            services.AddSingleton<IFamilyMembersRepository, FamilyMembersRepository>();
            services.Decorate<IFamilyMembersRepository, CachedFamilyMembersRepository>();
            services.AddMediatR(
                typeof(AddFamilyMemberHandler).GetTypeInfo().Assembly,
                typeof(GetFamilyMembersHandler).GetTypeInfo().Assembly,
                typeof(GetFamilyMemberByIdHandler).GetTypeInfo().Assembly);
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddValidatorsFromAssembly(typeof(Startup).Assembly);

            services.AddSingleton<IGraphClient>(context =>
            {
                var graphClient = new GraphClient(
                    new Uri(Configuration["Neo4j:ConnectionString"]),
                    Configuration["Neo4j:User"],
                    Configuration["Neo4j:Password"]);
                graphClient.Connect();
                return graphClient;
            });

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseErrorHandlingMiddleware();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
