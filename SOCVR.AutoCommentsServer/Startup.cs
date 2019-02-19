using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SOCVR.AutoCommentsServer.Services;
using SOCVR.AutoCommentsServer.Services.Abstract;
using SOCVR.AutoCommentsServer.Settings;

namespace SOCVR.AutoCommentsServer
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddTransient<IAutoCommentRepoReader, AutoCommentRepoReader>();
            services.AddTransient<ICommentDataTransformer, CommentDataTransformer>();
            services.AddTransient<IFileSystem, FileSystem>();
            services.AddTransient<IGitInstanceManager, GitInstanceManager>();
            services.AddTransient<IGitManager, GitManager>();
            services.AddTransient<IGitPullCache, GitPullCache>();
            services.AddTransient<IProcessRunner, ProcessRunner>();

            services.AddMemoryCache();

            services.Configure<GitPullCacheSettings>(Configuration.GetSection(nameof(GitPullCacheSettings)));
            services.Configure<GitSettings>(Configuration.GetSection(nameof(GitSettings)));

            // app insights
            if (Configuration.GetValue<bool>("AppInsights:Enabled"))
            {
                services.AddApplicationInsightsTelemetry(o =>
                {
                    o.ApplicationVersion = Configuration["Meta:AppVersion"] ?? "";
                    o.DeveloperMode = Configuration.GetValue<bool?>("AppInsights:DeveloperMode").GetValueOrDefault(false);
                    o.EnableAdaptiveSampling = Configuration.GetValue<bool?>("AppInsights:AdaptiveSampling").GetValueOrDefault(false);
                    o.EnableQuickPulseMetricStream = true;
                    o.InstrumentationKey = Configuration["AppInsights:InstrumentationKey"];
                });
            }

            services.AddTransient<IJavaScriptSnippetFactory, JavaScriptSnippetFactory>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
